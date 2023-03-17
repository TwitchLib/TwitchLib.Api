using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Channels.GetFollowedChannels
{
    public class FollowedChannel
    {
        /// <summary>
        /// An ID that uniquely identifies the broadcaster that this user is following.
        /// </summary>
        [JsonProperty(PropertyName = "broadcaster_id")]
        public string BroadcasterId { get; protected set; }
        
        /// <summary>
        /// The broadcaster’s login name.
        /// </summary>
        [JsonProperty(PropertyName = "broadcaster_login")]
        public string BroadcasterLogin { get; protected set; }
        
        /// <summary>
        /// The broadcaster’s display name.
        /// </summary>
        [JsonProperty(PropertyName = "broadcaster_name")]
        public string BroadcasterName { get; protected set; }
        
        /// <summary>
        /// The UTC timestamp when the user started following the broadcaster.
        /// </summary>
        [JsonProperty(PropertyName = "followed_at")]
        public string FollowedAt { get; protected set;  }
    }
}