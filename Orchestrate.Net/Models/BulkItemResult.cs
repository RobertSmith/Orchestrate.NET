using Newtonsoft.Json;

namespace Orchestrate.Net.Models
{
    public class BulkItemResult
    {
        public string Status { get; set; }
        [JsonProperty("operation_index")]
        public int OperationIndex { get; set; }
        public Result Item { get; set; }
        public BulkError Error { get; set; }
    }
}
