﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Enums;
using TwitchLib.Api.Exceptions;

namespace TwitchLib.Api.Sections
{
    public class Auth
    {
        public V5 v5 { get; }

        public Auth(TwitchAPI api)
        {
            v5 = new V5(api);
        }

        public class V5 : ApiSection
        {
            public V5(TwitchAPI api) : base(api)
            {
            }

            #region GetFreshToken
            /// <summary>
            /// <para>[ASYNC] Refreshes an expired auth token</para>
            /// <para>ATTENTION: Client Secret required. Never expose it to consumers!</para>
            /// <para>Throws a BadRequest Exception if the request fails due to a bad refresh token</para>
            /// </summary>
            /// <returns>A RefreshResponse object that holds your new auth and refresh token and the list of scopes for that token</returns>
            public async Task<Models.v5.Auth.RefreshResponse> RefreshAuthTokenAsync(string refreshToken, string clientSecret, string clientId = null)
            {
                var internalClientId = clientId ?? Api.Settings.ClientId;

                if (string.IsNullOrWhiteSpace(refreshToken)) { throw new BadParameterException("The refresh token is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(clientSecret)) { throw new BadParameterException("The client secret is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(internalClientId)) { throw new BadParameterException("The clientId is not valid. It is not allowed to be null, empty or filled with whitespaces."); }

                var getParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string> ("grant_type", "refresh_token"),
                    new KeyValuePair<string, string> ("refresh_token", refreshToken),
                    new KeyValuePair<string, string> ("client_id", internalClientId),
                    new KeyValuePair<string, string> ("client_secret", clientSecret)
                };

                return await Api.TwitchGetGenericAsync<Models.v5.Auth.RefreshResponse>("/oauth2/token", ApiVersion.v5, getParams, customBase: "https://id.twitch.tv");
            }
            #endregion
        }
    }
}