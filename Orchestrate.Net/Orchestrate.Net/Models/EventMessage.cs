using Newtonsoft.Json;

namespace Orchestrate.Net
{
    public class EventMessage
    {
        [JsonProperty("msg")]
        public string Msg { get; set; }
    }
}
