using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.ChannelPoints
{
    public class Reward
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; protected set; }
        [JsonProperty(PropertyName = "prompt")]
        public string Prompt { get; protected set; }
        [JsonProperty(PropertyName = "cost")]
        public int Cost { get; protected set; }
    }
}
