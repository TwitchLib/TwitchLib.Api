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
                case AuthScopes.Channel_Check_Subscription:
                    return "channel_check_subscription";
                case AuthScopes.Channel_Commercial:
                    return "channel_commercial";
                case AuthScopes.Channel_Editor:
                    return "channel_editor";
                case AuthScopes.Channel_Feed_Edit:
                    return "channel_feed_edit";
                case AuthScopes.Channel_Feed_Read:
                    return "channel_feed_read";
                case AuthScopes.Chat_Read:
                    return "chat:read";
                case AuthScopes.Chat_Moderate:
                    return "chat:moderate";
                case AuthScopes.Chat_Edit:
                    return "chat:edit";
                case AuthScopes.Channel_Read:
                    return "channel_read";
                case AuthScopes.Channel_Stream:
                    return "channel_stream";
                case AuthScopes.Channel_Subscriptions:
                    return "channel_subscriptions";
                case AuthScopes.Collections_Edit:
                    return "collections_edit";
                case AuthScopes.Communities_Edit:
                    return "communities_edit";
                case AuthScopes.Communities_Moderate:
                    return "communities_moderate";
                case AuthScopes.OpenId:
                    return "openid";
                case AuthScopes.User_Blocks_Edit:
                    return "user_blocks_edit";
                case AuthScopes.User_Blocks_Read:
                    return "user_blocks_read";
                case AuthScopes.User_Follows_Edit:
                    return "user_follows_edit";
                case AuthScopes.User_Read:
                    return "user_read";
                case AuthScopes.User_Subscriptions:
                    return "user_subscriptions";
                case AuthScopes.Viewing_Activity_Read:
                    return "viewing_activity_read";
                case AuthScopes.Helix_Analytics_Read_Extensions:
                    return "analytics:read:extensions";
                case AuthScopes.Helix_Analytics_Read_Games:
                    return "analytics:read:games";
                case AuthScopes.Helix_Bits_Read:
                    return "bits:read";
                case AuthScopes.Helix_Channel_Edit_Commercial:
                    return "channel:edit:commercial";
                case AuthScopes.Helix_Channel_Manage_Broadcast:
                    return "channel:manage:broadcast";
                case AuthScopes.Helix_Channel_Manage_Extensions:
                    return "channel:manage:extensions";
                case AuthScopes.Helix_Channel_Manage_Moderators:
                    return "channel:manage:moderators";
                case AuthScopes.Helix_Channel_Manage_Redemptions:
                    return "channel:manage:redemptions";
                case AuthScopes.Helix_Channel_Manage_Polls:
                    return "channel:manage:polls";
                case AuthScopes.Helix_Channel_Manage_Predictions:
                    return "channel:manage:predictions";
                case AuthScopes.Helix_Channel_Manage_Schedule:
                    return "channel:manage:schedule";
                case AuthScopes.Helix_Channel_Manage_Videos:
                    return "channel:manage:videos";
                case AuthScopes.Helix_Channel_Manage_VIPs:
                    return "channel:manage:vips";
                case AuthScopes.Helix_Channel_Read_Charity:
                    return "channel:read:charity";
                case AuthScopes.Helix_Channel_Read_Editors:
                    return "channel:read:editors";
                case AuthScopes.Helix_Channel_Read_Goals:
                    return "channel:read:goals";
                case AuthScopes.Helix_Channel_Read_Hype_Train:
                    return "channel:read:hype_train";
                case AuthScopes.Helix_Channel_Read_Polls:
                    return "channel:read:polls";
                case AuthScopes.Helix_Channel_Read_Predictions:
                    return "channel:read:predictions";
                case AuthScopes.Helix_Channel_Read_Redemptions:
                    return "channel:read:redemptions";
                case AuthScopes.Helix_Channel_Read_Stream_Key:
                    return "channel:read:stream_key";
                case AuthScopes.Helix_Channel_Read_Subscriptions:
                    return "channel:read:subscriptions";
                case AuthScopes.Helix_Channel_Read_VIPs:
                    return "channel:read:vips";
                case AuthScopes.Helix_Clips_Edit:
                    return "clips:edit";
                case AuthScopes.Helix_Moderation_Read:
                    return "moderation:read";
                case AuthScopes.Helix_User_Edit:
                    return "user:edit";
                case AuthScopes.Helix_User_Edit_Broadcast:
                    return "user:edit:broadcast";
                case AuthScopes.Helix_User_Edit_Follows:
                    return "user:edit:follows";
                case AuthScopes.Helix_User_Read_BlockedUsers:
                    return "user:read:blocked_users";
                case AuthScopes.Helix_User_Read_Broadcast:
                    return "user:read:broadcast";
                case AuthScopes.Helix_User_Read_Email:
                    return "user:read:email";
                case AuthScopes.Helix_User_Read_Follows:
                    return "user:read:follows";
                case AuthScopes.Helix_User_Read_Subscriptions:
                    return "user:read:subscriptions";
                case AuthScopes.Helix_User_Manage_BlockedUsers:
                    return "user:manage:blocked_users";
                case AuthScopes.Helix_User_Manage_Chat_Color:
                    return "user:manage:chat_color";
                case AuthScopes.Helix_User_Manage_Whispers:
                    return "user:manage:whispers";
                case AuthScopes.Helix_Moderator_Manage_Announcements:
                    return "moderator:manage:announcements";
                case AuthScopes.Helix_Moderator_Manage_Automod:
                    return "moderator:manage:automod";
                case AuthScopes.Helix_Moderator_Manage_Automod_Settings:
                    return "moderator:manage:automod_settings";
                case AuthScopes.Helix_Moderator_Manage_Banned_Users:
                    return "moderator:manage:banned_users";
                case AuthScopes.Helix_Moderator_Manage_Blocked_Terms:
                    return "moderator:manage:blocked_terms";
                case AuthScopes.Helix_moderator_Manage_Chat_Messages:
                    return "moderator:manage:chat_messages";
                case AuthScopes.Helix_Moderator_Manage_Chat_Settings:
                    return "moderator:manage:chat_settings";
                case AuthScopes.Helix_Moderator_Read_Automod_Settings:
                    return "moderator:read:automod_settings";
                case AuthScopes.Helix_Moderator_Read_Blocked_Terms:
                    return "moderator:read:blocked_terms";
                case AuthScopes.Helix_Moderator_Read_Chat_Settings:
                    return "moderator:read:chat_settings";
                case AuthScopes.Helix_Moderator_Read_Chatters:
                    return "moderator:read:chatters";
                case AuthScopes.Helix_Moderator_Read_Followers:
                    return "moderator:read:followers";
                case AuthScopes.Any:
                case AuthScopes.None:
                default:
                    return "";
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
