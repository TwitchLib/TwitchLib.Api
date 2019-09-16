using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Moderation.GetBannedEvents
{
    public class EventData
    {
        [JsonProperty(PropertyName = "broadcaster_id")]
        public string BroadcasterId { get; protected set; }
        [JsonProperty(PropertyName = "broadcaster_name")]
        public string BroadcasterName { get; protected set; }
        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; protected set; }
        [JsonProperty(PropertyName = "user_name")]
        public string UserName { get; protected set; }
        [JsonProperty(PropertyName = "expires_at")]
        public DateTime? ExpiresAt { get; protected set; }
    }
}
