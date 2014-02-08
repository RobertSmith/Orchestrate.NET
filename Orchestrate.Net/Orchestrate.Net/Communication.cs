using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace Orchestrate.Net
{
    internal static class Communication
    {
        internal static string Get(string url, string apiKey)
        {
            var request = WebRequest.Create(url);
            request.Method = "GET";
            request.Credentials = GetCredentials(apiKey, url);
            request.ContentType = "application/json";

            String result = null;

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                Stream dataStream = response.GetResponseStream();

                if (dataStream != null)
                {
                    var reader = new StreamReader(dataStream);
                    result = reader.ReadToEnd();
                    reader.Close();
                }

                if (dataStream != null)
                    dataStream.Close();
            }

            return result;
        }

        internal static string Put(string url, string apiKey, object dataPayload)
        {
            var request = WebRequest.Create(url);
            request.Method = "PUT";
            request.Credentials = GetCredentials(apiKey, url);
            request.ContentType = "application/json";

            var json = JsonConvert.SerializeObject(dataPayload);
            var data = StringToByteArray(json);

            request.ContentLength = data.Length;
            using (var dataStream = request.GetRequestStream())
            {
                dataStream.Write(data, 0, data.Length);
            }

            String result = null;

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                Stream dataStream = response.GetResponseStream();

                if (dataStream != null)
                {
                    var reader = new StreamReader(dataStream);
                    result = reader.ReadToEnd();
                    reader.Close();
                }

                if (dataStream != null)
                    dataStream.Close();
            }

            return result;
        }

        internal static string Delete(string url, string apiKey, object dataPayload)
        {
            var request = WebRequest.Create(url);
            request.Method = "DELETE";
            request.Credentials = GetCredentials(apiKey, url);
            request.ContentType = "application/json";

            var json = JsonConvert.SerializeObject(dataPayload);
            var data = StringToByteArray(json);

            request.ContentLength = data.Length;
            using (var dataStream = request.GetRequestStream())
            {
                dataStream.Write(data, 0, data.Length);
            }

            String result = null;

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                Stream dataStream = response.GetResponseStream();

                if (dataStream != null)
                {
                    var reader = new StreamReader(dataStream);
                    result = reader.ReadToEnd();
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
