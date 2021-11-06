﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Bits.ExtensionBitsProducts
{
    public class Cost
    {
        [JsonProperty(PropertyName = "amount")]
        public int Amount { get; protected set; }
        [JsonProperty(PropertyName = "type")]
        public string Type { get; protected set; }
    }
}
