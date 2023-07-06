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
            switch (scope)
            {
                case AuthScopes.Chat_Read:
                    return "chat:read";
                case AuthScopes.Channel_Moderate:
                    return "channel:moderate";
                case AuthScopes.Chat_Edit:
                    return "chat:edit";
                case AuthScopes.Whisper_Read:
                    return "whispers:read";
                case AuthScopes.Whisper_Edit:
                    return "whispers:edit";
                case AuthScopes.Analytics_Read_Extensions:
                    return "analytics:read:extensions";
                case AuthScopes.Analytics_Read_Games:
                    return "analytics:read:games";
                case AuthScopes.Bits_Read:
                    return "bits:read";
                case AuthScopes.Channel_Edit_Commercial:
                    return "channel:edit:commercial";
                case AuthScopes.Channel_Manage_Broadcast:
                    return "channel:manage:broadcast";
                case AuthScopes.Channel_Manage_Extensions:
                    return "channel:manage:extensions";
                case AuthScopes.Channel_Manage_Moderators:
                    return "channel:manage:moderators";
                case AuthScopes.Channel_Manage_Redemptions:
                    return "channel:manage:redemptions";
                case AuthScopes.Channel_Manage_Polls:
                    return "channel:manage:polls";
                case AuthScopes.Channel_Manage_Predictions:
                    return "channel:manage:predictions";
                case AuthScopes.Channel_Manage_Schedule:
                    return "channel:manage:schedule";
                case AuthScopes.Channel_Manage_Videos:
                    return "channel:manage:videos";
                case AuthScopes.Channel_Manage_VIPs:
                    return "channel:manage:vips";
                case AuthScopes.Channel_Manage_Guest_Star:
                    return "channel:manage:guest_star";
                case AuthScopes.Channel_Manage_Raids:
                    return "channel:manage:raids";
                case AuthScopes.Channel_Read_Charity:
                    return "channel:read:charity";
                case AuthScopes.Channel_Read_Editors:
                    return "channel:read:editors";
                case AuthScopes.Channel_Read_Goals:
                    return "channel:read:goals";
                case AuthScopes.Channel_Read_Hype_Train:
                    return "channel:read:hype_train";
                case AuthScopes.Channel_Read_Polls:
                    return "channel:read:polls";
                case AuthScopes.Channel_Read_Predictions:
                    return "channel:read:predictions";
                case AuthScopes.Channel_Read_Redemptions:
                    return "channel:read:redemptions";
                case AuthScopes.Channel_Read_Stream_Key:
                    return "channel:read:stream_key";
                case AuthScopes.Channel_Read_Subscriptions:
                    return "channel:read:subscriptions";
                case AuthScopes.Channel_Read_VIPs:
                    return "channel:read:vips";
                case AuthScopes.Channel_Read_Guest_Star:
                    return "channel:read:guest_star";
                case AuthScopes.Clips_Edit:
                    return "clips:edit";
                case AuthScopes.Moderation_Read:
                    return "moderation:read";
                case AuthScopes.User_Edit:
                    return "user:edit";
                case AuthScopes.User_Edit_Follows:
                    return "user:edit:follows";
                case AuthScopes.User_Read_BlockedUsers:
                    return "user:read:blocked_users";
                case AuthScopes.User_Read_Broadcast:
                    return "user:read:broadcast";
                case AuthScopes.User_Read_Email:
                    return "user:read:email";
                case AuthScopes.User_Read_Follows:
                    return "user:read:follows";
                case AuthScopes.User_Read_Subscriptions:
                    return "user:read:subscriptions";
                case AuthScopes.User_Manage_BlockedUsers:
                    return "user:manage:blocked_users";
                case AuthScopes.User_Manage_Chat_Color:
                    return "user:manage:chat_color";
                case AuthScopes.User_Manage_Whispers:
                    return "user:manage:whispers";
                case AuthScopes.Moderator_Manage_Announcements:
                    return "moderator:manage:announcements";
                case AuthScopes.Moderator_Manage_Automod:
                    return "moderator:manage:automod";
                case AuthScopes.Moderator_Manage_Automod_Settings:
                    return "moderator:manage:automod_settings";
                case AuthScopes.Moderator_Manage_Banned_Users:
                    return "moderator:manage:banned_users";
                case AuthScopes.Moderator_Manage_Blocked_Terms:
                    return "moderator:manage:blocked_terms";
                case AuthScopes.Moderator_Manage_Chat_Messages:
                    return "moderator:manage:chat_messages";
                case AuthScopes.Moderator_Manage_Chat_Settings:
                    return "moderator:manage:chat_settings";
                case AuthScopes.Moderator_Read_Automod_Settings:
                    return "moderator:read:automod_settings";
                case AuthScopes.Moderator_Read_Blocked_Terms:
                    return "moderator:read:blocked_terms";
                case AuthScopes.Moderator_Read_Chat_Settings:
                    return "moderator:read:chat_settings";
                case AuthScopes.Moderator_Read_Chatters:
                    return "moderator:read:chatters";
                case AuthScopes.Moderator_Read_Followers:
                    return "moderator:read:followers";
                case AuthScopes.Moderator_Read_Guest_Star:
                    return "moderator:read:guest_star";
                case AuthScopes.Moderator_Read_Shield_Mode:
                    return "moderator:read:shield_mode";
                case AuthScopes.Moderator_Read_Shoutouts:
                    return "moderator:read:shoutouts";
                case AuthScopes.Moderator_Manage_Guest_Star:
                    return "moderator:manage:guest_star";
                case AuthScopes.Moderator_Manage_Shield_Mode:
                    return "moderator:manage:shield_mode";
                case AuthScopes.Moderator_Manage_Shoutouts:
                    return "moderator:manage:shoutouts";
                case AuthScopes.Any:
                case AuthScopes.None:
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Converts Twitch string to AuthScope enum.
        /// </summary>
        /// <param name="scope">Scope as twitch string</param>
        /// <returns>Twitch scope as AuthScope</returns>
        public static AuthScopes StringToScope(string scope)
        {
            switch (scope)
            {
                case "chat:read":
                    return AuthScopes.Chat_Read;
                case "channel:moderate":
                    return AuthScopes.Channel_Moderate;
                case "chat:edit":
                    return AuthScopes.Chat_Edit;
                case "whispers:read":
                    return AuthScopes.Whisper_Read;
                case "whispers:edit":
                    return AuthScopes.Whisper_Edit;
                case "analytics:read:extensions":
                    return AuthScopes.Analytics_Read_Extensions;
                case "analytics:read:games":
                    return AuthScopes.Analytics_Read_Games;
                case "bits:read":
                    return AuthScopes.Bits_Read;
                case "channel:edit:commercial":
                    return AuthScopes.Channel_Edit_Commercial;
                case "channel:manage:broadcast":
                    return AuthScopes.Channel_Manage_Broadcast;
                case "channel:manage:extensions":
                    return AuthScopes.Channel_Manage_Extensions;
                case "channel:manage:moderators":
                    return AuthScopes.Channel_Manage_Moderators;
                case "channel:manage:redemptions":
                    return AuthScopes.Channel_Manage_Redemptions;
                case "channel:manage:polls":
                    return AuthScopes.Channel_Manage_Polls;
                case "channel:manage:predictions":
                    return AuthScopes.Channel_Manage_Predictions;
                case "channel:manage:schedule":
                    return AuthScopes.Channel_Manage_Schedule;
                case "channel:manage:videos":
                    return AuthScopes.Channel_Manage_Videos;
                case "channel:manage:vips":
                    return AuthScopes.Channel_Manage_VIPs;
                case "channel:manage:guest_star":
                    return AuthScopes.Channel_Manage_Guest_Star;
                case "channel:manage:raids":
                    return AuthScopes.Channel_Manage_Raids;
                case "channel:read:charity":
                    return AuthScopes.Channel_Read_Charity;
                case "channel:read:editors":
                    return AuthScopes.Channel_Read_Editors;
                case "channel:read:goals":
                    return AuthScopes.Channel_Read_Goals;
                case "channel:read:hype_train":
                    return AuthScopes.Channel_Read_Hype_Train;
                case "channel:read:polls":
                    return AuthScopes.Channel_Read_Polls;
                case "channel:read:predictions":
                    return AuthScopes.Channel_Read_Predictions;
                case "channel:read:redemptions":
                    return AuthScopes.Channel_Read_Redemptions;
                case "channel:read:stream_key":
                    return AuthScopes.Channel_Read_Stream_Key;
                case "channel:read:subscriptions":
                    return AuthScopes.Channel_Read_Subscriptions;
                case "channel:read:vips":
                    return AuthScopes.Channel_Read_VIPs;
                case "channel:read:guest_star":
                    return AuthScopes.Channel_Read_Guest_Star;
                case "clips:edit":
                    return AuthScopes.Clips_Edit;
                case "moderation:read":
                    return AuthScopes.Moderation_Read;
                case "user:edit":
                    return AuthScopes.User_Edit;
                case "user:edit:follows":
                    return AuthScopes.User_Edit_Follows;
                case "user:read:blocked_users":
                    return AuthScopes.User_Read_BlockedUsers;
                case "user:read:broadcast":
                    return AuthScopes.User_Read_Broadcast;
                case "user:read:email":
                    return AuthScopes.User_Read_Email;
                case "user:read:follows":
                    return AuthScopes.User_Read_Follows;
                case "user:read:subscriptions":
                    return AuthScopes.User_Read_Subscriptions;
                case "user:manage:blocked_users":
                    return AuthScopes.User_Manage_BlockedUsers;
                case "user:manage:chat_color":
                    return AuthScopes.User_Manage_Chat_Color;
                case "user:manage:whispers":
                    return AuthScopes.User_Manage_Whispers;
                case "moderator:manage:announcements":
                    return AuthScopes.Moderator_Manage_Announcements;
                case "moderator:manage:automod":
                    return AuthScopes.Moderator_Manage_Automod;
                case "moderator:manage:automod_settings":
                    return AuthScopes.Moderator_Manage_Automod_Settings;
                case "moderator:manage:banned_users":
                    return AuthScopes.Moderator_Manage_Banned_Users;
                case "moderator:manage:blocked_terms":
                    return AuthScopes.Moderator_Manage_Blocked_Terms;
                case "moderator:manage:chat_messages":
                    return AuthScopes.Moderator_Manage_Chat_Messages;
                case "moderator:manage:chat_settings":
                    return AuthScopes.Moderator_Manage_Chat_Settings;
                case "moderator:manage:guest_star":
                    return AuthScopes.Moderator_Manage_Guest_Star;
                case "moderator:manage:shield_mode":
                    return AuthScopes.Moderator_Manage_Shield_Mode;
                case "moderator:manage:shoutouts":
                    return AuthScopes.Moderator_Manage_Shoutouts;
                case "moderator:read:automod_settings":
                    return AuthScopes.Moderator_Read_Automod_Settings;
                case "moderator:read:blocked_terms":
                    return AuthScopes.Moderator_Read_Blocked_Terms;
                case "moderator:read:chat_settings":
                    return AuthScopes.Moderator_Read_Chat_Settings;
                case "moderator:read:chatters":
                    return AuthScopes.Moderator_Read_Chatters;
                case "moderator:read:followers":
                    return AuthScopes.Moderator_Read_Followers;
                case "moderator:read:guest_star":
                    return AuthScopes.Moderator_Read_Guest_Star;
                case "moderator:read:shield_mode":
                    return AuthScopes.Moderator_Read_Shield_Mode;
                case "moderator:read:shoutouts":
                    return AuthScopes.Moderator_Read_Shoutouts;
                case "":
                    return AuthScopes.None;
                default:
                    throw new Exception("Unknown scope");
            }
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
