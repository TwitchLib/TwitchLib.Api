using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Streams.GetStreamTags
{
    public class GetStreamTagsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Tag[] Data { get; protected set; }
    }
}
