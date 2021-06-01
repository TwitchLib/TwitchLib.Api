﻿using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Subscriptions
{
    public class GetBroadcasterSubscriptionsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Subscription[] Data { get; protected set; }
        [JsonProperty(PropertyName = "pagination")]
        public Pagination Pagination { get; protected set; }
    }
}