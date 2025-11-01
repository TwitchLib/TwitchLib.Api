using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Teams;

/// <summary>
/// Team Base model
/// </summary>
public abstract class TeamBase
{
    /// <summary>
    /// A URL to the team’s banner.
    /// </summary>
    [JsonProperty(PropertyName = "banner")]
    public string Banner { get; protected set; }

    /// <summary>
    /// A URL to the team’s background image.
    /// </summary>
    [JsonProperty(PropertyName = "background_image_url")]
    public string BackgroundImageUrl { get; protected set; }

    /// <summary>
    /// The UTC date and time (in RFC3339 format) of when the team was created.
    /// </summary>
    [JsonProperty(PropertyName = "created_at")]
    public string CreatedAt { get; protected set; }

    /// <summary>
    /// The UTC date and time (in RFC3339 format) of the last time the team was updated.
    /// </summary>
    [JsonProperty(PropertyName = "updated_at")]
    public string UpdatedAt { get; protected set; }

    /// <summary>
    /// The team’s description.
    /// The description may contain formatting such as Markdown, HTML, newline (\n) characters, etc.
    /// </summary>
    [JsonProperty(PropertyName = "info")]
    public string Info { get; protected set; }

    /// <summary>
    /// A URL to a thumbnail image of the team’s logo.
    /// </summary>
    [JsonProperty(PropertyName = "thumbnail_url")]
    public string ThumbnailUrl { get; protected set; }

    /// <summary>
    /// The team’s name.
    /// </summary>
    [JsonProperty(PropertyName = "team_name")]
    public string TeamName { get; protected set; }

    /// <summary>
    /// The team’s display name.
    /// </summary>
    [JsonProperty(PropertyName = "team_display_name")]
    public string TeamDisplayName { get; protected set; }

    /// <summary>
    /// An ID that identifies the team.
    /// </summary>
    [JsonProperty(PropertyName = "id")]
    public string Id { get; protected set; }
}