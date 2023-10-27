using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Channels.GetAdSchedule
{
    /// <summary>
    /// <para>Contains information related to the channel’s ad schedule.</para>
    /// </summary>
    public class AdSchedule
    {
        /// <summary>
        /// <para>The number of snoozes available for the broadcaster.</para>
        /// </summary>
        [JsonProperty(PropertyName = "snooze_count")]
        public int SnoozeCount { get; protected set; }
        /// <summary>
        /// <para>The UTC timestamp when the broadcaster will gain an additional snooze, in RFC3339 format.</para>
        /// </summary>
        [JsonProperty(PropertyName = "snooze_refresh_at")]
        public string SnoozeRefreshAt { get; protected set; }
        /// <summary>
        /// <para>The UTC timestamp of the broadcaster’s next scheduled ad, in RFC3339 format. Empty if the channel has no ad scheduled or is not live.</para>
        /// </summary>
        [JsonProperty(PropertyName = "next_ad_at")]
        public string NextAdAt { get; protected set; }
        /// <summary>
        /// <para>The length in seconds of the scheduled upcoming ad break.</para>
        /// </summary>
        [JsonProperty(PropertyName = "length_seconds")]
        public int LengthSeconds { get;protected set; }
        /// <summary>
        /// <para>The UTC timestamp of the broadcaster’s last ad-break, in RFC3339 format. Empty if the channel has not run an ad or is not live.</para>
        /// </summary>
        [JsonProperty(PropertyName = "last_ad_at")]
        public string LastAdAt { get; protected set; }
        /// <summary>
        /// <para>The amount of pre-roll free time remaining for the channel in seconds. Returns 0 if they are currently not pre-roll free.</para>
        /// </summary>
        [JsonProperty(PropertyName = "preroll_free_time_seconds")]
        public int PrerollFreeTimeSeconds { get; protected set; }
    }
}
