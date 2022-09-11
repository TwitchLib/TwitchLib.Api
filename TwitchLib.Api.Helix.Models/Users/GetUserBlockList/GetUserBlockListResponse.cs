using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Users.GetUserBlockList
{
    public class GetUserBlockListResponse
    {
        [JsonProperty(PropertyName = "data")]
        public BlockedUser[] Data { get; protected set; }
    }
}
