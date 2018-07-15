using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.V5.Models.Chat
{
    public class ChatRoomsByChannelResponse
    {
        [JsonProperty(PropertyName = "_total")]
        public int Total { get; protected set; }
        [JsonProperty(PropertyName = "rooms")]
        public ChatRoom[] Rooms { get; protected set; }
    }
}
