using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Chat.ChatSettings
{
    public class ChatSettings
    {
        [JsonProperty(PropertyName = "slow_mode")]
        public bool SlowMode;
        [JsonProperty(PropertyName = "slow_mode_wait_time")]
        public int? SlowModeWaitTime;
        [JsonProperty(PropertyName = "follower_mode")]
        public bool FollowerMode;
        [JsonProperty(PropertyName = "follower_mode_duration")]
        public int? FollowerModeDuration;
        [JsonProperty(PropertyName = "subscriber_mode")]
        public bool SubscriberMode;
        [JsonProperty(PropertyName = "emote_mode")]
        public bool EmoteMode;
        [JsonProperty(PropertyName = "unique_chat_mode")]
        public bool UniqueChatMode;
        [JsonProperty(PropertyName = "non_moderator_chat_delay")]
        public bool NonModeratorChatDelay;
        [JsonProperty(PropertyName = "non_moderator_chat_delay_duration")]
        public int? NonModeratorChatDelayDuration;
    }
}
