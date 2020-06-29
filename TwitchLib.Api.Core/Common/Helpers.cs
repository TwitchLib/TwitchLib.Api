using System;
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
                case AuthScopes.Channel_Read:
                    return "channel_read";
                case AuthScopes.Channel_Stream:
                    return "channel_stream";
                case AuthScopes.Channel_Subscriptions:
                    return "channel_subscriptions";
                case AuthScopes.Chat_Login:
                    return "chat_login";
                case AuthScopes.Collections_Edit:
                    return "collections_edit";
                case AuthScopes.Communities_Edit:
                    return "communities_edit";
                case AuthScopes.Communities_Moderate:
                    return "communities_moderate";
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
                case AuthScopes.OpenId:
                    return "openid";
                case AuthScopes.Helix_User_Edit:
                    return "user:edit";
                case AuthScopes.Helix_User_Read_Email:
                    return "user:read:email";
                case AuthScopes.Helix_Clips_Edit:
                    return "clips:edit";
                case AuthScopes.Helix_Analytics_Read_Games:
                    return "analytics:read:games";
                case AuthScopes.Helix_Analytics_Read_Extensions:
                    return "analytics:read:extensions";
                case AuthScopes.Helix_Bits_Read:
                    return "bits:read";
                case AuthScopes.Helix_User_Edit_Broadcast:
                    return "user:edit:broadcast";
                case AuthScopes.Helix_User_Read_Broadcast:
                    return "user:read:broadcast";
                case AuthScopes.Helix_Channel_Read_Subscriptions:
                    return "channel:read:subscriptions";
                case AuthScopes.Helix_Channel_Read_Hype_Train:
                    return "channel:read:hype_train";
                default:
                    return "";
            }
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}