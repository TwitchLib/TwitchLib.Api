using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.ChannelPoints
{
    public class CustomReward
    {
        [JsonProperty(PropertyName = "broadcaster_id")]
        public string BroadcasterId { get; protected set; }
        [JsonProperty(PropertyName = "broadcaster_login")]
        public string BroadcasterLogin { get; protected set; }
        [JsonProperty(PropertyName = "broadcaster_name")]
        public string BroadcasterName { get; protected set; }
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; protected set; }
        [JsonProperty(PropertyName = "prompt")]
        public string Prompt { get; protected set; }
        [JsonProperty(PropertyName = "cost")]
        public int Cost { get; protected set; }
        [JsonProperty(PropertyName = "image")]
        public Image Image { get; protected set; }
        [JsonProperty(PropertyName = "default_image")]
        public DefaultImage DefaultImage { get; protected set; }
        [JsonProperty(PropertyName = "background_color")]
        public string BackgroundColor { get; protected set; }
        [JsonProperty(PropertyName = "is_enabled")]
        public bool IsEnabled { get; protected set; }
        [JsonProperty(PropertyName = "is_user_input_required")]
        public bool IsUserInputRequired { get; protected set; }
        [JsonProperty(PropertyName = "max_per_stream_setting")]
        public MaxPerStreamSetting MaxPerStreamSetting { get; protected set; }
        [JsonProperty(PropertyName = "max_per_user_per_stream_setting")]
        public MaxPerUserPerStreamSetting MaxPerUserPerStreamSetting { get; protected set; }
        [JsonProperty(PropertyName = "global_cooldown_setting")]
        public GlobalCooldownSetting GlobalCooldownSetting { get; protected set; }
        [JsonProperty(PropertyName = "is_paused")]
        public bool IsPaused { get; protected set; }
        [JsonProperty(PropertyName = "is_in_stock")]
        public bool IsInStock { get; protected set; }
        [JsonProperty(PropertyName = "should_redemptions_skip_request_queue")]
        public bool ShouldRedemptionsSkipQueue { get; protected set; }
        [JsonProperty(PropertyName = "redemptions_redeemed_current_stream")]
        public int? RedemptionsRedeemedCurrentStream { get; protected set; }
        [JsonProperty(PropertyName = "cooldown_expires_at")]
        public string CooldownExpiresAt { get; protected set; }
    }
}
