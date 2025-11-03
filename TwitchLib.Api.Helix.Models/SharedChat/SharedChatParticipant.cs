#nullable disable
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.SharedChat;

/// <summary>
/// A participant in the shared chat session.
/// </summary>
public class SharedChatParticipant
{
    /// <summary>
    /// The User ID of the participant's channel.
    /// </summary>
    [JsonProperty(PropertyName = "broadcaster_id")]
    public string BroadcasterId { get; protected set; }
}