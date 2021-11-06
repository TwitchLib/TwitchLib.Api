﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Extensions.LiveChannels
{
    public class GetExtensionLiveChannelsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public LiveChannel[] Data { get; protected set; }
    }
}
