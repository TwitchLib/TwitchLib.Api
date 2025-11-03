#nullable disable
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Users.Internal;

/// <summary>
/// A active extension.
/// </summary>
public class ActiveExtensions
{
    /// <summary>
    /// A dictionary that contains the data for a panel extension. 
    /// </summary>
    [JsonProperty(PropertyName = "panel")]
    public Dictionary<string, UserActiveExtension> Panel { get; protected set; }

    /// <summary>
    /// A dictionary that contains the data for a video-overlay extension. 
    /// </summary>
    [JsonProperty(PropertyName = "overlay")]
    public Dictionary<string, UserActiveExtension> Overlay { get; protected set; }

    /// <summary>
    /// A dictionary that contains the data for a video-component extension.
    /// </summary>
    [JsonProperty(PropertyName = "component")]
    public Dictionary<string, UserActiveExtension> Component { get; protected set; }
}
