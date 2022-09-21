using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Channels.GetChannelEditors
{
    public class GetChannelEditorsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public ChannelEditor[] Data { get; protected set; }
    }
}
