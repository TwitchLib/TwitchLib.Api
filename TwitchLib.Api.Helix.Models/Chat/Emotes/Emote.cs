using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Chat.Emotes
{
    public abstract class Emote
    {
        /// <summary>
        /// An ID that identifies the emote.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; protected set; }
        /// <summary>
        /// The name of the emote. This is the name that viewers type in the chat window to get the emote to appear.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; protected set; }
        /// <summary>
        /// Contains the image URLs for the emote.
        /// </summary>
        [JsonProperty("images")]
        public EmoteImages Images { get; protected set; }
        /// <summary>
        /// The formats that the emote is available in.
        /// </summary>
        [JsonProperty("format")]
        public string[] Format { get; protected set; }
        /// <summary>
        /// The sizes that the emote is available in.
        /// </summary>
        [JsonProperty("scale")]
        public string[] Scale { get; protected set; }
        /// <summary>
        /// The background themes that the emote is available in.
        /// </summary>
        [JsonProperty("theme_mode")]
        public string[] ThemeMode { get; protected set; }
    }
}