using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Models.Undocumented.ChannelExtensionData
{
    public class ExtToken
    {
        [JsonProperty(PropertyName = "extension_id")]
        public string ExtensionId { get; protected set; }
        [JsonProperty(PropertyName = "token")]
        public string Token { get; protected set; }
    }
}
