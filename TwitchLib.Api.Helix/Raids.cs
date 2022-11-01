using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Raids.StartRaid;

namespace TwitchLib.Api.Helix
{
    /// <summary>
    /// Raids related APIs
    /// </summary>
    public class Raids : ApiBase
    {
        public Raids(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        /// <summary>
        /// Raid another channel by sending the broadcaster’s viewers to the targeted channel.
        /// <para>Rate Limit: The limit is 10 requests within a 10-minute window.</para>
        /// <para>Requires a user access token that includes the channel:manage:raids scope.</para>
        /// <para>The ID in the from_broadcaster_id query parameter must match the user ID in the OAuth token.</para>
        /// </summary>
        /// <param name="fromBroadcasterId">The ID of the broadcaster that’s sending the raiding party.</param>
        /// <param name="toBroadcasterId">	The ID of the broadcaster to raid.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="StartRaidResponse"></returns>
        public Task<StartRaidResponse> StartRaidAsync(string fromBroadcasterId, string toBroadcasterId, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("from_broadcaster_id", fromBroadcasterId),
                new KeyValuePair<string, string>("to_broadcaster_id", toBroadcasterId)
            };

            return TwitchPostGenericAsync<StartRaidResponse>("/raids", ApiVersion.Helix, string.Empty, getParams: getParams, accessToken: accessToken);
        }

        /// <summary>
        /// Cancel a pending raid.
        /// <para>You can cancel a raid at any point up until the broadcaster clicks Raid Now in the Twitch UX or the 90 seconds countdown expires.</para>
        /// <para>Rate Limit: The limit is 10 requests within a 10-minute window.</para>
        /// <para>Requires a user access token that includes the channel:manage:raids scope.</para>
        /// <para>The ID in the broadcaster_id query parameter must match the user ID in the OAuth token.</para>
        /// </summary>
        /// <param name="broadcasterId">The ID of the broadcaster that sent the raiding party.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
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
