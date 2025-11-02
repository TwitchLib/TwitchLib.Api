#nullable disable
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Videos.DeleteVideos;

/// <summary>
/// Delete videos response object.
/// </summary>
public class DeleteVideosResponse
{
    /// <summary>
    /// The list of IDs of the videos that were deleted.
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public string[] Data { get; protected set; }
}
