using System.Configuration;

namespace Orchestrate.Net.Tests.Helpers
{
	public static class TestHelper
	{
		private static string apiKey;
		public static string ApiKey
		{
			get
			{
				return apiKey ?? (apiKey = ConfigurationManager.AppSettings["Orchestrate:ApiKey"]);
			}
		}
	}
}