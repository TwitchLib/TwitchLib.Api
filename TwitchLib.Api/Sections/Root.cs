using System.Threading.Tasks;
using TwitchLib.Api.Enums;

namespace TwitchLib.Api.Sections
{
    public class Root
    {
        public Root(TwitchAPI api)
        {
            v5 = new V5Api(api);
        }

        public V5Api v5 { get; }

        public class V5Api : ApiSection
        {
            public V5Api(TwitchAPI api) : base(api)
            {
            }
            #region GetRoot

            public Task<Models.v5.Root.Root> GetRootAsync(string authToken = null, string clientId = null)
            {
                return Api.TwitchGetGenericAsync<Models.v5.Root.Root>("", ApiVersion.v5, accessToken: authToken, clientId: clientId);
            }

            #endregion
        }

    }
}
