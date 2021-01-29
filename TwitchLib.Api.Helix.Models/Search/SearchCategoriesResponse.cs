using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;
using TwitchLib.Api.Helix.Models.Games;

namespace TwitchLib.Api.Helix.Models.Search
{
    public class SearchCategoriesResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Game[] Games { get; protected set; }
        [JsonProperty(PropertyName = "pagination")]
        public Pagination Pagination { get; protected set; }
    }
}
