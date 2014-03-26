using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Orchestrate.Net
{
	internal static class HttpMethodExtensions
	{
		private static readonly IEnumerable<HttpMethod> NoBodyHttpMethods = new[]
		{
			//Not sure if this is the full list. Need to confirm with RFC.
			HttpMethod.Get,
			HttpMethod.Head
		};

		public static bool CanHaveContent(this HttpMethod httpMethod)
		{
			return NoBodyHttpMethods.All(a => a != httpMethod);
		}
	}
}