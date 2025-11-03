#nullable disable
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Moderation.CheckAutoModStatus;

/// <summary>
/// Check automod status response object.
/// </summary>
public class CheckAutoModStatusResponse
{
    /// <summary>
    /// The list of messages and whether Twitch would approve them for chat.
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public AutoModResult[] Data { get; protected set; }
}
