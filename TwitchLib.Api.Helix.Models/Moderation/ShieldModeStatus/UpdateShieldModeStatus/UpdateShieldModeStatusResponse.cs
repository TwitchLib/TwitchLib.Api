using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Moderation.ShieldModeStatus.UpdateShieldModeStatus
{
    public class UpdateChannelStreamSegmentResponse
    {
        /// <summary>
        /// A list that contains a single object with the broadcaster’s updated Shield Mode status.
        /// </summary>
        [JsonProperty("data")]
        public ShieldModeStatus Data { get; protected set; }
    }
}