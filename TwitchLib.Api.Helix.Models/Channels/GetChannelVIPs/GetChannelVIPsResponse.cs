#nullable disable
using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Channels.GetChannelVIPs;

/// <summary>
/// <para>Response for GetVIPs that returns a list of the broadcaster's VIPs.</para>
/// </summary>
public class GetChannelVIPsResponse
{
  /// <summary>
  /// <para>The list of VIPs.</para>
  /// <para>The list is empty if the channel doesn’t have VIP users. The list does not include the broadcaster.</para>
  /// </summary>
  [JsonProperty(PropertyName = "data")]
  public ChannelVIPsResponseModel[] Data { get; protected set; }

  /// <summary>
  /// <para>Contains the information used to page through the list of results.</para>
  /// </summary>
  [JsonProperty(PropertyName = "pagination")]
  public Pagination Pagination { get; protected set; }
}
