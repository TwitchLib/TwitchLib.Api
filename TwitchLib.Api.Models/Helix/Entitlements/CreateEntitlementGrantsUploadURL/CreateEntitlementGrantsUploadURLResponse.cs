using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Models.Helix.Entitlements.CreateEntitlementGrantsUploadURL
{
    public class CreateEntitlementGrantsUploadURLResponse
    {
        [JsonProperty(PropertyName = "data")]
        public UploadURL[] Data { get; protected set; }
    }
}
