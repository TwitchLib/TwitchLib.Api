#nullable disable
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.EventSub.Conduits.Shards;

/// <summary>
/// A Shard.
/// </summary>
public class Shard
{
    /// <summary>
    /// <para>Shard ID.</para>
    /// </summary>
    [JsonProperty(PropertyName = "id")]
    public string Id { get; protected set; }

    /// <summary>
    /// <para>The shard status. The subscriber receives events only for enabled shards. Possible values are: </para>
    /// <para>enabled, webhook_callback_verification_pending, webhook_callback_verification_failed, notification_failures_exceeded, websocket_disconnected, websocket_failed_ping_pong, websocket_received_inbound_traffic, websocket_internal_error, websocket_network_timeout, websocket_network_error</para>
    /// </summary>
    [JsonProperty(PropertyName = "status")]
    public string Status { get; protected set; }

    /// <summary>
    /// <para>The transport details used to send the notifications.</para>
    /// </summary>
    [JsonProperty(PropertyName = "transport")]
    public Transport Transport { get; set; }
}