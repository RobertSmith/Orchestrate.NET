using System;
using System.Configuration;

namespace Orchestrate.Net.Tests.Helpers
{
	public static class TestHelper
	{
		private static string _apiKey;
		public static string ApiKey => _apiKey ?? 
		                               (
		                                _apiKey = Environment.GetEnvironmentVariable("OrchestrateApiKey") ?? 
		                                            ConfigurationManager.AppSettings["Orchestrate:ApiKey"]
		                                );
	}
}