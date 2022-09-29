using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Chat.GetChatters
{
    public class Chatter
    {
        /// <summary>
        /// The login name of a user that’s connected to the broadcaster’s chat room.
        /// </summary>
        [JsonProperty("user_login")]
        public string UserLogin { get; protected set; }
    }
}