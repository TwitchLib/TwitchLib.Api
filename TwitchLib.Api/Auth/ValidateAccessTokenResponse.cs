#nullable disable
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TwitchLib.Api.Auth;

/// <summary>
/// Validate Access Token
/// </summary>
public class ValidateAccessTokenResponse
{
    /// <summary>
    /// Client Id
    /// </summary>
    [JsonProperty(PropertyName = "client_id")]
    public string ClientId { get; protected set; }

    /// <summary>
    /// Login Name
    /// </summary>
    [JsonProperty(PropertyName = "login")]
    public string Login { get; protected set; }

    /// <summary>
    /// Scopes
    /// </summary>
    [JsonProperty(PropertyName = "scopes")]
    public List<string> Scopes { get; protected set; }

    /// <summary>
    /// User Id
    /// </summary>
    [JsonProperty(PropertyName = "user_id")]
    public string UserId { get; protected set; }

    /// <summary>
    /// Expires In
    /// </summary>
    [JsonProperty(PropertyName = "expires_in")]
    public int ExpiresIn { get; protected set; }
}
