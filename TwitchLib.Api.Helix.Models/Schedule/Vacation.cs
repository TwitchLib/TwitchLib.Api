using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Schedule
{
    public class Vacation
    {
        [JsonProperty("start_time")]
        public DateTime StartTime { get; protected set; }
        [JsonProperty("end_time")]
        public DateTime EndTime { get; protected set; }
    }
}