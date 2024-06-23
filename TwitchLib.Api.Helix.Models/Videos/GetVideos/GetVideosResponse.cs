using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Videos.GetVideos;

/// <summary>
/// Get videos response object.
/// </summary>
public class GetVideosResponse
{
    /// <summary>
    /// The list of published videos that match the filter criteria.
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public Video[] Videos { get; protected set; }

    /// <summary>
    /// Contains the information used to page through the list of results.
    /// </summary>
    [JsonProperty(PropertyName = "pagination")]
    public Pagination Pagination { get; protected set; }
}
