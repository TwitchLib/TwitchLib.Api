using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchLib.Api.Core.Models.Undocumented.ChannelExtensionData
{
    public class InstallationStatus
    {
        [JsonProperty(PropertyName = "extension_id")]
        public string ExtensionId { get; protected set; }
        [JsonProperty(PropertyName = "activation_config")]
        public ActivationConfig ActivationConfig { get; protected set; }
        [JsonProperty(PropertyName = "activation_state")]
        public string ActivationState { get; protected set; }
        [JsonProperty(PropertyName = "can_activate")]
        public bool CanActivate { get; protected set; }
    }
}
