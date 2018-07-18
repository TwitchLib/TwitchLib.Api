using Newtonsoft.Json;

namespace TwitchLib.Api.Core.Models.Undocumented.ChannelExtensionData
{
    public class View
    {
        [JsonProperty(PropertyName = "viewer_url")]
        public string ViewerUrl { get; protected set; }
    }
}
