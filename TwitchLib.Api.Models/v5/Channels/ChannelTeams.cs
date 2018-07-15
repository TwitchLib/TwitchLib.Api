﻿using Newtonsoft.Json;

namespace TwitchLib.Api.Models.V5.Channels
{
    /// <summary>Class representing the teams response of a channel from Twitch API.</summary>
    public class ChannelTeams
    {
        #region Teams
        /// <summary>Property representing the channel teams.</summary>
        [JsonProperty(PropertyName = "teams")]
        public Teams.Team[] Teams { get; protected set; }
        #endregion
    }
}
