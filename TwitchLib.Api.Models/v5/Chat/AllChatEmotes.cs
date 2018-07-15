using Newtonsoft.Json;

namespace TwitchLib.Api.Models.V5.Chat
{
    public class AllChatEmotes
    {
        #region Emoticons
        [JsonProperty(PropertyName = "emoticons")]
        public AllChatEmote[] Emoticons { get; protected set; }
        #endregion
    }
}
