using System;
using System.Collections.Generic;
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
    [Obsolete("This is a v5 class, please use a Helix class. All v5 calls will be turned off on February 28, 2022. Details: https://blog.twitch.tv/en/2021/07/15/legacy-twitch-api-v5-shutdown-details-and-timeline/ ")]
    public class Users : ApiBase
    {
        public Users(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        #region GetUsersByName
        [Obsolete("This is a v5 method, please use a Helix method. All v5 methods will be turned off on February 28, 2022. Details: https://blog.twitch.tv/en/2021/07/15/legacy-twitch-api-v5-shutdown-details-and-timeline/ ")]
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
        [Obsolete("This is a v5 method, please use a Helix method. All v5 methods will be turned off on February 28, 2022. Details: https://blog.twitch.tv/en/2021/07/15/legacy-twitch-api-v5-shutdown-details-and-timeline/ ")]
        public Task<UserAuthed> GetUserAsync(string authToken = null)
        {
            return TwitchGetGenericAsync<UserAuthed>("/user", ApiVersion.V5, accessToken: authToken);
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
        [Obsolete("This is a v5 method, please use a Helix method. All v5 methods will be turned off on February 28, 2022. Details: https://blog.twitch.tv/en/2021/07/15/legacy-twitch-api-v5-shutdown-details-and-timeline/ ")]
        public Task<Models.Users.Users> GetUserByNameAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
                throw new BadParameterException("The username is not valid.");

            var getParams = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("login", username) };
            return TwitchGetGenericAsync<Models.Users.Users>("/users", ApiVersion.V5, getParams);
        }

        #endregion

        #region GetUserEmotes
        [Obsolete("This is a v5 method, please use a Helix method. All v5 methods will be turned off on February 28, 2022. Details: https://blog.twitch.tv/en/2021/07/15/legacy-twitch-api-v5-shutdown-details-and-timeline/ ")]
        public Task<UserEmotes> GetUserEmotesAsync(string userId, string authToken = null)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            return TwitchGetGenericAsync<UserEmotes>($"/users/{userId}/emotes", ApiVersion.V5, accessToken: authToken);
        }

        #endregion

        #region CheckUserSubscriptionByChannel
        [Obsolete("This is a v5 method, please use a Helix method. All v5 methods will be turned off on February 28, 2022. Details: https://blog.twitch.tv/en/2021/07/15/legacy-twitch-api-v5-shutdown-details-and-timeline/ ")]
        public Task<Subscription> CheckUserSubscriptionByChannelAsync(string userId, string channelId, string authToken = null)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            if (string.IsNullOrWhiteSpace(channelId))
                throw new BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            return TwitchGetGenericAsync<Subscription>($"/users/{userId}/subscriptions/{channelId}", ApiVersion.V5, accessToken: authToken);
        }

        #endregion

        #region GetUserFollows
        [Obsolete("This is a v5 method, please use a Helix method. All v5 methods will be turned off on February 28, 2022. Details: https://blog.twitch.tv/en/2021/07/15/legacy-twitch-api-v5-shutdown-details-and-timeline/ ")]
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
        [Obsolete("This is a v5 method, please use a Helix method. All v5 methods will be turned off on February 28, 2022. Details: https://blog.twitch.tv/en/2021/07/15/legacy-twitch-api-v5-shutdown-details-and-timeline/ ")]
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
        [Obsolete("This is a v5 method, please use a Helix method. All v5 methods will be turned off on February 28, 2022. Details: https://blog.twitch.tv/en/2021/07/15/legacy-twitch-api-v5-shutdown-details-and-timeline/ ")]
        public async Task<bool> UserFollowsChannelAsync(string userId, string channelId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            if (string.IsNullOrWhiteSpace(channelId))
                throw new BadParameterException("The channel id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            try
            {
                await TwitchGetGenericAsync<UserFollow>($"/users/{userId}/follows/channels/{channelId}", ApiVersion.V5);
                return true;
            }
            catch (BadResourceException)
            {
                return false;
            }
        }

        #endregion

        #region GetUserBlockList
        [Obsolete("This is a v5 method, please use a Helix method. All v5 methods will be turned off on February 28, 2022. Details: https://blog.twitch.tv/en/2021/07/15/legacy-twitch-api-v5-shutdown-details-and-timeline/ ")]
        public Task<UserBlocks> GetUserBlockListAsync(string userId, int? limit = null, int? offset = null, string authToken = null)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new BadParameterException("The user id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            var getParams = new List<KeyValuePair<string, string>>();
            if (limit.HasValue)
                getParams.Add(new KeyValuePair<string, string>("limit", limit.Value.ToString()));
            if (offset.HasValue)
                getParams.Add(new KeyValuePair<string, string>("offset", offset.Value.ToString()));

            return TwitchGetGenericAsync<UserBlocks>($"/users/{userId}/blocks", ApiVersion.V5, getParams, authToken);
        }

        #endregion

        #region BlockUser
        [Obsolete("This is a v5 method, please use a Helix method. All v5 methods will be turned off on February 28, 2022. Details: https://blog.twitch.tv/en/2021/07/15/legacy-twitch-api-v5-shutdown-details-and-timeline/ ")]
        public Task<UserBlock> BlockUserAsync(string sourceUserId, string targetUserId, string authToken = null)
        {
            if (string.IsNullOrWhiteSpace(sourceUserId))
                throw new BadParameterException("The source user id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            if (string.IsNullOrWhiteSpace(targetUserId))
                throw new BadParameterException("The target user id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            return TwitchPutGenericAsync<UserBlock>($"/users/{sourceUserId}/blocks/{targetUserId}", ApiVersion.V5, null, accessToken: authToken);
        }

        #endregion

        #region UnblockUser
        [Obsolete("This is a v5 method, please use a Helix method. All v5 methods will be turned off on February 28, 2022. Details: https://blog.twitch.tv/en/2021/07/15/legacy-twitch-api-v5-shutdown-details-and-timeline/ ")]
        public Task UnblockUserAsync(string sourceUserId, string targetUserId, string authToken = null)
        {
            if (string.IsNullOrWhiteSpace(sourceUserId))
                throw new BadParameterException("The source user id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            if (string.IsNullOrWhiteSpace(targetUserId))
                throw new BadParameterException("The target user id is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            return TwitchDeleteAsync($"/users/{sourceUserId}/blocks/{targetUserId}", ApiVersion.V5, accessToken: authToken);
        }

        #endregion

        #region ViewerHeartbeatService

        #region CreateUserConnectionToViewerHeartbeatService
        [Obsolete("This is a v5 method, please use a Helix method. All v5 methods will be turned off on February 28, 2022. Details: https://blog.twitch.tv/en/2021/07/15/legacy-twitch-api-v5-shutdown-details-and-timeline/ ")]
        public Task CreateUserConnectionToViewerHeartbeatServiceAsync(string identifier, string authToken = null)
        {
            if (string.IsNullOrWhiteSpace(identifier))
                throw new BadParameterException("The identifier is not valid. It is not allowed to be null, empty or filled with whitespaces.");

            var payload = "{\"identifier\": \"" + identifier + "\"}";
            return TwitchPutAsync("/user/vhs", ApiVersion.V5, payload, accessToken: authToken);
        }

        #endregion

        #region CheckUserConnectionToViewerHeartbeatService
        [Obsolete("This is a v5 method, please use a Helix method. All v5 methods will be turned off on February 28, 2022. Details: https://blog.twitch.tv/en/2021/07/15/legacy-twitch-api-v5-shutdown-details-and-timeline/ ")]
        public Task<VHSConnectionCheck> CheckUserConnectionToViewerHeartbeatServiceAsync(string authToken = null)
        {

            return TwitchGetGenericAsync<VHSConnectionCheck>("/user/vhs", ApiVersion.V5, accessToken: authToken);
        }

        #endregion

        #region DeleteUserConnectionToViewerHeartbeatService
        [Obsolete("This is a v5 method, please use a Helix method. All v5 methods will be turned off on February 28, 2022. Details: https://blog.twitch.tv/en/2021/07/15/legacy-twitch-api-v5-shutdown-details-and-timeline/ ")]
        public Task DeleteUserConnectionToViewerHeartbeatServicechStreamsAsync(string authToken = null)
        {

            return TwitchDeleteAsync("/user/vhs", ApiVersion.V5, accessToken: authToken);
        }

        #endregion

        #endregion
    }

}
