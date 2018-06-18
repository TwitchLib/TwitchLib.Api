using System;

namespace TwitchLib.Api.Exceptions
{
    /// <inheritdoc />
    /// <summary>Exception representing a 429 Http Statuscode</summary>
    public class TooManyRequestsException : Exception
    {
        /// <inheritdoc />
        /// <summary>Exception constructor</summary>
        public TooManyRequestsException(string data)
            : base(data)
        {
        }
    }
}
