using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Core.Models.Undocumented.ChannelExtensionData
{
    public class InstalledExtension
    {
        [JsonProperty(PropertyName = "extension")]
        public Extension Extension { get; protected set; }
        [JsonProperty(PropertyName = "installation_status")]
        public InstallationStatus InstallationStatus { get; protected set; }
    }
}
