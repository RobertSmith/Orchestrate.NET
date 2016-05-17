using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Orchestrate.Net
{
    public partial class Orchestrate
    {
        [Obsolete("You can create collections from the Orchestrate Dashboard. You can also perform a Key/Value PUT to a non-existent collection. If a collection does not exist, one will be created.")]
        public Result CreateCollection(string collectionName, string key, string item)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (string.IsNullOrWhiteSpace(item))
                throw new ArgumentNullException(nameof(item), "item cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key;
            var baseResult = Communication.CallWebRequest(_apiKey, url, "PUT", item);

            return BuildResult(collectionName, key, baseResult);
        }

        [Obsolete("You can create collections from the Orchestrate Dashboard. You can also perform a Key/Value PUT to a non-existent collection. If a collection does not exist, one will be created.")]
        public Result CreateCollection(string collectionName, string key, object item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "item cannot be null");

            var json = JsonConvert.SerializeObject(item);
            return CreateCollection(collectionName, key, json);
        }

        public Result DeleteCollection(string collectionName)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            var url = _urlBase + collectionName + "?force=true";
            var baseResult = Communication.CallWebRequest(_apiKey, url, "DELETE", null);

            return BuildResult(collectionName, string.Empty, baseResult);
        }

        [Obsolete("You can create collections from the Orchestrate Dashboard. You can also perform a Key/Value PUT to a non-existent collection. If a collection does not exist, one will be created.")]
        public async Task<Result> CreateCollectionAsync(string collectionName, string key, object item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "item cannot be null");

            var json = JsonConvert.SerializeObject(item);
            return await CreateCollectionAsync(collectionName, key, json);
        }

        [Obsolete("You can create collections from the Orchestrate Dashboard. You can also perform a Key/Value PUT to a non-existent collection. If a collection does not exist, one will be created.")]
        public async Task<Result> CreateCollectionAsync(string collectionName, string key, string item)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (string.IsNullOrWhiteSpace(item))
                throw new ArgumentNullException(nameof(item), "item cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key;
            var baseResult = await Communication.CallWebRequestAsync(_apiKey, url, "PUT", item);

            return BuildResult(collectionName, key, baseResult);
        }

        public async Task<Result> DeleteCollectionAsync(string collectionName)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            var url = _urlBase + collectionName + "?force=true";
            var baseResult = await Communication.CallWebRequestAsync(_apiKey, url, "DELETE", null);

            return BuildResult(collectionName, string.Empty, baseResult);
        }
    }
}
