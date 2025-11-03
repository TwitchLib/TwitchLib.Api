#nullable disable
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Moderation.AutomodSettings;

/// <summary>
/// Get automod settings response object.
/// </summary>
public class GetAutomodSettingsResponse
{
    /// <summary>
    /// The list of AutoMod settings. 
    /// The list contains a single object that contains all the AutoMod settings.
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public AutomodSettingsResponseModel[] Data { get; protected set; }
}
