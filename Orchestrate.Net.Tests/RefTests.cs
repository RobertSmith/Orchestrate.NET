using System;
using System.Linq;
using NUnit.Framework;
using Orchestrate.Net.Tests.Helpers;

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

        #region Ref Tests

        [Test]
        public void GetByRef()
        {
            var list = _orchestrate.List(CollectionName, 10, null, null);
            var match = list.Results.Single(x => x.Path.Key == "1");

            var result = _orchestrate.Get(CollectionName, "1", match.Path.Ref);

            Assert.IsTrue(result.Value != null);
        }

        [Test]
        public void GetByRefAsync()
        {
            var list = _orchestrate.List(CollectionName, 10, null, null);
            var match = list.Results.Single(x => x.Path.Key == "1");

            var result = _orchestrate.GetAsync(CollectionName, "1", match.Path.Ref).Result;

            Assert.IsTrue(result.Value != null);
        }

        [Test]
        public void GetByRefWithNoCollectionName()
        {
            try
            {
                _orchestrate.Get(string.Empty, "9999", "refernce#");
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
                var result = _orchestrate.GetAsync(string.Empty, "9999", "refernce#").Result;
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerExceptions.First() as ArgumentNullException;
                Assert.IsTrue(inner.ParamName == "collectionName");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void GetByRefWithNoKey()
        {
            try
            {
                _orchestrate.Get(CollectionName, string.Empty, "refernce#");
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
                var result = _orchestrate.GetAsync(CollectionName, string.Empty, "refernce#").Result;
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerExceptions.First() as ArgumentNullException;
                Assert.IsTrue(inner.ParamName == "key");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void GetByRefWithNoRef()
        {
            try
            {
                _orchestrate.Get(CollectionName, "9999", string.Empty);
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
                var result = _orchestrate.GetAsync(CollectionName, "9999", string.Empty).Result;
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerExceptions.First() as ArgumentNullException;
                Assert.IsTrue(inner.ParamName == "reference");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        #endregion
    }
}
