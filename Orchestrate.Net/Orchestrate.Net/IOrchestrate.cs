using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchestrate.Net
{
    public interface IOrchestrate
    {
        void CreateCollection(string collectionName, string key);
        void DeleteCollection(string collectionName);

        void Search(string collectionName, string query, int limit, int offset);
    }
}
