using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Chat.Emotes
{
    public class GlobalEmote : Emote
    {
        /// <summary>
        /// Contains the image URLs for the emote.
        /// </summary>
        [JsonProperty("images")]
        public EmoteImages Images { get; protected set; }
    }
}