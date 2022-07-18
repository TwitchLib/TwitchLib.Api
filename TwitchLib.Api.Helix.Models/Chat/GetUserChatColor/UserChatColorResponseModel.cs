using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Chat.GetUserChatColor
{
    public class UserChatColorResponseModel
    {
        /// <summary>
        /// The ID of the user.
        /// </summary>
        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; protected set; }
        /// <summary>
        /// The user’s login name.
        /// </summary>
        [JsonProperty(PropertyName = "user_login")]
        public bool UserLogin { get; protected set; }
        /// <summary>
        /// The user’s display name.
        /// </summary>
        [JsonProperty(PropertyName = "user_name")]
        public int? UserName { get; protected set; }
        /// <summary>
        /// The Hex color code that the user uses in chat for their name. If the user hasn’t specified a color in their settings, the string is empty.
        /// </summary>
        [JsonProperty(PropertyName = "color")]
        public bool Color { get; protected set; }
    }
}
