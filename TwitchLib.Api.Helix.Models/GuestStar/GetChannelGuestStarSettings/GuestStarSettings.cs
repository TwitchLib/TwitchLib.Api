using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.GuestStar.GetChannelGuestStarSettings;

public class GuestStarSettings : GuestStarSettingsBase
{
    /// <summary>
    /// View only token to generate browser source URLs
    /// </summary>
    [JsonProperty(PropertyName = "browser_source_token")]
    public string BrowserSourceToken { get; protected set; }
}