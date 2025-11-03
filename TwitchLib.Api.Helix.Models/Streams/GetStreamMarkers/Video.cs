#nullable disable
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Streams.GetStreamMarkers;

/// <summary>
/// A video that contains markers.
/// </summary>
public class Video
{
    /// <summary>
    /// An ID that identifies this video.
    /// </summary>
    [JsonProperty(PropertyName = "video_id")]
    public string VideoId { get; protected set; }

    /// <summary>
    /// The list of markers in this video.
    /// </summary>
    [JsonProperty(PropertyName = "markers")]
    public Marker[] Markers { get; protected set; }
}
