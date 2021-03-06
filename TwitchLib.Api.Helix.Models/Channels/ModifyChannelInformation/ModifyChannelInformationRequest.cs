﻿using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Channels.ModifyChannelInformation
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ModifyChannelInformationRequest
    {
        [JsonProperty(PropertyName = "game_id")]
        public string GameId { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "broadcaster_language")]
        public string BroadcasterLanguage { get; set; }
        [JsonProperty(PropertyName = "delay")]
        public int Delay { get; set; }
    }
}
