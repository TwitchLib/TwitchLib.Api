using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Entitlements
{
    public class UploadURL
    {
        [JsonProperty(PropertyName = "url")]
        public string Url { get; protected set; }
    }
}
