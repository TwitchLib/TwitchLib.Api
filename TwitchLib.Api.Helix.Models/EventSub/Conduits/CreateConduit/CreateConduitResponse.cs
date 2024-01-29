using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.EventSub.Conduits.CreateConduit;

public sealed class CreateConduitResponse
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }
    [JsonProperty(PropertyName = "shard_count")]
    public string ShardCount { get; set; }
}