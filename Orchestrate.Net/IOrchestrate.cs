﻿using System;

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

        ListResult List(string collectionName, int limit, string startKey, string afterKey);

        SearchResult Search(string collectionName, string query, int limit, int offset);

        EventResultList GetEvents(string collectionName, string key, string type, DateTime? start, DateTime? end);
        Result PutEvent(string collectionName, string key, string type, DateTime? timeStamp, string msg);

        ListResult GetGraph(string collectionName, string key, string[] kinds);
        Result PutGraph(string collectionName, string key, string kind, string toCollectionName, string toKey);
        Result DeleteGraph(string collectionName, string key, string kind, string toCollectionName, string toKey);
    }
}
