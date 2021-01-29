using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Channels.GetChannelInformation
{
    public class GetChannelInformationResponse
    {
        [JsonProperty(PropertyName = "data")]
        public ChannelInformation[] Data { get; protected set; }
    }
}
