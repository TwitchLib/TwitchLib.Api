using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Moderation.GetModeratorEvents
{
    public class ModeratorEvent
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "event_type")]
        public string EventType { get; protected set; }
        [JsonProperty(PropertyName = "event_timestamp")]
        public DateTime EventTimestamp { get; protected set; }
        [JsonProperty(PropertyName = "version")]
        public string Version { get; protected set; }
        [JsonProperty(PropertyName = "event_data")]
        public EventData EventData { get; protected set; }
    }
}
