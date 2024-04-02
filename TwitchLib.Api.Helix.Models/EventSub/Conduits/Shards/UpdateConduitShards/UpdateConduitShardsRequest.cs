using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.EventSub.Conduits.Shards.UpdateConduitShards;

public class UpdateConduitShardsRequest
{
    /// <summary>
    /// <para>Conduit ID.</para>
    /// </summary>
    [JsonProperty(PropertyName = "conduit_id")]
    public string ConduitId { get; set; }
    /// <summary>
    /// <para>List of shards to update.</para>
    /// </summary>
    [JsonProperty(PropertyName = "shards")]
    public ShardUpdate[] Shards { get; set; }
}