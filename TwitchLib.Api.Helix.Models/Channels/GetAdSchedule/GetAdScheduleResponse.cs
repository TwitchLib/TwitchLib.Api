﻿using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Channels.GetAdSchedule
{
    /// <summary>
    /// <para>Response to getting ad schedule</para>
    /// </summary>
    public class GetAdScheduleResponse
    {
        /// <summary>
        /// <para>A list that contains information related to the channel’s ad schedule.</para>
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public AdSchedule[] Data { get; protected set; }
    }
}
