using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Api.Core.Interfaces;

namespace TwitchLib.Api.Core.Interfaces
{
    /// <summary>
    /// Enables API calls to use user access tokens instead of client credentials. Most of the best parts of
    /// the Twitch API are only available when using user access tokens. 
    /// </summary>
    public interface IUserAccessTokenManager
    {
        /// <summary>
        /// Uses the Authoization Grant flow to get an access code to get a token and refresh token.
        /// https://dev.twitch.tv/docs/authentication/getting-tokens-oauth/#authorization-code-grant-flow
        /// </summary>
        /// <returns></returns>
        Task<string> GetUserAccessToken();
    }
}
