using Newtonsoft.Json;

namespace TwitchLib.Api.Models.V5.Teams
{
    public class AllTeams
    {
        #region Teams
        [JsonProperty(PropertyName = "teams")]
        public Team[] Teams { get; protected set; }
        #endregion
    }
}
