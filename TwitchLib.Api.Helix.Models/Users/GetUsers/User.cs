using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Users.GetUsers
{
    public class User
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "login")]
        public string Login { get; protected set; }
        [JsonProperty(PropertyName = "display_name")]
        public string DisplayName { get; protected set; }
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; protected set; }
        [JsonProperty(PropertyName = "type")]
        public string Type { get; protected set; }
        [JsonProperty(PropertyName = "broadcaster_type")]
        public string BroadcasterType { get; protected set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; protected set; }
        [JsonProperty(PropertyName = "profile_image_url")]
        public string ProfileImageUrl { get; protected set; }
        [JsonProperty(PropertyName = "offline_image_url")]
        public string OfflineImageUrl { get; protected set; }
        [JsonProperty(PropertyName = "view_count")]
        public long ViewCount { get; protected set; }
        [JsonProperty(PropertyName = "email")]
        public string Email { get; protected set; }
    }
}
