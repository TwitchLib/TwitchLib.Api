using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Channels.StartCommercial
{
   /// <summary>
   /// <para>Request Body for StartCommercial</para>
   /// </summary>
   public class StartCommercialRequest
   {
      /// <summary>
      /// <para>The ID of the partner or affiliate broadcaster that wants to run the commercial.</para>
      /// <para><b>This ID must match the user ID found in the OAuth token.</b></para>
      /// </summary>
      [JsonProperty(PropertyName = "broadcaster_id")]
      public string BroadcasterId { get; set; }

      /// <summary>
      /// <para> The length of the commercial to run, in seconds.</para>
      /// <para> Twitch tries to serve a commercial that’s the requested length, but it may be shorter or longer. 
      /// The maximum length you should request is 180 seconds.</para>
      /// </summary>
      [JsonProperty(PropertyName = "length")]
      public int Length { get; set; }
   }
}