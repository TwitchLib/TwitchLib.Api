using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Streams.CreateStreamMarker
{
    public class CreateStreamMarkerResponse
    {
        [JsonProperty(PropertyName = "data")]
        public CreatedMarker[] Data { get; protected set; }
    }
}
