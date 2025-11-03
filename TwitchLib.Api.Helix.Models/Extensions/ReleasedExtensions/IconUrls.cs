#nullable disable
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Extensions.ReleasedExtensions;

/// <summary>
/// A list that contains URLs to different sizes of the default icon.
/// </summary>
public class IconUrls
{
    /// <summary>
    /// Size100x100
    /// </summary>
    [JsonProperty(PropertyName = "100x100")]
    public string Size100x100 { get; protected set; }

    /// <summary>
    /// Size24x24
    /// </summary>
    [JsonProperty(PropertyName = "24x24")]
    public string Size24x24 { get; protected set; }

    /// <summary>
    /// Size300x200
    /// </summary>
    [JsonProperty(PropertyName = "300x200")]
    public string Size300x200 { get; protected set; }
}
