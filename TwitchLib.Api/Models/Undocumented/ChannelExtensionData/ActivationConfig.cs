using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Models.Undocumented.ChannelExtensionData
{
    public class ActivationConfig
    {
        [JsonProperty(PropertyName = "slot")]
        public string Slot { get; protected set; }
        [JsonProperty(PropertyName = "anchor")]
        public string Anchor { get; protected set; }
    }
}
