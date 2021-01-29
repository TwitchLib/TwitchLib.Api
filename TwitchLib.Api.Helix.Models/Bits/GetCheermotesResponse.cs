using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Helix.Models.Bits
{
    public class GetCheermotesResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Cheermote[] Listings { get; protected set; }
    }
}
