using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Moderation.ShieldModeStatus
{
    public class ShieldModeStatus
    {
        /// <summary>
        /// A Boolean value that determines whether Shield Mode is active.
        /// Is true if the broadcaster activated Shield Mode; otherwise, false.
        /// </summary>
        [JsonProperty(PropertyName = "is_active")]
        public bool IsActive { get; protected set; }
        /// <summary>
        /// An ID that identifies the moderator that last activated Shield Mode.
        /// Is an empty string if Shield Mode hasn’t been previously activated.
        /// </summary>
        [JsonProperty(PropertyName = "moderator_id")]
        public string ModeratorId { get; protected set; }
        /// <summary>
        /// The moderator’s login name. Is an empty string if Shield Mode hasn’t been previously activated.
        /// </summary>
        [JsonProperty(PropertyName = "moderator_login")]
        public string ModeratorLogin { get; protected set; }
        /// <summary>
        /// The moderator’s display name. Is an empty string if Shield Mode hasn’t been previously activated.
        /// </summary>
        [JsonProperty(PropertyName = "moderator_name")]
        public string ModeratorName { get; protected set; }
        /// <summary>
        /// The UTC timestamp (in RFC3339 format) of when Shield Mode was last activated.
        /// Is an empty string if Shield Mode hasn’t been previously activated.
        /// </summary>
        [JsonProperty(PropertyName = "last_activated_at")]
        public string LastActivatedAt { get; protected set; }
    }
}
