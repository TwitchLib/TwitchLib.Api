using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Ads
{
   /// <summary>
   /// <para>Starts a commercial on the specified channel. </para>
   /// <para>Only partners and affiliates may run commercials and they must be streaming live at the time.</para>
   /// <para>Only the broadcaster may start a commercial; the broadcaster’s editors and moderators may not start commercials on behalf of the broadcaster.</para>
   /// </summary>
   public class StartCommercialRequest
   {
      /// <summary>
      /// The ID of the partner or affiliate broadcaster that wants to run the commercial. 
      /// This ID must match the user ID found in the OAuth token.
      /// </summary>
      [JsonProperty(PropertyName = "broadcaster_id")]
      public string BroadcasterId { get; set; }

      /// <summary>
      /// The length of the commercial to run, in seconds.
      /// Twitch tries to serve a commercial that’s the requested length, but it may be shorter or longer. 
      /// The maximum length you should request is 180 seconds.
      /// </summary>
      [JsonProperty(PropertyName = "length")]
      public int Length { get; set; }
   }
}
