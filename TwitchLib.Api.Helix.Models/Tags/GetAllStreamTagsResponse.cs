using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Tags
{
    public class GetAllStreamTagsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Tag[] Data { get; protected set; }
        [JsonProperty(PropertyName = "pagination")]
        public Pagination Pagination { get; protected set; }
    }
}
