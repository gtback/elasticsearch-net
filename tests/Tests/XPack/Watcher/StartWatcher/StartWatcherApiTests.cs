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
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.Watcher.StartWatcher
{
	public class StartWatcherApiTests
		: ApiIntegrationTestBase<WatcherStateCluster, StartWatcherResponse, IStartWatcherRequest, StartWatcherDescriptor, StartWatcherRequest>
	{
		public StartWatcherApiTests(WatcherStateCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;

		protected override Func<StartWatcherDescriptor, IStartWatcherRequest> Fluent => f => f;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override StartWatcherRequest Initializer => new StartWatcherRequest();

		protected override string UrlPath => "/_watcher/_start";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Watcher.Start(f),
			(client, f) => client.Watcher.StartAsync(f),
			(client, r) => client.Watcher.Start(r),
			(client, r) => client.Watcher.StartAsync(r)
		);

		protected override void ExpectResponse(StartWatcherResponse response) => response.Acknowledged.Should().BeTrue();
	}
}
