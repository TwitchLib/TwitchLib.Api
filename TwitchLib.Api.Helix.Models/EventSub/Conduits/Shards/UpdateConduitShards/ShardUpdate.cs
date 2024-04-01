using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.EventSub.Conduits.Shards.UpdateConduitShards;

public class ShardUpdate
{
    /// <summary>
    /// <para>Shard ID.</para>
    /// </summary>
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }
    /// <summary>
    /// <para>The transport details that you want Twitch to use when sending you notifications.</para>
    /// </summary>
    [JsonProperty(PropertyName = "transport")]
    public TransportUpdate Transport { get; set; }
    
}