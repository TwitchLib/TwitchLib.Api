using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
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
    public class ChannelPoints : ApiBase
    {
        public ChannelPoints(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        #region CreateCustomRewards
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
        public Task<GetCustomRewardRedemptionResponse> GetCustomRewardRedemptionAsync(string broadcasterId, string rewardId, string redemptionId = null, string status = null, string sort = null, string after = null, string first = null, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId),
                new KeyValuePair<string, string>("reward_id", rewardId),
            };

            if(!string.IsNullOrWhiteSpace(redemptionId))
            {
                getParams.Add(new KeyValuePair<string, string>("id", redemptionId));
            }

            if(!string.IsNullOrWhiteSpace(status))
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
