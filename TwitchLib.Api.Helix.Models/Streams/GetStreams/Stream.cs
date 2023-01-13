using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Streams.GetStreams
{
    public class Stream
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; protected set; }
        [JsonProperty(PropertyName = "user_login")]
        public string UserLogin { get; protected set; }
        [JsonProperty(PropertyName = "user_name")]
        public string UserName { get; protected set; }
        [JsonProperty(PropertyName = "game_id")]
        public string GameId { get; protected set; }
        [JsonProperty(PropertyName = "game_name")]
        public string GameName { get; protected set; }
        [JsonProperty(PropertyName = "community_ids")]
        public string[] CommunityIds { get; protected set; }
        [JsonProperty(PropertyName = "type")]
        public string Type { get; protected set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; protected set; }
        [JsonProperty(PropertyName = "tags")]
        public string[] Tags { get; protected set; }
        [JsonProperty(PropertyName = "viewer_count")]
        public int ViewerCount { get; protected set; }
        [JsonProperty(PropertyName = "started_at")]
        public DateTime StartedAt { get; protected set; }
        [JsonProperty(PropertyName = "language")]
        public string Language { get; protected set; }
        [JsonProperty(PropertyName = "thumbnail_url")]
        public string ThumbnailUrl { get; protected set; }
        [JsonProperty(PropertyName = "tag_ids")]
        public string[] TagIds { get; protected set; }
        [JsonProperty(PropertyName = "is_mature")]
        public bool IsMature { get; protected set; }
    }
}
