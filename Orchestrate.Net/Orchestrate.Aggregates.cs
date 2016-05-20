using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Orchestrate.Net.Models;

namespace Orchestrate.Net
{
    public partial class Orchestrate
    {
        public AggregateResult Aggregate(string collectionName, string query, string aggregate)
        {
            if (string.IsNullOrWhiteSpace(query))
                query = "*";

            if (string.IsNullOrWhiteSpace(aggregate))
                throw new ArgumentNullException(nameof(aggregate), "aggregate cannot be null or empty");

            var url = _urlBase + collectionName + "?query=" + query + "&aggregate=" + aggregate;

            return JsonConvert.DeserializeObject<AggregateResult>(Communication.CallWebRequest(_apiKey, url, "GET", null).Payload);
        }

        public async Task<AggregateResult> AggregateAsync(string collectionName, string query, string aggregate)
        {
            if (string.IsNullOrWhiteSpace(query))
                query = "*";

            if (string.IsNullOrWhiteSpace(aggregate))
                throw new ArgumentNullException(nameof(aggregate), "aggregate cannot be null or empty");

            var url = _urlBase + collectionName + "?query=" + query + "&aggregate=" + aggregate;

            var result = await Communication.CallWebRequestAsync(_apiKey, url, "GET", null);

            return JsonConvert.DeserializeObject<AggregateResult>(result.Payload);
        }
    }
}
