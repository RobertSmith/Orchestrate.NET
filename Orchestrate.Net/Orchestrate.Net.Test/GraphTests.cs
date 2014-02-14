using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Orchestrate.Net.Test
{
    [TestClass]
    public class GraphTests
    {
        const string ApiKey = "<API KEY>";
        private const string CollectionName = "GraphTestCollection";
        private Orchestrate _orchestrate;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var orchestrate = new Orchestrate(ApiKey);
            var item = new TestData { Id = 1, Value = "Inital Test Item" };

            orchestrate.CreateCollection(CollectionName, "1", item);
        }

        [ClassCleanup]
        public static void ClassCleanUp()
        {
            var orchestrate = new Orchestrate(ApiKey);
            orchestrate.DeleteCollection(CollectionName);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _orchestrate = new Orchestrate(ApiKey);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // nothing to see here...
        }

        [TestMethod]
        public void AddGraph()
        {
            var data = new TestData { Id = 2, Value = "This is collection 2 data" };
            _orchestrate.CreateCollection("GraphTestCollection2", "2", data);

            var result = _orchestrate.PutGraph(CollectionName, "1", "toplevelgraph", "GraphTestCollection2", "2");

            Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);
        }

        [TestMethod]
        public void AddGraphWithNoCollectionName()
        {
            try
            {
                _orchestrate.PutGraph(string.Empty, "1", "toplevelgraph", "GraphTestCollection2", "2");
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "collectionName");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }


        [TestMethod]
        public void AddGraphWithNoKey()
        {
            try
            {
                _orchestrate.PutGraph(CollectionName, string.Empty, "toplevelgraph", "GraphTestCollection2", "2");
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "key");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }


        [TestMethod]
        public void AddGraphWithNoKind()
        {
            try
            {
                _orchestrate.PutGraph(CollectionName, "1", string.Empty, "GraphTestCollection2", "2");
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "kind");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }


        [TestMethod]
        public void AddGraphWithNoToCollectionName()
        {
            try
            {
                _orchestrate.PutGraph(CollectionName, "1", "toplevelgraph", string.Empty, "2");
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "toCollectionName");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }


        [TestMethod]
        public void AddGraphWithNoToKey()
        {
            try
            {
                _orchestrate.PutGraph(CollectionName, "1", "toplevelgraph", "GraphTestCollection2", string.Empty);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "toKey");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [TestMethod]
        public void GetSingleGraph()
        {
            var data = new TestData { Id = 2, Value = "This is collection 2 data" };
            _orchestrate.CreateCollection("GraphTestCollection2", "2", data);
            _orchestrate.PutGraph(CollectionName, "1", "toplevelgraph", "GraphTestCollection2", "2");
            var kinds = new[] { "toplevelgraph" };

            var result = _orchestrate.GetGraph(CollectionName, "1", kinds);

            Assert.IsTrue(result.Count == 1);
        }

        [TestMethod]
        public void GetMultipleLevleGraph()
        {
            var data = new TestData { Id = 2, Value = "This is collection 2 data" };
            _orchestrate.CreateCollection("GraphTestCollection2", "2", data);
            _orchestrate.PutGraph(CollectionName, "1", "toplevelgraph", "GraphTestCollection2", "2");

            data = new TestData { Id = 3, Value = "This is collection 3 data" };
            _orchestrate.CreateCollection("GraphTestCollection3", "3", data);
            _orchestrate.PutGraph("GraphTestCollection2", "2", "sublevelgraph", "GraphTestCollection3", "3");

            var kinds = new[] { "toplevelgraph", "sublevelgraph" };

            var result = _orchestrate.GetGraph(CollectionName, "1", kinds);

            Assert.IsTrue(result.Count == 1);
        }

        [TestMethod]
        public void GetSingleGraphWithNoCollectionName()
        {
            var data = new TestData { Id = 2, Value = "This is collection 2 data" };
            _orchestrate.CreateCollection("GraphTestCollection2", "2", data);
            _orchestrate.PutGraph(CollectionName, "1", "toplevelgraph", "GraphTestCollection2", "2");
            var kinds = new[] { "toplevelgraph" };

            try
            {
                _orchestrate.GetGraph(string.Empty, "1", kinds);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "collectionName");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [TestMethod]
        public void GetSingleGraphWithNoKey()
        {
            var data = new TestData { Id = 2, Value = "This is collection 2 data" };
            _orchestrate.CreateCollection("GraphTestCollection2", "2", data);
            _orchestrate.PutGraph(CollectionName, "1", "toplevelgraph", "GraphTestCollection2", "2");
            var kinds = new[] { "toplevelgraph" };

            try
            {
                _orchestrate.GetGraph(CollectionName, string.Empty, kinds);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "key");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [TestMethod]
        public void GetSingleGraphWithNoKinds()
        {
            var data = new TestData { Id = 2, Value = "This is collection 2 data" };
            _orchestrate.CreateCollection("GraphTestCollection2", "2", data);
            _orchestrate.PutGraph(CollectionName, "1", "toplevelgraph", "GraphTestCollection2", "2");

            try
            {
                _orchestrate.GetGraph(CollectionName, "1", null);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "kinds");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [TestMethod]
        public void DeleteGraph()
        {
            var data = new TestData { Id = 2, Value = "This is collection 2 data" };
            _orchestrate.CreateCollection("GraphTestCollection2", "2", data);
            _orchestrate.PutGraph(CollectionName, "1", "toplevelgraph", "GraphTestCollection2", "2");

            var result = _orchestrate.DeleteGraph(CollectionName, "1", "toplevelgraph", "GraphTestCollection2", "2");

            Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);
        }

        [TestMethod]
        public void DeleteGraphWithNoCollectionName()
        {
            try
            {
                _orchestrate.DeleteGraph(string.Empty, "1", "toplevelgraph", "GraphTestCollection2", "2");
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "collectionName");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [TestMethod]
        public void DeleteGraphWithNoKey()
        {
            try
            {
                _orchestrate.DeleteGraph(CollectionName, string.Empty, "toplevelgraph", "GraphTestCollection2", "2");
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "key");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }


        [TestMethod]
        public void DeleteGraphWithNoKind()
        {
            try
            {
                _orchestrate.DeleteGraph(CollectionName, "1", string.Empty, "GraphTestCollection2", "2");
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "kind");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }


        [TestMethod]
        public void DeleteGraphWithNoToCollectionName()
        {
            try
            {
                _orchestrate.DeleteGraph(CollectionName, "1", "toplevelgraph", string.Empty, "2");
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "toCollectionName");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }


        [TestMethod]
        public void DeleteGraphWithNoToKey()
        {
            try
            {
                _orchestrate.DeleteGraph(CollectionName, "1", "toplevelgraph", "GraphTestCollection2", string.Empty);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "toKey");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }
    }
}
