using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Charity.GetCharityCampaignDonations
{
   /// <summary>
   /// <para>The response for GetCharityCampaignDonations that returns a list of donations that 
   /// users have made to the broadcaster's active charity campaign.</para>
   /// </summary>
   public class GetCharityCampaignDonationsResponse
   {
      /// <summary>
      /// <para>A list that contains the donations that users have made to the broadcaster’s charity campaign.</para>
      /// <para>The list is empty if the broadcaster is not currently running a charity campaign.</para>
      /// <para>The donation information is not available after the campaign ends.</para>
      /// </summary>
      [JsonProperty(PropertyName = "data")]
      public CharityCampaignDonationsResponseModel[] Data { get; protected set; }

      /// <summary>
      /// <para>Contains the information used to page through the list of results.</para>
      /// <para>The object is empty if there are no more pages left to page through.</para>
      /// </summary>
      [JsonProperty(PropertyName = "pagination")]
      public Pagination Pagination { get; protected set; }
   }
}
