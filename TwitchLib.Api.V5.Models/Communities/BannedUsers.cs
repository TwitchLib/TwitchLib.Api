using Newtonsoft.Json;

namespace TwitchLib.Api.V5.Models.Communities
{
    public class BannedUsers
    {
        #region Cursor
        [JsonProperty(PropertyName = "_cursor")]
        public string Cursor { get; protected set; }
        #endregion
        #region Users
        [JsonProperty(PropertyName = "banned_users")]
        public BannedUser[] Users { get; protected set; }
        #endregion
    }
}
