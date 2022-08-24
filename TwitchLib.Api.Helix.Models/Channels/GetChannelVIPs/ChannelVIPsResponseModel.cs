using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Channels.GetChannelVIPs
{
    public class ChannelVIPsResponseModel
    {
        /// <summary>
        /// An ID that uniquely identifies the VIP user.
        /// </summary>
        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; protected set; }
        /// <summary>
        /// The user’s display name.
        /// </summary>
        [JsonProperty(PropertyName = "user_name")]
        public string UserName { get; protected set; }
        /// <summary>
        /// The user’s login name.
        /// </summary>
        [JsonProperty(PropertyName = "user_login")]
        public string UserLogin { get; protected set; }
    }
}
