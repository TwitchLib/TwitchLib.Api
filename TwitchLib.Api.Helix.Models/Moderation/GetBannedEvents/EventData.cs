using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Moderation.GetBannedEvents
{
    public class EventData
    {
        [JsonProperty(PropertyName = "broadcaster_id")]
        public string BroadcasterId { get; protected set; }
        [JsonProperty(PropertyName = "broadcaster_login")]
        public string BroadcasterLogin { get; protected set; }
        [JsonProperty(PropertyName = "broadcaster_name")]
        public string BroadcasterName { get; protected set; }
        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; protected set; }
        [JsonProperty(PropertyName = "user_login")]
        public string UserLogin { get; protected set; }
        [JsonProperty(PropertyName = "user_name")]
        public string UserName { get; protected set; }
        [JsonProperty(PropertyName = "expires_at")]
        public DateTime? ExpiresAt { get; protected set; }
        [JsonProperty(PropertyName = "reason")]
        public string Reason { get; protected set; }
        [JsonProperty(PropertyName = "moderator_id")]
        public string ModeratorId { get; protected set; }
        [JsonProperty(PropertyName = "moderator_login")]
        public string ModeratorLogin { get; protected set; }
        [JsonProperty(PropertyName = "moderator_name")]
        public string ModeratorName { get; protected set; }
    }
}
