using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Moderation.AutomodSettings
{
    public class GetAutomodSettingsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public AutomodSettingsResponseModel[] Data { get; protected set; }
    }
}
