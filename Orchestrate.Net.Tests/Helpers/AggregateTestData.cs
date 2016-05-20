using System;
using System.Security.Cryptography.X509Certificates;

namespace Orchestrate.Net.Tests.Helpers
{
    public class AggregateTestData
    {
        public string Category { get; set; }
        public string Manufacturer { get; set; }
        public string ItemName { get; set; }
        public DateTime SaleDate { get; set; }
        public double SalePrice { get; set; }
        public int SaleCount { get; set; }
        public Location Distance { get; set; }
    }
}
