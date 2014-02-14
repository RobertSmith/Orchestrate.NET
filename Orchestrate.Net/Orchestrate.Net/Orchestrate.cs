using System;
using System.Linq;
using Newtonsoft.Json;

namespace Orchestrate.Net
{
    public class Orchestrate : IOrchestrate
    {
        private readonly string _apiKey;
        private const string UrlBase = @"https://api.orchestrate.io/v0/";

        public Orchestrate(string apiKey)
        {
            _apiKey = apiKey;
        }

        public Result CreateCollection(string collectionName, string key, string item)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "key cannot be null or empty");

            if (string.IsNullOrEmpty(item))
                throw new ArgumentNullException("item", "item cannot be null or empty");

            var url = UrlBase + collectionName + "/" + key;
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
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "key cannot be null or empty");

            if (item == null)
                throw new ArgumentNullException("item", "item cannot be null");

            var json = JsonConvert.SerializeObject(item);
            return CreateCollection(collectionName, key, json);
        }

        public Result DeleteCollection(string collectionName)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            var url = UrlBase + collectionName + "?force=true";
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

        public Result Get(string collectionName, string key)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "key cannot be null or empty");

            var url = UrlBase + collectionName + "/" + key;
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

            var url = UrlBase + collectionName + "/" + key + "/refs/" + reference;
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

        public Result Put(string collectionName, string key, string item)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "key cannot be null or empty");

            if (string.IsNullOrEmpty(item))
                throw new ArgumentNullException("item", "item cannot be empty");

            var url = UrlBase + collectionName + "/" + key;
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
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "key cannot be null or empty");

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

            var url = UrlBase + collectionName + "/" + key;
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
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "key cannot be null or empty");

            if (item == null)
                throw new ArgumentNullException("item", "item cannot be null");

            if (string.IsNullOrEmpty(ifMatch))
                throw new ArgumentNullException("ifMatch", "ifMatch cannot be empty");

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

            var url = UrlBase + collectionName + "/" + key;
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
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "key cannot be null or empty");

            if (item == null)
                throw new ArgumentNullException("item", "item cannot be null");

            var json = JsonConvert.SerializeObject(item);
            return PutIfNoneMatch(collectionName, key, json);
        }

        public Result Delete(string collectionName, string key)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "key cannot be null or empty");

            var url = UrlBase + collectionName + "/" + key;
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

        public ListResult List(string collectionName, int limit, string startKey, string afterKey)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (limit < 1 || limit > 100)
                throw new ArgumentOutOfRangeException("limit", limit, "limit must be between 1 and 100");

            if (!string.IsNullOrEmpty(startKey) && !string.IsNullOrEmpty(afterKey))
                throw new ArgumentException("May only specify either a startKey or an afterKey", "startKey");

            var url = UrlBase + collectionName + "?limit=" + limit;

            if (!string.IsNullOrEmpty(startKey))
                url += "&startKey=" + startKey;

            if (!string.IsNullOrEmpty(afterKey))
                url += "&afterKey=" + afterKey;

            return JsonConvert.DeserializeObject<ListResult>(Communication.CallWebRequest(_apiKey, url, "GET", null).Payload);
        }

        public Result DeleteIfMatch(string collectionName, string key, string ifMatch)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "key cannot be null or empty");

            if (string.IsNullOrEmpty(ifMatch))
                throw new ArgumentNullException("ifMatch", "ifMatch cannot be null or empty");

            var url = UrlBase + collectionName + "/" + key;
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

        public SearchResult Search(string collectionName, string query, int limit = 10, int offset = 0)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(query))
                throw new ArgumentNullException("query", "query cannot be null or empty");

            if (limit < 1 || limit > 100)
                throw new ArgumentOutOfRangeException("limit", limit, "limit must be between 1 and 100");

            if (offset < 0)
                throw new ArgumentOutOfRangeException("offset", offset, "offset must be at least 0");

            var url = UrlBase + collectionName + "?query=" + query + "&limit=" + limit + "&offset=" + offset;

            return JsonConvert.DeserializeObject<SearchResult>(Communication.CallWebRequest(_apiKey, url, "GET", null).Payload);
        }

        public EventResultList GetEvents(string collectionName, string key, string type, DateTime? start = null, DateTime? end = null)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "key cannot be null or empty");

            if (string.IsNullOrEmpty(type))
                throw new ArgumentNullException("type", "type cannot be null or empty");

            var url = UrlBase + collectionName + "/" + key + "/events/" + type;

            if (start != null)
                url += "?start=" + ConvertToUnixTimestamp(start.Value);

            if (end != null && start != null)
                url += "&end=" + ConvertToUnixTimestamp(end.Value);
            else if (end != null)
                url += "?end=" + ConvertToUnixTimestamp(end.Value);

            return JsonConvert.DeserializeObject<EventResultList>(Communication.CallWebRequest(_apiKey, url, "GET", null).Payload);
        }

        public Result PutEvent(string collectionName, string key, string type, DateTime? timeStamp, string msg)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "key cannot be null or empty");

            if (string.IsNullOrEmpty(type))
                throw new ArgumentNullException("type", "type cannot be null or empty");

            var url = UrlBase + collectionName + "/" + key + "/events/" + type;

            if (timeStamp != null)
                url += "?timestamp=" + ConvertToUnixTimestamp(timeStamp.Value);

            var message = new EventMessage { Msg = msg };
            var json = JsonConvert.SerializeObject(message);

            var baseResult = Communication.CallWebRequest(_apiKey, url, "PUT", json);

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

        public ListResult GetGraph(string collectionName, string key, string[] kinds)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentNullException("collectionName", "collectionName cannot be null or empty");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key", "key cannot be null or empty");

            if (kinds == null || kinds.Length == 0)
                throw new ArgumentNullException("kinds", "kinds cannot be null or empty");

            var url = UrlBase + collectionName + "/" + key + "/relations";

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

            var url = UrlBase + collectionName + "/" + key + "/relation/" + kind + "/" + toCollectionName + "/" + toKey;

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
        
        #region Helper Functions
        
        private static double ConvertToUnixTimestamp(DateTime date)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalMilliseconds);
        }

        #endregion

        #region IOrchestrate Members

        #endregion
    }
}
