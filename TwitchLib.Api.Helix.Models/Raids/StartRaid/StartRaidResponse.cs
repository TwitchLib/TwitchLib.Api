using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Raids.StartRaid
{
    public class StartRaidResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Raid[] Data { get; protected set; }
    }
}
