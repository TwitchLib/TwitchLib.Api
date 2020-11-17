using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.ChannelPoints.CreateCustomReward
{
    public class CreateCustomRewardsRequest
    {
        [JsonProperty(PropertyName = "broadcaster_id")]
        public string BroadcasterId { get; protected set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; protected set; }
        [JsonProperty(PropertyName = "prompt")]
        public string Prompt { get; protected set; }
        [JsonProperty(PropertyName = "cost")]
        public int Cost { get; protected set; }
        [JsonProperty(PropertyName = "is_enabled")]
        public bool IsEnabled { get; protected set; }
        [JsonProperty(PropertyName = "background_color")]
        public string BackgroundColor { get; protected set; }
        [JsonProperty(PropertyName = "is_user_input_required")]
        public bool IsUserInputRequired { get; protected set; }
        [JsonProperty(PropertyName = "is_max_per_stream-Enabled")]
        public bool IsMaxPerStreamEnabled { get; protected set; }
        [JsonProperty(PropertyName = "max_per_stream")]
        public int? MaxPerStream { get; protected set; }
        [JsonProperty(PropertyName = "is_max_per_user_per_stream_enabled")]
        public bool IsMaxPerUserPerStreamEnabled { get; protected set; }
        [JsonProperty(PropertyName = "max_per_user_per_stream")]
        public int? MaxPerUserPerStream { get; protected set; }
        [JsonProperty(PropertyName = "is_global_cooldown_enabled")]
        public bool IsGlobalCooldownEnabled { get; protected set; }
        [JsonProperty(PropertyName = "global_cooldown_seconds")]
        public int? GlobalCooldownSeconds { get; protected set; }
        [JsonProperty(PropertyName = "should_redemptions_skip_request_queue")]
        public bool ShouldRedemptionsSkipRequestQueue { get; protected set; }
    }
}
