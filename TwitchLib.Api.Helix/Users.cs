using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Exceptions;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.Users;
using TwitchLib.Api.Helix.Models.Users.Internal;

namespace TwitchLib.Api.Helix
{
    public class Users : ApiBase
    {
        public Users(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http) : base(settings, rateLimiter, http)
        {
        }

        public Task<GetUsersResponse> GetUsersAsync(List<string> ids = null, List<string> logins = null, string accessToken = null)
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

            return TwitchGetGenericAsync<GetUsersResponse>("/users", ApiVersion.Helix, getParams, accessToken);
        }

        public Task<GetUsersFollowsResponse> GetUsersFollowsAsync(string after = null, string before = null, int first = 20, string fromId = null, string toId = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("first", first.ToString())
                };
            if (after != null)
                getParams.Add(new KeyValuePair<string, string>("after", after));
            if (before != null)
                getParams.Add(new KeyValuePair<string, string>("before", before));
            if (fromId != null)
                getParams.Add(new KeyValuePair<string, string>("from_id", fromId));
            if (toId != null)
                getParams.Add(new KeyValuePair<string, string>("to_id", toId));

            return TwitchGetGenericAsync<GetUsersFollowsResponse>("/users/follows", ApiVersion.Helix, getParams);
        }

        public Task PutUsersAsync(string description, string accessToken = null)
        {
            var getParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("description", description)
                };

            return TwitchPutAsync("/users", ApiVersion.Helix, null, getParams, accessToken);
        }

        public Task<GetUserExtensionsResponse> GetUserExtensionsAsync(string authToken = null)
        {
            return TwitchGetGenericAsync<GetUserExtensionsResponse>("/users/extensions/list", ApiVersion.Helix, accessToken: authToken);
        }

        public Task<GetUserActiveExtensionsResponse> GetUserActiveExtensionsAsync(string authToken = null)
        {
            return TwitchGetGenericAsync<GetUserActiveExtensionsResponse>("/users/extensions", ApiVersion.Helix, accessToken: authToken);
        }

        public Task<GetUserActiveExtensionsResponse> UpdateUserExtensionsAsync(IEnumerable<ExtensionSlot> userExtensionStates, string authToken = null)
        {
            DynamicScopeValidation(AuthScopes.Channel_Editor, authToken);

            var panels = new Dictionary<string, UserExtensionState>();
            var overlays = new Dictionary<string, UserExtensionState>();
            var components = new Dictionary<string, UserExtensionState>();

            foreach (var extension in userExtensionStates)
                switch (extension.Type)
                {
                    case ExtensionType.Component:
                        components.Add(extension.Slot, extension.UserExtensionState);
                        break;
                    case ExtensionType.Overlay:
                        overlays.Add(extension.Slot, extension.UserExtensionState);
                        break;
                    case ExtensionType.Panel:
                        panels.Add(extension.Slot, extension.UserExtensionState);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(ExtensionType));
                }

            var json = new JObject();
            var p = new UpdateUserExtensionsRequest();

            if (panels.Count > 0)
                p.panel = panels;

            if (overlays.Count > 0)
                p.overlay = overlays;

            if (components.Count > 0)
                p.component = components;

            json.Add(new JProperty("data", JObject.FromObject(p)));
            var payload = json.ToString();

            return TwitchPutGenericAsync<GetUserActiveExtensionsResponse>("/users/extensions", ApiVersion.Helix, payload, accessToken: authToken);
        }
    }
}
