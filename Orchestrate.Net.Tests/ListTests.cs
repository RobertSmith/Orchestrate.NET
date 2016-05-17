using System;
using System.Linq;
using NUnit.Framework;
using Orchestrate.Net.Tests.Helpers;

// ReSharper disable UnusedVariable

namespace Orchestrate.Net.Tests
{
    [TestFixture]
    public class ListTests
    {
        private const string CollectionName = "ListTestCollection";
        private Orchestrate _orchestrate;

        [TestFixtureSetUp]
        public static void ClassInitialize()
        {
            var orchestrate = new Orchestrate(TestHelper.ApiKey);

            var item = new TestData {Id = 1, Value = "Inital Test Item"};
            var item2 = new TestData {Id = 2, Value = "Inital Test Item #2"};
            var item3 = new TestData {Id = 3, Value = "Inital Test Item #3"};

            orchestrate.CreateCollection(CollectionName, "1", item);
            orchestrate.Put(CollectionName, "2", item2);
            orchestrate.Put(CollectionName, "3", item3);
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

        #region List Tests

        [Test]
        public void List()
        {
            var result = _orchestrate.List(CollectionName, 10, null, null);
            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void ListAsync()
        {
            var result = _orchestrate.ListAsync(CollectionName, 10, null, null).Result;
            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void ListWithStartKey()
        {
            var result = _orchestrate.List(CollectionName, 10, "1", null);
            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void ListWithStartKeyAsync()
        {
            var result = _orchestrate.ListAsync(CollectionName, 10, "1", null).Result;
            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void ListWithAfterKey()
        {
            var result = _orchestrate.List(CollectionName, 10, null, "1");
            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void ListWithAfterKeyAsync()
        {
            var result = _orchestrate.ListAsync(CollectionName, 10, null, "1").Result;
            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void ListWithStartKeyAndAfterKey()
        {
            try
            {
                _orchestrate.List(CollectionName, 10, "1", "2");
            }
            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.ParamName == "startKey");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void ListWithStartKeyAndAfterKeyAsync()
        {
            try
            {
                var result = _orchestrate.ListAsync(CollectionName, 10, "1", "2").Result;
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerExceptions.First() as ArgumentException;
                Assert.IsTrue(inner?.ParamName == "startKey");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void ListWithEndKey()
        {
            var result = _orchestrate.List(CollectionName, 10, null, null, "2");
            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void ListWithEndKeyAsync()
        {
            var result = _orchestrate.ListAsync(CollectionName, 10, null, null, "2").Result;
            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void ListWithBeforeKey()
        {
            var result = _orchestrate.List(CollectionName, 10, null, null, null, "2");
            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void ListWithBeforeKeyAsync()
        {
            var result = _orchestrate.ListAsync(CollectionName, 10, null, null, null, "2").Result;
            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void ListWithBeforeKeyAndEndKey()
        {
            try
            {
                var result = _orchestrate.List(CollectionName, 10, null, null, "1", "2");
            }
            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.ParamName == "endKey");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void ListWithBeforeKeyAndEndKeyAsync()
        {
            try
            {
                var result = _orchestrate.ListAsync(CollectionName, 10, null, null, "1", "2").Result;
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerExceptions.First() as ArgumentException;
                Assert.IsTrue(inner?.ParamName == "endKey");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void ListWithStartKeyAndEndKey()
        {
            var result = _orchestrate.List(CollectionName, 10, "1", null, "2");
            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void ListWithStartKeyAndEndKeyAsync()
        {
            var result = _orchestrate.ListAsync(CollectionName, 10, "1", null, "2").Result;
            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void ListWithStartKeyAndBeforeKey()
        {
            var result = _orchestrate.List(CollectionName, 10, "1", null, null, "2");
            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void ListWithStartKeyAndBeforeKeyAsync()
        {
            var result = _orchestrate.ListAsync(CollectionName, 10, "1", null, null, "2").Result;
            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void ListWithAfterKeyAndEndKey()
        {
            var result = _orchestrate.List(CollectionName, 10, null, "1", "2");
            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void ListWithAfterKeyAndEndKeyAsync()
        {
            var result = _orchestrate.ListAsync(CollectionName, 10, null, "1", "2").Result;
            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void ListWithAfterKeyAndBeforeKey()
        {
            var result = _orchestrate.List(CollectionName, 10, null, "1", null, "3");
            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void ListWithAfterKeyAndBeforeKeyAsync()
        {
            var result = _orchestrate.ListAsync(CollectionName, 10, null, "1", null, "3").Result;
            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void ListWithNoCollectionName()
        {
            try
            {
                _orchestrate.List(string.Empty, 10, null, null);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "collectionName");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void ListWithNoCollectionNameAsync()
        {
            try
            {
                var result = _orchestrate.ListAsync(string.Empty, 10, null, null).Result;
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
        public void ListWithLimitOutOfBounds()
        {
            try
            {
                _orchestrate.List(CollectionName, -110, null, null);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.IsTrue(ex.ParamName == "limit");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void ListWithLimitOutOfBoundsAsync()
        {
            try
            {
                var result = _orchestrate.ListAsync(CollectionName, -110, null, null).Result;
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerExceptions.First() as ArgumentOutOfRangeException;
                Assert.IsTrue(inner?.ParamName == "limit");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        #endregion
    }
}