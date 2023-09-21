using System.Collections.Generic;
using System.ComponentModel;
using TwitchLib.Api.Core.Enums;

namespace TwitchLib.Api.Core.Interfaces
{
    /// <summary>
    /// These are settings that are shared throughout the application. Define these before
    /// creating an instance of TwitchAPI
    /// </summary>
    public interface IApiSettings
    {
        /// <summary>
        /// The current client credential access token. Provides limited access to some of the TwitchAPI.
        /// </summary>
        string AccessToken { get; set; }
        /// <summary>
        /// Your application's Secret. Do not expose this to anyone else. This comes from the Twitch developer panel. https://dev.twitch.tv/console
        /// </summary>
        string Secret { get; set; }
        /// <summary>
        /// Your application's client ID. This comes from the Twitch developer panel. https://dev.twitch.tv/console
        /// </summary>
        string ClientId { get; set; }
        /// <summary>
        /// This does not appear to be used.
        /// </summary>
        bool SkipDynamicScopeValidation { get; set; }
        /// <summary>
        /// If AccessToken is null, and this is set to true, then Helix API calls will not attempt to use 
        /// the ClientID/Secret to generate a client_credential access token.
        /// </summary>
        bool SkipAutoServerTokenGeneration { get; set; }
        /// <summary>
        /// Add scopes that your application will be using to this collection before calling any Helix APIs. 
        /// A list of scopes can be found here: https://dev.twitch.tv/docs/authentication/scopes/
        /// See the TwitchAPI reference for the scopes specific to each API. 
        /// Note: Do not add ALL the scopes, or your account may be banned (see warning here: https://dev.twitch.tv/docs/authentication/scopes/)
        /// </summary>
        List<AuthScopes> Scopes { get; set; }
        /// <summary>
        /// Set this value to another port if you have another application already listening to port 5000 on your machine.
        /// Defaults to: 5000
        /// </summary>
        int OAuthResponsePort { get; set; }
        /// <summary>
        ///  Set this value to a hostname or IP address if you have a multi-homed machine (more than one IP address) 
        ///  and you would like to bind the OAuth response listener to a specific IP address. Defaults to 'localhost'
        /// </summary>
        string OAuthResponseHostname { get; set; }
        /// <summary>
        /// Storage for oAuth refresh token, expiration dates, etc. Defaults to %AppData%\\TwitchLib.API\\[ApplicationName].json
        /// Set this if you will be running multiple instances of the same application that you would like to use with different 
        /// user tokens.
        /// </summary>
        string OAuthTokenFile { get; set; }
        /// <summary>
        /// Set this value to true to enable Helix calls that require an oAuth User Token. This requires you to also set
        /// ApiSettings.ClientID and ApiSettings.Secret. 
        /// </summary>
        bool UseUserTokenForHelixCalls {  get; set; }
        /// <summary>
        /// Setting this value to true will enable storage of the oAuth refresh token and other data. This storage will be done in 
        /// an unencrypted, insecure local file. Anyone else with access to your computer could read this file and gain access to 
        /// your Twitch account in unexpected ways. Only set this value to true if you have properly secured your computer.
        /// If you do not set this value to True, and UseUserTokenForHelixCalls = True, a browser window will always open on the
        /// first call to any Helix API to perform the OAuth handshake.
        /// Defaults to: False
        /// </summary>
        bool EnableInsecureTokenStorage { get; set; }

        event PropertyChangedEventHandler PropertyChanged;
    }
}