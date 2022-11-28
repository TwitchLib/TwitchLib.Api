using System;
using System.Net.Http;

namespace TwitchLib.Api.Core.Exceptions
{
    /// <inheritdoc />
    /// <summary>Exception representing a token not correctly associated with the given user.</summary>
    public class BadTokenException : HttpResponseException
    {
        /// <inheritdoc />
        /// <summary>Exception constructor</summary>
        public BadTokenException(string data, HttpResponseMessage httpResponse)
            : base(data, httpResponse)
        {
        }
    }
}
