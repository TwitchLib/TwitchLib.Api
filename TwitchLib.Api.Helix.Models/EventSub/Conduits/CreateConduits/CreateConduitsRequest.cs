using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.EventSub.Conduits.CreateConduits;

public class CreateConduitsRequest
{
    /// <summary>
    /// <para>The number of shards to create for this conduit.</para>
    /// </summary>
    [JsonProperty(PropertyName = "shard_count")]
    public int ShardCount { get; set; }
}