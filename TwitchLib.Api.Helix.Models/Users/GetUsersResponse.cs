using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Users.Internal;

namespace TwitchLib.Api.Helix.Models.Users
{
    public class GetUsersResponse
    {
        [JsonProperty(PropertyName = "data")]
        public User[] Users { get; protected set; }
    }
}
