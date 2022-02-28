using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core.Enums;

namespace TwitchLib.Api.Core.Interfaces
{
    public interface IHttpCallHandler
    {
        Task<KeyValuePair<int, string>> GeneralRequestAsync(string url, string method, string payload = null, ApiVersion api = ApiVersion.Helix, string clientId = null, string accessToken = null);
        Task PutBytesAsync(string url, byte[] payload);
        Task<int> RequestReturnResponseCodeAsync(string url, string method, List<KeyValuePair<string, string>> getParams = null);
    }
}
