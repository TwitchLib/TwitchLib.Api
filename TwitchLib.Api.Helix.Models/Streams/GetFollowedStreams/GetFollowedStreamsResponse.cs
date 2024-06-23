using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Streams.GetFollowedStreams;

/// <summary>
/// Get followed streams response object.
/// </summary>
public class GetFollowedStreamsResponse
{
    /// <summary>
    /// The list of live streams of broadcasters that the specified user follows.
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public Stream[] Data { get; protected set; }

    /// <summary>
    /// The information used to page through the list of results.
    /// </summary>
    [JsonProperty(PropertyName = "pagination")]
    public Pagination Pagination { get; protected set; }
}
