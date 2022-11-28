using System;
using System.Net.Http;

namespace TwitchLib.Api.Core.Exceptions
{
    /// <inheritdoc />
    /// <summary>Exception representing a response received from Twitch that is not expected by the library</summary>
    public class UnexpectedResponseException : HttpResponseException
    {
        /// <inheritdoc />
        /// <summary>Exception constructor</summary>
        public UnexpectedResponseException(string data, HttpResponseMessage httpResponse)
            : base(data, httpResponse)
        {
        }
    }
}
