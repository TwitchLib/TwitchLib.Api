using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TwitchLib.Api.Core.Exceptions
{
    public class HttpResponseException : Exception
    {
        /// <summary>
        /// Null if using <see cref="TwitchLib.Api.Core.HttpCallHandlers.TwitchWebRequest"/> or <see cref="TwitchLib.Api.Core.Undocumented.Undocumented"/>
        /// </summary>
        public HttpResponseMessage HttpResponse { get; }

        public HttpResponseException(string apiData, HttpResponseMessage httpResponse) : base(apiData)
        {
            HttpResponse = httpResponse;
        }
    }
}