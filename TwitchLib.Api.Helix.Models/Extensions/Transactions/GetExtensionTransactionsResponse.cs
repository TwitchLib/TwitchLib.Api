using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Extensions.Transactions;

/// <summary>
/// Get extension transactions response object.
/// </summary>
public class GetExtensionTransactionsResponse
{
    /// <summary>
    /// The list of transactions.
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public Transaction[] Data { get; protected set; }

    /// <summary>
    /// Contains the information used to page through the list of results.
    /// </summary>
    [JsonProperty(PropertyName = "pagination")]
    public Pagination Pagination { get; protected set; }
}
