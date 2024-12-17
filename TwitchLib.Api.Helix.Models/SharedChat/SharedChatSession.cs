using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.SharedChat;

/// <summary>
/// The shared chat session details
/// </summary>
public class SharedChatSession
{
    /// <summary>
    /// The unique identifier for the shared chat session.
    /// </summary>
    [JsonProperty(PropertyName = "session_id")]
    public string SessionId { get; protected set; }

    /// <summary>
    /// The User ID of the host channel.
    /// </summary>
    [JsonProperty(PropertyName = "host_broadcaster_id")]
    public string HostBroadcasterId { get; protected set; }
    
    /// <summary>
    /// The list of participants in the session.
    /// </summary>
    [JsonProperty(PropertyName = "participants")]
    public SharedChatParticipant[] Participants { get; protected set; }
    
    /// <summary>
    /// The UTC date and time (in RFC3339 format) for when the session was created.
    /// </summary>
    [JsonProperty(PropertyName = "created_at")]
    public string CreatedAt { get; protected set; }
    
    /// <summary>
    /// The UTC date and time (in RFC3339 format) for when the session was last updated.
    /// </summary>
    [JsonProperty(PropertyName = "updated_at")]
    public string UpdatedAt { get; protected set; }
}