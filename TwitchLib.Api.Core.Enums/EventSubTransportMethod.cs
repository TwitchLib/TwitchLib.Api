#nullable disable
namespace TwitchLib.Api.Core.Enums;

/// <summary>
/// Enum representing the eventsub transport method
/// </summary>
public enum EventSubTransportMethod
{
    /// <summary>
    /// Webhook
    /// </summary>
    Webhook,

    /// <summary>
    /// Websocket
    /// </summary>
    Websocket,

    /// <summary>
    /// Conduit
    /// </summary>
    Conduit
}
