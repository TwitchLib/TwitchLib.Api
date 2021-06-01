using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Videos.GetVideos
{
    public class GetVideosResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Video[] Videos { get; protected set; }
        [JsonProperty(PropertyName = "pagination")]
        public Pagination Pagination { get; protected set; }
    }
}
