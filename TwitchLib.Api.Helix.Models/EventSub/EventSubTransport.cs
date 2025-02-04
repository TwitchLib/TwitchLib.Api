using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.EventSub;

/// <summary>
/// The transport details used to send the notifications.
/// </summary>
public class EventSubTransport
{
    /// <summary>
    /// The transport method. Possible values are:
    /// </summary>
    [JsonProperty(PropertyName = "method")]
    public string Method { get; protected set; }

    /// <summary>
    /// The callback URL where the notifications are sent. Included only if method is set to webhook.
    /// </summary>
    [JsonProperty(PropertyName = "callback")]
    public string Callback { get; protected set; }

    /// <summary>
    /// An ID that identifies the WebSocket that notifications are sent to. Included only if method is set to websocket.
    /// </summary>
    [JsonProperty(PropertyName = "session_id")]
    public string SessionId { get; protected set; }

    /// <summary>
    /// The UTC date and time that the WebSocket connection was established. Included only if method is set to websocket.
    /// </summary>
    [JsonProperty(PropertyName = "disconnected_at")]
    public string DisconnectedAt { get; protected set; }
}
