using System.Collections.Generic;
using Newtonsoft.Json;

namespace Orchestrate.Net
{
    public class SearchResult
    {
        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }
        [JsonProperty("results")]
        public IEnumerable<Result> Results { get; set; } 
    }
}
