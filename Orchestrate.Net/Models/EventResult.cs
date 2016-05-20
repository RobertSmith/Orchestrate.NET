namespace Orchestrate.Net
{
    public class EventResult
    {
        public Path Path { get; set; }
        public long TimeStamp { get; set; }
        public object Value { get; set; }
        public long Ordinal { get; set; }
    }
}
