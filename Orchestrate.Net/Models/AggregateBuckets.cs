namespace Orchestrate.Net.Models
{
    public class AggregateBuckets
    {
        public string Bucket { get; set;  }
        public double Max { get; set; }
        public double Min { get; set; }
        public long Count { get; set; }
    }
}
