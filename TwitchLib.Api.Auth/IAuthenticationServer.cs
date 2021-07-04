using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Api.Auth.Models;

namespace TwitchLib.Api.Auth
{
    public interface IAuthenticationServer : IDisposable
    {
        Uri BaseUri { get; }

        event Func<object, AuthorizationCodeResponse, Task> AuthorizationCodeReceived;

        Uri Start();
        void Stop();
    }
}
