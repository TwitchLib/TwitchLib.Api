using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Timers;
using TwitchLib.Api.Interfaces;
using TwitchLib.Api.Services.Events.FollowerService;
using TwitchLib.Api.Services.Exceptions;

namespace TwitchLib.Api.Services
{
    /// <summary>Service that allows customizability and subscribing to detection of new Twitch followers.</summary>
    public class FollowerService
    {
        private readonly ITwitchAPI _api;
        private readonly Timer _followerServiceTimer = new Timer();
        
        private readonly HashSet<string> _channelsToRemove = new HashSet<string>();
        private readonly HashSet<string> _channelsToAdd = new HashSet<string>();

        private int _queryCount;
        private int _checkIntervalSeconds;
        private bool _checkingForNewFollowers;

        /// <summary>Property representing the number of followers to compare a fresh query against for new followers. Default: 1000.</summary>
        public int CacheSize { get; set; } = 1000;
        /// <summary>Property representing number of recent followers that service should request. Recommended: 25, increase for larger channels. MAX: 100, MINIMUM: 1</summary>
        /// <exception cref="BadQueryCountException">Throws BadQueryCountException if queryCount is larger than 100 or smaller than 1.</exception>
        public int QueryCount { get => _queryCount; set { if (value < 1 || value > 100) { throw new BadQueryCountException("Query count was smaller than 1 or exceeded 100"); } _queryCount = value; } }
        /// <summary>Property representing the cache where detected followers are stored and compared against</summary>
        public Dictionary<string, List<IFollow>> FollowerCache { get; set; } = new Dictionary<string, List<IFollow>>();
        /// <summary>Property representing interval between Twitch Api calls, in seconds. Recommended: 60</summary>
        public int CheckIntervalSeconds { get => _checkIntervalSeconds; set { _checkIntervalSeconds = value; _followerServiceTimer.Interval = value * 1000; } } 

        private bool CheckingForNewFollowers
        {
            get => _checkingForNewFollowers;
            set
            {
                _checkingForNewFollowers = value;
                if (!_checkingForNewFollowers)
                    UpdateCache();
            }
        }

        /// <summary>Event fires when service starts.</summary>
        public event EventHandler<OnServiceStartedArgs> OnServiceStarted;
        /// <summary>Event fires when service stops.</summary>
        public event EventHandler<OnServiceStoppedArgs> OnServiceStopped;
        /// <summary>Event fires when new followers are detected.</summary>
        public event EventHandler<OnNewFollowersDetectedArgs> OnNewFollowersDetected;

        /// <summary>Service constructor.</summary>
        /// <exception cref="ArgumentNullException">If the provided api is invalid, an ArgumentNullException will be thrown.</exception>
        /// <param name="api">TwitchApi instance</param>
        /// <param name="checkIntervalSeconds">Param representing number of seconds between calls to Twitch Api.</param>
        /// <param name="queryCount">Number of recent followers service should request from Twitch Api. Max: 100, Min: 1</param>
        public FollowerService(ITwitchAPI api, int checkIntervalSeconds = 60, int queryCount = 25)
        {
            _api = api ?? throw new ArgumentNullException(nameof(api));
            _followerServiceTimer.Elapsed += CheckForNewFollowers;

            CheckIntervalSeconds = checkIntervalSeconds;
            QueryCount = queryCount;
        }

        /// <summary>
        /// Starts the service and calls the OnServiceStarted event
        /// </summary>
        public void StartService()
        {
            _followerServiceTimer.Start();

            OnServiceStarted?.Invoke(this,
                new OnServiceStartedArgs { ChannelIds = FollowerCache.Keys.ToList() });
        }

        /// <summary>
        /// Stops the service and calls the OnServiceStopped event
        /// </summary>
        public void StopService()
        {
            _followerServiceTimer.Stop();
            OnServiceStopped?.Invoke(this,
                new OnServiceStoppedArgs { ChannelIds = FollowerCache.Keys.ToList() });
        }

        /// <summary>
        /// Sets the followercache from all of the added channels to contain the last followers based on the queryCount
        /// </summary>
        public async Task InitializeWithLatestFollowersAsync()
        {
            foreach (var channelId in FollowerCache.Keys)
                await InitializeWithLatestFollowersAsync(channelId);
        }

        /// <summary>
        /// Sets the followercache from the given channelId to contain the last followers based on the queryCount
        /// </summary>
        /// <exception cref="ArgumentNullException">If the provided channelId is null.</exception>
        /// <exception cref="UninitializedChannelDataException">If the provided channelId is invalid.</exception>
        /// <param name="channelId">The id of the channel, this channel must be added through AddChannel()</param>
        public async Task InitializeWithLatestFollowersAsync(string channelId)
        {
            if (channelId == null)
                throw new ArgumentNullException(nameof(channelId));

            if (!FollowerCache.ContainsKey(channelId))
                throw new UninitializedChannelDataException("ChannelId must be set before starting the FollowerService. Use AddChannel() to set the channel to monitor");

            await InitializeWithLatestFollowersAsyncInternal(channelId);
        }

        /// <summary>
        /// Adds a channel to get NewFollowersDetected callbacks from by id.
        /// </summary>
        /// <exception cref="ArgumentNullException">If the provided channelId is null.</exception>
        /// <param name="channelId">The id from the channel</param>
        public void AddChannel(string channelId)
        {
            if (channelId == null)
                throw new ArgumentNullException(nameof(channelId));

            AddChannelInternal(channelId);
        }

        /// <summary>
        /// Removes a channel to get NewFollowersDetected callbacks from by id.
        /// </summary>
        /// <exception cref="ArgumentNullException">If the provided channelId is null.</exception>
        /// <param name="channelId">The id from the channel</param>
        public void RemoveChannelById(string channelId)
        {
            if (channelId == null)
                throw new ArgumentNullException(nameof(channelId));

            RemoveChannelInternal(channelId);
        }

        private async Task InitializeWithLatestFollowersAsyncInternal(string channelId)
        {
            var response = await _api.Channels.v5.GetChannelFollowersAsync(channelId, QueryCount);

            FollowerCache[channelId].Clear();

            foreach (var follower in response.Follows)
                FollowerCache[channelId].Add(follower);
        }

        private async void CheckForNewFollowers(object sender, ElapsedEventArgs e)
        {
            CheckingForNewFollowers = true; //'Prevents' the UpdateCache from being be modified while looping through it.
            await CheckForNewFollowersAsync();
            CheckingForNewFollowers = false; //When setting CheckingForNewFollowers to false, it'll call UpdateCache to add/remove channels which were added during the check.
        }

        private async Task CheckForNewFollowersAsync()
        {
            foreach (var channelId in FollowerCache.Keys)
            {
                try
                {
                    var followers = await _api.Channels.v5.GetChannelFollowersAsync(channelId, QueryCount);
                    HandleNewFollowers(channelId, followers.Follows);
                }
                catch (WebException) { return; }
            }
        }
        
        private void HandleNewFollowers(string channelId, IEnumerable<IFollow> followers)
        {
            var newFollowers = new List<IFollow>();
            var knownFollowers = FollowerCache[channelId];

            var existingFollowerIds = new HashSet<string>(knownFollowers.Select(f => f.User.Id));

            foreach (var follower in followers)
            {
                if (existingFollowerIds.Add(follower.User.Id))
                {
                    newFollowers.Add(follower);
                    knownFollowers.Add(follower);
                }
            }

            existingFollowerIds.Clear();
            existingFollowerIds.TrimExcess();

            // Check for new followers
            if (newFollowers.Count <= 0)
                return;

            // prune cache so we don't use too much space unnecessarily
            if (knownFollowers.Count > CacheSize)
                knownFollowers.RemoveRange(0, knownFollowers.Count - CacheSize);

            // Invoke followers event with list of follows - IFollow
            OnNewFollowersDetected?.Invoke(this,
                new OnNewFollowersDetectedArgs
                {
                    ChannelId = channelId,
                    NewFollowers = newFollowers
                });
        }

        private void AddChannelInternal(string channelId)
        {
            channelId = channelId.ToLower();

            if (FollowerCache.ContainsKey(channelId))
                return;

            //If we're currently checking for new followers, changing the FollowerCache might throw a collection modified exception, so we add it to a list instead to handle later
            if (CheckingForNewFollowers)
            {
                _channelsToRemove.Remove(channelId);
                _channelsToAdd.Add(channelId);
            }
            else
            {
                FollowerCache[channelId] = new List<IFollow>();
            }
        }

        private void RemoveChannelInternal(string channelId)
        {
            channelId = channelId.ToLower();

            if (!FollowerCache.ContainsKey(channelId))
                return;

            //If we're currently checking for new followers, changing the FollowerCache might throw a collection modified exception, so we add it to a list instead to handle later
            if (CheckingForNewFollowers)
            {
                _channelsToAdd.Remove(channelId);
                _channelsToRemove.Add(channelId);
            }
            else
            {
                FollowerCache.Remove(channelId);
            }
        }

        private void UpdateCache()
        {
            foreach (var channel in _channelsToAdd)
                FollowerCache[channel] = new List<IFollow>();

            foreach (var channel in _channelsToRemove)
                FollowerCache.Remove(channel);
        }
    }
}
