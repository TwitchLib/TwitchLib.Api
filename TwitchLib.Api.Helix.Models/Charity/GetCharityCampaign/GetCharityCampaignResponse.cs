using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Charity.GetCharityCampaign
{
   /// <summary>
   /// <para>The response for the GetCharityCampaign that returns information about the charity campaign that a broadcaster is running.</para>
   /// </summary>
   public class GetCharityCampaignResponse
   {
      /// <summary>
      /// <para>A list that contains the charity campaign that the broadcaster is currently running.</para>
      /// <para>The array is empty if the broadcaster is not running a charity campaign.</para>
      /// <para>The campaign information is no longer available as soon as the campaign ends.</para>
      /// </summary>
      [JsonProperty(PropertyName = "data")]
      public CharityCampaignResponseModel[] Data { get; protected set; }
   }
}
