namespace Orchestrate.Net
{
    public class Result
    {
        public Path Path { get; set; }
        public decimal Score { get; set; }
        public object Value { get; set; }
        public string Kind { get; set; }
        public long RefTime { get; set; }
    }
}
