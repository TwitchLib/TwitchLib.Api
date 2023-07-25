using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.ContentClassificationLabels
{
   /// <summary>
   /// <para>The list of Content Classification Labels available.</para>
   /// </summary>
   public class ContentClassificationLabel
   {
      /// <summary>
      /// <para>Unique identifier for the Content Classification Labels.</para>
      /// </summary>
      [JsonProperty(PropertyName = "id")]
      public string ID { get; set; }

      /// <summary>
      /// <para>Localized description of the Content Classification Labels.</para>
      /// </summary>
      [JsonProperty(PropertyName = "description")]
      public string Description { get; set; }

      /// <summary>
      /// <para>Localized name of the Content Classification Labels.</para>
      /// </summary>
      [JsonProperty(PropertyName = "name")]
      public string Name { get; set; }
   }
}
