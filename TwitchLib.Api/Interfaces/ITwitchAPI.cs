using TwitchLib.Api.Auth;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Core.Undocumented;

namespace TwitchLib.Api.Interfaces
{
    /// <summary>
    /// ITwitchAPI Interface
    /// </summary>
    public interface ITwitchAPI
    {
        /// <summary>
        /// Settings
        /// </summary>
        IApiSettings Settings { get; }

        /// <summary>
        /// Authentication
        /// </summary>
        Auth.Auth Auth { get; }

        /// <summary>
        /// Helix
        /// </summary>
        Helix.Helix Helix { get; }

        /// <summary>
        /// Third Party
        /// </summary>
        ThirdParty.ThirdParty ThirdParty { get; }

        /// <summary>
        /// Undocumented
        /// </summary>
        Undocumented Undocumented { get; }
    }
}
