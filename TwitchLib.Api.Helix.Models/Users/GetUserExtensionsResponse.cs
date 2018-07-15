using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Users.Internal;

namespace TwitchLib.Api.Helix.Models.Users
{
    public class GetUserExtensionsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public UserExtension[] Users { get; protected set; }
    }
}
