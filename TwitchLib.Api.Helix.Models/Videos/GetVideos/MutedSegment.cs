using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Videos.GetVideos;

/// <summary>
/// A segments that Twitch Audio Recognition muted
/// </summary>
public class MutedSegment
{
    /// <summary>
    /// The duration of the muted segment, in seconds.
    /// </summary>
    [JsonProperty(PropertyName = "duration")]
    public int Duration { get; protected set; }

    /// <summary>
    /// The offset, in seconds, from the beginning of the video to where the muted segment begins.
    /// </summary>
    [JsonProperty(PropertyName = "offset")]
    public int Offset { get; protected set; }
}