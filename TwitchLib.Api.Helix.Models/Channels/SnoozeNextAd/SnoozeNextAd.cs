using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Channels.SnoozeNextAd
{
    /// <summary>
    /// <para>Contains information about the channel’s snoozes and next upcoming ad after successfully snoozing.</para>
    /// </summary>
    public class SnoozeNextAd
    {
        /// <summary>
        /// <para>The number of snoozes available for the broadcaster.</para>
        /// </summary>
        [JsonProperty(PropertyName = "snooze_count")]
        public int SnoozeCount { get; protected set; }
        /// <summary>
        /// The UTC timestamp when the broadcaster will gain an additional snooze, in RFC3339 format.
        /// </summary>
        [JsonProperty(PropertyName = "snooze_refresh_at")]
        public string SnoozeRefreshAt { get; protected set; }
        /// <summary>
        /// The UTC timestamp of the broadcaster’s next scheduled ad, in RFC3339 format.
        /// </summary>
        [JsonProperty(PropertyName = "next_ad_at")]
        public string NextAdAt { get; protected set; }
    }
}
