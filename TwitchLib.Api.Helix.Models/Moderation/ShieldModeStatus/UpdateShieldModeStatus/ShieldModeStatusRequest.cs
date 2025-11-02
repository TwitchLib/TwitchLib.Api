#nullable disable
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Moderation.ShieldModeStatus.UpdateShieldModeStatus;

/// <summary>
/// Shield mode status request object.
/// </summary>
public class ShieldModeStatusRequest
{
    /// <summary>
    /// A Boolean value that determines whether to activate Shield Mode.
    /// Set to true to activate Shield Mode; otherwise, false to deactivate Shield Mode.
    /// </summary>
    [JsonProperty("is_active")]
    public bool IsActive { get; set; }
}