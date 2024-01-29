using System;

namespace TwitchLib.Api.Helix.Models.EventSub.Conduits.GetConduits;

public sealed class GetConduitsResponse
{
    public ConduitShard[] Data { get; protected set; }
}