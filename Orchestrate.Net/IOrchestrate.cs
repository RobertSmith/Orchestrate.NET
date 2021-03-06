﻿using System;
using System.Threading.Tasks;
using Orchestrate.Net.Models;

namespace Orchestrate.Net
{
    public interface IOrchestrate
    {
        #region Authentication

        bool Authenticate(string key);
        Task<bool> AuthenticateAsync(string key);

        #endregion

        #region Collections

        Result DeleteCollection(string collectionName);
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

        #region Refs

        Result Ref(string collectionName, string key, string reference);
        Task<Result> RefAsync(string collectionName, string key, string reference);
        ListResult RefList(string collectionName, string key, int limit, int offset, bool values);
        Task<ListResult> RefListAsync(string collectionName, string key, int limit, int offset, bool values);

        #endregion

        #region Search

        SearchResult Search(string collectionName, string query, int limit, int offset, string sort, string aggregate);
        Task<SearchResult> SearchAsync(string collectionName, string query, int limit, int offset, string sort, string aggregate);

        #endregion

        #region Events

        EventResult GetEvent(string collectionName, string key, string type, DateTime timestamp, long ordinal);
        EventResult GetEvent(string collectionName, string key, string type, long timestamp, long ordinal);
        Result PostEvent(string collectionName, string key, string type, DateTime? timeStamp, string item);
        Result PostEvent(string collectionName, string key, string type, DateTime? timeStamp, object item);
        Result PutEvent(string collectionName, string key, string type, DateTime timestamp, long ordinal, string item);
        Result PutEvent(string collectionName, string key, string type, DateTime timestamp, long ordinal, object item);
        Result PutEvent(string collectionName, string key, string type, long timestamp, long ordinal, string item);
        Result PutEvent(string collectionName, string key, string type, long timestamp, long ordinal, object item);
        Result PutEventIfMatch(string collectionName, string key, string type, DateTime timestamp, long ordinal, string item, string ifMatch);
        Result PutEventIfMatch(string collectionName, string key, string type, DateTime timestamp, long ordinal, object item, string ifMatch);
        Result PutEventIfMatch(string collectionName, string key, string type, long timestamp, long ordinal, string item, string ifMatch);
        Result PutEventIfMatch(string collectionName, string key, string type, long timestamp, long ordinal, object item, string ifMatch);
        EventResultList ListEvents(string collectionName, string key, string type, int limit);
        EventResultList ListEvents(string collectionName, string key, string type, int? limit, DateTime? startEvent, DateTime? endEvent, DateTime? afterEvent, DateTime? beforeEvent);
        EventResultList ListEvents(string collectionName, string key, string type, int? limit, long? startEvent, long? endEvent, long? afterEvent, long? beforeEvent);

        Task<EventResult> GetEventAsync(string collectionName, string key, string type, DateTime timestamp, long ordinal);
        Task<EventResult> GetEventAsync(string collectionName, string key, string type, long timestamp, long ordinal);
        Task<Result> PostEventAsync(string collectionName, string key, string type, DateTime? timeStamp, string item);
        Task<Result> PostEventAsync(string collectionName, string key, string type, DateTime? timeStamp, object item);
        Task<Result> PutEventAsync(string collectionName, string key, string type, DateTime timeStamp, long ordinal, string item);
        Task<Result> PutEventAsync(string collectionName, string key, string type, DateTime timeStamp, long ordinal, object item);
        Task<Result> PutEventAsync(string collectionName, string key, string type, long timeStamp, long ordinal, string item);
        Task<Result> PutEventAsync(string collectionName, string key, string type, long timeStamp, long ordinal, object item);
        Task<Result> PutEventIfMatchAsync(string collectionName, string key, string type, DateTime timeStamp, long ordinal, string item, string ifMatch);
        Task<Result> PutEventIfMatchAsync(string collectionName, string key, string type, DateTime timeStamp, long ordinal, object item, string ifMatch);
        Task<Result> PutEventIfMatchAsync(string collectionName, string key, string type, long timeStamp, long ordinal, string item, string ifMatch);
        Task<Result> PutEventIfMatchAsync(string collectionName, string key, string type, long timeStamp, long ordinal, object item, string ifMatch);
        Task<EventResultList> ListEventsAsync(string collectionName, string key, string type, int limit);
        Task<EventResultList> ListEventsAsync(string collectionName, string key, string type, int? limit, DateTime? startEvent, DateTime? endEvent, DateTime? afterEvent, DateTime? beforeEvent);
        Task<EventResultList> ListEventsAsync(string collectionName, string key, string type, int? limit, long? startEvent, long? endEvent, long? afterEvent, long? beforeEvent);

        #endregion

        #region Graphs

        SearchResult GetGraph(string collectionName, string key, string[] kinds, int limit, int offset);
        Result PutGraph(string collectionName, string key, string kind, string toCollectionName, string toKey);
        Result PutGraph(string collectionName, string key, string kind, string toCollectionName, string toKey, object properties);
        Result PutGraph(string collectionName, string key, string kind, string toCollectionName, string toKey, string properties);
        Result PutGraphIfMatch(string collectionName, string key, string kind, string toCollectionName, string toKey, string ifMatch);
        Result PutGraphIfMatch(string collectionName, string key, string kind, string toCollectionName, string toKey, string ifMatch, object properties);
        Result PutGraphIfMatch(string collectionName, string key, string kind, string toCollectionName, string toKey, string ifMatch, string properties);
        Result PutGraphIfNoneMatch(string collectionName, string key, string kind, string toCollectionName, string toKey);
        Result PutGraphIfNoneMatch(string collectionName, string key, string kind, string toCollectionName, string toKey, object properties);
        Result PutGraphIfNoneMatch(string collectionName, string key, string kind, string toCollectionName, string toKey, string properties);
        Result DeleteGraph(string collectionName, string key, string kind, string toCollectionName, string toKey);

        Task<ListResult> GetGraphAsync(string collectionName, string key, string[] kinds);
        Task<Result> PutGraphAsync(string collectionName, string key, string kind, string toCollectionName, string toKey);
        Task<Result> PutGraphAsync(string collectionName, string key, string kind, string toCollectionName, string toKey, object properties);
        Task<Result> PutGraphAsync(string collectionName, string key, string kind, string toCollectionName, string toKey, string properties);
        Task<Result> PutGraphIfMatchAsync(string collectionName, string key, string kind, string toCollectionName, string toKey, string ifMatch);
        Task<Result> PutGraphIfMatchAsync(string collectionName, string key, string kind, string toCollectionName, string toKey, string ifMatch, object properties);
        Task<Result> PutGraphIfMatchAsync(string collectionName, string key, string kind, string toCollectionName, string toKey, string ifMatch, string properties);
        Task<Result> PutGraphIfNoneMatchAsync(string collectionName, string key, string kind, string toCollectionName, string toKey);
        Task<Result> PutGraphIfNoneMatchAsync(string collectionName, string key, string kind, string toCollectionName, string toKey, object properties);
        Task<Result> PutGraphIfNoneMatchAsync(string collectionName, string key, string kind, string toCollectionName, string toKey, string properties);
        Task<Result> DeleteGraphAsync(string collectionName, string key, string kind, string toCollectionName, string toKey);

        #endregion

        #region Aggregates

        AggregateResult Aggregate(string collectionName, string query, string aggregate);
        Task<AggregateResult> AggregateAsync(string collectionName, string query, string aggregate);

        #endregion

        #region Bulk Operations

        BulkResult BulkOperation(string collectionName, string items, bool stream = false);
        BulkResult BulkOperation(string collectionName, object items, bool stream = false);
        Task<BulkResult> BulkOperationAsync(string collectionName, string items, bool stream = false);
        Task<BulkResult> BulkOperationAsync(string collectionName, object items, bool stream = false);

        #endregion
    }
}
