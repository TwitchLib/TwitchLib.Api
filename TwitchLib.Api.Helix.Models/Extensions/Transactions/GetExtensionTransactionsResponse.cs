using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Extensions.Transactions
{
    public class GetExtensionTransactionsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Transaction[] Data { get; protected set; }
        [JsonProperty(PropertyName = "pagination")]
        public Pagination Pagination { get; protected set; }
    }
}
