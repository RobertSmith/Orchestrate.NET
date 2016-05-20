using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Orchestrate.Net
{
    public partial class Orchestrate
    {
        #region Gets

        public Result Get(string collectionName, string key)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key;
            var baseResult = Communication.CallWebRequest(_apiKey, url, "GET", null);

            return BuildResult(collectionName, key, baseResult);
        }

        public Result Get(string collectionName, string key, string reference)
        {
            return Ref(collectionName, key, reference);
        }

        public async Task<Result> GetAsync(string collectionName, string key)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key;
            var baseResult = await Communication.CallWebRequestAsync(_apiKey, url, "GET", null);

            return BuildResult(collectionName, key, baseResult);
        }

        public async Task<Result> GetAsync(string collectionName, string key, string reference)
        {
            return await RefAsync(collectionName, key, reference);
        }

        #endregion

        #region Posts

        public Result Post(string collectionName, string item)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(item))
                throw new ArgumentNullException(nameof(item), "item cannot be empty");

            var url = _urlBase + collectionName;
            var baseResult = Communication.CallWebRequest(_apiKey, url, "POST", item);
            var key = ExtractKeyFromLocation(baseResult);

            return BuildResult(collectionName, key, baseResult);
        }

        public Result Post(string collectionName, object item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "item cannot be null");

            var json = JsonConvert.SerializeObject(item);
            return Post(collectionName, json);
        }

        public async Task<Result> PostAsync(string collectionName, object item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "item cannot be null");

            var json = JsonConvert.SerializeObject(item);
            return await PostAsync(collectionName, json);
        }

        public async Task<Result> PostAsync(string collectionName, string item)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(item))
                throw new ArgumentNullException(nameof(item), "item cannot be empty");

            var url = _urlBase + collectionName;
            var baseResult = await Communication.CallWebRequestAsync(_apiKey, url, "POST", item);
            var key = ExtractKeyFromLocation(baseResult);

            return BuildResult(collectionName, key, baseResult);
        }

        #endregion

        #region Puts

        public Result Put(string collectionName, string key, string item)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (string.IsNullOrWhiteSpace(item))
                throw new ArgumentNullException(nameof(item), "item cannot be empty");

            var url = _urlBase + collectionName + "/" + key;
            var baseResult = Communication.CallWebRequest(_apiKey, url, "PUT", item);

            return BuildResult(collectionName, key, baseResult);
        }

        public Result Put(string collectionName, string key, object item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "item cannot be null");

            var json = JsonConvert.SerializeObject(item);
            return Put(collectionName, key, json);
        }

        public Result PutIfMatch(string collectionName, string key, string item, string ifMatch)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (string.IsNullOrWhiteSpace(item))
                throw new ArgumentNullException(nameof(item), "json cannot be empty");

            if (string.IsNullOrWhiteSpace(ifMatch))
                throw new ArgumentNullException(nameof(ifMatch), "ifMatch cannot be empty");

            var url = _urlBase + collectionName + "/" + key;
            var baseResult = Communication.CallWebRequest(_apiKey, url, "PUT", item, ifMatch);

            return BuildResult(collectionName, key, baseResult);
        }

        public Result PutIfMatch(string collectionName, string key, object item, string ifMatch)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "item cannot be null");

            var json = JsonConvert.SerializeObject(item);
            return PutIfMatch(collectionName, key, json, ifMatch);
        }

        public Result PutIfNoneMatch(string collectionName, string key, string item)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (string.IsNullOrWhiteSpace(item))
                throw new ArgumentNullException(nameof(item), "item cannot be empty");

            var url = _urlBase + collectionName + "/" + key;
            var baseResult = Communication.CallWebRequest(_apiKey, url, "PUT", item, null, true);

            return BuildResult(collectionName, key, baseResult);
        }

        public Result PutIfNoneMatch(string collectionName, string key, object item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "item cannot be null");

            var json = JsonConvert.SerializeObject(item);
            return PutIfNoneMatch(collectionName, key, json);
        }

        public async Task<Result> PutAsync(string collectionName, string key, object item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "item cannot be null");

            var json = JsonConvert.SerializeObject(item);
            return await PutAsync(collectionName, key, json);
        }

        public async Task<Result> PutAsync(string collectionName, string key, string item)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (string.IsNullOrWhiteSpace(item))
                throw new ArgumentNullException(nameof(item), "item cannot be empty");

            var url = _urlBase + collectionName + "/" + key;
            var baseResult = await Communication.CallWebRequestAsync(_apiKey, url, "PUT", item);

            return BuildResult(collectionName, key, baseResult);
        }

        public async Task<Result> PutIfMatchAsync(string collectionName, string key, object item, string ifMatch)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "item cannot be null");

            var json = JsonConvert.SerializeObject(item);
            return await PutIfMatchAsync(collectionName, key, json, ifMatch);
        }

        public async Task<Result> PutIfMatchAsync(string collectionName, string key, string item, string ifMatch)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (string.IsNullOrWhiteSpace(item))
                throw new ArgumentNullException(nameof(item), "json cannot be empty");

            if (string.IsNullOrWhiteSpace(ifMatch))
                throw new ArgumentNullException(nameof(ifMatch), "ifMatch cannot be empty");

            var url = _urlBase + collectionName + "/" + key;
            var baseResult = await Communication.CallWebRequestAsync(_apiKey, url, "PUT", item, ifMatch);

            return BuildResult(collectionName, key, baseResult);
        }

        public async Task<Result> PutIfNoneMatchAsync(string collectionName, string key, object item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "item cannot be null");

            var json = JsonConvert.SerializeObject(item);
            return await PutIfNoneMatchAsync(collectionName, key, json);
        }

        public async Task<Result> PutIfNoneMatchAsync(string collectionName, string key, string item)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (string.IsNullOrWhiteSpace(item))
                throw new ArgumentNullException(nameof(item), "item cannot be empty");

            var url = _urlBase + collectionName + "/" + key;
            var baseResult = await Communication.CallWebRequestAsync(_apiKey, url, "PUT", item, null, true);

            return BuildResult(collectionName, key, baseResult);
        }

        #endregion

        #region Patches

        public Result Patch(string collectionName, string key, string item)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (string.IsNullOrWhiteSpace(item))
                throw new ArgumentNullException(nameof(item), "item cannot be empty");

            var url = _urlBase + collectionName + "/" + key;
            var baseResult = Communication.CallWebRequest(_apiKey, url, "PATCH", item);

            return BuildResult(collectionName, key, baseResult);
        }

        public Result Patch(string collectionName, string key, object item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "item cannot be null");

            var json = JsonConvert.SerializeObject(item);

            return Patch(collectionName, key, json);
        }

        public Result PatchIfMatch(string collectionName, string key, string item, string ifMatch)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (string.IsNullOrWhiteSpace(item))
                throw new ArgumentNullException(nameof(item), "json cannot be empty");

            if (string.IsNullOrWhiteSpace(ifMatch))
                throw new ArgumentNullException(nameof(ifMatch), "ifMatch cannot be empty");

            var url = _urlBase + collectionName + "/" + key;
            var baseResult = Communication.CallWebRequest(_apiKey, url, "PUT", item, ifMatch);

            return BuildResult(collectionName, key, baseResult);
        }

        public Result PatchIfMatch(string collectionName, string key, object item, string ifMatch)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "item cannot be null");

            var json = JsonConvert.SerializeObject(item);

            return PutIfMatch(collectionName, key, json, ifMatch);
        }

        public async Task<Result> PatchAsync(string collectionName, string key, string item)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (string.IsNullOrWhiteSpace(item))
                throw new ArgumentNullException(nameof(item), "item cannot be empty");

            var url = _urlBase + collectionName + "/" + key;
            var baseResult = await Communication.CallWebRequestAsync(_apiKey, url, "PATCH", item);

            return BuildResult(collectionName, key, baseResult);
        }

        public async Task<Result> PatchAsync(string collectionName, string key, object item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "item cannot be null");

            var json = JsonConvert.SerializeObject(item);

            return await PatchAsync(collectionName, key, json);
        }

        public async Task<Result> PatchIfMatchAsync(string collectionName, string key, string item, string ifMatch)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (string.IsNullOrWhiteSpace(item))
                throw new ArgumentNullException(nameof(item), "json cannot be empty");

            if (string.IsNullOrWhiteSpace(ifMatch))
                throw new ArgumentNullException(nameof(ifMatch), "ifMatch cannot be empty");

            var url = _urlBase + collectionName + "/" + key;
            var baseResult = await Communication.CallWebRequestAsync(_apiKey, url, "PUT", item, ifMatch);

            return BuildResult(collectionName, key, baseResult);
        }

        public async Task<Result> PatchIfMatchAsync(string collectionName, string key, object item, string ifMatch)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "item cannot be null");

            var json = JsonConvert.SerializeObject(item);

            return await PutIfMatchAsync(collectionName, key, json, ifMatch);
        }

        #endregion

        #region Deletes

        public Result Delete(string collectionName, string key, bool purge)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key;

            if (purge)
                url += "?purge=true";
            else
                url += "?purge=false";

            var baseResult = Communication.CallWebRequest(_apiKey, url, "DELETE", null);

            return BuildResult(collectionName, key, baseResult);
        }

        public Result DeleteIfMatch(string collectionName, string key, string ifMatch, bool purge)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (string.IsNullOrWhiteSpace(ifMatch))
                throw new ArgumentNullException(nameof(ifMatch), "ifMatch cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key;

            if (purge)
                url += "?purge=true";
            else
                url += "?purge=false";

            var baseResult = Communication.CallWebRequest(_apiKey, url, "DELETE", null, ifMatch);

            return BuildResult(collectionName, key, baseResult);
        }

        public async Task<Result> DeleteAsync(string collectionName, string key, bool purge)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key;

            if (purge)
                url += "?purge=true";
            else
                url += "?purge=false";

            var baseResult = await Communication.CallWebRequestAsync(_apiKey, url, "DELETE", null);

            return BuildResult(collectionName, key, baseResult);
        }

        public async Task<Result> DeleteIfMatchAsync(string collectionName, string key, string ifMatch, bool purge)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (string.IsNullOrWhiteSpace(ifMatch))
                throw new ArgumentNullException(nameof(ifMatch), "ifMatch cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key;

            if (purge)
                url += "?purge=true";
            else
                url += "?purge=false";

            var baseResult = await Communication.CallWebRequestAsync(_apiKey, url, "DELETE", null, ifMatch);

            return BuildResult(collectionName, key, baseResult);
        }

        #endregion

        #region Lists

        public ListResult List(string collectionName, int limit, string startKey, string afterKey, string endKey = "", string beforeKey = "")
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (limit < 1 || limit > 100)
                throw new ArgumentOutOfRangeException(nameof(limit), "limit must be between 1 and 100");

            if (!string.IsNullOrWhiteSpace(startKey) && !string.IsNullOrWhiteSpace(afterKey))
                throw new ArgumentException("May only specify either a startKey or an afterKey", nameof(startKey));

            if (!string.IsNullOrWhiteSpace(endKey) && !string.IsNullOrWhiteSpace(beforeKey))
                throw new ArgumentException("May only specify either a endKey or an beforeKey", nameof(endKey));

            var url = _urlBase + collectionName + "?limit=" + limit;

            if (!string.IsNullOrWhiteSpace(startKey))
                url += "&startKey=" + startKey;

            if (!string.IsNullOrWhiteSpace(afterKey))
                url += "&afterKey=" + afterKey;

            if (!string.IsNullOrWhiteSpace(endKey))
                url += "&endKey=" + endKey;

            if (!string.IsNullOrWhiteSpace(beforeKey))
                url += "&beforeKey=" + beforeKey;

            var result = Communication.CallWebRequestAsync(_apiKey, url, "GET", null);

            return JsonConvert.DeserializeObject<ListResult>(result.Result.Payload);
        }

        public async Task<ListResult> ListAsync(string collectionName, int limit, string startKey, string afterKey, string endKey = "", string beforeKey = "")
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (limit < 1 || limit > 100)
                throw new ArgumentOutOfRangeException(nameof(limit), "limit must be between 1 and 100");

            if (!string.IsNullOrWhiteSpace(startKey) && !string.IsNullOrWhiteSpace(afterKey))
                throw new ArgumentException("May only specify either a startKey or an afterKey", nameof(startKey));

            if (!string.IsNullOrWhiteSpace(endKey) && !string.IsNullOrWhiteSpace(beforeKey))
                throw new ArgumentException("May only specify either a endKey or an beforeKey", nameof(endKey));

            var url = _urlBase + collectionName + "?limit=" + limit;

            if (!string.IsNullOrWhiteSpace(startKey))
                url += "&startKey=" + startKey;

            if (!string.IsNullOrWhiteSpace(afterKey))
                url += "&afterKey=" + afterKey;

            if (!string.IsNullOrWhiteSpace(endKey))
                url += "&endKey=" + endKey;

            if (!string.IsNullOrWhiteSpace(beforeKey))
                url += "&beforeKey=" + beforeKey;

            var result = await Communication.CallWebRequestAsync(_apiKey, url, "GET", null);

            return JsonConvert.DeserializeObject<ListResult>(result.Payload);
        }

        #endregion
    }
}
