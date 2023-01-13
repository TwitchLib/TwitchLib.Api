using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Channels.ModifyChannelInformation
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ModifyChannelInformationRequest
    {
        [JsonProperty(PropertyName = "game_id", NullValueHandling = NullValueHandling.Ignore)]
        public string GameId { get; set; }
        [JsonProperty(PropertyName = "title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "broadcaster_language", NullValueHandling = NullValueHandling.Ignore)]
        public string BroadcasterLanguage { get; set; }
        [JsonProperty(PropertyName = "delay", NullValueHandling = NullValueHandling.Ignore)]
        public int? Delay { get; set; }
        [JsonProperty(PropertyName = "tags", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Tags { get; set; }
    }
}
