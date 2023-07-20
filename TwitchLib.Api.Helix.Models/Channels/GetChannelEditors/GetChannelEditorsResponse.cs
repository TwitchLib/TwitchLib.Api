using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Channels.GetChannelEditors
{
   /// <summary>
   /// <para>Response for GetChannelEditors that returns a list of the broadcaster's channels editors.</para>
   /// </summary>
   public class GetChannelEditorsResponse
   {
      /// <summary>
      /// <para>A list of users that are editors for the specified broadcaster.</para>
      /// <para>The list is empty if the broadcaster doesn’t have editors.</para>
      /// </summary>
      [JsonProperty(PropertyName = "data")]
      public ChannelEditor[] Data { get; protected set; }
   }
}
