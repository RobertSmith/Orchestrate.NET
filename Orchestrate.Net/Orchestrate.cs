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

        private static double ConvertToUnixTimestamp(DateTime date)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalMilliseconds);
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
                Path = new OrchestratePath
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
