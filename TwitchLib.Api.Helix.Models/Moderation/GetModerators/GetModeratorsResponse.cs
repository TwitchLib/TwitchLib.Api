using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Moderation.GetModerators;

/// <summary>
/// Get moderators response object.
/// </summary>
public class GetModeratorsResponse
{
    /// <summary>
    /// The list of moderators.
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public Moderator[] Data { get; protected set; }

    /// <summary>
    /// Contains the information used to page through the list of results.
    /// </summary>
    [JsonProperty(PropertyName = "pagination")]
    public Pagination Pagination { get; protected set; }
}
