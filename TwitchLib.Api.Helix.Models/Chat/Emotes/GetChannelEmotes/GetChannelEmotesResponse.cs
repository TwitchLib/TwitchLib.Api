using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Chat.Emotes.GetChannelEmotes
{
    public class GetChannelEmotesResponse
    {
        [JsonProperty("data")]
        public ChannelEmote[] ChannelEmotes { get; protected set; }
        [JsonProperty("template")]
        public string Template { get; protected set; }
    }
}