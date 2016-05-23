using System.Linq;
using NUnit.Framework;
using Orchestrate.Net.Tests.Helpers;

namespace Orchestrate.Net.Tests
{
    [TestFixture]
    public class BulkOperationTests
    {
        private Orchestrate _orchestrate;

        [TestFixtureSetUp]
        public static void ClassInitialize()
        {
        }

        [TestFixtureTearDown]
        public static void ClassCleanUp()
        {
        }

        [SetUp]
        public void TestInitialize()
        {
            _orchestrate = new Orchestrate(TestHelper.ApiKey);
        }

        [TearDown]
        public void TestCleanup()
        {
            _orchestrate.DeleteCollection("user");
        }

        [Test]
        public void BulkInsert_String_Stream()
        {
            var payload = "{\r\n    \"path\" : {\r\n      \"kind\" : \"item\",\r\n      \"collection\" : \"user\",\r\n      \"key\" : \"johnny\"\r\n    },\r\n    \"value\" : {\r\n      \"name\" : \"John Jones\"\r\n    }\r\n  }{\r\n    \"path\" : {\r\n      \"kind\" : \"item\",\r\n      \"collection\" : \"user\",\r\n      \"key\" : \"jenny\"\r\n    },\r\n    \"value\" : {\r\n      \"name\" : \"Jennifer Smith\"\r\n    }\r\n  }{\r\n    \"path\" : {\r\n      \"kind\" : \"event\",\r\n      \"collection\" : \"user\",\r\n      \"key\" : \"johnny\",\r\n      \"type\" : \"status-update\",\r\n      \"timestamp\" : \"2015-05-14T12:12:12.123+00:00\"\r\n    },\r\n    \"value\" : {\r\n      \"message\" : \"Orchestrate rules!\"\r\n    }\r\n  }{\r\n    \"kind\" : \"relationship\",\r\n    \"source\" : {\r\n      \"collection\" : \"user\",\r\n      \"key\" : \"johnny\"\r\n    },\r\n    \"destination\" : {\r\n      \"collection\" : \"user\",\r\n      \"key\" : \"jenny\"\r\n    },\r\n    \"relation\" : \"friend\"\r\n  }";

            var result = _orchestrate.BulkOperation(string.Empty, payload, true);

            Assert.IsTrue(result != null);
            Assert.IsTrue(result.Status == "success");
            Assert.IsTrue(result.Results.Count() == 4);
        }

        [Test]
        public void BulkInsertAsync_String_Stream()
        {
            var payload = "{\r\n    \"path\" : {\r\n      \"kind\" : \"item\",\r\n      \"collection\" : \"user\",\r\n      \"key\" : \"johnny\"\r\n    },\r\n    \"value\" : {\r\n      \"name\" : \"John Jones\"\r\n    }\r\n  }{\r\n    \"path\" : {\r\n      \"kind\" : \"item\",\r\n      \"collection\" : \"user\",\r\n      \"key\" : \"jenny\"\r\n    },\r\n    \"value\" : {\r\n      \"name\" : \"Jennifer Smith\"\r\n    }\r\n  }{\r\n    \"path\" : {\r\n      \"kind\" : \"event\",\r\n      \"collection\" : \"user\",\r\n      \"key\" : \"johnny\",\r\n      \"type\" : \"status-update\",\r\n      \"timestamp\" : \"2015-05-14T12:12:12.123+00:00\"\r\n    },\r\n    \"value\" : {\r\n      \"message\" : \"Orchestrate rules!\"\r\n    }\r\n  }{\r\n    \"kind\" : \"relationship\",\r\n    \"source\" : {\r\n      \"collection\" : \"user\",\r\n      \"key\" : \"johnny\"\r\n    },\r\n    \"destination\" : {\r\n      \"collection\" : \"user\",\r\n      \"key\" : \"jenny\"\r\n    },\r\n    \"relation\" : \"friend\"\r\n  }";

            var result = _orchestrate.BulkOperationAsync(string.Empty, payload, true).Result;

            Assert.IsTrue(result != null);
            Assert.IsTrue(result.Status == "success");
            Assert.IsTrue(result.Results.Count() == 4);
        }

        [Test]
        public void BulkInsert_String_NoStream()
        {
            var payload = "[{\r\n    \"path\" : {\r\n      \"kind\" : \"item\",\r\n      \"collection\" : \"user\",\r\n      \"key\" : \"johnny\"\r\n    },\r\n    \"value\" : {\r\n      \"name\" : \"John Jones\"\r\n    }\r\n  },{\r\n    \"path\" : {\r\n      \"kind\" : \"item\",\r\n      \"collection\" : \"user\",\r\n      \"key\" : \"jenny\"\r\n    },\r\n    \"value\" : {\r\n      \"name\" : \"Jennifer Smith\"\r\n    }\r\n  },{\r\n    \"path\" : {\r\n      \"kind\" : \"event\",\r\n      \"collection\" : \"user\",\r\n      \"key\" : \"johnny\",\r\n      \"type\" : \"status-update\",\r\n      \"timestamp\" : \"2015-05-14T12:12:12.123+00:00\"\r\n    },\r\n    \"value\" : {\r\n      \"message\" : \"Orchestrate rules!\"\r\n    }\r\n  },{\r\n    \"kind\" : \"relationship\",\r\n    \"source\" : {\r\n      \"collection\" : \"user\",\r\n      \"key\" : \"johnny\"\r\n    },\r\n    \"destination\" : {\r\n      \"collection\" : \"user\",\r\n      \"key\" : \"jenny\"\r\n    },\r\n    \"relation\" : \"friend\"\r\n  }]";

            var result = _orchestrate.BulkOperation(string.Empty, payload);

            Assert.IsTrue(result != null);
            Assert.IsTrue(result.Status == "success");
            Assert.IsTrue(result.Results.Count() == 4);
        }

        [Test]
        public void BulkInsertAsync_String_NoStream()
        {
            var payload = "[{\r\n    \"path\" : {\r\n      \"kind\" : \"item\",\r\n      \"collection\" : \"user\",\r\n      \"key\" : \"johnny\"\r\n    },\r\n    \"value\" : {\r\n      \"name\" : \"John Jones\"\r\n    }\r\n  },{\r\n    \"path\" : {\r\n      \"kind\" : \"item\",\r\n      \"collection\" : \"user\",\r\n      \"key\" : \"jenny\"\r\n    },\r\n    \"value\" : {\r\n      \"name\" : \"Jennifer Smith\"\r\n    }\r\n  },{\r\n    \"path\" : {\r\n      \"kind\" : \"event\",\r\n      \"collection\" : \"user\",\r\n      \"key\" : \"johnny\",\r\n      \"type\" : \"status-update\",\r\n      \"timestamp\" : \"2015-05-14T12:12:12.123+00:00\"\r\n    },\r\n    \"value\" : {\r\n      \"message\" : \"Orchestrate rules!\"\r\n    }\r\n  },{\r\n    \"kind\" : \"relationship\",\r\n    \"source\" : {\r\n      \"collection\" : \"user\",\r\n      \"key\" : \"johnny\"\r\n    },\r\n    \"destination\" : {\r\n      \"collection\" : \"user\",\r\n      \"key\" : \"jenny\"\r\n    },\r\n    \"relation\" : \"friend\"\r\n  }]";

            var result = _orchestrate.BulkOperationAsync(string.Empty, payload).Result;

            Assert.IsTrue(result != null);
            Assert.IsTrue(result.Status == "success");
            Assert.IsTrue(result.Results.Count() == 4);
        }

        [Test]
        public void BulkInsert_String_Stream_One_Bad_Bit()
        {
            var payload = "{\r\n    \"path\" : {\r\n      \"kind\" : \"item\",\r\n      \"collection\" : \"user\",\r\n      \"key\" : \"johnny\"\r\n    },\r\n    \"value\" : {\r\n      \"name\" : \"John Jones\"\r\n    }\r\n  }{\r\n    \"path\" : {\r\n      \"kind\" : \"item\",\r\n      \"collection\" : \"user\",\r\n      \"key\" : \"jenny\"\r\n    },\r\n    \"value\" : {\r\n      \"name\" : \"Jennifer Smith\"\r\n    }\r\n  }{\r\n    \"path\" : {\r\n      \"kind\" : \"event\",\r\n      \"collection\" : \"user\",\r\n      \"key\" : \"johnny\",\r\n      \"type\" : \"status-update\",\r\n      \"timestamp\" : \"2015-05-14T12:12:12.123+00:00\"\r\n    },\r\n    \"value\" : {\r\n      \"message\" : \"Orchestrate rules!\"\r\n    }\r\n  }{\r\n    \"kind\" : \"relationship\",\r\n    \"source\" : {\r\n      \"collection\" : \"user\",\r\n      \"key\" : \"johnny\"\r\n    },\r\n    \"destination\" : {\r\n      \"collection\" : \"user\",\r\n      \"key\" : \"katelynn\"\r\n    },\r\n    \"relation\" : \"friend\"\r\n  }";

            var result = _orchestrate.BulkOperation(string.Empty, payload, true);

            Assert.IsTrue(result != null);
            Assert.IsTrue(result.Status == "partial");
            Assert.IsTrue(result.SuccessCount == 3);
            Assert.IsTrue(result.Results.Count() == 4);
        }
    }
}
