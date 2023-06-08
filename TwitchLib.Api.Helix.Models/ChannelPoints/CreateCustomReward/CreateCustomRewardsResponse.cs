using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.ChannelPoints.CreateCustomReward
{
   /// <summary>
   /// The response for creating a Custom Reward in the broadcaster’s channel.
   /// </summary>
   public class CreateCustomRewardsResponse
   {
      /// <summary>
      /// A list that contains the single custom reward you created.
      /// </summary>
      [JsonProperty(PropertyName = "data")]
      public CustomReward[] Data { get; protected set; }
   }
}
