using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TwitchLib.Api.Core.Enums;

namespace TwitchLib.Api.Helix.Models.ChannelPoints.UpdateCustomRewardRedemptionStatus;

/// <summary>
/// The request for updating a redemption’s status.
/// </summary>
public class UpdateCustomRewardRedemptionStatusRequest
{
  /// <summary>
  /// The status to set the redemption to. Possible values are: CANCELED, FULFILLED. Setting the status to CANCELED refunds the user’s channel points.
  /// </summary>
  [JsonConverter(typeof(StringEnumConverter))]
  [JsonProperty(PropertyName = "status")]
  public CustomRewardRedemptionStatus Status { get; set; }
}
