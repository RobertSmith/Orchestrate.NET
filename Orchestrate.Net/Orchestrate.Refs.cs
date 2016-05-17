using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Orchestrate.Net
{
    public partial class Orchestrate
    {
        public Result Ref(string collectionName, string key, string reference)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (string.IsNullOrWhiteSpace(reference))
                throw new ArgumentNullException(nameof(reference), "reference cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key + "/refs/" + reference;
            var baseResult = Communication.CallWebRequest(_apiKey, url, "GET", null);

            return BuildResult(collectionName, key, baseResult);
        }

        public ListResult RefList(string collectionName, string key, int limit = 10, int offset = 0, bool values = false)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (limit < 1 || limit > 100)
                throw new ArgumentOutOfRangeException(nameof(limit), "limit must be between 1 and 100");

            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset), "offset must be at least 0");

            var url = _urlBase + collectionName + "/" + key + "/refs/?limit=" + limit + "&offset=" + offset + "&values=" + values;

            return JsonConvert.DeserializeObject<ListResult>(Communication.CallWebRequest(_apiKey, url, "GET", null).Payload);
        }

        public async Task<Result> RefAsync(string collectionName, string key, string reference)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (string.IsNullOrWhiteSpace(reference))
                throw new ArgumentNullException(nameof(reference), "reference cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key + "/refs/" + reference;
            var baseResult = await Communication.CallWebRequestAsync(_apiKey, url, "GET", null);

            return BuildResult(collectionName, key, baseResult);
        }

        public async Task<ListResult> RefListAsync(string collectionName, string key, int limit = 10, int offset = 0, bool values = false)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (limit < 1 || limit > 100)
                throw new ArgumentOutOfRangeException(nameof(limit), "limit must be between 1 and 100");

            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset), "offset must be at least 0");

            var url = _urlBase + collectionName + "/" + key + "/refs/?limit=" + limit + "&offset=" + offset + "&values=" + values;
            var result = await Communication.CallWebRequestAsync(_apiKey, url, "GET", null);

            return JsonConvert.DeserializeObject<ListResult>(result.Payload);
        }
    }
}
