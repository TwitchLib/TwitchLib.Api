using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Games
{
    public class GetGamesResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Game[] Games { get; protected set; }
    }
}
