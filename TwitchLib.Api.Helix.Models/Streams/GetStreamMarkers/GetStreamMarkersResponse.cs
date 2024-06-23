using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Streams.GetStreamMarkers;

/// <summary>
/// Get stream markers response object.
/// </summary>
public class GetStreamMarkersResponse
{
    /// <summary>
    /// The list of markers grouped by the user that created the marks.
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public UserMarkerListing[] Data { get; protected set; }

    /// <summary>
    /// The information used to page through the list of results.
    /// </summary>
    [JsonProperty(PropertyName = "pagination")]
    public Pagination Pagination { get; protected set; }
}
