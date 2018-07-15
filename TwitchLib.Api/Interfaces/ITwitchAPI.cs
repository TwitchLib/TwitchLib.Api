using TwitchLib.Api.Core.Undocumented;

namespace TwitchLib.Api.Interfaces
{
    public interface ITwitchAPI
    {
        V5.V5 V5 { get; }
        Helix.Helix Helix { get; }
        ThirdParty.ThirdParty ThirdParty { get; }
        Undocumented Undocumented { get; }
    }
}