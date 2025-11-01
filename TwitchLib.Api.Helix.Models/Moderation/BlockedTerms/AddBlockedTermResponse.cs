using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Moderation.BlockedTerms;

/// <summary>
/// Add blocked term response object.
/// </summary>
public class AddBlockedTermResponse
{
    /// <summary>
    /// A list that contains the single blocked term that the broadcaster added.
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public BlockedTerm[] Data { get; protected set; }
}
