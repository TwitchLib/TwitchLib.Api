namespace TwitchLib.Api.Core.Enums
{
    /// <summary>
    /// Twitch API Version
    /// </summary>
    public enum ApiVersion
    {
        /// <summary>
        /// Auth APIs with base url: id.twitch.tv
        /// </summary>
        Auth = 1,
        /// <summary>
        /// Helix APIs, "Version 6" of the Twitch api with base url: api.twitch.tv/helix
        /// </summary>
        Helix = 6,
        /// <summary>
        /// Anything else
        /// </summary>
        Void = 0
    }
}
