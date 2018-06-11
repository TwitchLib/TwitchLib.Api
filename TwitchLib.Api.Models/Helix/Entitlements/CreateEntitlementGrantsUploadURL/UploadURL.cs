using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Models.Helix.Entitlements.CreateEntitlementGrantsUploadURL
{
    public class UploadURL
    {
        [JsonProperty(PropertyName = "url")]
        public string Url { get; protected set; }
    }
}
