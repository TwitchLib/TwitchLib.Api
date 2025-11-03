#nullable disable
using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Streams.GetStreams;

/// <summary>
/// Get streams response object.
/// </summary>
public class GetStreamsResponse
{
    /// <summary>
    /// The list of streams.
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public Stream[] Streams { get; protected set; }

    /// <summary>
    /// The information used to page through the list of results.
    /// </summary>
    [JsonProperty(PropertyName = "pagination")]
    public Pagination Pagination { get; protected set; }
}
