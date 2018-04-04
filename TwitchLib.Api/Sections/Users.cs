﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Enums;
using TwitchLib.Api.Exceptions;

namespace TwitchLib.Api.Sections
{
    public class Users
    {
        public Users(TwitchAPI api)
        {
            v5 = new V5(api);
            helix = new Helix(api);
        }
        
        public V5 v5 { get; }
        public Helix helix { get; }

        public class V5 : ApiSection
        {
            public V5(TwitchAPI api) : base(api)
            {
            }
            #region GetUsersByName
            public async Task<Models.v5.Users.Users> GetUsersByNameAsync(List<string> usernames)
            {
                if (usernames == null || usernames.Count == 0) { throw new BadParameterException("The username list is not valid. It is not allowed to be null or empty."); }
                var getParams = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("login", string.Join(",", usernames)) };
                return await Api.TwitchGetGenericAsync<Models.v5.Users.Users>("/users", ApiVersion.v5, getParams).ConfigureAwait(false);
            }
            #endregion
            #region GetUser
            public async Task<Models.v5.Users.UserAuthed> GetUserAsync(string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.User_Read, authToken);
                return await Api.TwitchGetGenericAsync<Models.v5.Users.UserAuthed>("/user", ApiVersion.v5, accessToken: authToken).ConfigureAwait(false);
            }
            #endregion
            #region GetUserByID
            public async Task<Models.v5.Users.User> GetUserByIDAsync(string userId)
            {
                if (string.IsNullOrWhiteSpace(userId)) { throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Api.TwitchGetGenericAsync<Models.v5.Users.User>($"/users/{userId}", ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region GetUserByName
            public async Task<Models.v5.Users.Users> GetUserByNameAsync(string username)
            {
                if (string.IsNullOrEmpty(username)) { throw new BadParameterException("The username is not valid."); }
                var getParams = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("login", username) };
                return await Api.TwitchGetGenericAsync<Models.v5.Users.Users>("/users", ApiVersion.v5, getParams).ConfigureAwait(false);
            }
            #endregion
            #region GetUserEmotes
            public async Task<Models.v5.Users.UserEmotes> GetUserEmotesAsync(string userId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.User_Subscriptions, authToken);
                if (string.IsNullOrWhiteSpace(userId)) { throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Api.TwitchGetGenericAsync<Models.v5.Users.UserEmotes>($"/users/{userId}/emotes", ApiVersion.v5, accessToken: authToken).ConfigureAwait(false);
            }
            #endregion
            #region CheckUserSubscriptionByChannel
            public async Task<Models.v5.Subscriptions.Subscription> CheckUserSubscriptionByChannelAsync(string userId, string channelId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.User_Subscriptions, authToken);
                if (string.IsNullOrWhiteSpace(userId)) { throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(channelId)) { throw new BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Api.TwitchGetGenericAsync<Models.v5.Subscriptions.Subscription>($"/users/{userId}/subscriptions/{channelId}", ApiVersion.v5, accessToken: authToken).ConfigureAwait(false);
            }
            #endregion
            #region GetUserFollows
            public async Task<Models.v5.Users.UserFollows> GetUserFollowsAsync(string userId, int? limit = null, int? offset = null, string direction = null, string sortby = null)
            {
                if (string.IsNullOrWhiteSpace(userId)) { throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                var getParams = new List<KeyValuePair<string, string>>();
                if (limit.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("limit", limit.Value.ToString()));
                if (offset.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("offset", offset.Value.ToString()));
                if (!string.IsNullOrEmpty(direction) && (direction == "asc" || direction == "desc"))
                    getParams.Add(new KeyValuePair<string, string>("direction", direction));
                if (!string.IsNullOrEmpty(sortby) && (sortby == "created_at" || sortby == "last_broadcast" || sortby == "login"))
                    getParams.Add(new KeyValuePair<string, string>("sortby", sortby));
                
                return await Api.TwitchGetGenericAsync < Models.v5.Users.UserFollows>($"/users/{userId}/follows/channels", ApiVersion.v5, getParams).ConfigureAwait(false);
            }
            #endregion
            #region CheckUserFollowsByChannel
            public async Task<Models.v5.Users.UserFollow> CheckUserFollowsByChannelAsync(string userId, string channelId)
            {
                if (string.IsNullOrWhiteSpace(userId)) { throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(channelId)) { throw new BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Api.TwitchGetGenericAsync<Models.v5.Users.UserFollow>($"/users/{userId}/follows/channels/{channelId}", ApiVersion.v5).ConfigureAwait(false);
            }
            #endregion
            #region UserFollowsChannel
            public async Task<bool> UserFollowsChannelAsync(string userId, string channelId)
            {
                if (string.IsNullOrWhiteSpace(userId)) { throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(channelId)) { throw new BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                try
                {
                    await Api.TwitchGetGenericAsync<Models.v5.Users.UserFollow>($"/users/{userId}/follows/channels/{channelId}", ApiVersion.v5).ConfigureAwait(false);
                    return true;
                }
                catch (BadResourceException)
                {
                    return false;
                }
            }
            #endregion
            #region FollowChannel
            public async Task<Models.v5.Users.UserFollow> FollowChannelAsync(string userId, string channelId, bool? notifications = null, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.User_Follows_Edit, authToken);
                if (string.IsNullOrWhiteSpace(userId)) { throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(channelId)) { throw new BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                var optionalRequestBody = notifications.HasValue ? "{\"notifications\": " + notifications.Value + "}" : null;
                return await Api.TwitchPutGenericAsync<Models.v5.Users.UserFollow>($"/users/{userId}/follows/channels/{channelId}", ApiVersion.v5, optionalRequestBody, accessToken: authToken).ConfigureAwait(false);
            }
            #endregion
            #region UnfollowChannel
            public async Task UnfollowChannelAsync(string userId, string channelId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.User_Follows_Edit, authToken);
                if (string.IsNullOrWhiteSpace(userId)) { throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(channelId)) { throw new BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                await Api.TwitchDeleteAsync($"/users/{userId}/follows/channels/{channelId}", ApiVersion.v5, accessToken: authToken).ConfigureAwait(false);
            }
            #endregion
            #region GetUserBlockList
            public async Task<Models.v5.Users.UserBlocks> GetUserBlockListAsync(string userId, int? limit = null, int? offset = null, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.User_Blocks_Read, authToken);
                if (string.IsNullOrWhiteSpace(userId)) { throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                var getParams = new List<KeyValuePair<string, string>>();
                if (limit.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("limit", limit.Value.ToString()));
                if (offset.HasValue)
                    getParams.Add(new KeyValuePair<string, string>("offset", offset.Value.ToString()));
                
                return await Api.TwitchGetGenericAsync<Models.v5.Users.UserBlocks>($"/users/{userId}/blocks", ApiVersion.v5, getParams, authToken).ConfigureAwait(false);
            }
            #endregion
            #region BlockUser
            public async Task<Models.v5.Users.UserBlock> BlockUserAsync(string sourceUserId, string targetUserId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.User_Blocks_Edit, authToken);
                if (string.IsNullOrWhiteSpace(sourceUserId)) { throw new BadParameterException("The source user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(targetUserId)) { throw new BadParameterException("The target user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                return await Api.TwitchPutGenericAsync<Models.v5.Users.UserBlock>($"/users/{sourceUserId}/blocks/{targetUserId}", ApiVersion.v5, null, accessToken: authToken).ConfigureAwait(false);
            }
            #endregion
            #region UnblockUser
            public async Task UnblockUserAsync(string sourceUserId, string targetUserId, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.User_Blocks_Edit, authToken);
                if (string.IsNullOrWhiteSpace(sourceUserId)) { throw new BadParameterException("The source user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                if (string.IsNullOrWhiteSpace(targetUserId)) { throw new BadParameterException("The target user id is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                await Api.TwitchDeleteAsync($"/users/{sourceUserId}/blocks/{targetUserId}", ApiVersion.v5, accessToken: authToken).ConfigureAwait(false);
            }
            #endregion
            #region ViewerHeartbeatService
            #region CreateUserConnectionToViewerHeartbeatService
            public async Task CreateUserConnectionToViewerHeartbeatServiceAsync(string identifier, string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Viewing_Activity_Read, authToken);
                if (string.IsNullOrWhiteSpace(identifier)) { throw new BadParameterException("The identifier is not valid. It is not allowed to be null, empty or filled with whitespaces."); }
                string payload = "{\"identifier\": \"" + identifier + "\"}";
                await Api.TwitchPutAsync("/user/vhs", ApiVersion.v5, payload, accessToken: authToken).ConfigureAwait(false);
            }
            #endregion
            #region CheckUserConnectionToViewerHeartbeatService
            public async Task<Models.v5.ViewerHeartbeatService.VHSConnectionCheck> CheckUserConnectionToViewerHeartbeatServiceAsync(string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.User_Read, authToken);
                return await Api.TwitchGetGenericAsync<Models.v5.ViewerHeartbeatService.VHSConnectionCheck>("/user/vhs", ApiVersion.v5, accessToken: authToken).ConfigureAwait(false);
            }
            #endregion
            #region DeleteUserConnectionToViewerHeartbeatService

            public async Task DeleteUserConnectionToViewerHeartbeatServicechStreamsAsync(string authToken = null)
            {
                Api.Settings.DynamicScopeValidation(AuthScopes.Viewing_Activity_Read, authToken);
                await Api.TwitchDeleteAsync("/user/vhs", ApiVersion.v5, accessToken: authToken).ConfigureAwait(false);
            }
            #endregion
            #endregion
        }

        public class Helix : ApiSection
        {
            public Helix(TwitchAPI api) : base(api)
            {
            }

            public async Task<Models.Helix.Users.GetUsers.GetUsersResponse> GetUsersAsync(List<string> ids = null, List<string> logins = null, string accessToken = null)
            {
                var getParams = new List<KeyValuePair<string, string>>();
                if (ids != null && ids.Count > 0)
                {
                    foreach (var id in ids)
                        getParams.Add(new KeyValuePair<string, string>("id", id));
                }
                if (logins != null && logins.Count > 0)
                {
                    foreach (var login in logins)
                        getParams.Add(new KeyValuePair<string, string>("login", login));
                }
                return await Api.TwitchGetGenericAsync<Models.Helix.Users.GetUsers.GetUsersResponse>("/users", ApiVersion.Helix, getParams, accessToken).ConfigureAwait(false);
            }

            public async Task<Models.Helix.Users.GetUsersFollows.GetUsersFollowsResponse> GetUsersFollowsAsync(string after = null, string before = null, int first = 20, string fromId = null, string toId = null)
            {
                var getParams = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("first", first.ToString()) };
                if (after != null)
                    getParams.Add(new KeyValuePair<string, string>("after", after));
                if (before != null)
                    getParams.Add(new KeyValuePair<string, string>("before", before));
                if (fromId != null)
                    getParams.Add(new KeyValuePair<string, string>("from_id", fromId));
                if (toId != null)
                    getParams.Add(new KeyValuePair<string, string>("to_id", toId));
                
                return await Api.TwitchGetGenericAsync<Models.Helix.Users.GetUsersFollows.GetUsersFollowsResponse>("/users/follows", ApiVersion.Helix, getParams).ConfigureAwait(false);
            }

            public async Task PutUsersAsync(string description, string accessToken = null)
            {
                var getParams = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("description", description) };
                await Api.TwitchPutAsync("/users", ApiVersion.Helix, null, getParams, accessToken).ConfigureAwait(false);
            }
        }
    }
}
