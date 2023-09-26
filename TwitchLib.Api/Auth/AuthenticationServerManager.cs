
using System;
using System.Threading;
using System.Threading.Tasks;
using EmbedIO;
using EmbedIO.WebApi;
using Microsoft.Extensions.Logging;
using Swan.Logging;

namespace TwitchLib.Api.Auth
{
    /// <summary>
    /// Controls a private WebAPI service that can receive authentication results from oAuth.
    /// </summary>
    internal class AuthenticationServerManager
    {
        internal AuthenticationServerManager(Microsoft.Extensions.Logging.ILogger logger)
        {
            Logger.UnregisterLogger<ConsoleLogger>();

            if (logger != null)
                Logger.RegisterLogger(new LoggerBridge(logger));
        }

        private event EventHandler<AccessCodeResponse> _accessCodeReceived;

        internal AccessCodeResponse WaitForAuthorizationCodeCallback(CancellationTokenSource cancellationToken, string hostname, int listenerPort)
        {
            AccessCodeResponse returnValue = null;
            AutoResetEvent waitHandle = new AutoResetEvent(false);

            _accessCodeReceived += (source, accessCodeResponse) =>
            {
                returnValue = accessCodeResponse;
                waitHandle.Set();
            };

            StartLocalService(cancellationToken.Token, hostname, listenerPort);

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

        private void StartLocalService(CancellationToken cancellationToken, string hostname, int port = 5000)
        {
            WebServer ws = new WebServer(o => o
                .WithUrlPrefix($"http://{hostname}:{port}")
                .WithMode(HttpListenerMode.EmbedIO))
                .WithWebApi("/api", m => m
                .WithController<ApiController>(() => { return new ApiController(_accessCodeReceived); }));

            ws.RunAsync(cancellationToken);
        }
    }
}
