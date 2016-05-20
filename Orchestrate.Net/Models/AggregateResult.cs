using System.Collections.Generic;

namespace Orchestrate.Net.Models
{
    public class AggregateResult : SearchResult
    {
        public IEnumerable<Aggregates> Aggregates { get; set; }
    }
}
