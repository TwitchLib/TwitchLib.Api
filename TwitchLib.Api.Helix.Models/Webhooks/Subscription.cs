using Newtonsoft.Json;
using System;

namespace TwitchLib.Api.Helix.Models.Webhooks
{
    public class Subscription
    {
        [JsonProperty(PropertyName = "topic")]
        public string Topic { get; protected set; }
        [JsonProperty(PropertyName = "callback")]
        public string Callback { get; protected set; }
        [JsonProperty(PropertyName = "expires_at")]
        public DateTime ExpiresAt { get; protected set; }
    }
}
