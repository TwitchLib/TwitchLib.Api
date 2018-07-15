using Newtonsoft.Json;

namespace TwitchLib.Api.Core.Models.Undocumented.TwitchPrimeOffers
{
    public class TwitchPrimeOffers
    {
        [JsonProperty(PropertyName = "offers")]
        public Offer[] Offers { get; protected set; }
    }
}
