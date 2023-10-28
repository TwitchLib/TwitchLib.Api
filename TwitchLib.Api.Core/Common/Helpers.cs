using System;
using System.Text;
using TwitchLib.Api.Core.Enums;

namespace TwitchLib.Api.Core.Common
{
    /// <summary>Static class of helper functions used around the project.</summary>
    public static class Helpers
    {
        /// <summary>
        /// Function that extracts just the token for consistency
        /// </summary>
        /// <param name="token">Full token string</param>
        /// <returns></returns>
        public static string FormatOAuth(string token)
        {
            return token.Contains(" ") ? token.Split(' ')[1] : token;
        }

        /// <summary>
        /// Converts AuthScope enum to Twitch scope string
        /// </summary>
        /// <param name="scope">Scope as AuthScope Enum</param>
        /// <returns>Twitch scope string</returns>
        public static string AuthScopesToString(AuthScopes scope)
        {
            return scope switch
            {
                AuthScopes.Chat_Read => "chat:read",
                AuthScopes.Channel_Moderate => "channel:moderate",
                AuthScopes.Chat_Edit => "chat:edit",
                AuthScopes.Whisper_Read => "whispers:read",
                AuthScopes.Whisper_Edit => "whispers:edit",
                AuthScopes.Analytics_Read_Extensions => "analytics:read:extensions",
                AuthScopes.Analytics_Read_Games => "analytics:read:games",
                AuthScopes.Bits_Read => "bits:read",
                AuthScopes.Channel_Bot => "channel:bot",
                AuthScopes.Channel_Manage_Ads => "channel:manage:ads",
                AuthScopes.Channel_Edit_Commercial => "channel:edit:commercial",
                AuthScopes.Channel_Manage_Broadcast => "channel:manage:broadcast",
                AuthScopes.Channel_Manage_Extensions => "channel:manage:extensions",
                AuthScopes.Channel_Manage_Moderators => "channel:manage:moderators",
                AuthScopes.Channel_Manage_Redemptions => "channel:manage:redemptions",
                AuthScopes.Channel_Manage_Polls => "channel:manage:polls",
                AuthScopes.Channel_Manage_Predictions => "channel:manage:predictions",
                AuthScopes.Channel_Manage_Schedule => "channel:manage:schedule",
                AuthScopes.Channel_Manage_Videos => "channel:manage:videos",
                AuthScopes.Channel_Manage_VIPs => "channel:manage:vips",
                AuthScopes.Channel_Manage_Guest_Star => "channel:manage:guest_star",
                AuthScopes.Channel_Manage_Raids => "channel:manage:raids",
                AuthScopes.Channel_Read_Ads => "channel:read:ads",
                AuthScopes.Channel_Read_Charity => "channel:read:charity",
                AuthScopes.Channel_Read_Editors => "channel:read:editors",
                AuthScopes.Channel_Read_Goals => "channel:read:goals",
                AuthScopes.Channel_Read_Hype_Train => "channel:read:hype_train",
                AuthScopes.Channel_Read_Polls => "channel:read:polls",
                AuthScopes.Channel_Read_Predictions => "channel:read:predictions",
                AuthScopes.Channel_Read_Redemptions => "channel:read:redemptions",
                AuthScopes.Channel_Read_Stream_Key => "channel:read:stream_key",
                AuthScopes.Channel_Read_Subscriptions => "channel:read:subscriptions",
                AuthScopes.Channel_Read_VIPs => "channel:read:vips",
                AuthScopes.Channel_Read_Guest_Star => "channel:read:guest_star",
                AuthScopes.Clips_Edit => "clips:edit",
                AuthScopes.Moderation_Read => "moderation:read",
                AuthScopes.User_Bot => "user:bot",
                AuthScopes.User_Edit => "user:edit",
                AuthScopes.User_Edit_Follows => "user:edit:follows",
                AuthScopes.User_Read_BlockedUsers => "user:read:blocked_users",
                AuthScopes.User_Read_Broadcast => "user:read:broadcast",
                AuthScopes.User_Read_Chat => "user:read:chat",
                AuthScopes.User_Read_Email => "user:read:email",
                AuthScopes.User_Read_Follows => "user:read:follows",
                AuthScopes.User_Read_Subscriptions => "user:read:subscriptions",
                AuthScopes.User_Manage_BlockedUsers => "user:manage:blocked_users",
                AuthScopes.User_Manage_Chat_Color => "user:manage:chat_color",
                AuthScopes.User_Manage_Whispers => "user:manage:whispers",
                AuthScopes.Moderator_Manage_Announcements => "moderator:manage:announcements",
                AuthScopes.Moderator_Manage_Automod => "moderator:manage:automod",
                AuthScopes.Moderator_Manage_Automod_Settings => "moderator:manage:automod_settings",
                AuthScopes.Moderator_Manage_Banned_Users => "moderator:manage:banned_users",
                AuthScopes.Moderator_Manage_Blocked_Terms => "moderator:manage:blocked_terms",
                AuthScopes.Moderator_Manage_Chat_Messages => "moderator:manage:chat_messages",
                AuthScopes.Moderator_Manage_Chat_Settings => "moderator:manage:chat_settings",
                AuthScopes.Moderator_Read_Automod_Settings => "moderator:read:automod_settings",
                AuthScopes.Moderator_Read_Blocked_Terms => "moderator:read:blocked_terms",
                AuthScopes.Moderator_Read_Chat_Settings => "moderator:read:chat_settings",
                AuthScopes.Moderator_Read_Chatters => "moderator:read:chatters",
                AuthScopes.Moderator_Read_Followers => "moderator:read:followers",
                AuthScopes.Moderator_Read_Guest_Star => "moderator:read:guest_star",
                AuthScopes.Moderator_Read_Shield_Mode => "moderator:read:shield_mode",
                AuthScopes.Moderator_Read_Shoutouts => "moderator:read:shoutouts",
                AuthScopes.Moderator_Manage_Guest_Star => "moderator:manage:guest_star",
                AuthScopes.Moderator_Manage_Shield_Mode => "moderator:manage:shield_mode",
                AuthScopes.Moderator_Manage_Shoutouts => "moderator:manage:shoutouts",
                AuthScopes.Any => string.Empty,
                AuthScopes.None => string.Empty,
                _ => string.Empty
            };
        }

        /// <summary>
        /// Converts Twitch string to AuthScope enum.
        /// </summary>
        /// <param name="scope">Scope as twitch string</param>
        /// <returns>Twitch scope as AuthScope</returns>
        public static AuthScopes StringToScope(string scope)
        {
            return scope switch
            {
                "chat:read" => AuthScopes.Chat_Read,
                "channel:moderate" => AuthScopes.Channel_Moderate,
                "chat:edit" => AuthScopes.Chat_Edit,
                "whispers:read" => AuthScopes.Whisper_Read,
                "whispers:edit" => AuthScopes.Whisper_Edit,
                "analytics:read:extensions" => AuthScopes.Analytics_Read_Extensions,
                "analytics:read:games" => AuthScopes.Analytics_Read_Games,
                "bits:read" => AuthScopes.Bits_Read,
                "channel:bot" => AuthScopes.Channel_Bot,
                "channel:edit:commercial" => AuthScopes.Channel_Edit_Commercial,
                "channel:manage:ads" => AuthScopes.Channel_Manage_Ads,
                "channel:manage:broadcast" => AuthScopes.Channel_Manage_Broadcast,
                "channel:manage:extensions" => AuthScopes.Channel_Manage_Extensions,
                "channel:manage:moderators" => AuthScopes.Channel_Manage_Moderators,
                "channel:manage:redemptions" => AuthScopes.Channel_Manage_Redemptions,
                "channel:manage:polls" => AuthScopes.Channel_Manage_Polls,
                "channel:manage:predictions" => AuthScopes.Channel_Manage_Predictions,
                "channel:manage:schedule" => AuthScopes.Channel_Manage_Schedule,
                "channel:manage:videos" => AuthScopes.Channel_Manage_Videos,
                "channel:manage:vips" => AuthScopes.Channel_Manage_VIPs,
                "channel:manage:guest_star" => AuthScopes.Channel_Manage_Guest_Star,
                "channel:manage:raids" => AuthScopes.Channel_Manage_Raids,
                "channel:read:ads" => AuthScopes.Channel_Read_Ads,
                "channel:read:charity" => AuthScopes.Channel_Read_Charity,
                "channel:read:editors" => AuthScopes.Channel_Read_Editors,
                "channel:read:goals" => AuthScopes.Channel_Read_Goals,
                "channel:read:hype_train" => AuthScopes.Channel_Read_Hype_Train,
                "channel:read:polls" => AuthScopes.Channel_Read_Polls,
                "channel:read:predictions" => AuthScopes.Channel_Read_Predictions,
                "channel:read:redemptions" => AuthScopes.Channel_Read_Redemptions,
                "channel:read:stream_key" => AuthScopes.Channel_Read_Stream_Key,
                "channel:read:subscriptions" => AuthScopes.Channel_Read_Subscriptions,
                "channel:read:vips" => AuthScopes.Channel_Read_VIPs,
                "channel:read:guest_star" => AuthScopes.Channel_Read_Guest_Star,
                "clips:edit" => AuthScopes.Clips_Edit,
                "moderation:read" => AuthScopes.Moderation_Read,
                "user:bot" => AuthScopes.User_Bot,
                "user:edit" => AuthScopes.User_Edit,
                "user:edit:follows" => AuthScopes.User_Edit_Follows,
                "user:read:blocked_users" => AuthScopes.User_Read_BlockedUsers,
                "user:read:broadcast" => AuthScopes.User_Read_Broadcast,
                "user:read:chat" => AuthScopes.User_Read_Chat,
                "user:read:email" => AuthScopes.User_Read_Email,
                "user:read:follows" => AuthScopes.User_Read_Follows,
                "user:read:subscriptions" => AuthScopes.User_Read_Subscriptions,
                "user:manage:blocked_users" => AuthScopes.User_Manage_BlockedUsers,
                "user:manage:chat_color" => AuthScopes.User_Manage_Chat_Color,
                "user:manage:whispers" => AuthScopes.User_Manage_Whispers,
                "moderator:manage:announcements" => AuthScopes.Moderator_Manage_Announcements,
                "moderator:manage:automod" => AuthScopes.Moderator_Manage_Automod,
                "moderator:manage:automod_settings" => AuthScopes.Moderator_Manage_Automod_Settings,
                "moderator:manage:banned_users" => AuthScopes.Moderator_Manage_Banned_Users,
                "moderator:manage:blocked_terms" => AuthScopes.Moderator_Manage_Blocked_Terms,
                "moderator:manage:chat_messages" => AuthScopes.Moderator_Manage_Chat_Messages,
                "moderator:manage:chat_settings" => AuthScopes.Moderator_Manage_Chat_Settings,
                "moderator:manage:guest_star" => AuthScopes.Moderator_Manage_Guest_Star,
                "moderator:manage:shield_mode" => AuthScopes.Moderator_Manage_Shield_Mode,
                "moderator:manage:shoutouts" => AuthScopes.Moderator_Manage_Shoutouts,
                "moderator:read:automod_settings" => AuthScopes.Moderator_Read_Automod_Settings,
                "moderator:read:blocked_terms" => AuthScopes.Moderator_Read_Blocked_Terms,
                "moderator:read:chat_settings" => AuthScopes.Moderator_Read_Chat_Settings,
                "moderator:read:chatters" => AuthScopes.Moderator_Read_Chatters,
                "moderator:read:followers" => AuthScopes.Moderator_Read_Followers,
                "moderator:read:guest_star" => AuthScopes.Moderator_Read_Guest_Star,
                "moderator:read:shield_mode" => AuthScopes.Moderator_Read_Shield_Mode,
                "moderator:read:shoutouts" => AuthScopes.Moderator_Read_Shoutouts,
                "" => AuthScopes.None,
                _ => throw new Exception("Unknown scope")
            };
        }

        /// <summary>
        /// Helper for Base64 encoding a given input
        /// </summary>
        /// <param name="plainText">plain UTF8 string</param>
        /// <returns>input as Base64 string</returns>
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}
