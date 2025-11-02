#nullable disable
namespace TwitchLib.Api.Core.Enums;

/// <summary>
/// Enum representing drop entitlement update status
/// </summary>
public enum DropEntitlementUpdateStatus
{
    /// <summary>
    /// Completed
    /// </summary>
    SUCCESS,

    /// <summary>
    /// Not Authorized
    /// </summary>
    UNAUTHORIZED,

    /// <summary>
    /// Failed
    /// </summary>
    UPDATE_FAILED,

    /// <summary>
    /// Invalid Id
    /// </summary>
    INVALID_ID,

    /// <summary>
    /// Not found
    /// </summary>
    NOT_FOUND
}