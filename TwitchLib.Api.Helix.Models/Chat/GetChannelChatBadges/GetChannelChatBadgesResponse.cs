using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Chat.GetChannelChatBadges
{
    public class GetChannelChatBadgesResponse
    {
        [JsonProperty(PropertyName = "data")]
        public EmoteSet[] EmoteSet { get; protected set; }
    }
}
