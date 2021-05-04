using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Predictions
{
    public class User
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; protected set; }
        [JsonProperty(PropertyName = "login")]
        public string Login { get; protected set; }
        [JsonProperty(PropertyName = "channel_points_used")]
        public int ChannelPointsUsed { get; protected set; }
        [JsonProperty(PropertyName = "channel_points_won")]
        public int ChannelPointsWon { get; protected set; }
    }
}
