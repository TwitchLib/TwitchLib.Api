using Newtonsoft.Json;

namespace TwitchLib.Api.V5.Models.ChannelFeed
{
    public class FeedPostReaction
    {
        #region Count
        [JsonProperty(PropertyName = "count")]
        public int Count { get; protected set; }
        #endregion
        #region Emote
        [JsonProperty(PropertyName = "emote")]
        public string Emote { get; protected set; }
        #endregion
        #region UserIds
        [JsonProperty(PropertyName = "user_ids")]
        public int[] UserIds { get; protected set; }
        #endregion
    }
}
