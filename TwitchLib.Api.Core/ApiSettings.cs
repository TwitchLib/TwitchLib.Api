using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Interfaces;

namespace TwitchLib.Api.Core
{
    public class ApiSettings : IApiSettings, INotifyPropertyChanged
    {
        private string _clientId;
        private string _secret;
        private string _accessToken;
        private bool _skipDynamicScopeValidation;
        private bool _skipAutoServerTokenGeneration;
        private List<AuthScopes> _scopes = new List<AuthScopes>();
        private int _oauthResponsePort = 5000;
        private string _oauthTokenFile = System.Environment.ExpandEnvironmentVariables("%AppData%\\TwitchLib.API\\" + System.Diagnostics.Process.GetCurrentProcess().ProcessName + ".json");
        private bool _useUserTokenForHelixCalls = false;
        private bool _enableInsecureTokenStorage = false;
        private IUserAccessTokenManager _userAccessTokenManager;


        public string ClientId
        {
            get => _clientId;
            set
            {
                if (value != _clientId)
                {
                    _clientId = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Secret
        {
            get => _secret;
            set
            {
                if (value != _secret)
                {
                    _secret = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string AccessToken
        {
            get => _accessToken;
            set
            {
                if (value != _accessToken)
                {
                    _accessToken = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool SkipDynamicScopeValidation
        {
            get => _skipDynamicScopeValidation;
            set
            {
                if (value != _skipDynamicScopeValidation)
                {
                    _skipDynamicScopeValidation = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool SkipAutoServerTokenGeneration
        {
            get => _skipAutoServerTokenGeneration;
            set
            {
                if (value != _skipAutoServerTokenGeneration)
                {
                    _skipAutoServerTokenGeneration = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public List<AuthScopes> Scopes
        {
            get => _scopes;
            set
            {
                if (value != _scopes)
                {
                    _scopes = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// If you are using TwitchLib.Api and make calls to API endpoints that require a user token, then you can use 
        /// your ClientSecret and ClientID to establish an OAuth token for your service. Part of this token generation 
        /// process requires Twitch to authenticate your application using your browser. Twitch will return your browser
        /// back to this library for token storage so, this library needs to listen for your browser's request on a port. 
        /// By default, this is port 5000. If you have another application also on port 5000, set this to another open port. 
        /// </summary>
        public int OAuthResponsePort
        {
            get => _oauthResponsePort;
            set
            {
                if (value != _oauthResponsePort)
                {
                    _oauthResponsePort = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Storage for oAuth refresh token, expiration dates, etc. Defaults to %AppData%\\TwitchLib.API\\[ApplicationName].json
        /// Set this if you will be running multiple instances of the same application that you would like to use with different 
        /// user tokens.
        /// </summary>
        public string OAuthTokenFile
        {
            get => _oauthTokenFile;
            set
            {
                if (value != _oauthTokenFile)
                {
                    _oauthTokenFile = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Set this value to true to enable Helix calls that require an oAuth User Token. This requires you to also set
        /// ApiSettings.ClientID and ApiSettings.Secret. 
        /// </summary>
        public bool UseUserTokenForHelixCalls
        {
            get => _useUserTokenForHelixCalls;
            set
            {
                if (value != _useUserTokenForHelixCalls)
                {
                    if (String.IsNullOrWhiteSpace(ClientId) == true || String.IsNullOrWhiteSpace(Secret) == true)
                        throw new Exception("You must set ApiSettings.ClientId and ApiSettings.Secret before you can enable this setting.");

                    _useUserTokenForHelixCalls = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Setting this value to true will enable storage of the oAuth refresh token and other data. This storage will be done in 
        /// an unencrypted, insecure local file. Anyone else with access to your computer could read this file and gain access to 
        /// your Twitch account in unexpected ways. Only set this value to true if you have properly secured your computer.
        /// </summary>
        public bool EnableInsecureTokenStorage
        {
            get => _enableInsecureTokenStorage;
            set
            {
                if (value != _enableInsecureTokenStorage)
                {
                    _enableInsecureTokenStorage = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
