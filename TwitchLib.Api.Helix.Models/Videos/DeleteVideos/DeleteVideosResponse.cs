using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Videos.DeleteVideos
{
    public class DeleteVideosResponse
    {
        [JsonProperty(PropertyName = "data")]
        public string[] Data { get; protected set; }
    }
}
