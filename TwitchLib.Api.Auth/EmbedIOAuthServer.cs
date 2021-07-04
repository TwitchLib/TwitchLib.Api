using EmbedIO;
using EmbedIO.Actions;
using Swan.Logging;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TwitchLib.Api.Auth.Exceptions;
using TwitchLib.Api.Auth.Models;

namespace TwitchLib.Api.Auth
{
    public class EmbedIOAuthServer : IAuthenticationServer
    {
        public event Func<object, AuthorizationCodeResponse, Task> AuthorizationCodeReceived;

        public Uri BaseUri { get; }
        public int Port { get; }
        public string CallbackHtml { get; set; } = @"
        <html>
            <body>
                <h3>TwitchLib.Api.Auth Callback Completed!</h3>
                <p>Your authorization flow has been completed. The application has received the results.</p>
            </body>
        </html>";
        private CancellationTokenSource cancelTokenSource;
        private readonly WebServer server;

        public EmbedIOAuthServer()
            : this(new Uri("http://localhost:5000/callback"), 5000) { }

        public EmbedIOAuthServer(string callbackUrl, int port)
            : this(new Uri(callbackUrl), port) { }

        public EmbedIOAuthServer(Uri baseUri, int port)
        {
            // turn off direct console logging
            Logger.UnregisterLogger<ConsoleLogger>();

            BaseUri = baseUri;
            Port = port;

            server = new WebServer(port);
            server.WithModule(new ActionModule("/", HttpVerbs.Get, (context) =>
            {
                if(context.RequestedPath == baseUri.AbsolutePath)
                {
                    var queryString = context.Request.QueryString;
                    if (queryString.Count == 0 || (queryString["error"] == null && queryString["code"] == null))
                        throw new AuthorizationException("invalid query string parameters", "");

                    var error = queryString["error"];
                    var state = queryString["state"];
                    if (error != null)
                        throw new AuthorizationException(error, state);

                    var code = queryString["code"];
                    if (code != null)
                        AuthorizationCodeReceived?.Invoke(this, new AuthorizationCodeResponse(code, state, queryString["scope"]));

                    return context.SendStandardHtmlAsync(200, text =>
                    {
                        text.Write(CallbackHtml);
                    });
                } else
                {
                    return context.SendStandardHtmlAsync(404);
                }
            }));
        }

        public void Dispose()
        {
            server?.Dispose();
        }

        public Uri Start()
        {
            cancelTokenSource = new CancellationTokenSource();
            server.Start(cancelTokenSource.Token);
            return BaseUri;
        }

        public void Stop()
        {
            if (cancelTokenSource != null)
                cancelTokenSource.Cancel();
        }
    }
}
