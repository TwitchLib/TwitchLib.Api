using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Extensions.LiveChannels
{
    public class GetExtensionLiveChannelsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public LiveChannel[] Data { get; protected set; }
    }
}
