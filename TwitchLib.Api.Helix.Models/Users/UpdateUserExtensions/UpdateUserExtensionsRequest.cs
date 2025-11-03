#nullable disable
using System.Collections.Generic;
using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Users.Internal;

namespace TwitchLib.Api.Helix.Models.Users.UpdateUserExtensions;

/// <summary>
/// Update user extensions request object.
/// </summary>
public class UpdateUserExtensionsRequest
{
    /// <summary>
    /// A dictionary that contains the data for a panel extension. 
    /// </summary>
    [JsonProperty(PropertyName = "panel")]
    public Dictionary<string, UserExtensionState> Panel { get; set; }

    /// <summary>
    /// A dictionary that contains the data for a video-component extension.
    /// </summary>
    [JsonProperty(PropertyName = "component")]
    public Dictionary<string, UserExtensionState> Component { get; set; }

    /// <summary>
    /// A dictionary that contains the data for a video-overlay extension.
    /// </summary>
    [JsonProperty(PropertyName = "overlay")]
    public Dictionary<string, UserExtensionState> Overlay { get; set; }
}
