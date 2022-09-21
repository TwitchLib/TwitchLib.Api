using System.Collections.Generic;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.EventSub
{
    public class EventSubSubscription
    {
        /// <summary>
        /// An ID that identifies the subscription.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }

        /// <summary>
        /// The subscription’s status. Possible values: enable, webhook_callback_verification_pending, webhook_callback_verification_failed, notification_failures_exceeded, authorization_revoked, user_removed
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public string Status { get; protected set; }

        /// <summary>
        /// The subscription’s type.
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; protected set; }

        /// <summary>
        /// The version of the subscription type.
        /// </summary>
        [JsonProperty(PropertyName = "version")]
        public string Version { get; protected set; }

        /// <summary>
        /// The subscription’s parameter values.
        /// </summary>
        [JsonProperty(PropertyName = "condition")]
        public Dictionary<string, string> Condition { get; protected set; }

        /// <summary>
        /// The RFC 3339 timestamp indicating when the subscription was created.
        /// </summary>
        [JsonProperty(PropertyName = "created_at")]
        public string CreatedAt { get; protected set; }

        /// <summary>
        /// The transport details used to send you notifications.
        /// </summary>
        [JsonProperty(PropertyName = "transport")]
        public EventSubTransport Transport { get; protected set; }

        /// <summary>
        /// The amount that the subscription counts against your limit.
        /// </summary>
        [JsonProperty(PropertyName = "cost")]
        public int Cost { get; protected set; }
    }
}
