﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Streams.GetStreamMarkers
{
    public class Video
    {
        [JsonProperty(PropertyName = "video_id")]
        public string VideoId { get; protected set; }
        [JsonProperty(PropertyName = "markers")]
        public Marker[] Markers { get; protected set; }
    }
}
