using System.Collections.Generic;
using Newtonsoft.Json;

namespace Orchestrate.Net.Models
{
    public class Aggregates
    {
        [JsonProperty("aggregate_kind")]
        public string AggregateKind { get; set; }
        [JsonProperty("field_name")]
        public string FieldName { get; set; }
        [JsonProperty("value_count")]
        public string ValueCount { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
        public IEnumerable<AggregateEntries> Entries { get; set; }
        public AggregateStatistics Statistics { get; set; }
        public IEnumerable<AggregateBuckets> Buckets { get; set; }
    }
}
