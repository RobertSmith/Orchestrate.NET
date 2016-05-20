using System;
using System.Linq;
using Newtonsoft.Json;
using NUnit.Framework;
using Orchestrate.Net.Tests.Helpers;

// ReSharper disable UnusedVariable

namespace Orchestrate.Net.Tests
{
    [TestFixture]
    public class CollectionTests
    {
        [Test]
        public void CreateCollectionWithItemAsObject()
        {
            // Set up
            const string collectionName = "TestCollection01";
            var orchestration = new Orchestrate(TestHelper.ApiKey);
            var item = new TestData {Id = 1, Value = "CreateCollectionWithItemAsObject"};

            try
            {
                var result = orchestration.Put(collectionName, Guid.NewGuid().ToString(), item);

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

        [Test]
        public void CreateCollectionWithItemAsObjectAsync()
        {
            // Set up
            const string collectionName = "TestCollection01";
            var orchestration = new Orchestrate(TestHelper.ApiKey);
            var item = new TestData {Id = 1, Value = "CreateCollectionWithItemAsObject"};

            try
            {
                var result = orchestration.PutAsync(collectionName, Guid.NewGuid().ToString(), item).Result;

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

        [Test]
        public void CreateCollectionWithItemAsJsonString()
        {
            // Set up
            const string collectionName = "TestCollection02";
            var orchestration = new Orchestrate(TestHelper.ApiKey);
            var item = new TestData {Id = 1, Value = "CreateCollectionWithItemAsJsonString"};
            var json = JsonConvert.SerializeObject(item);

            try
            {
                var result = orchestration.Put(collectionName, Guid.NewGuid().ToString(), json);

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

        [Test]
        public void CreateCollectionWithItemAsJsonStringAsync()
        {
            // Set up
            const string collectionName = "TestCollection02";
            var orchestration = new Orchestrate(TestHelper.ApiKey);
            var item = new TestData {Id = 1, Value = "CreateCollectionWithItemAsJsonString"};
            var json = JsonConvert.SerializeObject(item);

            try
            {
                var result = orchestration.PutAsync(collectionName, Guid.NewGuid().ToString(), json).Result;

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

        [Test]
        public void CreateCollectionNoCollectionName()
        {
            // Set up
            const string collectionName = "";
            var orchestration = new Orchestrate(TestHelper.ApiKey);
            var item = new TestData {Id = 1, Value = "CreateCollectionNoCollectionName"};

            try
            {
                orchestration.Put(collectionName, Guid.NewGuid().ToString(), item);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "collectionName");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void CreateCollectionNoCollectionNameAsync()
        {
            // Set up
            const string collectionName = "";
            var orchestration = new Orchestrate(TestHelper.ApiKey);
            var item = new TestData {Id = 1, Value = "CreateCollectionNoCollectionName"};

            try
            {
                var result = orchestration.PutAsync(collectionName, Guid.NewGuid().ToString(), item).Result;
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerExceptions.First() as ArgumentNullException;
                Assert.IsTrue(inner?.ParamName == "collectionName");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void CreateCollectionNoKey()
        {
            // Set up
            const string collectionName = "TestCollection04";
            var orchestration = new Orchestrate(TestHelper.ApiKey);
            var item = new TestData {Id = 1, Value = "CreateCollectionNoCollectionName"};

            try
            {
                orchestration.Put(collectionName, string.Empty, item);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "key");
                return;
            }

            orchestration.DeleteCollection(collectionName);
            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void CreateCollectionNoKeyAsync()
        {
            // Set up
            const string collectionName = "TestCollection04";
            var orchestration = new Orchestrate(TestHelper.ApiKey);
            var item = new TestData {Id = 1, Value = "CreateCollectionNoCollectionName"};

            try
            {
                var result = orchestration.PutAsync(collectionName, string.Empty, item).Result;
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerExceptions.First() as ArgumentNullException;
                Assert.IsTrue(inner?.ParamName == "key");
                return;
            }

            orchestration.DeleteCollection(collectionName);
            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void CreateCollectionNoItem()
        {
            // Set up
            const string collectionName = "TestCollection05";
            var orchestration = new Orchestrate(TestHelper.ApiKey);

            try
            {
                orchestration.Put(collectionName, Guid.NewGuid().ToString(), (object) null);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "item");
                return;
            }

            orchestration.DeleteCollection(collectionName);
            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void CreateCollectionNoItemAsync()
        {
            // Set up
            const string collectionName = "TestCollection05";
            var orchestration = new Orchestrate(TestHelper.ApiKey);

            try
            {
                var result = orchestration.PutAsync(collectionName, Guid.NewGuid().ToString(), (object) null).Result;
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerExceptions.First() as ArgumentNullException;
                Assert.IsTrue(inner?.ParamName == "item");
                return;
            }

            orchestration.DeleteCollection(collectionName);
            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void DeleteCollection()
        {
            // Set up
            const string collectionName = "TestCollection03";
            var orchestration = new Orchestrate(TestHelper.ApiKey);
            var item = new TestData {Id = 1, Value = "DeleteCollection"};
            var json = JsonConvert.SerializeObject(item);

            try
            {
                orchestration.Put(collectionName, Guid.NewGuid().ToString(), json);
                var result = orchestration.DeleteCollection(collectionName);

                Assert.IsTrue(result.Path.Collection.Length > 0);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void DeleteCollectionAsync()
        {
            // Set up
            const string collectionName = "TestCollection03";
            var orchestration = new Orchestrate(TestHelper.ApiKey);
            var item = new TestData {Id = 1, Value = "DeleteCollection"};
            var json = JsonConvert.SerializeObject(item);

            try
            {
                orchestration.Put(collectionName, Guid.NewGuid().ToString(), json);
                var result = orchestration.DeleteCollectionAsync(collectionName).Result;

                Assert.IsTrue(result.Path.Collection.Length > 0);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void DeleteNonExistantCollection()
        {
            // Set up
            var orchestration = new Orchestrate(TestHelper.ApiKey);

            try
            {
                var result = orchestration.DeleteCollection("NonExistantCollection");

                Assert.IsTrue(result.Path.Collection.Length > 0);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void DeleteNonExistantCollectionAsync()
        {
            // Set up
            var orchestration = new Orchestrate(TestHelper.ApiKey);

            try
            {
                var result = orchestration.DeleteCollectionAsync("NonExistantCollection").Result;

                Assert.IsTrue(result.Path.Collection.Length > 0);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void DeleteCollectionNoName()
        {
            // Set up
            const string collectionName = "TestCollection04";
            var orchestration = new Orchestrate(TestHelper.ApiKey);
            var item = new TestData {Id = 1, Value = "DeleteCollection"};
            var json = JsonConvert.SerializeObject(item);

            try
            {
                orchestration.Put(collectionName, Guid.NewGuid().ToString(), json);
                orchestration.DeleteCollection(string.Empty);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "collectionName");
                orchestration.DeleteCollection(collectionName);
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void DeleteCollectionNoNameAsync()
        {
            // Set up
            const string collectionName = "TestCollection04";
            var orchestration = new Orchestrate(TestHelper.ApiKey);
            var item = new TestData {Id = 1, Value = "DeleteCollection"};
            var json = JsonConvert.SerializeObject(item);

            try
            {
                var result = orchestration.PutAsync(collectionName, Guid.NewGuid().ToString(), json).Result;
                var deleteResult = orchestration.DeleteCollectionAsync(string.Empty).Result;
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerExceptions.First() as ArgumentNullException;
                Assert.IsTrue(inner?.ParamName == "collectionName");
                orchestration.DeleteCollection(collectionName);
                return;
            }

            Assert.Fail("No Exception Thrown");
        }
    }
}