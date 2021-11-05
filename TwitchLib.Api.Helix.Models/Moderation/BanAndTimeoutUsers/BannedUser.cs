using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Moderation.BanUsers
{
    public class BannedUser
    {
        [JsonProperty(PropertyName = "broadcaster_id")]
        public string BroadcasterId { get; protected set; }
        [JsonProperty(PropertyName = "moderator_id")]
        public string ModeratorId { get; protected set; }
        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; protected set; }
        [JsonProperty(PropertyName = "end_time")]
        public DateTime? EndTime { get; protected set; }
    }
}
