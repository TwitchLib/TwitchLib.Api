﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Extensions.ReleasedExtensions
{
    public class VideoOverlay
    {
        [JsonProperty(PropertyName = "viewer_url")]
        public string ViewerUrl { get; protected set; }
        [JsonProperty(PropertyName = "can_link_external_content")]
        public bool CanLinkExternalContent { get; protected set; }
    }
}
