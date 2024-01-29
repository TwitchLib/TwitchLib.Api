using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.EventSub.Conduits.CreateConduit;

public sealed class CreateConduitRequest
{
    [JsonProperty(PropertyName = "shard_count")]
    public int ShardCount { get; set; }
}