using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Orchestrate.Net.Test
{
    [TestClass]
    public class EventTests
    {
        const string ApiKey = "<API KEY>";
        private const string CollectionName = "EventTestCollection";
        private Orchestrate _orchestrate;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var orchestrate = new Orchestrate(ApiKey);
            var item = new TestData { Id = 1, Value = "Inital Test Item" };

            orchestrate.CreateCollection(CollectionName, "1", item);
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

        [TestMethod]
        public void PutEventNowTimeStamp()
        {
            var result = _orchestrate.PutEvent(CollectionName, "1", "comment", DateTime.UtcNow, "This is the PutEventNowTimeStamp comment.");

            Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);
        }

        [TestMethod]
        public void PutEventNoTimeStamp()
        {
            var result = _orchestrate.PutEvent(CollectionName, "1", "comment", null, "This is the PutEventNoTimeStamp comment.");

            Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);
        }

        [TestMethod]
        public void PutEventWithNoCollectionName()
        {
            try
            {
                _orchestrate.PutEvent(string.Empty, "1", "comment", null, "This is the PutEventWithNoCollectionName comment.");
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "collectionName");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [TestMethod]
        public void PutEventWithNoKey()
        {
            try
            {
                _orchestrate.PutEvent(CollectionName, string.Empty, "comment", null, "This is the PutEventWithNoKey comment.");
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "key");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [TestMethod]
        public void PutEventWithNoType()
        {
            try
            {
                _orchestrate.PutEvent(CollectionName, "1", string.Empty, null, "This is the PutEventWithNoType comment.");
            }
            catch (ArgumentNullException ex)
            {
                Assert.IsTrue(ex.ParamName == "type");
                return;
            }

            Assert.Fail("No Exception Thrown");
        }

        [TestMethod]
        public void GetEventsNoStartEnd()
        {
            _orchestrate.PutEvent(CollectionName, "1", "comment", DateTime.UtcNow, "This is the GetEventsNoStartEnd comment."); 
            var result = _orchestrate.GetEvents(CollectionName, "1", "comment");

            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void GetEventsWithStartDate()
        {
            _orchestrate.PutEvent(CollectionName, "1", "comment", DateTime.UtcNow, "This is the GetEventsWithStartDate comment.");
            var result = _orchestrate.GetEvents(CollectionName, "1", "comment", DateTime.UtcNow.AddHours(-1));

            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void GetEventsWithEndDate()
        {
            _orchestrate.PutEvent(CollectionName, "1", "comment", DateTime.UtcNow, "This is the GetEventsWithEndDate comment.");
            var result = _orchestrate.GetEvents(CollectionName, "1", "comment", null, DateTime.UtcNow.AddHours(1));

            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void GetEventsWithStartAndEndDate()
        {
            _orchestrate.PutEvent(CollectionName, "1", "comment", DateTime.UtcNow, "This is the GetEventsWithStartAndEndDate comment.");
            var result = _orchestrate.GetEvents(CollectionName, "1", "comment", DateTime.UtcNow.AddHours(-1), DateTime.UtcNow.AddHours(1));

            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

    }
}
