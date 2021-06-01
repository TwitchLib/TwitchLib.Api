﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Channels.GetChannelEditors
{
    public class GetChannelEditorsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public ChannelEditor[] Data { get; protected set; }
    }
}
