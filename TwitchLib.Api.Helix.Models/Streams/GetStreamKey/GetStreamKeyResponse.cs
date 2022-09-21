using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Streams.GetStreamKey
{
    public class GetStreamKeyResponse
    {
        [JsonProperty(PropertyName = "data")]
        public StreamKey[] Streams { get; protected set; }
    }
}
