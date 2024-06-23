using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Moderation.CheckAutoModStatus;

/// <summary>
/// The message and whether Twitch would approve it for chat.
/// </summary>
public class AutoModResult
{
    /// <summary>
    /// The msg_id passed in the body of the POST message. Maps each message to its status.
    /// </summary>
    [JsonProperty(PropertyName = "msg_id")]
    public string MsgId { get; protected set; }

    /// <summary>
    /// Indicates if this message meets AutoMod requirements.
    /// </summary>
    [JsonProperty(PropertyName = "is_permitted")]
    public bool IsPermitted { get; protected set; }
}
