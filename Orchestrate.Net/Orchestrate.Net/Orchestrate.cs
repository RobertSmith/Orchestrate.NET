using System;
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
        
        public Result CreateCollection(string collectionName, string key, object item)
        {
            if (item == null)
                throw new ArgumentNullException("item", "item cannot be null");

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

        public Result DeleteCollection(string collectionName)
        {
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

        public Result Put(string collectionName, string key, object item)
        {
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

        public Result PutIfMatch(string collectionName, string key, object item, string ifMatch)
        {
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

        public Result PutIfNoneMatch(string collectionName, string key, object item)
        {
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

        public Result Delete(string collectionName, string key)
        {
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
            if (limit < 1 || limit > 100)
                throw new ArgumentOutOfRangeException("limit", limit, "limit must be between 1 and 100");

            if (!string.IsNullOrEmpty(startKey) && !string.IsNullOrEmpty(afterKey))
                throw new ArgumentException("May only specify either a startKey or an afterKey");

            var url = UrlBase + collectionName + "?limit=" + limit;

            if (!string.IsNullOrEmpty(startKey))
                url += "&startKey=" + startKey;

            if (!string.IsNullOrEmpty(afterKey))
                url += "&afterKey=" + afterKey;

            return JsonConvert.DeserializeObject<ListResult>(Communication.CallWebRequest(_apiKey, url, "GET", null).Payload);
        }

        public Result DeleteIfMatch(string collectionName, string key, string ifMatch)
        {
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

        public SearchResult Search(string collectionName, string query, int limit, int offset)
        {
            if (limit < 1 || limit > 100)
                throw new ArgumentOutOfRangeException("limit", limit, "limit must be between 1 and 100");

            if (offset < 0)
                throw new ArgumentOutOfRangeException("offset", offset, "offset must be at least 0");

            var url = UrlBase + collectionName + "?query=" + query + "&limit=" + limit + "&offset=" + offset;

            return JsonConvert.DeserializeObject<SearchResult>(Communication.CallWebRequest(_apiKey, url, "GET", null).Payload);
        }
    }
}
