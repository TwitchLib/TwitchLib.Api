using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Moderation.GetBannedUsers
{
    public class BannedUserEvent
    {
        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; protected set; }
        [JsonProperty(PropertyName = "user_name")]
        public string UserName { get; protected set; }
        [JsonProperty(PropertyName = "expires_at")]
        public DateTime? ExpiresAt { get; protected set; }
    }
}
