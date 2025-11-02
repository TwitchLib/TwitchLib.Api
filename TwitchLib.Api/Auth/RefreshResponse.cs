#nullable disable
using Newtonsoft.Json;

namespace TwitchLib.Api.Auth;

/// <summary>
/// Refresh Response
/// </summary>
public class RefreshResponse
{
    /// <summary>
    /// Access Token
    /// </summary>
    [JsonProperty(PropertyName = "access_token")]
    public string AccessToken { get; protected set; }

    /// <summary>
    /// Refresh Token
    /// </summary>
    [JsonProperty(PropertyName = "refresh_token")]
    public string RefreshToken { get; protected set; }
    
    /// <summary>
    /// Expires In
    /// </summary>
    [JsonProperty(PropertyName = "expires_in")]
    public int ExpiresIn { get; protected set; }

    /// <summary>
    /// Scopes
    /// </summary>
    [JsonProperty(PropertyName = "scope")]
    public string[] Scopes { get; protected set; }
}
