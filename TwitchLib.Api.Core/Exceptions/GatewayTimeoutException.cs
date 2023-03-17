using System;
using System.Net.Http;

namespace TwitchLib.Api.Core.Exceptions
{
    /// <inheritdoc />
    /// <summary>Exception representing a 504 Http Statuscode</summary>
    public class GatewayTimeoutException : HttpResponseException
    {
        /// <inheritdoc />
        /// <summary>Exception constructor</summary>
        public GatewayTimeoutException(string data, HttpResponseMessage httpResponse)
            : base(data, httpResponse)
        {
        }
    }
}