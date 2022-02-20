using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Users.GetUserFollows
{
    public class Follow
    {
        [JsonProperty(PropertyName = "from_id")]
        public string FromUserId { get; protected set; }
        [JsonProperty(PropertyName = "from_login")]
        public string FromLogin { get; protected set; }
        [JsonProperty(PropertyName = "from_name")]
        public string FromUserName { get; protected set; }
        [JsonProperty(PropertyName = "to_id")]
        public string ToUserId { get; protected set; }
        [JsonProperty(PropertyName = "to_login")]
        public string ToLogin { get; protected set; }
        [JsonProperty(PropertyName = "to_name")]
        public string ToUserName { get; protected set; }
        [JsonProperty(PropertyName = "followed_at")]
        public DateTime FollowedAt { get; protected set; }
    }
}
