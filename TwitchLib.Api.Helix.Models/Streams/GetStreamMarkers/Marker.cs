using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Streams.GetStreamMarkers;

/// <summary>
/// A marker in a video.
/// </summary>
public class Marker
{
    /// <summary>
    /// An ID that identifies this marker.
    /// </summary>
    [JsonProperty(PropertyName = "id")]
    public string Id { get; protected set; }

    /// <summary>
    /// The UTC date and time (in RFC3339 format) of when the user created the marker.
    /// </summary>
    [JsonProperty(PropertyName = "created_at")]
    public DateTime CreatedAt { get; protected set; }

    /// <summary>
    /// The description that the user gave the marker to help them remember why they marked the location.
    /// </summary>
    [JsonProperty(PropertyName = "description")]
    public string Description { get; protected set; }

    /// <summary>
    /// The relative offset (in seconds) of the marker from the beginning of the stream.
    /// </summary>
    [JsonProperty(PropertyName = "position_seconds")]
    public int PositionSeconds { get; protected set; }

    /// <summary>
    /// A URL that opens the video in Twitch Highlighter.
    /// </summary>
    [JsonProperty(PropertyName = "URL")]
    public string Url { get; protected set; }
}
