using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.StreamsMetadata
{
    public class GetStreamsMetadataResponse
    {
        [JsonProperty(PropertyName = "data")]
        public StreamMetadata[] StreamsMetadatas { get; protected set; }
        [JsonProperty(PropertyName = "pagination")]
        public Pagination Pagination { get; protected set; }
    }
}
