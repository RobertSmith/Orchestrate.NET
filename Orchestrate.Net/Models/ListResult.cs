using System.Collections.Generic;

namespace Orchestrate.Net
{
    public class ListResult
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public IEnumerable<Result> Results { get; set; }
    }
}
