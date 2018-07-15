using Newtonsoft.Json;

namespace TwitchLib.Api.V5.Models.Communities
{
    public class Moderators
    {
        #region Moderators
        [JsonProperty(PropertyName = "moderators")]
        public Moderator[] Users { get; protected set; }
        #endregion
    }
}
