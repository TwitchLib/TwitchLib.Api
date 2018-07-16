using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Core.Interfaces.Clips;
using TwitchLib.Api.Core.Models.Undocumented.ChannelExtensionData;
using TwitchLib.Api.Core.Models.Undocumented.ChannelPanels;
using TwitchLib.Api.Core.Models.Undocumented.ChatProperties;
using TwitchLib.Api.Core.Models.Undocumented.Chatters;
using TwitchLib.Api.Core.Models.Undocumented.ChatUser;
using TwitchLib.Api.Core.Models.Undocumented.ClipChat;
using TwitchLib.Api.Core.Models.Undocumented.Comments;
using TwitchLib.Api.Core.Models.Undocumented.CSMaps;
using TwitchLib.Api.Core.Models.Undocumented.CSStreams;
using TwitchLib.Api.Core.Models.Undocumented.Hosting;
using TwitchLib.Api.Core.Models.Undocumented.RecentEvents;
using TwitchLib.Api.Core.Models.Undocumented.RecentMessages;
using TwitchLib.Api.Core.Models.Undocumented.TwitchPrimeOffers;

namespace TwitchLib.Api.Core.Root
{
    public class Root : ApiBase
    {
        public Root(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        #region GetRoot

        public new Task<Models.Root.Root> GetRootAsync(string authToken = null, string clientId = null)
        {
            return TwitchGetGenericAsync<Models.Root.Root>("", ApiVersion.v5, accessToken: authToken, clientId: clientId);
        }

        #endregion

    }
}