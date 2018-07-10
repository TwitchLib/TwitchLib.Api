using System.Collections.Generic;
using TwitchLib.Api.Enums;

namespace TwitchLib.Api.Interfaces
{
    public interface IApiSettings
    {
        string AccessToken { get; set; }
        string Secret { get; set; }
        string ClientId { get; set; }
        bool SkipDynamicScopeValidation { get; set; }
        bool SkipAutoServerTokenGeneration { get; set; }
        List<AuthScopes> Scopes { get; set; }
    }
}