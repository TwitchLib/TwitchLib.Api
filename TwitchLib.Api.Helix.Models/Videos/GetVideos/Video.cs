#nullable disable
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Videos.GetVideos;

/// <summary>
/// Information about a video.
/// </summary>
public class Video
{
    /// <summary>
    /// The date and time, in UTC, of when the video was created. The timestamp is in RFC3339 format.
    /// </summary>
    [JsonProperty(PropertyName = "created_at")]
    public string CreatedAt { get; protected set; }

    /// <summary>
    /// The video's description.
    /// </summary>
    [JsonProperty(PropertyName = "description")]
    public string Description { get; protected set; }

    /// <summary>
    /// The video's length in ISO 8601 duration format. For example, 3m21s represents 3 minutes, 21 seconds.
    /// </summary>
    [JsonProperty(PropertyName = "duration")]
    public string Duration { get; protected set; }

    /// <summary>
    /// An ID that identifies the video.
    /// </summary>
    [JsonProperty(PropertyName = "id")]
    public string Id { get; protected set; }

    /// <summary>
    /// he ISO 639-1 two-letter language code that the video was broadcast in.
    /// </summary>
    [JsonProperty(PropertyName = "language")]
    public string Language { get; protected set; }

    /// <summary>
    /// The date and time, in UTC, of when the video was published. The timestamp is in RFC3339 format.
    /// </summary>
    [JsonProperty(PropertyName = "published_at")]
    public string PublishedAt { get; protected set; }

    /// <summary>
    /// A URL to a thumbnail image of the video.
    /// </summary>
    [JsonProperty(PropertyName = "thumbnail_url")]
    public string ThumbnailUrl { get; protected set; }

    /// <summary>
    /// The video's title.
    /// </summary>
    [JsonProperty(PropertyName = "title")]
    public string Title { get; protected set; }

    /// <summary>
    /// The video's type.
    /// </summary>
    [JsonProperty(PropertyName = "type")]
    public string Type { get; protected set; }

    /// <summary>
    /// The video's URL.
    /// </summary>
    [JsonProperty(PropertyName = "url")]
    public string Url { get; protected set; }

    /// <summary>
    /// The ID of the broadcaster that owns the video.
    /// </summary>
    [JsonProperty(PropertyName = "user_id")]
    public string UserId { get; protected set; }

    /// <summary>
    /// The broadcaster's login name.
    /// </summary>
    [JsonProperty(PropertyName = "user_login")]
    public string UserLogin { get; protected set; }

    /// <summary>
    /// The broadcaster's display name.
    /// </summary>
    [JsonProperty(PropertyName = "user_name")]
    public string UserName { get; protected set; }

    /// <summary>
    /// The video's viewable state. Always set to public.
    /// </summary>
    [JsonProperty(PropertyName = "viewable")]
    public string Viewable { get; protected set; }

    /// <summary>
    /// The number of times that users have watched the video.
    /// </summary>
    [JsonProperty(PropertyName = "view_count")]
    public int ViewCount { get; protected set; }

    /// <summary>
    /// The ID of the stream that the video originated from if the video's type is "archive;" otherwise, null.
    /// </summary>
    [JsonProperty(PropertyName = "stream_id")]
    public string StreamId { get; protected set; }

    /// <summary>
    /// The segments that Twitch Audio Recognition muted.
    /// </summary>
    [JsonProperty(PropertyName = "muted_segments")]
    public MutedSegment[] MutedSegments { get; protected set; }
}
