using System.Net.Http;

namespace TwitchLib.Api.Core.Exceptions
{
    /// <inheritdoc />
    /// <summary>Exception representing a provided scope was not permitted.</summary>
    public class BadScopeException : HttpResponseException
    {
        /// <inheritdoc />
        /// <summary>Exception constructor</summary>
        public BadScopeException(string apiData, HttpResponseMessage httpResponse)
            : base(apiData, httpResponse)
        {
        }
    }
}