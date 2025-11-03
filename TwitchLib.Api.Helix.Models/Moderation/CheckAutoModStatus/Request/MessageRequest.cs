#nullable disable
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Moderation.CheckAutoModStatus.Request;

/// <summary>
/// Message request object.
/// </summary>
public class MessageRequest
{
    /// <summary>
    /// The list of messages to check.
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public Message[] Messages { get; set; }
}
