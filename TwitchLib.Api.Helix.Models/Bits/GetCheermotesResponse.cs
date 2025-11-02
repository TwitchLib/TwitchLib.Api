#nullable disable
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Bits;

/// <summary>
/// Response from GetCheermotes which gets a list of Cheermotes that users can use to cheer Bits in any Bits-enabled channel’s chat room.
/// </summary>
public class GetCheermotesResponse
{
  /// <summary>
  /// The list of Cheermotes. The list is in ascending order by the order field’s value.
  /// </summary>
  [JsonProperty(PropertyName = "data")]
  public Cheermote[] Listings { get; protected set; }
}
