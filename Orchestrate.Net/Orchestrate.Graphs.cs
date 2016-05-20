using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Orchestrate.Net
{
    public partial class Orchestrate
    {
        #region Get Graph

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

        #endregion

        #region Put Graph

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

        public Result PutGraph(string collectionName, string key, string kind, string toCollectionName, string toKey, object properties)
        {
            if (properties == null)
                PutGraph(collectionName, key, kind, toCollectionName, toKey);

            var json = JsonConvert.SerializeObject(properties);

            return PutGraph(collectionName, key, kind, toCollectionName, toKey, json);
        }

        public Result PutGraph(string collectionName, string key, string kind, string toCollectionName, string toKey, string properties)
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

            var baseResult = Communication.CallWebRequest(_apiKey, url, "PUT", properties);

            return BuildResult(collectionName, key, baseResult);
        }

        public async Task<Result> PutGraphAsync(string collectionName, string key, string kind, string toCollectionName, string toKey, object properties)
        {
            if (properties == null)
                PutGraph(collectionName, key, kind, toCollectionName, toKey);

            var json = JsonConvert.SerializeObject(properties);

            return await PutGraphAsync(collectionName, key, kind, toCollectionName, toKey, json);
        }

        public async Task<Result> PutGraphAsync(string collectionName, string key, string kind, string toCollectionName, string toKey, string properties)
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

            var baseResult = await Communication.CallWebRequestAsync(_apiKey, url, "PUT", properties);

            return BuildResult(collectionName, key, baseResult);
        }

        public Result PutGraphIfMatch(string collectionName, string key, string kind, string toCollectionName, string toKey, string ifMatch)
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

            var baseResult = Communication.CallWebRequest(_apiKey, url, "PUT", null, ifMatch);

            return BuildResult(collectionName, key, baseResult);
        }

        public async Task<Result> PutGraphIfMatchAsync(string collectionName, string key, string kind, string toCollectionName, string toKey, string ifMatch)
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

            var baseResult = await Communication.CallWebRequestAsync(_apiKey, url, "PUT", null, ifMatch);

            return BuildResult(collectionName, key, baseResult);
        }

        public Result PutGraphIfMatch(string collectionName, string key, string kind, string toCollectionName, string toKey, string ifMatch, object properties)
        {
            if (properties == null)
                PutGraph(collectionName, key, kind, toCollectionName, toKey);

            var json = JsonConvert.SerializeObject(properties);

            return PutGraphIfMatch(collectionName, key, kind, toCollectionName, toKey, json, ifMatch);
        }

        public Result PutGraphIfMatch(string collectionName, string key, string kind, string toCollectionName, string toKey, string ifMatch, string properties)
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

            var baseResult = Communication.CallWebRequest(_apiKey, url, "PUT", properties, ifMatch);

            return BuildResult(collectionName, key, baseResult);
        }

        public async Task<Result> PutGraphIfMatchAsync(string collectionName, string key, string kind, string toCollectionName, string toKey, string ifMatch, object properties)
        {
            if (properties == null)
                PutGraph(collectionName, key, kind, toCollectionName, toKey);

            var json = JsonConvert.SerializeObject(properties);

            return await PutGraphIfMatchAsync(collectionName, key, kind, toCollectionName, toKey, json, ifMatch);
        }

        public async Task<Result> PutGraphIfMatchAsync(string collectionName, string key, string kind, string toCollectionName, string toKey, string ifMatch, string properties)
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

            var baseResult = await Communication.CallWebRequestAsync(_apiKey, url, "PUT", properties, ifMatch);

            return BuildResult(collectionName, key, baseResult);
        }

        public Result PutGraphIfNoneMatch(string collectionName, string key, string kind, string toCollectionName, string toKey)
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

            var baseResult = Communication.CallWebRequest(_apiKey, url, "PUT", null, null, true);

            return BuildResult(collectionName, key, baseResult);
        }

        public async Task<Result> PutGraphIfNoneMatchAsync(string collectionName, string key, string kind, string toCollectionName, string toKey)
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

            var baseResult = await Communication.CallWebRequestAsync(_apiKey, url, "PUT", null, null, true);

            return BuildResult(collectionName, key, baseResult);
        }

        public Result PutGraphIfNoneMatch(string collectionName, string key, string kind, string toCollectionName, string toKey, object properties)
        {
            if (properties == null)
                PutGraph(collectionName, key, kind, toCollectionName, toKey);

            var json = JsonConvert.SerializeObject(properties);

            return PutGraphIfNoneMatch(collectionName, key, kind, toCollectionName, toKey, json);
        }

        public Result PutGraphIfNoneMatch(string collectionName, string key, string kind, string toCollectionName, string toKey, string properties)
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

            var baseResult = Communication.CallWebRequest(_apiKey, url, "PUT", properties, null, true);

            return BuildResult(collectionName, key, baseResult);
        }

        public async Task<Result> PutGraphIfNoneMatchAsync(string collectionName, string key, string kind, string toCollectionName, string toKey, object properties)
        {
            if (properties == null)
                PutGraph(collectionName, key, kind, toCollectionName, toKey);

            var json = JsonConvert.SerializeObject(properties);

            return await PutGraphIfNoneMatchAsync(collectionName, key, kind, toCollectionName, toKey, json);
        }

        public async Task<Result> PutGraphIfNoneMatchAsync(string collectionName, string key, string kind, string toCollectionName, string toKey, string properties)
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

            var baseResult = await Communication.CallWebRequestAsync(_apiKey, url, "PUT", properties, null, true);

            return BuildResult(collectionName, key, baseResult);
        }

        #endregion

        #region Delete Graph

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

        #endregion
    }
}
