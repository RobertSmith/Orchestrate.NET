using Orchestrate.Net.Models;
using System;
using System.Threading.Tasks;

namespace Orchestrate.Net
{
    public interface IOrchestrate
    {
        Result CreateCollection(string collectionName, string key, object item);
        Result CreateCollection(string collectionName, string key, string item);
        Result DeleteCollection(string collectionName);

        Result Get(string collectionName, string key);
        Result Get(string collectionName, string key, string reference);
        Result Put(string collectionName, string key, object item);
        Result Put(string collectionName, string key, string item);
        Result PutIfMatch(string collectionName, string key, object item, string ifMatch);
        Result PutIfMatch(string collectionName, string key, string item, string ifMatch);
        Result PutIfNoneMatch(string collectionName, string key, object item);
        Result PutIfNoneMatch(string collectionName, string key, string item);
        Result Delete(string collectionName, string key, bool purge);
        Result DeleteIfMatch(string collectionName, string key, string ifMatch, bool purge);
        Result Patch(string CollectionName,string key, PatchModel item);
        Result Patch(string CollectionName, string key, object item);
       
        ListResult List(string collectionName, int limit, string startKey, string afterKey);

        SearchResult Search(string collectionName, string query, int limit, int offset);

        EventResultList GetEvents(string collectionName, string key, string type, DateTime? start, DateTime? end);
        Result PutEvent(string collectionName, string key, string type, DateTime? timeStamp, string msg);

        ListResult GetGraph(string collectionName, string key, string[] kinds);
        Result PutGraph(string collectionName, string key, string kind, string toCollectionName, string toKey);
        Result DeleteGraph(string collectionName, string key, string kind, string toCollectionName, string toKey);

        Task<Result> CreateCollectionAsync(string collectionName, string key, object item);
        Task<Result> CreateCollectionAsync(string collectionName, string key, string item);
        Task<Result> DeleteCollectionAsync(string collectionName);

        Task<Result> GetAsync(string collectionName, string key);
        Task<Result> GetAsync(string collectionName, string key, string reference);
        Task<Result> PutAsync(string collectionName, string key, object item);
        Task<Result> PutAsync(string collectionName, string key, string item);
        Task<Result> PutIfMatchAsync(string collectionName, string key, object item, string ifMatch);
        Task<Result> PutIfMatchAsync(string collectionName, string key, string item, string ifMatch);
        Task<Result> PutIfNoneMatchAsync(string collectionName, string key, object item);
        Task<Result> PutIfNoneMatchAsync(string collectionName, string key, string item);
        Task<Result> DeleteAsync(string collectionName, string key, bool purge);
        Task<Result> DeleteIfMatchAsync(string collectionName, string key, string ifMatch, bool purge);
        Task<Result> PatchAsync(string CollectionName, string Operation, PatchModel item);


        Task<ListResult> ListAsync(string collectionName, int limit, string startKey, string afterKey);

        Task<SearchResult> SearchAsync(string collectionName, string query, int limit, int offset);

        Task<EventResultList> GetEventsAsync(string collectionName, string key, string type, DateTime? start, DateTime? end);
        Task<Result> PutEventAsync(string collectionName, string key, string type, DateTime? timeStamp, string msg);

        Task<ListResult> GetGraphAsync(string collectionName, string key, string[] kinds);
        Task<Result> PutGraphAsync(string collectionName, string key, string kind, string toCollectionName, string toKey);
        Task<Result> DeleteGraphAsync(string collectionName, string key, string kind, string toCollectionName, string toKey);
    }
}
