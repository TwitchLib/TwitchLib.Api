using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.GuestStar.CreateGuestStarSession;
using TwitchLib.Api.Helix.Models.GuestStar.GetChannelGuestStarSettings;
using TwitchLib.Api.Helix.Models.GuestStar.GetGuestStarSession;
using TwitchLib.Api.Helix.Models.GuestStar.UpdateChannelGuestStarSettings;

namespace TwitchLib.Api.Helix;

public class GuestStar : ApiBase
{
    public GuestStar(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
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
    /// <param name="accessToken"> Optional access token to override the use of the stored one in the TwitchAPI instance</param>
    /// <returns cref="GetChannelGuestStarSettingsResponse"></returns>
    /// <exception cref="BadParameterException"></exception>
    public Task<GetChannelGuestStarSettingsResponse> GetChannelGuestStarSettingsAsync(string broadcasterId, string moderatorId, string accessToken = null)
    {
        if (string.IsNullOrWhiteSpace(broadcasterId))
            throw new BadParameterException("broadcasterId must be set");
        if (string.IsNullOrWhiteSpace(moderatorId))
            throw new BadParameterException("moderatorId must be set");
        
        var getParams = new List<KeyValuePair<string, string>>
        {
            new ("broadcaster_id", broadcasterId),
            new ("moderator_id", moderatorId)
        };

        return TwitchGetGenericAsync<GetChannelGuestStarSettingsResponse>("/guest_star/channel_settings", ApiVersion.Helix, getParams, accessToken);
    }
    
    #endregion

    #region UpdateChannelGuestStarSettings

    /// <summary>
    /// <para><see href="https://dev.twitch.tv/docs/api/reference/#update-channel-guest-star-settings">
    /// Twitch Docs: Update Channel Guest Star Settings</see></para>
    /// <para>Mutates the channel settings for configuration of the Guest Star feature for a particular host.</para>
    /// </summary>
    /// <param name="broadcasterId"></param>
    /// <param name="newSettings"></param>
    /// <param name="accessToken"></param>
    /// <exception cref="BadParameterException"></exception>
    public Task UpdateChannelGuestStarSettingsAsync(string broadcasterId, UpdateChannelGuestStarSettingsRequest newSettings, string accessToken = null)
    {
        if (string.IsNullOrWhiteSpace(broadcasterId))
            throw new BadParameterException("broadcasterId must be set");

        if (newSettings == null)
            throw new BadParameterException("newSettings cannot be null");
        
        var getParams = new List<KeyValuePair<string, string>>
        {
            new ("broadcaster_id", broadcasterId)
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
    /// <param name="accessToken"> Optional access token to override the use of the stored one in the TwitchAPI instance</param>
    /// <returns cref="GetGuestStarSessionResponse"></returns>
    /// <exception cref="BadParameterException"></exception>
    public Task<GetGuestStarSessionResponse> GetGuestStarSessionAsync(string broadcasterId, string moderatorId, string accessToken = null)
    {
        if (string.IsNullOrWhiteSpace(broadcasterId))
            throw new BadParameterException("broadcasterId must be set");
        if (string.IsNullOrWhiteSpace(moderatorId))
            throw new BadParameterException("moderatorId must be set");
        
        var getParams = new List<KeyValuePair<string, string>>
        {
            new ("broadcaster_id", broadcasterId),
            new ("moderator_id", moderatorId)
        };
        
        return TwitchGetGenericAsync<GetGuestStarSessionResponse>("/guest_star/session", ApiVersion.Helix, getParams, accessToken);
    }

    #endregion
    
    #region CreateGuestStarSession

    /// <summary>
    /// <para><see href="https://dev.twitch.tv/docs/api/reference/#create-guest-star-session">
    /// Twitch Docs: Create Guest Star Session</see></para>
    /// <para>Programmatically creates a Guest Star session on behalf of the broadcaster. Requires the broadcaster to be present in the call interface, or the call will be ended automatically.</para>
    /// </summary>
    /// <param name="broadcasterId">The ID of the broadcaster you want to create a Guest Star session for. Provided broadcaster_id must match the user_id in the auth token.</param>
    /// <param name="accessToken"> Optional access token to override the use of the stored one in the TwitchAPI instance</param>
    /// <returns cref="CreateGuestStarSessionResponse"></returns>
    /// <exception cref="BadParameterException"></exception>
    public Task<CreateGuestStarSessionResponse> CreateGuestStarSession(string broadcasterId, string accessToken = null)
    {
        if (string.IsNullOrWhiteSpace(broadcasterId))
            throw new BadParameterException("broadcasterId must be set");
        
        var getParams = new List<KeyValuePair<string, string>>
        {
            new ("broadcaster_id", broadcasterId)
        };
        
        return TwitchPostGenericAsync<CreateGuestStarSessionResponse>("/guest_star/session", ApiVersion.Helix, null, getParams, accessToken);
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
    /// <param name="accessToken"> Optional access token to override the use of the stored one in the TwitchAPI instance</param>
    /// <returns cref="EndGuestStarSessionResponse"></returns>
    /// <exception cref="BadParameterException"></exception>
    public Task<EndGuestStarSessionResponse> EndGuestStarSession(string broadcasterId, string sessionId, string accessToken = null)
    {
        if (string.IsNullOrWhiteSpace(broadcasterId))
            throw new BadParameterException("broadcasterId must be set");
        
        if (string.IsNullOrWhiteSpace(sessionId))
            throw new BadParameterException("sessionId must be set");
        
        var getParams = new List<KeyValuePair<string, string>>
        {
            new ("broadcaster_id", broadcasterId),
            new ("sessionId", sessionId)
        };
        
        return TwitchDeleteGenericAsync<EndGuestStarSessionResponse>("/guest_star/session", ApiVersion.Helix, getParams, accessToken);
    }

    #endregion
}