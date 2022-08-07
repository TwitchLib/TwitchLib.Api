using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Chat.GetUserChatColor
{
    public class GetUserChatColorResponse
    {
        /// <summary>
        /// The list of users and the color code that’s used for their name.
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public UserChatColorResponseModel[] Data { get; protected set; }
    }
}
