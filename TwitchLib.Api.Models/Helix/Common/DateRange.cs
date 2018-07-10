using Newtonsoft.Json;
using System;

namespace TwitchLib.Api.Models.Helix.Common
{
    public class DateRange
    {
        [JsonProperty(PropertyName = "started_at")]
        public DateTime StartedAt { get; protected set; }
        [JsonProperty(PropertyName = "ended_at")]
        public DateTime EndedAt { get; protected set; }
    }
}
