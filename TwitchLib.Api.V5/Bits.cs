using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.V5.Models.Bits;

namespace TwitchLib.Api.V5
{
    [Obsolete("This is a v5 class, please use a Helix class. All v5 calls will be turned off on February 28, 2022. Details: https://blog.twitch.tv/en/2021/07/15/legacy-twitch-api-v5-shutdown-details-and-timeline/ ")]
    public class Bits : ApiBase
    {
        public Bits(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        #region GetCheermotes

        [Obsolete("This is a v5 method, please use a Helix method. All v5 methods will be turned off on February 28, 2022. Details: https://blog.twitch.tv/en/2021/07/15/legacy-twitch-api-v5-shutdown-details-and-timeline/ ")]
        public Task<Cheermotes> GetCheermotesAsync(string channelId = null)
        {
            List<KeyValuePair<string, string>> getParams = null;
            if (channelId != null)
            {
                getParams = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("channel_id", channelId)
                    };
            }

            return TwitchGetGenericAsync<Cheermotes>("/bits/actions", ApiVersion.V5, getParams);
        }

        #endregion
    }

}