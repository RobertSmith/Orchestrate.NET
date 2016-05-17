using System;
using System.Threading.Tasks;

namespace Orchestrate.Net
{
    public partial class Orchestrate
    {
        public bool Authenticate(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            try
            {
                var url = _urlBase;
                Communication.CallWebRequest(key, url, "HEAD", string.Empty);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> AuthenticateAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "key cannot be null or empty");

            try
            {
                var url = _urlBase;
                await Communication.CallWebRequestAsync(key, url, "HEAD", string.Empty);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
