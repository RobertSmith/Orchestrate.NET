using System;
using System.Linq;
using NUnit.Framework;
using Newtonsoft.Json;
using Orchestrate.Net.Models;
using Orchestrate.Net.Tests.Helpers;

// ReSharper disable UnusedVariable

namespace Orchestrate.Net.Tests
{
    [TestFixture]
    public class KeyValueTests
    {
        private const string CollectionName = "KeyValueTestCollection";
        private Orchestrate _orchestrate;

        [TestFixtureSetUp]
        public static void ClassInitialize()
        {
            var orchestrate = new Orchestrate(TestHelper.ApiKey);
            var item = new TestData { Id = 1, Value = "Inital Test Item" };

            orchestrate.CreateCollection(CollectionName, "1", item);
        }

        [TestFixtureTearDown]
        public static void ClassCleanUp()
        {
            var orchestrate = new Orchestrate(TestHelper.ApiKey);
            orchestrate.DeleteCollection(CollectionName);
        }

        [SetUp]
        public void TestInitialize()
        {
            _orchestrate = new Orchestrate(TestHelper.ApiKey);
        }

        [TearDown]
        public void TestCleanup()
        {
            // nothing to see here...
        }

        #region Get Tests

        [Test]
        public void GetByKey()
        {
            var result = _orchestrate.Get(CollectionName, "1");

            Assert.IsTrue(result.Value != null);
        }

        [Test]
        public void GetByKeyAsync()
        {
            var result = _orchestrate.GetAsync(CollectionName, "1").Result;

            Assert.IsTrue(result.Value != null);
        }

        [Test]
        public void GetByNonExistantKey()
        {
            try
            {
                var result = _orchestrate.Get(CollectionName, "9999");

                Assert.Fail("No Exception Thrown");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.ToString().Contains("404"));
            }
        }

        [Test]
        public void GetByNonExistantKeyAsync()
        {
            try
            {
                var result = _orchestrate.GetAsync(CollectionName, "9999").Result;

                Assert.Fail("No Exception Thrown");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.ToString().Contains("404"));
            }
        }

        [Test]
        public void GetWithNoCollectionName()
        {
            try
            {
                _orchestrate.Get(string.Empty, "9999");

                Assert.Fail("No Exception Thrown");
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "collectionName");
            }
        }

        [Test]
        public void GetWithNoCollectionNameAsync()
        {
            try
            {
                var result = _orchestrate.GetAsync(string.Empty, "9999").Result;

                Assert.Fail("No Exception Thrown");
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerExceptions.First() as ArgumentNullException;
                Assert.IsTrue(inner?.ParamName == "collectionName");
            }
        }

        [Test]
        public void GetWithNoKey()
        {
            try
            {
                _orchestrate.Get(CollectionName, string.Empty);

                Assert.Fail("No Exception Thrown");
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "key");
            }
        }

        [Test]
        public void GetWithNoKeyAsync()
        {
            try
            {
                var result = _orchestrate.GetAsync(CollectionName, string.Empty).Result;

                Assert.Fail("No Exception Thrown");
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerExceptions.First() as ArgumentNullException;
                Assert.IsTrue(inner?.ParamName == "key");
            }
        }

        #endregion

        #region Post Tests

        [Test]
        public void PostAsObject()
        {
            var item = new TestData { Id = 3, Value = "A successful object POST" };
            var result = _orchestrate.Post(CollectionName, item);

            Assert.IsTrue(result.Path.Ref.Length > 0);
        }

        [Test]
        public void PostAsObjectAsync()
        {
            var item = new TestData { Id = 3, Value = "A successful object POST" };
            var result = _orchestrate.PostAsync(CollectionName, item).Result;

            Assert.IsTrue(result.Path.Ref.Length > 0);
        }

        [Test]
        public void PostAsString()
        {
            var item = new TestData { Id = 4, Value = "A successful string POST" };
            var json = JsonConvert.SerializeObject(item);
            var result = _orchestrate.Post(CollectionName, json);

            Assert.IsTrue(result.Path.Ref.Length > 0);
        }

        [Test]
        public void PostAsStringAsync()
        {
            var item = new TestData { Id = 4, Value = "A successful string POST" };
            var json = JsonConvert.SerializeObject(item);
            var result = _orchestrate.PostAsync(CollectionName, json).Result;

            Assert.IsTrue(result.Path.Ref.Length > 0);
        }

        [Test]
        public void PostWithNoCollectionName()
        {
            var item = new TestData { Id = 5, Value = "An unsuccessful string POST" };
            var json = JsonConvert.SerializeObject(item);

            try
            {
                _orchestrate.Post(string.Empty, json);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "collectionName");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void PostWithNoCollectionNameAsync()
        {
            var item = new TestData { Id = 5, Value = "An unsuccessful string POST" };
            var json = JsonConvert.SerializeObject(item);

            try
            {
                var result = _orchestrate.PostAsync(string.Empty, json).Result;
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
        public void PostWithNoItem()
        {
            try
            {
                _orchestrate.Post(CollectionName, string.Empty);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "item");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void PostWithNoItemAsync()
        {
            try
            {
                var result = _orchestrate.PostAsync(CollectionName, string.Empty).Result;
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerExceptions.First() as ArgumentNullException;
                Assert.IsTrue(inner?.ParamName == "item");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        #endregion

        #region Put Tests

        [Test]
        public void PutAsObject()
        {
            var item = new TestData {Id = 3, Value = "A successful object PUT"};
            var result = _orchestrate.Put(CollectionName, Guid.NewGuid().ToString(), item);

            Assert.IsTrue(result.Path.Ref.Length > 0);
        }

        [Test]
        public void PutAsObjectAsync()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            var result = _orchestrate.PutAsync(CollectionName, Guid.NewGuid().ToString(), item).Result;

            Assert.IsTrue(result.Path.Ref.Length > 0);
        }

        [Test]
        public void PutAsString()
        {
            var item = new TestData {Id = 4, Value = "A successful string PUT"};
            var json = JsonConvert.SerializeObject(item);
            var result = _orchestrate.Put(CollectionName, Guid.NewGuid().ToString(), json);

            Assert.IsTrue(result.Path.Ref.Length > 0);
        }

        [Test]
        public void PutAsStringAsync()
        {
            var item = new TestData { Id = 4, Value = "A successful string PUT" };
            var json = JsonConvert.SerializeObject(item);
            var result = _orchestrate.PutAsync(CollectionName, Guid.NewGuid().ToString(), json).Result;

            Assert.IsTrue(result.Path.Ref.Length > 0);
        }

        [Test]
        public void PutWithNoCollectionName()
        {
            var item = new TestData {Id = 5, Value = "An unsuccessful string PUT"};
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

        [Test]
        public void PutWithNoCollectionNameAsync()
        {
            var item = new TestData { Id = 5, Value = "An unsuccessful string PUT" };
            var json = JsonConvert.SerializeObject(item);

            try
            {
                var result = _orchestrate.PutAsync(string.Empty, Guid.NewGuid().ToString(), json).Result;
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
        public void PutWithNoKey()
        {
            var item = new TestData {Id = 6, Value = "An unsuccessful string PUT"};
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

        [Test]
        public void PutWithNoKeyAsync()
        {
            var item = new TestData { Id = 6, Value = "An unsuccessful string PUT" };
            var json = JsonConvert.SerializeObject(item);

            try
            {
                var result = _orchestrate.PutAsync(CollectionName, string.Empty, json).Result;
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerExceptions.First() as ArgumentNullException;
                Assert.IsTrue(inner?.ParamName == "key");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
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

        [Test]
        public void PutWithNoItemAsync()
        {
            try
            {
                var result = _orchestrate.PutAsync(CollectionName, Guid.NewGuid().ToString(), string.Empty).Result;
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerExceptions.First() as ArgumentNullException;
                Assert.IsTrue(inner?.ParamName == "item");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void PutIfMatchSuccess()
        {
            var match = _orchestrate.Get(CollectionName, "1");
            var item = new TestData {Id = 1, Value = "New and improved value!"};

            var result = _orchestrate.PutIfMatch(CollectionName, "1", item, match.Path.Ref);

            Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);
        }

        [Test]
        public void PutIfMatchAsyncSuccess()
        {
            var match = _orchestrate.Get(CollectionName, "1");
            var item = new TestData { Id = 1, Value = "New and improved value!" };

            var result = _orchestrate.PutIfMatchAsync(CollectionName, "1", item, match.Path.Ref).Result;

            Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);
        }

        [Test]
        public void PutIfMatchFail()
        {
            var match = _orchestrate.Get(CollectionName, "1");
            var item = new TestData {Id = 1, Value = "Value, now with more moxie!"};

            try
            {
                _orchestrate.PutIfMatch(CollectionName, "2", item, match.Path.Ref);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("412"));
            }
        }

        [Test]
        public void PutIfMatchFailAsync()
        {
            var match = _orchestrate.Get(CollectionName, "1");
            var item = new TestData { Id = 1, Value = "Value, now with more moxie!" };

            try
            {
                var result = _orchestrate.PutIfMatchAsync(CollectionName, "2", item, match.Path.Ref).Result;
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("412"));
            }
        }

        [Test]
        public void PutIfMatchWithNoCollectionName()
        {
            var match = _orchestrate.Get(CollectionName, "1");
            var item = new TestData {Id = 1, Value = "Value, now with more moxie!"};

            try
            {
                _orchestrate.PutIfMatch(string.Empty, "2", item, match.Path.Ref);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "collectionName");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void PutIfMatchWithNoCollectionNameAsync()
        {
            var match = _orchestrate.Get(CollectionName, "1");
            var item = new TestData { Id = 1, Value = "Value, now with more moxie!" };

            try
            {
                var result = _orchestrate.PutIfMatchAsync(string.Empty, "2", item, match.Path.Ref).Result;
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
        public void PutIfMatchWithNoKey()
        {
            var match = _orchestrate.Get(CollectionName, "1");
            var item = new TestData {Id = 1, Value = "Value, now with more moxie!"};

            try
            {
                _orchestrate.PutIfMatch(CollectionName, string.Empty, item, match.Path.Ref);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "key");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void PutIfMatchWithNoKeyAsync()
        {
            var match = _orchestrate.Get(CollectionName, "1");
            var item = new TestData { Id = 1, Value = "Value, now with more moxie!" };

            try
            {
                var result = _orchestrate.PutIfMatchAsync(CollectionName, string.Empty, item, match.Path.Ref).Result;
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerExceptions.First() as ArgumentNullException;
                Assert.IsTrue(inner?.ParamName == "key");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void PutIfMatchWithNoItem()
        {
            var match = _orchestrate.Get(CollectionName, "1");

            try
            {
                _orchestrate.PutIfMatch(CollectionName, "2", null, match.Path.Ref);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "item");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void PutIfMatchWithNoItemAsync()
        {
            var match = _orchestrate.Get(CollectionName, "1");

            try
            {
                var result = _orchestrate.PutIfMatchAsync(CollectionName, "2", null, match.Path.Ref).Result;
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerExceptions.First() as ArgumentNullException;
                Assert.IsTrue(inner?.ParamName == "item");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void PutIfMatchWithNoIfMatch()
        {
            var item = new TestData {Id = 1, Value = "Value, now with more moxie!"};

            try
            {
                _orchestrate.PutIfMatch(CollectionName, "2", item, string.Empty);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "ifMatch");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void PutIfMatchWithNoIfMatchAsync()
        {
            var item = new TestData { Id = 1, Value = "Value, now with more moxie!" };

            try
            {
                var result = _orchestrate.PutIfMatchAsync(CollectionName, "2", item, string.Empty).Result;
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerExceptions.First() as ArgumentNullException;
                Assert.IsTrue(inner?.ParamName == "ifMatch");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void PutIfNoneMatchSucess()
        {
            var item = new TestData {Id = 88, Value = "Test Value 88"};

            var result = _orchestrate.PutIfNoneMatch(CollectionName, "88", item);

            Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);
        }

        [Test]
        public void PutIfNoneMatchSucessAsync()
        {
            var item = new TestData { Id = 88, Value = "Test Value 88" };

            var result = _orchestrate.PutIfNoneMatchAsync(CollectionName, "88", item).Result;

            Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);
        }

        [Test]
        public void PutIfNoneMatchFail()
        {
            var item = new TestData {Id = 1, Value = "Test Value 1"};

            try
            {
                _orchestrate.PutIfNoneMatch(CollectionName, "1", item);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("412"));
            }
        }

        [Test]
        public void PutIfNoneMatchFailAsync()
        {
            var item = new TestData { Id = 1, Value = "Test Value 1" };

            try
            {
                var result = _orchestrate.PutIfNoneMatchAsync(CollectionName, "1", item).Result;
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("412"));
            }
        }

        [Test]
        public void PutIfNoneMatchWithNoCollectionName()
        {
            var item = new TestData {Id = 77, Value = "Test Value 77"};

            try
            {
                _orchestrate.PutIfNoneMatch(string.Empty, "77", item);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "collectionName");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void PutIfNoneMatchWithNoCollectionNameAsync()
        {
            var item = new TestData { Id = 77, Value = "Test Value 77" };

            try
            {
                var result = _orchestrate.PutIfNoneMatchAsync(string.Empty, "77", item).Result;
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
        public void PutIfNoneMatchWithNoKey()
        {
            var item = new TestData {Id = 77, Value = "Test Value 77"};

            try
            {
                _orchestrate.PutIfNoneMatch(CollectionName, string.Empty, item);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "key");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void PutIfNoneMatchWithNoKeyAsync()
        {
            var item = new TestData { Id = 77, Value = "Test Value 77" };

            try
            {
                var result = _orchestrate.PutIfNoneMatchAsync(CollectionName, string.Empty, item).Result;
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerExceptions.First() as ArgumentNullException;
                Assert.IsTrue(inner?.ParamName == "key");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void PutIfNoneMatchWithNoItem()
        {
            try
            {
                _orchestrate.PutIfNoneMatch(CollectionName, "77", null);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "item");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void PutIfNoneMatchWithNoItemAsync()
        {
            try
            {
                var result = _orchestrate.PutIfNoneMatchAsync(CollectionName, "77", null).Result;
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerExceptions.First() as ArgumentNullException;
                Assert.IsTrue(inner?.ParamName == "item");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        #endregion

        #region Patch/Merge Tests

        [Test]
        public void PatchAsObject()
        {
            var item = new PatchTestData { Id = 3, FirstName = "John", Phone = "404-555-1212", NickName = "Big John", WorkPhone = "404-555-8989", LogIns = 1 };
            var key = Guid.NewGuid();
            _orchestrate.Put(CollectionName, key.ToString(), item);

            var patchItems = new object[]
            {
                new PatchItemString {Op = "add", Path = "/LastName", Value = "Doe"},
                new PatchItemString {Op = "remove", Path = "/Phone"},
                new PatchItemString {Op = "replace", Path = "/FirstName", Value = "Jane"},
                new PatchItemString {Op = "move", From = "/WorkPhone", Path = "/Phone"},
                new PatchItemString {Op = "copy", From = "/NickName", Path = "/LastName"},
                new PatchItemInt {Op = "test", Path = "/LogIns", Value = 1},
                new PatchItemInt {Op = "inc", Path = "/LogIns", Value = -10}
            };

            var result = _orchestrate.Patch(CollectionName, key.ToString(), patchItems);

            Assert.IsTrue(result.Path.Ref.Length > 0);
        }

        [Test]
        public void MergeAsObject()
        {
            var match = _orchestrate.Get(CollectionName, "1");
            var item = new MergeTestData { NewValue = "This is a merged value" };

            var result = _orchestrate.Patch(CollectionName, match.Path.Key, item);

            Assert.IsTrue(result.Path.Ref.Length > 0);
        }

        [Test]
        public void PatchAsObjectAsync()
        {
            var item = new PatchTestData { Id = 3, FirstName = "John", Phone = "404-555-1212", NickName = "Big John", WorkPhone = "404-555-8989", LogIns = 1 };
            var key = Guid.NewGuid();
            _orchestrate.Put(CollectionName, key.ToString(), item);

            var patchItems = new object[7];

            patchItems[0] = new PatchItemString { Op = "add", Path = "/LastName", Value = "Doe" };
            patchItems[1] = new PatchItemString { Op = "remove", Path = "/Phone" };
            patchItems[2] = new PatchItemString { Op = "replace", Path = "/FirstName", Value = "Jane" };
            patchItems[3] = new PatchItemString { Op = "move", From = "/WorkPhone", Path = "/Phone" };
            patchItems[4] = new PatchItemString { Op = "copy", From = "/NickName", Path = "/LastName" };
            patchItems[5] = new PatchItemInt { Op = "test", Path = "/LogIns", Value = 1 };
            patchItems[6] = new PatchItemInt { Op = "inc", Path = "/LogIns", Value = -10 };

            var result = _orchestrate.PatchAsync(CollectionName, key.ToString(), patchItems).Result;

            Assert.IsTrue(result.Path.Ref.Length > 0);
        }

        [Test]
        public void MergeAsObjectAsync()
        {
            var match = _orchestrate.Get(CollectionName, "1");
            var item = new MergeTestData { NewValue = "This is a merged value" };

            var result = _orchestrate.PatchAsync(CollectionName, match.Path.Key, item).Result;

            Assert.IsTrue(result.Path.Ref.Length > 0);
        }

        [Test]
        public void PatchAsString()
        {
            var item = new PatchTestData { Id = 3, FirstName = "John", Phone = "404-555-1212", NickName = "Big John", WorkPhone = "404-555-8989", LogIns = 1 };
            var key = Guid.NewGuid();
            _orchestrate.Put(CollectionName, key.ToString(), item);

            var patchItems = new object[7];

            patchItems[0] = new PatchItemString { Op = "add", Path = "/LastName", Value = "Doe" };
            patchItems[1] = new PatchItemString { Op = "remove", Path = "/Phone" };
            patchItems[2] = new PatchItemString { Op = "replace", Path = "/FirstName", Value = "Jane" };
            patchItems[3] = new PatchItemString { Op = "move", From = "/WorkPhone", Path = "/Phone" };
            patchItems[4] = new PatchItemString { Op = "copy", From = "/NickName", Path = "/LastName" };
            patchItems[5] = new PatchItemInt { Op = "test", Path = "/LogIns", Value = 1 };
            patchItems[6] = new PatchItemInt { Op = "inc", Path = "/LogIns", Value = -10 };

            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            var json = JsonConvert.SerializeObject(item, settings);

            var result = _orchestrate.Patch(CollectionName, key.ToString(), json);

            Assert.IsTrue(result.Path.Ref.Length > 0);
        }

        [Test]
        public void MergeAsString()
        {
            var match = _orchestrate.Get(CollectionName, "1");
            var item = new MergeTestData { NewValue = "This is a merged value" };

            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            var json = JsonConvert.SerializeObject(item, settings);

            var result = _orchestrate.Patch(CollectionName, match.Path.Key, json);

            Assert.IsTrue(result.Path.Ref.Length > 0);
        }

        [Test]
        public void PatchAsStringAsync()
        {
            var item = new PatchTestData { Id = 3, FirstName = "John", Phone = "404-555-1212", NickName = "Big John", WorkPhone = "404-555-8989", LogIns = 1 };
            var key = Guid.NewGuid();
            _orchestrate.Put(CollectionName, key.ToString(), item);

            var patchItems = new object[7];

            patchItems[0] = new PatchItemString { Op = "add", Path = "/LastName", Value = "Doe" };
            patchItems[1] = new PatchItemString { Op = "remove", Path = "/Phone" };
            patchItems[2] = new PatchItemString { Op = "replace", Path = "/FirstName", Value = "Jane" };
            patchItems[3] = new PatchItemString { Op = "move", From = "/WorkPhone", Path = "/Phone" };
            patchItems[4] = new PatchItemString { Op = "copy", From = "/NickName", Path = "/LastName" };
            patchItems[5] = new PatchItemInt { Op = "test", Path = "/LogIns", Value = 1 };
            patchItems[6] = new PatchItemInt { Op = "inc", Path = "/LogIns", Value = -10 };

            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            var json = JsonConvert.SerializeObject(item, settings);

            var result = _orchestrate.PatchAsync(CollectionName, key.ToString(), json).Result;

            Assert.IsTrue(result.Path.Ref.Length > 0);
        }

        [Test]
        public void MergeAsStringAsync()
        {
            var match = _orchestrate.Get(CollectionName, "1");
            var item = new MergeTestData { NewValue = "This is a merged value" };

            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            var json = JsonConvert.SerializeObject(item, settings);

            var result = _orchestrate.PatchAsync(CollectionName, match.Path.Key, json).Result;

            Assert.IsTrue(result.Path.Ref.Length > 0);
        }

        [Test]
        public void PatchWithNoCollectionName()
        {
            var patchItem = new PatchItemString { Op = "add", Path = "/LastName", Value = "Doe" };

            try
            {
                _orchestrate.Patch(string.Empty, Guid.NewGuid().ToString(), patchItem);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "collectionName");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void PatchWithNoCollectionNameAsync()
        {
            var patchItem = new PatchItemString { Op = "add", Path = "/LastName", Value = "Doe" };

            try
            {
                var result = _orchestrate.PatchAsync(string.Empty, Guid.NewGuid().ToString(), patchItem).Result;
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
        public void PatchWithNoKey()
        {
            var patchItem = new PatchItemString { Op = "add", Path = "/LastName", Value = "Doe" };

            try
            {
                _orchestrate.Patch(CollectionName, string.Empty, patchItem);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "key");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void PatchWithNoKeyAsync()
        {
            var patchItem = new PatchItemString { Op = "add", Path = "/LastName", Value = "Doe" };

            try
            {
                var result = _orchestrate.PatchAsync(CollectionName, string.Empty, patchItem).Result;
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerExceptions.First() as ArgumentNullException;
                Assert.IsTrue(inner?.ParamName == "key");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void PatchWithNoItem()
        {
            try
            {
                _orchestrate.Patch(CollectionName, Guid.NewGuid().ToString(), string.Empty);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "item");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void PatchWithNoItemAsync()
        {
            try
            {
                var result = _orchestrate.PatchAsync(CollectionName, Guid.NewGuid().ToString(), string.Empty).Result;
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerExceptions.First() as ArgumentNullException;
                Assert.IsTrue(inner?.ParamName == "item");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void PatchIfMatchSuccess()
        {
            var match = _orchestrate.Get(CollectionName, "1");
            var item = new PatchItemString { Op = "replace", Path = "Valie", Value = "New and improved value!" };

            var result = _orchestrate.PatchIfMatch(CollectionName, "1", item, match.Path.Ref);

            Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);
        }

        [Test]
        public void MergeIfMatchSuccess()
        {
            var match = _orchestrate.Get(CollectionName, "1");
            var item = new MergeTestData { NewValue = "This is a merged value" };

            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            var json = JsonConvert.SerializeObject(item, settings);

            var result = _orchestrate.PatchIfMatch(CollectionName, match.Path.Key, json, match.Path.Ref);

            Assert.IsTrue(result.Path.Ref.Length > 0);
        }

        [Test]
        public void PatchIfMatchAsyncSuccess()
        {
            var match = _orchestrate.Get(CollectionName, "1");
            var item = new PatchItemString { Op = "replace", Path = "Valie", Value = "New and improved value!" };

            var result = _orchestrate.PatchIfMatchAsync(CollectionName, "1", item, match.Path.Ref).Result;

            Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);
        }

        [Test]
        public void MergeIfMatchAsyncSuccess()
        {
            var match = _orchestrate.Get(CollectionName, "1");
            var item = new MergeTestData { NewValue = "This is a merged value" };

            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            var json = JsonConvert.SerializeObject(item, settings);

            var result = _orchestrate.PatchIfMatchAsync(CollectionName, match.Path.Key, json, match.Path.Ref).Result;

            Assert.IsTrue(result.Path.Ref.Length > 0);
        }

        [Test]
        public void PatchIfMatchFail()
        {
            var match = _orchestrate.Get(CollectionName, "1");
            var item = new PatchItemString { Op = "replace", Path = "Valie", Value = "New and improved value!" };

            try
            {
                _orchestrate.PatchIfMatch(CollectionName, "2", item, match.Path.Ref);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("412"));
            }
        }

        [Test]
        public void PatchIfMatchFailAsync()
        {
            var match = _orchestrate.Get(CollectionName, "1");
            var item = new PatchItemString { Op = "replace", Path = "Valie", Value = "New and improved value!" };

            try
            {
                var result = _orchestrate.PatchIfMatchAsync(CollectionName, "2", item, match.Path.Ref).Result;
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("412"));
            }
        }

        [Test]
        public void PatchIfMatchWithNoCollectionName()
        {
            var match = _orchestrate.Get(CollectionName, "1");
            var patchItem = new PatchItemString { Op = "add", Path = "/LastName", Value = "Doe" };

            try
            {
                _orchestrate.PatchIfMatch(string.Empty, "2", patchItem, match.Path.Ref);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "collectionName");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void PatchIfMatchWithNoCollectionNameAsync()
        {
            var match = _orchestrate.Get(CollectionName, "1");
            var patchItem = new PatchItemString { Op = "add", Path = "/LastName", Value = "Doe" };

            try
            {
                var result = _orchestrate.PatchIfMatchAsync(string.Empty, "2", patchItem, match.Path.Ref).Result;
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
        public void PatchIfMatchWithNoKey()
        {
            var match = _orchestrate.Get(CollectionName, "1");
            var patchItem = new PatchItemString { Op = "add", Path = "/LastName", Value = "Doe" };

            try
            {
                _orchestrate.PatchIfMatch(CollectionName, string.Empty, patchItem, match.Path.Ref);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "key");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void PatchIfMatchWithNoKeyAsync()
        {
            var match = _orchestrate.Get(CollectionName, "1");
            var patchItem = new PatchItemString { Op = "add", Path = "/LastName", Value = "Doe" };

            try
            {
                var result = _orchestrate.PatchIfMatchAsync(CollectionName, string.Empty, patchItem, match.Path.Ref).Result;
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerExceptions.First() as ArgumentNullException;
                Assert.IsTrue(inner?.ParamName == "key");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void PatchIfMatchWithNoItem()
        {
            var match = _orchestrate.Get(CollectionName, "1");

            try
            {
                _orchestrate.PatchIfMatch(CollectionName, "2", null, match.Path.Ref);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "item");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void PatchIfMatchWithNoItemAsync()
        {
            var match = _orchestrate.Get(CollectionName, "1");

            try
            {
                var result = _orchestrate.PatchIfMatchAsync(CollectionName, "2", null, match.Path.Ref).Result;
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerExceptions.First() as ArgumentNullException;
                Assert.IsTrue(inner?.ParamName == "item");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void PatchIfMatchWithNoIfMatch()
        {
            var patchItem = new PatchItemString { Op = "add", Path = "/LastName", Value = "Doe" };

            try
            {
                _orchestrate.PatchIfMatch(CollectionName, "2", patchItem, string.Empty);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "ifMatch");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void PatchIfMatchWithNoIfMatchAsync()
        {
            var patchItem = new PatchItemString { Op = "add", Path = "/LastName", Value = "Doe" };

            try
            {
                var result = _orchestrate.PatchIfMatchAsync(CollectionName, "2", patchItem, string.Empty).Result;
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerExceptions.First() as ArgumentNullException;
                Assert.IsTrue(inner?.ParamName == "ifMatch");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        #endregion

        #region Delete Tests

        [Test]
        public void DeleteSuccessNoPurge()
        {
            var item = new TestData {Id = 3, Value = "A successful object PUT"};
            var put = _orchestrate.Put(CollectionName, "3", item);

            var result = _orchestrate.Delete(CollectionName, "3", false);

            Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);

            var graveyard = _orchestrate.Get(CollectionName, "3", put.Path.Ref.Replace("\"", string.Empty));

            Assert.IsTrue(graveyard.Value != null);
        }

        [Test]
        public void DeleteSuccessNoPurgeAsync()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            var put = _orchestrate.Put(CollectionName, "3", item);

            var result = _orchestrate.DeleteAsync(CollectionName, "3", false).Result;

            Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);

            var graveyard = _orchestrate.Get(CollectionName, "3", put.Path.Ref.Replace("\"", string.Empty));

            Assert.IsTrue(graveyard.Value != null);
        }

        [Test]
        public void DeleteSuccessPurge()
        {
            var item = new TestData {Id = 3, Value = "A successful object PUT"};
            var put = _orchestrate.Put(CollectionName, "3", item);

            var result = _orchestrate.Delete(CollectionName, "3", true);

            Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);

            try
            {
                _orchestrate.Get(CollectionName, "3", put.Path.Ref.Replace("\"", string.Empty));
            }
            catch (Exception ex)
            {
                //TODO: (CV) Should change this to rely on the result rather than exception.
                Assert.IsTrue(ex.ToString().Contains("404"));
            }
        }

        [Test]
        public void DeleteSuccessPurgeAsync()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            var put = _orchestrate.Put(CollectionName, "3", item);

            var result = _orchestrate.DeleteAsync(CollectionName, "3", true).Result;

            Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);

            try
            {
                _orchestrate.Get(CollectionName, "3", put.Path.Ref.Replace("\"", string.Empty));
            }
            catch (Exception ex)
            {
                //TODO: (CV) Should change this to rely on the result rather than exception.
                Assert.IsTrue(ex.ToString().Contains("404"));
            }
        }

        [Test]
        public void DeleteNotFound()
        {
            var result = _orchestrate.Delete(CollectionName, "ABCD", false);
            Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);
        }

        [Test]
        public void DeleteNotFoundAsync()
        {
            var result = _orchestrate.DeleteAsync(CollectionName, "ABCD", false).Result;
            Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);
        }

        [Test]
        public void DeleteWithNoCollectionName()
        {
            try
            {
                _orchestrate.Delete(string.Empty, "ABCD", false);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "collectionName");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void DeleteWithNoCollectionNameAsync()
        {
            try
            {
                var result = _orchestrate.DeleteAsync(string.Empty, "ABCD", false).Result;
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
        public void DeleteWithNoKey()
        {
            try
            {
                _orchestrate.Delete(CollectionName, string.Empty, false);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "key");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void DeleteWithNoKeyAsync()
        {
            try
            {
                var result = _orchestrate.DeleteAsync(CollectionName, string.Empty, false).Result;
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerExceptions.First() as ArgumentNullException;
                Assert.IsTrue(inner?.ParamName == "key");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void DeleteIfMatchSuccedNoPurge()
        {
            var item = new TestData {Id = 4, Value = "A successful object PUT"};
            _orchestrate.Put(CollectionName, "4", item);
            var match = _orchestrate.Get(CollectionName, "4");

            var result = _orchestrate.DeleteIfMatch(CollectionName, "4", match.Path.Ref, false);

            Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);

            var graveyard = _orchestrate.Get(CollectionName, "4", match.Path.Ref.Replace("\"", string.Empty));

            Assert.IsTrue(graveyard.Value != null);
        }

        [Test]
        public void DeleteIfMatchSuccedNoPurgeAsync()
        {
            var item = new TestData { Id = 4, Value = "A successful object PUT" };
            _orchestrate.Put(CollectionName, "4", item);
            var match = _orchestrate.Get(CollectionName, "4");

            var result = _orchestrate.DeleteIfMatchAsync(CollectionName, "4", match.Path.Ref, false).Result;

            Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);

            var graveyard = _orchestrate.Get(CollectionName, "4", match.Path.Ref.Replace("\"", string.Empty));

            Assert.IsTrue(graveyard.Value != null);
        }

        [Test]
        public void DeleteIfMatchFail()
        {
            var match = _orchestrate.Get(CollectionName, "1");

            try
            {
                _orchestrate.DeleteIfMatch(CollectionName, "2", match.Path.Ref, false);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("412"));
            }
        }

        [Test]
        public void DeleteIfMatchFailAsync()
        {
            var match = _orchestrate.Get(CollectionName, "1");

            try
            {
                var result = _orchestrate.DeleteIfMatchAsync(CollectionName, "2", match.Path.Ref, false).Result;
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("412"));
            }
        }

        [Test]
        public void DeleteIfMatchNoCollectionName()
        {
            var match = _orchestrate.Get(CollectionName, "1");

            try
            {
                _orchestrate.DeleteIfMatch(string.Empty, "3", match.Path.Ref, false);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "collectionName");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void DeleteIfMatchNoCollectionNameAsync()
        {
            var match = _orchestrate.Get(CollectionName, "1");

            try
            {
                var result = _orchestrate.DeleteIfMatchAsync(string.Empty, "3", match.Path.Ref, false).Result;
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
        public void DeleteIfMatchNoKey()
        {
            var match = _orchestrate.Get(CollectionName, "1");

            try
            {
                _orchestrate.DeleteIfMatch(CollectionName, string.Empty, match.Path.Ref, false);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "key");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void DeleteIfMatchNoKeyAsync()
        {
            var match = _orchestrate.Get(CollectionName, "1");

            try
            {
                var result = _orchestrate.DeleteIfMatchAsync(CollectionName, string.Empty, match.Path.Ref, false).Result;
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerExceptions.First() as ArgumentNullException;
                Assert.IsTrue(inner?.ParamName == "key");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void DeleteIfMatchNoIfMatch()
        {
            try
            {
                _orchestrate.DeleteIfMatch(CollectionName, "3", string.Empty, false);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "ifMatch");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void DeleteIfMatchNoIfMatchAsync()
        {
            try
            {
                var result = _orchestrate.DeleteIfMatchAsync(CollectionName, "3", string.Empty, false).Result;
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerExceptions.First() as ArgumentNullException;
                Assert.IsTrue(inner?.ParamName == "ifMatch");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        #endregion
    }
}
