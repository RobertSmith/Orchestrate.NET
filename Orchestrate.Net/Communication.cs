using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Orchestrate.Net
{
	internal static class Communication
	{
		internal static BaseResult CallWebRequest(string apiKey, string url, string method, string jsonPayload, string ifMatch = null, bool ifNoneMatch = false)
		{
			return CallOrchestrate(apiKey, url, method, jsonPayload, ifMatch, ifNoneMatch);
		}

		internal static Task<BaseResult> CallWebRequestAsync(string apiKey, string url, string method, string jsonPayload, string ifMatch = null, bool ifNoneMatch = false)
		{
			var httpMethod = method.ToHttpMethod();
			var httpClient = new HttpClient();
			var request = new HttpRequestMessage(httpMethod, url);

			if (jsonPayload != null && httpMethod.CanHaveContent())
				request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            if (!string.IsNullOrEmpty(ifMatch))
				request.Headers.Add(HttpRequestHeader.IfMatch.ToString(), ifMatch);
            else if (ifNoneMatch)
				request.Headers.Add(HttpRequestHeader.IfNoneMatch.ToString(), "\"*\"");

			var authorization =
						Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:", apiKey)));

			request.Headers.Authorization = new AuthenticationHeaderValue("Basic", authorization);
			return httpClient.SendAsync(request).ContinueWith<BaseResult>(BuildResult);
		}

		private static BaseResult BuildResult(Task<HttpResponseMessage> responseMessageTask)
		{
			var response = responseMessageTask.Result;
			response.EnsureSuccessStatusCode();

			var payload = response.Content.ReadAsStringAsync().Result;
			var location = (response.Headers.Location != null) ? response.Headers.Location.ToString() : string.Empty;
            var eTag = (response.Headers.ETag != null) ? response.Headers.ETag.Tag : string.Empty;

			var toReturn = new BaseResult
			{
				Location = location,
				ETag = eTag,
				Payload = payload
			};

			return toReturn;
		}

		private static BaseResult CallOrchestrate(string apiKey, string url, string method, string jsonPayload, string ifMatch, bool ifNoneMatch)
		{
			return CallWebRequestAsync(apiKey, url, method, jsonPayload, ifMatch, ifNoneMatch).Result;
		}

		private static byte[] StringToByteArray(string str)
		{
			var bytes = new byte[str.Length * sizeof(char)];
			Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
			return bytes;
		}
	}
}