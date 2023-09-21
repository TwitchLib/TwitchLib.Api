﻿using System.Collections.Generic;
using System.ComponentModel;
using TwitchLib.Api.Core.Enums;

namespace TwitchLib.Api.Core.Interfaces
{
    public interface IApiSettings
    {
        string AccessToken { get; set; }
        string Secret { get; set; }
        string ClientId { get; set; }
        bool SkipDynamicScopeValidation { get; set; }
        bool SkipAutoServerTokenGeneration { get; set; }
        List<AuthScopes> Scopes { get; set; }
        int OAuthResponsePort { get; set; }
        string OAuthTokenFile { get; set; }
        bool UseUserTokenForHelixCalls {  get; set; }
        bool EnableInsecureTokenStorage { get; set; }

        event PropertyChangedEventHandler PropertyChanged;
    }
}