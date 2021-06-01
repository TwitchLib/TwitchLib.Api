using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Teams
{
    public class GetTeamsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Team[] Teams { get; protected set; }
    }
}