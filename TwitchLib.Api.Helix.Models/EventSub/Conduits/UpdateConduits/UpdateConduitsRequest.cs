using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.EventSub.Conduits.UpdateConduits;

public class UpdateConduitsRequest
{
    /// <summary>
    /// <para>Conduit ID.</para>
    /// </summary>
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }
    /// <summary>
    /// <para>The new number of shards for this conduit.</para>
    /// </summary>
    [JsonProperty(PropertyName = "shard_count")]
    public int ShardCount { get; set; }
}