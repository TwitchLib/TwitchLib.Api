using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using TwitchLib.Api.Extensions.System;
using TwitchLib.Api.Interfaces;
using TwitchLib.Api.Services.Events.LiveStreamMonitor;
using TwitchLib.Api.Services.Exceptions;

namespace TwitchLib.Api.Services
{
    /// <summary>Service that allows customizability and subscribing to detection of channels going online/offline.</summary>
    public class LiveStreamMonitor
    {
        #region Private Variables
        private int _checkIntervalSeconds;
        private bool _isStarting;
        private List<string> _channelIds;
        private readonly ConcurrentDictionary<string, string> _channelToId;
        private readonly ConcurrentDictionary<string, Models.Helix.Streams.Stream> _statuses;
        private readonly Timer _streamMonitorTimer = new Timer();
        private readonly bool _checkStatusOnStart;
        private readonly bool _invokeEventsOnStart;
        private readonly ITwitchAPI _api;

        #endregion

        #region Public Variables
        /// <summary>Property representing Twitch channelIds service is monitoring.</summary>
        public List<string> ChannelIds
        {
            get => _channelIds.ToList();
            protected set => _channelIds = value;
        }
        /// <summary> Property representing Twitch channels service is monitoring. </summary>
        public ReadOnlyCollection<string> Channels => _channelToId.Keys.ToList().AsReadOnly();
        /// <summary> </summary>
        public List<Models.Helix.Streams.Stream> CurrentLiveStreams { get { return _statuses.Where(x => x.Value != null).Select(x => x.Value).ToList(); } }
        /// <summary> </summary>
        public List<string> CurrentOfflineStreams { get { return _statuses.Where(x => x.Value == null).Select(x => x.Key).ToList(); } }
        /// <summary>Property representing interval between Twitch Api calls, in seconds. Recommended: 60</summary>
        public int CheckIntervalSeconds { get => _checkIntervalSeconds; set { _checkIntervalSeconds = value; _streamMonitorTimer.Interval = value * 1000; } }
        #endregion

        #region EVENTS
        /// <summary>Event fires when Stream goes online</summary>
        public event EventHandler<OnStreamOnlineArgs> OnStreamOnline;
        /// <summary>Event fires when Stream goes offline</summary>
        public event EventHandler<OnStreamOfflineArgs> OnStreamOffline;
        /// <summary>Event fires when Stream gets updated</summary>
        public event EventHandler<OnStreamUpdateArgs> OnStreamUpdate;
        /// <summary>Event fires when service started.</summary>
        public event EventHandler<OnStreamMonitorStartedArgs> OnStreamMonitorStarted;
        /// <summary>Event fires when service ended.</summary>
        public event EventHandler<OnStreamMonitorEndedArgs> OnStreamMonitorEnded;
        /// <summary>Event fires when channels to monitor are intitialized.</summary>
        public event EventHandler<OnStreamsSetArgs> OnStreamsSet;
        /// <summary>Event fires when timer ticks.</summary>
        public event EventHandler<OnStreamMonitorTimerTickArgs> OnStreamMonitorTimerTick;
        #endregion

        /// <summary>Service constructor.</summary>
        /// <exception cref="ArgumentNullException">If the provided api is null.</exception>
        /// <param name="api">Instance of the Twitch Api Interface.</param>
        /// <param name="checkIntervalSeconds">Param representing number of seconds between calls to Twitch Api. Note: Lowering the number of seconds may increase the chance that you will hit rate limits.</param>
        /// <param name="checkStatusOnStart">Checks the channel statuses on starting the service</param>
        /// <param name="invokeEventsOnStart">If checking the status on service start, optionally fire the OnStream Events (OnStreamOnline, OnStreamOffline, OnStreamUpdate)</param>
        public LiveStreamMonitor(ITwitchAPI api, int checkIntervalSeconds = 60, bool checkStatusOnStart = true, bool invokeEventsOnStart = false)
        {
            _api = api ?? throw new ArgumentNullException(nameof(api));
            _channelIds = new List<string>();
            _statuses = new ConcurrentDictionary<string, Models.Helix.Streams.Stream>();
            _channelToId = new ConcurrentDictionary<string, string>();
            _checkStatusOnStart = checkStatusOnStart;
            _invokeEventsOnStart = invokeEventsOnStart;
            CheckIntervalSeconds = checkIntervalSeconds;
            _streamMonitorTimer.Elapsed += CheckForOnlineStreamChangesAsync;
        }

        #region CONTROLS
        /// <summary>Starts service, updates status of all channels, fires OnStreamMonitorStarted event. </summary>
        /// <exception cref="UnintializedChannelListException">If no channels to monitor were provided an UnintializedChannelListException will be thrown.</exception>
        public void StartService()
        {
            if (!_channelIds.Any())
                throw new UnintializedChannelListException("You must atleast add 1 channel to monitor before starting the Service. Use SetStreamsById() to add a channel to monitor");

            if (_checkStatusOnStart)
            {
                Task.Run(async() =>
                {
                    _isStarting = true;
                    await CheckForOnlineStreamChangesAsync();
                    _isStarting = false;

                    OnInitialized();
                });
                return;
            }

            OnInitialized();
        }

        private void OnInitialized()
        {
            //Timer not started until initial check complete
            _streamMonitorTimer.Start();
            OnStreamMonitorStarted?.Invoke(this,
                new OnStreamMonitorStartedArgs { ChannelIds = ChannelIds, Channels = _channelToId, CheckIntervalSeconds = CheckIntervalSeconds });
        }

        /// <summary>Stops service and fires OnStreamMonitorStopped event.</summary>
        public void StopService()
        {
            _streamMonitorTimer.Stop();
            OnStreamMonitorEnded?.Invoke(this,
               new OnStreamMonitorEndedArgs { ChannelIds = ChannelIds, Channels = _channelToId, CheckIntervalSeconds = CheckIntervalSeconds });
        }
        /// <summary>
        /// Sets the list of channels to monitor by username
        /// </summary>
        /// <param name="usernames">List of channels to monitor as usernames</param>
        public void SetStreamsByUsername(List<string> usernames)
        {
            GetUserIdsAsync(usernames).Wait();

            foreach (var item in _channelToId.Keys.Where(x => !usernames.Any(channelToId => channelToId.Equals(x))).ToList())
                _channelToId.TryRemove(item, out _);

            SetStreamsByUserId(_channelToId.Values.ToList());
        }

        /// <summary> Sets the list of channels to monitor by userid </summary>
        /// <param name="userids">List of channels to monitor as userids</param>
        public void SetStreamsByUserId(List<string> userids)
        {
            _channelIds = userids;
            _channelIds.ForEach(x => _statuses.TryAdd(x, null));

            foreach (var item in _statuses.Keys.Where(x => !_channelIds.Any(channelId => channelId.Equals(x))).ToList())
                _statuses.TryRemove(item, out _);

            OnStreamsSet?.Invoke(this,
                new OnStreamsSetArgs { ChannelIds = ChannelIds, Channels = _channelToId, CheckIntervalSeconds = CheckIntervalSeconds });
        }
        #endregion

        private async void CheckForOnlineStreamChangesAsync(object sender, ElapsedEventArgs e)
        {
            OnStreamMonitorTimerTick?.Invoke(this,
                new OnStreamMonitorTimerTickArgs { CheckIntervalSeconds = CheckIntervalSeconds });
            await CheckForOnlineStreamChangesAsync();
        }

        private async Task CheckForOnlineStreamChangesAsync()
        {
            var liveStreamers = await GetLiveStreamersAsync();

            foreach (var channel in _channelIds)
            {
                var currentStream = liveStreamers.FirstOrDefault(x => x.Id == channel);
                if (currentStream == null)
                {
                    //offline
                    if (_statuses[channel] != null)
                    {
                        var channelId = new List<string> { _statuses[channel].Id };
                        var userObject = (await _api.Users.Helix.GetUsersAsync(channelId)).Users.First();
                        var channelName = userObject.DisplayName;
                        //have gone offline

                        _statuses[channel] = null;

                        if (!_isStarting || _invokeEventsOnStart)
                        {
                            OnStreamOffline?.Invoke(this,
                                new OnStreamOfflineArgs { ChannelId = channel, Channel = channelName, CheckIntervalSeconds = CheckIntervalSeconds });
                        }
                    }
                }
                else
                {
                    var channelId = new List<string> { _statuses[channel].Id };
                    var userObject = (await _api.Users.Helix.GetUsersAsync(channelId)).Users.First();
                    var channelName = userObject.DisplayName;
                    //online
                    if (_statuses[channel] == null)
                    {
                        //have gone online
                        if (!_isStarting || _invokeEventsOnStart)
                        {
                            OnStreamOnline?.Invoke(this,
                                new OnStreamOnlineArgs { ChannelId = channel, Channel = channelName, Stream = currentStream, CheckIntervalSeconds = CheckIntervalSeconds });
                        }
                    }
                    else
                    {
                        //stream updated
                        if (!_isStarting || _invokeEventsOnStart)
                        {
                            OnStreamUpdate?.Invoke(this,
                                new OnStreamUpdateArgs { ChannelId = channel, Channel = channelName, Stream = currentStream, CheckIntervalSeconds = CheckIntervalSeconds });
                        }
                    }
                    _statuses[channel] = currentStream;
                }
            }
        }

        private async Task<List<Models.Helix.Streams.Stream>> GetLiveStreamersAsync()
        {
            var livestreamers = new List<Models.Helix.Streams.Stream>();
            var pages = _channelIds.Count - 1 / 100 + 1;
            for (var i = 0; i < pages; i++)
            {
                var selectedSet = _channelIds.Skip(i * 100).Take(100).ToList();
                var resultset = await _api.Streams.Helix.GetStreamsAsync(userIds: selectedSet.Select(x => x.ToString()).ToList(), first: 100);
                resultset?.Streams?.Where(x => x != null).Where(x => x.Type == "live").AddTo(livestreamers);
            }
            return livestreamers;
        }

        private async Task GetUserIdsAsync(IEnumerable<string> usernames)
        {
            var usernamesToGet = usernames.Where(u => !_channelToId.Any(c => c.Key.Equals(u))).ToList();
            var pages = (usernamesToGet.Count + 100 - 1) / 100;

            for (var i = 0; i < pages; i++)
            {
                var selectedSet = usernamesToGet.Skip(i * 100).Take(100).ToList();
                var users = await _api.Users.Helix.GetUsersAsync(logins: selectedSet);

                foreach (var user in users.Users)
                    _channelToId.TryAdd(user.DisplayName, user.Id);
            }
        }
    }
}
