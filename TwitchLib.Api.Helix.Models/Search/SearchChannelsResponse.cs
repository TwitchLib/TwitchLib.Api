using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Search
{
    public class SearchChannelsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Channel[] Channels { get; protected set; }
        [JsonProperty(PropertyName = "pagination")]
        public Pagination Pagination { get; protected set; }
    }
}
