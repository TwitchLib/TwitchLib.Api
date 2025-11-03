#nullable disable
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Channels.SendChatMessage;

public class DropReason
{
    /// <summary>
    /// 	Code for why the message was dropped.
    /// </summary>
    [JsonProperty(PropertyName = "code", NullValueHandling = NullValueHandling.Ignore)]
    public string Code { get; set; } = string.Empty;
    /// <summary>
    /// Message for why the message was dropped.
    /// </summary>
    [JsonProperty(PropertyName = "message", NullValueHandling = NullValueHandling.Ignore)]
    public string Message { get; set; } = string.Empty;
}
