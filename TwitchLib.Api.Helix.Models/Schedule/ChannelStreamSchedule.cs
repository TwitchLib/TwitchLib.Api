using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Schedule
{
    public class ChannelStreamSchedule
    {
        [JsonProperty("segments")]
        public Segment[] Segments { get; protected set; }
        [JsonProperty("broadcaster_id")]
        public string BroadcasterId { get; protected set; }
        [JsonProperty("broadcaster_name")]
        public string BroadcasterName { get; protected set; }
        [JsonProperty("broadcaster_login")]
        public string BroadcasterLogin { get; protected set; }
        [JsonProperty("vacation")]
        public Vacation Vacation { get; protected set; }
    }
}