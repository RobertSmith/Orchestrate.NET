using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Orchestrate.Net
{
    public partial class Orchestrate
    {
        public EventResultList GetEvents(string collectionName, string key, string type, DateTime timestamp, Int64 ordinal)
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

            var url = _urlBase + collectionName + "/" + key + "/events/" + type + "/" + ConvertToUnixTimestamp(timestamp) + "/" + ordinal;

            return JsonConvert.DeserializeObject<EventResultList>(Communication.CallWebRequest(_apiKey, url, "GET", null).Payload);
        }

        /// <summary>
        /// Depreciated
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="key"></param>
        /// <param name="type"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public EventResultList GetEvents(string collectionName, string key, string type, DateTime? start = null, DateTime? end = null)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type), "type cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key + "/events/" + type;

            if (start != null)
                url += "?start=" + ConvertToUnixTimestamp(start.Value);

            if (end != null && start != null)
                url += "&end=" + ConvertToUnixTimestamp(end.Value);
            else if (end != null)
                url += "?end=" + ConvertToUnixTimestamp(end.Value);

            return JsonConvert.DeserializeObject<EventResultList>(Communication.CallWebRequest(_apiKey, url, "GET", null).Payload);
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

        public Result PostEvent(string collectionName, string key, string type, DateTime? timeStamp, object item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "item cannot be null");

            var json = JsonConvert.SerializeObject(item);

            return PostEvent(collectionName, key, type, timeStamp, json);
        }

        public Result PutEvent(string collectionName, string key, string type, DateTime timeStamp, Int64 ordinal, string item)
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

        public Result PutEvent(string collectionName, string key, string type, DateTime timeStamp, Int64 ordinal, object item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "item cannot be null");

            var json = JsonConvert.SerializeObject(item);

            return PutEvent(collectionName, key, type, timeStamp, ordinal, json);
        }

        public Result PutEventIfMatch(string collectionName, string key, string type, DateTime timeStamp, Int64 ordinal, string item, string ifMatch)
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

        public Result PutEventIfMatch(string collectionName, string key, string type, DateTime timeStamp, Int64 ordinal, object item, string ifMatch)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "item cannot be null");

            var json = JsonConvert.SerializeObject(item);

            return PutEventIfMatch(collectionName, key, type, timeStamp, ordinal, json, ifMatch);
        }

        /// <summary>
        /// Depreciated
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="key"></param>
        /// <param name="type"></param>
        /// <param name="timeStamp"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public Result PutEvent(string collectionName, string key, string type, DateTime? timeStamp, string item)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type), "type cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key + "/events/" + type;

            if (timeStamp != null)
                url += "?timestamp=" + ConvertToUnixTimestamp(timeStamp.Value);

            var baseResult = Communication.CallWebRequest(_apiKey, url, "PUT", item);

            return BuildResult(collectionName, key, baseResult);
        }

        /// <summary>
        /// Depreciated
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="key"></param>
        /// <param name="type"></param>
        /// <param name="timeStamp"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public Result PutEvent(string collectionName, string key, string type, DateTime? timeStamp, object item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "item cannot be null");

            var json = JsonConvert.SerializeObject(item);

            return PutEvent(collectionName, key, type, timeStamp, json);
        }

        public async Task<EventResultList> GetEventsAsync(string collectionName, string key, string type, DateTime timestamp, Int64 ordinal)
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

            var url = _urlBase + collectionName + "/" + key + "/events/" + type + "/" + ConvertToUnixTimestamp(timestamp) + "/" + ordinal;

            var result = await Communication.CallWebRequestAsync(_apiKey, url, "GET", null);

            return JsonConvert.DeserializeObject<EventResultList>(result.Payload);
        }

        /// <summary>
        /// Depreciated
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="key"></param>
        /// <param name="type"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public async Task<EventResultList> GetEventsAsync(string collectionName, string key, string type, DateTime? start = null, DateTime? end = null)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type), "type cannot be null or empty");

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

        public async Task<Result> PostEventAsync(string collectionName, string key, string type, DateTime? timeStamp, object item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "item cannot be null");

            var json = JsonConvert.SerializeObject(item);

            return await PostEventAsync(collectionName, key, type, timeStamp, json);
        }

        public async Task<Result> PutEventAsync(string collectionName, string key, string type, DateTime timeStamp, Int64 ordinal, string item)
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

        public async Task<Result> PutEventAsync(string collectionName, string key, string type, DateTime timeStamp, Int64 ordinal, object item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "item cannot be null");

            var json = JsonConvert.SerializeObject(item);

            return await PutEventAsync(collectionName, key, type, timeStamp, json);
        }

        public async Task<Result> PutEventIfMatchAsync(string collectionName, string key, string type, DateTime timeStamp, Int64 ordinal, string item, string ifMatch)
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

        public async Task<Result> PutEventIfMatchAsync(string collectionName, string key, string type, DateTime timeStamp, Int64 ordinal, object item, string ifMatch)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "item cannot be null");

            var json = JsonConvert.SerializeObject(item);

            return await PutEventIfMatchAsync(collectionName, key, type, timeStamp, ordinal, json, ifMatch);
        }

        /// <summary>
        /// Depreciated
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="key"></param>
        /// <param name="type"></param>
        /// <param name="timeStamp"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<Result> PutEventAsync(string collectionName, string key, string type, DateTime? timeStamp, string item)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName), "collectionName cannot be null or empty");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type), "type cannot be null or empty");

            var url = _urlBase + collectionName + "/" + key + "/events/" + type;

            if (timeStamp != null)
                url += "?timestamp=" + ConvertToUnixTimestamp(timeStamp.Value);

            var baseResult = await Communication.CallWebRequestAsync(_apiKey, url, "PUT", item);

            return BuildResult(collectionName, key, baseResult);
        }

        /// <summary>
        /// Depreciated
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="key"></param>
        /// <param name="type"></param>
        /// <param name="timeStamp"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<Result> PutEventAsync(string collectionName, string key, string type, DateTime? timeStamp, object item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "item cannot be null");

            var json = JsonConvert.SerializeObject(item);

            return await PutEventAsync(collectionName, key, type, timeStamp, json);
        }
    }
}
