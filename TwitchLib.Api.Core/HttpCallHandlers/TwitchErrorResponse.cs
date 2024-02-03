using Newtonsoft.Json;

namespace TwitchLib.Api.Core.HttpCallHandlers
{
    public class TwitchErrorResponse
    {
        [JsonProperty("error")]
        public string Error;

        [JsonProperty("status")]
        public int Status;

        [JsonProperty("message")]
        public string Message;
    }
}