using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.GuestStar;

public class GuestStarGuest
{
    /// <summary>
    /// ID representing this guest’s slot assignment.
    /// <para>Host is always in slot "0"</para>
    /// <para>Guests are assigned the following consecutive IDs (e.g, "1", "2", "3", etc)</para>
    /// <para>Screen Share is represented as a special guest with the ID "SCREENSHARE"</para>
    /// <para>The identifier here matches the ID referenced in browser source links used in broadcasting software.</para>
    /// </summary>
    [JsonProperty(PropertyName = "slot_id")]
    public string SlotId { get; protected set; }
    
    /// <summary>
    /// Flag determining whether or not the guest is visible in the browser source in the host’s streaming software.
    /// </summary>
    [JsonProperty(PropertyName = "is_live")]
    public bool IsLive { get; protected set; }
    
    /// <summary>
    /// User ID of the guest assigned to this slot.
    /// </summary>
    [JsonProperty(PropertyName = "user_id")]
    public string UserId { get; protected set; }
    
    /// <summary>
    /// Display name of the guest assigned to this slot.
    /// </summary>
    [JsonProperty(PropertyName = "user_display_name")]
    public string UserDisplayName { get; protected set; }
    
    /// <summary>
    /// Login of the guest assigned to this slot.
    /// </summary>
    [JsonProperty(PropertyName = "user_login")]
    public string UserLogin { get; protected set; }
    
    /// <summary>
    /// Value from 0 to 100 representing the host’s volume setting for this guest.
    /// </summary>
    [JsonProperty(PropertyName = "volume")]
    public int Volume { get; protected set; }
    
    /// <summary>
    /// Timestamp when this guest was assigned a slot in the session.
    /// </summary>
    [JsonProperty(PropertyName = "assigned_at")]
    public string AssignedAt { get; protected set; }
    
    /// <summary>
    /// Information about the guest’s audio settings
    /// </summary>
    [JsonProperty(PropertyName = "audio_settings")]
    public GuestStarMediaSettings AudioSettings { get; protected set; }
    
    /// <summary>
    /// Information about the guest’s video settings
    /// </summary>
    [JsonProperty(PropertyName = "video_settings")]
    public GuestStarMediaSettings VideoSettings { get; protected set; }
}