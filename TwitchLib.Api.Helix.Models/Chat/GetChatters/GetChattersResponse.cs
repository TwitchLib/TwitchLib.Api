using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Chat.GetChatters
{
    public class GetChattersResponse
    {
        /// <summary>
        /// List of login names that are connected to the broadcaster’s chat room.
        /// </summary>
        [JsonProperty("data")]
        public Chatter[] Data { get; set; }

        /// <summary>
        /// Contains the information used to page through the list of results. The object is empty if there are no more pages left to page through.
        /// </summary>
        [JsonProperty(PropertyName = "pagination")]
        public Pagination Pagination { get; protected set; }
    }
}