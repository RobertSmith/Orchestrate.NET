﻿using System.Collections.Generic;

namespace Orchestrate.Net
{
    public class EventResultList
    {
        public int Count { get; set; }
        public string Kind { get; set; }
        public IEnumerable<EventResult> Results { get; set; }
    }
}
