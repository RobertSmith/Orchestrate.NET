using System.Collections.Generic;
using Newtonsoft.Json;

namespace Orchestrate.Net.Models
{
    public class BulkResult
    {
        public string Status { get; set; }
        [JsonProperty("success_count")]
        public int SuccessCount { get; set; }
        public string Message { get; set; }
        public IEnumerable<BulkItemResult> Results { get; set; }
    }
}
