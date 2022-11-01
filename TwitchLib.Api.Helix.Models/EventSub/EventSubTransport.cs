using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.EventSub
{
    public class EventSubTransport
    {
        [JsonProperty(PropertyName = "method")]
        public string Method { get; protected set; }
        [JsonProperty(PropertyName = "callback")]
        public string Callback { get; protected set; }
        [JsonProperty(PropertyName = "created_at")]
        public DateTime? CreatedAt { get; protected set; }
        [JsonProperty(PropertyName = "disconnected_at")]
        public DateTime? DisconnectedAt { get; protected set; }
    }
}