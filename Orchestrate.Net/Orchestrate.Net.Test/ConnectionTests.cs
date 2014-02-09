using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Orchestrate.Net.Test
{
    [TestClass]
    public class ConnectionTests
    {
        [TestMethod]
        public void CreateCollection()
        {
            const string apiKey = "<API KEY>";
            const string collectionName = "TestCollection";
            var orchestraton = new Orchestrate(apiKey);

            var result = orchestraton.CreateCollection(collectionName, "1");

            Assert.IsTrue(result.Length == 0);
        }

        [TestMethod]
        public void DeleteCollection()
        {
            const string apiKey = "<API KEY>";
            const string collectionName = "TestCollection";
            var orchestraton = new Orchestrate(apiKey);

            var result = orchestraton.DeleteCollection(collectionName);

            Assert.IsTrue(result.Length == 0);
        }

        [TestMethod]
        public void CollectionGet()
        {
            const string apiKey = "<API KEY>";
            const string collectionName = "PostalCodes";
            var orchestraton = new Orchestrate(apiKey);

            var result = orchestraton.Get(collectionName, "1");

            Assert.IsTrue(result.Length > 0);
        }
        [TestMethod]
        public void SearchCollection()
        {
            const string apiKey = "<API KEY>";
            const string collectionName = "PostalCodes";
            var orchestraton = new Orchestrate(apiKey);

            var result = orchestraton.Search(collectionName, "*", 20, 0);

            Assert.IsTrue(result.Length > 0);
        }
    }
}
