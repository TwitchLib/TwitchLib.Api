using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Moderation.CheckAutoModStatus
{
    public class CheckAutoModStatusResponse
    {
        [JsonProperty(PropertyName = "data")]
        public AutoModResult[] Data { get; protected set; }
    }
}
