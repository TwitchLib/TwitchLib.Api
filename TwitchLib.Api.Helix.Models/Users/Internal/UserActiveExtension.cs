using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Users.Internal;

/// <summary>
/// A active extension.
/// </summary>
public class UserActiveExtension
{
    /// <summary>
    /// A Boolean value that determines the extension’s activation state. 
    /// </summary>
    [JsonProperty(PropertyName = "active")]
    public bool Active { get; protected set; }

    /// <summary>
    /// An ID that identifies the extension.
    /// </summary>
    [JsonProperty(PropertyName = "id")]
    public string Id { get; protected set; }

    /// <summary>
    /// The extension’s version.
    /// </summary>
    [JsonProperty(PropertyName = "version")]
    public string Version { get; protected set; }

    /// <summary>
    /// The extension’s name.
    /// </summary>
    [JsonProperty(PropertyName = "name")]
    public string Name { get; protected set; }

    /// <summary>
    /// The x-coordinate where the extension is placed.
    /// </summary>
    [JsonProperty(PropertyName = "x")]
    public int X { get; protected set; }

    /// <summary>
    /// The y-coordinate where the extension is placed.
    /// </summary>
    [JsonProperty(PropertyName = "y")]
    public int Y { get; protected set; }
}
