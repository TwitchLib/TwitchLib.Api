#nullable disable
namespace TwitchLib.Api.Core.Enums;

/// <summary>
/// Enum representing the prediction status
/// </summary>
public enum PredictionStatus
{
    /// <summary>
    /// Prediction is active
    /// </summary>
    ACTIVE,

    /// <summary>
    /// Prediction is resolved
    /// </summary>
    RESOLVED,

    /// <summary>
    /// Prediction is canceled
    /// </summary>
    CANCELED,

    /// <summary>
    /// Prediction is locked
    /// </summary>
    LOCKED
}
