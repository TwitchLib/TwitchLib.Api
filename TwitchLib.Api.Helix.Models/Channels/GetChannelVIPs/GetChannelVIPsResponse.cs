using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Channels.GetChannelVIPs
{
    public class GetChannelVIPsResponse
    {
        /// <summary>
        /// The list of VIPs. The list is empty if the channel doesn’t have VIP users. The list does not include the broadcaster.
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public ChannelVIPsResponseModel[] Data { get; protected set; }
        /// <summary>
        /// Contains the information used to page through the list of results.
        /// </summary>
        [JsonProperty(PropertyName = "pagination")]
        public Pagination Pagination { get; protected set; }
    }
}
