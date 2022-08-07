using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Clips.GetClips
{
    public class Clip
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "url")]
        public string Url { get; protected set; }
        [JsonProperty(PropertyName = "embed_url")]
        public string EmbedUrl { get; protected set; }
        [JsonProperty(PropertyName = "broadcaster_id")]
        public string BroadcasterId { get; protected set; }
        [JsonProperty(PropertyName = "broadcaster_name")]
        public string BroadcasterName { get; protected set; }
        [JsonProperty(PropertyName = "creator_id")]
        public string CreatorId { get; protected set; }
        [JsonProperty(PropertyName = "creator_name")]
        public string CreatorName { get; protected set; }
        [JsonProperty(PropertyName = "video_id")]
        public string VideoId { get; protected set; }
        [JsonProperty(PropertyName = "game_id")]
        public string GameId { get; protected set; }
        [JsonProperty(PropertyName = "language")]
        public string Language { get; protected set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; protected set; }
        [JsonProperty(PropertyName = "view_count")]
        public int ViewCount { get; protected set; }
        [JsonProperty(PropertyName = "created_at")]
        public string CreatedAt { get; protected set; }
        [JsonProperty(PropertyName = "thumbnail_url")]
        public string ThumbnailUrl { get; protected set; }
        [JsonProperty(PropertyName = "duration")]
        public float Duration { get; protected set; }
        [JsonProperty(PropertyName = "vod_offset")]
        public int VodOffset { get; protected set; }
    }
}
