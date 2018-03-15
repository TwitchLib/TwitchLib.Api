using System.Threading.Tasks;
using TwitchLib.Api.Enums;

namespace TwitchLib.Api.Sections
{
    public class Root
    {
        public Root(TwitchAPI api)
        {
            v3 = new V3(api);
            v5 = new V5(api);
        }

        public V3 v3 { get; }
        public V5 v5 { get; }

        public class V3 : ApiSection
        {
            public V3(TwitchAPI api) : base(api)
            {
            }
            #region Root
            public async Task<Models.v3.Root.RootResponse> GetRootAsync(string accessToken = null, string clientId = null)
            {
                return await Api.GetGenericAsync<Models.v3.Root.RootResponse>(Api.baseV3.Substring(0, Api.baseV3.Length - 1), null, accessToken, ApiVersion.v3, clientId).ConfigureAwait(false);
            }
            #endregion
        }

        public class V5 : ApiSection
        {
            public V5(TwitchAPI api) : base(api)
            {
            }
            #region GetRoot

            public async Task<Models.v5.Root.Root> GetRoot(string authToken = null, string clientId = null)
            {
                return await Api.GetGenericAsync<Models.v5.Root.Root>(Api.baseV5.Substring(0, Api.baseV5.Length - 1), null, authToken, ApiVersion.v5, clientId).ConfigureAwait(false);
            }

            #endregion
        }

    }
}