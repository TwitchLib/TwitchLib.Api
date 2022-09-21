using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Raids.StartRaid
{
    public class StartRaidResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Raid[] Data { get; protected set; }
    }
}
