#nullable disable
using Newtonsoft.Json;
using TwitchLib.Api.Core.Enums;

namespace TwitchLib.Api.Helix.Models.Extensions.ReleasedExtensions;

/// <summary>
/// A specified extension.
/// </summary>
public class ReleasedExtension
{
    /// <summary>
    /// The name of the user or organization that owns the extension.
    /// </summary>
    [JsonProperty(PropertyName = "author_name")]
    public string AuthorName { get; protected set; }

    /// <summary>
    /// A Boolean value that determines whether the extension has features that use Bits. 
    /// </summary>
    [JsonProperty(PropertyName = "bits_enabled")]
    public bool BitsEnabled { get; protected set; }

    /// <summary>
    /// A Boolean value that determines whether a user can install the extension on their channel.
    /// </summary>
    [JsonProperty(PropertyName = "can_install")]
    public bool CanInstall { get; protected set; }

    /// <summary>
    /// The location of where the extension’s configuration is stored.
    /// </summary>
    [JsonProperty(PropertyName = "configuration_location")]
    public string ConfigurationLocation { get; protected set; }

    /// <summary>
    /// A longer description of the extension. It appears on the details page.
    /// </summary>
    [JsonProperty(PropertyName = "description")]
    public string Description { get; protected set; }

    /// <summary>
    /// A URL to the extension’s Terms of Service.
    /// </summary>
    [JsonProperty(PropertyName = "eula_tos_url")]
    public string EulaTosUrl { get; protected set; }

    /// <summary>
    /// A Boolean value that determines whether the extension can communicate with the installed channel’s chat.
    /// </summary>
    [JsonProperty(PropertyName = "has_chat_support")]
    public bool HasChatSupport { get; protected set; }

    /// <summary>
    /// A URL to the default icon that’s displayed in the Extensions directory.
    /// </summary>
    [JsonProperty(PropertyName = "icon_url")]
    public string IconUrl { get; protected set; }

    /// <summary>
    /// A list that contains URLs to different sizes of the default icon.
    /// </summary>
    [JsonProperty(PropertyName = "icon_urls")]
    public IconUrls IconUrls { get; protected set; }

    /// <summary>
    /// The extension’s ID.
    /// </summary>
    [JsonProperty(PropertyName = "id")]
    public string Id { get; protected set; }

    /// <summary>
    /// The extension’s name.
    /// </summary>
    [JsonProperty(PropertyName = "name")]
    public string Name { get; protected set; }

    /// <summary>
    /// A URL to the extension’s privacy policy.
    /// </summary>
    [JsonProperty(PropertyName = "privacy_policy_url")]
    public string PrivacyPolicyUrl { get; protected set; }

    /// <summary>
    /// A Boolean value that determines whether the extension wants to explicitly ask viewers to link their Twitch identity.
    /// </summary>
    [JsonProperty(PropertyName = "request_identity_link")]
    public bool RequestIdentityLink { get; protected set; }

    /// <summary>
    /// A list of URLs to screenshots that are shown in the Extensions marketplace.
    /// </summary>
    [JsonProperty(PropertyName = "screenshot_urls")]
    public string[] ScreenshotUrls { get; protected set; }

    /// <summary>
    /// The extension’s state. 
    /// </summary>
    [JsonProperty(PropertyName = "state")]
    public ExtensionState State { get; protected set; }

    /// <summary>
    /// Indicates whether the extension can view the user’s subscription level on the channel that the extension is installed on. 
    /// </summary>
    [JsonProperty(PropertyName = "subscriptions_support_level")]
    public string SubscriptionsSupportLevel { get; protected set; }

    /// <summary>
    /// A short description of the extension that streamers see when hovering over the discovery splash screen in the Extensions manager.
    /// </summary>
    [JsonProperty(PropertyName = "summary")]
    public string Summary { get; protected set; }

    /// <summary>
    /// The email address that users use to get support for the extension.
    /// </summary>
    [JsonProperty(PropertyName = "support_email")]
    public string SupportEmail { get; protected set; }

    /// <summary>
    /// The extension’s version number.
    /// </summary>
    [JsonProperty(PropertyName = "version")]
    public string Version { get; protected set; }

    /// <summary>
    /// A brief description displayed on the channel to explain how the extension works.
    /// </summary>
    [JsonProperty(PropertyName = "viewer_summary")]
    public string ViewerSummary { get; protected set; }

    /// <summary>
    /// Describes all views-related information such as how the extension is displayed on mobile devices.
    /// </summary>
    [JsonProperty(PropertyName = "views")]
    public Views Views { get; protected set; }

    /// <summary>
    /// Allowlisted configuration URLs for displaying the extension.
    /// </summary>
    [JsonProperty(PropertyName = "allowlisted_config_urls")]
    public string[] AllowlistedConfigUrls { get; protected set; }

    /// <summary>
    /// Allowlisted panel URLs for displaying the extension.
    /// </summary>
    [JsonProperty(PropertyName = "allowlisted_panel_urls")]
    public string[] AllowlistedPanelUrls { get; protected set; }
}
