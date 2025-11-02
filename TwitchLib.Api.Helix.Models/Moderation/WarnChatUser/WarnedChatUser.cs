#nullable disable
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Moderation.WarnChatUser;

/// <summary>
/// Contains information about the warning.
/// </summary>
public class WarnedChatUser
{
    /// <summary>
    /// The ID of the channel in which the warning will take effect.
    /// </summary>
    [JsonProperty(PropertyName = "broadcaster_id")]
    public string BroadcasterId { get; protected set; }

    /// <summary>
    /// The ID of the warned user.
    /// </summary>
    [JsonProperty(PropertyName = "user_id")]
    public string UserId { get; protected set; }

    /// <summary>
    /// The ID of the user who applied the warning.
    /// </summary>
    [JsonProperty(PropertyName = "moderator_id")]
    public string ModeratorId { get; protected set; }

    /// <summary>
    /// The reason provided for warning.
    /// </summary>
    [JsonProperty(PropertyName = "reason")]
    public string Reason { get; protected set; }
}

