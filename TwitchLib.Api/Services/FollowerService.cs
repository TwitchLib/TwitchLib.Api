using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchLib.Api.Interfaces;
using TwitchLib.Api.Services.Core.FollowerService;
using TwitchLib.Api.Services.Events.FollowerService;
using TwitchLib.Api.Helix.Models.Users.Internal;

namespace TwitchLib.Api.Services
{
    public class FollowerService : ApiService
    {
        private readonly Dictionary<string, DateTime> _lastFollowerDates = new Dictionary<string, DateTime>(StringComparer.OrdinalIgnoreCase);

        private CoreMonitor _monitor;
        private IdBasedMonitor _idBasedMonitor;
        private NameBasedMonitor _nameBasedMonitor;

        public Dictionary<string, List<Follow>> KnownFollowers { get; } = new Dictionary<string, List<Follow>>(StringComparer.OrdinalIgnoreCase);
        public int QueryCountPerRequest { get; }
        public int CacheSize { get; }

        private IdBasedMonitor IdBasedMonitor => _idBasedMonitor ?? (_idBasedMonitor = new IdBasedMonitor(_api));
        private NameBasedMonitor NameBasedMonitor => _nameBasedMonitor ?? (_nameBasedMonitor = new NameBasedMonitor(_api));

        public event EventHandler<OnNewFollowersDetectedArgs> OnNewFollowersDetected;

        public FollowerService(ITwitchAPI api, int checkIntervalInSeconds = 60, int queryCountPerRequest = 100, int cacheSize = 1000) : 
            base(api, checkIntervalInSeconds)
        {
            if (queryCountPerRequest < 1 || queryCountPerRequest > 100)
                throw new ArgumentException("Twitch doesn't support less than 1 or more than 100 followers per request.", nameof(queryCountPerRequest));

            if (cacheSize < queryCountPerRequest)
                throw new ArgumentException($"The cache size must be at least the size of the {nameof(queryCountPerRequest)} parameter.", nameof(cacheSize));

            QueryCountPerRequest = queryCountPerRequest;
            CacheSize = cacheSize;
        }

        public void ClearCache()
        {
            KnownFollowers.Clear();

            _lastFollowerDates.Clear();

            _nameBasedMonitor?.ClearCache();

            _nameBasedMonitor = null;
            _idBasedMonitor = null;
        }

        public void SetChannelsById(List<string> channelsToMonitor)
        {
            SetChannels(channelsToMonitor);

            _monitor = IdBasedMonitor;
        }

        public void SetChannelsByName(List<string> channelsToMonitor)
        {
            SetChannels(channelsToMonitor);

            _monitor = NameBasedMonitor;
        }

        public async Task UpdateLatestFollowersAsync(bool callEvents = true)
        {
            if (ChannelsToMonitor == null)
                return;

            foreach (var channel in ChannelsToMonitor)
            {
                List<Follow> newFollowers;
                var latestFollowers = await GetLatestFollowersAsync(channel);

                if (latestFollowers.Count == 0)
                    return;

                if (!KnownFollowers.TryGetValue(channel, out var knownFollowers))
                {
                    newFollowers = latestFollowers;
                    KnownFollowers[channel] = latestFollowers.Take(CacheSize).ToList();
                    _lastFollowerDates[channel] = latestFollowers.Last().FollowedAt;
                }
                else
                {
                    var existingFollowerIds = new HashSet<string>(knownFollowers.Select(f => f.FromUserId));
                    var latestKnownFollowerDate = _lastFollowerDates[channel];
                    newFollowers = new List<Follow>();

                    foreach (var follower in latestFollowers)
                    {
                        if (!existingFollowerIds.Add(follower.FromUserId)) continue;

                        if (follower.FollowedAt < latestKnownFollowerDate) continue;

                        newFollowers.Add(follower);
                        latestKnownFollowerDate = follower.FollowedAt;
                        knownFollowers.Add(follower);
                    }

                    existingFollowerIds.Clear();
                    existingFollowerIds.TrimExcess();

                    // prune cache so we don't use too much space unnecessarily
                    if (knownFollowers.Count > CacheSize)
                        knownFollowers.RemoveRange(0, knownFollowers.Count - CacheSize);

                    if (newFollowers.Count <= 0)
                        return;

                    _lastFollowerDates[channel] = latestKnownFollowerDate;
                }

                if (!callEvents)
                    return;

                OnNewFollowersDetected?.Invoke(this, new OnNewFollowersDetectedArgs { Channel = channel, NewFollowers = newFollowers });
            }
        }

        protected override async Task OnServiceTimerTick(bool callEvents = true)
        {
            await base.OnServiceTimerTick(callEvents);
            await UpdateLatestFollowersAsync(callEvents);
        }

        private async Task<List<Follow>> GetLatestFollowersAsync(string channel)
        {
            var resultset = await _monitor.GetUsersFollowsAsync(channel, QueryCountPerRequest);
            
            return resultset.Follows.Reverse().ToList();
        }
    }
}
