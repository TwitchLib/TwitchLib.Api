using Newtonsoft.Json;
using TwitchLib.Api.Core.Enums;

namespace TwitchLib.Api.Helix.Models.Extensions.ReleasedExtensions
{
    public class ReleasedExtension
    {
        [JsonProperty(PropertyName = "author_name")]
        public string AuthorName { get; protected set; }
        [JsonProperty(PropertyName = "bits_enabled")]
        public bool BitsEnabled { get; protected set; }
        [JsonProperty(PropertyName = "can_install")]
        public bool CanInstall { get; protected set; }
        [JsonProperty(PropertyName = "configuration_location")]
        public string ConfigurationLocation { get; protected set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; protected set; }
        [JsonProperty(PropertyName = "eula_tos_url")]
        public string EulaTosUrl { get; protected set; }
        [JsonProperty(PropertyName = "has_chat_support")]
        public bool HasChatSupport { get; protected set; }
        [JsonProperty(PropertyName = "icon_url")]
        public string IconUrl { get; protected set; }
        [JsonProperty(PropertyName = "icon_urls")]
        public IconUrls IconUrls { get; protected set; }
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; protected set; }
        [JsonProperty(PropertyName = "privacy_policy_url")]
        public string PrivacyPolicyUrl { get; protected set; }
        [JsonProperty(PropertyName = "request_identity_link")]
        public bool RequestIdentityLink { get; protected set; }
        [JsonProperty(PropertyName = "screenshot_urls")]
        public string[] ScreenshotUrls { get; protected set; }
        [JsonProperty(PropertyName = "state")]
        public ExtensionState State { get; protected set; }
        [JsonProperty(PropertyName = "subscriptions_support_level")]
        public string SubscriptionsSupportLevel { get; protected set; }
        [JsonProperty(PropertyName = "summary")]
        public string Summary { get; protected set; }
        [JsonProperty(PropertyName = "support_email")]
        public string SupportEmail { get; protected set; }
        [JsonProperty(PropertyName = "version")]
        public string Version { get; protected set; }
        [JsonProperty(PropertyName = "viewer_summary")]
        public string ViewerSummary { get; protected set; }
        [JsonProperty(PropertyName = "views")]
        public Views Views { get; protected set; }
        [JsonProperty(PropertyName = "allowlisted_config_urls")]
        public string[] AllowlistedConfigUrls { get; protected set; }
        [JsonProperty(PropertyName = "allowlisted_panel_urls")]
        public string[] AllowlistedPanelUrls { get; protected set; }
    }
}
