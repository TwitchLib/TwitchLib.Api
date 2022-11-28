using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TwitchLib.Api.Core.Common;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;

namespace TwitchLib.Api.Core.HttpCallHandlers
{
    [Obsolete("The WebRequest handler is deprecated and is not updated to be working with Helix correctly")]
    public class TwitchWebRequest : IHttpCallHandler
    {
        private readonly ILogger<TwitchWebRequest> _logger;

        /// <summary>
        /// Creates an Instance of the TwitchHttpClient Class.
        /// </summary>
        /// <param name="logger">Instance Of Logger, otherwise no logging is used,  </param>
        public TwitchWebRequest(ILogger<TwitchWebRequest> logger = null)
        {
            _logger = logger;
        }


        public Task PutBytesAsync(string url, byte[] payload)
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    using (var client = new WebClient())
                        client.UploadData(new Uri(url), "PUT", payload);
                }
                catch (WebException ex)
                {
                    HandleWebException(ex);
                }
            });

        }

        public async Task<KeyValuePair<int, string>> GeneralRequestAsync(string url, string method, string payload = null, ApiVersion api = ApiVersion.Helix, string clientId = null, string accessToken = null)
        {
            var request = WebRequest.CreateHttp(url);
            if (string.IsNullOrEmpty(clientId) && string.IsNullOrEmpty(accessToken))
                throw new InvalidCredentialException("A Client-Id or OAuth token is required to use the Twitch API. If you previously set them in InitializeAsync, please be sure to await the method.");

            if (!string.IsNullOrEmpty(clientId))
            {
                request.Headers["Client-ID"] = clientId;
            }
           

            request.Method = method;
            request.ContentType = "application/json";

            var authPrefix = "OAuth";
            if (api == ApiVersion.Helix)
            {
                request.Accept = "application/json";
                authPrefix = "Bearer";
            }
            else if (api != ApiVersion.Void)
            {
                request.Accept = $"application/vnd.twitchtv.v{(int)api}+json";
            }

            if (!string.IsNullOrEmpty(accessToken))
                request.Headers["Authorization"] = $"{authPrefix} {Helpers.FormatOAuth(accessToken)}";
            

            if (payload != null)
                using (var writer = new StreamWriter(await request.GetRequestStreamAsync().ConfigureAwait(false)))
                    await writer.WriteAsync(payload).ConfigureAwait(false);

            try
            {
                var response = (HttpWebResponse)request.GetResponse();

                using (var reader = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException()))
                {
                    var data = await reader.ReadToEndAsync().ConfigureAwait(false);
                    return new KeyValuePair<int, string>((int)response.StatusCode, data);
                }
            }
            catch (WebException ex) { HandleWebException(ex); }

            return new KeyValuePair<int, string>(0, null);
        }

        public Task<int> RequestReturnResponseCodeAsync(string url, string method, List<KeyValuePair<string, string>> getParams = null)
        {
            return Task.Factory.StartNew(() =>
            {
                if (getParams != null)
                {
                    for (var i = 0; i < getParams.Count; i++)
                    {
                        if (i == 0)
                            url += $"?{getParams[i].Key}={Uri.EscapeDataString(getParams[i].Value)}";
                        else
                            url += $"&{getParams[i].Key}={Uri.EscapeDataString(getParams[i].Value)}";
                    }
                }

                var req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = method;
                var response = (HttpWebResponse)req.GetResponse();
                return (int)response.StatusCode;
            });
        }

        private void HandleWebException(WebException e)
        {
            if (!(e.Response is HttpWebResponse errorResp))
                throw e;
            switch (errorResp.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    throw new BadRequestException("Your request failed because either: \n 1. Your ClientID was invalid/not set. \n 2. Your refresh token was invalid. \n 3. You requested a username when the server was expecting a user ID.", null);
                case HttpStatusCode.Unauthorized:
                    var authenticateHeader = errorResp.Headers.GetValues("WWW-Authenticate");
                    if (authenticateHeader?.Length ==0 || string.IsNullOrEmpty(authenticateHeader?[0]))
                        throw new BadScopeException("Your request was blocked due to bad credentials (do you have the right scope for your access token?).", null);

                    var invalidTokenFound = authenticateHeader[0].Contains("error='invalid_token'");
                    if (invalidTokenFound)
                        throw new TokenExpiredException("Your request was blocked du to an expired Token. Please refresh your token and update your API instance settings.", null);
                    break;
                case HttpStatusCode.NotFound:
                    throw new BadResourceException("The resource you tried to access was not valid.", null);
                case (HttpStatusCode)429:
                    var resetTime = errorResp.Headers.Get("Ratelimit-Reset");
                    throw new TooManyRequestsException("You have reached your rate limit. Too many requests were made", resetTime, null);
                default:
                    throw e;
            }
        }

    }
}
