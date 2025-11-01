using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Channels.GetChannelInformation;

/// <summary>
/// <para>Response for GetChannelInformation which returns information about one or more channels.</para>
/// </summary>
public class GetChannelInformationResponse
{
  /// <summary>
  /// <para>A list that contains information about the specified channels.</para>
  /// <para>The list is empty if the specified channels weren’t found.</para>
  /// </summary>
  [JsonProperty(PropertyName = "data")]
  public ChannelInformation[] Data { get; protected set; }
}
