using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.GuestStar;

namespace TwitchLib.Api.Helix
{
    /// <summary>
    /// Guest Star related APIs
    /// </summary>
    public class GuestStar : ApiBase
    {
        public GuestStar(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        #region GetCreatorGoals
        /// <summary>
        /// [BETA] Gets the channel settings for configuration of the Guest Star feature for a particular host.
        /// <para>Requires a user OAuth access token with scope set to channel:read:guest_star or channel:manage:guest_star. </para>
        /// <para>The ID in the broadcasterId query parameter must match the user ID associated with the user OAuth token.</para>
        /// </summary>
        /// <param name="broadcasterId">The ID of the broadcaster you want to get guest star settings for.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetChannelGuestStarSettingsResponse"></returns>
        /// <exception cref="BadParameterException"></exception>
        public Task<GetChannelGuestStarSettingsResponse> GetChannelGuestStarSettingsAsync(string broadcasterId, string accessToken = null)
        {
            if (string.IsNullOrEmpty(broadcasterId))
                throw new BadParameterException("broadcasterId cannot be null or empty");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId)
            };

            return TwitchGetGenericAsync<GetChannelGuestStarSettingsResponse>("/guest_star/channel_settings", ApiVersion.Helix, getParams, accessToken);
        }
        #endregion
    }
}
