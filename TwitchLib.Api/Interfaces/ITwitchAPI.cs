using TwitchLib.Api.Sections;

namespace TwitchLib.Api.Interfaces
{
    public interface ITwitchAPI
    {
        Auth Auth { get; }
        Badges Badges { get; }
        Bits Bits { get; }
        ChannelFeeds ChannelFeeds { get; }
        Channels Channels { get; }
        Chat Chat { get; }
        Clips Clips { get; }
        Collections Collections { get; }
        Communities Communities { get; }
        Games Games { get; }
        Ingests Ingests { get; }
        Search Search { get; }
        IApiSettings Settings { get; }
        Streams Streams { get; }
        Teams Teams { get; }
        ThirdParty ThirdParty { get; }
        Undocumented Undocumented { get; }
        Users Users { get; }
        Videos Videos { get; }
        Webhooks Webhooks { get; }
    }
}