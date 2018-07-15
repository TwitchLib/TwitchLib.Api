using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Core.Models.Undocumented.ChannelExtensionData
{
    public class GetChannelExtensionDataResponse
    {
        [JsonProperty(PropertyName = "issued_at")]
        public string IssuedAt { get; protected set; }
        [JsonProperty(PropertyName = "tokens")]
        public ExtToken[] Tokens { get; protected set; }
        [JsonProperty(PropertyName = "installed_extensions")]
        public InstalledExtension[] InstalledExtensions { get; protected set; }
    }
}
