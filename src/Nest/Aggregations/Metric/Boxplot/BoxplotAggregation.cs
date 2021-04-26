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

using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A metrics aggregation that computes boxplot of numeric values extracted from the aggregated documents.
	/// These values can be generated by a provided script or extracted from specific numeric or histogram fields in the documents.
	/// <para />
	/// Available in Elasticsearch 7.7.0+ with at least basic license level
	/// </summary>
	[InterfaceDataContract]
	[ReadAs(typeof(BoxplotAggregation))]
	public interface IBoxplotAggregation : IMetricAggregation
	{
		/// <summary>
		/// Balances memory utilization with estimation accuracy.
		/// Increasing compression, increases the accuracy of percentiles at the cost
		/// of more memory. Larger compression values also make the algorithm slower since the underlying tree data structure grows in size,
		/// resulting in more expensive operations.
		/// </summary>
		[DataMember(Name = "compression")]
		double? Compression { get; set; }
	}

	/// <inheritdoc cref="IBoxplotAggregation"/>
	public class BoxplotAggregation : MetricAggregationBase, IBoxplotAggregation
	{
		internal BoxplotAggregation() { }

		public BoxplotAggregation(string name, Field field) : base(name, field) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Boxplot = this;

		/// <inheritdoc />
		public double? Compression { get; set; }
	}

	/// <inheritdoc cref="IBoxplotAggregation"/>
	public class BoxplotAggregationDescriptor<T>
		: MetricAggregationDescriptorBase<BoxplotAggregationDescriptor<T>, IBoxplotAggregation, T>
			, IBoxplotAggregation
		where T : class
	{
		double? IBoxplotAggregation.Compression { get; set; }

		/// <inheritdoc cref="IBoxplotAggregation.Compression"/>
		public BoxplotAggregationDescriptor<T> Compression(double? compression) =>
			Assign(compression, (a, v) => a.Compression = v);
	}
}
