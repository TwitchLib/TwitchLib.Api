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
        public Chatter[] Data { get; protected set; }

        /// <summary>
        /// Contains the information used to page through the list of results. The object is empty if there are no more pages left to page through.
        /// </summary>
        [JsonProperty(PropertyName = "pagination")]
        public Pagination Pagination { get; protected set; }

        /// <summary>
        /// The total number of users that are connected to the broadcaster’s chat room.
        /// <para>As you page through the list, the number of users may change as users join and leave the chat room.</para>
        /// </summary>
        [JsonProperty("total")]
        public int Total { get; protected set; }
    }
}