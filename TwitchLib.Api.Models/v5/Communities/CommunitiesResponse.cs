﻿using Newtonsoft.Json;

namespace TwitchLib.Api.Models.V5.Communities
{
    public class CommunitiesResponse
    {
        [JsonProperty(PropertyName = "communities")]
        public Community[] Communities { get; protected set; }
    }
}
