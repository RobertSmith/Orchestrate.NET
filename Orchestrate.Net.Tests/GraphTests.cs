using System;
using NUnit.Framework;

namespace Orchestrate.Net.Tests
{
	[TestFixture]
    public class GraphTests
    {
        const string ApiKey = "<API KEY>";
        private const string CollectionName = "GraphTestCollection";
        private Orchestrate _orchestrate;

		[TestFixtureSetUp]
        public static void ClassInitialize(TestContext context)
        {
            var orchestrate = new Orchestrate(ApiKey);
            var item = new TestData { Id = 1, Value = "Inital Test Item" };

            orchestrate.CreateCollection(CollectionName, "1", item);
        }

		[TestFixtureTearDown]
        public static void ClassCleanUp()
        {
            var orchestrate = new Orchestrate(ApiKey);
            orchestrate.DeleteCollection(CollectionName);
            orchestrate.DeleteCollection("GraphTestCollection2");
            orchestrate.DeleteCollection("GraphTestCollection3");
        }

		[SetUp]
        public void TestInitialize()
        {
            _orchestrate = new Orchestrate(ApiKey);
        }

		[TearDown]
        public void TestCleanup()
        {
        }

		[Test]
        public void AddGraph()
        {
            var data = new TestData { Id = 2, Value = "This is collection 2 data" };
            _orchestrate.CreateCollection("GraphTestCollection2", "2", data);

            var result = _orchestrate.PutGraph(CollectionName, "1", "toplevelgraph", "GraphTestCollection2", "2");

            Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);
        }

		[Test]
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


		[Test]
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


		[Test]
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


		[Test]
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


		[Test]
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

		[Test]
        public void GetSingleGraph()
        {
            var data = new TestData { Id = 2, Value = "This is collection 2 data" };
            _orchestrate.CreateCollection("GraphTestCollection2", "2", data);
            _orchestrate.PutGraph(CollectionName, "1", "toplevelgraph", "GraphTestCollection2", "2");
            var kinds = new[] { "toplevelgraph" };

            var result = _orchestrate.GetGraph(CollectionName, "1", kinds);

            Assert.IsTrue(result.Count == 1);
        }

		[Test]
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

		[Test]
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

		[Test]
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

		[Test]
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

		[Test]
        public void DeleteGraph()
        {
            var data = new TestData { Id = 2, Value = "This is collection 2 data" };
            _orchestrate.CreateCollection("GraphTestCollection2", "2", data);
            _orchestrate.PutGraph(CollectionName, "1", "toplevelgraph", "GraphTestCollection2", "2");
            _orchestrate.DeleteGraph(CollectionName, "1", "toplevelgraph", "GraphTestCollection2", "2");
            var kinds = new[] { "toplevelgraph" };

            try
            {
                _orchestrate.GetGraph(CollectionName, "1", kinds);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("404"));
            }
        }

		[Test]
        public void DeleteGraphPurge()
        {
            var data = new TestData { Id = 2, Value = "This is collection 2 data" };
            _orchestrate.CreateCollection("GraphTestCollection2", "2", data);
            _orchestrate.PutGraph(CollectionName, "1", "toplevelgraph", "GraphTestCollection2", "2");

            var result = _orchestrate.DeleteGraph(CollectionName, "1", "toplevelgraph", "GraphTestCollection2", "2");

            Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);
        }

		[Test]
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

		[Test]
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


		[Test]
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


		[Test]
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


		[Test]
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
