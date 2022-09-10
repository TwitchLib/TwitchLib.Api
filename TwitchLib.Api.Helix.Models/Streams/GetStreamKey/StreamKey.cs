using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Streams.GetStreamKey
{
    public class StreamKey
    {
        [JsonProperty(PropertyName = "stream_key")]
        public string Key { get; protected set; }
    }
}
