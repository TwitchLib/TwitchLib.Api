using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Streams.CreateStreamMarker
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class CreateStreamMarkerRequest
    {
        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
    }
}
