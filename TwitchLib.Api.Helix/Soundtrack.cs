using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Soundtrack.GetCurrentTrack;
using TwitchLib.Api.Helix.Models.Soundtrack.GetPlaylist;
using TwitchLib.Api.Helix.Models.Soundtrack.GetPlaylists;

namespace TwitchLib.Api.Helix
{
    public class Soundtrack : ApiBase
    {
        public Soundtrack(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        public Task<GetCurrentTrackResponse> GetCurrentTrackAsync(string broadcasterId, string accessToken = null)
        {
            if (string.IsNullOrEmpty(broadcasterId))
                throw new BadParameterException("'broadcasterId' must be set");

            var getParams = new List<KeyValuePair<string, string>>
            {
                    new KeyValuePair<string, string>("broadcaster_id", broadcasterId)
            };

            return TwitchGetGenericAsync<GetCurrentTrackResponse>("/soundtrack/current_track", ApiVersion.Helix, getParams, accessToken);
        }

        public Task<GetPlaylistResponse> GetPlaylistAsync(string id, string accessToken = null)
        {
            if (string.IsNullOrEmpty(id))
                throw new BadParameterException("'id' must be set");

            var getParams = new List<KeyValuePair<string, string>>
            {
                    new KeyValuePair<string, string>("id", id)
            };

            return TwitchGetGenericAsync<GetPlaylistResponse>("/soundtrack/playlist", ApiVersion.Helix, getParams, accessToken);
        }

        public Task<GetPlaylistsResponse> GetPlaylistsAsync(string accessToken = null)
        {
            return TwitchGetGenericAsync<GetPlaylistsResponse>("/soundtrack/playlists", ApiVersion.Helix, accessToken: accessToken);
        }
    }
}
