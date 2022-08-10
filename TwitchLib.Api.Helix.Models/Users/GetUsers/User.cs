using Newtonsoft.Json;
using System;

namespace TwitchLib.Api.Helix.Models.Users.GetUsers
{
    public class User
    {
        /// <summary>
        /// User’s ID.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }

        /// <summary>
        /// User’s login name.
        /// </summary>
        [JsonProperty(PropertyName = "login")]
        public string Login { get; protected set; }

        /// <summary>
        /// User’s display name.
        /// </summary>
        [JsonProperty(PropertyName = "display_name")]
        public string DisplayName { get; protected set; }

        /// <summary>
        /// Date when the user was created.
        /// </summary>
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; protected set; }

        /// <summary>
        /// User’s type: "staff", "admin", "global_mod", or "".
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; protected set; }

        /// <summary>
        /// User’s broadcaster type: "partner", "affiliate", or "".
        /// </summary>
        [JsonProperty(PropertyName = "broadcaster_type")]
        public string BroadcasterType { get; protected set; }

        /// <summary>
        /// User’s channel description.
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; protected set; }

        /// <summary>
        /// URL of the user’s profile image.
        /// </summary>
        [JsonProperty(PropertyName = "profile_image_url")]
        public string ProfileImageUrl { get; protected set; }

        /// <summary>
        /// URL of the user’s offline image.
        /// </summary>
        [JsonProperty(PropertyName = "offline_image_url")]
        public string OfflineImageUrl { get; protected set; }

        /// <summary>
        /// User’s verified email address. Returned if the request includes the user:read:email scope.
        /// </summary>
        [JsonProperty(PropertyName = "email")]
        public string Email { get; protected set; }
    }
}
