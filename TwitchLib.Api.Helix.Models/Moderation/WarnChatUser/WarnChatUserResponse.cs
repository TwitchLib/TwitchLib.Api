using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Moderation.WarnChatUser;

/// <summary>
/// Warn chat user response object.
/// </summary>
public class WarnChatUserResponse
{
    /// <summary>
    /// A list that contains information about the warning.
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public WarnedChatUser[] Data { get; protected set; }
}

