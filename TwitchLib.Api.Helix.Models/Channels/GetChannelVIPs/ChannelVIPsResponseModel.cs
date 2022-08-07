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
        public bool UserName { get; protected set; }
        /// <summary>
        /// The user’s login name.
        /// </summary>
        [JsonProperty(PropertyName = "user_login")]
        public int? UserLogin { get; protected set; }
    }
}
