using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Videos.GetVideos
{
    public class MutedSegment
    {
        [JsonProperty(PropertyName = "duration")]
        public int Duration { get; protected set; }
        [JsonProperty(PropertyName = "offset")]
        public int Offset { get; protected set; }
    }
}