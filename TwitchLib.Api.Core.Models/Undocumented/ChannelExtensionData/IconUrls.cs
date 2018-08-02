using Newtonsoft.Json;

namespace TwitchLib.Api.Core.Models.Undocumented.ChannelExtensionData
{
    public class IconUrls
    {
        [JsonProperty(PropertyName = "100x100")]
        public string Url100x100 { get; protected set; }
    }
}
