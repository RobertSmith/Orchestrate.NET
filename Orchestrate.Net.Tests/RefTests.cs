using System;
using System.Linq;
using NUnit.Framework;
using Orchestrate.Net.Tests.Helpers;

// ReSharper disable UnusedVariable

namespace Orchestrate.Net.Tests
{
    [TestFixture]
    public class RefTests
    {
        private const string CollectionName = "RefTestCollection";
        private Orchestrate _orchestrate;

        [TestFixtureSetUp]
        public static void ClassInitialize()
        {
            var orchestrate = new Orchestrate(TestHelper.ApiKey);
            var item = new TestData { Id = 1, Value = "Inital Test Item" };

            orchestrate.Put(CollectionName, "1", item);

            item.Value = "Updated Value";
            orchestrate.Patch(CollectionName, "1", item);
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

        #region Ref

        [Test]
        public void GetByRef()
        {
            var list = _orchestrate.List(CollectionName, 10, null, null);
            var match = list.Results.Single(x => x.Path.Key == "1");

            var result = _orchestrate.Ref(CollectionName, "1", match.Path.Ref);

            Assert.IsTrue(result.Value != null);
        }

        [Test]
        public void GetByRefAsync()
        {
            var list = _orchestrate.List(CollectionName, 10, null, null);
            var match = list.Results.Single(x => x.Path.Key == "1");

            var result = _orchestrate.RefAsync(CollectionName, "1", match.Path.Ref).Result;

            Assert.IsTrue(result.Value != null);
        }

        [Test]
        public void GetByRefWithNoCollectionName()
        {
            try
            {
                _orchestrate.Ref(string.Empty, "9999", "refernce#");
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "collectionName");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void GetByRefWithNoCollectionNameAsync()
        {
            try
            {
                var result = _orchestrate.RefAsync(string.Empty, "9999", "refernce#").Result;
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
        public void GetByRefWithNoKey()
        {
            try
            {
                _orchestrate.Ref(CollectionName, string.Empty, "refernce#");
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "key");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void GetByRefWithNoKeyAsync()
        {
            try
            {
                var result = _orchestrate.RefAsync(CollectionName, string.Empty, "refernce#").Result;
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
        public void GetByRefWithNoRef()
        {
            try
            {
                _orchestrate.Ref(CollectionName, "9999", string.Empty);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "reference");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void GetByRefWithNoRefAsync()
        {
            try
            {
                var result = _orchestrate.RefAsync(CollectionName, "9999", string.Empty).Result;
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerExceptions.First() as ArgumentNullException;
                Assert.IsTrue(inner?.ParamName == "reference");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        #endregion

        #region RefList 

        [Test]
        public void RefList()
        {
            var result = _orchestrate.RefList(CollectionName, "1");
            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void RefListAsync()
        {
            var result = _orchestrate.RefListAsync(CollectionName, "1").Result;
            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void RefListWithLimit()
        {
            var result = _orchestrate.RefList(CollectionName, "1", 1);
            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void RefListWithLimitAsync()
        {
            var result = _orchestrate.RefListAsync(CollectionName, "1", 1).Result;
            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void RefListWithOffset()
        {
            var result = _orchestrate.RefList(CollectionName, "1", 10, 1);
            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void RefListWithOffsetAsync()
        {
            var result = _orchestrate.RefListAsync(CollectionName, "1", 10, 1).Result;
            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void RefListWithValues()
        {
            var result = _orchestrate.RefList(CollectionName, "1", 10, 0, true);
            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void RefListWithValuesAsync()
        {
            var result = _orchestrate.RefListAsync(CollectionName, "1", 10, 0, true).Result;
            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void RefListNoCollectionName()
        {
            try
            {
                _orchestrate.RefList(string.Empty, "1", 10, 0, true);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "collectionName");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void RefListNoCollectionNameAsync()
        {
            try
            {
                var result = _orchestrate.RefListAsync(string.Empty, "1", 10, 0, true).Result;
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
        public void RefListNoKey()
        {
            try
            {
                _orchestrate.RefList(CollectionName, string.Empty, 10, 0, true);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "key");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void RefListNoKeyAsync()
        {
            try
            {
                var result = _orchestrate.RefListAsync(CollectionName, string.Empty, 10, 0, true).Result;
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
        public void RefListBadLimit()
        {
            try
            {
                _orchestrate.RefList(CollectionName, "1", 999, 0, true);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.IsTrue(ex.ParamName == "limit");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void RefListBadLimitAsync()
        {
            try
            {
                var result = _orchestrate.RefListAsync(CollectionName, "1", 999, 0, true).Result;
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerExceptions.First() as ArgumentOutOfRangeException;
                Assert.IsTrue(inner?.ParamName == "limit");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void RefListBadOffset()
        {
            try
            {
                _orchestrate.RefList(CollectionName, "1", 10, -1, true);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.IsTrue(ex.ParamName == "offset");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void RefListBadOffsetAsync()
        {
            try
            {
                var result = _orchestrate.RefListAsync(CollectionName, "1", 10, -1, true).Result;
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerExceptions.First() as ArgumentOutOfRangeException;
                Assert.IsTrue(inner?.ParamName == "offset");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        #endregion
    }
}