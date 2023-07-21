using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.ContentClassificationLabels
{
   /// <summary>
   /// Response from GetContentClassificationLabels which gets information about Twitch content classification labels.
   /// </summary>
   public class GetContentClassificationLabelsResponse
   {
      /// <summary>
      /// A list that contains information about the available content classification labels.
      /// </summary>
      [JsonProperty(PropertyName = "data")]
      public ContentClassificationLabel[] Data { get; protected set; }
   }
}
