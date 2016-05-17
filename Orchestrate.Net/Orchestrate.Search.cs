using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Orchestrate.Net
{
    public partial class Orchestrate
    {
        public SearchResult Search(string collectionName, string query, int limit = 10, int offset = 0, string sort = "", string aggregate = "")
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(query))
                throw new ArgumentNullException(nameof(query), "query cannot be null or empty");

            if (limit < 1 || limit > 100)
                throw new ArgumentOutOfRangeException(nameof(limit), "limit must be between 1 and 100");

            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset), "offset must be at least 0");

            var url = _urlBase + collectionName + "?query=" + query + "&limit=" + limit + "&offset=" + offset;

            if (!string.IsNullOrWhiteSpace(sort))
                url = url + "&sort=" + sort;

            if (!string.IsNullOrWhiteSpace(aggregate))
                url = url + "&aggregate=" + aggregate;

            return JsonConvert.DeserializeObject<SearchResult>(Communication.CallWebRequest(_apiKey, url, "GET", null).Payload);
        }

        public async Task<SearchResult> SearchAsync(string collectionName, string query, int limit = 10, int offset = 0, string sort = "", string aggregate = "")
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(query))
                throw new ArgumentNullException(nameof(query), "query cannot be null or empty");

            if (limit < 1 || limit > 100)
                throw new ArgumentOutOfRangeException(nameof(limit), "limit must be between 1 and 100");

            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset), "offset must be at least 0");

            var url = _urlBase + collectionName + "?query=" + query + "&limit=" + limit + "&offset=" + offset;

            if (!string.IsNullOrWhiteSpace(sort))
                url = url + "&sort=" + sort;

            if (!string.IsNullOrWhiteSpace(aggregate))
                url = url + "&aggregate=" + aggregate;

            var result = await Communication.CallWebRequestAsync(_apiKey, url, "GET", null);

            return JsonConvert.DeserializeObject<SearchResult>(result.Payload);
        }
    }
}
