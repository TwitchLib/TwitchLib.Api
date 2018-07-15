using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Clips.GetClip
{
    public class GetClipResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Clip[] Clips { get; protected set; }
    }
}
