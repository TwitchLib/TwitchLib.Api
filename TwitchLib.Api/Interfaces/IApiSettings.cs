using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Enums;

namespace TwitchLib.Api.Interfaces
{
    public interface IApiSettings
    {
        string AccessToken { get; }
        string ClientId { get; }
        ApiSettings.CredentialValidators Validators { get; }
        List<AuthScopes> Scopes { get; }

        Task SetClientIdAsync(string clientId);
        Task SetAccessTokenAsync(string accessToken);

        void DynamicScopeValidation(AuthScopes requiredScope, string accessToken = null);
        void ValidateScope(AuthScopes requiredScope, string accessToken = null);
    }
}