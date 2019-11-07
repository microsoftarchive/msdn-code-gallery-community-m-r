// <copyright file="ManifestPlayableMediaData.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ManifestPlayableMediaData.cs                     
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
    using System.IO;
    using System.Windows;

    using Microsoft.SilverlightMediaFramework.Plugins;
    using Microsoft.SilverlightMediaFramework.Plugins.Primitives;

    using RCE.Infrastructure;

    public class ManifestPlayableMediaData : MediaData
    {
        private readonly bool videoMedia;

        private IRCESmoothStreamingMediaPlugin mediaPlugin;

        /// <summary>
        /// Indicates if the media is buffering.
        /// </summary>
        private bool isBufferStarted;

        private bool disposed;

        private bool playEnqueued;

        private bool sourceChanging;

        private TimeSpan? bufferedPosition;

        private TimeSpan? lastSeekPosition { get; set; }

        private object lastSeekPositionLock = new object();
       
        public ManifestPlayableMediaData(bool videoMedia)
        {
            this.videoMedia = videoMedia;
            this.mediaPlugin = new RCESmoothStreamingMediaPlugin { AutoPlay = false };

            this.mediaPlugin.VisualElement.Opacity = 0;
            this.mediaPlugin.VisualElement.Visibility = Visibility.Collapsed;

            this.mediaPlugin.CurrentStateChanged += this.MediaPluginCurrentStateChanged;
            this.mediaPlugin.SeekCompleted += this.MediaPluginSeekCompleted;
            this.mediaPlugin.MediaOpened += this.OnMediaOpened;
            this.mediaPlugin.MediaFailed += this.MediaPlugin_MediaFailed;
        }
        
        public event EventHandler PlayingStateChanged;

        public event Action<IMediaPlugin> MediaPluginFailed;

        public override bool Playing
        {
            get
            {
                return base.Playing;
            }

            set
            {
                bool oldValue = base.Playing;
                base.Playing = value;

                if (oldValue != value)
                {
                    this.InvokePlayingStateChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the position of the <see cref="PlayableMediaData"/>.
        /// </summary>
        /// <value>The position.</value>
        public override TimeSpan Position
        {
            get
            {
                if (this.mediaPlugin.StartPosition.TotalSeconds > 0)
                {
                    if (this.mediaPlugin.Position.TotalSeconds >= this.mediaPlugin.StartPosition.TotalSeconds)
                    {
                        return this.mediaPlugin.Position - this.mediaPlugin.StartPosition;
                    }

                    if (this.mediaPlugin.Position.TotalSeconds < this.mediaPlugin.StartPosition.TotalSeconds)
                    {
                        return this.mediaPlugin.StartPosition - this.mediaPlugin.StartPosition;
                    }
                }

                return this.mediaPlugin.Position;
            }

            set
            {
                if (this.IsMediaPluginPlayReadyForSeeking)
                {
                    this.DoActualSeek(value);  
                }
                else
                {
                    this.bufferedPosition = value;
                }
            }
        }

        public TimeSpan Duration 
        {
            get
            {
                return this.mediaPlugin.Duration;
            }
        }

        public TimeSpan? LastSeekPosition
        {
            get
            {
                return this.lastSeekPosition;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the mediadata is muted.
        /// </summary>
        /// <value>
        /// Value would be <c>true</c> if the <see cref="PlayableMediaData"/> is muted; otherwise, <c>false</c>.
        /// </value>
        public override bool IsMuted
        {
            get
            {
                return this.mediaPlugin.IsMuted;
            }

            set
            {
                if (this.ForceMuted)
                {
                    this.mediaPlugin.IsMuted = true;
                }
                else
                {
                    this.mediaPlugin.IsMuted = value;
                }
            }
        }

        public bool ForceMuted { get; set; }

        public override IMediaPlugin MediaPlugin
        {
            get
            {
                return this.mediaPlugin;
            }
        }

        public bool IsStopped
        {
            get
            {
                return this.mediaPlugin.CurrentState == MediaPluginState.Stopped;
            }
        }

        public override object Media
        {
            get { return this.mediaPlugin.VisualElement; }
        }

        private bool IsMediaPluginPlayReadyForPlaying
        {
            get
            {
                return (this.IsMediaPluginPlayReadyForSeeking
                            || this.mediaPlugin.CurrentState == MediaPluginState.Stopped)
                            && !this.sourceChanging && this.HasMedia;
            }
        }

        private bool IsMediaPluginPlayReadyForSeeking
        {
            get
            {
                return (this.mediaPlugin.CurrentState == MediaPluginState.Buffering
                            || this.mediaPlugin.CurrentState == MediaPluginState.Paused
                            || this.mediaPlugin.CurrentState == MediaPluginState.Playing
                            || this.mediaPlugin.CurrentState == MediaPluginState.ClipPlaying)
                            && !this.sourceChanging && this.HasMedia; 
            }
        }

        /// <summary>
        /// Shows this <see cref="MediaData"/>.
        /// </summary>
        public override void Show()
        {
            if (!this.HasMedia)
            {
                return;
            }

            base.Show();

            if (!this.videoMedia)
            {
                this.mediaPlugin.VisualElement.Opacity = 0;
            }
            
            this.mediaPlugin.VisualElement.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public override void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Hides this <see cref="PlayableMediaData"/>.
        /// </summary>
        public override void Hide()
        {
            base.Hide();

            // this.mediaPlugin.VisualElement.Opacity = 0;
            this.mediaPlugin.VisualElement.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Stops this <see cref="PlayableMediaData"/>.
        /// </summary>
        public override void Stop()
        {
            if (!this.mediaPlugin.PlaySpeedManager.IsPlaySpeedNormal)
            {
                this.mediaPlugin.PlaySpeedManager.RestoreNaturalPlaySpeed();
            }

            this.mediaPlugin.Stop();
            this.Playing = false;
        }

        /// <summary>
        /// Plays this <see cref="PlayableMediaData"/>.
        /// </summary>
        public override void Play()
        {
            if (this.IsMediaPluginPlayReadyForPlaying)
            {
                if (this.bufferedPosition.HasValue)
                {
                    this.mediaPlugin.Position = this.bufferedPosition.Value;
                    this.bufferedPosition = null;
                }

                this.playEnqueued = false;
                this.mediaPlugin.Play();
                this.Playing = true;
            }
            else
            {
                this.playEnqueued = true;
            }
        }

        /// <summary>
        /// Pauses this <see cref="PlayableMediaData"/>.
        /// </summary>
        public override void Pause()
        {
            if (this.mediaPlugin.CurrentState == MediaPluginState.Playing)
            {
                this.mediaPlugin.Pause();
            }

            this.Playing = false;
        }

        public override void FastRewind()
        {
            this.mediaPlugin.FastRewind();
        }

        public override void FastForward()
        {
            this.mediaPlugin.FastForward();
        }

        public override void SetStreamSource(Stream manifestStream)
        {
            base.SetStreamSource(manifestStream);
            this.mediaPlugin.SetManifestStreamSource(manifestStream);
            this.sourceChanging = true;
        }

        public override void SetSource(Uri source)
        {
            base.SetSource(source);
            this.mediaPlugin.AdaptiveSource = source;
            this.mediaPlugin.Stop();
            this.sourceChanging = true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Indicates if Dispose is being called from the Dispose method.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !this.disposed)
            {
                this.mediaPlugin.CurrentStateChanged -= this.MediaPluginCurrentStateChanged;
                this.mediaPlugin.SeekCompleted -= this.MediaPluginSeekCompleted;
                this.mediaPlugin.Unload();
                this.mediaPlugin.Dispose();
                this.mediaPlugin = null;

                this.disposed = true;
            }
        }

        /// <summary>
        /// Triggers MediaFailed event.
        /// </summary>
        protected void OnMediaFailed(IMediaPlugin plugin)
        {
            Action<IMediaPlugin> mediaFailed = this.MediaPluginFailed;
            if (mediaFailed != null)
            {
                mediaFailed(plugin);
            }
        }

        /// <summary>
        /// Handles the CurrentStateChanged event of the media element.
        /// </summary>
        private void MediaPluginCurrentStateChanged(IMediaPlugin plugin, MediaPluginState mediaPluginState)
        {
            if (this.mediaPlugin.CurrentState == MediaPluginState.Stopped || this.mediaPlugin.CurrentState == MediaPluginState.Paused || this.mediaPlugin.CurrentState == MediaPluginState.Closed)
            {
                this.Playing = false;
            }

            lock (this.lastSeekPositionLock)
            {
                if (mediaPluginState != MediaPluginState.Buffering && this.lastSeekPosition.HasValue)
                {
                    plugin.Position = this.lastSeekPosition.Value;
                    this.lastSeekPosition = null;
                }
            }

            if (this.mediaPlugin.CurrentState == MediaPluginState.Buffering)
            {
                this.isBufferStarted = true;
                this.OnBufferStart();
            }
            else if (this.isBufferStarted)
            {
                this.OnBufferEnd();
                this.isBufferStarted = false;
            }
        }

        /// <summary>
        /// Handles the SeekCompleted event of the MediaElement. Stop seek animation.
        /// </summary>
        private void MediaPluginSeekCompleted(IMediaPlugin plugin, bool success)
        {
            this.OnBufferEnd();
        }

        /// <summary>
        /// Performs a seek to the given value.
        /// </summary>
        /// <param name="value">The value to seek.</param>
        private void DoActualSeek(TimeSpan value)
        {
            if (this.mediaPlugin != null && (this.mediaPlugin.CurrentState == MediaPluginState.Paused || this.mediaPlugin.CurrentState == MediaPluginState.Buffering || this.mediaPlugin.CurrentState == MediaPluginState.Playing || this.mediaPlugin.CurrentState == MediaPluginState.Opening))
            {
                double livePosition = this.mediaPlugin.IsSourceLive ? this.mediaPlugin.LivePosition.TotalSeconds : this.mediaPlugin.EndPosition.TotalSeconds;

                double time = Math.Min(livePosition, Math.Max(this.mediaPlugin.StartPosition.TotalSeconds, this.mediaPlugin.StartPosition.TotalSeconds + value.TotalSeconds));

                TimeSpan position = TimeSpan.FromSeconds(time);

                if (!this.mediaPlugin.IsSeeking)
                {
                    this.OnBufferStart();
                }

                this.mediaPlugin.Position = position;

                if (this.mediaPlugin.Position != position)
                {
                    lock (this.lastSeekPositionLock)
                    {
                        this.lastSeekPosition = position;
                    }
                }
            }
        }

        private void OnMediaOpened(IMediaPlugin obj)
        {
            this.sourceChanging = false;

            if (this.bufferedPosition.HasValue)
            {
                this.Position = this.bufferedPosition.Value;
                this.bufferedPosition = null;
            }

            if (this.playEnqueued)
            {
                this.Play();
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

        private void MediaPlugin_MediaFailed(IMediaPlugin arg1, Exception arg2)
        {
            this.OnMediaFailed(arg1);
        }
    }
}
