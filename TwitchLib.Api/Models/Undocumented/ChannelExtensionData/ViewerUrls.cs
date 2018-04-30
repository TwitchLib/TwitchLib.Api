using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Models.Undocumented.ChannelExtensionData
{
    public class ViewerUrls
    {
        [JsonProperty(PropertyName = "video_overlay")]
        public string VideoOverlay { get; protected set; }
    }
}
