#nullable disable
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.GuestStar.GetChannelGuestStarSettings;
using TwitchLib.Api.Helix.Models.SharedChat.GetSharedChatSession;

namespace TwitchLib.Api.Helix;

/// <summary>
/// Shared Chat related APIs
/// </summary>
public class SharedChat : ApiBase
{
    public SharedChat(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings,
        rateLimiter, http)
    {}

    #region GetSharedChatSession

    /// <summary>
    /// <para><see href="https://dev.twitch.tv/docs/api/reference/#get-shared-chat-session">
    /// Twitch Docs: Get Shared Chat Session</see></para>
    /// <para>Retrieves the active shared chat session for a channel.</para>
    /// </summary>
    /// <param name="broadcasterId">The User ID of the channel broadcaster.</param>
    /// <param name="accessToken">Optional access token to override the use of the stored one in the TwitchAPI instance</param>
    /// <returns cref="GetChannelGuestStarSettingsResponse"></returns>
    /// <exception cref="BadParameterException"></exception>
    public Task<GetSharedChatSessionResponse> GetSharedChatSessionAsync(string broadcasterId, string accessToken = null)
    {
        if (string.IsNullOrWhiteSpace(broadcasterId))
            throw new BadParameterException("broadcasterId must be set");

        var getParams = new List<KeyValuePair<string, string>>
        {
            new("broadcaster_id", broadcasterId)
        };

        return TwitchGetGenericAsync<GetSharedChatSessionResponse>("/shared_chat/session", ApiVersion.Helix, getParams, accessToken);
    }

    #endregion
}