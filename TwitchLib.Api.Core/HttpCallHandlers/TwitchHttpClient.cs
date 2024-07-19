using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TwitchLib.Api.Core.Common;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Core.Internal;

namespace TwitchLib.Api.Core.HttpCallHandlers
{
    /// <summary>
    /// Main HttpClient used to call the Twitch API
    /// </summary>
    public class TwitchHttpClient : IHttpCallHandler
    {
        private readonly ILogger<TwitchHttpClient> _logger;
        private readonly HttpClient _http;

        /// <summary>
        /// Creates an Instance of the TwitchHttpClient Class.
        /// </summary>
        /// <param name="logger">Instance Of Logger, otherwise no logging is used,  </param>
        public TwitchHttpClient(ILogger<TwitchHttpClient> logger = null)
        {
            _logger = logger;
            _http = new HttpClient(new TwitchHttpClientHandler(_logger));
        }

        /// <summary>
        /// PUT Request with a byte array body
        /// </summary>
        /// <param name="url">URL to direct the PUT request at</param>
        /// <param name="payload">Payload to send with the request</param>
        /// <returns>Task for the request</returns>
        public async Task PutBytesAsync(string url, byte[] payload)
        {
            var response = await _http.PutAsync(new Uri(url), new ByteArrayContent(payload)).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
                HandleWebException(response);
        }

        /// <summary>
        /// Used to make API calls to the Twitch API varying by Method, URL and payload
        /// </summary>
        /// <param name="url">URL to call</param>
        /// <param name="method">HTTP Method to use for the API call</param>
        /// <param name="payload">Payload to send with the API call</param>
        /// <param name="api">Which API version is called</param>
        /// <param name="clientId">Twitch ClientId</param>
        /// <param name="accessToken">Twitch AccessToken linked to the ClientId</param>
        /// <returns>KeyValuePair with the key being the returned StatusCode and the Value being the ResponseBody as string</returns>
        /// <exception cref="InvalidCredentialException"></exception>
        public async Task<KeyValuePair<int, string>> GeneralRequestAsync(string url, string method,
            string payload = null, ApiVersion api = ApiVersion.Helix, string clientId = null, string accessToken = null)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(url),
                Method = new HttpMethod(method)
            };

            if (api == ApiVersion.Helix)
            {
                if (string.IsNullOrWhiteSpace(clientId) || string.IsNullOrWhiteSpace(accessToken))
                    throw new InvalidCredentialException("A Client-Id and OAuth token is required to use the Twitch API.");

                request.Headers.Add("Client-ID", clientId);
            }

            var authPrefix = "OAuth";
            if (api == ApiVersion.Helix || api == ApiVersion.Auth)
            {
                request.Headers.Add(HttpRequestHeader.Accept.ToString(), "application/json");
                authPrefix = "Bearer";
            }

            if (!string.IsNullOrWhiteSpace(accessToken))
                request.Headers.Add(HttpRequestHeader.Authorization.ToString(), $"{authPrefix} {Helpers.FormatOAuth(accessToken)}");

            if (payload != null)
                request.Content = new StringContent(payload, Encoding.UTF8, "application/json");

            var response = await _http.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var respStr = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return new KeyValuePair<int, string>((int)response.StatusCode, respStr);
            }

            HandleWebException(response);
            return new KeyValuePair<int, string>(0, null);
        }

        public async Task<int> RequestReturnResponseCodeAsync(string url, string method, List<KeyValuePair<string, string>> getParams = null)
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

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(url),
                Method = new HttpMethod(method)
            };
            var response = await _http.SendAsync(request).ConfigureAwait(false);
            return (int)response.StatusCode;
        }

        private void HandleWebException(HttpResponseMessage errorResp)
        {
            // Show the actual message that came back from the server. Masking it only makes debugging hard.
            var actualContent = String.Empty;

            try
            {
                actualContent = errorResp.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                actualContent = $"Couldn't read response from server: {ex.Message}";
            }

            switch (errorResp.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    throw new BadRequestException(actualContent + "\n\nYour request may have failed because either: \n 1. Your ClientID was invalid/not set. \n 2. Your refresh token was invalid. \n 3. You requested a username when the server was expecting a user ID.", errorResp);
                case HttpStatusCode.Unauthorized:
                    var authenticateHeader = errorResp.Headers.WwwAuthenticate;
                    if (authenticateHeader == null || authenticateHeader.Count <= 0)
                        throw new BadScopeException(actualContent + "\n\nYour request was blocked due to bad credentials (Do you have the right scope for your access token?).", errorResp);
                    throw new TokenExpiredException(actualContent + "\n\nYour request was blocked due to an expired Token. Please refresh your token and update your API instance settings.", errorResp);
                case HttpStatusCode.NotFound:
                    throw new BadResourceException(actualContent + "\n\nThe resource you tried to access was not valid.", errorResp);
                case (HttpStatusCode)429:
					errorResp.Headers.TryGetValues("Ratelimit-Reset", out var resetTime);
					throw new TooManyRequestsException(actualContent + "\n\nYou have reached your rate limit. Too many requests were made", resetTime.FirstOrDefault(), errorResp);
                case HttpStatusCode.BadGateway:
                    throw new BadGatewayException(actualContent + "\n\nThe API answered with a 502 Bad Gateway. Please retry your request", errorResp);
                case HttpStatusCode.GatewayTimeout:
                    throw new GatewayTimeoutException(actualContent + "\n\nThe API answered with a 504 Gateway Timeout. Please retry your request", errorResp);
                case HttpStatusCode.InternalServerError:
                    throw new InternalServerErrorException(actualContent + "\n\nThe API answered with a 500 Internal Server Error. Please retry your request", errorResp);
                case HttpStatusCode.Forbidden:
                    throw new BadTokenException(actualContent + "\n\nThe token provided in the request did not match the associated user. Make sure the token you're using is from the resource owner (streamer? viewer?)", errorResp);
                default:
                    throw new HttpRequestException(actualContent + "\n\nSomething went wrong during the request! Please try again later");
            }
        }

    }
}