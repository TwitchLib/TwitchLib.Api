using Newtonsoft.Json;
using System.Collections.Generic;

namespace TwitchLib.Api.Helix.Models.Bits
{
   /// <summary>
   /// <para>Animated and static image sets for a Cheermote.</para>
   /// <para>Each set is a dictionary with the following sizes:  1, 1.5, 2, 3, and 4.</para>
   /// The value of each size contains the URL to the image.
   /// </summary>
   public class ImageList
   {
      /// <summary>
      /// <para>Animated image set for a Cheermote and include the following sizes: 1, 1.5, 2, 3, and 4</para>
      /// The value of each size contains the URL to the image.
      /// </summary>
      [JsonProperty(PropertyName = "animated")]
      public Dictionary<string, string> Animated { get; protected set; }

      /// <summary>
      /// <para>Static image set for a Cheermote and include the following sizes: 1, 1.5, 2, 3, and 4</para>
      /// The value of each size contains the URL to the image.
      /// </summary>
      [JsonProperty(PropertyName = "static")]
      public Dictionary<string, string> Static { get; protected set; }
   }
}
