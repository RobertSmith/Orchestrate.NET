using System;

namespace Orchestrate.Net
{
    public partial class Orchestrate : IOrchestrate
    {
        private readonly string _apiKey;
        private readonly string _urlBase;

        public Orchestrate(string apiKey, string url = "https://api.orchestrate.io/v0/")
        {
            _apiKey = apiKey;
            _urlBase = url;
        }

        #region Helper Functions

        public static long ConvertToUnixTimestamp(DateTime date)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return (long)Math.Floor(diff.TotalMilliseconds);
        }

        public static DateTime ConvertFromUnixTimeStamp(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(unixTimeStamp).ToUniversalTime();
            return dtDateTime;
        }

        public static DateTime ConvertFromUnixTimeStamp(string unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            double ts = double.Parse(unixTimeStamp);
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(ts).ToUniversalTime();
            return dtDateTime;
        }

        private static string ExtractKeyFromLocation(BaseResult baseResult)
        {
            // Always in the format /<api version>/<collection>/<key>/refs/<ref>
            var locationParts = baseResult.Location.Split('/');
            return locationParts[3];
        }

        private static Result BuildResult(string collectionName, string key, BaseResult baseResult)
        {
            return new Result
            {
                Path = new Path
                {
                    Collection = collectionName,
                    Key = key,
                    Ref = baseResult.ETag
                },
                Score = 1,
                Value = baseResult.Payload
            };
        }

        #endregion
    }
}
