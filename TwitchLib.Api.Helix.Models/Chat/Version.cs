using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Chat
{
    public class Version
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "image_url_1x")]
        public string ImageUrl1x { get; protected set; }
        [JsonProperty(PropertyName = "image_url_2x")]
        public string ImageUrl2x { get; protected set; }
        [JsonProperty(PropertyName = "image_url_4x")]
        public string ImageUrl4x { get; protected set; }
    }
}
