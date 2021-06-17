using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Schedule
{
    public class Vacation
    {
        [JsonProperty("start_time")]
        public string StartTime { get; protected set; }
        [JsonProperty("end_time")]
        public string EndTime { get; protected set; }
    }
}