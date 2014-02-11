using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Orchestrate.Net.Test
{
    [TestClass]
    public class ConnectionTests
    {
        const string CollectionName = "TestCollection";
        const string ApiKey = "0ad9fa6a-96cd-44fd-818e-061fe40fca2d";

        static readonly object TurnStile = new object();
        static Orchestrate _orchestration;

        [TestInitialize]
        public void Initialize()
        {
            _orchestration = new Orchestrate(ApiKey);
        }

        [TestMethod]
        public void A01_CreateCollection()
        {
            lock (TurnStile)
            {
                var item = new TestData {Id = 1, Value = "Test Value 1"};

                var result = _orchestration.CreateCollection(CollectionName, "1", item);

                Assert.IsTrue(result.Path.Ref.Length > 0);
            }
        }

        [TestMethod]
        public void A02_CreateCollectionFail()
        {
            lock (TurnStile)
            {
                try
                {
                    _orchestration.CreateCollection(CollectionName + "_Fail", "1", null);
                }
                catch (ArgumentNullException ex)
                {
                    Assert.IsTrue(ex.Message.Contains("json cannot be empty"));
                }
            }
        }

        [TestMethod]
        public void B01_CollectionGetExists()
        {
            lock (TurnStile)
            {
                var result = _orchestration.Get(CollectionName, "1");

                Assert.IsTrue(result.Value != null);
            }
        }

        [TestMethod]
        public void B02_CollectionGetDoesNotExist()
        {
            lock (TurnStile)
            {
                try
                {
                    _orchestration.Get(CollectionName, "9999");
                }
                catch (Exception ex)
                {
                    Assert.IsTrue(ex.Message.Contains("404"));
                }
            }
        }

        [TestMethod]
        public void B03_CollectionGetByRef()
        {
            lock (TurnStile)
            {
                var list = _orchestration.List(CollectionName, 10, null, null);
                var match = list.Results.First();
                var result = _orchestration.Get(CollectionName, "1", match.Path.Ref);

                Assert.IsTrue(result.Value != null);
            }
        }

        [TestMethod]
        public void C01_CollectionPut()
        {
            lock (TurnStile)
            {
                var item = new TestData {Id = 2, Value = "Test Value 2"};

                var result = _orchestration.Put(CollectionName, "2", item);

                Assert.IsTrue(result.Path.Ref.Length > 0);
            }
        }

        [TestMethod]
        public void C02_CollectionPutIfMatchSuccess()
        {
            lock (TurnStile)
            {
                var match = _orchestration.Get(CollectionName, "2");
                var item = new TestData {Id = 2, Value = "Test Value 2a"};

                var result = _orchestration.PutIfMatch(CollectionName, "2", item, match.Path.Ref);

                Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);
            }
        }

        [TestMethod]
        public void C03_CollectionPutIfMatchFail()
        {
            lock (TurnStile)
            {
                var match = _orchestration.Get(CollectionName, "1");
                var item = new TestData {Id = 2, Value = "Test Value 2b"};

                try
                {
                    _orchestration.PutIfMatch(CollectionName, "2", item, match.Path.Ref);
                }
                catch (Exception ex)
                {
                    Assert.IsTrue(ex.Message.Contains("412"));
                }
            }
        }

        [TestMethod]
        public void C04_CollectionPutIfNoneMatchSucess()
        {
            lock (TurnStile)
            {
                var item = new TestData {Id = 3, Value = "Test Value 3"};

                var result = _orchestration.PutIfNoneMatch(CollectionName, "3", item);

                Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);
            }
        }

        [TestMethod]
        public void C05_CollectionPutIfNoneMatchFail()
        {
            lock (TurnStile)
            {
                var item = new TestData {Id = 3, Value = "Test Value 3a"};

                try
                {
                    _orchestration.PutIfNoneMatch(CollectionName, "3", item);
                }
                catch (Exception ex)
                {
                    Assert.IsTrue(ex.Message.Contains("412"));
                }
            }
        }

        [TestMethod]
        public void D01_CollectionDeleteSuccess()
        {
            lock (TurnStile)
            {
                var result = _orchestration.Delete(CollectionName, "3");
                Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);
            }
        }

        [TestMethod]
        public void D02_CollectionDeleteNotFound()
        {
            lock (TurnStile)
            {
                var result = _orchestration.Delete(CollectionName, "9999");
                Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);
            }
        }

        [TestMethod]
        public void D03_CollectionDeleteIfMatchFail()
        {
            lock (TurnStile)
            {
                var match = _orchestration.Get(CollectionName, "1");
                try
                {
                    _orchestration.DeleteIfMatch(CollectionName, "2", match.Path.Ref);
                }
                catch (Exception ex)
                {
                    Assert.IsTrue(ex.Message.Contains("412"));
                }
            }
        }

        [TestMethod]
        public void D04_CollectionDeleteIfMatchSucced()
        {
            lock (TurnStile)
            {
                var match = _orchestration.Get(CollectionName, "2");
                var result = _orchestration.DeleteIfMatch(CollectionName, "2", match.Path.Ref);
                Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);
            }
        }

        [TestMethod]
        public void E01_CollectionList()
        {
            lock (TurnStile)
            {
                var result = _orchestration.List(CollectionName, 10, null, null);
                Assert.IsTrue(result.Count > 0);
            }
        }

        [TestMethod]
        public void E02_CollectionListWithStartKey()
        {
            lock (TurnStile)
            {
                var result = _orchestration.List(CollectionName, 10, "1", null);
                Assert.IsTrue(result.Count > 0);
            }
        }

        [TestMethod]
        public void E03_CollectionListWithAfterKey()
        {
            lock (TurnStile)
            {
                var result = _orchestration.List(CollectionName, 10, null, "1");
                Assert.IsTrue(result.Count == 0);
            }
        }

        [TestMethod]
        public void E04_CollectionListInvlaidLimit()
        {
            lock (TurnStile)
            {
                try
                {
                    _orchestration.List(CollectionName, -10, null, null);
                }
                catch (Exception ex)
                {
                    Assert.IsTrue(ex.Message.Contains("limit"));
                }
            }
        }

        [TestMethod]
        public void E05_CollectionListStartAndAfter()
        {
            lock (TurnStile)
            {
                try
                {
                    _orchestration.List(CollectionName, 10, "start", "after");
                }
                catch (Exception ex)
                {
                    Assert.IsTrue(ex.Message.Contains("startKey"));
                }
            }
        }

        [TestMethod]
        public void G01_SearchCollectionSuccess()
        {
            lock (TurnStile)
            {
                var result = _orchestration.Search(CollectionName, "*", 10, 0);

                Assert.IsTrue(result.Count > 0);
            }
        }

        [TestMethod]
        public void G02_SearchCollectionNotFound()
        {
            lock (TurnStile)
            {
                var result = _orchestration.Search(CollectionName, "Id:9999", 10, 0);

                Assert.IsTrue(result.Count == 0);
            }
        }

        [TestMethod]
        public void G03_SearchCollectionBadKey()
        {
            lock (TurnStile)
            {
                var result = _orchestration.Search(CollectionName, "NonExistantKey:9999", 10, 0);

                Assert.IsTrue(result.Count == 0);
            }
        }

        [TestMethod]
        public void H01_CollectionAddEventNoTimeStamp()
        {
            lock (TurnStile)
            {
                var result = _orchestration.PutEvent(CollectionName, "1", "comment", null, "This is the first comment.");

                Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);
            }
        }

        [TestMethod]
        public void H02_CollectionAddEventWithTimeStamp()
        {
            lock (TurnStile)
            {
                var result = _orchestration.PutEvent(CollectionName, "1", "comment", DateTime.UtcNow.AddDays(1), "This is the second comment.");

                Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);
            }
        }

        [TestMethod]
        public void H03_CollectionGetEventsNoStartEnd()
        {
            lock (TurnStile)
            {
                var result = _orchestration.GetEvents(CollectionName, "1", "comment", null, null);

                Assert.IsTrue(result.Count == 2);
            }
        }

        [TestMethod]
        public void H04_CollectionGetEventsWithStartAndEndDate()
        {
            lock (TurnStile)
            {
                var result = _orchestration.GetEvents(CollectionName, "1", "comment", DateTime.UtcNow.AddHours(-1), DateTime.UtcNow.AddHours(1));

                Assert.IsTrue(result.Count == 1);
            }
        }

        [TestMethod]
        public void I01_CollectionAddGraph()
        {
            lock (TurnStile)
            {
                var data = new TestData {Id = 99, Value = "Olaf"};
                _orchestration.CreateCollection("Snowmen", "99", data);
                var result = _orchestration.PutGraph(CollectionName, "1", "snowman", "Snowmen", "99");

                Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);
            }
        }

        [TestMethod]
        public void I02_CollectionGetSingleGraph()
        {
            lock (TurnStile)
            {
                var kinds = new[]{ "snowman" };
                var result = _orchestration.GetGraph(CollectionName, "1", kinds);

                Assert.IsTrue(result.Count == 1);
            }
        }

        [TestMethod]
        public void I03_CollectionGetMultipleGraph()
        {
            lock (TurnStile)
            {
                var data = new TestData { Id = 66, Value = "Sven" };
                _orchestration.CreateCollection("IceMerchants", "66", data);
                _orchestration.PutGraph("Snowmen", "99", "icemerchant", "IceMerchants", "66");
                var kinds = new[] { "snowman", "icemerchant" };
                
                var result = _orchestration.GetGraph(CollectionName, "1", kinds);

                Assert.IsTrue(result.Count == 1);
            }
        }

        [TestMethod]
        public void Z01_DeleteCollection()
        {
            lock (TurnStile)
            {
                var result = _orchestration.DeleteCollection(CollectionName);

                Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);
            }
        }

        [TestMethod]
        public void Z02_DeleteNonExistantCollection()
        {
            lock (TurnStile)
            {
                var result = _orchestration.DeleteCollection("ThisCollectionDoesNotExist");

                Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);
            }
        }

        [TestMethod]
        public void Z03_DeleteGraphCollections()
        {
            lock (TurnStile)
            {
                _orchestration.DeleteCollection("Snowmen");
                var result = _orchestration.DeleteCollection("IceMerchants");

                Assert.IsTrue(result.Value == null || result.Value.ToString() == string.Empty);
            }
        }
    }

    public class TestData
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}
