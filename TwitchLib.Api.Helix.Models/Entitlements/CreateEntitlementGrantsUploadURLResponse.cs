using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Entitlements
{
    public class CreateEntitlementGrantsUploadURLResponse
    {
        [JsonProperty(PropertyName = "data")]
        public UploadURL[] Data { get; protected set; }
    }
}
