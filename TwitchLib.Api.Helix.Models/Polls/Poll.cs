using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Polls
{
    public class Poll
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "broadcaster_id")]
        public string BroadcasterId { get; protected set; }
        [JsonProperty(PropertyName = "broadcaster_name")]
        public string BroadcasterName { get; protected set; }
        [JsonProperty(PropertyName = "broadcaster_login")]
        public string BroadcasterLogin { get; protected set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; protected set; }
        [JsonProperty(PropertyName = "choices")]
        public Choice[] Choices { get; protected set; }
        [JsonProperty(PropertyName = "bits_voting_enabled")]
        public bool BitsVotingEnabled { get; protected set; }
        [JsonProperty(PropertyName = "bits_per_vote")]
        public int BitsPerVote { get; protected set; }
        [JsonProperty(PropertyName = "channel_points_voting_enabled")]
        public bool ChannelPointsVotingEnabled { get; protected set; }
        [JsonProperty(PropertyName = "channel_points_per_vote")]
        public int ChannelPointsPerVote { get; protected set; }
        [JsonProperty(PropertyName = "status")]
        public string Status { get; protected set; }
        [JsonProperty(PropertyName = "duration")]
        public int DurationSeconds { get; protected set; }
        [JsonProperty(PropertyName = "started_at")]
        public DateTime StartedAt { get; protected set; }
    }
}
