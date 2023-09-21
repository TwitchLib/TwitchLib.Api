using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
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
        private DateTime _tokenExpiration = DateTime.MinValue;
        private string _refreshToken = null;
        private string _accessToken = null;
        private string[] _scopes;

        public UserAccessTokenManager(IApiSettings settings, Auth auth) 
        {
            _settings = settings;   
            _auth = auth;

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
                }
            }

            // If we couldn't read the refresh token, then do a full reauthorize.
            if (_refreshToken == null)
            {
                await Reauthorize();
            }

            // If the token hasn't expired yet, then we can return it.
            if (_tokenExpiration > DateTime.Now.AddMinutes(5))
                return _accessToken;

            // If the token has expired, then do a refresh
            await Refresh();

            return _accessToken;
        }

        private async Task Reauthorize()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            var accessCodeResponse = _auth.GetAccessCodeFromClientIdAndSecret(cancellationTokenSource, _settings.ClientId, _settings.Secret);
            Console.WriteLine($"code: {accessCodeResponse.AccessCode}");

            var accessTokenReponse = await _auth.GetAccessTokenFromCodeAsync(accessCodeResponse.AccessCode, _settings.Secret, $"http://localhost:{_settings.OAuthResponsePort}/api/callback", _settings.ClientId);
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
