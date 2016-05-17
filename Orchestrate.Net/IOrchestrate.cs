using System;
using System.Threading.Tasks;

namespace Orchestrate.Net
{
    public interface IOrchestrate
    {
        #region Authentication

        bool Authenticate(string key);
        Task<bool> AuthenticateAsync(string key);

        #endregion

        #region Collections

        [Obsolete ("You can create collections from the Orchestrate Dashboard. You can also perform a Key/Value PUT to a non-existent collection. If a collection does not exist, one will be created.")]
        Result CreateCollection(string collectionName, string key, object item);
        [Obsolete("You can create collections from the Orchestrate Dashboard. You can also perform a Key/Value PUT to a non-existent collection. If a collection does not exist, one will be created.")]
        Result CreateCollection(string collectionName, string key, string item);
        Result DeleteCollection(string collectionName);

        [Obsolete("You can create collections from the Orchestrate Dashboard. You can also perform a Key/Value PUT to a non-existent collection. If a collection does not exist, one will be created.")]
        Task<Result> CreateCollectionAsync(string collectionName, string key, object item);
        [Obsolete("You can create collections from the Orchestrate Dashboard. You can also perform a Key/Value PUT to a non-existent collection. If a collection does not exist, one will be created.")]
        Task<Result> CreateCollectionAsync(string collectionName, string key, string item);
        Task<Result> DeleteCollectionAsync(string collectionName);

        #endregion

        #region Key/Value

        Result Get(string collectionName, string key);
        Result Get(string collectionName, string key, string reference);
        Result Post(string collectionName, object item);
        Result Post(string collectionName, string item);
        Result Put(string collectionName, string key, object item);
        Result Put(string collectionName, string key, string item);
        Result PutIfMatch(string collectionName, string key, object item, string ifMatch);
        Result PutIfMatch(string collectionName, string key, string item, string ifMatch);
        Result PutIfNoneMatch(string collectionName, string key, object item);
        Result PutIfNoneMatch(string collectionName, string key, string item);
        Result Patch(string collectionName, string key, object item);
        Result Patch(string collectionName, string key, string item);
        Result PatchIfMatch(string collectionName, string key, object item, string ifMatch);
        Result PatchIfMatch(string collectionName, string key, string item, string ifMatch);
        Result Delete(string collectionName, string key, bool purge);
        Result DeleteIfMatch(string collectionName, string key, string ifMatch, bool purge);
        ListResult List(string collectionName, int limit, string startKey, string afterKey, string endKey, string beforeKey);

        Task<Result> GetAsync(string collectionName, string key);
        Task<Result> GetAsync(string collectionName, string key, string reference);
        Task<Result> PostAsync(string collectionName, object item);
        Task<Result> PostAsync(string collectionName, string item);
        Task<Result> PutAsync(string collectionName, string key, object item);
        Task<Result> PutAsync(string collectionName, string key, string item);
        Task<Result> PutIfMatchAsync(string collectionName, string key, object item, string ifMatch);
        Task<Result> PutIfMatchAsync(string collectionName, string key, string item, string ifMatch);
        Task<Result> PutIfNoneMatchAsync(string collectionName, string key, object item);
        Task<Result> PutIfNoneMatchAsync(string collectionName, string key, string item);
        Task<Result> PatchAsync(string collectionName, string key, object item);
        Task<Result> PatchAsync(string collectionName, string key, string item);
        Task<Result> PatchIfMatchAsync(string collectionName, string key, object item, string ifMatch);
        Task<Result> PatchIfMatchAsync(string collectionName, string key, string item, string ifMatch);
        Task<Result> DeleteAsync(string collectionName, string key, bool purge);
        Task<Result> DeleteIfMatchAsync(string collectionName, string key, string ifMatch, bool purge);
        Task<ListResult> ListAsync(string collectionName, int limit, string startKey, string afterKey, string endKey, string beforeKey);

        #endregion


        Result Ref(string collectionName, string key, string reference);
        ListResult RefList(string collectionName, string key, int limit, int offset, bool values);

        SearchResult Search(string collectionName, string query, int limit, int offset, string sort, string aggregate);

        EventResultList GetEvents(string collectionName, string key, string type, DateTime? start, DateTime? end);
        EventResultList GetEvents(string collectionName, string key, string type, DateTime timestamp, Int64 ordinal);
        Result PostEvent(string collectionName, string key, string type, DateTime? timeStamp, string item);
        Result PostEvent(string collectionName, string key, string type, DateTime? timeStamp, object item);
        Result PutEvent(string collectionName, string key, string type, DateTime timestamp, Int64 ordinal, string item);
        Result PutEvent(string collectionName, string key, string type, DateTime timestamp, Int64 ordinal, object item);
        Result PutEventIfMatch(string collectionName, string key, string type, DateTime timestamp, Int64 ordinal, string item, string ifMatch);
        Result PutEventIfMatch(string collectionName, string key, string type, DateTime timestamp, Int64 ordinal, object item, string ifMatch);

        SearchResult GetGraph(string collectionName, string key, string[] kinds, int limit, int offset);
        Result PutGraph(string collectionName, string key, string kind, string toCollectionName, string toKey);
        Result DeleteGraph(string collectionName, string key, string kind, string toCollectionName, string toKey);
        Task<Result> RefAsync(string collectionName, string key, string reference);
        Task<ListResult> RefListAsync(string collectionName, string key, int limit, int offset, bool values);

        Task<SearchResult> SearchAsync(string collectionName, string query, int limit, int offset, string sort, string aggregate);

        Task<EventResultList> GetEventsAsync(string collectionName, string key, string type, DateTime? start, DateTime? end);
        Task<EventResultList> GetEventsAsync(string collectionName, string key, string type, DateTime timestamp, Int64 ordinal);
        Task<Result> PostEventAsync(string collectionName, string key, string type, DateTime? timeStamp, string item);
        Task<Result> PostEventAsync(string collectionName, string key, string type, DateTime? timeStamp, object item);
        Task<Result> PutEventAsync(string collectionName, string key, string type, DateTime timeStamp, Int64 ordinal, string item);
        Task<Result> PutEventAsync(string collectionName, string key, string type, DateTime timeStamp, Int64 ordinal, object item);
        Task<Result> PutEventIfMatchAsync(string collectionName, string key, string type, DateTime timeStamp, Int64 ordinal, string item, string ifMatch);
        Task<Result> PutEventIfMatchAsync(string collectionName, string key, string type, DateTime timeStamp, Int64 ordinal, object item, string ifMatch);

        Task<ListResult> GetGraphAsync(string collectionName, string key, string[] kinds);
        Task<Result> PutGraphAsync(string collectionName, string key, string kind, string toCollectionName, string toKey);
        Task<Result> DeleteGraphAsync(string collectionName, string key, string kind, string toCollectionName, string toKey);
    }
}
