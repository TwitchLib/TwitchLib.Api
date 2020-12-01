using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Clips.GetClips
{
    public class GetClipsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Clip[] Clips { get; protected set; }
        [JsonProperty(PropertyName = "pagination")]
        public Pagination Pagination { get; protected set; }
    }
}
