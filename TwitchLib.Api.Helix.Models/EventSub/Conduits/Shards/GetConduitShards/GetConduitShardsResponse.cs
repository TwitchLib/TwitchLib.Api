using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.EventSub.Conduits.Shards.GetConduitShards;

public class GetConduitShardsResponse
{
    /// <summary>
    /// <para>List of information about a conduit's shards.</para>
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public Shard[] Shards { get; protected set; }
    /// <summary>
    /// <para>Contains information used to page through a list of results. The object is empty if there are no more pages left to page through.</para>
    /// </summary>
    [JsonProperty(PropertyName = "pagination")]
    public Pagination Pagination { get; protected set; }
}