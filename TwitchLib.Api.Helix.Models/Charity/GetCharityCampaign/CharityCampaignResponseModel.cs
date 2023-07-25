using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Charity.GetCharityCampaign
{
   /// <summary>
   /// <para>The charity campaign a broadcaster is raising money for through their channel.</para>
   /// </summary>
   public class CharityCampaignResponseModel
   {
      /// <summary>
      /// <para>An ID that uniquely identifies the charity campaign.</para>
      /// </summary>
      [JsonProperty(PropertyName = "id")]
      public string Id { get; protected set; }

      /// <summary>
      /// <para>An ID that uniquely identifies the broadcaster that’s running the campaign.</para>
      /// </summary>
      [JsonProperty(PropertyName = "broadcaster_id")]
      public string BroadcasterId { get; protected set; }

      /// <summary>
      /// <para>The broadcaster’s login name. (Name is lowercase)</para>
      /// </summary>
      [JsonProperty(PropertyName = "broadcaster_login")]
      public string BroadcasterLogin { get; protected set; }

      /// <summary>
      /// <para>The broadcaster’s display name. (Name has capitalization)</para>
      /// </summary>
      [JsonProperty(PropertyName = "broadcaster_name")]
      public string BroadcasterName { get; protected set; }

      /// <summary>
      /// <para>The charity’s name.</para>
      /// </summary>
      [JsonProperty(PropertyName = "charity_name")]
      public string CharityName { get; protected set; }

      /// <summary>
      /// <para>A description of the charity.</para>
      /// </summary>
      [JsonProperty(PropertyName = "charity_description")]
      public string CharityDescription { get; protected set; }

      /// <summary>
      /// <para>A URL to an image of the charity’s logo.<br/> 
      /// The image’s type is PNG and its size is 100px X 100px.</para>
      /// </summary>
      [JsonProperty(PropertyName = "charity_logo")]
      public string CharityLogo { get; protected set; }

      /// <summary>
      /// <para>A URL to the charity’s website.</para>
      /// </summary>
      [JsonProperty(PropertyName = "charity_website")]
      public string CharityWebsite { get; protected set; }

      /// <summary>
      /// <para>An object that contains the current amount of donations that the campaign has received.</para>
      /// </summary>
      [JsonProperty(PropertyName = "current_amount")]
      public Amount CurrentAmount { get; protected set; }

      /// <summary>
      /// <para>An object that contains the amount of money that the campaign is trying to raise.</para>
      /// <para>This field may be null if the broadcaster has not defined a target fundraising goal.</para>
      /// </summary>
      [JsonProperty(PropertyName = "target_amount")]
      public Amount TargetAmount { get; protected set; }
   }
}
