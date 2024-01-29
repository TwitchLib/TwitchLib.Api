using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.EventSub.Conduits.UpdateConduit;

public sealed class UpdateConduitResponse
{
    [JsonProperty(PropertyName = "data")]
    public ConduitShard[] Data { get; protected set; }
}