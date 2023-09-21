using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Api.Core.Interfaces;

namespace TwitchLib.Api.Core.Interfaces
{
    public interface IUserAccessTokenManager
    {
        /// <summary>
        /// Uses the Authoization Grant flow to get an access code to get a token and refresh token.
        /// https://dev.twitch.tv/docs/authentication/getting-tokens-oauth/#authorization-code-grant-flow
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        Task<string> GetUserAccessToken();
    }
}
