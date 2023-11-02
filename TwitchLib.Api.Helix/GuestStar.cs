using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.GuestStar.CreateGuestStarSession;
using TwitchLib.Api.Helix.Models.GuestStar.GetChannelGuestStarSettings;
using TwitchLib.Api.Helix.Models.GuestStar.GetGuestStarInvites;
using TwitchLib.Api.Helix.Models.GuestStar.GetGuestStarSession;
using TwitchLib.Api.Helix.Models.GuestStar.UpdateChannelGuestStarSettings;

namespace TwitchLib.Api.Helix;

public class GuestStar : ApiBase
{
    public GuestStar(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings,
        rateLimiter, http)
    {
    }

    #region GetChannelGuestStarSettings

    /// <summary>
    /// <para><see href="https://dev.twitch.tv/docs/api/reference/#get-channel-guest-star-settings">
    /// Twitch Docs: Get Channel Guest Star Settings</see></para>
    /// <para>Gets the channel settings for configuration of the Guest Star feature for a particular host.</para>
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster you want to get guest star settings for.</param>
    /// <param name="moderatorId">The ID of the broadcaster or a user that has permission to moderate the broadcaster’s chat room. This ID must match the user ID in the user access token.</param>
    /// <param name="accessToken">Optional access token to override the use of the stored one in the TwitchAPI instance</param>
    /// <returns cref="GetChannelGuestStarSettingsResponse"></returns>
    /// <exception cref="BadParameterException"></exception>
    public Task<GetChannelGuestStarSettingsResponse> GetChannelGuestStarSettingsAsync(string broadcasterId,
        string moderatorId, string accessToken = null)
    {
        if (string.IsNullOrWhiteSpace(broadcasterId))
            throw new BadParameterException("broadcasterId must be set");
        if (string.IsNullOrWhiteSpace(moderatorId))
            throw new BadParameterException("moderatorId must be set");

        var getParams = new List<KeyValuePair<string, string>>
        {
            new("broadcaster_id", broadcasterId),
            new("moderator_id", moderatorId)
        };

        return TwitchGetGenericAsync<GetChannelGuestStarSettingsResponse>("/guest_star/channel_settings",
            ApiVersion.Helix, getParams, accessToken);
    }

    #endregion

    #region UpdateChannelGuestStarSettings

    /// <summary>
    /// <para><see href="https://dev.twitch.tv/docs/api/reference/#update-channel-guest-star-settings">
    /// Twitch Docs: Update Channel Guest Star Settings</see></para>
    /// <para>Mutates the channel settings for configuration of the Guest Star feature for a particular host.</para>
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster you want to update Guest Star settings for.</param>
    /// <param name="newSettings">The new settings you want to apply</param>
    /// <param name="accessToken">Optional access token to override the use of the stored one in the TwitchAPI instance</param>
    /// <exception cref="BadParameterException"></exception>
    public Task UpdateChannelGuestStarSettingsAsync(string broadcasterId,
        UpdateChannelGuestStarSettingsRequest newSettings, string accessToken = null)
    {
        if (string.IsNullOrWhiteSpace(broadcasterId))
            throw new BadParameterException("broadcasterId must be set");

        if (newSettings == null)
            throw new BadParameterException("newSettings cannot be null");

        var getParams = new List<KeyValuePair<string, string>>
        {
            new("broadcaster_id", broadcasterId)
        };

        var payload = JsonConvert.SerializeObject(newSettings);

        return TwitchPatchAsync("/guest_star/channel_settings", ApiVersion.Helix, payload, getParams, accessToken);
    }

    #endregion

    #region GetGuestStarSession

    /// <summary>
    /// <para><see href="https://dev.twitch.tv/docs/api/reference/#get-guest-star-session">
    /// Twitch Docs: Get Guest Star Session</see></para>
    /// <para>Gets information about an ongoing Guest Star session for a particular channel.</para>
    /// </summary>
    /// <param name="broadcasterId">ID for the user hosting the Guest Star session.</param>
    /// <param name="moderatorId">The ID of the broadcaster or a user that has permission to moderate the broadcaster’s chat room. This ID must match the user ID in the user access token.</param>
    /// <param name="accessToken">Optional access token to override the use of the stored one in the TwitchAPI instance</param>
    /// <returns cref="GetGuestStarSessionResponse"></returns>
    /// <exception cref="BadParameterException"></exception>
    public Task<GetGuestStarSessionResponse> GetGuestStarSessionAsync(string broadcasterId, string moderatorId,
        string accessToken = null)
    {
        if (string.IsNullOrWhiteSpace(broadcasterId))
            throw new BadParameterException("broadcasterId must be set");
        if (string.IsNullOrWhiteSpace(moderatorId))
            throw new BadParameterException("moderatorId must be set");

        var getParams = new List<KeyValuePair<string, string>>
        {
            new("broadcaster_id", broadcasterId),
            new("moderator_id", moderatorId)
        };

        return TwitchGetGenericAsync<GetGuestStarSessionResponse>("/guest_star/session", ApiVersion.Helix, getParams,
            accessToken);
    }

    #endregion

    #region CreateGuestStarSession

    /// <summary>
    /// <para><see href="https://dev.twitch.tv/docs/api/reference/#create-guest-star-session">
    /// Twitch Docs: Create Guest Star Session</see></para>
    /// <para>Programmatically creates a Guest Star session on behalf of the broadcaster. Requires the broadcaster to be present in the call interface, or the call will be ended automatically.</para>
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster you want to create a Guest Star session for. Provided broadcaster_id must match the user_id in the auth token.</param>
    /// <param name="accessToken">Optional access token to override the use of the stored one in the TwitchAPI instance</param>
    /// <returns cref="CreateGuestStarSessionResponse"></returns>
    /// <exception cref="BadParameterException"></exception>
    public Task<CreateGuestStarSessionResponse> CreateGuestStarSession(string broadcasterId, string accessToken = null)
    {
        if (string.IsNullOrWhiteSpace(broadcasterId))
            throw new BadParameterException("broadcasterId must be set");

        var getParams = new List<KeyValuePair<string, string>>
        {
            new("broadcaster_id", broadcasterId)
        };

        return TwitchPostGenericAsync<CreateGuestStarSessionResponse>("/guest_star/session", ApiVersion.Helix, null,
            getParams, accessToken);
    }

    #endregion

    #region EndGuestStarSession

    /// <summary>
    /// <para><see href="https://dev.twitch.tv/docs/api/reference/#end-guest-star-session">
    /// Twitch Docs: End Guest Star Session</see></para>
    /// <para>A Programmatically ends a Guest Star session on behalf of the broadcaster. Performs the same action as if the host clicked the “End Call” button in the Guest Star UI.</para>
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster you want to end a Guest Star session for. Provided broadcaster_id must match the user_id in the auth token.</param>
    /// <param name="sessionId">ID for the session to end on behalf of the broadcaster.</param>
    /// <param name="accessToken">Optional access token to override the use of the stored one in the TwitchAPI instance</param>
    /// <returns cref="EndGuestStarSessionResponse"></returns>
    /// <exception cref="BadParameterException"></exception>
    public Task<EndGuestStarSessionResponse> EndGuestStarSession(string broadcasterId, string sessionId,
        string accessToken = null)
    {
        if (string.IsNullOrWhiteSpace(broadcasterId))
            throw new BadParameterException("broadcasterId must be set");

        if (string.IsNullOrWhiteSpace(sessionId))
            throw new BadParameterException("sessionId must be set");

        var getParams = new List<KeyValuePair<string, string>>
        {
            new("broadcaster_id", broadcasterId),
            new("sessionId", sessionId)
        };

        return TwitchDeleteGenericAsync<EndGuestStarSessionResponse>("/guest_star/session", ApiVersion.Helix, getParams,
            accessToken);
    }

    #endregion

    #region GetGuestStarInvites

    /// <summary>
    /// <para><see href="https://dev.twitch.tv/docs/api/reference/#get-guest-star-invites">
    /// Twitch Docs: Get Guest Star Invites</see></para>
    /// <para>Provides the caller with a list of pending invites to a Guest Star session, including the invitee’s ready status while joining the waiting room.</para>
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster running the Guest Star session.</param>
    /// <param name="moderatorId">The ID of the broadcaster or a user that has permission to moderate the broadcaster’s chat room. This ID must match the user ID in the user access token.</param>
    /// <param name="sessionId">The session ID to query for invite status.</param>
    /// <param name="accessToken">Optional access token to override the use of the stored one in the TwitchAPI instance</param>
    /// <returns cref="GetGuestStarSessionResponse"></returns>
    /// <exception cref="BadParameterException"></exception>
    public Task<GetGuestStarInvitesResponse> GetGuestStarInvitesAsync(string broadcasterId, string moderatorId,
        string sessionId, string accessToken = null)
    {
        if (string.IsNullOrWhiteSpace(broadcasterId))
            throw new BadParameterException("broadcasterId must be set");
        if (string.IsNullOrWhiteSpace(moderatorId))
            throw new BadParameterException("moderatorId must be set");
        if (string.IsNullOrWhiteSpace(sessionId))
            throw new BadParameterException("sessionId must be set");

        var getParams = new List<KeyValuePair<string, string>>
        {
            new("broadcaster_id", broadcasterId),
            new("moderator_id", moderatorId),
            new("session_id", sessionId)
        };

        return TwitchGetGenericAsync<GetGuestStarInvitesResponse>("/guest_star/invites", ApiVersion.Helix, getParams,
            accessToken);
    }

    #endregion

    #region SendGuestStarInvite

    /// <summary>
    /// <para><see href="https://dev.twitch.tv/docs/api/reference/#send-guest-star-invite">
    /// Twitch Docs: Send Guest Star Invite</see></para>
    /// <para>Sends an invite to a specified guest on behalf of the broadcaster for a Guest Star session in progress.</para>
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster running the Guest Star session.</param>
    /// <param name="moderatorId">The ID of the broadcaster or a user that has permission to moderate the broadcaster’s chat room. This ID must match the user ID in the user access token.</param>
    /// <param name="sessionId">The session ID for the invite to be sent on behalf of the broadcaster.</param>
    /// <param name="guestId">Twitch User ID for the guest to invite to the Guest Star session.</param>
    /// <param name="accessToken">Optional access token to override the use of the stored one in the TwitchAPI instance</param>
    /// <exception cref="BadParameterException"></exception>
    public Task SendGuestStarInvitesAsync(string broadcasterId, string moderatorId, string sessionId, string guestId,
        string accessToken = null)
    {
        if (string.IsNullOrWhiteSpace(broadcasterId))
            throw new BadParameterException("broadcasterId must be set");
        if (string.IsNullOrWhiteSpace(moderatorId))
            throw new BadParameterException("moderatorId must be set");
        if (string.IsNullOrWhiteSpace(sessionId))
            throw new BadParameterException("sessionId must be set");
        if (string.IsNullOrWhiteSpace(guestId))
            throw new BadParameterException("guestId must be set");

        var getParams = new List<KeyValuePair<string, string>>
        {
            new("broadcaster_id", broadcasterId),
            new("moderator_id", moderatorId),
            new("session_id", sessionId),
            new("guest_id", guestId)
        };

        return TwitchPostAsync("/guest_star/invites", ApiVersion.Helix, null, getParams, accessToken);
    }

    #endregion

    #region DeleteGuestStarInvite

    /// <summary>
    /// <para><see href="https://dev.twitch.tv/docs/api/reference/#delete-guest-star-invite">
    /// Twitch Docs: Delete Guest Star Invite</see></para>
    /// <para>Revokes a previously sent invite for a Guest Star session.</para>
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster running the Guest Star session.</param>
    /// <param name="moderatorId">The ID of the broadcaster or a user that has permission to moderate the broadcaster’s chat room. This ID must match the user ID in the user access token.</param>
    /// <param name="sessionId">The ID of the session for the invite to be revoked on behalf of the broadcaster.</param>
    /// <param name="guestId">Twitch User ID for the guest to revoke the Guest Star session invite from.</param>
    /// <param name="accessToken">Optional access token to override the use of the stored one in the TwitchAPI instance</param>
    /// <exception cref="BadParameterException"></exception>
    public Task DeleteGuestStarInvitesAsync(string broadcasterId, string moderatorId, string sessionId, string guestId,
        string accessToken = null)
    {
        if (string.IsNullOrWhiteSpace(broadcasterId))
            throw new BadParameterException("broadcasterId must be set");
        if (string.IsNullOrWhiteSpace(moderatorId))
            throw new BadParameterException("moderatorId must be set");
        if (string.IsNullOrWhiteSpace(sessionId))
            throw new BadParameterException("sessionId must be set");
        if (string.IsNullOrWhiteSpace(guestId))
            throw new BadParameterException("guestId must be set");

        var getParams = new List<KeyValuePair<string, string>>
        {
            new("broadcaster_id", broadcasterId),
            new("moderator_id", moderatorId),
            new("session_id", sessionId),
            new("guest_id", guestId)
        };

        return TwitchDeleteAsync("/guest_star/invites", ApiVersion.Helix, getParams, accessToken);
    }

    #endregion

    #region AssignGuestStarSlot

    /// <summary>
    /// <para><see href="https://dev.twitch.tv/docs/api/reference/#assign-guest-star-slot">
    /// Twitch Docs: Assign Guest Star Slot</see></para>
    /// <para>Allows a previously invited user to be assigned a slot within the active Guest Star session, once that guest has indicated they are ready to join.</para>
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster running the Guest Star session.</param>
    /// <param name="moderatorId">The ID of the broadcaster or a user that has permission to moderate the broadcaster’s chat room. This ID must match the user ID in the user access token.</param>
    /// <param name="sessionId">The ID of the Guest Star session in which to assign the slot.</param>
    /// <param name="guestId">The Twitch User ID corresponding to the guest to assign a slot in the session. This user must already have an invite to this session, and have indicated that they are ready to join.</param>
    /// <param name="slotId">The slot assignment to give to the user. Must be a numeric identifier between “1” and “N” where N is the max number of slots for the session. Max number of slots allowed for the session is reported by Get Channel Guest Star Settings.</param>
    /// <param name="accessToken">Optional access token to override the use of the stored one in the TwitchAPI instance</param>
    /// <exception cref="BadParameterException"></exception>
    public Task AssignGuestStarSlotAsync(string broadcasterId, string moderatorId, string sessionId, string guestId,
        string slotId, string accessToken = null)
    {
        if (string.IsNullOrWhiteSpace(broadcasterId))
            throw new BadParameterException("broadcasterId must be set");
        if (string.IsNullOrWhiteSpace(moderatorId))
            throw new BadParameterException("moderatorId must be set");
        if (string.IsNullOrWhiteSpace(sessionId))
            throw new BadParameterException("sessionId must be set");
        if (string.IsNullOrWhiteSpace(guestId))
            throw new BadParameterException("guestId must be set");
        if (string.IsNullOrWhiteSpace(slotId))
            throw new BadParameterException("slotId must be set");

        var getParams = new List<KeyValuePair<string, string>>
        {
            new("broadcaster_id", broadcasterId),
            new("moderator_id", moderatorId),
            new("session_id", sessionId),
            new("guest_id", guestId),
            new("slot_id", slotId)
        };

        return TwitchPostAsync("/guest_star/slot", ApiVersion.Helix, null, getParams, accessToken);
    }

    #endregion

    #region UpdateGuestStarSlot

    /// <summary>
    /// <para><see href="https://dev.twitch.tv/docs/api/reference/#update-guest-star-slot">
    /// Twitch Docs: Update Guest Star Slot</see></para>
    /// <para>Allows a user to update the assigned slot for a particular user within the active Guest Star session.</para>
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster running the Guest Star session.</param>
    /// <param name="moderatorId">The ID of the broadcaster or a user that has permission to moderate the broadcaster’s chat room. This ID must match the user ID in the user access token.</param>
    /// <param name="sessionId">The ID of the Guest Star session in which to update slot settings.</param>
    /// <param name="sourceSlotId">The slot assignment previously assigned to a user.</param>
    /// <param name="destinationSlotId">The slot to move this user assignment to. If the destination slot is occupied, the user assigned will be swapped into source_slot_id.</param>
    /// <param name="accessToken">Optional access token to override the use of the stored one in the TwitchAPI instance</param>
    /// <exception cref="BadParameterException"></exception>
    public Task UpdateGuestStarSlotAsync(string broadcasterId, string moderatorId, string sessionId,
        string sourceSlotId, string destinationSlotId, string accessToken = null)
    {
        if (string.IsNullOrWhiteSpace(broadcasterId))
            throw new BadParameterException("broadcasterId must be set");
        if (string.IsNullOrWhiteSpace(moderatorId))
            throw new BadParameterException("moderatorId must be set");
        if (string.IsNullOrWhiteSpace(sessionId))
            throw new BadParameterException("sessionId must be set");
        if (string.IsNullOrWhiteSpace(sourceSlotId))
            throw new BadParameterException("sourceSlotId must be set");
        if (string.IsNullOrWhiteSpace(destinationSlotId))
            throw new BadParameterException("destinationSlotId must be set");

        var getParams = new List<KeyValuePair<string, string>>
        {
            new("broadcaster_id", broadcasterId),
            new("moderator_id", moderatorId),
            new("session_id", sessionId),
            new("source_slot_id", sourceSlotId),
            new("destination_slot_id", destinationSlotId)
        };

        return TwitchPatchAsync("/guest_star/slot", ApiVersion.Helix, null, getParams, accessToken);
    }

    #endregion

    #region DeleteGuestStarSlot

    /// <summary>
    /// <para><see href="https://dev.twitch.tv/docs/api/reference/#delete-guest-star-slot">
    /// Twitch Docs: Delete Guest Star Slot</see></para>
    /// <para>Allows a caller to remove a slot assignment from a user participating in an active Guest Star session. This revokes their access to the session immediately and disables their access to publish or subscribe to media within the session.</para>
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster running the Guest Star session.</param>
    /// <param name="moderatorId">The ID of the broadcaster or a user that has permission to moderate the broadcaster’s chat room. This ID must match the user ID in the user access token.</param>
    /// <param name="sessionId">The ID of the Guest Star session in which to remove the slot assignment.</param>
    /// <param name="slotId">The slot ID representing the slot assignment to remove from the session.</param>
    /// <param name="shouldReinviteGuest">Optional Flag signaling that the guest should be reinvited to the session, sending them back to the invite queue.</param>
    /// <param name="accessToken">Optional access token to override the use of the stored one in the TwitchAPI instance</param>
    /// <exception cref="BadParameterException"></exception>
    public Task DeleteGuestStarSlotAsync(string broadcasterId, string moderatorId, string sessionId, string slotId, 
        bool? shouldReinviteGuest = null, string accessToken = null)
    {
        if (string.IsNullOrWhiteSpace(broadcasterId))
            throw new BadParameterException("broadcasterId must be set");
        if (string.IsNullOrWhiteSpace(moderatorId))
            throw new BadParameterException("moderatorId must be set");
        if (string.IsNullOrWhiteSpace(sessionId))
            throw new BadParameterException("sessionId must be set");
        if (string.IsNullOrWhiteSpace(slotId))
            throw new BadParameterException("slotId must be set");

        var getParams = new List<KeyValuePair<string, string>>
        {
            new("broadcaster_id", broadcasterId),
            new("moderator_id", moderatorId),
            new("session_id", sessionId),
            new("slot_id", slotId)
        };
        
        if (shouldReinviteGuest.HasValue)
            getParams.Add(new KeyValuePair<string, string>("should_reinvite_guest", shouldReinviteGuest.Value.ToString()));

        return TwitchDeleteAsync("/guest_star/slot", ApiVersion.Helix, getParams, accessToken);
    }

    #endregion
    
        #region DeleteGuestStarSlot

    /// <summary>
    /// <para><see href="https://dev.twitch.tv/docs/api/reference/#update-guest-star-slot-settings">
    /// Twitch Docs: Update Guest Star Slo Settings</see></para>
    /// <para>Allows a user to update slot settings for a particular guest within a Guest Star session, such as allowing the user to share audio or video within the call as a host. These settings will be broadcasted to all subscribers which control their view of the guest in that slot. One or more of the optional parameters to this API can be specified at any time.</para>
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster running the Guest Star session.</param>
    /// <param name="moderatorId">The ID of the broadcaster or a user that has permission to moderate the broadcaster’s chat room. This ID must match the user ID in the user access token.</param>
    /// <param name="sessionId">The ID of the Guest Star session in which to update a slot’s settings.</param>
    /// <param name="slotId">The slot assignment that has previously been assigned to a user.</param>
    /// <param name="isAudioEnabled">Optional Flag indicating whether the slot is allowed to share their audio with the rest of the session. If false, the slot will be muted in any views containing the slot.</param>
    /// <param name="isVideoEnabled">Optional Flag indicating whether the slot is allowed to share their video with the rest of the session. If false, the slot will have no video shared in any views containing the slot.</param>
    /// <param name="isLive">Optional Flag indicating whether the user assigned to this slot is visible/can be heard from any public subscriptions. Generally, this determines whether or not the slot is enabled in any broadcasting software integrations.</param>
    /// <param name="volume">Optional Value from 0-100 that controls the audio volume for shared views containing the slot.</param>
    /// <param name="accessToken">Optional access token to override the use of the stored one in the TwitchAPI instance</param>
    /// <exception cref="BadParameterException"></exception>
    public Task UpdateGuestStarSlotSettingsAsync(string broadcasterId, string moderatorId, string sessionId, string slotId, 
        bool? isAudioEnabled = null, bool? isVideoEnabled = null, bool? isLive = null, int? volume = null, string accessToken = null)
    {
        if (string.IsNullOrWhiteSpace(broadcasterId))
            throw new BadParameterException("broadcasterId must be set");
        if (string.IsNullOrWhiteSpace(moderatorId))
            throw new BadParameterException("moderatorId must be set");
        if (string.IsNullOrWhiteSpace(sessionId))
            throw new BadParameterException("sessionId must be set");
        if (string.IsNullOrWhiteSpace(slotId))
            throw new BadParameterException("slotId must be set");

        var getParams = new List<KeyValuePair<string, string>>
        {
            new("broadcaster_id", broadcasterId),
            new("moderator_id", moderatorId),
            new("session_id", sessionId),
            new("slot_id", slotId),
        };
        
        if (isAudioEnabled.HasValue)
            getParams.Add(new KeyValuePair<string, string>("is_audio_enabled", isAudioEnabled.Value.ToString()));
        if (isVideoEnabled.HasValue)
            getParams.Add(new KeyValuePair<string, string>("is_video_enabled", isVideoEnabled.Value.ToString()));
        if (isLive.HasValue)
            getParams.Add(new KeyValuePair<string, string>("is_live", isLive.Value.ToString()));
        if (volume is >= 0 and <= 100)
            getParams.Add(new KeyValuePair<string, string>("volume", volume.Value.ToString()));

        return TwitchPatchAsync("/guest_star/slot_settings", ApiVersion.Helix, null, getParams, accessToken);
    }

    #endregion
}