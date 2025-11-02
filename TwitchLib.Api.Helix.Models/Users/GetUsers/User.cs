#nullable disable
using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Users.GetUsers;

/// <summary>
/// A user.
/// </summary>
public class User
{
    /// <summary>
    /// An ID that identifies the user.
    /// </summary>
    [JsonProperty(PropertyName = "id")]
    public string Id { get; protected set; }

    /// <summary>
    /// The user’s login name.
    /// </summary>
    [JsonProperty(PropertyName = "login")]
    public string Login { get; protected set; }

    /// <summary>
    /// The user’s display name.
    /// </summary>
    [JsonProperty(PropertyName = "display_name")]
    public string DisplayName { get; protected set; }

    /// <summary>
    /// The UTC date and time that the user’s account was created. The timestamp is in RFC3339 format.
    /// </summary>
    [JsonProperty(PropertyName = "created_at")]
    public DateTime CreatedAt { get; protected set; }

    /// <summary>
    /// The type of user.
    /// </summary>
    [JsonProperty(PropertyName = "type")]
    public string Type { get; protected set; }

    /// <summary>
    /// The type of broadcaster.
    /// </summary>
    [JsonProperty(PropertyName = "broadcaster_type")]
    public string BroadcasterType { get; protected set; }

    /// <summary>
    /// The user’s description of their channel.
    /// </summary>
    [JsonProperty(PropertyName = "description")]
    public string Description { get; protected set; }

    /// <summary>
    /// A URL to the user’s profile image.
    /// </summary>
    [JsonProperty(PropertyName = "profile_image_url")]
    public string ProfileImageUrl { get; protected set; }

    /// <summary>
    /// A URL to the user’s offline image.
    /// </summary>
    [JsonProperty(PropertyName = "offline_image_url")]
    public string OfflineImageUrl { get; protected set; }

    /// <summary>
    /// The number of times the user’s channel has been viewed.
    /// </summary>
    [Obsolete]
    [JsonProperty(PropertyName = "view_count")]
    public long ViewCount { get; protected set; }

    /// <summary>
    /// The user’s verified email address. 
    /// The object includes this field only if the user access token includes the user:read:email scope.
    /// </summary>
    [JsonProperty(PropertyName = "email")]
    public string Email { get; protected set; }
}
