using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Schedule.UpdateChannelStreamSegment
{
    public class UpdateChannelStreamSegmentRequest
    {
        [JsonProperty("start_time")]
        public DateTime StartTime { get; set; }
        [JsonProperty("duration")]
        public string Duration { get; set; }
        [JsonProperty("category_id")]
        public string CategoryId { get; set; }
        [JsonProperty("is_canceled")]
        public bool IsCanceled { get; set; }
        [JsonProperty("timezone")]
        public string Timezone { get; set; }
    }
}