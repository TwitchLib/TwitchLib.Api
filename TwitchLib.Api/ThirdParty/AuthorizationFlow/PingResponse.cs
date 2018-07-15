using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using TwitchLib.Api.Core.Enums;

namespace TwitchLib.Api.ThirdParty.Models.AuthorizationFlow
{
    public class PingResponse
    {
        public bool Success { get; protected set; }
        public string Id { get; protected set; }

        public int Error { get; protected set; }
        public string Message { get; protected set; }

        public List<AuthScopes> Scopes { get; protected set; }
        public string Token { get; protected set; }
        public string Refresh { get; protected set; }
        public string Username { get; protected set; }

        public PingResponse(string jsonStr)
        {
            var json = JObject.Parse(jsonStr);
            Success = bool.Parse(json.SelectToken("success").ToString());
            if(!Success)
            {
                Error = int.Parse(json.SelectToken("error").ToString());
                Message = json.SelectToken("message").ToString();
            } else
            {
                Scopes = new List<AuthScopes>();
                foreach (var scope in json.SelectToken("scopes"))
                    Scopes.Add(StringToScope(scope.ToString()));
                Token = json.SelectToken("token").ToString();
                Refresh = json.SelectToken("refresh").ToString();
                Username = json.SelectToken("username").ToString();
            }
        }
        
        private AuthScopes StringToScope(string scope)
        {
            switch (scope)
            {
                case "user_read":
                    return AuthScopes.User_Read;
                case "user_blocks_edit":
                    return AuthScopes.User_Blocks_Edit;
                case "user_blocks_read":
                    return AuthScopes.User_Blocks_Read;
                case "user_follows_edit":
                    return AuthScopes.User_Follows_Edit;
                case "channel_read":
                    return AuthScopes.Channel_Read;
                case "channel_commercial":
                    return AuthScopes.Channel_Commercial;
                case "channel_stream":
                    return AuthScopes.Channel_Subscriptions;
                case "channel_subscriptions":
                    return AuthScopes.Channel_Subscriptions;
                case "user_subscriptions":
                    return AuthScopes.User_Subscriptions;
                case "channel_check_subscription":
                    return AuthScopes.Channel_Check_Subscription;
                case "chat_login":
                    return AuthScopes.Chat_Login;
                case "channel_feed_read":
                    return AuthScopes.Channel_Feed_Read;
                case "channel_feed_edit":
                    return AuthScopes.Channel_Feed_Edit;
                case "collections_edit":
                    return AuthScopes.Collections_Edit;
                case "communities_edit":
                    return AuthScopes.Communities_Edit;
                case "communities_moderate":
                    return AuthScopes.Communities_Moderate;
                case "viewing_activity_read":
                    return AuthScopes.Viewing_Activity_Read;
                case "user:edit":
                    return AuthScopes.Helix_User_Edit;
                case "user:read:email":
                    return AuthScopes.Helix_User_Read_Email;
                case "clips:edit":
                    return AuthScopes.Helix_Clips_Edit;
                case "analytics:read:games":
                    return AuthScopes.Helix_Analytics_Read_Games;
                case "bits:read":
                    return AuthScopes.Helix_Bits_Read;
                default:
                    throw new Exception("Unknown scope");
            }
        }

    }
}
