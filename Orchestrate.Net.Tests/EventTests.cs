using System;
using System.Linq;
using NUnit.Framework;
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
		public void PutEventNowTimeStamp()
		{
			var result = _orchestrate.PutEvent(CollectionName, "1", "comment", DateTime.UtcNow, "This is the PutEventNowTimeStamp comment.");

			Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);
		}

        [Test]
        public void PutEventNowTimeStampAsync()
        {
            var result = _orchestrate.PutEventAsync(CollectionName, "1", "comment", DateTime.UtcNow, "This is the PutEventNowTimeStamp comment.").Result;

            Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);
        }

        [Test]
		public void PutEventNoTimeStamp()
		{
			var result = _orchestrate.PutEvent(CollectionName, "1", "comment", null, "This is the PutEventNoTimeStamp comment.");

			Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);
		}

        [Test]
        public void PutEventNoTimeStampAsync()
        {
            var result = _orchestrate.PutEventAsync(CollectionName, "1", "comment", null, "This is the PutEventNoTimeStamp comment.").Result;

            Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);
        }

        [Test]
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

        [Test]
        public void PutEventWithNoCollectionNameAsync()
        {
            try
            {
                var result = _orchestrate.PutEventAsync(string.Empty, "1", "comment", null, "This is the PutEventWithNoCollectionName comment.").Result;
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
				_orchestrate.PutEvent(CollectionName, string.Empty, "comment", null, "This is the PutEventWithNoKey comment.");
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
                var result = _orchestrate.PutEventAsync(CollectionName, string.Empty, "comment", null, "This is the PutEventWithNoKey comment.").Result;
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
				_orchestrate.PutEvent(CollectionName, "1", string.Empty, null, "This is the PutEventWithNoType comment.");
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
                var result = _orchestrate.PutEventAsync(CollectionName, "1", string.Empty, null, "This is the PutEventWithNoType comment.").Result;
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
			_orchestrate.PutEvent(CollectionName, "1", "comment", DateTime.UtcNow, "This is the GetEventsNoStartEnd comment.");
			var result = _orchestrate.GetEvents(CollectionName, "1", "comment");

			Assert.IsTrue(result.Count > 0);
		}

        [Test]
        public void GetEventsNoStartEndAsync()
        {
            _orchestrate.PutEvent(CollectionName, "1", "comment", DateTime.UtcNow, "This is the GetEventsNoStartEnd comment.");
            var result = _orchestrate.GetEventsAsync(CollectionName, "1", "comment").Result;

            Assert.IsTrue(result.Count > 0);
        }

        [Test]
		public void GetEventsWithStartDate()
		{
			_orchestrate.PutEvent(CollectionName, "1", "comment", DateTime.UtcNow, "This is the GetEventsWithStartDate comment.");
			var result = _orchestrate.GetEvents(CollectionName, "1", "comment", DateTime.UtcNow.AddHours(-1));

			Assert.IsTrue(result.Count > 0);
		}

        [Test]
        public void GetEventsWithStartDateAsync()
        {
            _orchestrate.PutEvent(CollectionName, "1", "comment", DateTime.UtcNow, "This is the GetEventsWithStartDate comment.");
            var result = _orchestrate.GetEventsAsync(CollectionName, "1", "comment", DateTime.UtcNow.AddHours(-1)).Result;

            Assert.IsTrue(result.Count > 0);
        }

        [Test]
		public void GetEventsWithEndDate()
		{
			_orchestrate.PutEvent(CollectionName, "1", "comment", DateTime.UtcNow, "This is the GetEventsWithEndDate comment.");
			var result = _orchestrate.GetEvents(CollectionName, "1", "comment", null, DateTime.UtcNow.AddHours(1));

			Assert.IsTrue(result.Count > 0);
		}

        [Test]
        public void GetEventsWithEndDateAsync()
        {
            _orchestrate.PutEvent(CollectionName, "1", "comment", DateTime.UtcNow, "This is the GetEventsWithEndDate comment.");
            var result = _orchestrate.GetEventsAsync(CollectionName, "1", "comment", null, DateTime.UtcNow.AddHours(1)).Result;

            Assert.IsTrue(result.Count > 0);
        }

        [Test]
		public void GetEventsWithStartAndEndDate()
		{
			_orchestrate.PutEvent(CollectionName, "1", "comment", DateTime.UtcNow, "This is the GetEventsWithStartAndEndDate comment.");
			var result = _orchestrate.GetEvents(CollectionName, "1", "comment", DateTime.UtcNow.AddHours(-1), DateTime.UtcNow.AddHours(1));

			Assert.IsTrue(result.Count > 0);
		}

        [Test]
        public void GetEventsWithStartAndEndDateAsync()
        {
            _orchestrate.PutEvent(CollectionName, "1", "comment", DateTime.UtcNow, "This is the GetEventsWithStartAndEndDate comment.");
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