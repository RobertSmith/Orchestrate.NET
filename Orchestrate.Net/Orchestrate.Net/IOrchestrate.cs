namespace Orchestrate.Net
{
    public interface IOrchestrate
    {
        string CreateCollection(string collectionName, string key);
        string DeleteCollection(string collectionName);

        string Get(string collectionName, string key);
        string Search(string collectionName, string query, int limit, int offset);
    }
}
