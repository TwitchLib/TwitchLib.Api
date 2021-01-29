using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;
using TwitchLib.Api.Helix.Models.Users.Internal;

namespace TwitchLib.Api.Helix.Models.Users.GetUserFollows
{
    public class GetUsersFollowsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Follow[] Follows { get; protected set; }
        [JsonProperty(PropertyName = "pagination")]
        public Pagination Pagination { get; protected set; }
        [JsonProperty(PropertyName = "total")]
        public long TotalFollows { get; protected set; }
    }
}
