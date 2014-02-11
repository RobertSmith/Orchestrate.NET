using Newtonsoft.Json;

namespace Orchestrate.Net
{
    public class Result
    {
        [JsonProperty("path")]
        public OrchestratePath Path { get; set; }
        [JsonProperty("score")]
        public decimal Score { get; set; }
        [JsonProperty("value")]
        public object Value { get; set; }
    }
}
