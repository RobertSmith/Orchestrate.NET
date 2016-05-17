using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Orchestrate.Net
{
    public partial class Orchestrate
    {
        public SearchResult GetGraph(string collectionName, string key, string[] kinds, int limit = 10, int offset = 0)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (kinds == null || kinds.Length == 0)
                throw new ArgumentNullException(nameof(kinds), "kinds cannot be null or empty");

            if (limit < 1 || limit > 100)
                throw new ArgumentOutOfRangeException(nameof(limit), "limit must be between 1 and 100");

            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset), "offset must be at least 0");

            var url = _urlBase + collectionName + "/" + key + "/relations";
            url = kinds.Aggregate(url, (current, kind) => current + ("/" + kind));
            url = url + "?limit=" + limit + "&offset=" + offset;

            return JsonConvert.DeserializeObject<SearchResult>(Communication.CallWebRequest(_apiKey, url, "GET", null).Payload);
        }

        public Result PutGraph(string collectionName, string key, string kind, string toCollectionName, string toKey)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (string.IsNullOrWhiteSpace(kind))
                throw new ArgumentNullException(nameof(kind), "kind cannot be null or empty");

            if (string.IsNullOrWhiteSpace(toCollectionName))
                throw new ArgumentNullException(nameof(toCollectionName), "toCollectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(toKey))
                throw new ArgumentNullException(nameof(toKey), "toKey cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key + "/relation/" + kind + "/" + toCollectionName + "/" + toKey;

            var baseResult = Communication.CallWebRequest(_apiKey, url, "PUT", null);

            return BuildResult(collectionName, key, baseResult);
        }

        public Result DeleteGraph(string collectionName, string key, string kind, string toCollectionName, string toKey)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (string.IsNullOrWhiteSpace(kind))
                throw new ArgumentNullException(nameof(kind), "kind cannot be null or empty");

            if (string.IsNullOrWhiteSpace(toCollectionName))
                throw new ArgumentNullException(nameof(toCollectionName), "toCollectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(toKey))
                throw new ArgumentNullException(nameof(toKey), "toKey cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key + "/relation/" + kind + "/" + toCollectionName + "/" + toKey + "?purge=true";

            var baseResult = Communication.CallWebRequest(_apiKey, url, "DELETE", null);

            return BuildResult(collectionName, key, baseResult);
        }

        public async Task<ListResult> GetGraphAsync(string collectionName, string key, string[] kinds)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (kinds == null || kinds.Length == 0)
                throw new ArgumentNullException(nameof(kinds), "kinds cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key + "/relations";

            url = kinds.Aggregate(url, (current, kind) => current + ("/" + kind));
            var result = await Communication.CallWebRequestAsync(_apiKey, url, "GET", null);

            return JsonConvert.DeserializeObject<ListResult>(result.Payload);
        }

        public async Task<Result> PutGraphAsync(string collectionName, string key, string kind, string toCollectionName, string toKey)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (string.IsNullOrWhiteSpace(kind))
                throw new ArgumentNullException(nameof(kind), "kind cannot be null or empty");

            if (string.IsNullOrWhiteSpace(toCollectionName))
                throw new ArgumentNullException(nameof(toCollectionName), "toCollectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(toKey))
                throw new ArgumentNullException(nameof(toKey), "toKey cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key + "/relation/" + kind + "/" + toCollectionName + "/" + toKey;

            var baseResult = await Communication.CallWebRequestAsync(_apiKey, url, "PUT", null);

            return BuildResult(collectionName, key, baseResult);
        }

        public async Task<Result> DeleteGraphAsync(string collectionName, string key, string kind, string toCollectionName, string toKey)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (string.IsNullOrWhiteSpace(kind))
                throw new ArgumentNullException(nameof(kind), "kind cannot be null or empty");

            if (string.IsNullOrWhiteSpace(toCollectionName))
                throw new ArgumentNullException(nameof(toCollectionName), "toCollectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(toKey))
                throw new ArgumentNullException(nameof(toKey), "toKey cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key + "/relation/" + kind + "/" + toCollectionName + "/" + toKey + "?purge=true";

            var baseResult = await Communication.CallWebRequestAsync(_apiKey, url, "DELETE", null);

            return BuildResult(collectionName, key, baseResult);
        }
    }
}
