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
    /// <summary>
    /// Soundtrack related APIs
    /// </summary>
    public class Soundtrack : ApiBase
    {
        public Soundtrack(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        /// <summary>
        /// Gets the Soundtrack track that the broadcaster is playing.
        /// </summary>
        /// <param name="broadcasterId">The ID of the broadcaster that’s playing a Soundtrack track.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetCurrentTrackResponse"></returns>
        /// <exception cref="BadParameterException"></exception>
        public Task<GetCurrentTrackResponse> GetCurrentTrackAsync(string broadcasterId, string accessToken = null)
        {
            if (string.IsNullOrWhiteSpace(broadcasterId))
                throw new BadParameterException("'broadcasterId' must be set");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("broadcaster_id", broadcasterId)
            };

            return TwitchGetGenericAsync<GetCurrentTrackResponse>("/soundtrack/current_track", ApiVersion.Helix, getParams, accessToken);
        }

        /// <summary>
        /// Gets the tracks of a Soundtrack playlist.
        /// </summary>
        /// <param name="id">[Required] The ID of the Soundtrack playlist to get.</param>
        /// <param name="first">The maximum number of tracks to return for this playlist in the response. Maximum: 50. Default: 20.</param>
        /// <param name="after">The cursor used to get the next page of tracks for this playlist.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetPlaylistResponse">The tracks of a Soundtrack playlist</returns>
        public Task<GetPlaylistResponse> GetPlaylistAsync(string id, int first = 20, string after = null, string accessToken = null)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new BadParameterException("'id' must be set");

            if (first < 1 || first > 50)
                throw new BadParameterException("'first' must be value of 1 - 50");

            var getParams = new List<KeyValuePair<string, string>>
            {
                    new KeyValuePair<string, string>("id", id),
                    new KeyValuePair<string, string>("first", first.ToString())
            };

            if (!string.IsNullOrWhiteSpace(after))
                getParams.Add(new KeyValuePair<string, string>("after", after));

            return TwitchGetGenericAsync<GetPlaylistResponse>("/soundtrack/playlist", ApiVersion.Helix, getParams, accessToken);
        }

        /// <summary>
        /// Gets a list of Soundtrack playlists.
        /// </summary>
        /// <param name="id">The ID of the Soundtrack playlist to get. Specify an ID only if you want to get a single playlist instead of all playlists.</param>
        /// <param name="first">The maximum number of playlists to return in the response. Maximum: 50. Default: 20.</param>
        /// <param name="after">The cursor used to get the next page of playlists.</param>
        /// <param name="accessToken">optional access token to override the use of the stored one in the TwitchAPI instance</param>
        /// <returns cref="GetPlaylistsResponse"></returns>
        public Task<GetPlaylistsResponse> GetPlaylistsAsync(string id = null, int first = 20, string after = null, string accessToken = null)
        {
            if (first < 1 || first > 50)
                throw new BadParameterException("'first' must be value of 1 - 50");

            var getParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("first", first.ToString())
            };

            if (!string.IsNullOrWhiteSpace(after))
                getParams.Add(new KeyValuePair<string, string>("after", after));

            if (!string.IsNullOrWhiteSpace(id))
                getParams.Add(new KeyValuePair<string, string>("id", id));

            return TwitchGetGenericAsync<GetPlaylistsResponse>("/soundtrack/playlists", ApiVersion.Helix, getParams, accessToken);
        }
    }
}