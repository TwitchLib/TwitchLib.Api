using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Interfaces;

namespace TwitchLib.Api
{
    public class ApiSettings : IApiSettings, INotifyPropertyChanged
    {
        private string _clientId;
        private string _secret;
        private string _accessToken;
        private bool _skipDynamicScopeValidation;
        private bool _skipAutoServerTokenGeneration;
        private List<AuthScopes> _scopes;
        public string ClientId
        {
            get
            {
                return _clientId;
            }
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
            get
            {
                return _secret;
            }
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
            get
            {
                return _accessToken;
            }
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
            get
            {
                return _skipDynamicScopeValidation;
            }
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
            get
            {
                return _skipAutoServerTokenGeneration;
            }
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
            get
            {
                return _scopes;
            }
            set
            {
                if (value != _scopes)
                {
                    _scopes = value;
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
