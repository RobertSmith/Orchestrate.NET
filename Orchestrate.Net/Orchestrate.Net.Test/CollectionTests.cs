using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Orchestrate.Net.Test
{
    [TestClass]
    public class CollectionTests
    {
        const string ApiKey = "<API KEY>";

        [TestMethod]
        public void CreateCollectionWithItemAsObject()
        {
            // Set up
            const string collectionName = "TestCollection01";
            var orchestration = new Orchestrate(ApiKey);
            var item = new TestData {Id = 1, Value = "CreateCollectionWithItemAsObject"};

            try
            {
                var result = orchestration.CreateCollection(collectionName, Guid.NewGuid().ToString(), item);

                Assert.IsTrue(result.Path.Ref.Length > 0);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                orchestration.DeleteCollection(collectionName);
            }
        }

        [TestMethod]
        public void CreateCollectionWithItemAsJsonString()
        {
            // Set up
            const string collectionName = "TestCollection02";
            var orchestration = new Orchestrate(ApiKey);
            var item = new TestData { Id = 1, Value = "CreateCollectionWithItemAsJsonString" };
            var json = JsonConvert.SerializeObject(item);

            try
            {
                var result = orchestration.CreateCollection(collectionName, Guid.NewGuid().ToString(), json);

                Assert.IsTrue(result.Path.Ref.Length > 0);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                orchestration.DeleteCollection(collectionName);
            }
        }

        [TestMethod]
        public void DeleteCollection()
        {
            // Set up
            const string collectionName = "TestCollection03";
            var orchestration = new Orchestrate(ApiKey);
            var item = new TestData { Id = 1, Value = "DeleteCollection" };
            var json = JsonConvert.SerializeObject(item);

            try
            {
                orchestration.CreateCollection(collectionName, Guid.NewGuid().ToString(), json);
                var result = orchestration.DeleteCollection(collectionName);

                Assert.IsTrue(result.Path.Collection.Length > 0);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void CreateCollectionNoCollectionName()
        {
            // Set up
            const string collectionName = "";
            var orchestration = new Orchestrate(ApiKey);
            var item = new TestData { Id = 1, Value = "CreateCollectionNoCollectionName" };

            try
            {
                orchestration.CreateCollection(collectionName, Guid.NewGuid().ToString(), item);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "collectionName");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [TestMethod]
        public void CreateCollectionNoKey()
        {
            // Set up
            const string collectionName = "TestCollection04";
            var orchestration = new Orchestrate(ApiKey);
            var item = new TestData { Id = 1, Value = "CreateCollectionNoCollectionName" };

            try
            {
                orchestration.CreateCollection(collectionName, string.Empty, item);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "key");
                return;
            }

            orchestration.DeleteCollection(collectionName);
            Assert.Fail("No Exception Thrown");
        }

        [TestMethod]
        public void CreateCollectionNoItem()
        {
            // Set up
            const string collectionName = "TestCollection05";
            var orchestration = new Orchestrate(ApiKey);

            try
            {
                orchestration.CreateCollection(collectionName, Guid.NewGuid().ToString(), null);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "item");
                return;
            }

            orchestration.DeleteCollection(collectionName);
            Assert.Fail("No Exception Thrown");
        }
    }
}
