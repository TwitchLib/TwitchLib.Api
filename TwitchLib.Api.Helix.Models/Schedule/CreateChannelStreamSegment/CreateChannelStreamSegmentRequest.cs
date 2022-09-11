using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Schedule.CreateChannelStreamSegment
{
    public class CreateChannelStreamSegmentRequest
    {
        // required
        [JsonProperty("start_time")]
        public DateTime StartTime { get; set; }
        [JsonProperty("timezone")]
        public string Timezone { get; set; }
        [JsonProperty("is_recurring")]
        public bool IsRecurring { get; set; }
        // optional
        [JsonProperty("duration")]
        public string Duration { get; set; }
        [JsonProperty("category_id")]
        public string CategoryId { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}