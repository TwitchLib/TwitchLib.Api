using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Moderation.AutomodSettings
{
    public class UpdateAutomodSettingsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public AutomodSettings[] Data { get; protected set; }
    }
}
