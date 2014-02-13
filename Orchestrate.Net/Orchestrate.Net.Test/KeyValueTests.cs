using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Orchestrate.Net.Test
{
    [TestClass]
    public class KeyValueTests
    {
        const string ApiKey = "<API KEY>";
        private const string CollectionName = "KeyValueTestCollection";
        private Orchestrate _orchestrate;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var orchestrate = new Orchestrate(ApiKey);

            var item = new TestData { Id = 1, Value = "Inital Test Item" };
            var item2 = new TestData { Id = 2, Value = "Inital Test Item #2" };
            var item3 = new TestData { Id = 3, Value = "Inital Test Item #3" };

            orchestrate.CreateCollection(CollectionName, "1", item);
            orchestrate.Put(CollectionName, "2", item2);
            orchestrate.Put(CollectionName, "3", item3);

            context.Properties.Add("Orchestrate", orchestrate);
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

        #region Get Tests

        [TestMethod]
        public void GetByKey()
        {
            var result = _orchestrate.Get(CollectionName, "1");

            Assert.IsTrue(result.Value != null);
        }

        [TestMethod]
        public void GetByNonExistantKey()
        {
            try
            {
                _orchestrate.Get(CollectionName, "9999");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("404"));
            }
        }

        [TestMethod]
        public void GetWithNoCollectionName()
        {
            try
            {
                _orchestrate.Get(string.Empty, "9999");
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "collectionName");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [TestMethod]
        public void GetWithNoKey()
        {
            try
            {
                _orchestrate.Get(CollectionName, string.Empty);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "key");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        #endregion

        #region Put Tests

        [TestMethod]
        public void PutAsObject()
        {
            var item = new TestData {Id = 3, Value = "A successful object PUT"};
            var result = _orchestrate.Put(CollectionName, Guid.NewGuid().ToString(), item);

            Assert.IsTrue(result.Path.Ref.Length > 0);
        }

        [TestMethod]
        public void PutAsString()
        {
            var item = new TestData { Id = 4, Value = "A successful string PUT" };
            var json = JsonConvert.SerializeObject(item);
            var result = _orchestrate.Put(CollectionName, Guid.NewGuid().ToString(), json);

            Assert.IsTrue(result.Path.Ref.Length > 0);
        }

        [TestMethod]
        public void PutWithNoCollectionName()
        {
            var item = new TestData { Id = 5, Value = "An  unsuccessful string PUT" };
            var json = JsonConvert.SerializeObject(item);

            try
            {
                _orchestrate.Put(string.Empty, Guid.NewGuid().ToString(), json);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "collectionName");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [TestMethod]
        public void PutWithNoKey()
        {
            var item = new TestData { Id = 6, Value = "An  unsuccessful string PUT" };
            var json = JsonConvert.SerializeObject(item);

            try
            {
                _orchestrate.Put(CollectionName, string.Empty, json);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "key");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [TestMethod]
        public void PutWithNoItem()
        {
            try
            {
                _orchestrate.Put(CollectionName, Guid.NewGuid().ToString(), string.Empty);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "item");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [TestMethod]
        public void CollectionPutIfMatchSuccess()
        {
            var match = _orchestrate.Get(CollectionName, "1");
            var item = new TestData { Id = 1, Value = "New and improved value!" };

            var result = _orchestrate.PutIfMatch(CollectionName, "1", item, match.Path.Ref);

            Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);
        }

        [TestMethod]
        public void CollectionPutIfMatchFail()
        {
            var match = _orchestrate.Get(CollectionName, "2");
            var item = new TestData { Id = 2, Value = "Value, now with more moxie!" };

            try
            {
                _orchestrate.PutIfMatch(CollectionName, "1", item, match.Path.Ref);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("412"));
            }
        }

        #endregion
    }
}
