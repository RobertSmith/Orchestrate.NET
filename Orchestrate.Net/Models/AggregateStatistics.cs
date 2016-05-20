using Newtonsoft.Json;

namespace Orchestrate.Net.Models
{
    public class AggregateStatistics
    {
        public double Min { get; set; }
        public double Max { get; set; }
        public double Mean { get; set; }
        public double Sum { get; set; }
        [JsonProperty("sum_of_squares")]
        public double SumOfSquares { get; set; }
        public double Variance { get; set; }
        [JsonProperty("std_dev")]
        public double StdDev { get; set; }
    }
}