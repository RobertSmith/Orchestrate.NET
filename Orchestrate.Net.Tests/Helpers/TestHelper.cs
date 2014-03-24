using System.Configuration;

namespace Orchestrate.Net.Tests.Helpers
{
	public static class TestHelper
	{
		private static string _apiKey;
		public static string ApiKey
		{
			get
			{
				return _apiKey ?? (_apiKey = ConfigurationManager.AppSettings["Orchestrate:ApiKey"]);
			}
		}
	}
}