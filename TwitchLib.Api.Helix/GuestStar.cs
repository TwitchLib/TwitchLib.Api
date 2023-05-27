using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.GuestStar.GetChannelGuestStarSettings;
using TwitchLib.Api.Helix.Models.GuestStar.UpdateChannelGuestStarSettings;

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

        #region GetChannelGuestStarSettings
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

        #region UpdateChannelGuestStarSettings

        /// <summary>
        /// [BETA] Updates the channel settings configuration of the Guest Star feature for a particular host.
        /// <para>The Custom Reward specified by id must have been created by the ClientId attached to the user OAuth token.</para>
        /// <para>Required scope: channel:manage:guest_star</para>
        /// </summary>
        /// <param name="broadcasterId">
        /// The ID of the broadcaster you want to update Guest Star settings for.
        /// </param>
        /// <param name="request" cref="UpdateChannelGuestStarSettingsRequest"></param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        public Task UpdateCustomRewardAsync(string broadcasterId, UpdateChannelGuestStarSettingsRequest request, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
            };

            return TwitchPatchAsync("/guest_star/channel_settings", ApiVersion.Helix, JsonConvert.SerializeObject(request), getParams, accessToken);
        }

        #endregion

        #region GetGuestStarSession
        /// <summary>
        /// [BETA] Gets information about an ongoing Guest Star session for a particular channel.
        /// <para>Requires OAuth Scope: channel:read:guest_star, channel:manage:guest_star, moderator:read:guest_star or moderator:manage:guest_star </para>
        /// <para>Guests must be either invited or assigned a slot within the session.</para>
        /// </summary>
        /// <param name="broadcasterId">ID for the user hosting the Guest Star session.</param>
        /// <param name="moderatorId">The ID of the broadcaster or a user that has permission to moderate the broadcaster’s chat room. This ID must match the user ID in the user access token.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetChannelGuestStarSettingsResponse"></returns>
        /// <exception cref="BadParameterException"></exception>
        public Task<GetGuestStarSessionResponse> GetGuestStarSessionAsync(string broadcasterId, string moderatorId, string accessToken = null)
        {
            if (string.IsNullOrEmpty(broadcasterId))
                throw new BadParameterException("broadcasterId cannot be null or empty");
            if (string.IsNullOrEmpty(moderatorId))
                throw new BadParameterException("moderatorId cannot be null or empty");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("moderator_id", moderatorId)
            };

            return TwitchGetGenericAsync<GetGuestStarSessionResponse>("/guest_star/session", ApiVersion.Helix, getParams, accessToken);
        }
        #endregion

        #region CreateGuestStarSession
        /// <summary>
        /// [BETA] Programmatically creates a Guest Star session on behalf of the broadcaster. 
        /// <para>Requires the broadcaster to be present in the call interface, or the call will be ended automatically.</para>
        /// <para>The ID in the broadcasterId query parameter must match the user ID associated with the user OAuth token.</para>
        /// </summary>
        /// <param name="broadcasterId">The ID of the broadcaster you want to create a Guest Star session for.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="CreateGuestStarSessionResponse"></returns>
        /// <exception cref="BadParameterException"></exception>
        public Task<CreateGuestStarSessionResponse> CreateGuestStarSessionAsync(string broadcasterId, string accessToken = null)
        {
            if (string.IsNullOrEmpty(broadcasterId))
                throw new BadParameterException("broadcasterId cannot be null or empty");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId)
            };

            return TwitchPostGenericAsync<CreateGuestStarSessionResponse>("/guest_star/session", ApiVersion.Helix, null, getParams, accessToken);
        }
        #endregion

        #region EndGuestStarSession
        /// <summary>
        /// [BETA] Programmatically ends a Guest Star session on behalf of the broadcaster.
        /// Performs the same action as if the host clicked the “End Call” button in the Guest Star UI.
        /// <para>Requires OAuth Scope: channel:manage:guest_star</para>
        /// <para>The ID in the broadcasterId query parameter must match the user ID associated with the user OAuth token.</para>
        /// </summary>
        /// <param name="broadcasterId">The ID of the broadcaster you want to create a Guest Star session for.</param>
        /// <param name="sessionId">ID for the session to end on behalf of the broadcaster.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="EndGuestStarSessionResponse"></returns>
        /// <exception cref="BadParameterException"></exception>
        public Task<EndGuestStarSessionResponse> EndGuestStarSessionAsync(string broadcasterId, string sessionId, string accessToken = null)
        {
            if (string.IsNullOrEmpty(broadcasterId))
                throw new BadParameterException("broadcasterId cannot be null or empty");
            if (string.IsNullOrEmpty(sessionId))
                throw new BadParameterException("sessionId cannot be null or empty");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("session_id", sessionId)
            };

            return TwitchPutGenericAsync<EndGuestStarSessionResponse>("/guest_star/session", ApiVersion.Helix, null, getParams, accessToken);
        }
        #endregion

        #region GetGuestStarInvites
        /// <summary>
        /// [BETA] Provides the caller with a list of pending invites to a Guest Star session,
        /// including the invitee’s ready status while joining the waiting room.
        /// <para>Requires OAuth Scope: channel:read:guest_star, channel:manage:guest_star, moderator:read:guest_star or moderator:manage:guest_star </para>
        /// <para>The ID in the broadcasterId query parameter must match the user ID associated with the user OAuth token.</para>
        /// </summary>
        /// <param name="broadcasterId">The ID of the broadcaster running the Guest Star session.</param>
        /// <param name="moderatorId">The ID of the broadcaster or a user that has permission to moderate the broadcaster’s chat room. This ID must match the user_id in the user access token.</param>
        /// <param name="sessionId">The session ID to query for invite status.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetChannelGuestStarSettingsResponse"></returns>
        /// <exception cref="BadParameterException"></exception>
        public Task<GetGuestStarInvitesResponse> GetGuestStarInvitesAsync(string broadcasterId, string moderatorId, string sessionId, string accessToken = null)
        {
            if (string.IsNullOrEmpty(broadcasterId))
                throw new BadParameterException("broadcasterId cannot be null or empty");
            if (string.IsNullOrEmpty(moderatorId))
                throw new BadParameterException("moderatorId cannot be null or empty");
            if (string.IsNullOrEmpty(sessionId))
                throw new BadParameterException("moderatorId cannot be null or empty");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("moderator_id", moderatorId),
                new KeyValuePair<string, string>("session_id", sessionId)
            };

            return TwitchGetGenericAsync<GetGuestStarInvitesResponse>("/guest_star/invites", ApiVersion.Helix, getParams, accessToken);
        }
        #endregion
    }
}
