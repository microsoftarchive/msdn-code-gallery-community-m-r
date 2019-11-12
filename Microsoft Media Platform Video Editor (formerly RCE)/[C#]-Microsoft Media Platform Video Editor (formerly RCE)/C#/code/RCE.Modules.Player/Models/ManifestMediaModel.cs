// <copyright file="ManifestMediaModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ManifestMediaModel.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Player.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Windows.Threading;

    using Microsoft.Practices.Unity;
    using Microsoft.SilverlightMediaFramework.Plugins;

    using RCE.Infrastructure;
    using RCE.Infrastructure.Events;
    using RCE.Infrastructure.Models;
    using RCE.Infrastructure.Services;
    using RCE.Modules.Player.Services;
    using RCE.Plugins.RubberBanding.Manager;
    using RCE.Transitions.Infrastructure.Managers;
    using RCE.VolumeOrchestrator;

    public class ManifestMediaModel : IManifestMediaModel
    {
        /// <summary>
        /// The <see cref="DispatcherTimer"/> to change the raise the <see cref="PositionUpdated"/> event
        /// and swith to diffrent <see cref="MediaData"/> when current media ends.
        /// </summary>
        private readonly DispatcherTimer timer;

        private readonly IOutputServiceFacade outputServiceFacade;

        private readonly ICache primaryCache;

        private readonly ISequenceRegistry sequenceRegistry;

        private readonly IPersistenceService persistenceService;

        private readonly IDictionary<int, ManifestPlayableMediaData> mediaDataByTrackId;

        private readonly IList<int> pendingPersists;

        private readonly IList<ITransitionsManager> transitionsManagers;

        private readonly IList<IRubberBandingManager> rubberBandingManagers;

        /// <summary>
        /// Contains the max number of Audio Tracks allowables.
        /// </summary>
        private readonly int maxNumberOfAudioTracks;

        public ManifestMediaModel(
            IOutputServiceFacade outputServiceFacade,
            [Dependency("PrimaryCache")] ICache primaryCache,
            ISequenceRegistry sequenceRegistry,
            IPersistenceService persistenceService,
            Func<ITransitionsManager> transitionsManagerFactory,
            Func<IRubberBandingManager> rubberBandingManagerFactory,
            IConfigurationService configurationService)
        {
            this.outputServiceFacade = outputServiceFacade;
            this.primaryCache = primaryCache;
            this.sequenceRegistry = sequenceRegistry;
            this.persistenceService = persistenceService;
            this.outputServiceFacade.PersistManifestCompleted += this.OnPersistManifestCompleted;
            this.mediaDataByTrackId = new Dictionary<int, ManifestPlayableMediaData>();
            this.maxNumberOfAudioTracks = configurationService.GetParameterValueAsInt("MaxNumberOfAudioTracks").GetValueOrDefault(1);

            this.transitionsManagers = new List<ITransitionsManager>();
            this.rubberBandingManagers = new List<IRubberBandingManager>();

            for (int i = 0; i < this.maxNumberOfAudioTracks + 1; i++)
            {
                ITransitionsManager transitionsManager = transitionsManagerFactory();
                IRubberBandingManager rubberBandingManager = rubberBandingManagerFactory();

                transitionsManager.IsAudioOnly = i != 0;

                VolumeOrchestrator.Bind(transitionsManager, rubberBandingManager);

                this.transitionsManagers.Add(transitionsManager);
                this.rubberBandingManagers.Add(rubberBandingManager);
            }

            this.InitializeMediaData();

            this.SetPluginForManagers();

            this.SubcribeToPlayingStateChanged();

            this.pendingPersists = new List<int>();
            this.timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(UtilityHelper.PositionUpdateIntervalMillis) };
            this.timer.Tick += this.OnFrameRendered;
            this.timer.Start();
        }

        public event EventHandler PlayingStateChanged;

        public event EventHandler FinishedPlaying;

        public event EventHandler<PositionPayloadEventArgs> PositionUpdated;

        public event EventHandler PersistCompleted;

        public event Action<IMediaPlugin> MediaElementFailed;

        public bool IsPlaying
        {
            get
            {
                return this.MediaDataCollection.Any(md => md.Playing);
            }
        }

        public bool IsStopped
        {
            get
            {
                return this.MediaDataCollection.Any(md => md.IsStopped);
            }
        }

        public TimeSpan Position
        {
            get
            {
                TimeSpan maxDuration = this.MediaDataCollection.Where(m => m.HasMedia).Max(m => m.Duration);
                return this.MediaDataCollection.Where(m => m.HasMedia).First(md => md.Duration == maxDuration).Position;
            }

            set
            {
                this.InvokeMethodForAllMediaData(md => md.Position = value);
            }
        }

        public TimeSpan Duration
        {
            get
            {
                return this.MediaDataCollection.Where(m => m.HasMedia).Max(md => md.MediaPlugin.Duration);
            }
        }

        public bool Mute
        {
            set
            {
                this.InvokeMethodForAllMediaData(md => md.IsMuted = value);
            }
        }

        private IEnumerable<ManifestPlayableMediaData> MediaDataCollection
        {
            get
            {
                return this.mediaDataByTrackId.Values;
            }
        }

        public MediaData GetPlayingMediaData()
        {
            return this.MediaDataCollection.Where(md => md.Playing).FirstOrDefault();
        }

        public void FastForward()
        {
            this.InvokeMethodForAllMediaData(md => md.FastForward());
        }

        public void FastRewind()
        {
            this.InvokeMethodForAllMediaData(md => md.FastRewind());
        }

        public void Play()
        {
            this.InvokeMethodForAllMediaData(md => md.Show());
            this.InvokeMethodForAllMediaData(md => md.Play());

            // There is a change the video is not ready to be reproduce at this moment. This is way we are also attached to the PlayingStateChanged event.
            if (this.IsPlaying)
            {
                this.StartTimer();
            }
        }

        public void Pause()
        {
            this.InvokeMethodForAllMediaData(md => md.Pause());
            this.StopTimer();
        }

        public void Stop()
        {
            this.InvokeMethodForAllMediaData(md => md.Stop());
            this.StopTimer();
        }

        public void PlaySlowMotion()
        {
            this.InvokeMethodForAllMediaData(md =>
                {
                    var rcePlugin = md.MediaPlugin as IRCESmoothStreamingMediaPlugin;
                    if (rcePlugin != null)
                    {
                        rcePlugin.SlowMotion();
                    }
                });
        }

        public MediaData GetVisualMediaData()
        {
            return this.mediaDataByTrackId[0];
        }

        public void ChangeVolumeSettingsRubberBanding(bool enableRubberBanding, double volume)
        {
            this.rubberBandingManagers.ForEach(rbm => rbm.Enabled = enableRubberBanding);

            if (!enableRubberBanding)
            {
                this.rubberBandingManagers.ForEach(rbm => rbm.VolumeLevel = volume);
            }
        }

        public void ChangeTrackMute(int trackId, bool isMuted)
        {
            this.mediaDataByTrackId[trackId].ForceMuted = false;
            this.mediaDataByTrackId[trackId].IsMuted = isMuted;
            this.mediaDataByTrackId[trackId].ForceMuted = isMuted;
        }

        public void ResetRubberBandingManagers()
        {
            this.rubberBandingManagers.ForEach(rbm => rbm.Reset());
        }

        public void ResetTransitionsManagers()
        {
            this.transitionsManagers.ForEach(tm => tm.Reset());
        }

        public void SetStreamSource(int trackId, Stream manifestStream)
        {
            if (manifestStream == null)
            {
                this.mediaDataByTrackId[trackId].HasMedia = false;
                return;
            }

            Uri uri = new Uri("http://localhost/" + Guid.NewGuid() + ".csm");

            manifestStream.Seek(0, SeekOrigin.Begin);

            string fileGuid = Guid.NewGuid().ToString();

            bool result = this.persistenceService.Persist(fileGuid, manifestStream);

            if (result)
            {
                var cacheItem = new CacheItem { Date = DateTime.Now, CachedValue = fileGuid, AvoidDeserialization = true };

                this.primaryCache.CacheItems.Add(uri.ToString(), cacheItem);

                this.persistenceService.AddApplicationSettings(uri.ToString(), cacheItem);
            }

            this.mediaDataByTrackId[trackId].SetSource(uri);
            this.mediaDataByTrackId[trackId].HasMedia = true;
        }

        public void SetSource(int trackId, string manifest)
        {
            this.pendingPersists.Add(trackId);
            this.outputServiceFacade.PersistManifestAsync(manifest, trackId);

            if (this.CanPlay())
            {
                this.OnPersistCompleted(EventArgs.Empty);
            }
        }

        public void InvokeMethodForAllMediaData(Action<MediaData> action)
        {
            foreach (var manifestPlayableMediaData in this.MediaDataCollection)
            {
                action(manifestPlayableMediaData);
            }
        }

        private void OnPersistManifestCompleted(object sender, OutputEventArgs e)
        {
            if (e.Generated)
            {
                int trackId = int.Parse(e.State.ToString());

                this.pendingPersists.Remove(trackId);
                this.mediaDataByTrackId[trackId].SetSource(new Uri(e.Result));

                if (this.CanPlay())
                {
                    this.OnPersistCompleted(EventArgs.Empty);
                }
            }
        }

        private bool CanPlay()
        {
            return this.pendingPersists.Count == 0;
        }

        private void OnPersistCompleted(EventArgs e)
        {
            EventHandler handler = this.PersistCompleted;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void SubcribeToPlayingStateChanged()
        {
            foreach (var value in this.mediaDataByTrackId.Values)
            {
                value.PlayingStateChanged += (s, e) => this.InvokePlayingStateChanged();
                value.PlayingStateChanged += (s, e) => this.OnPlayingStateChanged();
            }
        }

        private void OnPlayingStateChanged()
        {
            if (this.IsPlaying)
            {
                this.StartTimer();
            }
            else
            {
                this.StopTimer();
            }
        }

        private void InvokePlayingStateChanged()
        {
            EventHandler handler = this.PlayingStateChanged;
            if (handler != null)
            {
                handler(this, null);
            }
        }

        /// <summary>
        /// Raises the <see cref="PositionUpdated"/> event and the <see cref="FinishPlaying"/>
        /// event when the playback is over
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnFrameRendered(object sender, EventArgs e)
        {
            this.StopTimer();

            if (!this.IsPlaying)
            {
                return;
            }

            this.OnPositionUpdated(this.Position);

            this.StartTimer();
        }

        /// <summary>
        /// Called when position of the current <see cref="MediaData"/> is updated.
        /// </summary>
        /// <param name="position">The position.</param>
        private void OnPositionUpdated(TimeSpan position)
        {
            EventHandler<PositionPayloadEventArgs> positionUpdatedHandler = this.PositionUpdated;
            if (positionUpdatedHandler != null)
            {
                positionUpdatedHandler(this, new PositionPayloadEventArgs(position));
            }
        }

        private void StartTimer()
        {
            if (this.timer != null && !this.timer.IsEnabled)
            {
                this.timer.Start();
            }
        }

        private void StopTimer()
        {
            if (this.timer != null && this.timer.IsEnabled)
            {
                this.timer.Stop();
            }
        }

        /// <summary>
        /// Called when current <see cref="MediaData"/> reahces to the end.
        /// </summary>
        private void RaiseOnFinishedPlayingEvent()
        {
            EventHandler finishedPlayingHandler = this.FinishedPlaying;

            if (finishedPlayingHandler != null)
            {
                finishedPlayingHandler(this, EventArgs.Empty);
            }
        }

        private void MediaPluginMediaEnded(IMediaPlugin mediaPlugin)
        {
            if (mediaPlugin.Duration == this.Duration)
            {
                this.RaiseOnFinishedPlayingEvent();
            }
        }

        private void OnMediaElementFailed(IMediaPlugin plugin)
        {
            Action<IMediaPlugin> mediaElementFailed = this.MediaElementFailed;
            if (mediaElementFailed != null)
            {
                mediaElementFailed(plugin);
            }
        }

        private void InitializeMediaData()
        {
            var videoMediaData = new ManifestPlayableMediaData(true);
            videoMediaData.MediaPlugin.MediaEnded += this.MediaPluginMediaEnded;
            videoMediaData.MediaPluginFailed += this.OnMediaElementFailed;
            this.mediaDataByTrackId[0] = videoMediaData;

            for (int i = 1; i <= this.maxNumberOfAudioTracks; i++)
            {
                var audioMediaData = new ManifestPlayableMediaData(false);
                audioMediaData.MediaPlugin.MediaEnded += this.MediaPluginMediaEnded;
                audioMediaData.MediaPluginFailed += this.OnMediaElementFailed;
                this.mediaDataByTrackId[i] = audioMediaData;

                var track = this.sequenceRegistry.CurrentSequenceModel.Tracks[i];
                this.mediaDataByTrackId[i].IsMuted = track.IsMuted;
                this.mediaDataByTrackId[i].ForceMuted = track.IsMuted;
            }

            if (this.sequenceRegistry.CurrentSequenceModel != null)
            {
                foreach (var track in
                    this.sequenceRegistry.CurrentSequenceModel.Tracks.Where(
                        t => t.TrackType == TrackType.Audio || t.TrackType == TrackType.Visual))
                {
                    Track tempTrack = track;
                    var rubberBandingManager = this.rubberBandingManagers[tempTrack.Number - 1];
                    var transitionsManager = this.transitionsManagers[tempTrack.Number - 1];
                    track.PropertyChanged +=
                        (s, a) =>
                        {
                            if (a.PropertyName == "Volume")
                            {
                                // if rubberbanding is enabled, the factor to multiply by at each point in time should be 1.
                                rubberBandingManager.VolumeLevel = rubberBandingManager.Enabled ? 1.0 : tempTrack.Volume;
                                transitionsManager.VolumeLevel = tempTrack.Volume;
                            }
                            else if (a.PropertyName == "Balance")
                            {
                                this.mediaDataByTrackId[tempTrack.Number - 1].MediaPlugin.Balance = tempTrack.Balance;
                            }
                        };

                    // if rubberbanding is enabled, the factor to multiply by at each point in time should be 1.
                    rubberBandingManager.VolumeLevel = rubberBandingManager.Enabled ? 1.0 : tempTrack.Volume;
                    transitionsManager.VolumeLevel = tempTrack.Volume;
                }
            }
        }

        private void SetPluginForManagers()
        {
            int index = 0;

            this.InvokeMethodForAllMediaData(
                md =>
                {
                    IAdaptiveMediaPlugin plugin = md.MediaPlugin as IAdaptiveMediaPlugin;
                    this.rubberBandingManagers[index].SetAdaptivePlugin(plugin);
                    this.transitionsManagers[index].SetAdaptivePlugin(plugin);
                    index++;
                });
        }
    }
}
