using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Polls
{
    public class Choice
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; protected set; }
        [JsonProperty(PropertyName = "votes")]
        public int Votes { get; protected set; }
        [JsonProperty(PropertyName = "channel_points_votes")]
        public int ChannelPointsVotes { get; protected set; }
        [JsonProperty(PropertyName = "bits_votes")]
        public int BitsVotes { get; protected set; }
    }
}
