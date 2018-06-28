using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Models.Helix.Bits
{
    public class DateRange
    {
        [JsonProperty(PropertyName = "started_at")]
        public DateTime StartedAt { get; protected set; }
        [JsonProperty(PropertyName = "ended_at")]
        public DateTime EndedAt { get; protected set; }
    }
}
