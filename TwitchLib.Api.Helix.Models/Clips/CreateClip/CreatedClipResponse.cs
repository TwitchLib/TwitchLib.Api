using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Clips.CreateClip
{
    public class CreatedClipResponse
    {
        [JsonProperty(PropertyName = "data")]
        public CreatedClip[] CreatedClips { get; protected set; }
    }
}
