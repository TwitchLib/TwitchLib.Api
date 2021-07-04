using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Auth.Models
{
    public class AuthorizationCodeResponse
    {
        public string Code { internal set; get; }
        public string State { internal set; get; }
        public string Scope { internal set; get; }

        public AuthorizationCodeResponse(string code, string state, string scope)
        {
            Code = code;
            State = state;
            Scope = scope;
        }
    }
}
