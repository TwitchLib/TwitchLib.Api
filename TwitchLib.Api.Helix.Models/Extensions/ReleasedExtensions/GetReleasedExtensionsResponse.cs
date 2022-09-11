using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Extensions.ReleasedExtensions
{
    public class GetReleasedExtensionsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public ReleasedExtension[] Data { get; protected set; }
    }
}
