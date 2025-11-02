#nullable disable
namespace TwitchLib.Api.Core.Enums;

/// <summary>
/// Enum representing the reason a User was blocked
/// </summary>
public enum BlockUserReasonEnum
{
    /// <summary>
    /// Spam
    /// </summary>
    Spam,

    /// <summary>
    /// Harrassment
    /// </summary>
    Harassment,

    /// <summary>
    /// Some other reason
    /// </summary>
    Other
}
