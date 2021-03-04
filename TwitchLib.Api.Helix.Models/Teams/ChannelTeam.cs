using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Teams
{
    public class ChannelTeam : TeamBase
    {
        [JsonProperty(PropertyName = "broadcaster_id")]
        public string BroadcasterId { get; protected set; }
        [JsonProperty(PropertyName = "broadcaster_name")]
        public string BroadcasterName { get; protected set; }
        [JsonProperty(PropertyName = "broadcaster_login")]
        public string BroadcasterLogin { get; protected set; }
    }
}