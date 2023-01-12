using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Search
{
    public class Channel
    {
        [JsonProperty(PropertyName = "game_id")]
        public string GameId { get; protected set; }
        [JsonProperty(PropertyName = "game_name")]
        public string GameName { get; protected set; }
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "broadcaster_login")]
        public string BroadcasterLogin { get; protected set; }
        [JsonProperty(PropertyName = "display_name")]
        public string DisplayName { get; protected set; }
        [JsonProperty(PropertyName = "broadcaster_language")]
        public string BroadcasterLanguage { get; protected set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; protected set; }
        [JsonProperty(PropertyName = "thumbnail_url")]
        public string ThumbnailUrl { get; protected set; }
        [JsonProperty(PropertyName = "is_live")]
        public bool IsLive { get; protected set; }
        [JsonProperty(PropertyName = "started_at")]
        public DateTime? StartedAt { get; protected set; }
        [JsonProperty(PropertyName = "tag_ids")]
        public List<string> TagIds { get; protected set; }
        [JsonProperty(PropertyName = "tags")]
        public List<string> Tags { get; protected set; }
    }
}
