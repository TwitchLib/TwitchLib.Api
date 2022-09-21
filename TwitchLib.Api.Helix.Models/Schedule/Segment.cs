using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Schedule
{
    public class Segment
    {
        [JsonProperty("id")]
        public string Id { get; protected set; }
        [JsonProperty("start_time")]
        public DateTime StartTime { get; protected set; }
        [JsonProperty("end_time")]
        public DateTime EndTime { get; protected set; }
        [JsonProperty("title")]
        public string Title { get; protected set; }
        [JsonProperty("canceled_until")]
        public DateTime? CanceledUntil { get; protected set; }
        [JsonProperty("category")]
        public Category Category { get; protected set; }
        [JsonProperty("is_recurring")]
        public bool IsRecurring { get; protected set; }
    }
}