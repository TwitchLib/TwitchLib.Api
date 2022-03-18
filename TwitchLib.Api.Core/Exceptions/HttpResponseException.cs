using System;
using System.Net.Http;

namespace TwitchLib.Api.Core.Exceptions
{
    public class HttpResponseException : Exception
    {
        public HttpResponseMessage HttpResponse { get; }

        public HttpResponseException(string apiData, HttpResponseMessage httpResponse) : base(apiData)
        {
            HttpResponse = httpResponse;
        }
    }
}
