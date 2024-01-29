using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.EventSub.Conduits.CreateConduit;

public sealed class CreateConduitResponse
{
    public ConduitShard[] Data { get; protected set; }
}