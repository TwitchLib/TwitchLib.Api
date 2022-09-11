using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Channels.GetChannelInformation
{
    public class GetChannelInformationResponse
    {
        [JsonProperty(PropertyName = "data")]
        public ChannelInformation[] Data { get; protected set; }
    }
}
