using Newtonsoft.Json;

namespace Orchestrate.Net.Models
{
    public class PatchItemInt
    {
        [JsonProperty("op")]
        public string Op { get; set; }
        [JsonProperty("from")]
        public string From { get; set; }
        [JsonProperty("path")]
        public string Path { get; set; }
        [JsonProperty("value")]
        public int Value { get; set; }
    }
}
