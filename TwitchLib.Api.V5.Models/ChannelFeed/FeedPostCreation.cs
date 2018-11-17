using Newtonsoft.Json;

namespace TwitchLib.Api.V5.Models.ChannelFeed
{
    public class FeedPostCreation
    {
        #region Post
        [JsonProperty(PropertyName = "post")]
        public FeedPost Post { get; internal set; }
        #endregion
        #region Tweet
        [JsonProperty(PropertyName = "tweet")]
        public string Tweet { get; internal set; }
        #endregion
    }
}
