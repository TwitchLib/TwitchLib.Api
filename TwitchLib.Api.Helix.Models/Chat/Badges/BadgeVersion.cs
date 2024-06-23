using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Chat.Badges;

/// <summary>
/// Badge version of the badge set.
/// </summary>
public class BadgeVersion
{
    /// <summary>
    /// An ID that identifies this version of the badge.
    /// </summary>
    [JsonProperty(PropertyName = "id")]
    public string Id { get; protected set; }

    /// <summary>
    /// A URL to the small version (18px x 18px) of the badge.
    /// </summary>
    [JsonProperty(PropertyName = "image_url_1x")]
    public string ImageUrl1x { get; protected set; }

    /// <summary>
    /// A URL to the medium version (36px x 36px) of the badge.
    /// </summary>
    [JsonProperty(PropertyName = "image_url_2x")]
    public string ImageUrl2x { get; protected set; }

    /// <summary>
    /// A URL to the large version (72px x 72px) of the badge.
    /// </summary>
    [JsonProperty(PropertyName = "image_url_4x")]
    public string ImageUrl4x { get; protected set; }

    /// <summary>
    /// The title of the badge.
    /// </summary>
    [JsonProperty(PropertyName = "title")]
    public string Title { get; protected set; }

    /// <summary>
    /// The description of the badge.
    /// </summary>
    [JsonProperty(PropertyName = "description")]
    public string Description { get; protected set; }

    /// <summary>
    /// The action to take when clicking on the badge. Set to `null` if no action is specified.
    /// </summary>
    [JsonProperty(PropertyName = "click_action")]
    public string ClickAction { get; protected set; }

    /// <summary>
    /// The URL to navigate to when clicking on the badge. Set to `null` if no URL is specified.
    /// </summary>
    [JsonProperty(PropertyName = "click_url")]
    public string ClickUrl { get; protected set; }
}
