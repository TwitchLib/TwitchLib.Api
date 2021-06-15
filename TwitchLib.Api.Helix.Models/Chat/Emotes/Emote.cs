using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Chat.Emotes
{
    public abstract class Emote
    {
        [JsonProperty("id")]
        public string Id { get; protected set; }
        [JsonProperty("name")]
        public string Name { get; protected set; }
        [JsonProperty("images")]
        public EmoteImages Images { get; protected set; }
    }
}