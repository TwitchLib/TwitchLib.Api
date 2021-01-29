using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Users.Internal;

namespace TwitchLib.Api.Helix.Models.Users.GetUserActiveExtensions
{
    public class GetUserActiveExtensionsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public ActiveExtensions Data { get; protected set; }
    }
}
