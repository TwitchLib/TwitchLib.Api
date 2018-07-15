using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Core.Models.Undocumented.ChannelExtensionData
{
    public class View
    {
        [JsonProperty(PropertyName = "viewer_url")]
        public string ViewerUrl { get; protected set; }
    }
}
