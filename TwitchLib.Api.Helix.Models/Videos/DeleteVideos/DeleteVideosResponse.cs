using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Videos.DeleteVideos
{
    public class DeleteVideosResponse
    {
        [JsonProperty(PropertyName = "data")]
        public string[] Data { get; protected set; }
    }
}
