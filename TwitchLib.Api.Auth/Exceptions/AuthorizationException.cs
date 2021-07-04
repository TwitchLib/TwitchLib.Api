using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Auth.Exceptions
{
    public class AuthorizationException : Exception
    {
        public string Error { internal set; get; }
        public string State { internal set; get; }

        public AuthorizationException(string error, string state)
        {
            Error = error;
            State = state;
        }
    }
}
