/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using Xunit;

namespace Tests.XPack.MachineLearning.PutDatafeed
{
	public class PutDatafeedApiTests
		: MachineLearningIntegrationTestBase<PutDatafeedResponse,
			IPutDatafeedRequest, PutDatafeedDescriptor<Metric>, PutDatafeedRequest>
	{
		private IElasticClient _client;

		public PutDatafeedApiTests(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) => 
			_client = cluster.Client;

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			indices = new[] { "server-metrics" },
			job_id = CallIsolatedValue,
			query = new
			{
				match_all = new { }
			},
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<PutDatafeedDescriptor<Metric>, IPutDatafeedRequest> Fluent => f => f
			.Indices(typeof(Metric)) //goes on body not in the url
			.JobId(CallIsolatedValue)
			.Query(q => q.MatchAll());

		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override PutDatafeedRequest Initializer =>
			new PutDatafeedRequest(CallIsolatedValue)
			{
				JobId = CallIsolatedValue,
				Indices = typeof(Metric),
				Query = new MatchAllQuery()
			};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"_ml/datafeeds/{CallIsolatedValue}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MachineLearning.PutDatafeed(CallIsolatedValue, f),
			(client, f) => client.MachineLearning.PutDatafeedAsync(CallIsolatedValue, f),
			(client, r) => client.MachineLearning.PutDatafeed(r),
			(client, r) => client.MachineLearning.PutDatafeedAsync(r)
		);

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values) PutJob(client, callUniqueValue.Value);
		}

		protected override PutDatafeedDescriptor<Metric> NewDescriptor() => new PutDatafeedDescriptor<Metric>(CallIsolatedValue);

		protected override void ExpectResponse(PutDatafeedResponse response)
		{
			response.ShouldBeValid();

			response.DatafeedId.Should().NotBeNullOrWhiteSpace();
			response.JobId.Should().Be(CallIsolatedValue);

			response.QueryDelay.Should().NotBeNull("QueryDelay");
			response.QueryDelay.Should().BeGreaterThan(new Time("1nanos"));

			response.Indices.Should().NotBeNull("Indices");
			response.Indices.Match(
				all => { Assert.True(false); },
				many => { many.Indices.Should().HaveCount(1).And.Contain(_client.Infer.IndexName(typeof(Metric))); });

			response.ScrollSize.Should().Be(1000);

			response.ChunkingConfig.Should().NotBeNull();
			response.ChunkingConfig.Mode.Should().Be(ChunkingMode.Auto);
			response.Query.Should().NotBeNull();
		}
	}
}
