using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Enums;
using TwitchLib.Api.Models;

namespace TwitchLib.Api.Interfaces
{
    public interface IApiSettings
    {
        string AccessToken { get; set; }
        string ClientId { get; set; }
        ApiSettings.CredentialValidators Validators { get; }
        List<AuthScopes> Scopes { get; }
        void DynamicScopeValidation(AuthScopes requiredScope, string accessToken = null);
        Task<CredentialCheckResponseModel> CheckCredentialsAsync();
    }
}