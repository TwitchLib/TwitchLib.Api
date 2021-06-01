using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.ChannelPoints
{
    public class MaxPerStreamSetting
    {
        [JsonProperty(PropertyName = "is_enabled")]
        public bool IsEnabled { get; protected set; }
        [JsonProperty(PropertyName = "value")]
        public int Value { get; protected set; }
    }
}
