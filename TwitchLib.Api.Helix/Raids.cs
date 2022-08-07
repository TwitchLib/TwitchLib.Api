using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Raids.StartRaid;

namespace TwitchLib.Api.Helix
{
    public class Raids : ApiBase
    {
        public Raids(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        public Task<StartRaidResponse> StartRaidAsync(string fromBroadcasterId, string toBroadcasterId, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("from_broadcaster_id", fromBroadcasterId),
                new KeyValuePair<string, string>("to_broadcaster_id", toBroadcasterId)
            };

            return TwitchPostGenericAsync<StartRaidResponse>("/raids", ApiVersion.Helix, "", getParams: getParams, accessToken: accessToken);
        }

        public Task CancelRaidAsync(string broadcasterId, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
            };

            return TwitchDeleteAsync("/raids", ApiVersion.Helix, getParams: getParams, accessToken: accessToken);
        }
    }
}
