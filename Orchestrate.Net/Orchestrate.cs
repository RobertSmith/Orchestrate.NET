using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Orchestrate.Net
{
    public class Orchestrate : IOrchestrate
    {
        private readonly string _apiKey;
        private readonly string _urlBase;

        public Orchestrate(string apiKey, string url = "https://api.orchestrate.io/v0/")
        {
            _apiKey = apiKey;
            _urlBase = url;
        }

        #region IOrchestrate Sync Members

        #region Collections

        public Result CreateCollection(string collectionName, string key, string item)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "key cannot be null or empty");

            if (string.IsNullOrEmpty(item))
                throw new ArgumentNullException("item", "item cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key;
            var baseResult = Communication.CallWebRequest(_apiKey, url, "PUT", item);

            return new Result
            {
                Path = new OrchestratePath
                {
                    Collection = collectionName,
                    Key = key,
                    Ref = baseResult.ETag
                },
                Score = 1,
                Value = baseResult.Payload
            };
        }

        public Result CreateCollection(string collectionName, string key, object item)
        {
            if (item == null)
                throw new ArgumentNullException("item", "item cannot be null");

            var json = JsonConvert.SerializeObject(item);
            return CreateCollection(collectionName, key, json);
        }

        public Result DeleteCollection(string collectionName)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            var url = _urlBase + collectionName + "?force=true";
            var baseResult = Communication.CallWebRequest(_apiKey, url, "DELETE", null);

            return new Result
            {
                Path = new OrchestratePath
                {
                    Collection = collectionName,
                    Key = string.Empty,
                    Ref = baseResult.ETag
                },
                Score = 1,
                Value = baseResult.Payload
            };
        }

        #endregion

        #region Gets

        public Result Get(string collectionName, string key)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "key cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key;
            var baseResult = Communication.CallWebRequest(_apiKey, url, "GET", null);

            return new Result
            {
                Path = new OrchestratePath
                {
                    Collection = collectionName,
                    Key = key,
                    Ref = baseResult.ETag
                },
                Score = 1,
                Value = baseResult.Payload
            };
        }

        public Result Get(string collectionName, string key, string reference)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "key cannot be null or empty");

            if (string.IsNullOrEmpty(reference))
                throw new ArgumentNullException("reference", "reference cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key + "/refs/" + reference;
            var baseResult = Communication.CallWebRequest(_apiKey, url, "GET", null);

            return new Result
            {
                Path = new OrchestratePath
                {
                    Collection = collectionName,
                    Key = key,
                    Ref = baseResult.ETag
                },
                Score = 1,
                Value = baseResult.Payload
            };
        }

        #endregion

        #region Posts

        public Result Post(string collectionName, string item)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(item))
                throw new ArgumentNullException("item", "item cannot be empty");

            var url = _urlBase + collectionName;
            var baseResult = Communication.CallWebRequest(_apiKey, url, "POST", item);
            var key = ExtractKeyFromLocation(baseResult);

            return new Result
            {
                Path = new OrchestratePath
                {
                    Collection = collectionName,
                    Key = key,
                    Ref = baseResult.ETag
                },
                Score = 1,
                Value = baseResult.Payload
            };
        }

        public Result Post(string collectionName, object item)
        {
            if (item == null)
                throw new ArgumentNullException("item", "item cannot be null");

            var json = JsonConvert.SerializeObject(item);
            return Post(collectionName, json);
        }

        #endregion

        #region Puts

        public Result Put(string collectionName, string key, string item)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "key cannot be null or empty");

            if (string.IsNullOrEmpty(item))
                throw new ArgumentNullException("item", "item cannot be empty");

            var url = _urlBase + collectionName + "/" + key;
            var baseResult = Communication.CallWebRequest(_apiKey, url, "PUT", item);

            return new Result
            {
                Path = new OrchestratePath
                {
                    Collection = collectionName,
                    Key = key,
                    Ref = baseResult.ETag
                },
                Score = 1,
                Value = baseResult.Payload
            };
        }

        public Result Put(string collectionName, string key, object item)
        {
            if (item == null)
                throw new ArgumentNullException("item", "item cannot be null");

            var json = JsonConvert.SerializeObject(item);
            return Put(collectionName, key, json);
        }

        public Result PutIfMatch(string collectionName, string key, string item, string ifMatch)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "key cannot be null or empty");

            if (string.IsNullOrEmpty(item))
                throw new ArgumentNullException("item", "json cannot be empty");

            if (string.IsNullOrEmpty(ifMatch))
                throw new ArgumentNullException("ifMatch", "ifMatch cannot be empty");

            var url = _urlBase + collectionName + "/" + key;
            var baseResult = Communication.CallWebRequest(_apiKey, url, "PUT", item, ifMatch);

            return new Result
            {
                Path = new OrchestratePath
                {
                    Collection = collectionName,
                    Key = key,
                    Ref = baseResult.ETag
                },
                Score = 1,
                Value = baseResult.Payload
            };
        }

        public Result PutIfMatch(string collectionName, string key, object item, string ifMatch)
        {
            if (item == null)
                throw new ArgumentNullException("item", "item cannot be null");

            var json = JsonConvert.SerializeObject(item);
            return PutIfMatch(collectionName, key, json, ifMatch);
        }

        public Result PutIfNoneMatch(string collectionName, string key, string item)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "key cannot be null or empty");

            if (string.IsNullOrEmpty(item))
                throw new ArgumentNullException("item", "item cannot be empty");

            var url = _urlBase + collectionName + "/" + key;
            var baseResult = Communication.CallWebRequest(_apiKey, url, "PUT", item, null, true);

            return new Result
            {
                Path = new OrchestratePath
                {
                    Collection = collectionName,
                    Key = key,
                    Ref = baseResult.ETag
                },
                Score = 1,
                Value = baseResult.Payload
            };
        }

        public Result PutIfNoneMatch(string collectionName, string key, object item)
        {
            if (item == null)
                throw new ArgumentNullException("item", "item cannot be null");

            var json = JsonConvert.SerializeObject(item);
            return PutIfNoneMatch(collectionName, key, json);
        }

        #endregion

        #region Patches

        public Result Patch(string collectionName, string key, string item)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "key cannot be null or empty");

            if (string.IsNullOrEmpty(item))
                throw new ArgumentNullException("item", "item cannot be empty");

            var url = _urlBase + collectionName + "/" + key;
            var baseResult = Communication.CallWebRequest(_apiKey, url, "PATCH", item);

            return new Result
            {
                Path = new OrchestratePath
                {
                    Collection = collectionName,
                    Key = key,
                    Ref = baseResult.ETag
                },
                Score = 1,
                Value = baseResult.Payload
            };
        }

        public Result Patch(string collectionName, string key, object item)
        {
            if (item == null)
                throw new ArgumentNullException("item", "item cannot be null");

            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            var json = JsonConvert.SerializeObject(item, settings);

            return Patch(collectionName, key, json);
        }

        public Result PatchIfMatch(string collectionName, string key, string item, string ifMatch)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "key cannot be null or empty");

            if (string.IsNullOrEmpty(item))
                throw new ArgumentNullException("item", "json cannot be empty");

            if (string.IsNullOrEmpty(ifMatch))
                throw new ArgumentNullException("ifMatch", "ifMatch cannot be empty");

            var url = _urlBase + collectionName + "/" + key;
            var baseResult = Communication.CallWebRequest(_apiKey, url, "PUT", item, ifMatch);

            return new Result
            {
                Path = new OrchestratePath
                {
                    Collection = collectionName,
                    Key = key,
                    Ref = baseResult.ETag
                },
                Score = 1,
                Value = baseResult.Payload
            };
        }

        public Result PatchIfMatch(string collectionName, string key, object item, string ifMatch)
        {
            if (item == null)
                throw new ArgumentNullException("item", "item cannot be null");

            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            var json = JsonConvert.SerializeObject(item, settings);

            return PutIfMatch(collectionName, key, json, ifMatch);
        }

        #endregion

        #region Deletes

        public Result Delete(string collectionName, string key, bool purge)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "key cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key;

            if (purge)
                url += "?purge=true";
            else
                url += "?purge=false";

            var baseResult = Communication.CallWebRequest(_apiKey, url, "DELETE", null);

            return new Result
            {
                Path = new OrchestratePath
                {
                    Collection = collectionName,
                    Key = key,
                    Ref = baseResult.ETag
                },
                Score = 1,
                Value = baseResult.Payload
            };
        }

        public Result DeleteIfMatch(string collectionName, string key, string ifMatch, bool purge)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "key cannot be null or empty");

            if (string.IsNullOrEmpty(ifMatch))
                throw new ArgumentNullException("ifMatch", "ifMatch cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key;

            if (purge)
                url += "?purge=true";
            else
                url += "?purge=false";

            var baseResult = Communication.CallWebRequest(_apiKey, url, "DELETE", null, ifMatch);

            return new Result
            {
                Path = new OrchestratePath
                {
                    Collection = collectionName,
                    Key = key,
                    Ref = baseResult.ETag
                },
                Score = 1,
                Value = baseResult.Payload
            };
        }

        #endregion

        #region Lists

        public ListResult List(string collectionName, int limit, string startKey, string afterKey, string endKey = "", string beforeKey = "")
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (limit < 1 || limit > 100)
                throw new ArgumentOutOfRangeException("limit", "limit must be between 1 and 100");

            if (!string.IsNullOrEmpty(startKey) && !string.IsNullOrEmpty(afterKey))
                throw new ArgumentException("May only specify either a startKey or an afterKey", "startKey");

            if (!string.IsNullOrEmpty(endKey) && !string.IsNullOrEmpty(beforeKey))
                throw new ArgumentException("May only specify either a endKey or an beforeKey", "endKey");

            var url = _urlBase + collectionName + "?limit=" + limit;

            if (!string.IsNullOrEmpty(startKey))
                url += "&startKey=" + startKey;

            if (!string.IsNullOrEmpty(afterKey))
                url += "&afterKey=" + afterKey;

            if (!string.IsNullOrEmpty(endKey))
                url += "&endKey=" + endKey;

            if (!string.IsNullOrEmpty(beforeKey))
                url += "&beforeKey=" + beforeKey;

            return JsonConvert.DeserializeObject<ListResult>(Communication.CallWebRequest(_apiKey, url, "GET", null).Payload);
        }

        #endregion

        #region Searches

        public SearchResult Search(string collectionName, string query, int limit = 10, int offset = 0)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(query))
                throw new ArgumentNullException("query", "query cannot be null or empty");

            if (limit < 1 || limit > 100)
                throw new ArgumentOutOfRangeException("limit", "limit must be between 1 and 100");

            if (offset < 0)
                throw new ArgumentOutOfRangeException("offset", "offset must be at least 0");

            var url = _urlBase + collectionName + "?query=" + query + "&limit=" + limit + "&offset=" + offset;

            return JsonConvert.DeserializeObject<SearchResult>(Communication.CallWebRequest(_apiKey, url, "GET", null).Payload);
        }

        #endregion

        #region Events

        public EventResultList GetEvents(string collectionName, string key, string type, DateTime? start = null, DateTime? end = null)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "key cannot be null or empty");

            if (string.IsNullOrEmpty(type))
                throw new ArgumentNullException("type", "type cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key + "/events/" + type;

            if (start != null)
                url += "?start=" + ConvertToUnixTimestamp(start.Value);

            if (end != null && start != null)
                url += "&end=" + ConvertToUnixTimestamp(end.Value);
            else if (end != null)
                url += "?end=" + ConvertToUnixTimestamp(end.Value);

            return JsonConvert.DeserializeObject<EventResultList>(Communication.CallWebRequest(_apiKey, url, "GET", null).Payload);
        }

        public Result PutEvent(string collectionName, string key, string type, DateTime? timeStamp, string item)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "key cannot be null or empty");

            if (string.IsNullOrEmpty(type))
                throw new ArgumentNullException("type", "type cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key + "/events/" + type;

            if (timeStamp != null)
                url += "?timestamp=" + ConvertToUnixTimestamp(timeStamp.Value);

            var baseResult = Communication.CallWebRequest(_apiKey, url, "PUT", item);

            return new Result
            {
                Path = new OrchestratePath
                {
                    Collection = collectionName,
                    Key = key,
                    Ref = baseResult.ETag
                },
                Score = 1,
                Value = baseResult.Payload
            };
        }

        public Result PutEvent(string collectionName, string key, string type, DateTime? timeStamp, object item)
        {
            if (item == null)
                throw new ArgumentNullException("item", "item cannot be null");
            var json = JsonConvert.SerializeObject(item);

            return PutEvent(collectionName, key, type, timeStamp, json);
        }

        #endregion

        #region Graphs

        public ListResult GetGraph(string collectionName, string key, string[] kinds)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "key cannot be null or empty");

            if (kinds == null || kinds.Length == 0)
                throw new ArgumentNullException("kinds", "kinds cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key + "/relations";

            url = kinds.Aggregate(url, (current, kind) => current + ("/" + kind));

            return JsonConvert.DeserializeObject<ListResult>(Communication.CallWebRequest(_apiKey, url, "GET", null).Payload);
        }

        public Result PutGraph(string collectionName, string key, string kind, string toCollectionName, string toKey)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "key cannot be null or empty");

            if (string.IsNullOrEmpty(kind))
                throw new ArgumentNullException("kind", "kind cannot be null or empty");

            if (string.IsNullOrEmpty(toCollectionName))
                throw new ArgumentNullException("toCollectionName", "toCollectionName cannot be null or empty");

            if (string.IsNullOrEmpty(toKey))
                throw new ArgumentNullException("toKey", "toKey cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key + "/relation/" + kind + "/" + toCollectionName + "/" + toKey;

            var baseResult = Communication.CallWebRequest(_apiKey, url, "PUT", null);

            return new Result
            {
                Path = new OrchestratePath
                {
                    Collection = collectionName,
                    Key = key,
                    Ref = baseResult.ETag
                },
                Score = 1,
                Value = baseResult.Payload
            };
        }

        public Result DeleteGraph(string collectionName, string key, string kind, string toCollectionName, string toKey)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "key cannot be null or empty");

            if (string.IsNullOrEmpty(kind))
                throw new ArgumentNullException("kind", "kind cannot be null or empty");

            if (string.IsNullOrEmpty(toCollectionName))
                throw new ArgumentNullException("toCollectionName", "toCollectionName cannot be null or empty");

            if (string.IsNullOrEmpty(toKey))
                throw new ArgumentNullException("toKey", "toKey cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key + "/relation/" + kind + "/" + toCollectionName + "/" + toKey + "?purge=true";

            var baseResult = Communication.CallWebRequest(_apiKey, url, "DELETE", null);

            return new Result
            {
                Path = new OrchestratePath
                {
                    Collection = collectionName,
                    Key = key,
                    Ref = baseResult.ETag
                },
                Score = 1,
                Value = baseResult.Payload
            };
        }

        #endregion
        
        #endregion

        #region IOrchestrate Async Members

        #region Collections

        public async Task<Result> CreateCollectionAsync(string collectionName, string key, object item)
        {
            if (item == null)
                throw new ArgumentNullException("item", "item cannot be null");

            var json = JsonConvert.SerializeObject(item);
            return await CreateCollectionAsync(collectionName, key, json);
        }

        public async Task<Result> CreateCollectionAsync(string collectionName, string key, string item)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "key cannot be null or empty");

            if (string.IsNullOrEmpty(item))
                throw new ArgumentNullException("item", "item cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key;
            var baseResult = await Communication.CallWebRequestAsync(_apiKey, url, "PUT", item);

            return new Result
            {
                Path = new OrchestratePath
                {
                    Collection = collectionName,
                    Key = key,
                    Ref = baseResult.ETag
                },
                Score = 1,
                Value = baseResult.Payload
            };
        }

        public async Task<Result> DeleteCollectionAsync(string collectionName)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            var url = _urlBase + collectionName + "?force=true";
            var baseResult = await Communication.CallWebRequestAsync(_apiKey, url, "DELETE", null);

            return new Result
            {
                Path = new OrchestratePath
                {
                    Collection = collectionName,
                    Key = string.Empty,
                    Ref = baseResult.ETag
                },
                Score = 1,
                Value = baseResult.Payload
            };
        }

        #endregion

        #region Gets

        public async Task<Result> GetAsync(string collectionName, string key)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "key cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key;
            var baseResult = await Communication.CallWebRequestAsync(_apiKey, url, "GET", null);

            return new Result
            {
                Path = new OrchestratePath
                {
                    Collection = collectionName,
                    Key = key,
                    Ref = baseResult.ETag
                },
                Score = 1,
                Value = baseResult.Payload
            };
        }

        public async Task<Result> GetAsync(string collectionName, string key, string reference)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "key cannot be null or empty");

            if (string.IsNullOrEmpty(reference))
                throw new ArgumentNullException("reference", "reference cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key + "/refs/" + reference;
            var baseResult = await Communication.CallWebRequestAsync(_apiKey, url, "GET", null);

            return new Result
            {
                Path = new OrchestratePath
                {
                    Collection = collectionName,
                    Key = key,
                    Ref = baseResult.ETag
                },
                Score = 1,
                Value = baseResult.Payload
            };
        }

        #endregion

        #region Posts

        public async Task<Result> PostAsync(string collectionName, object item)
        {
            if (item == null)
                throw new ArgumentNullException("item", "item cannot be null");

            var json = JsonConvert.SerializeObject(item);
            return await PostAsync(collectionName, json);
        }

        public async Task<Result> PostAsync(string collectionName, string item)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(item))
                throw new ArgumentNullException("item", "item cannot be empty");

            var url = _urlBase + collectionName;
            var baseResult = await Communication.CallWebRequestAsync(_apiKey, url, "POST", item);
            var key = ExtractKeyFromLocation(baseResult);

            return new Result
            {
                Path = new OrchestratePath
                {
                    Collection = collectionName,
                    Key = key,
                    Ref = baseResult.ETag
                },
                Score = 1,
                Value = baseResult.Payload
            };
        }

        #endregion

        #region Puts

        public async Task<Result> PutAsync(string collectionName, string key, object item)
        {
            if (item == null)
                throw new ArgumentNullException("item", "item cannot be null");

            var json = JsonConvert.SerializeObject(item);
            return await PutAsync(collectionName, key, json);
        }

        public async Task<Result> PutAsync(string collectionName, string key, string item)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "key cannot be null or empty");

            if (string.IsNullOrEmpty(item))
                throw new ArgumentNullException("item", "item cannot be empty");

            var url = _urlBase + collectionName + "/" + key;
            var baseResult = await Communication.CallWebRequestAsync(_apiKey, url, "PUT", item);

            return new Result
            {
                Path = new OrchestratePath
                {
                    Collection = collectionName,
                    Key = key,
                    Ref = baseResult.ETag
                },
                Score = 1,
                Value = baseResult.Payload
            };
        }

        public async Task<Result> PutIfMatchAsync(string collectionName, string key, object item, string ifMatch)
        {
            if (item == null)
                throw new ArgumentNullException("item", "item cannot be null");

            var json = JsonConvert.SerializeObject(item);
            return await PutIfMatchAsync(collectionName, key, json, ifMatch);
        }

        public async Task<Result> PutIfMatchAsync(string collectionName, string key, string item, string ifMatch)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "key cannot be null or empty");

            if (string.IsNullOrEmpty(item))
                throw new ArgumentNullException("item", "json cannot be empty");

            if (string.IsNullOrEmpty(ifMatch))
                throw new ArgumentNullException("ifMatch", "ifMatch cannot be empty");

            var url = _urlBase + collectionName + "/" + key;
            var baseResult = await Communication.CallWebRequestAsync(_apiKey, url, "PUT", item, ifMatch);

            return new Result
            {
                Path = new OrchestratePath
                {
                    Collection = collectionName,
                    Key = key,
                    Ref = baseResult.ETag
                },
                Score = 1,
                Value = baseResult.Payload
            };
        }

        public async Task<Result> PutIfNoneMatchAsync(string collectionName, string key, object item)
        {
            if (item == null)
                throw new ArgumentNullException("item", "item cannot be null");

            var json = JsonConvert.SerializeObject(item);
            return await PutIfNoneMatchAsync(collectionName, key, json);
        }

        public async Task<Result> PutIfNoneMatchAsync(string collectionName, string key, string item)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "key cannot be null or empty");

            if (string.IsNullOrEmpty(item))
                throw new ArgumentNullException("item", "item cannot be empty");

            var url = _urlBase + collectionName + "/" + key;
            var baseResult = await Communication.CallWebRequestAsync(_apiKey, url, "PUT", item, null, true);

            return new Result
            {
                Path = new OrchestratePath
                {
                    Collection = collectionName,
                    Key = key,
                    Ref = baseResult.ETag
                },
                Score = 1,
                Value = baseResult.Payload
            };
        }

        #endregion

        #region Patches

        public async Task<Result> PatchAsync(string collectionName, string key, string item)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "key cannot be null or empty");

            if (string.IsNullOrEmpty(item))
                throw new ArgumentNullException("item", "item cannot be empty");

            var url = _urlBase + collectionName + "/" + key;
            var baseResult = await Communication.CallWebRequestAsync(_apiKey, url, "PATCH", item);

            return new Result
            {
                Path = new OrchestratePath
                {
                    Collection = collectionName,
                    Key = key,
                    Ref = baseResult.ETag
                },
                Score = 1,
                Value = baseResult.Payload
            };
        }

        public async Task<Result> PatchAsync(string collectionName, string key, object item)
        {
            if (item == null)
                throw new ArgumentNullException("item", "item cannot be null");

            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            var json = JsonConvert.SerializeObject(item, settings);

            return await PatchAsync(collectionName, key, json);
        }

        public async Task<Result> PatchIfMatchAsync(string collectionName, string key, string item, string ifMatch)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "key cannot be null or empty");

            if (string.IsNullOrEmpty(item))
                throw new ArgumentNullException("item", "json cannot be empty");

            if (string.IsNullOrEmpty(ifMatch))
                throw new ArgumentNullException("ifMatch", "ifMatch cannot be empty");

            var url = _urlBase + collectionName + "/" + key;
            var baseResult = await Communication.CallWebRequestAsync(_apiKey, url, "PUT", item, ifMatch);

            return new Result
            {
                Path = new OrchestratePath
                {
                    Collection = collectionName,
                    Key = key,
                    Ref = baseResult.ETag
                },
                Score = 1,
                Value = baseResult.Payload
            };
        }

        public async Task<Result> PatchIfMatchAsync(string collectionName, string key, object item, string ifMatch)
        {
            if (item == null)
                throw new ArgumentNullException("item", "item cannot be null");

            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            var json = JsonConvert.SerializeObject(item, settings);

            return await PutIfMatchAsync(collectionName, key, json, ifMatch);
        }

        #endregion

        #region Deletes

        public async Task<Result> DeleteAsync(string collectionName, string key, bool purge)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "key cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key;

            if (purge)
                url += "?purge=true";
            else
                url += "?purge=false";

            var baseResult = await Communication.CallWebRequestAsync(_apiKey, url, "DELETE", null);

            return new Result
            {
                Path = new OrchestratePath
                {
                    Collection = collectionName,
                    Key = key,
                    Ref = baseResult.ETag
                },
                Score = 1,
                Value = baseResult.Payload
            };
        }

        public async Task<Result> DeleteIfMatchAsync(string collectionName, string key, string ifMatch, bool purge)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "key cannot be null or empty");

            if (string.IsNullOrEmpty(ifMatch))
                throw new ArgumentNullException("ifMatch", "ifMatch cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key;

            if (purge)
                url += "?purge=true";
            else
                url += "?purge=false";

            var baseResult = await Communication.CallWebRequestAsync(_apiKey, url, "DELETE", null, ifMatch);

            return new Result
            {
                Path = new OrchestratePath
                {
                    Collection = collectionName,
                    Key = key,
                    Ref = baseResult.ETag
                },
                Score = 1,
                Value = baseResult.Payload
            };
        }

        #endregion

        #region Lists

        public async Task<ListResult> ListAsync(string collectionName, int limit, string startKey, string afterKey, string endKey = "", string beforeKey = "")
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (limit < 1 || limit > 100)
                throw new ArgumentOutOfRangeException("limit", "limit must be between 1 and 100");

            if (!string.IsNullOrEmpty(startKey) && !string.IsNullOrEmpty(afterKey))
                throw new ArgumentException("May only specify either a startKey or an afterKey", "startKey");

            if (!string.IsNullOrEmpty(endKey) && !string.IsNullOrEmpty(beforeKey))
                throw new ArgumentException("May only specify either a endKey or an beforeKey", "endKey");

            var url = _urlBase + collectionName + "?limit=" + limit;

            if (!string.IsNullOrEmpty(startKey))
                url += "&startKey=" + startKey;

            if (!string.IsNullOrEmpty(afterKey))
                url += "&afterKey=" + afterKey;

            if (!string.IsNullOrEmpty(endKey))
                url += "&endKey=" + endKey;

            if (!string.IsNullOrEmpty(beforeKey))
                url += "&beforeKey=" + beforeKey;

            var result = await Communication.CallWebRequestAsync(_apiKey, url, "GET", null);

            return JsonConvert.DeserializeObject<ListResult>(result.Payload);
        }

        #endregion

        #region Searches

        public async Task<SearchResult> SearchAsync(string collectionName, string query, int limit = 10, int offset = 0)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(query))
                throw new ArgumentNullException("query", "query cannot be null or empty");

            if (limit < 1 || limit > 100)
                throw new ArgumentOutOfRangeException("limit", "limit must be between 1 and 100");

            if (offset < 0)
                throw new ArgumentOutOfRangeException("offset", "offset must be at least 0");

            var url = _urlBase + collectionName + "?query=" + query + "&limit=" + limit + "&offset=" + offset;
            var result = await Communication.CallWebRequestAsync(_apiKey, url, "GET", null);

            return JsonConvert.DeserializeObject<SearchResult>(result.Payload);
        }

        #endregion

        #region Events

        public async Task<EventResultList> GetEventsAsync(string collectionName, string key, string type, DateTime? start = null, DateTime? end = null)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "key cannot be null or empty");

            if (string.IsNullOrEmpty(type))
                throw new ArgumentNullException("type", "type cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key + "/events/" + type;

            if (start != null)
                url += "?start=" + ConvertToUnixTimestamp(start.Value);

            if (end != null && start != null)
                url += "&end=" + ConvertToUnixTimestamp(end.Value);
            else if (end != null)
                url += "?end=" + ConvertToUnixTimestamp(end.Value);

            var result = await Communication.CallWebRequestAsync(_apiKey, url, "GET", null);

            return JsonConvert.DeserializeObject<EventResultList>(result.Payload);
        }

        public async Task<Result> PutEventAsync(string collectionName, string key, string type, DateTime? timeStamp, string item)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "key cannot be null or empty");

            if (string.IsNullOrEmpty(type))
                throw new ArgumentNullException("type", "type cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key + "/events/" + type;

            if (timeStamp != null)
                url += "?timestamp=" + ConvertToUnixTimestamp(timeStamp.Value);

            var baseResult = await Communication.CallWebRequestAsync(_apiKey, url, "PUT", item);

            return new Result
            {
                Path = new OrchestratePath
                {
                    Collection = collectionName,
                    Key = key,
                    Ref = baseResult.ETag
                },
                Score = 1,
                Value = baseResult.Payload
            };
        }

        public async Task<Result> PutEventAsync(string collectionName, string key, string type, DateTime? timeStamp, object item)
        {
            if (item == null)
                throw new ArgumentNullException("item", "item cannot be null");
            var json = JsonConvert.SerializeObject(item);

            return await PutEventAsync(collectionName, key, type, timeStamp, json);
        }

        #endregion

        #region Graphs

        public async Task<ListResult> GetGraphAsync(string collectionName, string key, string[] kinds)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "key cannot be null or empty");

            if (kinds == null || kinds.Length == 0)
                throw new ArgumentNullException("kinds", "kinds cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key + "/relations";

            url = kinds.Aggregate(url, (current, kind) => current + ("/" + kind));
            var result = await Communication.CallWebRequestAsync(_apiKey, url, "GET", null);

            return JsonConvert.DeserializeObject<ListResult>(result.Payload);
        }

        public async Task<Result> PutGraphAsync(string collectionName, string key, string kind, string toCollectionName, string toKey)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "key cannot be null or empty");

            if (string.IsNullOrEmpty(kind))
                throw new ArgumentNullException("kind", "kind cannot be null or empty");

            if (string.IsNullOrEmpty(toCollectionName))
                throw new ArgumentNullException("toCollectionName", "toCollectionName cannot be null or empty");

            if (string.IsNullOrEmpty(toKey))
                throw new ArgumentNullException("toKey", "toKey cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key + "/relation/" + kind + "/" + toCollectionName + "/" + toKey;

            var baseResult = await Communication.CallWebRequestAsync(_apiKey, url, "PUT", null);

            return new Result
            {
                Path = new OrchestratePath
                {
                    Collection = collectionName,
                    Key = key,
                    Ref = baseResult.ETag
                },
                Score = 1,
                Value = baseResult.Payload
            };
        }

        public async Task<Result> DeleteGraphAsync(string collectionName, string key, string kind, string toCollectionName, string toKey)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "key cannot be null or empty");

            if (string.IsNullOrEmpty(kind))
                throw new ArgumentNullException("kind", "kind cannot be null or empty");

            if (string.IsNullOrEmpty(toCollectionName))
                throw new ArgumentNullException("toCollectionName", "toCollectionName cannot be null or empty");

            if (string.IsNullOrEmpty(toKey))
                throw new ArgumentNullException("toKey", "toKey cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key + "/relation/" + kind + "/" + toCollectionName + "/" + toKey + "?purge=true";

            var baseResult = await Communication.CallWebRequestAsync(_apiKey, url, "DELETE", null);

            return new Result
            {
                Path = new OrchestratePath
                {
                    Collection = collectionName,
                    Key = key,
                    Ref = baseResult.ETag
                },
                Score = 1,
                Value = baseResult.Payload
            };
        }

        #endregion

        #endregion

        #region Helper Functions

        private static double ConvertToUnixTimestamp(DateTime date)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalMilliseconds);
        }

        private static string ExtractKeyFromLocation(BaseResult baseResult)
        {
            // Always in the format /<api version>/<collection>/<key>/refs/<ref>
            var locationParts = baseResult.Location.Split('/');
            return locationParts[3];
        }

        #endregion
    }
}
