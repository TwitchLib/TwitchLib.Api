using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Chat.ChatSettings
{
    public class ChatSettingsResponseModel
    {
        [JsonProperty(PropertyName = "broadcaster_id")]
        public string BroadcasterId { get; protected set; }
        [JsonProperty(PropertyName = "slow_mode")]
        public bool SlowMode { get; protected set; }
        [JsonProperty(PropertyName = "slow_mode_wait_time")]
        public int? SlowModeWaitDuration { get; protected set; }
        [JsonProperty(PropertyName = "follower_mode")]
        public bool FollowerMode { get; protected set; }
        [JsonProperty(PropertyName = "follower_mode_duration")]
        public int? FollowerModeDuration { get; protected set; }
        [JsonProperty(PropertyName = "subscriber_mode")]
        public bool SubscriberMode { get; protected set; }
        [JsonProperty(PropertyName = "emote_mode")]
        public bool EmoteMode { get; protected set; }
        [JsonProperty(PropertyName = "unique_chat_mode")]
        public bool UniqueChatMode { get; protected set; }
        [JsonProperty(PropertyName = "non_moderator_chat_delay")]
        public bool NonModeratorChatDelay { get; protected set; }
        [JsonProperty(PropertyName = "non_moderator_chat_delay_duration")]
        public int? NonModeratorChatDelayDuration { get; protected set; }
    }
}
