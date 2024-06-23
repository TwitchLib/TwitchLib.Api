using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Extensions.ReleasedExtensions;

/// <summary>
/// Describes how the extension is rendered if the extension may be activated as a video-component extension.
/// </summary>
public class Component
{
    /// <summary>
    /// The HTML file that is shown to viewers on the channel page when the extension is activated in a Video - Component slot.
    /// </summary>
    [JsonProperty(PropertyName = "viewer_url")]
    public string ViewerUrl { get; protected set; }

    /// <summary>
    /// The width
    /// </summary>
    [JsonProperty(PropertyName = "aspect_width")]
    public int AspectWidth { get; protected set; }

    /// <summary>
    /// The height 
    /// </summary>
    [JsonProperty(PropertyName = "aspect_height")]
    public int AspectHeight { get; protected set; }

    /// <summary>
    /// The width value of the ratio (width : height) which determines the extension’s width, and how the extension’s iframe will resize in different video player environments.
    /// </summary>
    [JsonProperty(PropertyName = "aspect_ratio_x")]
    public int AspectRatioX { get; protected set; }

    /// <summary>
    /// The height value of the ratio (width : height) which determines the extension’s height, and how the extension’s iframe will resize in different video player environments.
    /// </summary>
    [JsonProperty(PropertyName = "aspect_ratio_y")]
    public int AspectRatioY { get; protected set; }

    /// <summary>
    /// A Boolean value that determines whether to apply CSS zoom.
    /// </summary>
    [JsonProperty(PropertyName = "autoscale")]
    public bool Autoscale { get; protected set; }

    /// <summary>
    /// The base width, in pixels, of the extension to use when scaling.
    /// </summary>
    [JsonProperty(PropertyName = "scale_pixels")]
    public int ScalePixels { get; protected set; }

    /// <summary>
    /// The height as a percent of the maximum height of a video component extension. Values are between 1% - 100%.
    /// </summary>
    [JsonProperty(PropertyName = "target_height")]
    public int TargetHeight { get; protected set; }

    /// <summary>
    /// Size
    /// </summary>
    [JsonProperty(PropertyName = "size")]
    public int Size { get; protected set; }

    /// <summary>
    /// Zoom
    /// </summary>
    [JsonProperty(PropertyName = "zoom")]
    public bool Zoom { get; protected set; }

    /// <summary>
    /// Zoom Pixels
    /// </summary>
    [JsonProperty(PropertyName = "zoom_pixels")]
    public int ZoomPixels { get; protected set; }

    /// <summary>
    /// A Boolean value that determines whether the extension can link to non-Twitch domains.
    /// </summary>
    [JsonProperty(PropertyName = "can_link_external_content")]
    public string CanLinkExternalContent { get; protected set; }
}
