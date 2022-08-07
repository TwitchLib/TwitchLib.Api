using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Chat.Emotes.GetEmoteSets
{
    public class GetEmoteSetsResponse
    {
        [JsonProperty("data")]
        public EmoteSet[] EmoteSets { get; protected set; }
        [JsonProperty("template")]
        public string Template { get; protected set; }
    }
}