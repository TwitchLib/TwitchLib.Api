
using System;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Interfaces;
using RootModel = TwitchLib.Api.Core.Models.Root.Root;

namespace TwitchLib.Api.V5
{
    [Obsolete("This is a v5 class, please use a Helix class. All v5 calls will be turned off on February 28, 2022. Details: https://blog.twitch.tv/en/2021/07/15/legacy-twitch-api-v5-shutdown-details-and-timeline/ ")]
    public class Root : ApiBase
    {
        public Root(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        #region GetRoot
        [Obsolete("This is a v5 method, please use a Helix method. All v5 methods will be turned off on February 28, 2022. Details: https://blog.twitch.tv/en/2021/07/15/legacy-twitch-api-v5-shutdown-details-and-timeline/ ")]
        public Task<RootModel> GetRootAsync(string authToken = null, string clientId = null)
        {
            return TwitchGetGenericAsync<RootModel>("", ApiVersion.V5, accessToken: authToken, clientId: clientId);
        }

        #endregion

    }
}