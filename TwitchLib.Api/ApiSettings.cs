﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchLib.Api.Enums;
using TwitchLib.Api.Exceptions;
using TwitchLib.Api.Interfaces;
using TwitchLib.Api.Models.v5.Root;

namespace TwitchLib.Api
{
    public class ApiSettings : IApiSettings
    {
        public string ClientId { get; private set; }

        public string AccessToken { get; private set; }

        private readonly TwitchAPI _api;

        public ApiSettings(TwitchAPI api)
        {
            Validators = new CredentialValidators();
            _api = api;
        }

        public void ValidateScope(AuthScopes requiredScope, string accessToken = null)
        {
            if (accessToken != null)
                return;
            if (Scopes.Contains(requiredScope))
                throw new InvalidCredentialException($"The call you attempted was blocked because you are missing required scope: {requiredScope.ToString().ToLower()}. You can ignore this protection by using TwitchLib.TwitchAPI.Settings.Validators.SkipDynamicScopeValidation = false . You can also generate a new token with the required scope here: https://twitchtokengenerator.com");
        }

        public CredentialValidators Validators { get; private set; }
        
        #region DynamicScopeValidation
        public void DynamicScopeValidation(AuthScopes requiredScope, string accessToken = null)
        {
            if(Validators.SkipAccessTokenValidation) return;
            if (Validators.SkipDynamicScopeValidation || !string.IsNullOrWhiteSpace(accessToken)) return;

            if (!Scopes.Contains(requiredScope) || requiredScope == AuthScopes.Any && Scopes.Any(x => x == AuthScopes.None))
                throw new InvalidCredentialException($"The current access token ({String.Join(",", Scopes)}) does not support this call. Missing required scope: {requiredScope.ToString().ToLower()}. You can skip this check by using: TwitchLib.TwitchAPI.Settings.Validators.SkipDynamicScopeValidation = true . You can also generate a new token with this scope here: https://twitchtokengenerator.com");
        }
        #endregion

        #region SetClientId
        public async Task SetClientIdAsync(string clientId)
        {
            if (!Validators.SkipClientIdValidation)
            {
                if ((!string.IsNullOrWhiteSpace(clientId) || !string.IsNullOrWhiteSpace(AccessToken)) && !await ValidClientId(clientId))
                    throw new InvalidCredentialException("The passed Client Id was not valid. To get a valid Client Id, register an application here: https://www.twitch.tv/kraken/oauth2/clients/new");
            }
            ClientId = clientId;
        }
        #endregion

        #region SetAccessToken
        public async Task SetAccessTokenAsync(string accessToken)
        {
            if (!Validators.SkipAccessTokenValidation)
            {
                if (string.IsNullOrEmpty(accessToken))
                    throw new InvalidCredentialException("Access Token cannot be empty or null.");
                if (!await ValidAccessToken(accessToken))
                    throw new InvalidCredentialException("The passed Access Token was not valid. To get an access token, go here:  https://twitchtokengenerator.com/");
            }
            AccessToken = accessToken;
        }
        #endregion

        #region ValidClientId
        private async Task<bool> ValidClientId(string clientId)
        {
            try
            {
                var result = await _api.Root.v5.GetRootAsync(null, clientId);
                return result.Token != null;
            }
            catch (BadRequestException)
            {
                return false;
            }
        }
        #endregion
        #region ValidAccessToken
        private async Task<bool> ValidAccessToken(string accessToken)
        {
            try
            {
                var resp = await _api.Root.v5.GetRootAsync(accessToken);
                if (resp.Token == null) return false;

                Scopes = BuildScopesList(resp.Token);
                return true;

            }
            catch
            {
                return false;
            }
        }
        #endregion
        
        #region buildScopesList
        private static List<AuthScopes> BuildScopesList(RootToken token)
        {
            var scopes = new List<AuthScopes>();
            foreach (var scope in token.Auth.Scopes)
            {
                switch (scope)
                {
                    case "channel_check_subscription":
                        scopes.Add(AuthScopes.Channel_Check_Subscription);
                        break;
                    case "channel_commercial":
                        scopes.Add(AuthScopes.Channel_Commercial);
                        break;
                    case "channel_editor":
                        scopes.Add(AuthScopes.Channel_Editor);
                        break;
                    case "channel_feed_edit":
                        scopes.Add(AuthScopes.Channel_Feed_Edit);
                        break;
                    case "channel_feed_read":
                        scopes.Add(AuthScopes.Channel_Feed_Read);
                        break;
                    case "channel_read":
                        scopes.Add(AuthScopes.Channel_Read);
                        break;
                    case "channel_stream":
                        scopes.Add(AuthScopes.Channel_Stream);
                        break;
                    case "channel_subscriptions":
                        scopes.Add(AuthScopes.Channel_Subscriptions);
                        break;
                    case "chat_login":
                        scopes.Add(AuthScopes.Chat_Login);
                        break;
                    case "collections_edit":
                        scopes.Add(AuthScopes.Collections_Edit);
                        break;
                    case "communities_edit":
                        scopes.Add(AuthScopes.Communities_Edit);
                        break;
                    case "communities_moderate":
                        scopes.Add(AuthScopes.Communities_Moderate);
                        break;
                    case "user_blocks_edit":
                        scopes.Add(AuthScopes.User_Blocks_Edit);
                        break;
                    case "user_blocks_read":
                        scopes.Add(AuthScopes.User_Blocks_Read);
                        break;
                    case "user_follows_edit":
                        scopes.Add(AuthScopes.User_Follows_Edit);
                        break;
                    case "user_read":
                        scopes.Add(AuthScopes.User_Read);
                        break;
                    case "user_subscriptions":
                        scopes.Add(AuthScopes.User_Subscriptions);
                        break;
                    case "openid":
                        scopes.Add(AuthScopes.OpenId);
                        break;
                    case "viewing_activity_read":
                        scopes.Add(AuthScopes.Viewing_Activity_Read);
                        break;
                    case "user:edit":
                        scopes.Add(AuthScopes.Helix_User_Edit);
                        break;
                    case "user:read:email":
                        scopes.Add(AuthScopes.Helix_User_Read_Email);
                        break;
                    case "clips:edit":
                        scopes.Add(AuthScopes.Helix_Clips_Edit);
                        break;
                    case "bits:read":
                        scopes.Add(AuthScopes.Helix_Bits_Read);
                        break;
                    case "analytics:read:games":
                        scopes.Add(AuthScopes.Helix_Analytics_Read_Games);
                        break;
                }
            }

            if (scopes.Count == 0)
                scopes.Add(AuthScopes.None);
            return scopes;
        }

        public class CredentialValidators
        {
            #region ClientIdValidation
            public bool SkipClientIdValidation { get; set; } = false;
            #endregion
            #region AccessTokenValidation
            public bool SkipAccessTokenValidation { get; set; } = false;
            #endregion
            #region DynamicScopeValidation
            public bool SkipDynamicScopeValidation { get; set; } = false;
            #endregion
        }

        #region Scopes
        public List<AuthScopes> Scopes { get; private set; }
        #endregion
        #endregion
    }
}
