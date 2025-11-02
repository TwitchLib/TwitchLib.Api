#nullable disable
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Chat.Badges.GetChannelChatBadges;

/// <summary>
/// The broadcaster’s list of custom chat badges response object.
/// </summary>
public class GetChannelChatBadgesResponse
{
    /// <summary>
    /// The list of chat badges. The list is sorted in ascending order by set_id, and within a set, the list is sorted in ascending order by id.
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public BadgeEmoteSet[] EmoteSet { get; protected set; }
}
