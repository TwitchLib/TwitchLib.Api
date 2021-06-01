﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.ChannelPoints
{
    public class GlobalCooldownSetting
    {
        [JsonProperty(PropertyName = "is_enabled")]
        public bool IsEnabled { get; protected set; }
        [JsonProperty(PropertyName = "global_cooldown_seconds")]
        public int GlobalCooldownSeconds { get; protected set; }
    }
}
