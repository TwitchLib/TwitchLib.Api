#nullable disable
using System;

namespace TwitchLib.Api.Core.Interfaces;

/// <summary>
/// Interface for a follow
/// </summary>
public interface IFollow
{
    /// <summary>
    /// Created at date and time
    /// </summary>
    DateTime CreatedAt { get; }

    /// <summary>
    /// Notifications boolean
    /// </summary>
    bool Notifications { get; }

    /// <summary>
    /// User interface
    /// </summary>
    IUser User { get;  }
}
