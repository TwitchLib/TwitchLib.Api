#nullable disable
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.HypeTrain;
using TwitchLib.Api.Helix.Models.Moderation.GetModerators;

namespace TwitchLib.Api.Helix;

/// <summary>
/// HypeTrain related APIs
/// </summary>
public class HypeTrain : ApiBase
{
    public HypeTrain(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
    {
    }

    #region GetHypeTrainEvents

    /// <summary>
    /// Gets the information of the most recent Hype Train of the given channel ID.
    /// <para>When there is currently an active Hype Train, it returns information about that Hype Train. </para>
    /// <para>When there is currently no active Hype Train, it returns information about the most recent Hype Train.</para>
    /// <para>After 5 days, if no Hype Train has been active, the endpoint will return an empty response.</para>
    /// <para>Required scope: channel:read:hype_train</para>
    /// </summary>
    /// <param name="broadcasterId">User ID of the broadcaster. Must match the User ID in the Access Token.</param>
    /// <param name="first">Maximum number of objects to return. Maximum: 100. Default: 1.</param>
    /// <param name="cursor">Cursor for forward pagination: tells the server where to start fetching the next set of results in a multi-page response.</param>
    /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
    /// <returns cref="GetHypeTrainResponse"></returns>
    /// <exception cref="BadParameterException"></exception>
    public Task<GetHypeTrainResponse> GetHypeTrainEventsAsync(string broadcasterId, int first = 1, string cursor = null, string accessToken = null)
    {
        BadParameterException.ThrowIfNullOrEmpty(broadcasterId);

        var getParams = new List<KeyValuePair<string, string>>
        {
            new("broadcaster_id", broadcasterId),
            new("first", first.ToString())
        };

        if (!string.IsNullOrWhiteSpace(cursor))
            getParams.Add(new KeyValuePair<string, string>("cursor", cursor));

        return TwitchGetGenericAsync<GetHypeTrainResponse>("/hypetrain/events", ApiVersion.Helix, getParams, accessToken);
    }
    #endregion
}
