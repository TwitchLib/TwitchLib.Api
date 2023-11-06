using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.GuestStar.GetGuestStarInvites;

public class GuestStarInvite
{
    /// <summary>
    /// Twitch User ID corresponding to the invited guest
    /// </summary>
    [JsonProperty(PropertyName = "user_id")]
    public string UserId { get; protected set; }
    
    /// <summary>
    /// Timestamp when this user was invited to the session.
    /// </summary>
    [JsonProperty(PropertyName = "invited_at")]
    public string InvitedAt { get; protected set; }
    
    /// <summary>
    /// Status representing the invited user’s join state. Can be one of the following:
    /// <para>INVITED: The user has been invited to the session but has not acknowledged it.</para>
    /// <para>ACCEPTED: The invited user has acknowledged the invite and joined the waiting room, but may still be setting up their media devices or otherwise preparing to join the call.</para>
    /// <para>READY: The invited user has signaled they are ready to join the call from the waiting room.</para>
    /// </summary>
    [JsonProperty(PropertyName = "status")]
    public string Status { get; protected set; }
    
    /// <summary>
    /// Flag signaling that the invited user has chosen to disable their local video device. The user has hidden themselves, but they may choose to reveal their video feed upon joining the session.
    /// </summary>
    [JsonProperty(PropertyName = "is_video_enabled")]
    public bool IsVideoEnabled { get; protected set; }
    
    /// <summary>
    /// Flag signaling that the invited user has chosen to disable their local audio device. The user has muted themselves, but they may choose to unmute their audio feed upon joining the session.
    /// </summary>
    [JsonProperty(PropertyName = "is_audio_enabled")]
    public bool IsAudioEnabled { get; protected set; }
    
    /// <summary>
    /// Flag signaling that the invited user has chosen to disable their local video device. The user has hidden themselves, but they may choose to reveal their video feed upon joining the session.
    /// </summary>
    [JsonProperty(PropertyName = "is_video_available")]
    public bool IsVideoAvailable { get; protected set; }
    
    /// <summary>
    /// Flag signaling that the invited user has chosen to disable their local audio device. The user has muted themselves, but they may choose to unmute their audio feed upon joining the session.
    /// </summary>
    [JsonProperty(PropertyName = "is_audio_available")]
    public bool IsAudioAvailable { get; protected set; }
}