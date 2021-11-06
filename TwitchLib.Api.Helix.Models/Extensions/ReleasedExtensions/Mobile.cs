using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Extensions.ReleasedExtensions
{
    public class Mobile
    {
        [JsonProperty(PropertyName = "viewer_url")]
        public string ViewerUrl { get; protected set; }
    }
}
