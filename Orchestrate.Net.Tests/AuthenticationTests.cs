using System;
using NUnit.Framework;
using Orchestrate.Net.Tests.Helpers;

namespace Orchestrate.Net.Tests
{
    [TestFixture]
    public class AuthenticationTests
    {
        [Test]
        public void AuthenticateKey()
        {
            // Set up
            var orchestration = new Orchestrate(TestHelper.ApiKey);

            try
            {
                var result = orchestration.Authenticate(TestHelper.ApiKey);

                Assert.IsTrue(result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void AuthenticateKeyAsync()
        {
            // Set up
            var orchestration = new Orchestrate(TestHelper.ApiKey);

            try
            {
                var result = orchestration.AuthenticateAsync(TestHelper.ApiKey);

                Assert.IsTrue(result.Result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
