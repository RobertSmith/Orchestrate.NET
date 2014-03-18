using System.Collections.Generic;
using Newtonsoft.Json;

namespace Orchestrate.Net
{
    public class SearchResult
    {
        public int Count { get; set; }
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }
        public IEnumerable<Result> Results { get; set; } 
    }
}
