using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Extensions.ReleasedExtensions;

/// <summary>
/// Describes how the extension is displayed on mobile devices.
/// </summary>
public class Mobile
{
    /// <summary>
    /// The HTML file that is shown to viewers on mobile devices. This page is presented to viewers as a panel behind the chat area of the mobile app.
    /// </summary>
    [JsonProperty(PropertyName = "viewer_url")]
    public string ViewerUrl { get; protected set; }
}
