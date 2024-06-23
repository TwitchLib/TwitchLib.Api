using System.Collections.Generic;
using System.ComponentModel;
using TwitchLib.Api.Core.Enums;

namespace TwitchLib.Api.Core.Interfaces
{
    /// <summary>
    /// Interface for Api Settings
    /// </summary>
    public interface IApiSettings
    {
        /// <summary>
        /// Access token
        /// </summary>
        string AccessToken { get; set; }

        /// <summary>
        /// Client Secret
        /// </summary>
        string Secret { get; set; }

        /// <summary>
        /// Client Id
        /// </summary>
        string ClientId { get; set; }

        /// <summary>
        /// Skip Dynamic Scope Validation boolean
        /// </summary>
        bool SkipDynamicScopeValidation { get; set; }

        /// <summary>
        /// Skip Auto Server Token Generation boolean
        /// </summary>
        bool SkipAutoServerTokenGeneration { get; set; }

        /// <summary>
        /// Scopes
        /// </summary>
        List<AuthScopes> Scopes { get; set; }

        /// <summary>
        /// Event that fires when a property changed
        /// </summary>
        event PropertyChangedEventHandler PropertyChanged;
    }
}