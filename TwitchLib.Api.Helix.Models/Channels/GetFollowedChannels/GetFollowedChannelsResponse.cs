using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Channels.GetFollowedChannels
{
    public class GetFollowedChannelsResponse
    {
        /// <summary>
        /// The list of broadcasters that the user follows.
        /// <para>The list is in descending order by followed_at (with the most recently followed broadcaster first).</para>
        /// <para>The list is empty if the user doesn’t follow anyone.</para>
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public FollowedChannel[] Data { get; protected set; }
        
        /// <summary>
        /// Contains the information used to page through the list of results. The object is empty if there are no more pages left to page through.
        /// </summary>
        [JsonProperty(PropertyName = "pagination")]
        public Pagination Pagination { get; protected set; }
        
        /// <summary>
        /// The total number of broadcasters that the user follows. As someone pages through the list, the number may change as the user follows or unfollows broadcasters.
        /// </summary>
        [JsonProperty(PropertyName = "total")]
        public int Total { get; protected set; }
    }
}