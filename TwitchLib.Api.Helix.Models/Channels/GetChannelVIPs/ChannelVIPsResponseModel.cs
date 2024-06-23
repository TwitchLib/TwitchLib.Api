using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Channels.GetChannelVIPs;

/// <summary>
/// <para>A user that is a VIP in a broadcaster's channel.</para>
/// </summary>
public class ChannelVIPsResponseModel
{
  /// <summary>
  /// <para>An ID that uniquely identifies the VIP user.</para>
  /// </summary>
  [JsonProperty(PropertyName = "user_id")]
  public string UserId { get; protected set; }
  /// <summary>
  /// <para>The user’s display name. (Name has capitalization)</para>
  /// </summary>
  [JsonProperty(PropertyName = "user_name")]
  public string UserName { get; protected set; }
  /// <summary>
  /// <para>The user’s login name. (Name is lowercase)</para>
  /// </summary>
  [JsonProperty(PropertyName = "user_login")]
  public string UserLogin { get; protected set; }
}
