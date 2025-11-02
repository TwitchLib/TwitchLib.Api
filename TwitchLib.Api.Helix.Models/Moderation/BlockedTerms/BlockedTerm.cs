#nullable disable
using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Moderation.BlockedTerms;

/// <summary>
/// A single blocked term that the broadcaster added.
/// </summary>
public class BlockedTerm
{
    /// <summary>
    /// The broadcaster that owns the list of blocked terms.
    /// </summary>
    [JsonProperty(PropertyName = "broadcaster_id")]
    public string BroadcasterId { get; protected set; }

    /// <summary>
    /// The moderator that blocked the word or phrase from being used in the broadcaster’s chat room.
    /// </summary>
    [JsonProperty(PropertyName = "moderator_id")]
    public string ModeratorId { get; protected set; }

    /// <summary>
    /// An ID that identifies this blocked term.
    /// </summary>
    [JsonProperty(PropertyName = "id")]
    public string Id { get; protected set; }

    /// <summary>
    /// The blocked word or phrase.
    /// </summary>
    [JsonProperty(PropertyName = "text")]
    public string Text { get; protected set; }

    /// <summary>
    /// The UTC date and time (in RFC3339 format) that the term was blocked.
    /// </summary>
    [JsonProperty(PropertyName = "created_at")]
    public DateTime CreatedAt { get; protected set; }

    /// <summary>
    /// The UTC date and time (in RFC3339 format) that the term was updated.
    /// </summary>
    [JsonProperty(PropertyName = "updated_at")]
    public DateTime UpdatedAt { get; protected set; }

    /// <summary>
    /// The UTC date and time (in RFC3339 format) that the blocked term is set to expire.
    /// </summary>
    [JsonProperty(PropertyName = "expires_at")]
    public DateTime? ExpiresAt { get; protected set; }
}
