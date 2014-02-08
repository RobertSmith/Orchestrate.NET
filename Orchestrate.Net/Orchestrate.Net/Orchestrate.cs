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

        public void CreateCollection(string collectionName, string key)
        {
            var url = UrlBase + collectionName + "/" + key;
            Communication.Put(url, _apiKey, new object());
        }

        public void DeleteCollection(string collectionName)
        {
            var url = UrlBase + collectionName + "?force=true";
            Communication.Delete(url, _apiKey, new object());
        }

        public void Search(string collectionName, string query, int limit, int offset)
        {
            if (limit < 1 || limit > 100)
                throw new ArgumentOutOfRangeException("limit", limit, "limit must be between 1 and 100");

            if (offset < 0)
                throw new ArgumentOutOfRangeException("offset", offset, "offset must be at least 0");

            var url = UrlBase + collectionName + "?query=" + query + "&limit=" + limit + "&offset=" + offset;
            Communication.Get(url, _apiKey);
        }
    }
}
