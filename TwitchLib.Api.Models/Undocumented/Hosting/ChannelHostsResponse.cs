using Newtonsoft.Json;

namespace TwitchLib.Api.Core.Models.Undocumented.Hosting
{
    public class ChannelHostsResponse
    {
        [JsonProperty(PropertyName = "hosts")]
        public HostListing[] Hosts { get; protected set; }
    }
}
