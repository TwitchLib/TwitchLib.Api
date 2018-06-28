using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Models.Helix.Users.UpdateUserExtensions
{
    public class ExtensionSlot
    {
        public Enums.ExtensionType Type;
        public string Slot;
        public UserExtensionState UserExtensionState;

        public ExtensionSlot(Enums.ExtensionType type, string slot, UserExtensionState userExtensionState)
        {
            Type = type;
            Slot = slot;
            UserExtensionState = userExtensionState;
        }
    }
}
