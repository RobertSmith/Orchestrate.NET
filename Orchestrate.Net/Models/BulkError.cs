using System.Collections.Generic;

namespace Orchestrate.Net.Models
{
    public class BulkError
    {
        public string Message { get; set; }
        public string Code { get; set; }
        public BulkInfo Details { get; set; }
    }
}
