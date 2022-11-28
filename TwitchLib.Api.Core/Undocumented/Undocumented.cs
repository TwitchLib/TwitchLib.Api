using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Core.Models.Undocumented.Chatters;

namespace TwitchLib.Api.Core.Undocumented
{
    /// <summary>
    /// Undocumented Endpoints of the Twitch API, that generally are not for Third Party use and may break at any time without prior warning.
    /// <para>So its best to not rely on them and use official and documented Third Party APIs if possible!</para>
    /// </summary>
    public class Undocumented : ApiBase
    {
        public Undocumented(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        #region GetChatters

        /// <summary>
        /// Gets a list of chatters of a channel
        /// </summary>
        /// <param name="channelName">channel login/channel name to get the chatters list for</param>
        /// <returns>List of Chatters, grouped by UserType</returns>
        [Obsolete("Please use the new official Helix GetChatters Endpoint (api.Helix.Chat.GetChattersAsync) instead of this undocumented and unsupported endpoint.")]
        public async Task<List<ChatterFormatted>> GetChattersAsync(string channelName)
        {
            var resp = await GetGenericAsync<ChattersResponse>($"https://tmi.twitch.tv/group/user/{channelName.ToLower()}/chatters");

            var chatters = resp.Chatters.Staff.Select(chatter => new ChatterFormatted(chatter, UserType.Staff)).ToList();
            chatters.AddRange(resp.Chatters.Admins.Select(chatter => new ChatterFormatted(chatter, UserType.Admin)));
            chatters.AddRange(resp.Chatters.GlobalMods.Select(chatter => new ChatterFormatted(chatter, UserType.GlobalModerator)));
            chatters.AddRange(resp.Chatters.Moderators.Select(chatter => new ChatterFormatted(chatter, UserType.Moderator)));
            chatters.AddRange(resp.Chatters.Viewers.Select(chatter => new ChatterFormatted(chatter, UserType.Viewer)));
            chatters.AddRange(resp.Chatters.VIP.Select(chatter => new ChatterFormatted(chatter, UserType.VIP)));

            foreach (var chatter in chatters.Where(chatter => string.Equals(chatter.Username, channelName, StringComparison.InvariantCultureIgnoreCase)))
                chatter.UserType = UserType.Broadcaster;

            return chatters;
        }

        #endregion

        #region IsUsernameAvailable

        /// <summary>
        /// Checks if a given username is available or already in use
        /// </summary>
        /// <param name="username">username to check</param>
        /// <returns>true: if username is available; false: if username is already taken</returns>
        /// <exception cref="BadResourceException"></exception>
        public async Task<bool> IsUsernameAvailableAsync(string username)
        {
            var getParams = new List<KeyValuePair<string, string>> {new KeyValuePair<string, string>("users_service", "true")};
            var resp = await RequestReturnResponseCode($"https://passport.twitch.tv/usernames/{username}", "HEAD", getParams).ConfigureAwait(false);
            switch (resp)
            {
                case 200:
                    return false;
                case 204:
                    return true;
                default:
                    throw new BadResourceException("Unexpected response from resource. Expecting response code 200 or 204, received: " + resp, null);
            }
        }

        #endregion
    }
}