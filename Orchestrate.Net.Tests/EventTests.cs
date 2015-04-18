using System;
using System.Linq;
using NUnit.Framework;
using Newtonsoft.Json;
using Orchestrate.Net.Tests.Helpers;

namespace Orchestrate.Net.Tests
{
    [TestFixture]
    public class EventTests
    {
        private const string CollectionName = "EventTestCollection";
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

        [Test]
        public void PutEventNowTimeStampAsObject()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            var result = _orchestrate.PutEvent(CollectionName, "1", "comment", DateTime.UtcNow, item);

            Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);
        }

        [Test]
        public void PutEventNowTimeStampAsString()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            var str = JsonConvert.SerializeObject(item);
            var result = _orchestrate.PutEvent(CollectionName, "1", "comment", DateTime.UtcNow, str);

            Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);
        }

        [Test]
        public void PutEventNowTimeStampAsyncAsObject()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            var result = _orchestrate.PutEventAsync(CollectionName, "1", "comment", DateTime.UtcNow, item).Result;

            Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);
        }

        [Test]
        public void PutEventNowTimeStampAsyncAsString()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            var str = JsonConvert.SerializeObject(item);
            var result = _orchestrate.PutEventAsync(CollectionName, "1", "comment", DateTime.UtcNow, str).Result;

            Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);
        }

        [Test]
        public void PutEventNoTimeStamp()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            var result = _orchestrate.PutEvent(CollectionName, "1", "comment", null, item);

            Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);
        }

        [Test]
        public void PutEventNoTimeStampAsync()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            var result = _orchestrate.PutEventAsync(CollectionName, "1", "comment", null, item).Result;

            Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);
        }

        [Test]
        public void PutEventWithNoCollectionName()
        {
            try
            {
                var item = new TestData { Id = 3, Value = "A successful object PUT" };
                _orchestrate.PutEvent(string.Empty, "1", "comment", null, item);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "collectionName");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void PutEventWithNoCollectionNameAsync()
        {
            try
            {
                var item = new TestData { Id = 3, Value = "A successful object PUT" };
                var result = _orchestrate.PutEventAsync(string.Empty, "1", "comment", null, item).Result;
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
        public void PutEventWithNoKey()
        {
            try
            {
                var item = new TestData { Id = 3, Value = "A successful object PUT" };
                _orchestrate.PutEvent(CollectionName, string.Empty, "comment", null, item);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "key");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void PutEventWithNoKeyAsync()
        {
            try
            {
                var item = new TestData { Id = 3, Value = "A successful object PUT" };
                var result = _orchestrate.PutEventAsync(CollectionName, string.Empty, "comment", null, item).Result;
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
        public void PutEventWithNoType()
        {
            try
            {
                var item = new TestData { Id = 3, Value = "A successful object PUT" };
                _orchestrate.PutEvent(CollectionName, "1", string.Empty, null, item);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "type");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void PutEventWithNoTypeAsync()
        {
            try
            {
                var item = new TestData { Id = 3, Value = "A successful object PUT" };
                var result = _orchestrate.PutEventAsync(CollectionName, "1", string.Empty, null, item).Result;
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerExceptions.First() as ArgumentNullException;
                Assert.IsTrue(inner.ParamName == "type");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void GetEventsNoStartEnd()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            _orchestrate.PutEvent(CollectionName, "1", "comment", DateTime.UtcNow, item);
            var result = _orchestrate.GetEvents(CollectionName, "1", "comment");

            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void GetEventsNoStartEndAsync()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            _orchestrate.PutEvent(CollectionName, "1", "comment", DateTime.UtcNow, item);
            var result = _orchestrate.GetEventsAsync(CollectionName, "1", "comment").Result;

            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void GetEventsWithStartDate()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            _orchestrate.PutEvent(CollectionName, "1", "comment", DateTime.UtcNow, item);
            var result = _orchestrate.GetEvents(CollectionName, "1", "comment", DateTime.UtcNow.AddHours(-1));

            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void GetEventsWithStartDateAsync()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            _orchestrate.PutEvent(CollectionName, "1", "comment", DateTime.UtcNow, item);
            var result = _orchestrate.GetEventsAsync(CollectionName, "1", "comment", DateTime.UtcNow.AddHours(-1)).Result;

            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void GetEventsWithEndDate()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            _orchestrate.PutEvent(CollectionName, "1", "comment", DateTime.UtcNow, item);
            var result = _orchestrate.GetEvents(CollectionName, "1", "comment", null, DateTime.UtcNow.AddHours(1));

            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void GetEventsWithEndDateAsync()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            _orchestrate.PutEvent(CollectionName, "1", "comment", DateTime.UtcNow, item);
            var result = _orchestrate.GetEventsAsync(CollectionName, "1", "comment", null, DateTime.UtcNow.AddHours(1)).Result;

            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void GetEventsWithStartAndEndDate()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            _orchestrate.PutEvent(CollectionName, "1", "comment", DateTime.UtcNow, item);
            var result = _orchestrate.GetEvents(CollectionName, "1", "comment", DateTime.UtcNow.AddHours(-1), DateTime.UtcNow.AddHours(1));

            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void GetEventsWithStartAndEndDateAsync()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            _orchestrate.PutEvent(CollectionName, "1", "comment", DateTime.UtcNow, item);
            var result = _orchestrate.GetEventsAsync(CollectionName, "1", "comment", DateTime.UtcNow.AddHours(-1), DateTime.UtcNow.AddHours(1)).Result;

            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void GetEventsWithNoCollectionName()
        {
            try
            {
                _orchestrate.GetEvents(string.Empty, "1", "comment");
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "collectionName");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void GetEventsWithNoCollectionNameAsync()
        {
            try
            {
                var result = _orchestrate.GetEventsAsync(string.Empty, "1", "comment").Result;
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
        public void GetEventsWithNoKey()
        {
            try
            {
                _orchestrate.GetEvents(CollectionName, string.Empty, "comment");
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "key");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void GetEventsWithNoKeyAsync()
        {
            try
            {
                var result = _orchestrate.GetEventsAsync(CollectionName, string.Empty, "comment").Result;
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
        public void GetEventsWithNoType()
        {
            try
            {
                _orchestrate.GetEvents(CollectionName, "1", string.Empty);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "type");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void GetEventsWithNoTypeAsync()
        {
            try
            {
                var result = _orchestrate.GetEventsAsync(CollectionName, "1", string.Empty).Result;
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerExceptions.First() as ArgumentNullException;
                Assert.IsTrue(inner.ParamName == "type");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }
    }
}