#nullable disable
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.GuestStar.UpdateChannelGuestStarSettings;

/// <summary>
/// <para>Request to update guest star settings</para>
/// </summary>
public class UpdateChannelGuestStarSettingsRequest : GuestStarSettingsBase
{
    /// <summary>
    /// Flag determining if Guest Star should regenerate the auth token associated with the channel’s browser sources.
    /// <para>Providing a true value for this will immediately invalidate all browser sources previously configured in your streaming software.</para>
    /// </summary>
    [JsonProperty(PropertyName = "regenerate_browser_sources")]
    public bool RegenerateBrowserSources { get; protected set; }
}