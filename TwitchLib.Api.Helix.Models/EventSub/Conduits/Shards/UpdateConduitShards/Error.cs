using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.EventSub.Conduits.Shards.UpdateConduitShards;

public class Error
{
    /// <summary>
    /// <para>Shard ID.</para>
    /// </summary>
    [JsonProperty(PropertyName = "id")]
    public string Id { get; protected set; }
    /// <summary>
    /// <para>The error that occurred while updating the shard.</para>
    /// </summary>
    [JsonProperty(PropertyName = "message")]
    public string Message { get; protected set; }
    /// <summary>
    /// <para>Error codes used to represent a specific error condition while attempting to update shards.</para>
    /// </summary>
    [JsonProperty(PropertyName = "code")]
    public string Code { get; protected set; }
}