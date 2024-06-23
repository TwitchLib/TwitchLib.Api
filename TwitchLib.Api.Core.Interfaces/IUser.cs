using System;

namespace TwitchLib.Api.Core.Interfaces
{
    /// <summary>
    /// Interface for a user
    /// </summary>
    public interface IUser
    {
        string Id { get; }
        string Bio { get; }
        DateTime CreatedAt { get; }
        string DisplayName { get; }
        string Logo { get; }
        string Name { get; }
        string Type { get; }
        DateTime UpdatedAt { get; }

    }
}
