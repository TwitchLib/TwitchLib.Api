using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Schedule
{
    public class Segment
    {
        [JsonProperty("id")]
        public string Id { get; protected set; }
        [JsonProperty("start_time")]
        public string StartTime { get; protected set; }
        [JsonProperty("end_time")]
        public string EndTime { get; protected set; }
        [JsonProperty("title")]
        public string Title { get; protected set; }
        [JsonProperty("canceled_until")]
        public string CanceledUntil { get; protected set; }
        [JsonProperty("category")]
        public Category Category { get; protected set; }
        [JsonProperty("is_recurring")]
        public bool IsRecurring { get; protected set; }
    }
}