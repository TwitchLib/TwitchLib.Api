using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Channels.GetFollowedChannels;

/// <summary>
/// <para>The response for GetFollowedChannels that returns a list of broadcasters that the specified user follows.</para>
/// <para>This can also return whether a user follows a specific broadcaster.</para>
/// </summary>
public class GetFollowedChannelsResponse
{
  /// <summary>
  /// <para>The list of broadcasters that the user follows.</para>
  /// <para>The list is in descending order by followed_at, with the most recently followed broadcaster first.</para>
  /// <para>The list is empty if the user doesn’t follow anyone.</para>
  /// </summary>
  [JsonProperty(PropertyName = "data")]
  public FollowedChannel[] Data { get; protected set; }

  /// <summary>
  /// <para>Contains the information used to page through the list of results.</para>
  /// <para>The object is empty if there are no more pages left to page through.</para>
  /// </summary>
  [JsonProperty(PropertyName = "pagination")]
  public Pagination Pagination { get; protected set; }

  /// <summary>
  /// <para>The total number of broadcasters that the user follows.</para>
  /// <para>As someone pages through the list, the number may change as the user follows or unfollows broadcasters.</para>
  /// </summary>
  [JsonProperty(PropertyName = "total")]
  public int Total { get; protected set; }
}