#nullable disable
using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Extensions.LiveChannels;

/// <summary>
/// Get extension live channels repsonse object.
/// </summary>
public class GetExtensionLiveChannelsResponse
{
    /// <summary>
    /// The list of broadcasters that are streaming live and that have installed or activated the extension.
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public LiveChannel[] Data { get; protected set; }

    /// <summary>
    /// This field contains the cursor used to page through the results. 
    /// </summary>
    [JsonProperty(PropertyName = "pagination")]
    public Pagination Pagination { get; protected set; }
}
