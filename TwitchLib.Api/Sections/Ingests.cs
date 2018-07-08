using System.Threading.Tasks;
using TwitchLib.Api.Enums;

namespace TwitchLib.Api.Sections
{
    public class Ingests
    {
        public Ingests(TwitchAPI api)
        {
            v5 = new V5Api(api);
        }

        public V5Api v5 { get; }

        public class V5Api : ApiSection
        {
            public V5Api(TwitchAPI api) : base(api)
            {
            }

            #region GetIngestServerList

            public Task<Models.v5.Ingests.Ingests> GetIngestServerListAsync()
            {
                return Api.TwitchGetGenericAsync<Models.v5.Ingests.Ingests>("/ingests", ApiVersion.v5);
            }

            #endregion
        }
    }
}