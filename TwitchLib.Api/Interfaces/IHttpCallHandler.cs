using System.Collections.Generic;
using TwitchLib.Api.Enums;

namespace TwitchLib.Api.Interfaces
{
    public interface IHttpCallHandler
    {
        KeyValuePair<int, string> GeneralRequestAsync(string url, string method, string payload = null, ApiVersion api = ApiVersion.v5, string clientId = null, string accessToken = null);
        void PutBytes(string url, byte[] payload);
        int RequestReturnResponseCode(string url, string method, List<KeyValuePair<string, string>> getParams = null);
    }
}
