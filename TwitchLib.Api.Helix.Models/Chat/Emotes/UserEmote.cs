using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Chat.Emotes;

public class UserEmote : Emote
{
    /// <summary>
    /// The type of emote. The possible values are:
    /// none — No emote type was assigned to this emote.
    /// - bitstier — A Bits tier emote.
    /// - follower — A follower emote.
    /// - subscriptions — A subscriber emote.
    /// - channelpoints — An emote granted by using channel points.
    /// - rewards — An emote granted to the user through a special event.
    /// - hypetrain — An emote granted for participation in a Hype Train.
    /// - prime — An emote granted for linking an Amazon Prime account.
    /// - turbo — An emote granted for having Twitch Turbo.
    /// - smilies — Emoticons supported by Twitch.
    /// - globals — An emote accessible by everyone.
    /// - owl2019 — Emotes related to Overwatch League 2019.
    /// - twofactor — Emotes granted by enabling two-factor authentication on an account.
    /// - limitedtime — Emotes that were granted for only a limited time.
    /// </summary>
    [JsonProperty("emote_type")]
    public string EmoteType { get; protected set; }
    /// <summary>
    /// An ID that identifies the emote set that the emote belongs to.
    /// </summary>
    [JsonProperty("emote_set_id")]
    public string EmoteSetId { get; protected set; }
    /// <summary>
    /// The ID of the broadcaster who owns the emote.
    /// </summary>
    [JsonProperty("owner_id")]
    public string OwnerId { get; protected set; }
}