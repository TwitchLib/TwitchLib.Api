using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Bits
{
    public class Cheermote
    {
        /// <summary>
        /// The string used to Cheer that precedes the Bits amount.
        /// </summary>
        [JsonProperty(PropertyName = "prefix")]
        public string Prefix { get; protected set; }

        /// <summary>
        /// An array of Cheermotes with their metadata.
        /// </summary>
        [JsonProperty(PropertyName = "tiers")]
        public Tier[] Tiers { get; protected set; }

        /// <summary>
        /// Minimum number of bits needed to be used to hit the given tier of emote.  
        /// </summary>
        [JsonProperty(PropertyName = "min_bits")]
        public int MinBits { get; protected set; }

        /// <summary>
        /// ID of the emote tier. Possible tiers are: 1,100,500,1000,5000, 10k, or 100k.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }

        /// <summary>
        /// Hex code for the color associated with the bits of that tier.
        /// Grey, Purple, Teal, Blue, or Red color to match the base bit type.
        /// </summary>
        [JsonProperty(PropertyName = "color")]
        public string Color { get; protected set; }

        /// <summary>
        /// Structure containing both animated and static image sets, sorted by light and dark.
        /// </summary>
        [JsonProperty(PropertyName = "images")]
        public Images Images { get; protected set; }

        /// <summary>
        /// Indicates whether or not emote information is accessible to users.
        /// </summary>
        [JsonProperty(PropertyName = "can_cheer")]
        public bool CanCheer { get; protected set; }

        /// <summary>
        /// Indicates whether or not we hide the emote from the bits card.
        /// </summary>
        [JsonProperty(PropertyName = "show_in_bits_card")]
        public bool ShowInBitsCard { get; protected set; }

        /// <summary>
        /// Shows whether the emote is global_first_party,  global_third_party, channel_custom, display_only, or sponsored.
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; protected set; }

        /// <summary>
        /// Order of the emotes as shown in the bits card, in ascending order.
        /// </summary>
        [JsonProperty(PropertyName = "order")]
        public int Order { get; protected set; }

        /// <summary>
        /// The data when this Cheermote was last updated.
        /// </summary>
        [JsonProperty(PropertyName = "last_updated")]
        public DateTime LastUpdated { get; protected set; }

        /// <summary>
        /// Indicates whether or not this emote provides a charity contribution match during charity campaigns.
        /// </summary>
        [JsonProperty(PropertyName = "is_charitable")]
        public bool IsCharitable { get; protected set; }
    }
}
