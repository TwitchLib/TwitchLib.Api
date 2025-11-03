#nullable disable
using Newtonsoft.Json;
using TwitchLib.Api.Core.Enums;

namespace TwitchLib.Api.Helix.Models.Entitlements.UpdateDropsEntitlements;

/// <summary>
/// The Drop entitlement’s fulfillment update status.
/// </summary>
public class DropEntitlementUpdate
{
    /// <summary>
    /// A string that indicates whether the status of the entitlements in the ids field were successfully updated.
    /// </summary>
    [JsonProperty(PropertyName = "status")]
    public DropEntitlementUpdateStatus Status { get; protected set; }

    /// <summary>
    /// The list of entitlements that the status in the status field applies to.
    /// </summary>
    [JsonProperty(PropertyName = "ids")]
    public string[] Ids { get; protected set; }
}