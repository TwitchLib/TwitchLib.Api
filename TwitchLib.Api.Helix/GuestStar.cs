using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.GuestStar.GetChannelGuestStarSettings;
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
}