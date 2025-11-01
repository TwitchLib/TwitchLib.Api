using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.GuestStar;

namespace TwitchLib.Api.Helix.Models.SharedChat.GetSharedChatSession;

/// <summary>
/// Get shared chat session response object.
/// </summary>
public class GetSharedChatSessionResponse
{
    /// <summary>
    /// <para>A list that contains the channels shared chat sessions</para>
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public SharedChatSession[] Data { get; protected set; }
}