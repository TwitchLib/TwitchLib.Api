using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.ChannelPoints.CreateCustomReward;
using TwitchLib.Api.Helix.Models.ChannelPoints.GetCustomReward;
using TwitchLib.Api.Helix.Models.ChannelPoints.GetCustomRewardRedemption;
using TwitchLib.Api.Helix.Models.ChannelPoints.UpdateCustomReward;
using TwitchLib.Api.Helix.Models.ChannelPoints.UpdateCustomRewardRedemptionStatus;
using TwitchLib.Api.Helix.Models.ChannelPoints.UpdateRedemptionStatus;

namespace TwitchLib.Api.Helix
{
    /// <summary>
    /// Channel Points related APIs
    /// </summary>
    public class ChannelPoints : ApiBase
    {
        public ChannelPoints(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        #region CreateCustomRewards
        /// <summary>
        /// Creates a Custom Reward on a channel.
        /// <para>Required scope: channel:manage:redemptions</para>
        /// <para>The maximum number of custom rewards per channel is 50, which includes both enabled and disabled rewards.</para>
        /// </summary>
        /// <param name="broadcasterId">
        /// Broadcaster Id to create the reward for.
        /// <para>Provided broadcaster_id must match the user_id in the user OAuth token.</para>
        /// </param>
        /// <param name="request" cref="CreateCustomRewardsRequest"></param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="CreateCustomRewardsResponse"></returns>
        public Task<CreateCustomRewardsResponse> CreateCustomRewardsAsync(string broadcasterId, CreateCustomRewardsRequest request, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId)
            };

            return TwitchPostGenericAsync<CreateCustomRewardsResponse>("/channel_points/custom_rewards", ApiVersion.Helix, JsonConvert.SerializeObject(request), getParams, accessToken);
        }
        #endregion

        #region DeleteCustomReward
        /// <summary>
        /// Deletes a Custom Reward on a channel.
        /// <para>Required scope: channel:manage:redemptions</para>
        /// <para>The Custom Reward specified by id must have been created by the ClientId attached to the OAuth token in order to be deleted.</para>
        /// <para>Any UNFULFILLED Custom Reward Redemptions of the deleted Custom Reward will be updated to the FULFILLED status.</para>
        /// </summary>
        /// <param name="broadcasterId">
        /// Broadcaster Id to delete the reward from.
        /// <para>Provided broadcaster_id must match the user_id in the user OAuth token.</para>
        /// </param>
        /// <param name="rewardId">
        /// ID of the Custom Reward to delete.
        /// <para>Must match a Custom Reward on broadcaster_id’s channel.</para>
        /// </param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns></returns>
        public Task DeleteCustomRewardAsync(string broadcasterId, string rewardId, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("id", rewardId)
            };

            return TwitchDeleteAsync("/channel_points/custom_rewards", ApiVersion.Helix, getParams, accessToken);
        }
        #endregion

        #region GetCustomReward
        /// <summary>
        /// Returns a list of Custom Reward objects for the Custom Rewards on a channel.
        /// <para>Required scope: channel:read:redemptions</para>
        /// </summary>
        /// <param name="broadcasterId">
        /// Broadcaster Id to get the rewards for.
        /// <para>Provided broadcaster_id must match the user_id in the user OAuth token.</para>
        /// </param>
        /// <param name="rewardIds">When used, this parameter filters the results and only returns reward objects for the Custom Rewards with matching ID. Maximum: 50</param>
        /// <param name="onlyManageableRewards">When set to true, only returns custom rewards that the calling ClientId can manage. Default: false.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetCustomRewardsResponse"></returns>
        public Task<GetCustomRewardsResponse> GetCustomRewardAsync(string broadcasterId, List<string> rewardIds = null, bool onlyManageableRewards = false, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("only_manageable_rewards", onlyManageableRewards.ToString().ToLower())
            };

            if (rewardIds != null && rewardIds.Count > 0)
            {
                getParams.AddRange(rewardIds.Select(rewardId => new KeyValuePair<string, string>("id", rewardId)));
            }

            return TwitchGetGenericAsync<GetCustomRewardsResponse>("/channel_points/custom_rewards", ApiVersion.Helix, getParams, accessToken);
        }
        #endregion

        #region UpdateCustomReward
        /// <summary>
        /// Updates a Custom Reward created on a channel.
        /// <para>The Custom Reward specified by id must have been created by the ClientId attached to the user OAuth token.</para>
        /// <para>Required scope: channel:manage:redemptions</para>
        /// </summary>
        /// <param name="broadcasterId">
        /// Broadcaster Id to update the reward for.
        /// <para>Provided broadcaster_id must match the user_id in the user OAuth token.</para>
        /// </param>
        /// <param name="rewardId">ID of the Custom Reward to update. Must match a Custom Reward on the channel of the Broadcaster Id.</param>
        /// <param name="request" cref="UpdateCustomRewardRequest"></param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="UpdateCustomRewardResponse"></returns>
        public Task<UpdateCustomRewardResponse> UpdateCustomRewardAsync(string broadcasterId, string rewardId, UpdateCustomRewardRequest request, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("id", rewardId)
            };

            return TwitchPatchGenericAsync<UpdateCustomRewardResponse>("/channel_points/custom_rewards", ApiVersion.Helix, JsonConvert.SerializeObject(request), getParams, accessToken);
        }
        #endregion

        #region GetCustomRewardRedemption
        /// <summary>
        /// Returns Custom Reward Redemption objects for a Custom Reward on a channel that was created by the same ClientId.
        /// <para>Developers only have access to get and update redemptions for the rewards created programmatically by the same ClientId.</para>
        /// <para>Required scope: channel:read:redemptions</para>
        /// </summary>
        /// <param name="broadcasterId">
        /// Broadcaster Id to get the reward redemptions for.
        /// <para>Provided broadcaster_id must match the user_id in the user OAuth token.</para>
        /// </param>
        /// <param name="rewardId">When ID is not provided, this parameter returns paginated Custom Reward Redemption objects for redemptions of the Custom Reward with ID RewardId.</param>
        /// <param name="redemptionIds">When used, this param filters the results and only returns Custom Reward Redemption objects for the redemptions with matching IDs. Maximum: 50</param>
        /// <param name="status">
        /// When id is not provided, this param is required and filters the paginated Custom Reward Redemption objects for redemptions with the matching status.
        /// <para>Can be one of UNFULFILLED, FULFILLED or CANCELED</para>
        /// </param>
        /// <param name="sort">
        /// Sort order of redemptions returned when getting the paginated Custom Reward Redemption objects for a reward.
        /// <para>One of: OLDEST, NEWEST. Default: OLDEST.</para>
        /// </param>
        /// <param name="after">
        /// Cursor for forward pagination: tells the server where to start fetching the next set of results, in a multi-page response.
        /// <para>This applies only to queries without ID. If an ID is specified, it supersedes any cursor/offset combinations.</para>
        /// <para>The cursor value specified here is from the pagination response field of a prior query.</para>
        /// </param>
        /// <param name="first">Number of results to be returned when getting the paginated Custom Reward Redemption objects for a reward. Limit: 50. Default: 20.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetCustomRewardRedemptionResponse"></returns>
        public Task<GetCustomRewardRedemptionResponse> GetCustomRewardRedemptionAsync(string broadcasterId, string rewardId, List<string> redemptionIds = null, string status = null, string sort = null, string after = null, string first = null, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("reward_id", rewardId),
            };

            if (redemptionIds != null && redemptionIds.Count > 0)
            {
                getParams.AddRange(redemptionIds.Select(redemptionId => new KeyValuePair<string, string>("id", redemptionId)));
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                getParams.Add(new KeyValuePair<string, string>("status", status));
            }

            if(!string.IsNullOrWhiteSpace(sort))
            {
                getParams.Add(new KeyValuePair<string, string>("sort", sort));
            }

            if(!string.IsNullOrWhiteSpace(after))
            {
                getParams.Add(new KeyValuePair<string, string>("after", after));
            }

            if(!string.IsNullOrWhiteSpace(first))
            {
                getParams.Add(new KeyValuePair<string, string>("first", first));
            }

            return TwitchGetGenericAsync<GetCustomRewardRedemptionResponse>("/channel_points/custom_rewards/redemptions", ApiVersion.Helix, getParams, accessToken);
        }
        #endregion

        #region UpdateCustomRewardRedemption
        /// <summary>
        /// Updates a Custom Reward created on a channel.
        /// <para>The Custom Reward specified by id must have been created by the ClientId attached to the user OAuth token.</para>
        /// <para>Required scope: channel:manage:redemptions</para>
        /// </summary>
        /// <param name="broadcasterId">
        /// Broadcaster Id to update the reward redemptions status for.
        /// <para>Provided broadcaster_id must match the user_id in the user OAuth token.</para>
        /// </param>
        /// <param name="rewardId">ID of the Custom Reward the redemptions to be updated are for.</param>
        /// <param name="redemptionIds">IDs of the Custom Reward Redemptions to update, must match a Custom Reward Redemption on BroadcasterId’s channel. Maximum: 50.</param>
        /// <param name="request" cref="UpdateCustomRewardRedemptionStatusRequest"></param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="UpdateRedemptionStatusResponse"></returns>
        public Task<UpdateRedemptionStatusResponse> UpdateRedemptionStatusAsync(string broadcasterId, string rewardId, List<string> redemptionIds, UpdateCustomRewardRedemptionStatusRequest request, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("reward_id", rewardId)
            };

            getParams.AddRange(redemptionIds.Select(redemptionId => new KeyValuePair<string, string>("id", redemptionId)));

            return TwitchPatchGenericAsync<UpdateRedemptionStatusResponse>("/channel_points/custom_rewards/redemptions", ApiVersion.Helix, JsonConvert.SerializeObject(request), getParams, accessToken);
        }
        #endregion
    }
}
