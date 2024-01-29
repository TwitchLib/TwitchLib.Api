using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.EventSub.Conduits.UpdateConduit;

public sealed class UpdateConduitRequest
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }
    
    [JsonProperty(PropertyName = "shard_count")]
    public int ShardCount { get; set; }
}