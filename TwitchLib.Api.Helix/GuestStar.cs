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
        /// <para>Requires a user OAuth access token with scope set to channel:read:guest_star or channel:manage:guest_star. </para>
        /// <para>The ID in the broadcasterId query parameter must match the user ID associated with the user OAuth token.</para>
        /// </summary>
        /// <param name="broadcasterId">The ID of the broadcaster you want to update Guest Star settings for.</param>
        /// <param name="request" cref="UpdateChannelGuestStarSettingsRequest"></param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        public Task UpdateChannelGuestStarSettingsAsync(string broadcasterId, UpdateChannelGuestStarSettingsRequest request, string accessToken = null)
        {
            if (string.IsNullOrEmpty(broadcasterId))
                throw new BadParameterException("broadcasterId cannot be null or empty");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
            };

            return TwitchPutAsync("/guest_star/channel_settings", ApiVersion.Helix, JsonConvert.SerializeObject(request), getParams, accessToken);
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
        /// <returns cref="GuestStarSessionResponse"></returns>
        /// <exception cref="BadParameterException"></exception>
        public Task<GuestStarSessionResponse> GetGuestStarSessionAsync(string broadcasterId, string moderatorId, string accessToken = null)
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

            return TwitchGetGenericAsync<GuestStarSessionResponse>("/guest_star/session", ApiVersion.Helix, getParams, accessToken);
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
        /// <returns cref="GuestStarSessionResponse"></returns>
        /// <exception cref="BadParameterException"></exception>
        public Task<GuestStarSessionResponse> CreateGuestStarSessionAsync(string broadcasterId, string accessToken = null)
        {
            if (string.IsNullOrEmpty(broadcasterId))
                throw new BadParameterException("broadcasterId cannot be null or empty");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId)
            };

            return TwitchPostGenericAsync<GuestStarSessionResponse>("/guest_star/session", ApiVersion.Helix, null, getParams, accessToken);
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
        /// <returns cref="GuestStarSessionResponse"></returns>
        /// <exception cref="BadParameterException"></exception>
        public Task<GuestStarSessionResponse> EndGuestStarSessionAsync(string broadcasterId, string sessionId, string accessToken = null)
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

            return TwitchDeleteGenericAsync<GuestStarSessionResponse>("/guest_star/session", ApiVersion.Helix, getParams, accessToken);
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
        /// <returns cref="GetGuestStarInvitesResponse"></returns>
        /// <exception cref="BadParameterException"></exception>
        public Task<GetGuestStarInvitesResponse> GetGuestStarInvitesAsync(string broadcasterId, string moderatorId, string sessionId, string accessToken = null)
        {
            if (string.IsNullOrEmpty(broadcasterId))
                throw new BadParameterException("broadcasterId cannot be null or empty");
            if (string.IsNullOrEmpty(moderatorId))
                throw new BadParameterException("moderatorId cannot be null or empty");
            if (string.IsNullOrEmpty(sessionId))
                throw new BadParameterException("sessionId cannot be null or empty");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("moderator_id", moderatorId),
                new KeyValuePair<string, string>("session_id", sessionId)
            };

            return TwitchGetGenericAsync<GetGuestStarInvitesResponse>("/guest_star/invites", ApiVersion.Helix, getParams, accessToken);
        }
        #endregion

        #region SendGuestStarInvite
        /// <summary>
        /// [BETA] Sends an invite to a specified guest on behalf of the broadcaster for a Guest Star session in progress.
        /// <para>Requires OAuth Scope: channel:manage:guest_star or moderator:manage:guest_star </para>
        /// <para>The ID in the moderator_id query parameter must match the user ID associated with the user OAuth token.</para>
        /// </summary>
        /// <param name="broadcasterId">The ID of the broadcaster running the Guest Star session.</param>
        /// <param name="moderatorId">The ID of the broadcaster or a user that has permission to moderate the broadcaster’s chat room. This ID must match the user_id in the user access token.</param>
        /// <param name="sessionId">The session ID for the invite to be sent on behalf of the broadcaster.</param>
        /// <param name="guestId">Twitch User ID for the guest to invite to the Guest Star session.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <exception cref="BadParameterException"></exception>
        public Task SendGuestStarInviteAsync(string broadcasterId, string moderatorId, string sessionId, string guestId, string accessToken = null)
        {
            if (string.IsNullOrEmpty(broadcasterId))
                throw new BadParameterException("broadcasterId cannot be null or empty");
            if (string.IsNullOrEmpty(moderatorId))
                throw new BadParameterException("moderatorId cannot be null or empty");
            if (string.IsNullOrEmpty(sessionId))
                throw new BadParameterException("sessionId cannot be null or empty");
            if (string.IsNullOrEmpty(guestId))
                throw new BadParameterException("guestId cannot be null or empty");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("moderator_id", moderatorId),
                new KeyValuePair<string, string>("session_id", sessionId),
                new KeyValuePair<string, string>("guest_id", guestId)
            };

            return TwitchPostAsync("/guest_star/invites", ApiVersion.Helix, null, getParams, accessToken);
        }
        #endregion

        #region DeleteGuestStarInvite
        /// <summary>
        /// [BETA] Sends an invite to a specified guest on behalf of the broadcaster for a Guest Star session in progress.
        /// <para>Requires OAuth Scope: channel:manage:guest_star or moderator:manage:guest_star </para>
        /// <para>The ID in the moderator_id query parameter must match the user ID associated with the user OAuth token.</para>
        /// </summary>
        /// <param name="broadcasterId">The ID of the broadcaster running the Guest Star session.</param>
        /// <param name="moderatorId">The ID of the broadcaster or a user that has permission to moderate the broadcaster’s chat room. This ID must match the user_id in the user access token.</param>
        /// <param name="sessionId">The session ID for the invite to be sent on behalf of the broadcaster.</param>
        /// <param name="guestId">Twitch User ID for the guest to invite to the Guest Star session.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <exception cref="BadParameterException"></exception>
        public Task DeleteGuestStarInviteAsync(string broadcasterId, string moderatorId, string sessionId, string guestId, string accessToken = null)
        {
            if (string.IsNullOrEmpty(broadcasterId))
                throw new BadParameterException("broadcasterId cannot be null or empty");
            if (string.IsNullOrEmpty(moderatorId))
                throw new BadParameterException("moderatorId cannot be null or empty");
            if (string.IsNullOrEmpty(sessionId))
                throw new BadParameterException("sessionId cannot be null or empty");
            if (string.IsNullOrEmpty(guestId))
                throw new BadParameterException("guestId cannot be null or empty");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("moderator_id", moderatorId),
                new KeyValuePair<string, string>("session_id", sessionId),
                new KeyValuePair<string, string>("guest_id", guestId)
            };

            return TwitchDeleteAsync("/guest_star/invites", ApiVersion.Helix, getParams, accessToken);
        }
        #endregion

        #region AssignGuestStarSlot
        /// <summary>
        /// [BETA] Allows a previously invited user to be assigned a slot within the active Guest Star session, once that guest has indicated they are ready to join.
        /// <para>Requires OAuth Scope: channel:manage:guest_star or moderator:manage:guest_star </para>
        /// <para>The ID in the moderator_id query parameter must match the user ID associated with the user OAuth token.</para>
        /// </summary>
        /// <param name="broadcasterId">The ID of the broadcaster running the Guest Star session.</param>
        /// <param name="moderatorId">The ID of the broadcaster or a user that has permission to moderate the broadcaster’s chat room. This ID must match the user_id in the user access token.</param>
        /// <param name="sessionId">The ID of the Guest Star session in which to assign the slot.</param>
        /// <param name="guestId">The Twitch User ID corresponding to the guest to assign a slot in the session. This user must already have an invite to this session, and have indicated that they are ready to join.</param>
        /// <param name="slotId">The slot assignment to give to the user. Must be a numeric identifier between “1” and “N” where N is the max number of slots for the session.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <exception cref="BadParameterException"></exception>
        public Task AssignGuestStarSlotAsync(string broadcasterId, string moderatorId, string sessionId, string guestId, string slotId, string accessToken = null)
        {
            if (string.IsNullOrEmpty(broadcasterId))
                throw new BadParameterException("broadcasterId cannot be null or empty");
            if (string.IsNullOrEmpty(moderatorId))
                throw new BadParameterException("moderatorId cannot be null or empty");
            if (string.IsNullOrEmpty(sessionId))
                throw new BadParameterException("sessionId cannot be null or empty");
            if (string.IsNullOrEmpty(guestId))
                throw new BadParameterException("guestId cannot be null or empty");
            if (string.IsNullOrEmpty(slotId))
                throw new BadParameterException("slotId cannot be null or empty");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("moderator_id", moderatorId),
                new KeyValuePair<string, string>("session_id", sessionId),
                new KeyValuePair<string, string>("guest_id", guestId),
                new KeyValuePair<string, string>("slot_id", slotId)
            };

            return TwitchPostAsync("/guest_star/slot", ApiVersion.Helix, null, getParams, accessToken);
        }
        #endregion

        #region UpdateGuestStarSlot
        /// <summary>
        /// [BETA] Allows a user to update the assigned slot for a particular user within the active Guest Star session.
        /// <para>Requires OAuth Scope: channel:manage:guest_star or moderator:manage:guest_star </para>
        /// <para>The ID in the moderator_id query parameter must match the user ID associated with the user OAuth token.</para>
        /// </summary>
        /// <param name="broadcasterId">The ID of the broadcaster running the Guest Star session.</param>
        /// <param name="moderatorId">The ID of the broadcaster or a user that has permission to moderate the broadcaster’s chat room. This ID must match the user_id in the user access token.</param>
        /// <param name="sessionId">The ID of the Guest Star session in which to update slot settings.</param>
        /// <param name="sourceSlotId">The slot assignment previously assigned to a user.</param>
        /// <param name="destinationSlotId">The slot to move this user assignment to. If the destination slot is occupied, the user assigned will be swapped into sourceSlotId.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <exception cref="BadParameterException"></exception>
        public Task UpdateGuestStarSlotAsync(string broadcasterId, string moderatorId, string sessionId, string sourceSlotId, string destinationSlotId, string accessToken = null)
        {
            if (string.IsNullOrEmpty(broadcasterId))
                throw new BadParameterException("broadcasterId cannot be null or empty");
            if (string.IsNullOrEmpty(moderatorId))
                throw new BadParameterException("moderatorId cannot be null or empty");
            if (string.IsNullOrEmpty(sessionId))
                throw new BadParameterException("sessionId cannot be null or empty");
            if (string.IsNullOrEmpty(sourceSlotId))
                throw new BadParameterException("sourceSlotId cannot be null or empty");
            if (string.IsNullOrEmpty(destinationSlotId))
                throw new BadParameterException("destinationSlotId cannot be null or empty");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("moderator_id", moderatorId),
                new KeyValuePair<string, string>("session_id", sessionId),
                new KeyValuePair<string, string>("source_slot_id", sourceSlotId),
                new KeyValuePair<string, string>("destination_slot_id", destinationSlotId)
            };

            return TwitchPatchAsync("/guest_star/slot", ApiVersion.Helix, null, getParams, accessToken);
        }
        #endregion

        #region DeleteGuestStarSlot
        /// <summary>
        /// [BETA] Allows a caller to remove a slot assignment from a user participating in an active Guest Star session.
        /// This revokes their access to the session immediately and disables their access to publish or subscribe to media within the session.
        /// <para>Requires OAuth Scope: channel:manage:guest_star or moderator:manage:guest_star </para>
        /// <para>The ID in the moderator_id query parameter must match the user ID associated with the user OAuth token.</para>
        /// </summary>
        /// <param name="broadcasterId">The ID of the broadcaster running the Guest Star session.</param>
        /// <param name="moderatorId">The ID of the broadcaster or a user that has permission to moderate the broadcaster’s chat room. This ID must match the user_id in the user access token.</param>
        /// <param name="sessionId">The ID of the Guest Star session in which to remove the slot assignment.</param>
        /// <param name="guestId">The Twitch User ID corresponding to the guest to remove from the session.</param>
        /// <param name="slotId">The slot ID representing the slot assignment to remove from the session.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <exception cref="BadParameterException"></exception>
        public Task DeleteGuestStarSlotAsync(string broadcasterId, string moderatorId, string sessionId, string guestId, string slotId, string shouldReinviteGuest = null, string accessToken = null)
        {
            if (string.IsNullOrEmpty(broadcasterId))
                throw new BadParameterException("broadcasterId cannot be null or empty");
            if (string.IsNullOrEmpty(moderatorId))
                throw new BadParameterException("moderatorId cannot be null or empty");
            if (string.IsNullOrEmpty(sessionId))
                throw new BadParameterException("sessionId cannot be null or empty");
            if (string.IsNullOrEmpty(guestId))
                throw new BadParameterException("guestId cannot be null or empty");
            if (string.IsNullOrEmpty(slotId))
                throw new BadParameterException("slotId cannot be null or empty");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("moderator_id", moderatorId),
                new KeyValuePair<string, string>("session_id", sessionId),
                new KeyValuePair<string, string>("guest_id", guestId),
                new KeyValuePair<string, string>("slot_id", slotId)
            };

            if (shouldReinviteGuest != null)
            {
                getParams.Add(new KeyValuePair<string, string>("should_reinvite_guest", shouldReinviteGuest));
            }

            return TwitchDeleteAsync("/guest_star/slot", ApiVersion.Helix, getParams, accessToken);
        }
        #endregion

    }
}
