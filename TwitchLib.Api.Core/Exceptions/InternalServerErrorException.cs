using System;
using System.Net.Http;

namespace TwitchLib.Api.Core.Exceptions
{
    /// <inheritdoc />
    /// <summary>Exception representing a 500 Http Statuscode</summary>
    public class InternalServerErrorException : HttpResponseException
    {
        /// <inheritdoc />
        /// <summary>Exception constructor</summary>
        public InternalServerErrorException(string data, HttpResponseMessage httpResponse)
            : base(data, httpResponse)
        {
        }
    }
}