using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Moderation.GetBannedUsers
{
    public class BannedUserEvent
    {
        /// <summary>
        /// 	User ID of the banned user.
        /// </summary>
        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; protected set; }

        /// <summary>
        /// Login of the banned user.
        /// </summary>
        [JsonProperty(PropertyName = "user_login")]
        public string UserLogin { get; protected set; }

        /// <summary>
        /// Display name of the banned user.
        /// </summary>
        [JsonProperty(PropertyName = "user_name")]
        public string UserName { get; protected set; }

        /// <summary>
        /// The UTC date and time (in RFC3999 format) when the timeout expires, or an empty string if the user is permanently banned.
        /// </summary>
        [JsonProperty(PropertyName = "expires_at")]
        public DateTime? ExpiresAt { get; protected set; }

        /// <summary>
        /// The UTC date and time (in RFC3999 format) when the ban was created.
        /// </summary>
        [JsonProperty(PropertyName = "created_at")]
        public string CreatedAt { get; protected set; }

        /// <summary>
        /// The reason for the ban if provided by the moderator.
        /// </summary>
        [JsonProperty(PropertyName = "reason")]
        public string Reason { get; protected set; }

        /// <summary>
        /// User ID of the moderator who initiated the ban.
        /// </summary>
        [JsonProperty(PropertyName = "moderator_id")]
        public string ModeratorId { get; protected set; }

        /// <summary>
        /// 	Login of the moderator who initiated the ban.
        /// </summary>
        [JsonProperty(PropertyName = "moderator_login")]
        public string ModeratorLogin { get; protected set; }

        /// <summary>
        /// Display name of the moderator who initiated the ban.
        /// </summary>
        [JsonProperty(PropertyName = "moderator_name")]
        public string ModeratorName { get; protected set; }
    }
}
