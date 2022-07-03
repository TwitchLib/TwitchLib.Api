using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Videos.GetVideos
{
    public class Video
    {
        [JsonProperty(PropertyName = "created_at")]
        public string CreatedAt { get; protected set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; protected set; }
        [JsonProperty(PropertyName = "duration")]
        public string Duration { get; protected set; }
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "language")]
        public string Language { get; protected set; }
        [JsonProperty(PropertyName = "published_at")]
        public string PublishedAt { get; protected set; }
        [JsonProperty(PropertyName = "thumbnail_url")]
        public string ThumbnailUrl { get; protected set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; protected set; }
        [JsonProperty(PropertyName = "type")]
        public string Type { get; protected set; }
        [JsonProperty(PropertyName = "url")]
        public string Url { get; protected set; }
        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; protected set; }
        [JsonProperty(PropertyName = "user_login")]
        public string UserLogin { get; protected set; }
        [JsonProperty(PropertyName = "user_name")]
        public string UserName { get; protected set; }
        [JsonProperty(PropertyName = "viewable")]
        public string Viewable { get; protected set; }
        [JsonProperty(PropertyName = "view_count")]
        public int ViewCount { get; protected set; }
        [JsonProperty(PropertyName = "stream_id")]
        public string StreamId { get; protected set; }
        [JsonProperty(PropertyName = "muted_segments")]
        public MutedSegment[] MutedSegments { get; protected set; }
    }
}
