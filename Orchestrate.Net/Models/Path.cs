using Newtonsoft.Json;

namespace Orchestrate.Net
{
    public class Path
    {
        public string Collection { get; set; }
        public string Key { get; set; }
        public string Kind { get; set; }
        public string Ref { get; set; }
        public string RefTime { get; set; }
        public string TimeStamp { get; set; }
        public long Ordinal { get; set; }
        [JsonProperty("ordinal_str")]
        public string OrdinalStr { get; set; }
    }
}
