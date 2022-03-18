using System.Net.Http;

namespace TwitchLib.Api.Core.Exceptions
{
    /// <inheritdoc />
    /// <summary>Exception representing an invalid resource</summary>
    public class BadParameterException : HttpResponseException
    {
        /// <inheritdoc />
        /// <summary>Exception constructor</summary>
        public BadParameterException(string apiData, HttpResponseMessage httpResponse)
            : base(apiData, httpResponse)
        {
        }
    }
}
