using Newtonsoft.Json;

namespace TwitchLib.Api.Models.V5.Videos
{
    public class FollowedVideos
    {
        #region Videos
        [JsonProperty(PropertyName = "videos")]
        public Video[] Videos { get; protected set; }
        #endregion
    }
}
