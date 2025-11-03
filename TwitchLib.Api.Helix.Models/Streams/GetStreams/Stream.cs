#nullable disable
using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Streams.GetStreams;

/// <summary>
/// 
/// </summary>
public class Stream
{
    /// <summary>
    /// An ID that identifies the stream.
    /// </summary>
    [JsonProperty(PropertyName = "id")]
    public string Id { get; protected set; }

    /// <summary>
    /// The ID of the user that’s broadcasting the stream.
    /// </summary>
    [JsonProperty(PropertyName = "user_id")]
    public string UserId { get; protected set; }

    /// <summary>
    /// The user’s login name.
    /// </summary>
    [JsonProperty(PropertyName = "user_login")]
    public string UserLogin { get; protected set; }

    /// <summary>
    /// The user’s display name.
    /// </summary>
    [JsonProperty(PropertyName = "user_name")]
    public string UserName { get; protected set; }

    /// <summary>
    /// The ID of the category or game being played.
    /// </summary>
    [JsonProperty(PropertyName = "game_id")]
    public string GameId { get; protected set; }

    /// <summary>
    /// The name of the category or game being played.
    /// </summary>
    [JsonProperty(PropertyName = "game_name")]
    public string GameName { get; protected set; }

    /// <summary>
    /// The type of stream.
    /// </summary>
    [JsonProperty(PropertyName = "type")]
    public string Type { get; protected set; }

    /// <summary>
    /// The stream’s title.
    /// </summary>
    [JsonProperty(PropertyName = "title")]
    public string Title { get; protected set; }

    /// <summary>
    /// The tags applied to the stream.
    /// </summary>
    [JsonProperty(PropertyName = "tags")]
    public string[] Tags { get; protected set; }

    /// <summary>
    /// The number of users watching the stream.
    /// </summary>
    [JsonProperty(PropertyName = "viewer_count")]
    public int ViewerCount { get; protected set; }

    /// <summary>
    /// The UTC date and time (in RFC3339 format) of when the broadcast began.
    /// </summary>
    [JsonProperty(PropertyName = "started_at")]
    public DateTime StartedAt { get; protected set; }

    /// <summary>
    /// The language that the stream uses.
    /// </summary>
    [JsonProperty(PropertyName = "language")]
    public string Language { get; protected set; }

    /// <summary>
    /// A URL to an image of a frame from the last 5 minutes of the stream. 
    /// </summary>
    [JsonProperty(PropertyName = "thumbnail_url")]
    public string ThumbnailUrl { get; protected set; }

    /// <summary>
    /// IMPORTANT As of February 28, 2023, this field is deprecated and returns only an empty array. 
    /// </summary>
    [Obsolete]
    [JsonProperty(PropertyName = "tag_ids")]
    public string[] TagIds { get; protected set; }

    /// <summary>
    /// A Boolean value that indicates whether the stream is meant for mature audiences.
    /// </summary>
    [JsonProperty(PropertyName = "is_mature")]
    public bool IsMature { get; protected set; }
}
