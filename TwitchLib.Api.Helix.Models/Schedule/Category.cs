using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Schedule
{
    public class Category
    {
        [JsonProperty("id")]
        public string Id { get; protected set; }
        [JsonProperty("name")]
        public string Name { get; protected set; }
    }
}