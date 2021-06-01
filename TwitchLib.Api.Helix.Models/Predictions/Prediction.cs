using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Predictions
{
    public class Prediction
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
        [JsonProperty(PropertyName = "winning_outcome_id")]
        public string WinningOutcomeId { get; protected set; }
        [JsonProperty(PropertyName = "outcomes")]
        public Outcome[] Outcomes { get; protected set; }
    }
}
