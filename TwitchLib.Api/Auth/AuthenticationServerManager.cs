
using System;
using System.Threading;
using System.Threading.Tasks;
using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using Microsoft.Extensions.Logging;
using Swan.Logging;


// TODO: Clean up this whole class!

namespace TwitchLib.Api.Auth
{
    /// <summary>
    /// Controls a private WebAPI service that can receive authentication results from oAuth.
    /// </summary>
    public class AuthenticationServerManager
    {
        public event EventHandler<AccessCodeResponse> AccessCodeReceived;

        internal AccessCodeResponse WaitForAuthorizationCodeCallback(Microsoft.Extensions.Logging.ILogger logger, CancellationTokenSource cancellationToken, int listenerPort)
        {
            AccessCodeResponse returnValue = null;
            AutoResetEvent waitHandle = new AutoResetEvent(false);

            AccessCodeReceived += (source, accessCodeResponse) =>
            {
                returnValue = accessCodeResponse;
                waitHandle.Set();
            };

            StartLocalServiceAsync(logger, cancellationToken.Token, listenerPort);

            // Wait for oAuth to complete and Twitch to call us back with the Access Code.
            if (waitHandle.WaitOne(30 * 1000) == false)
            {
                cancellationToken.Cancel();
                throw new TimeoutException("More than 30 seconds elapsed without receiving an Access Code from Twitch. Check https://dev.twitch.tv/console to ensure the callback URL is in the oAuth Redirect URLs list.");
            }

            // Shutdown the web server.
            cancellationToken.Cancel();

            return returnValue;
        }

        private class LoggerBridge : Swan.Logging.ILogger
        {
            private Microsoft.Extensions.Logging.ILogger _logger;

            public LoggerBridge(Microsoft.Extensions.Logging.ILogger logger) => _logger = logger; 

            public Swan.Logging.LogLevel LogLevel => Swan.Logging.LogLevel.None;

            public void Dispose()
            {
            }

            public void Log(LogMessageReceivedEventArgs logEvent)
            {
                _logger.Log( Microsoft.Extensions.Logging.LogLevel.Trace, logEvent.ToString());
            }
        }

        public async void StartLocalServiceAsync(Microsoft.Extensions.Logging.ILogger logger, CancellationToken cancellationToken, int port = 5000)
        {
            Logger.UnregisterLogger<ConsoleLogger>();

            if (logger != null)
                Logger.RegisterLogger(new LoggerBridge(logger));
            
            WebServer ws = new WebServer(o => o
                .WithUrlPrefix($"http://localhost:{port}")
                .WithMode(HttpListenerMode.EmbedIO))
                .WithWebApi("/api", m => m
                .WithController<ApiController>(() => { return new ApiController(AccessCodeReceived); }));

            var task = ws.RunAsync(cancellationToken);
        }

        class ApiController : WebApiController
        {
            EventHandler<AccessCodeResponse> AccessCodeReceived;

            public ApiController(EventHandler<AccessCodeResponse> accessCodeReceived) : base()
            {
                AccessCodeReceived = accessCodeReceived;
            }

            [Route(HttpVerbs.Get, "/callback")]
            public async Task Callback([QueryField] string code, [QueryField] string scope, [QueryField] string state)
            {
                //await HttpContext.SendDataAsync($"code: {code}, scope: {scope}, state: {state}");
                AccessCodeReceived?.Invoke(this, new AccessCodeResponse
                {
                    AccessCode = code,
                    Scopes = scope.Split(' '),
                    State = state
                });
            }
        }
    }
}
