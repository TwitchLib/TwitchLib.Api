using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Chat.Emotes
{
    public class ChannelEmote : Emote
    {
        [JsonProperty("tier")]
        public string Tier { get; protected set; }
        [JsonProperty("emote_type")]
        public string EmoteType { get; protected set; }
        [JsonProperty("emote_set_id")]
        public string EmoteSetId { get; protected set; }
    }
}