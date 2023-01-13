using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Moderation.ShieldModeStatus.GetShieldModeStatus
{
    public class GetShieldModeStatusResponse
    {
        /// <summary>
        /// A list that contains a single object with the broadcaster’s Shield Mode status.
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public ShieldModeStatus[] Data { get; protected set; }
    }
}
