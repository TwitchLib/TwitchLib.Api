using System.Collections.Generic;
using TwitchLib.Api.Core.Enums;

namespace TwitchLib.Api.Events;

/// <summary>
/// On User Authorization Detected Args
/// </summary>
public class OnUserAuthorizationDetectedArgs
{
    /// <summary>
    /// Id
    /// </summary>
    public string Id { get; set; } = null!;

    /// <summary>
    /// Scopes
    /// </summary>
    public List<AuthScopes> Scopes { get; set; } = null!;

    /// <summary>
    /// Username
    /// </summary>
    public string Username { get; set; } = null!;

    /// <summary>
    /// Access Token
    /// </summary>
    public string Token { get; set; } = null!;

    /// <summary>
    /// Refresh Token
    /// </summary>
    public string Refresh { get; set; } = null!;

    /// <summary>
    /// Client Id
    /// </summary>
    public string ClientId { get; set; } = null!;
}
