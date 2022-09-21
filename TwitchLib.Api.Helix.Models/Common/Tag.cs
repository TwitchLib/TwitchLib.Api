using System.Collections.Generic;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Common
{
    public class Tag
    {
        [JsonProperty(PropertyName = "tag_id")]
        public string TagId { get; protected set; }
        [JsonProperty(PropertyName = "is_auto")]
        public bool IsAuto { get; protected set; }
        [JsonProperty(PropertyName = "localization_names")]
        public Dictionary<string, string> LocalizationNames { get; protected set; }
        [JsonProperty(PropertyName = "localization_descriptions")]
        public Dictionary<string, string> LocalizationDescriptions { get; protected set; }
    }
}
