using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Subscriptions
{
    public class GetUserSubscriptionsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Subscription[] Data { get; protected set; }
    }
}