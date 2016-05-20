using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Orchestrate.Net
{
    public partial class Orchestrate
    {
        #region Get Event

        public EventResult GetEvent(string collectionName, string key, string type, DateTime timestamp, long ordinal)
        {
            var ts = ConvertToUnixTimestamp(timestamp);

            return GetEvent(collectionName, key, type, ts, ordinal);
        }

        public EventResult GetEvent(string collectionName, string key, string type, long timestamp, long ordinal)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type), "type cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key + "/events/" + type + "/" + timestamp + "/" + ordinal;

            return JsonConvert.DeserializeObject<EventResult>(Communication.CallWebRequest(_apiKey, url, "GET", null).Payload);
        }

        public async Task<EventResult> GetEventAsync(string collectionName, string key, string type, DateTime timestamp, long ordinal)
        {
            var ts = ConvertToUnixTimestamp(timestamp);

            return await GetEventAsync(collectionName, key, type, ts, ordinal);
        }

        public async Task<EventResult> GetEventAsync(string collectionName, string key, string type, long timestamp, long ordinal)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type), "type cannot be null or empty");

            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(timestamp), "timestamp cannot be null or empty");

            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(ordinal), "ordinal cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key + "/events/" + type + "/" + timestamp + "/" + ordinal;

            var result = await Communication.CallWebRequestAsync(_apiKey, url, "GET", null);

            return JsonConvert.DeserializeObject<EventResult>(result.Payload);
        }

        #endregion

        #region Post Event

        public Result PostEvent(string collectionName, string key, string type, DateTime? timeStamp, object item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "item cannot be null");

            var json = JsonConvert.SerializeObject(item);

            return PostEvent(collectionName, key, type, timeStamp, json);
        }

        public Result PostEvent(string collectionName, string key, string type, DateTime? timeStamp, string item)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type), "type cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key + "/events/" + type;

            if (timeStamp != null)
                url += "/" + ConvertToUnixTimestamp(timeStamp.Value);

            var baseResult = Communication.CallWebRequest(_apiKey, url, "POST", item);

            return BuildResult(collectionName, key, baseResult);
        }

        public async Task<Result> PostEventAsync(string collectionName, string key, string type, DateTime? timeStamp, object item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "item cannot be null");

            var json = JsonConvert.SerializeObject(item);

            return await PostEventAsync(collectionName, key, type, timeStamp, json);
        }

        public async Task<Result> PostEventAsync(string collectionName, string key, string type, DateTime? timeStamp, string item)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type), "type cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key + "/events/" + type;

            if (timeStamp != null)
                url += "/" + ConvertToUnixTimestamp(timeStamp.Value);

            var baseResult = await Communication.CallWebRequestAsync(_apiKey, url, "POST", item);

            return BuildResult(collectionName, key, baseResult);
        }

        #endregion

        #region Put Event

        public Result PutEvent(string collectionName, string key, string type, DateTime timeStamp, long ordinal, object item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "item cannot be null");

            var json = JsonConvert.SerializeObject(item);

            return PutEvent(collectionName, key, type, timeStamp, ordinal, json);
        }

        public Result PutEvent(string collectionName, string key, string type, DateTime timeStamp, long ordinal, string item)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type), "type cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key + "/events/" + type + "/" + ConvertToUnixTimestamp(timeStamp) + "/" + ordinal;

            var baseResult = Communication.CallWebRequest(_apiKey, url, "PUT", item);

            return BuildResult(collectionName, key, baseResult);
        }

        public Result PutEvent(string collectionName, string key, string type, long timeStamp, long ordinal, object item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "item cannot be null");

            var json = JsonConvert.SerializeObject(item);

            return PutEvent(collectionName, key, type, timeStamp, ordinal, json);
        }

        public Result PutEvent(string collectionName, string key, string type, long timeStamp, long ordinal, string item)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type), "type cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key + "/events/" + type + "/" + timeStamp + "/" + ordinal;

            var baseResult = Communication.CallWebRequest(_apiKey, url, "PUT", item);

            return BuildResult(collectionName, key, baseResult);
        }

        public async Task<Result> PutEventAsync(string collectionName, string key, string type, DateTime timeStamp, long ordinal, object item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "item cannot be null");

            var json = JsonConvert.SerializeObject(item);

            return await PutEventAsync(collectionName, key, type, timeStamp, ordinal, json);
        }

        public async Task<Result> PutEventAsync(string collectionName, string key, string type, DateTime timeStamp, long ordinal, string item)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type), "type cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key + "/events/" + type + "/" + ConvertToUnixTimestamp(timeStamp) + "/" + ordinal;

            var baseResult = await Communication.CallWebRequestAsync(_apiKey, url, "PUT", item);

            return BuildResult(collectionName, key, baseResult);
        }

        public async Task<Result> PutEventAsync(string collectionName, string key, string type, long timeStamp, long ordinal, object item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "item cannot be null");

            var json = JsonConvert.SerializeObject(item);

            return await PutEventAsync(collectionName, key, type, timeStamp, ordinal, json);
        }

        public async Task<Result> PutEventAsync(string collectionName, string key, string type, long timeStamp, long ordinal, string item)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type), "type cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key + "/events/" + type + "/" + timeStamp + "/" + ordinal;

            var baseResult = await Communication.CallWebRequestAsync(_apiKey, url, "PUT", item);

            return BuildResult(collectionName, key, baseResult);
        }

        public Result PutEventIfMatch(string collectionName, string key, string type, DateTime timeStamp, long ordinal, object item, string ifMatch)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "item cannot be null");

            var json = JsonConvert.SerializeObject(item);

            return PutEventIfMatch(collectionName, key, type, timeStamp, ordinal, json, ifMatch);
        }

        public Result PutEventIfMatch(string collectionName, string key, string type, DateTime timeStamp, long ordinal, string item, string ifMatch)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type), "type cannot be null or empty");

            if (string.IsNullOrWhiteSpace(ifMatch))
                throw new ArgumentNullException(nameof(ifMatch), "ifMatch cannot be empty");

            var url = _urlBase + collectionName + "/" + key + "/events/" + type + "/" + ConvertToUnixTimestamp(timeStamp) + "/" + ordinal;

            var baseResult = Communication.CallWebRequest(_apiKey, url, "PUT", item, ifMatch);

            return BuildResult(collectionName, key, baseResult);
        }

        public Result PutEventIfMatch(string collectionName, string key, string type, long timeStamp, long ordinal, object item, string ifMatch)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "item cannot be null");

            var json = JsonConvert.SerializeObject(item);

            return PutEventIfMatch(collectionName, key, type, timeStamp, ordinal, json, ifMatch);
        }

        public Result PutEventIfMatch(string collectionName, string key, string type, long timeStamp, long ordinal, string item, string ifMatch)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type), "type cannot be null or empty");

            if (string.IsNullOrWhiteSpace(ifMatch))
                throw new ArgumentNullException(nameof(ifMatch), "ifMatch cannot be empty");

            var url = _urlBase + collectionName + "/" + key + "/events/" + type + "/" + timeStamp + "/" + ordinal;

            var baseResult = Communication.CallWebRequest(_apiKey, url, "PUT", item, ifMatch);

            return BuildResult(collectionName, key, baseResult);
        }

        public async Task<Result> PutEventIfMatchAsync(string collectionName, string key, string type, DateTime timeStamp, long ordinal, object item, string ifMatch)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "item cannot be null");

            var json = JsonConvert.SerializeObject(item);

            return await PutEventIfMatchAsync(collectionName, key, type, timeStamp, ordinal, json, ifMatch);
        }

        public async Task<Result> PutEventIfMatchAsync(string collectionName, string key, string type, DateTime timeStamp, long ordinal, string item, string ifMatch)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type), "type cannot be null or empty");

            if (string.IsNullOrWhiteSpace(ifMatch))
                throw new ArgumentNullException(nameof(ifMatch), "ifMatch cannot be empty");

            var url = _urlBase + collectionName + "/" + key + "/events/" + type + "/" + ConvertToUnixTimestamp(timeStamp) + "/" + ordinal;

            var baseResult = await Communication.CallWebRequestAsync(_apiKey, url, "PUT", item, ifMatch);

            return BuildResult(collectionName, key, baseResult);
        }

        public async Task<Result> PutEventIfMatchAsync(string collectionName, string key, string type, long timeStamp, long ordinal, object item, string ifMatch)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "item cannot be null");

            var json = JsonConvert.SerializeObject(item);

            return await PutEventIfMatchAsync(collectionName, key, type, timeStamp, ordinal, json, ifMatch);
        }

        public async Task<Result> PutEventIfMatchAsync(string collectionName, string key, string type, long timeStamp, long ordinal, string item, string ifMatch)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type), "type cannot be null or empty");

            if (string.IsNullOrWhiteSpace(ifMatch))
                throw new ArgumentNullException(nameof(ifMatch), "ifMatch cannot be empty");

            var url = _urlBase + collectionName + "/" + key + "/events/" + type + "/" + timeStamp + "/" + ordinal;

            var baseResult = await Communication.CallWebRequestAsync(_apiKey, url, "PUT", item, ifMatch);

            return BuildResult(collectionName, key, baseResult);
        }

        #endregion

        #region List Events

        public EventResultList ListEvents(string collectionName, string key, string type, int limit)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type), "type cannot be null or empty");

            if (limit < 1)
                throw new ArgumentException("Limit must be greater than 0", nameof(limit));

            if (limit > 100)
                throw new ArgumentException("Max Limit is 100", nameof(limit));

            var url = _urlBase + collectionName + "/" + key + "/events/" + type + "?limit=" + limit;

            return JsonConvert.DeserializeObject<EventResultList>(Communication.CallWebRequest(_apiKey, url, "GET", null).Payload);
        }

        public EventResultList ListEvents(string collectionName, string key, string type, 
            int? limit = 100, DateTime? startEvent = null, DateTime? endEvent = null, 
            DateTime? afterEvent = null, DateTime? beforeEvent = null)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type), "type cannot be null or empty");

            if (limit < 1)
                throw new ArgumentException("Limit must be greater than 0", nameof(limit));

            if (limit > 100)
                throw new ArgumentException("Max Limit is 100", nameof(limit));

            var url = _urlBase + collectionName + "/" + key + "/events/" + type + "?limit=" + limit;

            if (startEvent != null)
                url += "&startEvent=" + ConvertToUnixTimestamp(startEvent.Value);

            if (endEvent != null)
                url += "&endEvent=" + ConvertToUnixTimestamp(endEvent.Value);

            if (afterEvent != null)
                url += "&afterEvent=" + ConvertToUnixTimestamp(afterEvent.Value);

            if (beforeEvent != null)
                url += "&beforeEvent=" + ConvertToUnixTimestamp(beforeEvent.Value);

            return JsonConvert.DeserializeObject<EventResultList>(Communication.CallWebRequest(_apiKey, url, "GET", null).Payload);
        }

        public EventResultList ListEvents(string collectionName, string key, string type, 
            int? limit = 100, long? startEvent = null, long? endEvent = null, long? afterEvent = null, 
            long? beforeEvent = null)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type), "type cannot be null or empty");

            if (limit < 1)
                throw new ArgumentException("Limit must be greater than 0", nameof(limit));

            if (limit > 100)
                throw new ArgumentException("Max Limit is 100", nameof(limit));

            var url = _urlBase + collectionName + "/" + key + "/events/" + type + "?limit=" + limit;

            if (startEvent != null)
                url += "&startEvent=" + startEvent.Value;

            if (endEvent != null)
                url += "&endEvent=" + endEvent.Value;

            if (afterEvent != null)
                url += "&afterEvent=" + afterEvent.Value;

            if (beforeEvent != null)
                url += "&beforeEvent=" + beforeEvent.Value;

            return JsonConvert.DeserializeObject<EventResultList>(Communication.CallWebRequest(_apiKey, url, "GET", null).Payload);
        }

        public async Task<EventResultList> ListEventsAsync(string collectionName, string key, string type, int limit)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type), "type cannot be null or empty");

            if (limit < 1)
                throw new ArgumentException("Limit must be greater than 0", nameof(limit));

            if (limit > 100)
                throw new ArgumentException("Max Limit is 100", nameof(limit));

            var url = _urlBase + collectionName + "/" + key + "/events/" + type + "?limit=" + limit;

            var result = await Communication.CallWebRequestAsync(_apiKey, url, "GET", null);

            return JsonConvert.DeserializeObject<EventResultList>(result.Payload);
        }

        public async Task<EventResultList> ListEventsAsync(string collectionName, string key,
            string type, int? limit = 100, DateTime? startEvent = null, DateTime? endEvent = null,
            DateTime? afterEvent = null, DateTime? beforeEvent = null)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type), "type cannot be null or empty");

            if (limit < 1)
                throw new ArgumentException("Limit must be greater than 0", nameof(limit));

            if (limit > 100)
                throw new ArgumentException("Max Limit is 100", nameof(limit));

            var url = _urlBase + collectionName + "/" + key + "/events/" + type + "?limit=" + limit;

            if (startEvent != null)
                url += "&startEvent=" + ConvertToUnixTimestamp(startEvent.Value);

            if (endEvent != null)
                url += "&endEvent=" + ConvertToUnixTimestamp(endEvent.Value);

            if (afterEvent != null)
                url += "&afterEvent=" + ConvertToUnixTimestamp(afterEvent.Value);

            if (beforeEvent != null)
                url += "&beforeEvent=" + ConvertToUnixTimestamp(beforeEvent.Value);

            var result = await Communication.CallWebRequestAsync(_apiKey, url, "GET", null);

            return JsonConvert.DeserializeObject<EventResultList>(result.Payload);
        }

        public async Task<EventResultList> ListEventsAsync(string collectionName, string key, 
            string type, int? limit = 100, long? startEvent = null, long? endEvent = null, 
            long? afterEvent = null, long? beforeEvent = null)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type), "type cannot be null or empty");

            if (limit < 1)
                throw new ArgumentException("Limit must be greater than 0", nameof(limit));

            if (limit > 100)
                throw new ArgumentException("Max Limit is 100", nameof(limit));

            var url = _urlBase + collectionName + "/" + key + "/events/" + type + "?limit=" + limit;

            if (startEvent != null)
                url += "&startEvent=" + startEvent.Value;

            if (endEvent != null)
                url += "&endEvent=" + endEvent.Value;

            if (afterEvent != null)
                url += "&afterEvent=" + afterEvent.Value;

            if (beforeEvent != null)
                url += "&beforeEvent=" + beforeEvent.Value;

            var result = await Communication.CallWebRequestAsync(_apiKey, url, "GET", null);

            return JsonConvert.DeserializeObject<EventResultList>(result.Payload);
        }

        #endregion
    }
}
