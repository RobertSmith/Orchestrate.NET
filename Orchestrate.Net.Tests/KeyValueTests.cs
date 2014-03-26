using System;
using NUnit.Framework;
using Newtonsoft.Json;
using Orchestrate.Net.Tests.Helpers;

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
        public void GetByNonExistantKey()
        {
            try
            {
                _orchestrate.Get(CollectionName, "9999");
						}
            catch (Exception ex)
            {
							//TODO: (CV) Should change this to rely on the result rather than exception.
                Assert.IsTrue(ex.ToString().Contains("404"));
            }
        }

        [Test]
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

        [Test]
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

        [Test]
        public void PutAsObject()
        {
            var item = new TestData {Id = 3, Value = "A successful object PUT"};
            var result = _orchestrate.Put(CollectionName, Guid.NewGuid().ToString(), item);

            Assert.IsTrue(result.Path.Ref.Length > 0);
        }

        [Test]
        public void PutAsString()
        {
            var item = new TestData { Id = 4, Value = "A successful string PUT" };
            var json = JsonConvert.SerializeObject(item);
            var result = _orchestrate.Put(CollectionName, Guid.NewGuid().ToString(), json);

            Assert.IsTrue(result.Path.Ref.Length > 0);
        }

        [Test]
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

        [Test]
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
        public void PutIfMatchSuccess()
        {
            var match = _orchestrate.Get(CollectionName, "1");
            var item = new TestData { Id = 1, Value = "New and improved value!" };

            var result = _orchestrate.PutIfMatch(CollectionName, "1", item, match.Path.Ref);

            Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);
        }

        [Test]
        public void PutIfMatchFail()
        {
            var match = _orchestrate.Get(CollectionName, "1");
            var item = new TestData { Id = 1, Value = "Value, now with more moxie!" };

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
        public void PutIfMatchWithNoCollectionName()
        {
            var match = _orchestrate.Get(CollectionName, "1");
            var item = new TestData { Id = 1, Value = "Value, now with more moxie!" };

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
        public void PutIfMatchWithNoKey()
        {
            var match = _orchestrate.Get(CollectionName, "1");
            var item = new TestData { Id = 1, Value = "Value, now with more moxie!" };

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
        public void PutIfMatchWithNoIfMatch()
        {
            var item = new TestData { Id = 1, Value = "Value, now with more moxie!" };

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
        public void PutIfNoneMatchSucess()
        {
            var item = new TestData { Id = 88, Value = "Test Value 88" };

            var result = _orchestrate.PutIfNoneMatch(CollectionName, "88", item);

            Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);
        }

        [Test]
        public void PutIfNoneMatchFail()
        {
            var item = new TestData { Id = 1, Value = "Test Value 1" };

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
        public void PutIfNoneMatchWithNoCollectionName()
        {
            var item = new TestData { Id = 77, Value = "Test Value 77" };

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
        public void PutIfNoneMatchWithNoKey()
        {
            var item = new TestData { Id = 77, Value = "Test Value 77" };

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

        #endregion

        #region Delete Tests

        [Test]
        public void DeleteSuccessNoPurge()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            var put = _orchestrate.Put(CollectionName, "3", item);

            var result = _orchestrate.Delete(CollectionName, "3", false);

            Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);

            var graveyard = _orchestrate.Get(CollectionName, "3", put.Path.Ref.Replace("\"", string.Empty));

            Assert.IsTrue(graveyard.Value != null);
        }

        [Test]
        public void DeleteSuccessPurge()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
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
        public void DeleteNotFound()
        {
            var result = _orchestrate.Delete(CollectionName, "ABCD", false);
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
        public void DeleteIfMatchSuccedNoPurge()
        {
            var item = new TestData { Id = 4, Value = "A successful object PUT" };
            _orchestrate.Put(CollectionName, "4", item);
            var match = _orchestrate.Get(CollectionName, "4");

            var result = _orchestrate.DeleteIfMatch(CollectionName, "4", match.Path.Ref, false);

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

        #endregion
    }
}
