using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Streams;
using TwitchLib.Api.Helix.Models.StreamsMetadata;

namespace TwitchLib.Api.Helix
{
    public class Streams : ApiBase
    {
        public Streams(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        public Task<GetStreamsResponse> GetStreamsAsync(string after = null, List<string> communityIds = null, int first = 20, List<string> gameIds = null, List<string> languages = null, string type = "all", List<string> userIds = null, List<string> userLogins = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("first", first.ToString()),
                    new KeyValuePair<string, string>("type", type)
                };
            if (after != null)
                getParams.Add(new KeyValuePair<string, string>("after", after));
            if (communityIds != null && communityIds.Count > 0)
            {
                foreach (var communityId in communityIds)
                    getParams.Add(new KeyValuePair<string, string>("community_id", communityId));
            }

            if (gameIds != null && gameIds.Count > 0)
            {
                foreach (var gameId in gameIds)
                    getParams.Add(new KeyValuePair<string, string>("game_id", gameId));
            }

            if (languages != null && languages.Count > 0)
            {
                foreach (var language in languages)
                    getParams.Add(new KeyValuePair<string, string>("language", language));
            }

            if (userIds != null && userIds.Count > 0)
            {
                foreach (var userId in userIds)
                    getParams.Add(new KeyValuePair<string, string>("user_id", userId));
            }

            if (userLogins != null && userLogins.Count > 0)
            {
                foreach (var userLogin in userLogins)
                    getParams.Add(new KeyValuePair<string, string>("user_login", userLogin));
            }

            return TwitchGetGenericAsync<GetStreamsResponse>($"/streams", ApiVersion.Helix, getParams);
        }

        public Task<GetStreamsMetadataResponse> GetStreamsMetadataAsync(string after = null, List<string> communityIds = null, int first = 20, List<string> gameIds = null, List<string> languages = null, string type = "all", List<string> userIds = null, List<string> userLogins = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("first", first.ToString()),
                    new KeyValuePair<string, string>("type", type)
                };
            if (after != null)
                getParams.Add(new KeyValuePair<string, string>("after", after));
            if (communityIds != null && communityIds.Count > 0)
            {
                foreach (var communityId in communityIds)
                    getParams.Add(new KeyValuePair<string, string>("community_id", communityId));
            }

            if (gameIds != null && gameIds.Count > 0)
            {
                foreach (var gameId in gameIds)
                    getParams.Add(new KeyValuePair<string, string>("game_id", gameId));
            }

            if (languages != null && languages.Count > 0)
            {
                foreach (var language in languages)
                    getParams.Add(new KeyValuePair<string, string>("language", language));
            }

            if (userIds != null && userIds.Count > 0)
            {
                foreach (var userId in userIds)
                    getParams.Add(new KeyValuePair<string, string>("user_id", userId));
            }

            if (userLogins != null && userLogins.Count > 0)
            {
                foreach (var userLogin in userLogins)
                    getParams.Add(new KeyValuePair<string, string>("user_login", userLogin));
            }

            return TwitchGetGenericAsync<GetStreamsMetadataResponse>("/streams/metadata", ApiVersion.Helix, getParams);
        }
    }

}