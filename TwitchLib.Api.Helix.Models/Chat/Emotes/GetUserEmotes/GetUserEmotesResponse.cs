using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Chat.Emotes.GetUserEmotes;

public class GetUserEmotesResponse
{
    [JsonProperty("data")]
    public UserEmote[] Data { get; protected set; }
    [JsonProperty("template")]
    public string Template { get; protected set; }
    [JsonProperty("pagination")]
    public Pagination Pagination { get; protected set; }
}