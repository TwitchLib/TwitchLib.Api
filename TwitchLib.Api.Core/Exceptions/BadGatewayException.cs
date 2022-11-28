using System;
using System.Net.Http;

namespace TwitchLib.Api.Core.Exceptions
{
    /// <inheritdoc />
    /// <summary>Exception representing a 502 Http Statuscode</summary>
    public class BadGatewayException : HttpResponseException
    {
        /// <inheritdoc />
        /// <summary>Exception constructor</summary>
        public BadGatewayException(string data, HttpResponseMessage httpResponse)
            : base(data, httpResponse)
        {
        }
    }
}