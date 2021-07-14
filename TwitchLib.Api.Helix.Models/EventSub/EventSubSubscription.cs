using Newtonsoft.Json;
using System.Collections.Generic;

namespace TwitchLib.Api.Helix.Models.EventSub
{
    public class EventSubSubscription
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "status")]
        public string Status { get; protected set; }
        [JsonProperty(PropertyName = "type")]
        public string Type { get; protected set; }
        [JsonProperty(PropertyName = "version")]
        public string Version { get; protected set; }
        [JsonProperty(PropertyName = "condition")]
        public Dictionary<string, string> Condition { get; protected set; }
        [JsonProperty(PropertyName = "created_at")]
        public string CreatedAt { get; protected set; }
        [JsonProperty(PropertyName = "transport")]
        public EventSubTransport Transport { get; protected set; }
        [JsonProperty(PropertyName = "cost")]
        public int Cost { get; protected set; }
    }
}