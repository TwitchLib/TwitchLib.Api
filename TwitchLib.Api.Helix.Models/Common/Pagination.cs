#nullable disable
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Common;

/// <summary>
/// <para><see href="https://dev.twitch.tv/docs/api/guide/#pagination">
/// Twitch Docs: Pagination</see></para>
/// <para>Contains the cusor for the page.</para>
/// <para>The object is empty if there are no more pages left to page through.</para>
/// </summary>
public class Pagination
{
  /// <summary>
  /// <para>The cursor used to get the next page of results.</para>
  /// <para>Use the cursor’s value to set the after or before query parameter depending on the direction you want to page.</para>
  /// </summary>
  [JsonProperty(PropertyName = "cursor")]
  public string Cursor { get; protected set; }
}
