using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.ChannelPoints.CreateCustomReward
{
    public class CreateCustomRewardsRequest
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "prompt")]
        public string Prompt { get; set; }
        [JsonProperty(PropertyName = "cost")]
        public int Cost { get; set; }
        [JsonProperty(PropertyName = "is_enabled")]
        public bool IsEnabled { get; set; }
        [JsonProperty(PropertyName = "background_color")]
        public string BackgroundColor { get; set; }
        [JsonProperty(PropertyName = "is_user_input_required")]
        public bool IsUserInputRequired { get; set; }
        [JsonProperty(PropertyName = "is_max_per_stream_enabled")]
        public bool IsMaxPerStreamEnabled { get; set; }
        [JsonProperty(PropertyName = "max_per_stream")]
        public int? MaxPerStream { get; set; }
        [JsonProperty(PropertyName = "is_max_per_user_per_stream_enabled")]
        public bool IsMaxPerUserPerStreamEnabled { get; set; }
        [JsonProperty(PropertyName = "max_per_user_per_stream")]
        public int? MaxPerUserPerStream { get; set; }
        [JsonProperty(PropertyName = "is_global_cooldown_enabled")]
        public bool IsGlobalCooldownEnabled { get; set; }
        [JsonProperty(PropertyName = "global_cooldown_seconds")]
        public int? GlobalCooldownSeconds { get; set; }
        [JsonProperty(PropertyName = "should_redemptions_skip_request_queue")]
        public bool ShouldRedemptionsSkipRequestQueue { get; set; }
    }
}
