using System.Threading.Tasks;
using TwitchLib.Api.Enums;

namespace TwitchLib.Api.Sections
{
    public class Root
    {
        public Root(TwitchAPI api)
        {
            v5 = new V5(api);
        }
        
        public V5 v5 { get; }

        public class V5 : ApiSection
        {
            public V5(TwitchAPI api) : base(api)
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
