#nullable disable
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
    public string Id { get; set; }

    /// <summary>
    /// Scopes
    /// </summary>
    public List<AuthScopes> Scopes { get; set; }

    /// <summary>
    /// Username
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Access Token
    /// </summary>
    public string Token { get; set; }

    /// <summary>
    /// Refresh Token
    /// </summary>
    public string Refresh { get; set; }

    /// <summary>
    /// Client Id
    /// </summary>
    public string ClientId { get; set; }
}
