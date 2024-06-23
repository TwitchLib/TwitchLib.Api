using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Clips.GetClips;

/// <summary>
/// A clip that was created from a broadcaster's stream.
/// </summary>
public class Clip
{
  /// <summary>
  /// An ID that uniquely identifies the clip.
  /// </summary>
  [JsonProperty(PropertyName = "id")]
  public string Id { get; protected set; }

  /// <summary>
  /// A URL to the clip.
  /// </summary>
  [JsonProperty(PropertyName = "url")]
  public string Url { get; protected set; }

  /// <summary>
  /// A URL that you can use in an iframe to embed the clip.
  /// </summary>
  [JsonProperty(PropertyName = "embed_url")]
  public string EmbedUrl { get; protected set; }

  /// <summary>
  /// An ID that identifies the broadcaster that the video was clipped from.
  /// </summary>
  [JsonProperty(PropertyName = "broadcaster_id")]
  public string BroadcasterId { get; protected set; }

  /// <summary>
  /// The broadcaster’s display name.
  /// </summary>
  [JsonProperty(PropertyName = "broadcaster_name")]
  public string BroadcasterName { get; protected set; }

  /// <summary>
  /// An ID that identifies the user that created the clip.
  /// </summary>
  [JsonProperty(PropertyName = "creator_id")]
  public string CreatorId { get; protected set; }

  /// <summary>
  /// The display name of the user that created the clip.
  /// </summary>
  [JsonProperty(PropertyName = "creator_name")]
  public string CreatorName { get; protected set; }

  /// <summary>
  /// <para>An ID that identifies the video that the clip came from.</para>
  /// <para>This field contains an empty string if the video is not available.</para>
  /// </summary>
  [JsonProperty(PropertyName = "video_id")]
  public string VideoId { get; protected set; }

  /// <summary>
  /// The ID of the game that was being played when the clip was created.
  /// </summary>
  [JsonProperty(PropertyName = "game_id")]
  public string GameId { get; protected set; }

  /// <summary>
  /// <para>The ISO 639-1 two-letter language code that the broadcaster broadcasts in. For example, en for English.</para>
  /// <para>The value is other if the broadcaster uses a language that Twitch doesn’t support.</para>
  /// </summary>
  [JsonProperty(PropertyName = "language")]
  public string Language { get; protected set; }

  /// <summary>
  /// The title of the clip.
  /// </summary>
  [JsonProperty(PropertyName = "title")]
  public string Title { get; protected set; }

  /// <summary>
  /// The number of times the clip has been viewed.
  /// </summary>
  [JsonProperty(PropertyName = "view_count")]
  public int ViewCount { get; protected set; }

  /// <summary>
  /// The date and time of when the clip was created. The date and time is in RFC3339 format.
  /// </summary>
  [JsonProperty(PropertyName = "created_at")]
  public string CreatedAt { get; protected set; }

  /// <summary>
  /// A URL to a thumbnail image of the clip.
  /// </summary>
  [JsonProperty(PropertyName = "thumbnail_url")]
  public string ThumbnailUrl { get; protected set; }

  /// <summary>
  /// The length of the clip, in seconds. Precision is 0.1.
  /// </summary>
  [JsonProperty(PropertyName = "duration")]
  public float Duration { get; protected set; }

  /// <summary>
  /// <para>The zero-based offset, in seconds, to where the clip starts in the video (VOD).</para>
  /// <para>Is null if the video is not available or hasn’t been created yet from the live stream (see video_id).</para>
  /// <para>Note that there’s a delay between when a clip is created during a broadcast and when the offset is set. During the delay period, vod_offset is null. The delay is indeterminant but is typically minutes long.</para>
  /// </summary>
  [JsonProperty(PropertyName = "vod_offset")]
  public int VodOffset { get; protected set; }
  
  /// <summary>
  /// A Boolean value that indicates if the clip is featured or not.
  /// </summary>
  [JsonProperty(PropertyName = "is_featured")]
  public bool IsFeatured { get; protected set; }
}
