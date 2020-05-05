﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.V5.Models.Subscriptions;
using TwitchLib.Api.V5.Models.Users;
using TwitchLib.Api.V5.Models.ViewerHeartbeatService;
using User = TwitchLib.Api.V5.Models.Users.User;

namespace TwitchLib.Api.V5
{
    public class Users : ApiBase
    {
        public Users(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }
        
        #region GetUsersByName

        public Task<Models.Users.Users> GetUsersByNameAsync(List<string> usernames)
        {
            if (usernames == null || usernames.Count == 0)
                throw new BadParameterException("The username list is not valid. It is not allowed to be null or empty.");

            var getParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("login", string.Join(",", usernames))
                };
            return TwitchGetGenericAsync<Models.Users.Users>("/users", ApiVersion.V5, getParams);
        }

        #endregion

        #region GetUser

        public async Task<UserAuthed> GetUserAsync(string authToken = null)
        {
            await DynamicScopeValidationAsync(AuthScopes.User_Read, authToken).ConfigureAwait(false);

            return await TwitchGetGenericAsync<UserAuthed>("/user", ApiVersion.V5, accessToken: authToken).ConfigureAwait(false);
        }

        #endregion

        #region GetUserByID

        public Task<User> GetUserByIDAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            return TwitchGetGenericAsync<User>($"/users/{userId}", ApiVersion.V5);
        }

        #endregion

        #region GetUserByName

        public Task<Models.Users.Users> GetUserByNameAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
                throw new BadParameterException("The username is not valid.");

            var getParams = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("login", username) };
            return TwitchGetGenericAsync<Models.Users.Users>("/users", ApiVersion.V5, getParams);
        }

        #endregion

        #region GetUserEmotes

        public async Task<UserEmotes> GetUserEmotesAsync(string userId, string authToken = null)
        {
            await DynamicScopeValidationAsync(AuthScopes.User_Subscriptions, authToken).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(userId))
                throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            return await TwitchGetGenericAsync<UserEmotes>($"/users/{userId}/emotes", ApiVersion.V5, accessToken: authToken).ConfigureAwait(false);
        }

        #endregion

        #region CheckUserSubscriptionByChannel

        public async Task<Subscription> CheckUserSubscriptionByChannelAsync(string userId, string channelId, string authToken = null)
        {
            await DynamicScopeValidationAsync(AuthScopes.User_Subscriptions, authToken).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(userId))
                throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            if (string.IsNullOrWhiteSpace(channelId))
                throw new BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            return await TwitchGetGenericAsync<Subscription>($"/users/{userId}/subscriptions/{channelId}", ApiVersion.V5, accessToken: authToken).ConfigureAwait(false);
        }

        #endregion

        #region GetUserFollows

        public Task<UserFollows> GetUserFollowsAsync(string userId, int? limit = null, int? offset = null, string direction = null, string sortby = null)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            var getParams = new List<KeyValuePair<string, string>>();
            if (limit.HasValue)
                getParams.Add(new KeyValuePair<string, string>("limit", limit.Value.ToString()));
            if (offset.HasValue)
                getParams.Add(new KeyValuePair<string, string>("offset", offset.Value.ToString()));
            if (!string.IsNullOrEmpty(direction) && (direction == "asc" || direction == "desc"))
                getParams.Add(new KeyValuePair<string, string>("direction", direction));
            if (!string.IsNullOrEmpty(sortby) && (sortby == "created_at" || sortby == "last_broadcast" || sortby == "login"))
                getParams.Add(new KeyValuePair<string, string>("sortby", sortby));

            return TwitchGetGenericAsync<UserFollows>($"/users/{userId}/follows/channels", ApiVersion.V5, getParams);
        }

        #endregion

        #region CheckUserFollowsByChannel

        public Task<UserFollow> CheckUserFollowsByChannelAsync(string userId, string channelId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            if (string.IsNullOrWhiteSpace(channelId))
                throw new BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            return TwitchGetGenericAsync<UserFollow>($"/users/{userId}/follows/channels/{channelId}", ApiVersion.V5);
        }

        #endregion

        #region UserFollowsChannel

        public async Task<bool> UserFollowsChannelAsync(string userId, string channelId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            if (string.IsNullOrWhiteSpace(channelId))
                throw new BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            try
            {
                await TwitchGetGenericAsync<UserFollow>($"/users/{userId}/follows/channels/{channelId}", ApiVersion.V5).ConfigureAwait(false);
                return true;
            }
            catch (BadResourceException)
            {
                return false;
            }
        }

        #endregion

        #region FollowChannel

        public async Task<UserFollow> FollowChannelAsync(string userId, string channelId, bool? notifications = null, string authToken = null)
        {
            await DynamicScopeValidationAsync(AuthScopes.User_Follows_Edit, authToken).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(userId))
                throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            if (string.IsNullOrWhiteSpace(channelId))
                throw new BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            var optionalRequestBody = notifications.HasValue ? "{\"notifications\": " + notifications.Value.ToString().ToLower() + "}" : null;
            return await TwitchPutGenericAsync<UserFollow>($"/users/{userId}/follows/channels/{channelId}", ApiVersion.V5, optionalRequestBody, accessToken: authToken).ConfigureAwait(false);
        }

        #endregion

        #region UnfollowChannel

        public async Task UnfollowChannelAsync(string userId, string channelId, string authToken = null)
        {
            await DynamicScopeValidationAsync(AuthScopes.User_Follows_Edit, authToken).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(userId))
                throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            if (string.IsNullOrWhiteSpace(channelId))
                throw new BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            await TwitchDeleteAsync($"/users/{userId}/follows/channels/{channelId}", ApiVersion.V5, accessToken: authToken).ConfigureAwait(false);
        }

        #endregion

        #region GetUserBlockList

        public async Task<UserBlocks> GetUserBlockListAsync(string userId, int? limit = null, int? offset = null, string authToken = null)
        {
            await DynamicScopeValidationAsync(AuthScopes.User_Blocks_Read, authToken).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(userId))
                throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            var getParams = new List<KeyValuePair<string, string>>();
            if (limit.HasValue)
                getParams.Add(new KeyValuePair<string, string>("limit", limit.Value.ToString()));
            if (offset.HasValue)
                getParams.Add(new KeyValuePair<string, string>("offset", offset.Value.ToString()));

            return await TwitchGetGenericAsync<UserBlocks>($"/users/{userId}/blocks", ApiVersion.V5, getParams, authToken).ConfigureAwait(false);
        }

        #endregion

        #region BlockUser

        public async Task<UserBlock> BlockUserAsync(string sourceUserId, string targetUserId, string authToken = null)
        {
            await DynamicScopeValidationAsync(AuthScopes.User_Blocks_Edit, authToken).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(sourceUserId))
                throw new BadParameterException("The source user id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            if (string.IsNullOrWhiteSpace(targetUserId))
                throw new BadParameterException("The target user id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            return await TwitchPutGenericAsync<UserBlock>($"/users/{sourceUserId}/blocks/{targetUserId}", ApiVersion.V5, null, accessToken: authToken).ConfigureAwait(false);
        }

        #endregion

        #region UnblockUser

        public async Task UnblockUserAsync(string sourceUserId, string targetUserId, string authToken = null)
        {
            await DynamicScopeValidationAsync(AuthScopes.User_Blocks_Edit, authToken).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(sourceUserId))
                throw new BadParameterException("The source user id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            if (string.IsNullOrWhiteSpace(targetUserId))
                throw new BadParameterException("The target user id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            await TwitchDeleteAsync($"/users/{sourceUserId}/blocks/{targetUserId}", ApiVersion.V5, accessToken: authToken).ConfigureAwait(false);
        }

        #endregion

        #region ViewerHeartbeatService

        #region CreateUserConnectionToViewerHeartbeatService

        public async Task CreateUserConnectionToViewerHeartbeatServiceAsync(string identifier, string authToken = null)
        {
            await DynamicScopeValidationAsync(AuthScopes.Viewing_Activity_Read, authToken).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(identifier))
                throw new BadParameterException("The identifier is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            var payload = "{\"identifier\": \"" + identifier + "\"}";
            await TwitchPutAsync("/user/vhs", ApiVersion.V5, payload, accessToken: authToken).ConfigureAwait(false);
        }

        #endregion

        #region CheckUserConnectionToViewerHeartbeatService

        public async Task<VHSConnectionCheck> CheckUserConnectionToViewerHeartbeatServiceAsync(string authToken = null)
        {
            await DynamicScopeValidationAsync(AuthScopes.User_Read, authToken).ConfigureAwait(false);

            return await TwitchGetGenericAsync<VHSConnectionCheck>("/user/vhs", ApiVersion.V5, accessToken: authToken).ConfigureAwait(false);
        }

        #endregion

        #region DeleteUserConnectionToViewerHeartbeatService

        public async Task DeleteUserConnectionToViewerHeartbeatServicechStreamsAsync(string authToken = null)
        {
            await DynamicScopeValidationAsync(AuthScopes.Viewing_Activity_Read, authToken).ConfigureAwait(false);

            await TwitchDeleteAsync("/user/vhs", ApiVersion.V5, accessToken: authToken).ConfigureAwait(false);
        }

        #endregion

        #endregion
    }

}
