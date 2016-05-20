using System;
using System.Linq;
using System.Net.Http;
using NUnit.Framework;
using Newtonsoft.Json;
using Orchestrate.Net.Tests.Helpers;

// ReSharper disable UnusedVariable

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

            orchestrate.Put(CollectionName, "1", item);
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
        public void GetEvent()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            var eventPosted = _orchestrate.PostEvent(CollectionName, "1", "comment", DateTime.UtcNow, item);
            var eventList = _orchestrate.ListEvents(CollectionName, "1", "comment", 100);
            var firstEvent = eventList.Results.First();

            var result = _orchestrate.GetEvent(CollectionName, "1", "comment", Orchestrate.ConvertFromUnixTimeStamp(firstEvent.Path.TimeStamp), firstEvent.Path.Ordinal);

            Assert.IsTrue(result.Ordinal > 0);
        }

        [Test]
        public void GetEventAsync()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            var eventPosted = _orchestrate.PostEvent(CollectionName, "1", "comment", DateTime.UtcNow, item);
            var eventList = _orchestrate.ListEvents(CollectionName, "1", "comment", 100);
            var firstEvent = eventList.Results.First();

            var result = _orchestrate.GetEventAsync(CollectionName, "1", "comment", Orchestrate.ConvertFromUnixTimeStamp(firstEvent.Path.TimeStamp), firstEvent.Path.Ordinal).Result;

            Assert.IsTrue(result.Ordinal > 0);
        }

        [Test]
        public void GetEventBadTimeStamp()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            var eventPosted = _orchestrate.PostEvent(CollectionName, "1", "comment", DateTime.UtcNow, item);
            var eventList = _orchestrate.ListEvents(CollectionName, "1", "comment", 100);
            var firstEvent = eventList.Results.First();

            try
            {
                var result = _orchestrate.GetEvent(CollectionName, "1", "comment", DateTime.UtcNow.AddYears(-1000), firstEvent.Path.Ordinal);
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerExceptions.First() as HttpRequestException;
                Assert.IsTrue(inner?.Message.Contains("404") == true);
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void GetEventBadTimeStampAsync()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            var eventPosted = _orchestrate.PostEvent(CollectionName, "1", "comment", DateTime.UtcNow, item);
            var eventList = _orchestrate.ListEvents(CollectionName, "1", "comment", 100);
            var firstEvent = eventList.Results.First();

            try
            {
                var result = _orchestrate.GetEventAsync(CollectionName, "1", "comment", DateTime.UtcNow.AddYears(-1000), firstEvent.Path.Ordinal).Result;
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerExceptions.First() as HttpRequestException;
                Assert.IsTrue(inner?.Message.Contains("404") == true);
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void GetEventBadOrdinal()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            var eventPosted = _orchestrate.PostEvent(CollectionName, "1", "comment", DateTime.UtcNow, item);
            var eventList = _orchestrate.ListEvents(CollectionName, "1", "comment", 100);
            var firstEvent = eventList.Results.First();

            try
            {
                var result = _orchestrate.GetEvent(CollectionName, "1", "comment", Orchestrate.ConvertFromUnixTimeStamp(firstEvent.Path.TimeStamp), -1);
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerExceptions.First() as HttpRequestException;
                Assert.IsTrue(inner?.Message.Contains("404") == true);
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void GetEventBadOrdinalAsync()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            var eventPosted = _orchestrate.PostEvent(CollectionName, "1", "comment", DateTime.UtcNow, item);
            var eventList = _orchestrate.ListEvents(CollectionName, "1", "comment", 100);
            var firstEvent = eventList.Results.First();

            try
            {
                var result = _orchestrate.GetEventAsync(CollectionName, "1", "comment", Orchestrate.ConvertFromUnixTimeStamp(firstEvent.Path.TimeStamp), -1).Result;
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerExceptions.First() as HttpRequestException;
                Assert.IsTrue(inner?.Message.Contains("404") == true);
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void GetEventNoCollectionName()
        {
            try
            {
                var result = _orchestrate.GetEvent(string.Empty, "1", "comment", DateTime.UtcNow, 1);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.Message.Contains("collectionName"));
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void GetEventNoCollectionNameAsync()
        {
            try
            {
                var result = _orchestrate.GetEventAsync(string.Empty, "1", "comment", DateTime.UtcNow, 1).Result;
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerExceptions.First() as ArgumentNullException;
                Assert.IsTrue(inner?.Message.Contains("collectionName") == true);
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void GetEventBadKey()
        {
            try
            {
                var result = _orchestrate.GetEvent(CollectionName, "-1", "comment", DateTime.UtcNow, 1);
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerExceptions.First() as HttpRequestException;
                Assert.IsTrue(inner?.Message.Contains("404") == true);
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void GetEventBadKeyAsync()
        {
            try
            {
                var result = _orchestrate.GetEventAsync(CollectionName, "-1", "comment", DateTime.UtcNow, 1).Result;
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerExceptions.First() as HttpRequestException;
                Assert.IsTrue(inner?.Message.Contains("404") == true);
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void GetEventNoKey()
        {
            try
            {
                var result = _orchestrate.GetEvent(CollectionName, string.Empty, "comment", DateTime.UtcNow, 1);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.Message.Contains("key"));
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void GetEventNoKeyAsync()
        {
            try
            {
                var result = _orchestrate.GetEventAsync(CollectionName, string.Empty, "comment", DateTime.UtcNow, 1).Result;
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerExceptions.First() as ArgumentNullException;
                Assert.IsTrue(inner?.Message.Contains("key") == true);
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void GetEventBadType()
        {
            try
            {
                var result = _orchestrate.GetEvent(CollectionName, "-1", "thisisnotvalid", DateTime.UtcNow, 1);
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerExceptions.First() as HttpRequestException;
                Assert.IsTrue(inner?.Message.Contains("404") == true);
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void GetEventBadTypeAsync()
        {
            try
            {
                var result = _orchestrate.GetEventAsync(CollectionName, "-1", "thisisnotvalid", DateTime.UtcNow, 1).Result;
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerExceptions.First() as HttpRequestException;
                Assert.IsTrue(inner?.Message.Contains("404") == true);
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void GetEventNoType()
        {
            try
            {
                var result = _orchestrate.GetEvent(CollectionName, "1", string.Empty, DateTime.UtcNow, 1);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.Message.Contains("type"));
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void GetEventNoTypeAsync()
        {
            try
            {
                var result = _orchestrate.GetEventAsync(CollectionName, "1", string.Empty, DateTime.UtcNow, 1).Result;
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerExceptions.First() as ArgumentNullException;
                Assert.IsTrue(inner?.Message.Contains("type") == true);
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        #endregion

        #region Post Tests

        [Test]
        public void PostEventNowTimeStampAsObject()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            var result = _orchestrate.PostEvent(CollectionName, "1", "comment", DateTime.UtcNow, item);

            Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);
        }

        [Test]
        public void PostEventNowTimeStampAsObjectAsync()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            var result = _orchestrate.PostEventAsync(CollectionName, "1", "comment", DateTime.UtcNow, item).Result;

            Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);
        }

        [Test]
        public void PostEventNowTimeStampAsyncAsObject()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            var result = _orchestrate.PostEventAsync(CollectionName, "1", "comment", DateTime.UtcNow, item).Result;

            Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);
        }

        [Test]
        public void PostEventNowTimeStampAsyncAsString()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            var str = JsonConvert.SerializeObject(item);
            var result = _orchestrate.PostEventAsync(CollectionName, "1", "comment", DateTime.UtcNow, str).Result;

            Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);
        }

        [Test]
        public void PostEventNoTimeStamp()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            var result = _orchestrate.PostEvent(CollectionName, "1", "comment", null, item);

            Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);
        }

        [Test]
        public void PostEventNoTimeStampAsync()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            var result = _orchestrate.PostEventAsync(CollectionName, "1", "comment", null, item).Result;

            Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);
        }

        [Test]
        public void PostEventWithNoCollectionName()
        {
            try
            {
                var item = new TestData { Id = 3, Value = "A successful object PUT" };
                _orchestrate.PostEvent(string.Empty, "1", "comment", null, item);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "collectionName");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void PostEventWithNoCollectionNameAsync()
        {
            try
            {
                var item = new TestData { Id = 3, Value = "A successful object PUT" };
                var result = _orchestrate.PostEventAsync(string.Empty, "1", "comment", null, item).Result;
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
        public void PostEventWithNoKey()
        {
            try
            {
                var item = new TestData { Id = 3, Value = "A successful object PUT" };
                _orchestrate.PostEvent(CollectionName, string.Empty, "comment", null, item);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "key");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void PostEventWithNoKeyAsync()
        {
            try
            {
                var item = new TestData { Id = 3, Value = "A successful object PUT" };
                var result = _orchestrate.PostEventAsync(CollectionName, string.Empty, "comment", null, item).Result;
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
        public void PostEventWithNoType()
        {
            try
            {
                var item = new TestData { Id = 3, Value = "A successful object PUT" };
                _orchestrate.PostEvent(CollectionName, "1", string.Empty, null, item);
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "type");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [Test]
        public void PostEventWithNoTypeAsync()
        {
            try
            {
                var item = new TestData { Id = 3, Value = "A successful object PUT" };
                var result = _orchestrate.PostEventAsync(CollectionName, "1", string.Empty, null, item).Result;
            }
            catch (AggregateException ex)
            {
                var inner = ex.InnerExceptions.First() as ArgumentNullException;
                Assert.IsTrue(inner?.ParamName == "type");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        #endregion

        #region Put Tests

        [Test]
        public void PutEvent_DateTime_Object()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            var eventPosted = _orchestrate.PostEvent(CollectionName, "1", "comment", DateTime.UtcNow, item);
            var eventList = _orchestrate.ListEvents(CollectionName, "1", "comment", 100);
            var firstEvent = eventList.Results.First();

            item.Value = "This was updated";

            var result = _orchestrate.PutEvent(CollectionName, "1", "comment", Orchestrate.ConvertFromUnixTimeStamp(firstEvent.Path.TimeStamp), firstEvent.Path.Ordinal, item);

            Assert.IsTrue(!string.IsNullOrEmpty(result.Path.Ref));

            var getResult = _orchestrate.GetEvent(CollectionName, "1", "comment", Orchestrate.ConvertFromUnixTimeStamp(firstEvent.Path.TimeStamp), firstEvent.Path.Ordinal);

            Assert.IsTrue(getResult.Value.ToString().Contains("updated"));

        }

        [Test]
        public void PutEventAsync_DateTime_Object()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            var eventPosted = _orchestrate.PostEvent(CollectionName, "1", "comment", DateTime.UtcNow, item);
            var eventList = _orchestrate.ListEvents(CollectionName, "1", "comment", 100);
            var firstEvent = eventList.Results.First();

            item.Value = "This was updated";

            var result = _orchestrate.PutEventAsync(CollectionName, "1", "comment", Orchestrate.ConvertFromUnixTimeStamp(firstEvent.Path.TimeStamp), firstEvent.Path.Ordinal, item).Result;

            Assert.IsTrue(!string.IsNullOrEmpty(result.Path.Ref));

            var getResult = _orchestrate.GetEvent(CollectionName, "1", "comment", Orchestrate.ConvertFromUnixTimeStamp(firstEvent.Path.TimeStamp), firstEvent.Path.Ordinal);

            Assert.IsTrue(getResult.Value.ToString().Contains("updated"));

        }

        [Test]
        public void PutEvent_UnixTimeStamp_Object()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            var eventPosted = _orchestrate.PostEvent(CollectionName, "1", "comment", DateTime.UtcNow, item);
            var eventList = _orchestrate.ListEvents(CollectionName, "1", "comment", 100);
            var firstEvent = eventList.Results.First();

            item.Value = "This was updated";

            var result = _orchestrate.PutEvent(CollectionName, "1", "comment", long.Parse(firstEvent.Path.TimeStamp), firstEvent.Path.Ordinal, item);

            Assert.IsTrue(!string.IsNullOrEmpty(result.Path.Ref));

            var getResult = _orchestrate.GetEvent(CollectionName, "1", "comment", Orchestrate.ConvertFromUnixTimeStamp(firstEvent.Path.TimeStamp), firstEvent.Path.Ordinal);

            Assert.IsTrue(getResult.Value.ToString().Contains("updated"));

        }

        [Test]
        public void PutEventAsync_UnixTimeStamp_Object()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            var eventPosted = _orchestrate.PostEvent(CollectionName, "1", "comment", DateTime.UtcNow, item);
            var eventList = _orchestrate.ListEvents(CollectionName, "1", "comment", 100);
            var firstEvent = eventList.Results.First();

            item.Value = "This was updated";

            var result = _orchestrate.PutEventAsync(CollectionName, "1", "comment", long.Parse(firstEvent.Path.TimeStamp), firstEvent.Path.Ordinal, item).Result;

            Assert.IsTrue(!string.IsNullOrEmpty(result.Path.Ref));

            var getResult = _orchestrate.GetEvent(CollectionName, "1", "comment", Orchestrate.ConvertFromUnixTimeStamp(firstEvent.Path.TimeStamp), firstEvent.Path.Ordinal);

            Assert.IsTrue(getResult.Value.ToString().Contains("updated"));

        }

        [Test]
        public void PutEvent_DateTime_String()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            var eventPosted = _orchestrate.PostEvent(CollectionName, "1", "comment", DateTime.UtcNow, item);
            var eventList = _orchestrate.ListEvents(CollectionName, "1", "comment", 100);
            var firstEvent = eventList.Results.First();

            item.Value = "This was updated";
            var itemString = JsonConvert.SerializeObject(item);

            var result = _orchestrate.PutEvent(CollectionName, "1", "comment", Orchestrate.ConvertFromUnixTimeStamp(firstEvent.Path.TimeStamp), firstEvent.Path.Ordinal, itemString);

            Assert.IsTrue(!string.IsNullOrEmpty(result.Path.Ref));

            var getResult = _orchestrate.GetEvent(CollectionName, "1", "comment", Orchestrate.ConvertFromUnixTimeStamp(firstEvent.Path.TimeStamp), firstEvent.Path.Ordinal);

            Assert.IsTrue(getResult.Value.ToString().Contains("updated"));

        }

        [Test]
        public void PutEventAsync_DateTime_String()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            var eventPosted = _orchestrate.PostEvent(CollectionName, "1", "comment", DateTime.UtcNow, item);
            var eventList = _orchestrate.ListEvents(CollectionName, "1", "comment", 100);
            var firstEvent = eventList.Results.First();

            item.Value = "This was updated";
            var itemString = JsonConvert.SerializeObject(item);

            var result = _orchestrate.PutEventAsync(CollectionName, "1", "comment", Orchestrate.ConvertFromUnixTimeStamp(firstEvent.Path.TimeStamp), firstEvent.Path.Ordinal, itemString).Result;

            Assert.IsTrue(!string.IsNullOrEmpty(result.Path.Ref));

            var getResult = _orchestrate.GetEvent(CollectionName, "1", "comment", Orchestrate.ConvertFromUnixTimeStamp(firstEvent.Path.TimeStamp), firstEvent.Path.Ordinal);

            Assert.IsTrue(getResult.Value.ToString().Contains("updated"));

        }

        [Test]
        public void PutEvent_UnixTimeStamp_String()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            var eventPosted = _orchestrate.PostEvent(CollectionName, "1", "comment", DateTime.UtcNow, item);
            var eventList = _orchestrate.ListEvents(CollectionName, "1", "comment", 100);
            var firstEvent = eventList.Results.First();

            item.Value = "This was updated";
            var itemString = JsonConvert.SerializeObject(item);

            var result = _orchestrate.PutEvent(CollectionName, "1", "comment", long.Parse(firstEvent.Path.TimeStamp), firstEvent.Path.Ordinal, itemString);

            Assert.IsTrue(!string.IsNullOrEmpty(result.Path.Ref));

            var getResult = _orchestrate.GetEvent(CollectionName, "1", "comment", Orchestrate.ConvertFromUnixTimeStamp(firstEvent.Path.TimeStamp), firstEvent.Path.Ordinal);

            Assert.IsTrue(getResult.Value.ToString().Contains("updated"));

        }

        [Test]
        public void PutEventAsync_UnixTimeStamp_String()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            var eventPosted = _orchestrate.PostEvent(CollectionName, "1", "comment", DateTime.UtcNow, item);
            var eventList = _orchestrate.ListEvents(CollectionName, "1", "comment", 100);
            var firstEvent = eventList.Results.First();

            item.Value = "This was updated";
            var itemString = JsonConvert.SerializeObject(item);

            var result = _orchestrate.PutEventAsync(CollectionName, "1", "comment", long.Parse(firstEvent.Path.TimeStamp), firstEvent.Path.Ordinal, itemString).Result;

            Assert.IsTrue(!string.IsNullOrEmpty(result.Path.Ref));

            var getResult = _orchestrate.GetEvent(CollectionName, "1", "comment", Orchestrate.ConvertFromUnixTimeStamp(firstEvent.Path.TimeStamp), firstEvent.Path.Ordinal);

            Assert.IsTrue(getResult.Value.ToString().Contains("updated"));

        }

        [Test]
        public void PutEvent_DateTime_Object_IfMatch()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            var eventPosted = _orchestrate.PostEvent(CollectionName, "1", "comment", DateTime.UtcNow, item);
            var eventList = _orchestrate.ListEvents(CollectionName, "1", "comment", 100);
            var firstEvent = eventList.Results.First();

            item.Value = "This was updated";

            var result = _orchestrate.PutEventIfMatch(CollectionName, "1", "comment", Orchestrate.ConvertFromUnixTimeStamp(firstEvent.Path.TimeStamp), firstEvent.Path.Ordinal, item, firstEvent.Path.Ref);

            Assert.IsTrue(!string.IsNullOrEmpty(result.Path.Ref));

            var getResult = _orchestrate.GetEvent(CollectionName, "1", "comment", Orchestrate.ConvertFromUnixTimeStamp(firstEvent.Path.TimeStamp), firstEvent.Path.Ordinal);

            Assert.IsTrue(getResult.Value.ToString().Contains("updated"));

        }

        [Test]
        public void PutEventAsync_DateTime_Object_IfMatch()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            var eventPosted = _orchestrate.PostEvent(CollectionName, "1", "comment", DateTime.UtcNow, item);
            var eventList = _orchestrate.ListEvents(CollectionName, "1", "comment", 100);
            var firstEvent = eventList.Results.First();

            item.Value = "This was updated";

            var result = _orchestrate.PutEventIfMatchAsync(CollectionName, "1", "comment", Orchestrate.ConvertFromUnixTimeStamp(firstEvent.Path.TimeStamp), firstEvent.Path.Ordinal, item, firstEvent.Path.Ref).Result;

            Assert.IsTrue(!string.IsNullOrEmpty(result.Path.Ref));

            var getResult = _orchestrate.GetEvent(CollectionName, "1", "comment", Orchestrate.ConvertFromUnixTimeStamp(firstEvent.Path.TimeStamp), firstEvent.Path.Ordinal);

            Assert.IsTrue(getResult.Value.ToString().Contains("updated"));

        }

        [Test]
        public void PutEvent_UnixTimeStamp_Object_IfMatch()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            var eventPosted = _orchestrate.PostEvent(CollectionName, "1", "comment", DateTime.UtcNow, item);
            var eventList = _orchestrate.ListEvents(CollectionName, "1", "comment", 100);
            var firstEvent = eventList.Results.First();

            item.Value = "This was updated";

            var result = _orchestrate.PutEventIfMatch(CollectionName, "1", "comment", long.Parse(firstEvent.Path.TimeStamp), firstEvent.Path.Ordinal, item, firstEvent.Path.Ref);

            Assert.IsTrue(!string.IsNullOrEmpty(result.Path.Ref));

            var getResult = _orchestrate.GetEvent(CollectionName, "1", "comment", Orchestrate.ConvertFromUnixTimeStamp(firstEvent.Path.TimeStamp), firstEvent.Path.Ordinal);

            Assert.IsTrue(getResult.Value.ToString().Contains("updated"));

        }

        [Test]
        public void PutEventAsync_UnixTimeStamp_Object_IfMatch()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            var eventPosted = _orchestrate.PostEvent(CollectionName, "1", "comment", DateTime.UtcNow, item);
            var eventList = _orchestrate.ListEvents(CollectionName, "1", "comment", 100);
            var firstEvent = eventList.Results.First();

            item.Value = "This was updated";

            var result = _orchestrate.PutEventIfMatchAsync(CollectionName, "1", "comment", long.Parse(firstEvent.Path.TimeStamp), firstEvent.Path.Ordinal, item, firstEvent.Path.Ref).Result;

            Assert.IsTrue(!string.IsNullOrEmpty(result.Path.Ref));

            var getResult = _orchestrate.GetEvent(CollectionName, "1", "comment", Orchestrate.ConvertFromUnixTimeStamp(firstEvent.Path.TimeStamp), firstEvent.Path.Ordinal);

            Assert.IsTrue(getResult.Value.ToString().Contains("updated"));

        }

        [Test]
        public void PutEvent_DateTime_String_IfMatch()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            var eventPosted = _orchestrate.PostEvent(CollectionName, "1", "comment", DateTime.UtcNow, item);
            var eventList = _orchestrate.ListEvents(CollectionName, "1", "comment", 100);
            var firstEvent = eventList.Results.First();

            item.Value = "This was updated";
            var itemString = JsonConvert.SerializeObject(item);

            var result = _orchestrate.PutEventIfMatch(CollectionName, "1", "comment", Orchestrate.ConvertFromUnixTimeStamp(firstEvent.Path.TimeStamp), firstEvent.Path.Ordinal, itemString, firstEvent.Path.Ref);

            Assert.IsTrue(!string.IsNullOrEmpty(result.Path.Ref));

            var getResult = _orchestrate.GetEvent(CollectionName, "1", "comment", Orchestrate.ConvertFromUnixTimeStamp(firstEvent.Path.TimeStamp), firstEvent.Path.Ordinal);

            Assert.IsTrue(getResult.Value.ToString().Contains("updated"));

        }

        [Test]
        public void PutEventAsync_DateTime_String_IfMatch()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            var eventPosted = _orchestrate.PostEvent(CollectionName, "1", "comment", DateTime.UtcNow, item);
            var eventList = _orchestrate.ListEvents(CollectionName, "1", "comment", 100);
            var firstEvent = eventList.Results.First();

            item.Value = "This was updated";
            var itemString = JsonConvert.SerializeObject(item);

            var result = _orchestrate.PutEventIfMatchAsync(CollectionName, "1", "comment", Orchestrate.ConvertFromUnixTimeStamp(firstEvent.Path.TimeStamp), firstEvent.Path.Ordinal, itemString, firstEvent.Path.Ref).Result;

            Assert.IsTrue(!string.IsNullOrEmpty(result.Path.Ref));

            var getResult = _orchestrate.GetEvent(CollectionName, "1", "comment", Orchestrate.ConvertFromUnixTimeStamp(firstEvent.Path.TimeStamp), firstEvent.Path.Ordinal);

            Assert.IsTrue(getResult.Value.ToString().Contains("updated"));

        }

        [Test]
        public void PutEvent_UnixTimeStamp_String_IfMatch()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            var eventPosted = _orchestrate.PostEvent(CollectionName, "1", "comment", DateTime.UtcNow, item);
            var eventList = _orchestrate.ListEvents(CollectionName, "1", "comment", 100);
            var firstEvent = eventList.Results.First();

            item.Value = "This was updated";
            var itemString = JsonConvert.SerializeObject(item);

            var result = _orchestrate.PutEventIfMatch(CollectionName, "1", "comment", long.Parse(firstEvent.Path.TimeStamp), firstEvent.Path.Ordinal, itemString, firstEvent.Path.Ref);

            Assert.IsTrue(!string.IsNullOrEmpty(result.Path.Ref));

            var getResult = _orchestrate.GetEvent(CollectionName, "1", "comment", Orchestrate.ConvertFromUnixTimeStamp(firstEvent.Path.TimeStamp), firstEvent.Path.Ordinal);

            Assert.IsTrue(getResult.Value.ToString().Contains("updated"));

        }

        [Test]
        public void PutEventAsync_UnixTimeStamp_String_IfMatch()
        {
            var item = new TestData { Id = 3, Value = "A successful object PUT" };
            var eventPosted = _orchestrate.PostEvent(CollectionName, "1", "comment", DateTime.UtcNow, item);
            var eventList = _orchestrate.ListEvents(CollectionName, "1", "comment", 100);
            var firstEvent = eventList.Results.First();

            item.Value = "This was updated";
            var itemString = JsonConvert.SerializeObject(item);

            var result = _orchestrate.PutEventIfMatchAsync(CollectionName, "1", "comment", long.Parse(firstEvent.Path.TimeStamp), firstEvent.Path.Ordinal, itemString, firstEvent.Path.Ref).Result;

            Assert.IsTrue(!string.IsNullOrEmpty(result.Path.Ref));

            var getResult = _orchestrate.GetEvent(CollectionName, "1", "comment", Orchestrate.ConvertFromUnixTimeStamp(firstEvent.Path.TimeStamp), firstEvent.Path.Ordinal);

            Assert.IsTrue(getResult.Value.ToString().Contains("updated"));

        }

        #endregion

        #region List Tests

        [Test]
        public void ListEvents()
        {
            var item1 = new TestData { Id = 3, Value = "A successful object Post 1" };
            var eventPosted1 = _orchestrate.PostEvent(CollectionName, "1", "comment", DateTime.UtcNow, item1);

            var item2 = new TestData { Id = 4, Value = "A successful object Post 2" };
            var eventPosted2 = _orchestrate.PostEvent(CollectionName, "1", "comment", DateTime.UtcNow, item2);

            var eventList = _orchestrate.ListEvents(CollectionName, "1", "comment", 100);

            Assert.IsTrue(eventList.Count > 0);
        }

        [Test]
        public void ListEventsAsync()
        {
            var item1 = new TestData { Id = 3, Value = "A successful object Post 1" };
            var eventPosted1 = _orchestrate.PostEvent(CollectionName, "1", "comment", DateTime.UtcNow, item1);

            var item2 = new TestData { Id = 4, Value = "A successful object Post 2" };
            var eventPosted2 = _orchestrate.PostEvent(CollectionName, "1", "comment", DateTime.UtcNow, item2);

            var eventList = _orchestrate.ListEventsAsync(CollectionName, "1", "comment", 100).Result;

            Assert.IsTrue(eventList.Count > 0);
        }

        [Test]
        public void ListEvents_DateTime()
        {
            var item1 = new TestData { Id = 3, Value = "A successful object Post 1" };
            var eventPosted1 = _orchestrate.PostEvent(CollectionName, "1", "comment", DateTime.UtcNow, item1);

            var item2 = new TestData { Id = 4, Value = "A successful object Post 2" };
            var eventPosted2 = _orchestrate.PostEvent(CollectionName, "1", "comment", DateTime.UtcNow, item2);

            var eventList = _orchestrate.ListEvents(CollectionName, "1", "comment", 100, startEvent: DateTime.UtcNow.AddDays(-1), endEvent: DateTime.UtcNow.AddDays(1));

            Assert.IsTrue(eventList.Count > 0);
        }

        [Test]
        public void ListEventsAsync_DateTime()
        {
            var item1 = new TestData { Id = 3, Value = "A successful object Post 1" };
            var eventPosted1 = _orchestrate.PostEvent(CollectionName, "1", "comment", DateTime.UtcNow, item1);

            var item2 = new TestData { Id = 4, Value = "A successful object Post 2" };
            var eventPosted2 = _orchestrate.PostEvent(CollectionName, "1", "comment", DateTime.UtcNow, item2);

            var eventList = _orchestrate.ListEventsAsync(CollectionName, "1", "comment", 100, afterEvent: DateTime.UtcNow.AddDays(-1), beforeEvent: DateTime.UtcNow.AddDays(1)).Result;

            Assert.IsTrue(eventList.Count > 0);
        }

        [Test]
        public void ListEvents_TimeStamp()
        {
            var item1 = new TestData { Id = 3, Value = "A successful object Post 1" };
            var eventPosted1 = _orchestrate.PostEvent(CollectionName, "1", "comment", DateTime.UtcNow, item1);

            var item2 = new TestData { Id = 4, Value = "A successful object Post 2" };
            var eventPosted2 = _orchestrate.PostEvent(CollectionName, "1", "comment", DateTime.UtcNow, item2);

            var eventList = _orchestrate.ListEvents(CollectionName, "1", "comment", 100, startEvent: Orchestrate.ConvertToUnixTimestamp(DateTime.UtcNow.AddDays(-1)), endEvent: Orchestrate.ConvertToUnixTimestamp(DateTime.UtcNow.AddDays(1)));

            Assert.IsTrue(eventList.Count > 0);
        }

        [Test]
        public void ListEventsAsync_TimeStamp()
        {
            var item1 = new TestData { Id = 3, Value = "A successful object Post 1" };
            var eventPosted1 = _orchestrate.PostEvent(CollectionName, "1", "comment", DateTime.UtcNow, item1);

            var item2 = new TestData { Id = 4, Value = "A successful object Post 2" };
            var eventPosted2 = _orchestrate.PostEvent(CollectionName, "1", "comment", DateTime.UtcNow, item2);

            var eventList = _orchestrate.ListEventsAsync(CollectionName, "1", "comment", 100, afterEvent: Orchestrate.ConvertToUnixTimestamp(DateTime.UtcNow.AddDays(-1)), beforeEvent: Orchestrate.ConvertToUnixTimestamp(DateTime.UtcNow.AddDays(1))).Result;

            Assert.IsTrue(eventList.Count > 0);
        }

        #endregion
    }
}