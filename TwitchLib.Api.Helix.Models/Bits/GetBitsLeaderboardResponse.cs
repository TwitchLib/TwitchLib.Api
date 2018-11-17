using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Bits
{
    public class GetBitsLeaderboardResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Listing[] Listings { get; protected set; }
        [JsonProperty(PropertyName = "date_range")]
        public DateRange DateRange { get; protected set; }
        [JsonProperty(PropertyName = "total")]
        public int Total { get; protected set; }
    }
}
