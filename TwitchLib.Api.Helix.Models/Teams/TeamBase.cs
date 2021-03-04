using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Teams
{
    public abstract class TeamBase
    {
        [JsonProperty(PropertyName = "banner")]
        public string Banner { get; protected set; }
        [JsonProperty(PropertyName = "background_image_url")]
        public string BackgroundImageUrl { get; protected set; }
        [JsonProperty(PropertyName = "created_at")]
        public string CreatedAt { get; protected set; }
        [JsonProperty(PropertyName = "updated_at")]
        public string UpdatedAt { get; protected set; }
        public string Info { get; protected set; }
        [JsonProperty(PropertyName = "thumbnail_url")]
        public string ThumbnailUrl { get; protected set; }
        [JsonProperty(PropertyName = "team_name")]
        public string TeamName { get; protected set; }
        [JsonProperty(PropertyName = "team_display_name")]
        public string TeamDisplayName { get; protected set; }
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }
    }
}