using Newtonsoft.Json;

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
