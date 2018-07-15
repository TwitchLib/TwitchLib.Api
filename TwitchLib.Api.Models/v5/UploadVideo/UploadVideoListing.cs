﻿using Newtonsoft.Json;

namespace TwitchLib.Api.Models.V5.UploadVideo
{
    public class UploadVideoListing
    {
        [JsonProperty(PropertyName = "upload")]
        public Upload Upload { get; protected set; }
        [JsonProperty(PropertyName = "video")]
        public UploadedVideo Video { get; protected set; }
    }
}
