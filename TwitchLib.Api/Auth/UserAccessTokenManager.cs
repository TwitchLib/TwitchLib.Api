using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Extensions.ReleasedExtensions;
using TwitchLib.Api.Interfaces;
using static Swan.Terminal;

namespace TwitchLib.Api.Auth
{
    internal class UserAccessTokenManager : IUserAccessTokenManager
    {
        IApiSettings _settings;
        private Auth _auth;
        private ILogger _logger;

        private DateTime _tokenExpiration = DateTime.MinValue;
        private DateTime _tokenValidation = DateTime.MinValue;
        private string _refreshToken = null;
        private string _accessToken = null;
        private string[] _scopes;

        public UserAccessTokenManager(IApiSettings settings, Auth auth, ILogger logger) 
        {
            _settings = settings;   
            _auth = auth;
            _logger = logger;

            if (Directory.Exists(Path.GetDirectoryName(_settings.OAuthTokenFile)) == false)
                Directory.CreateDirectory(Path.GetDirectoryName(_settings.OAuthTokenFile));
        }

        public async Task<string> GetUserAccessToken()
        {
            // First, see if the stored refresh token is still valid.
            if (_refreshToken == null)
            {
                if (File.Exists(_settings.OAuthTokenFile) == true)
                {
                    RefreshResponse refreshData = JsonConvert.DeserializeObject<RefreshResponse>(File.ReadAllText(_settings.OAuthTokenFile));
                    _tokenExpiration = File.GetCreationTime(_settings.OAuthTokenFile).AddSeconds(refreshData.ExpiresIn);

                    _refreshToken = refreshData.RefreshToken;
                    _scopes = refreshData.Scopes;
                    _accessToken = refreshData.AccessToken;

                    _logger.LogTrace($"UserAccessTokenManager::GetUserAccessToken(): Loaded oAuth token from file: {_settings.OAuthTokenFile}");
                    _logger.LogTrace($"UserAccessTokenManager::GetUserAccessToken(): Token expiration: {_tokenExpiration.ToString()}");
                    _logger.LogTrace($"UserAccessTokenManager::GetUserAccessToken(): Token scopes: {String.Join(",", _scopes)}");
                }
            }

            // If we couldn't read the refresh token, then do a full reauthorize.
            if (_refreshToken == null || DoScopesMatchSettings(_settings.Scopes, _scopes) == false)
            {
                await Reauthorize();
            }

            // If the token hasn't expired yet, then we can return it.
            if (_tokenExpiration > DateTime.Now.AddMinutes(5))
            {
                // Ensure that the token is still valid once every 55 minutes.
                if(_tokenValidation.AddMinutes(55) > DateTime.Now || IsAccessTokenStillValid(_accessToken) == true)
                    return _accessToken;
            }

            // If the token has expired, then do a refresh
            await Refresh();

            return _accessToken;
        }

        private bool IsAccessTokenStillValid(string accessToken)
        {
            _tokenValidation = DateTime.Now;

            var result = _auth.ValidateAccessTokenAsync(accessToken);

            return result.Result != null;
        }

        private bool DoScopesMatchSettings(List<AuthScopes> desiredScopes, string[] currentScopes)
        {
            if(currentScopes.Length != desiredScopes.Count) 
                return false;

            var matches = desiredScopes.Join(currentScopes, p => TwitchLib.Api.Core.Common.Helpers.AuthScopesToString(p), r => r, (p, r) => p);

            if(matches.Count() != currentScopes.Length)  
                return false;

            return true;
        }

        private async Task Reauthorize()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            var accessCodeResponse = _auth.GetAccessCodeFromClientIdAndSecret(cancellationTokenSource, _settings.ClientId, _settings.Secret);
            Console.WriteLine($"code: {accessCodeResponse.AccessCode}");

            var accessTokenReponse = await _auth.GetAccessTokenFromCodeAsync(accessCodeResponse.AccessCode, _settings.Secret, $"http://{_settings.OAuthResponseHostname}:{_settings.OAuthResponsePort}/api/callback", _settings.ClientId);
            Console.WriteLine($"accessToken: {accessTokenReponse.AccessToken}, Expiration: {DateTime.Now.AddSeconds(accessTokenReponse.ExpiresIn)}");

            _accessToken = accessTokenReponse.AccessToken;
            _scopes = accessTokenReponse.Scopes;
            _refreshToken = accessTokenReponse.RefreshToken;
            _tokenExpiration = DateTime.Now.AddSeconds(accessTokenReponse.ExpiresIn);

            File.WriteAllText(_settings.OAuthTokenFile, JsonConvert.SerializeObject(accessTokenReponse));
        }

        private async Task Refresh()
        {
            var refreshResponse = await _auth.RefreshAuthTokenAsync(_refreshToken, _settings.Secret, _settings.ClientId);

            _accessToken = refreshResponse.AccessToken;
            _scopes = refreshResponse.Scopes;
            _refreshToken = refreshResponse.RefreshToken;
            _tokenExpiration = DateTime.Now.AddSeconds(refreshResponse.ExpiresIn);

            File.WriteAllText(_settings.OAuthTokenFile, JsonConvert.SerializeObject(refreshResponse));
        }
    }
}
