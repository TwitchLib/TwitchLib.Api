﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.ChannelPoints
{
    public class MaxPerUserPerStreamSetting
    {
        [JsonProperty(PropertyName = "is_enabled")]
        public bool IsEnabled { get; protected set; }
        [JsonProperty(PropertyName = "max_per_user_per_stream")]
        public int MaxPerUserPerStream { get; protected set; }
    }
}
