using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using TwitchLib.Api.Core.Extensions.System;
using TwitchLib.Api.Helix.Models.Streams;
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
        private bool _isStartup;
        private List<string> _channelIds;
        private readonly ConcurrentDictionary<string, string> _channelToId;
<<<<<<< HEAD
        private readonly ConcurrentDictionary<string, Stream> _statuses;
=======
        private readonly ConcurrentDictionary<string, Models.v5.Streams.Stream> _statuses;
>>>>>>> master
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
<<<<<<< HEAD
        public List<Stream> CurrentLiveStreams { get { return _statuses.Where(x => x.Value != null).Select(x => x.Value).ToList(); } }
=======
        public List<Models.v5.Streams.Stream> CurrentLiveStreams { get { return _statuses.Where(x => x.Value != null).Select(x => x.Value).ToList(); } }
>>>>>>> master
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
        /// <exception cref="BadResourceException">If channel is invalid, an InvalidChannelException will be thrown.</exception>
        /// <param name="api">Instance of the Twitch Api Interface.</param>
<<<<<<< HEAD
        /// <param name="checkIntervalSeconds">Param representing number of seconds between calls to Twitch Api. Note: Lowering the number of seconds may increase the chance that you will hit rate limits.</param>
=======
        /// <param name="checkIntervalSeconds">Param representing number of seconds between calls to Twitch Api.</param>
        /// <param name="clientId">Optional param representing Twitch Api-required application client id, not required if already set.</param>
>>>>>>> master
        /// <param name="checkStatusOnStart">Checks the channel statuses on starting the service</param>
        /// <param name="invokeEventsOnStart">If checking the status on service start, optionally fire the OnStream Events (OnStreamOnline, OnStreamOffline, OnStreamUpdate)</param>
        public LiveStreamMonitor(ITwitchAPI api, int checkIntervalSeconds = 60, bool checkStatusOnStart = true, bool invokeEventsOnStart = false)
        {
            _api = api ?? throw new ArgumentNullException(nameof(api));
            _channelIds = new List<string>();
<<<<<<< HEAD
            _statuses = new ConcurrentDictionary<string, Stream>();
=======
            _statuses = new ConcurrentDictionary<string, Models.v5.Streams.Stream>();
>>>>>>> master
            _channelToId = new ConcurrentDictionary<string, string>();
            _checkStatusOnStart = checkStatusOnStart;
            _invokeEventsOnStart = invokeEventsOnStart;
            CheckIntervalSeconds = checkIntervalSeconds;
            _streamMonitorTimer.Elapsed += _streamMonitorTimerElapsed;
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
                Task.Run(() =>
                {
                    _isStartup = true;
                    _checkOnlineStreams();
                    _isStartup = false;

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
            GetUserIds(usernames).Wait();

            foreach (var item in _channelToId.Keys.Where(x => !usernames.Any(channelToId => channelToId.Equals(x, StringComparison.OrdinalIgnoreCase))))
                _channelToId.TryRemove(item, out _);

            SetStreamsByUserId(_channelToId.Values.ToList());
        }

        /// <summary> Sets the list of channels to monitor by userid </summary>
        /// <param name="userids">List of channels to monitor as userids</param>
        public void SetStreamsByUserId(List<string> userids)
        {
            _channelIds = userids;
            _channelIds.ForEach(x => _statuses.TryAdd(x, null));

<<<<<<< HEAD
            foreach (var item in _statuses.Keys.Where(x => !_channelIds.Any(channelId => channelId.Equals(x, StringComparison.OrdinalIgnoreCase))))
                _statuses.TryRemove(item, out _);
=======
            foreach (var item in _statuses.Keys.Where(x => !_channelIds.Any(channelId => channelId.Equals(x))).ToList())
                _statuses.TryRemove(item, out Models.v5.Streams.Stream _);
>>>>>>> master

            OnStreamsSet?.Invoke(this,
                new OnStreamsSetArgs { ChannelIds = ChannelIds, Channels = _channelToId, CheckIntervalSeconds = CheckIntervalSeconds });
        }
        #endregion

        private void _streamMonitorTimerElapsed(object sender, ElapsedEventArgs e)
        {
<<<<<<< HEAD
            OnStreamMonitorTimerTick?.Invoke(this,
                new OnStreamMonitorTimerTickArgs { CheckIntervalSeconds = CheckIntervalSeconds });
            await CheckForOnlineStreamChangesAsync();
=======
            _checkOnlineStreams();
>>>>>>> master
        }

        private void _checkOnlineStreams()
        {
            var liveStreamers = GetLiveStreamers().GetAwaiter().GetResult();

            foreach (var channel in _channelIds)
            {
<<<<<<< HEAD
                var currentStream = liveStreamers.FirstOrDefault(x => x.UserId == channel);
=======
                var currentStream = liveStreamers.FirstOrDefault(x => x.Channel.Id == channel);
>>>>>>> master
                if (currentStream == null)
                {
                    //offline
                    if (_statuses[channel] != null)
                    {
<<<<<<< HEAD
                        var channelId = new List<string> { _statuses[channel].Id };
                        var userObject = (await _api.Helix.Users.GetUsersAsync(channelId)).Users.First();

                        var channelName = userObject.DisplayName;
=======
                        var channelName = _statuses[channel].Channel.Name;
>>>>>>> master
                        //have gone offline
                        _statuses[channel] = null;

                        if (!_isStartup || _invokeEventsOnStart)
                        {
                            OnStreamOffline?.Invoke(this,
                                new OnStreamOfflineArgs { ChannelId = channel, Channel = channelName, CheckIntervalSeconds = CheckIntervalSeconds });
                        }
                    }
                }
                else
                {
<<<<<<< HEAD
                    var channelId = new List<string> { _statuses[channel].Id };
                    var userObject = (await _api.Helix.Users.GetUsersAsync(channelId)).Users.First();

                    var channelName = userObject.DisplayName;
=======
                    var channelName = currentStream.Channel.Name;
>>>>>>> master
                    //online
                    if (_statuses[channel] == null)
                    {
                        //have gone online
                        if (!_isStartup || _invokeEventsOnStart)
                        {
                            OnStreamOnline?.Invoke(this,
                                new OnStreamOnlineArgs { ChannelId = channel, Channel = channelName, Stream = currentStream, CheckIntervalSeconds = CheckIntervalSeconds });
                        }
                    }
                    else
                    {
                        //stream updated
                        if (!_isStartup || _invokeEventsOnStart)
                        {
                            OnStreamUpdate?.Invoke(this,
                                new OnStreamUpdateArgs { ChannelId = channel, Channel = channelName, Stream = currentStream, CheckIntervalSeconds = CheckIntervalSeconds });
                        }
                    }
                    _statuses[channel] = currentStream;
                }
            }
        }

<<<<<<< HEAD
        private async Task<List<Stream>> GetLiveStreamersAsync()
        {
            var livestreamers = new List<Stream>();
            var pages = (int)Math.Ceiling((double)_channelIds.Count / 100);
            
            for (var i = 0; i < pages; i++)
            {
                var selectedSet = _channelIds.Skip(i * 100).Take(100).ToList();
                var resultset = await _api.Helix.Streams.GetStreamsAsync(userIds: selectedSet.Select(x => x.ToString()).ToList(), first: 100);
                resultset?.Streams?.Where(x => x != null).Where(x => x.Type == "live").AddTo(livestreamers);
=======
        private async Task<List<Models.v5.Streams.Stream>> GetLiveStreamers()
        {
            var livestreamers = new List<Models.v5.Streams.Stream>();

            var resultset = await _api.Streams.v5.GetLiveStreamsAsync(_channelIds.Select(x => x.ToString()).ToList(), limit: 100);

            livestreamers.AddRange(resultset.Streams.ToList());

            var pages = (int)Math.Ceiling((double)resultset.Total / 100);
            for (var i = 1; i < pages; i++)
            {
                resultset = await _api.Streams.v5.GetLiveStreamsAsync(_channelIds.Select(x => x.ToString()).ToList(), limit: 100, offset: i * 100);
                livestreamers.AddRange(resultset.Streams.ToList());
            }
>>>>>>> master

            }
            return livestreamers;
        }

        private async Task GetUserIds(IEnumerable<string> usernames)
        {
            var usernamesToGet = usernames.Where(u => !_channelToId.Any(c => c.Key.Equals(u))).ToList();
            var pages = (int)Math.Ceiling((double)usernamesToGet.Count / 100);
            
            for (var i = 0; i < pages; i++)
            {
                var selectedSet = usernamesToGet.Skip(i * 100).Take(100).ToList();
<<<<<<< HEAD
                var users = await _api.Helix.Users.GetUsersAsync(logins: selectedSet);

                foreach (var user in users.Users)
                    _channelToId.TryAdd(user.Login, user.Id);
=======
                var users = await _api.Users.v5.GetUsersByNameAsync(selectedSet);

                foreach (var user in users.Matches)
                    _channelToId.TryAdd(user.Name, user.Id);
>>>>>>> master
            }
        }
    }
}
