#nullable disable
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Extensions.ReleasedExtensions;

/// <summary>
/// Describes view-related information such as how the extension is displayed on mobile devices.
/// </summary>
public class Views
{
    /// <summary>
    /// Describes how the extension is displayed on mobile devices.
    /// </summary>
    [JsonProperty(PropertyName = "mobile")]
    public Mobile Mobile { get; protected set; }

    /// <summary>
    /// Describes how the extension is rendered if the extension may be activated as a panel extension.
    /// </summary>
    [JsonProperty(PropertyName = "panel")]
    public Panel Panel { get; protected set; }

    /// <summary>
    /// Describes how the extension is rendered if the extension may be activated as a video-overlay extension.
    /// </summary>
    [JsonProperty(PropertyName = "video_overlay")]
    public VideoOverlay VideoOverlay { get; protected set; }

    /// <summary>
    /// Describes how the extension is rendered if the extension may be activated as a video-component extension.
    /// </summary>
    [JsonProperty(PropertyName = "component")]
    public Component Component { get; protected set; }
}
