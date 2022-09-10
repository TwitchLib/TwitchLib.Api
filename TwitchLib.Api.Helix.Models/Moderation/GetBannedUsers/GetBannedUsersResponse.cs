using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Moderation.GetBannedUsers
{
    public class GetBannedUsersResponse
    {
        /// <summary>
        /// Banned and timed-out users for a channel.
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public BannedUserEvent[] Data { get; protected set; }

        /// <summary>
        /// A cursor value, to be used in a subsequent request to specify the starting point of the next set of results.
        /// </summary>
        [JsonProperty(PropertyName = "pagination")]
        public Pagination Pagination { get; protected set; }
    }
}
