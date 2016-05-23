using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Orchestrate.Net.Models;

namespace Orchestrate.Net
{
    public partial class Orchestrate
    {
        public BulkResult BulkOperation(string collectionName, string items, bool stream = false)
        {
            if (string.IsNullOrWhiteSpace(items))
                throw new ArgumentNullException(nameof(items), "item cannot be empty");

            var url = _urlBase + collectionName;

            return JsonConvert.DeserializeObject<BulkResult>(Communication.CallBulkWebRequest(_apiKey, url, items, stream));
        }

        public BulkResult BulkOperation(string collectionName, object items, bool stream = false)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items), "items cannot be null");

            var json = JsonConvert.SerializeObject(items);
            return BulkOperation(collectionName, json, stream);
        }
        public async Task<BulkResult> BulkOperationAsync(string collectionName, string items, bool stream = false)
        {
            if (string.IsNullOrWhiteSpace(items))
                throw new ArgumentNullException(nameof(items), "item cannot be empty");

            var url = _urlBase + collectionName;
            var baseResult = await Communication.CallBulkWebRequestAsync(_apiKey, url, items, stream);

            return JsonConvert.DeserializeObject<BulkResult>(baseResult);
        }

        public  async Task<BulkResult> BulkOperationAsync(string collectionName, object items, bool stream = false)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items), "items cannot be null");

            var json = JsonConvert.SerializeObject(items);
            return await BulkOperationAsync(collectionName, json, stream);
        }
    }
}
