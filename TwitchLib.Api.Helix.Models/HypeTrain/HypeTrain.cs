using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.HypeTrain
{
    public class HypeTrain
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "event_type")]
        public string EventType { get; protected set; }
        [JsonProperty(PropertyName = "event_timestamp")]
        public string EventTimeStamp { get; protected set; }
        [JsonProperty(PropertyName = "version")]
        public string Version { get; protected set; }
        [JsonProperty(PropertyName = "event_data")]
        public HypeTrainEventData EventData { get; protected set; }
    }
}