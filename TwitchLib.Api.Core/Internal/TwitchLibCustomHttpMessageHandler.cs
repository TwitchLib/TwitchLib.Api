using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TwitchLib.Api.Core.Interfaces;

namespace TwitchLib.Api.Core.Internal
{
    /// <summary>
    /// Custom HttpClientHandler that can be used to log requests made and their duration
    /// </summary>
    public class TwitchHttpClientHandler : DelegatingHandler
    {
        private readonly ILogger<IHttpCallHandler> _logger;

        /// <summary>
        /// Creates a new TwitchHttpClientHandler
        /// </summary>
        /// <param name="logger">Logger to use for logging</param>
        public TwitchHttpClientHandler(ILogger<IHttpCallHandler> logger) : base(new HttpClientHandler())
        {
            _logger = logger;
        }

        /// <summary>
        /// Overrides the HttpClient SendAsync method to hook into the request pipeline and log the http call
        /// </summary>
        /// <param name="request">The HttpRequest that is to be sent</param>
        /// <param name="cancellationToken">CancellationToken to cancel the HTTP Request</param>
        /// <returns></returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Content != null)
                _logger?.LogInformation("Timestamp: {timestamp} Type: {type} Method: {method} Resource: {url} Content: {content}",
                    DateTime.Now, "Request", request.Method.ToString(), request.RequestUri.ToString(), await request.Content.ReadAsStringAsync());
            else
                _logger?.LogInformation("Timestamp: {timestamp} Type: {type} Method: {method} Resource: {url}",
                    DateTime.Now, "Request", request.Method.ToString(), request.RequestUri.ToString());

            var stopwatch = Stopwatch.StartNew();
            var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            stopwatch.Stop();

            if (response.IsSuccessStatusCode)
            {
                if (response.Content != null)
                    _logger?.LogInformation("Timestamp: {timestamp} Type: {type} Resource: {url} Statuscode: {statuscode} Elapsed: {elapsed} ms Content: {content}",
                        DateTime.Now, "Response", response.RequestMessage.RequestUri, (int)response.StatusCode, stopwatch.ElapsedMilliseconds, await response.Content.ReadAsStringAsync());
                else
                    _logger?.LogInformation("Timestamp: {timestamp} Type: {type} Resource: {url} Statuscode: {statuscode} Elapsed: {elapsed} ms",
                        DateTime.Now, "Response", response.RequestMessage.RequestUri, (int)response.StatusCode, stopwatch.ElapsedMilliseconds);
            }
            else
            {
                if (response.Content != null)
                    _logger?.LogError("Timestamp: {timestamp} Type: {type} Resource: {url} Statuscode: {statuscode} Elapsed: {elapsed} ms Content: {content}",
                        DateTime.Now, "Response", response.RequestMessage.RequestUri, (int)response.StatusCode, stopwatch.ElapsedMilliseconds, await response.Content.ReadAsStringAsync());
                else
                    _logger?.LogError("Timestamp: {timestamp} Type: {type} Resource: {url} Statuscode: {statuscode} Elapsed: {elapsed} ms",
                        DateTime.Now, "Response", response.RequestMessage.RequestUri, (int)response.StatusCode, stopwatch.ElapsedMilliseconds);
            }

            return response;
        }
    }
}