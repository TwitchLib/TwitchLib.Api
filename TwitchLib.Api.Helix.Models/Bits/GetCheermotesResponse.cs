using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Bits
{
    public class GetCheermotesResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Cheermote[] Listings { get; protected set; }
    }
}
