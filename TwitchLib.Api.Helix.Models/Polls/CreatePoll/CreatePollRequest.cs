using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Polls.CreatePoll
{
    public class CreatePollRequest
    {
        [JsonProperty(PropertyName = "broadcaster_id")]
        public string BroadcasterId { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "choices")]
        public Choice[] Choices { get; set; }
        [JsonProperty(PropertyName = "bits_voting_enabled")]
        public bool BitsVotingEnabled { get; set; }
        [JsonProperty(PropertyName = "bits_per_vote")]
        public int BitsPerVote { get; set; }
        [JsonProperty(PropertyName = "channel_points_voting_enabled")]
        public bool ChannelPointsVotingEnabled { get; set; }
        [JsonProperty(PropertyName = "channel_points_per_vote")]
        public int ChannelPointsPerVote { get; set; }
        [JsonProperty(PropertyName = "duration")]
        public int DurationSeconds { get; set; }
    }
}
