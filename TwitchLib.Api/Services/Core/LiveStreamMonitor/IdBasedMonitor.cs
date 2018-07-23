using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Interfaces;
using TwitchLib.Api.Helix.Models.Streams;

namespace TwitchLib.Api.Services.Core.LiveStreamMonitor
{
    /// <summary>Service that allows customizability and subscribing to detection of new Twitch followers.</summary>
    internal class IdBasedMonitor : CoreMonitor
    {
        public IdBasedMonitor(ITwitchAPI api) : base(api) { }

        public override Task<Func<Stream, bool>> CompareStream(string channel)
        {
            return Task.FromResult(new Func<Stream, bool>(stream => stream.UserId == channel));
        }

        public override Task<GetStreamsResponse> GetStreamsAsync(List<string> channels)
        {
            return _api.Helix.Streams.GetStreamsAsync(first: channels.Count, userIds: channels);
        }
    }
}
