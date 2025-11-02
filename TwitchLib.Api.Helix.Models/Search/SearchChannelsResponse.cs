#nullable disable
using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Search;

/// <summary>
/// Search channels response object.
/// </summary>
public class SearchChannelsResponse
{
    /// <summary>
    /// The list of channels that match the query.
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public Channel[] Channels { get; protected set; }

    /// <summary>
    /// The information used to page through the list of results.
    /// </summary>
    [JsonProperty(PropertyName = "pagination")]
    public Pagination Pagination { get; protected set; }
}
