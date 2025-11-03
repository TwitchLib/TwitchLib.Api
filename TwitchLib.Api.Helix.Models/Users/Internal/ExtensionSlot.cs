#nullable disable
using TwitchLib.Api.Core.Enums;

namespace TwitchLib.Api.Helix.Models.Users.Internal;

/// <summary>
/// Extension slot.
/// </summary>
public class ExtensionSlot
{
    /// <summary>
    /// Type
    /// </summary>
    public ExtensionType Type;

    /// <summary>
    /// Slot
    /// </summary>
    public string Slot;

    /// <summary>
    /// User extension slot
    /// </summary>
    public UserExtensionState UserExtensionState;

    /// <summary>
    /// Extension slot.
    /// </summary>
    /// <param name="type">Type</param>
    /// <param name="slot">Slot</param>
    /// <param name="userExtensionState"></param>
    public ExtensionSlot(ExtensionType type, string slot, UserExtensionState userExtensionState)
    {
        Type = type;
        Slot = slot;
        UserExtensionState = userExtensionState;
    }
}
