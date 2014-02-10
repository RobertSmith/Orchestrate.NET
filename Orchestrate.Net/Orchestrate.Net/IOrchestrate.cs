namespace Orchestrate.Net
{
    public interface IOrchestrate
    {
        Result CreateCollection(string collectionName, string key, object item);
        Result DeleteCollection(string collectionName);

        Result Get(string collectionName, string key);
        Result Put(string collectionName, string key, object item);
        Result PutIfMatch(string collectionName, string key, object item, string ifMatch);
        Result PutIfNoneMatch(string collectionName, string key, object item);

        SearchResult Search(string collectionName, string query, int limit, int offset);
    }
}
