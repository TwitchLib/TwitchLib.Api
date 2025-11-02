#nullable disable
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.EventSub.Conduits.Shards.UpdateConduitShards;

/// <summary>
/// The transport details used to send the notifications.
/// </summary>
public class TransportUpdate
{
    /// <summary>
    /// <para>The transport method. Possible values are:</para>
    /// <para>webhook, websocket</para>
    /// </summary>
    [JsonProperty(PropertyName = "method")]
    public string Method { get; set; }

    /// <summary>
    /// <para>The callback URL where the notifications are sent. The URL must use the HTTPS protocol and port 443. See Processing an event.Specify this field only if method is set to webhook.NOTE: Redirects are not followed.</para>
    /// </summary>
    [JsonProperty(PropertyName = "callback", NullValueHandling = NullValueHandling.Ignore)]
    public string Callback { get; set; }

    /// <summary>
    /// <para>The secret used to verify the signature. The secret must be an ASCII string that’s a minimum of 10 characters long and a maximum of 100 characters long. For information about how the secret is used, see Verifying the event message.Specify this field only if method is set to webhook.</para>
    /// </summary>
    [JsonProperty(PropertyName = "secret", NullValueHandling = NullValueHandling.Ignore)]
    public string Secret { get; set; }

    /// <summary>
    /// <para>An ID that identifies the WebSocket to send notifications to. When you connect to EventSub using WebSockets, the server returns the ID in the Welcome message.Specify this field only if method is set to websocket.</para>
    /// </summary>
    [JsonProperty(PropertyName = "session_id", NullValueHandling = NullValueHandling.Ignore)]
    public string SessionId { get; set; }
}