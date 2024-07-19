using EmbedIO.Routing;
using EmbedIO.WebApi;
using EmbedIO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TwitchLib.Api.Auth
{
    internal class ApiController : WebApiController
    {
        EventHandler<AccessCodeResponse> AccessCodeReceived;

        public ApiController(EventHandler<AccessCodeResponse> accessCodeReceived) : base()
        {
            AccessCodeReceived = accessCodeReceived;
        }

        [Route(HttpVerbs.Get, "/callback")]
        public void Callback([QueryField] string code, [QueryField] string scope, [QueryField] string state)
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
