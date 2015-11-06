using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<GeoDistanceAggregation>))]
	public interface IGeoDistanceAggregation : IBucketAggregation
	{
		[JsonProperty("field")]
		Field Field { get; set; }

		[JsonProperty("origin")]
		GeoLocation Origin { get; set; }

		[JsonProperty("unit")]
		GeoUnit? Unit { get; set; }

		[JsonProperty("distance_type")]
		GeoDistance? DistanceType { get; set; }

		[JsonProperty(PropertyName = "ranges")]
		IEnumerable<Range<double>> Ranges { get; set; }
	}

	public class GeoDistanceAggregation : BucketAggregationBase, IGeoDistanceAggregation
	{
		public Field Field { get; set; }

		public GeoLocation Origin { get; set; }

		public GeoUnit? Unit { get; set; }

		public GeoDistance? DistanceType { get; set; }

		public IEnumerable<Range<double>> Ranges { get; set; }

		internal GeoDistanceAggregation() { }

		public GeoDistanceAggregation(string name) : base(name) { }

		internal override void WrapInContainer(AggregationContainer c) => c.GeoDistance = this;
	}

	public class GeoDistanceAggregationDescriptor<T> :
		BucketAggregationDescriptorBase<GeoDistanceAggregationDescriptor<T>, IGeoDistanceAggregation, T>
			, IGeoDistanceAggregation
		where T : class
	{
		Field IGeoDistanceAggregation.Field { get; set; }

		GeoLocation IGeoDistanceAggregation.Origin { get; set; }

		GeoUnit? IGeoDistanceAggregation.Unit { get; set; }

		GeoDistance? IGeoDistanceAggregation.DistanceType { get; set; }

		IEnumerable<Range<double>> IGeoDistanceAggregation.Ranges { get; set; }

		public GeoDistanceAggregationDescriptor<T> Field(string field) => Assign(a => a.Field = field);

		public GeoDistanceAggregationDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		public GeoDistanceAggregationDescriptor<T> Origin(double lat, double lon) => Assign(a => a.Origin = new GeoLocation(lat, lon));

		public GeoDistanceAggregationDescriptor<T> Origin(GeoLocation geoLocation) => Assign(a => a.Origin = geoLocation);

		public GeoDistanceAggregationDescriptor<T> Unit(GeoUnit unit) => Assign(a => a.Unit = unit);

		public GeoDistanceAggregationDescriptor<T> DistanceType(GeoDistance geoDistance) => Assign(a => a.DistanceType = geoDistance);

		public GeoDistanceAggregationDescriptor<T> Ranges(params Func<Range<double>, Range<double>>[] ranges) =>
			Assign(a => a.Ranges = (from range in ranges let r = new Range<double>() select range(r)).ToListOrNullIfEmpty());

	}
}