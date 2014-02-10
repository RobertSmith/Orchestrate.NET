using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace Orchestrate.Net
{
    internal static class Communication
    {
        internal static BaseResult CallWebRequest(string apiKey, string url, string method, object payload, string ifMatch = null, bool ifNoneMatch = false)
        {
            var request = WebRequest.Create(url);
            request.Method = method;
            request.Credentials = GetCredentials(apiKey, url);
            request.ContentType = "application/json";

            if (!string.IsNullOrEmpty(ifMatch))
                request.Headers.Add(HttpRequestHeader.IfMatch, ifMatch);
            else if (ifNoneMatch)
                request.Headers.Add(HttpRequestHeader.IfNoneMatch, "\"*\"");

            if (payload != null)
            {
                var json = JsonConvert.SerializeObject(payload);
                var data = StringToByteArray(json);

                request.ContentLength = data.Length;
                using (var dataStream = request.GetRequestStream())
                {
                    dataStream.Write(data, 0, data.Length);
                }
            }

            var result = new BaseResult();

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                result.Location = response.Headers["location"];
                result.ETag = response.Headers["eTag"];

                Stream dataStream = response.GetResponseStream();

                if (dataStream != null)
                {
                    var reader = new StreamReader(dataStream);
                    result.Payload = reader.ReadToEnd();
                    reader.Close();
                }

                if (dataStream != null)
                    dataStream.Close();
            }

            return result;
        }

        private static CredentialCache GetCredentials(string apiKey, string url)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;

            var credentialCache = new CredentialCache
            {
                {
                    new Uri(url),
                    "Basic",
                    new NetworkCredential(apiKey, String.Empty)
                }
            };

            return credentialCache;
        }

        private static byte[] StringToByteArray(string str)
        {
            var bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
    }
}
