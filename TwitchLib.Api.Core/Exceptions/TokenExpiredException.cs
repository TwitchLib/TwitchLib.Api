using System;
using System.Net.Http;

namespace TwitchLib.Api.Core.Exceptions
{
    /// <inheritdoc />
    /// <summary>Exception representing a detection that the OAuth token expired</summary>
    public class TokenExpiredException : HttpResponseException
    {
        /// <inheritdoc />
        /// <summary>Exception constructor</summary>
        public TokenExpiredException(string data, HttpResponseMessage httpResponse)
            : base(data, httpResponse)
        {
        }
    }
}