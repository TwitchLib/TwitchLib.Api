using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Channels.ModifyChannelInformation
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ModifyChannelInformationRequest
    {
        [JsonProperty(PropertyName = "game_id")]
        public string GameId { get; protected set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; protected set; }
        [JsonProperty(PropertyName = "broadcaster_language")]
        public string BroadcasterLanguage { get; protected set; }
    }
}
