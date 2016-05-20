using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using Orchestrate.Net.Tests.Helpers;

namespace Orchestrate.Net.Tests
{
    [TestFixture]
    public class AggregateTests
    {
        private const string CollectionName = "AggregatesTestCollection";
        private Orchestrate _orchestrate;

        [TestFixtureSetUp]
        public static void ClassInitialize()
        {
            var orchestrate = new Orchestrate(TestHelper.ApiKey);

            var itemList = new List<AggregateTestData>();

            itemList.Add(new AggregateTestData { Category = "TV", Manufacturer = "Sony", ItemName = "Sony TV - Gold", SaleCount = 1, SaleDate = DateTime.UtcNow.AddDays(-7), SalePrice = 399.95, Distance = new Location { Long = -122.674146, Lat =  45.843452 }});
            itemList.Add(new AggregateTestData { Category = "TV", Manufacturer = "Sony", ItemName = "Sony TV - Gold", SaleCount = 1, SaleDate = DateTime.UtcNow.AddDays(-6), SalePrice = 399.95, Distance = new Location { Long = -122.674146, Lat = 45.843452 }});
            itemList.Add(new AggregateTestData { Category = "TV", Manufacturer = "Sony", ItemName = "Sony TV - Gold", SaleCount = 2, SaleDate = DateTime.UtcNow.AddDays(-5), SalePrice = 349.95, Distance = new Location { Long = -122.674146, Lat = 45.843452 }});
            itemList.Add(new AggregateTestData { Category = "TV", Manufacturer = "Sony", ItemName = "Sony TV - Gold", SaleCount = 3, SaleDate = DateTime.UtcNow.AddDays(-4), SalePrice = 349.95, Distance = new Location { Long = -122.674146, Lat = 45.843452 }});
            itemList.Add(new AggregateTestData { Category = "TV", Manufacturer = "Sony", ItemName = "Sony TV - Gold", SaleCount = 3, SaleDate = DateTime.UtcNow.AddDays(-3), SalePrice = 349.95, Distance = new Location { Long = -122.674146, Lat = 45.843452 }});
            itemList.Add(new AggregateTestData { Category = "TV", Manufacturer = "Sony", ItemName = "Sony TV - Gold", SaleCount = 6, SaleDate = DateTime.UtcNow.AddDays(-2), SalePrice = 299.95, Distance = new Location { Long = -122.674146, Lat = 45.843452 }});
            itemList.Add(new AggregateTestData { Category = "TV", Manufacturer = "Sony", ItemName = "Sony TV - Gold", SaleCount = 5, SaleDate = DateTime.UtcNow.AddDays(-1), SalePrice = 299.95, Distance = new Location { Long = -122.674146, Lat = 45.843452 }});
            itemList.Add(new AggregateTestData { Category = "TV", Manufacturer = "Sony", ItemName = "Sony TV - Gold", SaleCount = 7, SaleDate = DateTime.UtcNow.AddDays(0), SalePrice = 299.95, Distance = new Location { Long = -122.674146, Lat = 45.843452 }});

            itemList.Add(new AggregateTestData { Category = "TV", Manufacturer = "Samsung", ItemName = "Samsung TV - Gold", SaleCount = 1, SaleDate = DateTime.UtcNow.AddDays(-7), SalePrice = 399.95, Distance = new Location { Long = -121.674146, Lat = 46.843452 }});
            itemList.Add(new AggregateTestData { Category = "TV", Manufacturer = "Samsung", ItemName = "Samsung TV - Gold", SaleCount = 1, SaleDate = DateTime.UtcNow.AddDays(-6), SalePrice = 399.95, Distance = new Location { Long = -121.674146, Lat = 46.843452 }});
            itemList.Add(new AggregateTestData { Category = "TV", Manufacturer = "Samsung", ItemName = "Samsung TV - Gold", SaleCount = 2, SaleDate = DateTime.UtcNow.AddDays(-5), SalePrice = 349.95, Distance = new Location { Long = -121.674146, Lat = 46.843452 }});
            itemList.Add(new AggregateTestData { Category = "TV", Manufacturer = "Samsung", ItemName = "Samsung TV - Gold", SaleCount = 3, SaleDate = DateTime.UtcNow.AddDays(-4), SalePrice = 349.95, Distance = new Location { Long = -121.674146, Lat = 46.843452 }});
            itemList.Add(new AggregateTestData { Category = "TV", Manufacturer = "Samsung", ItemName = "Samsung TV - Gold", SaleCount = 3, SaleDate = DateTime.UtcNow.AddDays(-3), SalePrice = 349.95, Distance = new Location { Long = -121.674146, Lat = 46.843452 }});
            itemList.Add(new AggregateTestData { Category = "TV", Manufacturer = "Samsung", ItemName = "Samsung TV - Gold", SaleCount = 6, SaleDate = DateTime.UtcNow.AddDays(-2), SalePrice = 299.95, Distance = new Location { Long = -121.674146, Lat = 46.843452 }});
            itemList.Add(new AggregateTestData { Category = "TV", Manufacturer = "Samsung", ItemName = "Samsung TV - Gold", SaleCount = 5, SaleDate = DateTime.UtcNow.AddDays(-1), SalePrice = 299.95, Distance = new Location { Long = -121.674146, Lat = 46.843452 }});
            itemList.Add(new AggregateTestData { Category = "TV", Manufacturer = "Samsung", ItemName = "Samsung TV - Gold", SaleCount = 7, SaleDate = DateTime.UtcNow.AddDays(0), SalePrice = 299.95, Distance = new Location { Long = -121.674146, Lat = 46.843452 }});

            itemList.Add(new AggregateTestData { Category = "TV", Manufacturer = "Samsung", ItemName = "Samsung TV - Silver", SaleCount = 11, SaleDate = DateTime.UtcNow.AddDays(-7), SalePrice = 199.95, Distance = new Location { Long = -120.674146, Lat = 47.843452 }});
            itemList.Add(new AggregateTestData { Category = "TV", Manufacturer = "Samsung", ItemName = "Samsung TV - Silver", SaleCount = 11, SaleDate = DateTime.UtcNow.AddDays(-6), SalePrice = 199.95, Distance = new Location { Long = -120.674146, Lat = 47.843452 }});
            itemList.Add(new AggregateTestData { Category = "TV", Manufacturer = "Samsung", ItemName = "Samsung TV - Silver", SaleCount = 12, SaleDate = DateTime.UtcNow.AddDays(-5), SalePrice = 149.95, Distance = new Location { Long = -120.674146, Lat = 47.843452 }});
            itemList.Add(new AggregateTestData { Category = "TV", Manufacturer = "Samsung", ItemName = "Samsung TV - Silver", SaleCount = 13, SaleDate = DateTime.UtcNow.AddDays(-4), SalePrice = 149.95, Distance = new Location { Long = -120.674146, Lat = 47.843452 }});
            itemList.Add(new AggregateTestData { Category = "TV", Manufacturer = "Samsung", ItemName = "Samsung TV - Silver", SaleCount = 13, SaleDate = DateTime.UtcNow.AddDays(-3), SalePrice = 149.95, Distance = new Location { Long = -120.674146, Lat = 47.843452 }});
            itemList.Add(new AggregateTestData { Category = "TV", Manufacturer = "Samsung", ItemName = "Samsung TV - Silver", SaleCount = 16, SaleDate = DateTime.UtcNow.AddDays(-2), SalePrice = 99.95, Distance = new Location { Long = -120.674146, Lat = 47.843452 }});
            itemList.Add(new AggregateTestData { Category = "TV", Manufacturer = "Samsung", ItemName = "Samsung TV - Silver", SaleCount = 15, SaleDate = DateTime.UtcNow.AddDays(-1), SalePrice = 99.95, Distance = new Location { Long = -120.674146, Lat = 47.843452 }});
            itemList.Add(new AggregateTestData { Category = "TV", Manufacturer = "Samsung", ItemName = "Samsung TV - Silver", SaleCount = 17, SaleDate = DateTime.UtcNow.AddDays(0), SalePrice = 99.95, Distance = new Location { Long = -120.674146, Lat = 47.843452 }});

            itemList.Add(new AggregateTestData { Category = "Computer", Manufacturer = "Dell", ItemName = "DXV - 9000", SaleCount = 4, SaleDate = DateTime.UtcNow.AddDays(-7), SalePrice = 1199.95, Distance = new Location { Long = -119, Lat = 48.843452 }});
            itemList.Add(new AggregateTestData { Category = "Computer", Manufacturer = "Dell", ItemName = "DXV - 9000", SaleCount = 3, SaleDate = DateTime.UtcNow.AddDays(-6), SalePrice = 1199.95, Distance = new Location { Long = -119, Lat = 48.843452 }});
            itemList.Add(new AggregateTestData { Category = "Computer", Manufacturer = "Dell", ItemName = "DXV - 9000", SaleCount = 2, SaleDate = DateTime.UtcNow.AddDays(-5), SalePrice = 1149.95, Distance = new Location { Long = -119, Lat = 48.843452 }});
            itemList.Add(new AggregateTestData { Category = "Computer", Manufacturer = "Dell", ItemName = "DXV - 9000", SaleCount = 1, SaleDate = DateTime.UtcNow.AddDays(-4), SalePrice = 1149.95, Distance = new Location { Long = -119, Lat = 48.843452 }});
            itemList.Add(new AggregateTestData { Category = "Computer", Manufacturer = "Dell", ItemName = "DXV - 9000", SaleCount = 1, SaleDate = DateTime.UtcNow.AddDays(-3), SalePrice = 1149.95, Distance = new Location { Long = -119, Lat = 48.843452 }});
            itemList.Add(new AggregateTestData { Category = "Computer", Manufacturer = "Dell", ItemName = "DXV - 9000", SaleCount = 0, SaleDate = DateTime.UtcNow.AddDays(-2), SalePrice = 1099.95, Distance = new Location { Long = -119, Lat = 48.843452 }});
            itemList.Add(new AggregateTestData { Category = "Computer", Manufacturer = "Dell", ItemName = "DXV - 9000", SaleCount = 3, SaleDate = DateTime.UtcNow.AddDays(-1), SalePrice = 1099.95, Distance = new Location { Long = -119, Lat = 48.843452 }});
            itemList.Add(new AggregateTestData { Category = "Computer", Manufacturer = "Dell", ItemName = "DXV - 9000", SaleCount = 7, SaleDate = DateTime.UtcNow.AddDays(0), SalePrice = 999.95, Distance = new Location { Long = -119, Lat = 48.843452 }});

            foreach (var item in itemList)
            {
                orchestrate.Post(CollectionName, item);
            }

            // need to let Orchestrate get all of these in before starting to test.
            Thread.Sleep(5000);
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

        #region Aggregate Tests

        [Test]
        public void TopValues()
        {
            var result = _orchestrate.Aggregate(CollectionName, "*", "value.Category:top_values");

            Assert.IsTrue(result.Aggregates.First().AggregateKind == "top_values");
            Assert.IsTrue(result.Aggregates.First().FieldName == "value.Category");
            Assert.IsTrue(result.Aggregates.First().Entries.First(x => x.Value == "Computer").Count == 8);
            Assert.IsTrue(result.Aggregates.First().Entries.First(x => x.Value == "TV").Count == 24);
        }

        [Test]
        public void TopValuesAsync()
        {
            var result = _orchestrate.AggregateAsync(CollectionName, "*", "value.Category:top_values").Result;

            Assert.IsTrue(result.Aggregates.First().AggregateKind == "top_values");
            Assert.IsTrue(result.Aggregates.First().FieldName == "value.Category");
            Assert.IsTrue(result.Aggregates.First().Entries.First(x => x.Value == "Computer").Count == 8);
            Assert.IsTrue(result.Aggregates.First().Entries.First(x => x.Value == "TV").Count == 24);
        }

        [Test]
        public void Stats()
        {
            var result = _orchestrate.Aggregate(CollectionName, "*", "value.SalePrice:stats");

            Assert.IsTrue(result.Aggregates.First().AggregateKind == "stats");
            Assert.IsTrue(result.Aggregates.First().FieldName == "value.SalePrice");
            Assert.IsTrue(Math.Abs(result.Aggregates.First().Statistics.Min - 99.95) < .001);
            Assert.IsTrue(Math.Abs(result.Aggregates.First().Statistics.Max - 1199.95) < .001);
        }

        [Test]
        public void StatsAsync()
        {
            var result = _orchestrate.AggregateAsync(CollectionName, "*", "value.SalePrice:stats").Result;

            Assert.IsTrue(result.Aggregates.First().AggregateKind == "stats");
            Assert.IsTrue(result.Aggregates.First().FieldName == "value.SalePrice");
            Assert.IsTrue(Math.Abs(result.Aggregates.First().Statistics.Min - 99.95) < .001);
            Assert.IsTrue(Math.Abs(result.Aggregates.First().Statistics.Max - 1199.95) < .001);
        }

        [Test]
        public void Range()
        {
            var result = _orchestrate.Aggregate(CollectionName, "*", "value.SalePrice:range:*~100:100~200:200~300:300~*");

            Assert.IsTrue(result.Aggregates.First().AggregateKind == "range");
            Assert.IsTrue(result.Aggregates.First().FieldName == "value.SalePrice");
            Assert.IsTrue(result.Aggregates.First().Buckets.Count() == 4);
        }

        [Test]
        public void RangeAsync()
        {
            var result = _orchestrate.AggregateAsync(CollectionName, "*", "value.SalePrice:range:*~100:100~200:200~300:300~*").Result;

            Assert.IsTrue(result.Aggregates.First().AggregateKind == "range");
            Assert.IsTrue(result.Aggregates.First().FieldName == "value.SalePrice");
            Assert.IsTrue(result.Aggregates.First().Buckets.Count() == 4);
        }

        [Test]
        public void Distance()
        {
            var result = _orchestrate.Aggregate(CollectionName, "value.Location:NEAR:{lat:47.843452 lon:-120.674146 dist:400km}", "value.Location:distance:*~100:100~*");

            Assert.IsTrue(result.Aggregates.First().AggregateKind == "distance");
            Assert.IsTrue(result.Aggregates.First().FieldName == "value.Location");
            Assert.IsTrue(result.Aggregates.First().Buckets.Count() == 2);
        }

        [Test]
        public void DistanceAsync()
        {
            var result = _orchestrate.AggregateAsync(CollectionName, "value.Location:NEAR:{lat:47.843452 lon:-120.674146 dist:400km}", "value.Location:distance:*~200:200~*").Result;

            Assert.IsTrue(result.Aggregates.First().AggregateKind == "distance");
            Assert.IsTrue(result.Aggregates.First().FieldName == "value.Location");
            Assert.IsTrue(result.Aggregates.First().Buckets.Count() == 2);
        }

        [Test]
        public void TimeSeries()
        {
            var result = _orchestrate.Aggregate(CollectionName, "*", "value.SaleDate:time_series:day");

            Assert.IsTrue(result.Aggregates.First().AggregateKind == "time_series");
            Assert.IsTrue(result.Aggregates.First().FieldName == "value.SaleDate");
            Assert.IsTrue(result.Aggregates.First().Buckets.Count() == 8);
        }

        [Test]
        public void TimeSeriesAsync()
        {
            var result = _orchestrate.AggregateAsync(CollectionName, "*", "value.SaleDate:time_series:day").Result;

            Assert.IsTrue(result.Aggregates.First().AggregateKind == "time_series");
            Assert.IsTrue(result.Aggregates.First().FieldName == "value.SaleDate");
            Assert.IsTrue(result.Aggregates.First().Buckets.Count() == 8);
        }

        #endregion
    }
}