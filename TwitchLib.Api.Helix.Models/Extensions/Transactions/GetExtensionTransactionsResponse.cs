using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Extensions.Transactions
{
    public class GetExtensionTransactionsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Transaction[] CreatedClips { get; protected set; }
    }
}
