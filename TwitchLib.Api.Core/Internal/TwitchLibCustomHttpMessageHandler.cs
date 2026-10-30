using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TwitchLib.Api.Core.Interfaces;

namespace TwitchLib.Api.Core.Internal;

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
    public TwitchHttpClientHandler(ILogger<IHttpCallHandler> logger) : base(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate })
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
        var contentStr = request.Content is null
            ? "<null>"
            : await request.Content.ReadAsStringAsync().ConfigureAwait(false);
        _logger.LogInformation("Timestamp: {timestamp} Type: {type} Method: {method} Resource: {url} Content: {content}",
                DateTime.Now, "Request", request.Method, request.RequestUri, contentStr);

#if NET
        var startTime = Stopwatch.GetTimestamp();
        var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        var elapsed = Stopwatch.GetElapsedTime(startTime);
#else
        var stopwatch = Stopwatch.StartNew();
        var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        stopwatch.Stop();
        var elapsed = stopwatch.Elapsed;
#endif

        var logLevel = response.IsSuccessStatusCode ? LogLevel.Information : LogLevel.Error;
        contentStr = response.Content is null
            ? "<null>"
            : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        _logger.Log(logLevel, "Timestamp: {timestamp} Type: {type} Resource: {url} Statuscode: {statuscode} Elapsed: {elapsed} ms Content: {content}",
            DateTime.Now, "Response", response.RequestMessage.RequestUri, (int)response.StatusCode, elapsed.TotalMilliseconds, contentStr);

        return response;
    }
}