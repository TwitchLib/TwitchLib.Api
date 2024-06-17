using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Moderation.GetModeratedChannels
{
    /// <summary>
    /// Channel that the user has moderator privileges in.
    /// </summary>
    public class ModeratedChannel
    {
        /// <summary>
        /// An ID that uniquely identifies the channel this user can moderate.
        /// </summary>
        [JsonProperty(PropertyName = "broadcaster_id")]
        public string BroadcasterId { get; protected set; }
        /// <summary>
        /// The channel’s login name.
        /// </summary>
        [JsonProperty(PropertyName = "broadcaster_login")]
        public string BroadcasterLogin { get; protected set; }
        /// <summary>
        /// The channels’ display name.
        /// </summary>
        [JsonProperty(PropertyName = "broadcaster_name")]
        public string BroadcasterName { get; protected set; }
    }
}
