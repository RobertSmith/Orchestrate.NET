using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Orchestrate.Net.Test
{
    [TestClass]
    public class ConnectionTests
    {
        [TestMethod]
        public void CreateCollection()
        {
            const string apiKey = "<Put Your Api Key Here>";
            const string collectionName = "TestCollection";
            var orchestraton = new Orchestrate(apiKey);

            orchestraton.CreateCollection(collectionName, "1");

            Assert.AreEqual(1, 1);
        }

        [TestMethod]
        public void DeleteCollection()
        {
            const string apiKey = "<Put Your Api Key Here>";
            const string collectionName = "TestCollection";
            var orchestraton = new Orchestrate(apiKey);

            orchestraton.DeleteCollection(collectionName);

            Assert.AreEqual(1, 1);
        }

        [TestMethod]
        public void SearchCollection()
        {
            const string apiKey = "<Put Your Api Key Here>";
            const string collectionName = "PostalCodes";
            var orchestraton = new Orchestrate(apiKey);

            orchestraton.Search(collectionName, "*", 20, 0);

            Assert.AreEqual(1, 1);
        }
    }
}
