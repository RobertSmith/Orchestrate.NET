using System;

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
        
        public string CreateCollection(string collectionName, string key)
        {
            var url = UrlBase + collectionName + "/" + key;
            return Communication.CallWebRequest(_apiKey, url, "PUT", new object());
        }

        public string DeleteCollection(string collectionName)
        {
            var url = UrlBase + collectionName + "?force=true";
            return Communication.CallWebRequest(_apiKey, url, "DELETE", null);
        }

        public string Get(string collectionName, string key)
        {
            var url = UrlBase + collectionName + "/" + key;
            return Communication.CallWebRequest(_apiKey, url, "GET", null);
        }

        public string Search(string collectionName, string query, int limit, int offset)
        {
            if (limit < 1 || limit > 100)
                throw new ArgumentOutOfRangeException("limit", limit, "limit must be between 1 and 100");

            if (offset < 0)
                throw new ArgumentOutOfRangeException("offset", offset, "offset must be at least 0");

            var url = UrlBase + collectionName + "?query=" + query + "&limit=" + limit + "&offset=" + offset;
            return Communication.CallWebRequest(_apiKey, url, "GET", null);
        }
    }
}
