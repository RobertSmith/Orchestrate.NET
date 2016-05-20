using System;
using System.Threading.Tasks;

namespace Orchestrate.Net
{
    public partial class Orchestrate
    {
        public Result DeleteCollection(string collectionName)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            var url = _urlBase + collectionName + "?force=true";
            var baseResult = Communication.CallWebRequest(_apiKey, url, "DELETE", null);

            return BuildResult(collectionName, string.Empty, baseResult);
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
