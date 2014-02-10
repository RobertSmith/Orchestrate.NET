using Newtonsoft.Json;

namespace Orchestrate.Net
{
    public class OrchestratePath
    {
        [JsonProperty("collection")]
        public string Collection { get; set; }
        [JsonProperty("key")]
        public string Key { get; set; }
        [JsonProperty("ref")]
        public string Ref { get; set; }
    }
}
