using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Charity.GetCharityCampaign
{
    public class GetCharityCampaignResponse
    {
        /// <summary>
        /// A list that contains the charity campaign that the broadcaster is currently running. 
        /// The array is empty if the broadcaster is not running a charity campaign; 
        /// The campaign information is no longer available as soon as the campaign ends.
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public CharityCampaignResponseModel[] Data { get; protected set; }
    }
}



