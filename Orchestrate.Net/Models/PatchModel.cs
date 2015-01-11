using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orchestrate.Net.Models
{
    public class PatchModel
    {
        public string op { get; set; }
        public string value { get; set; }
        public string path { get; set; }
        public string from { get; set; }

    }

}
