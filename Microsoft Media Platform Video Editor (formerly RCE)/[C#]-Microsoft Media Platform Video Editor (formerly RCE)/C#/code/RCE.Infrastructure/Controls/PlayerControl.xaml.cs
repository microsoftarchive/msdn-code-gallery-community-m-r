// <copyright file="PlayerControl.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: PlayerControl.xaml.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Threading;
    using Microsoft.Practices.ServiceLocation;
    using Microsoft.SilverlightMediaFramework.Core.Media;
    using Microsoft.SilverlightMediaFramework.Plugins;
    using Microsoft.SilverlightMediaFramework.Plugins.Primitives;

    using RCE.Infrastructure.Models;
    using RCE.Infrastructure.Services;

    using SMPTETimecode;

    using SmpteFrameRate = SMPTETimecode.SmpteFrameRate;

    /// <summary>
    /// Player control.
    /// </summary>
    public partial class PlayerControl : UserControl
    {
        /// <summary>
        /// Dependency Property for ExpandButton.
        /// </summary>
        public static readonly DependencyProperty DisplayExpandButtonProperty =
            DependencyProperty.Register("DisplayExpandButton", typeof(bool), typeof(PlayerControl), new PropertyMetadata(false, OnDisplayExpandButtonChangedCallback));

        /// <summary>
        /// Dependency Property to determine if all data streams should be used.
        /// </summary>
        public static readonly DependencyProperty UseAllDataStreamsProperty =
            DependencyProperty.Register("UseAllDataStreams", typeof(bool), typeof(PlayerControl), new PropertyMetadata(true, OnUseAllDataStreamsChangedCallback));

        /// <summary>
        /// Dependency Property to determine if the data streams should be parsed.
        /// </summary>
        public static readonly DependencyProperty ParseDataStreamsProperty =
            DependencyProperty.Register("ParseDataStreams", typeof(bool), typeof(PlayerControl), new PropertyMetadata(false, OnParseDataStreamsChangedCallback));

        public static readonly DependencyProperty IsMediaErrorExpandedProperty =
            DependencyProperty.Register("IsMediaErrorExpanded", typeof(bool), typeof(PlayerControl), new PropertyMetadata(false, OnIsMediaErrorExpandedChangedCallback));

        /// <summary>
        /// Timer to handle forward/backward the media element.
        /// </summary>
        private readonly DispatcherTimer timer;

        /// <summary>
        /// Timer to trigger position change event.
        /// </summary>
        private readonly DispatcherTimer positionTimer;

               /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger logger;

        private readonly ICodecPrivateDataParser codecPrivateDataParser;

        // The SSME has an internal limit where the the playback rate cannot be negative
        private readonly TimeSpan rewindLimit = TimeSpan.FromSeconds(1);

        /// <summary>
        /// Timeout in Milliseconds to Merge Manifest.
        /// </summary>
        private const int MergeManifestTimeout = 2000;

        /// <summary>
        /// Current Media Element to be played.
        /// </summary>
        private IRCESmoothStreamingMediaPlugin mediaPlugin;

        /// <summary>
        /// The value would be 1 if forward and -1 if backward.
        /// </summary>
        private int currentSkipDirection;

        /// <summary>
        /// True the media element is playing; otherwise false.
        /// </summary>
        private bool isPlaying;

        /// <summary>
        /// Current SmpteFrameRate.
        /// </summary>
        private SmpteFrameRate currentSmpteFrameRate;

        /// <summary>
        /// True when seeking to live; otherwise false.
        /// </summary>
        private bool seekingToLive;

        /// <summary>
        /// The list of external manifest files.
        /// </summary>
        private IList<Uri> externalManifestFiles;

        private bool isLive;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerControl"/> class.
        /// </summary>
        public PlayerControl()
        {
            InitializeComponent();
            this.timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 1) };
            this.timer.Tick += this.Timer_Tick;
            this.positionTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(UtilityHelper.PositionUpdateIntervalMillis) };
            this.positionTimer.Tick += this.PositionTimer_Tick;
            this.currentSmpteFrameRate = SmpteFrameRate.Smpte2997NonDrop;
            this.externalManifestFiles = new List<Uri>();

            if (!DesignerProperties.IsInDesignMode)
            {
                this.logger = ServiceLocator.Current.GetInstance(typeof(ILogger)) as ILogger;
                this.codecPrivateDataParser = ServiceLocator.Current.GetInstance(typeof(ICodecPrivateDataParser)) as ICodecPrivateDataParser;
                
                var configurationSerive = ServiceLocator.Current.GetInstance<IConfigurationService>();
                if (configurationSerive != null)
                {
                    this.ShowFragmentBoundariesButton.Visibility = configurationSerive.GetSnapToFragmentBoundaries()
                        ? Visibility.Visible
                        : this.ShowFragmentBoundariesButton.Visibility;

                    this.currentSmpteFrameRate = configurationSerive.GetDefaultFrameRate();
                }
            }
        }

        /// <summary>
        /// Occurs when [metadata click].
        /// </summary>
        public event EventHandler MetadataClick;

        /// <summary>
        /// Occurs when play button is clicked.
        /// </summary>
        public event EventHandler StartMediaPlay;

        /// <summary>
        /// Occurs when [expand to full screen].
        /// </summary>
        public event EventHandler ExpandToFullScreen;

        /// <summary>
        /// Occurs when Media position changes.
        /// </summary>
        public event EventHandler MediaPositionChanged;

        /// <summary>
        /// Occurs when the Seek to live is completed.
        /// </summary>
        public event EventHandler<DataEventArgs<bool>> LiveSeekCompleted;

        /// <summary>
        /// Occurs when the MarkIn or MarkOut button is clicked.
        /// </summary>
        public event EventHandler<DataEventArgs<ScrubShiftType>> SetSubMarkClipClicked;

        /// <summary>
        /// Occurs when the PlaySubClip button is clicked.
        /// </summary>
        public event EventHandler PlaySubClipClicked;

        /// <summary>
        /// Occures when the GoToTimeCode button is clicked.
        /// </summary>
        public event EventHandler GoToTimeCodeClicked;

        /// <summary>
        /// Occurs when the FrameRate is being parsed.
        /// </summary>
        public event EventHandler<DataEventArgs<SmpteFrameRate>> FrameRateParsed;

        public event RoutedEventHandler MediaErrorExpandedChanged;

        public event EventHandler SelectedAudioStreamChanged;

        /// <summary>
        /// Occurs when the ShowFragmentBoundaries button is clicked.
        /// </summary>
        public event EventHandler<DataEventArgs<bool>> ShowFragmentBoundariesChanged;

        /// <summary>
        /// Gets or sets the media element.
        /// </summary>
        /// <value>The media element.</value>
        [CLSCompliant(false)]
        public IRCESmoothStreamingMediaPlugin MediaPlugin
        {
            get
            {
                return this.mediaPlugin;
            }

            set
            {
                this.mediaPlugin = value;
                if (value != null)
                {
                    ILogEntryCollection collection = ServiceLocator.Current.GetInstance<ILogEntryCollection>();
                    collection.UseAllLogStreams = this.UseAllDataStreams;
                    this.InStreamData = collection;
                    if (this.ParseDataStreams)
                    {
                        this.InStreamData.SetLogEntriesSource(this.MediaPlugin);
                    }

                    this.MediaPlugin.MediaOpened += this.MediaElement_MediaOpened;
                    this.MediaPlugin.MediaEnded += this.MediaElement_MediaEnded;
                    this.MediaPlugin.SeekCompleted += this.MediaElement_SeekCompleted;
                    this.MediaPlugin.ManifestMerge += this.MediaElement_ManifestMerge;
                    this.MediaPlugin.AdaptiveStreamingErrorOccurred += this.MediaElement_SmoothStreamingErrorOccurred;
                    this.MediaPlugin.MediaFailed += this.MediaElement_MediaFailed;
                    this.MediaPlugin.PlaybackRateChanged += this.MediaElement_PlaybackRateChanged;
                    this.MediaPlugin.ManifestReady += this.MediaPlugin_ManifestReady;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Player is currently playing a live stream or not.
        /// </summary>
        /// <value>A true if the presentation is Live;otherwise false.</value>
        public bool IsLive
        {
            get
            {
                return this.isLive;
            }

            set
            {
                if (this.isLive != value)
                {
                    this.isLive = value;
                    this.buttonGoToLive.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
                }
            } 
        }

        /// <summary>
        /// Sets a value indicating whether the Player should show adaptative operations or not..
        /// </summary>
        /// <value>A true if the asset is adaptive;otherwise false.</value>
        public bool IsAdaptiveAsset
        {
            set
            {
                Visibility visibility = value ? Visibility.Visible : Visibility.Collapsed;

                this.SlowMotionButton.Visibility = visibility;
                this.FastRewindButton.Visibility = visibility;
                this.FastForwardButton.Visibility = visibility;
            }
        }

        /// <summary>
        /// Sets a value indicating whether the Mark In and Mark Out buttons should be visible or not.
        /// </summary>
        /// <value>A true if the controls are visible;otherwise false.</value>
        public bool HasSubClipControls
        {
            set
            {
                Visibility visibility = value ? Visibility.Visible : Visibility.Collapsed;

                this.MarkInButton.Visibility = visibility;
                this.MarkOutButton.Visibility = visibility;
                this.PlaySubClipButton.Visibility = visibility;
            } 
        }

        public bool HasGoToButton
        {
            set
            {
                Visibility visibility = value ? Visibility.Visible : Visibility.Collapsed;

                this.GoToTimecodeButton.Visibility = visibility;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Player is in PlaySubClipMode or not.
        /// </summary>
        /// <value>A true if the player is in play subclip mode;otherwise false.</value>
        public bool IsPlaySubClipMode
        {
            get { return this.PlaySubClipButton.IsChecked.GetValueOrDefault(); }
            set { this.PlaySubClipButton.IsChecked = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Expand button should be displayed.
        /// </summary>
        /// <value>
        /// A <seealso cref="bool"/> value. <c>true</c> if [Expand button should be displayed]; otherwise, <c>false</c>.
        /// </value>
        public bool DisplayExpandButton
        {
            get
            {
                return (bool)this.GetValue(DisplayExpandButtonProperty);
            }

            set
            {
                this.SetValue(DisplayExpandButtonProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether all data streams should be used or not.
        /// </summary>
        /// <value>A true if all data streams should be used;otherwise false.</value>
        public bool UseAllDataStreams
        {
            get
            {
                return (bool)this.GetValue(UseAllDataStreamsProperty);
            }

            set
            {
                this.SetValue(UseAllDataStreamsProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the data streams should be parsed or not.
        /// </summary>
        /// <value>A true if the data streams should be parsed;otherwise false.</value>
        public bool ParseDataStreams
        {
            get { return (bool)this.GetValue(ParseDataStreamsProperty); }
            set { this.SetValue(ParseDataStreamsProperty, value); }
        }

        public bool IsMediaErrorExpanded
        {
            get { return (bool)GetValue(IsMediaErrorExpandedProperty); }
            set { SetValue(IsMediaErrorExpandedProperty, value); }
        }

        public bool IsPlaying
        {
            get
            {
                return this.isPlaying;
            }
        }

        /// <summary>
        /// Gets the in strema collection containing the events.
        /// </summary>
        /// <value>The collection of events.</value>
        public ILogEntryCollection InStreamData { get; private set; }

        /// <summary>
        /// Gets or sets the position of the Player.
        /// </summary>
        /// <value>The position.</value>
        public TimeSpan Position
        {
            get
            {
                return this.mediaPlugin.Position;
            }

            set
            {
                if (this.mediaPlugin != null && this.mediaPlugin.CurrentState != MediaPluginState.Opening)
                {
                    double time = value.TotalSeconds;

                    if (this.mediaPlugin.IsSourceLive)
                    {
                        double suggestedLivePosition = this.mediaPlugin.LivePosition.TotalSeconds;
                        time = Math.Min(suggestedLivePosition, Math.Max(this.mediaPlugin.StartPosition.TotalSeconds, time));
                    }

                    this.mediaPlugin.Position = TimeSpan.FromSeconds(time);
                }
            }
        }

        public string SelectedAudioStreamName
        {
            get
            {
                if (this.MediaPlugin != null && this.MediaPlugin.SelectedAudioStream != null)
                {
                    return this.MediaPlugin.SelectedAudioStream.Name;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has source.
        /// </summary>
        /// <value>
        /// <c>True</c> if this instance has source; otherwise, <c>false</c>.
        /// </value>
        private bool HasSource
        {
            get
            {
                return this.MediaPlugin != null;
            }
        }

        // flag, if the SSME can rewind the current position
        private bool CanRewind
        {
            get
            {
                if (this.mediaPlugin == null)
                {
                    return false;
                }

                return this.mediaPlugin.Position >= this.mediaPlugin.StartPosition + this.rewindLimit && this.mediaPlugin.PlaySpeedManager.SupportsNaturalRewind;
            }
        }

        /// <summary>
        /// Sets the source of the player.
        /// </summary>
        /// <param name="asset">The asset representing the source.</param>
        public void SetSource(Asset asset)
        {
            if (asset.IsAdaptiveAsset)
            {
                SmoothStreamingVideoAsset videoAsset = asset as SmoothStreamingVideoAsset;
                
                if (videoAsset != null)
                {
                    this.externalManifestFiles = videoAsset.ExternalManifests;
                    videoAsset.DataStreams.ForEach(this.InStreamData.LogStreams.Add);
                }

                this.MediaPlugin.AdaptiveSource = asset.Source;
            }
            else
            {
                this.MediaPlugin.Source = asset.Source;
            }
        }

        public void SetVideoCamera(string camera)
        {
            this.MediaPlugin.VideoStreamName = camera;
        }

        public void SetAudioStream(string audioStream)
        {
            if (this.MediaPlugin.AvailableAudioStreams != null)
            {
                this.MediaPlugin.SelectedAudioStream = this.MediaPlugin.AvailableAudioStreams
                    .FirstOrDefault(s => string.Equals(s.Name, audioStream, StringComparison.OrdinalIgnoreCase));                
            }
        }

        /// <summary>
        /// Sets the smpte frame rate.
        /// </summary>
        /// <param name="frameRate">The frame rate.</param>
        public void SetSmpteFrameRate(SmpteFrameRate frameRate)
        {
            if (frameRate != SmpteFrameRate.Unknown)
            {
                this.currentSmpteFrameRate = frameRate;
            }
        }

        /// <summary>
        /// Plays the media.
        /// </summary>
        public void PlayMedia()
        {
            if (!this.isPlaying)
            {
                this.TogglePlay(); 
            }
        }

        /// <summary>
        /// Enables the Play button on the value of isEnabled.
        /// </summary>
        /// <param name="isEnabled">A boolen value indicating to enable the button or not</param>
        public void EnablePlayButton(bool isEnabled)
        {
            this.PlayButton.IsEnabled = isEnabled;
        }

        /// <summary>
        /// Stops the media.
        /// </summary>
        public void StopMedia()
        {
            if (this.HasSource)
            {
                try
                {
                    this.MediaPlugin.Stop();
                }
                catch (Exception ex)
                {
                    this.logger.Log("PlayerControl", ex);
                }

                this.MediaPlugin.MediaOpened -= this.MediaElement_MediaOpened;
                this.MediaPlugin.MediaEnded -= this.MediaElement_MediaEnded;
                this.MediaPlugin.ManifestMerge -= this.MediaElement_ManifestMerge;
                this.MediaPlugin.SeekCompleted -= this.MediaElement_SeekCompleted;
                this.MediaPlugin.MediaFailed -= this.MediaElement_MediaFailed;
                this.MediaPlugin.AdaptiveStreamingErrorOccurred -= this.MediaElement_SmoothStreamingErrorOccurred;
                this.MediaPlugin.PlaybackRateChanged -= this.MediaElement_PlaybackRateChanged;
                this.MediaPlugin.ManifestReady -= this.MediaPlugin_ManifestReady;

                this.InStreamData.Dispose();
                this.isPlaying = false;
                this.MediaPlugin.Unload();
                this.MediaPlugin.Dispose();
                this.MediaPlugin = null;
                this.PlayButton.Visibility = Visibility.Visible;
                this.PauseButton.Visibility = Visibility.Collapsed;

                this.StopPositionTimer();
            }
        }

        /// <summary>
        /// Pauses the media.
        /// </summary>
        public void PauseMedia()
        {
            if (this.isPlaying)
            {
                this.TogglePlay();
            }
        }

        public void RetrySource(Asset asset)
        {
            if (asset.IsAdaptiveAsset)
            {
                this.MediaPlugin.AdaptiveSource = null;

                this.MediaPlugin.AutoPlay = true;

                this.MediaPlugin.AdaptiveSource = asset.Source;
            }
        }

        /// <summary>
        /// Occurs when the DisplaExpandbuttonProperty changes.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="e">The event args instance containing event data.</param>
        private static void OnDisplayExpandButtonChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            PlayerControl playerControl = dependencyObject as PlayerControl;

            if (playerControl != null)
            {
                playerControl.OnDisplayExpandButtonChanged((bool)e.NewValue);
            }
        }

        private static void OnUseAllDataStreamsChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            PlayerControl playerControl = dependencyObject as PlayerControl;

            if (playerControl != null)
            {
                playerControl.OnUseAllDataStreamsChanged();
            }
        }

        private static void OnParseDataStreamsChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            PlayerControl playerControl = dependencyObject as PlayerControl;

            if (playerControl != null)
            {
                playerControl.OnParseDataStreamsChanged();
            }
        }

        private static void OnIsMediaErrorExpandedChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            PlayerControl playerControl = dependencyObject as PlayerControl;

            if (playerControl != null)
            {
                playerControl.OnIsMediaErrorExpandedChanged((bool)e.NewValue);
            }
        }

        /// <summary>
        /// Raises the FrameRateParsed event.
        /// </summary>
        /// <param name="frameRate">The frame rate included on the event args data.</param>
        private void OnFrameRateParsed(SmpteFrameRate frameRate)
        {
            EventHandler<DataEventArgs<SmpteFrameRate>> handler = this.FrameRateParsed;
            if (handler != null)
            {
                handler(this, new DataEventArgs<SmpteFrameRate>(frameRate));
            }
        }

        /// <summary>
        /// Toggles the Expand Visibility.
        /// </summary>
        /// <param name="value">A true if the button should be visible;othwersise false.</param>
        private void OnDisplayExpandButtonChanged(bool value)
        {
            this.Expand.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
        }

        private void OnUseAllDataStreamsChanged()
        {
            if (this.InStreamData != null)
            {
                this.InStreamData.UseAllLogStreams = this.UseAllDataStreams;
            }
        }

        private void OnParseDataStreamsChanged()
        {
            if (this.InStreamData != null)
            {
                this.InStreamData.SetLogEntriesSource(this.MediaPlugin);
            }
        }

        private void OnIsMediaErrorExpandedChanged(bool value)
        {
            if (value)
            {
                VisualStateManager.GoToState(this, "MediaErrorExpanded", true);
            }
            else
            {
                VisualStateManager.GoToState(this, "MediaErrorCollapsed", true);
            }
        }

        /// <summary>
        /// Handles the SmoothStreamingErrorOccurred event. Logs the exception.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event args instance containg the exception data.</param>
        private void MediaElement_SmoothStreamingErrorOccurred(IAdaptiveMediaPlugin adaptiveMediaPlugin, Exception exception)
        {
            if (exception != null)
            {
                this.logger.Log("PlayerControl", exception);
            }
        }

        private void MediaElement_MediaFailed(IMediaPlugin mediaPlugin, Exception exception)
        {
            this.StopPositionTimer();
            this.IsMediaErrorExpanded = true;

            if (exception != null)
            {
                this.logger.Log("PlayerControl", exception);
            }

            this.TogglePlay();

            this.OnMediaErrorExpandedChanged();
        }

        /// <summary>
        /// Handles the ManifestMerge event of the MediaElement.
        /// </summary>
        /// <param name="ssme">The sender media element.</param>
        private void MediaElement_ManifestMerge(IRCESmoothStreamingMediaPlugin plugin)
        {
            this.MergeExternalManifestFiles();
        }

        private void MediaPlugin_ManifestReady(IAdaptiveMediaPlugin adaptiveMediaPlugin)
        {
            const string FourCCAttributeName = "FourCC";
            const string CodecPrivateDataAttributeName = "CodecPrivateData";

            var currentSegment = adaptiveMediaPlugin.CurrentSegment;
            if (currentSegment != null)
            {
                var firstVideoStream = currentSegment.SelectedStreams.Where(s => s.Type == StreamType.Video).FirstOrDefault();
                if (firstVideoStream != null)
                {
                    var fourCC = firstVideoStream.FourCC;
                    var codecPrivateData = string.Empty;

                    if (string.IsNullOrWhiteSpace(fourCC) && !firstVideoStream.Attributes.TryGetValue(CodecPrivateDataAttributeName, out codecPrivateData))
                    {
                        var videoTrack = firstVideoStream.SelectedTracks.Where(t => t.Attributes.ContainsKey(FourCCAttributeName) && t.Attributes.ContainsKey(CodecPrivateDataAttributeName)).FirstOrDefault();
                        if (videoTrack != null)
                        {
                            fourCC = videoTrack.Attributes[FourCCAttributeName];
                            codecPrivateData = videoTrack.Attributes[CodecPrivateDataAttributeName];
                        }
                    }

                    try
                    {
                        var frameRate = this.codecPrivateDataParser.GetFrameRate(fourCC, codecPrivateData);
                        this.OnFrameRateParsed(frameRate);
                    }
                    catch (Exception ex)
                    {
                        this.logger.Log("PlayerControl", ex);
                    }
                }
            }
        }

        private void MediaElement_PlaybackRateChanged(IMediaPlugin mediaPlugin)
        {
            if (this.MediaPlugin.PlaySpeedManager.IsPlaySpeedNormal)
            {
                if (this.FastRewindButton.IsChecked.GetValueOrDefault())
                {
                    this.FastRewindButton.IsChecked = false;
                }

                if (this.FastForwardButton.IsChecked.GetValueOrDefault())
                {
                    this.FastForwardButton.IsChecked = false;
                }
            }
        }

        /// <summary>
        /// Merges the external manifest files with the current media element.
        /// </summary>
        private void MergeExternalManifestFiles()
        {
            if (this.MediaPlugin == null)
            {
                return;
            }

            foreach (Uri manifestFile in this.externalManifestFiles)
            {
                try
                {
                    object mergeManifest;
                    this.MediaPlugin.ParseExternalManifest(manifestFile, MergeManifestTimeout, out mergeManifest);
                    this.MediaPlugin.MergeExternalManifest(mergeManifest);
                }
                catch (Exception ex)
                {
                    this.logger.Log("PlayerControl", ex);
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the Pause button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            this.TogglePlay();
        }

        /// <summary>
        /// Handles the Click event of the Play button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Play_Click(object sender, RoutedEventArgs e)
        {   
            if (this.StartMediaPlay != null)
            {
                this.StartMediaPlay(this, EventArgs.Empty);
            }

            this.TogglePlay();
        }

        /// <summary>
        /// Handles the Click event of the Expand button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Expand_Click(object sender, RoutedEventArgs e)
        {
            // Expand the parent container to full screen mode.
            if (this.ExpandToFullScreen != null)
            {
                this.ExpandToFullScreen(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Handles the Click event of the Metadata button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Metadata_Click(object sender, RoutedEventArgs e)
        {
            if (this.MetadataClick != null)
            {
                this.MetadataClick(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the Forward button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void Forward_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this.HasSource)
            {
                this.currentSkipDirection = 1;
                this.MediaPlugin.Pause();
                this.timer.Start();

                this.StartPositionTimer();
            }

            e.Handled = true;
        }

        /// <summary>
        /// Handles the MouseLeftButtonUp event of the Forward button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void Forward_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this.HasSource)
            {
                this.currentSkipDirection = 0;
                this.timer.Stop();

                // this.positionTimer.Start();
                this.PlayPauseMediaElement();
            }

            e.Handled = true;
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the Rewind button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void Rewind_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this.HasSource)
            {
                this.currentSkipDirection = -1;
                this.MediaPlugin.Pause();
                this.timer.Start();

                this.StartPositionTimer();
            }

            e.Handled = true;
        }

        private void StartPositionTimer()
        {
            if (this.positionTimer != null && !this.positionTimer.IsEnabled)
            {
                this.positionTimer.Start();
            }
        }

        private void StopPositionTimer()
        {
            if (this.positionTimer != null && this.positionTimer.IsEnabled)
            {
                this.PositionTimer_Tick(null, null);
                this.positionTimer.Stop();
            }
        }

        /// <summary>
        /// Handles the MouseLeftButtonUp event of the Rewind button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void Rewind_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this.HasSource)
            {
                this.currentSkipDirection = 0;
                this.timer.Stop();

                // this.positionTimer.Start();
                this.PlayPauseMediaElement();
            }

            e.Handled = true;
        }

        /// <summary>
        /// Handles the Click event of the Mute button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Mute_Click(object sender, RoutedEventArgs e)
        {
            if (this.HasSource)
            {
                this.MediaPlugin.IsMuted = !this.MediaPlugin.IsMuted;
            }
        }

        /// <summary>
        /// Handles the Tick event of the Timer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (this.currentSkipDirection == 0)
            {
                return;
            }

            bool add = this.currentSkipDirection > 0;
            long newSkipDirection = 1;

            TimeCode currentTimeCode = TimeCode.FromTimeSpan(this.MediaPlugin.Position, this.currentSmpteFrameRate);
            long currentTotalFrames = currentTimeCode.TotalFrames;

            TimeCode frameTimeCode = TimeCode.FromFrames(newSkipDirection, this.currentSmpteFrameRate);

            currentTimeCode = add ? currentTimeCode.Add(frameTimeCode) : currentTimeCode.Subtract(frameTimeCode);
            newSkipDirection++;

            while (currentTimeCode.TotalFrames == currentTotalFrames)
            {
                frameTimeCode = TimeCode.FromFrames(newSkipDirection, this.currentSmpteFrameRate);
                currentTimeCode = add ? currentTimeCode.Add(frameTimeCode) : currentTimeCode.Subtract(frameTimeCode);
                newSkipDirection++;
            }

            this.MediaPlugin.Position = TimeSpan.FromSeconds(Math.Max(0, currentTimeCode.TotalSeconds));
            this.OnMediaPositionChanged();
        }

        /// <summary>
        /// Triggers the <see cref="MediaPositionChanged"/> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/>.</param>
        private void PositionTimer_Tick(object sender, EventArgs e)
        {
            this.UpdateDisplay();
            this.OnMediaPositionChanged();
        }

        /// <summary>
        /// Handles the MediaOpened event of the MediaElement control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void MediaElement_MediaOpened(IMediaPlugin mediaPlugin)
        {
            this.StartPositionTimer();
            this.mediaPlugin.IsMuted = this.MuteButton.IsChecked.HasValue ? this.MuteButton.IsChecked.Value : false;
            this.UpdateDisplay();
            this.PlayPauseMediaElement();
        }

        private void UpdateDisplay()
        {
            this.UpdateLiveMode();
            this.UpdateRewindButton();
            this.UpdateFastForwardButton();
        }

        private void UpdateLiveMode()
        {
            if (this.mediaPlugin == null)
            {
                this.IsLive = false;
                return;
            }

            this.IsLive = this.mediaPlugin.IsSourceLive;

            if (this.IsLive && this.mediaPlugin.IsLivePosition)
            {
                if (!this.mediaPlugin.PlaySpeedManager.IsPlaySpeedNormal)
                {
                    this.mediaPlugin.PlaySpeedManager.RestoreNaturalPlaySpeed();
                }
            }
        }

        // SSME has a rewind limit where the playback rate cannot be set to a
        // negative value, disable the rewind control if within the limit
        private void UpdateRewindButton()
        {
            if (this.mediaPlugin != null && this.FastRewindButton != null)
            {
                this.FastRewindButton.IsEnabled = this.CanRewind;

                if (!this.FastRewindButton.IsEnabled)
                {
                    if (!this.mediaPlugin.PlaySpeedManager.IsPlaySpeedNormal)
                    {
                        this.mediaPlugin.PlaySpeedManager.RestoreNaturalPlaySpeed();
                    }

                    this.FastRewindButton.IsChecked = false;
                }
            }
        }

        private void UpdateFastForwardButton()
        {
            if (this.mediaPlugin != null && this.FastForwardButton != null)
            {
                this.FastForwardButton.IsEnabled = !this.mediaPlugin.IsLivePosition;

                if (!this.FastForwardButton.IsEnabled)
                {
                    if (!this.mediaPlugin.PlaySpeedManager.IsPlaySpeedNormal)
                    {
                        this.mediaPlugin.PlaySpeedManager.RestoreNaturalPlaySpeed();
                    }

                    this.FastForwardButton.IsChecked = false;
                }
            }
        }

        /// <summary>
        /// Handles the MediaEnded event of the MediaElement control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void MediaElement_MediaEnded(IMediaPlugin mediaPlugin)
        {
           this.StopPositionTimer();
           this.TogglePlay();

           if (this.mediaPlugin != null)
           {
               if (!this.mediaPlugin.PlaySpeedManager.IsPlaySpeedNormal)
               {
                   this.mediaPlugin.PlaySpeedManager.RestoreNaturalPlaySpeed();
               }
           }
        }

        /// <summary>
        /// Toggles the state between play and pause.
        /// </summary>
        private void TogglePlay()
        {
            if (this.isPlaying)
            {
                this.isPlaying = false;
                this.PlayButton.Visibility = Visibility.Visible;
                this.PauseButton.Visibility = Visibility.Collapsed;

                if (this.MediaPlugin != null)
                {
                    this.MediaPlugin.Pause();
                    this.OnMediaPositionChanged();
                }
            }
            else
            {
                this.isPlaying = true;
                this.PlayButton.Visibility = Visibility.Collapsed;
                this.PauseButton.Visibility = Visibility.Visible;
            }

            this.PlayPauseMediaElement();
        }

        /// <summary>
        /// Plays/Pause the media element corresponding to the <see cref="isPlaying"/> flag.
        /// </summary>
        private void PlayPauseMediaElement()
        {
            if (this.MediaPlugin != null)
            {
                if (this.isPlaying)
                {
                    if (this.MediaPlugin.PlaySpeedManager.IsFastForwarding || this.MediaPlugin.PlaySpeedManager.IsRewinding)
                    {
                        this.MediaPlugin.PlaySpeedManager.RestoreNaturalPlaySpeed();
                    }

                    this.StartPositionTimer();

                    this.MediaPlugin.Play();
                }
                else
                {
                    this.StopPositionTimer();
                    this.MediaPlugin.Pause();
                }
            }
        }

        /// <summary>
        /// Triggers the <see cref="MediaPositionChanged"/> event.
        /// </summary>
        private void OnMediaPositionChanged()
        {
            EventHandler mediaPositionChanged = this.MediaPositionChanged;
            if (mediaPositionChanged != null)
            {
                mediaPositionChanged(this, EventArgs.Empty);
            }
        }
        
        /// <summary>
        /// Handles the Click event of the SlowMotion button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args instance containing event data.</param>
        private void SlowMotion_Click(object sender, RoutedEventArgs e)
        {
            if (this.MediaPlugin != null)
            {
                this.MediaPlugin.SlowMotion();
                this.SlowMotionButton.IsChecked = this.MediaPlugin.PlaySpeedManager.IsSlowMotion;
            }
        }

        /// <summary>
        /// Handles the Click event of the FastRewind button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args instance containing event data.</param>
        private void FastRewind_Click(object sender, RoutedEventArgs e)
        {
            if (this.MediaPlugin != null)
            {
                if (this.MediaPlugin.CurrentState == MediaPluginState.Paused)
                {
                    this.TogglePlay();
                }

                this.MediaPlugin.FastRewind();
                this.FastRewindButton.IsChecked = this.MediaPlugin.PlaySpeedManager.IsRewinding;
            }
        }

        /// <summary>
        /// Handles the FastForward event of the SlowMotion button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args instance containing event data.</param>
        private void FastForward_Click(object sender, RoutedEventArgs e)
        {
            if (this.MediaPlugin != null)
            {
                if (this.MediaPlugin.CurrentState == MediaPluginState.Paused)
                {
                    this.TogglePlay();
                }

                this.MediaPlugin.FastForward();
                this.FastForwardButton.IsChecked = this.MediaPlugin.PlaySpeedManager.IsFastForwarding;
            }
        }

        /// <summary>
        /// Handles the Click event of the GoToLive button. Tries to go to the live position of the current media element.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event args instance cointaining event data.</param>
        private void GoToLive_Click(object sender, RoutedEventArgs e)
        {
            if (this.MediaPlugin != null && this.MediaPlugin.IsSourceLive && !this.MediaPlugin.IsLivePosition)
            {
                lock (this)
                {
                    if (!this.seekingToLive)
                    {
                        this.seekingToLive = true;
                        this.MediaPlugin.StartSeekToLive();
                    }
                }
            }
        }

        /// <summary>
        /// Handles the SeekCompleted event of the <see cref="CoreSmoothStreamingMediaElement"/> media element.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event args instance cointaining event data.</param>
        private void MediaElement_SeekCompleted(IMediaPlugin mediaPlugin, bool success)
        {
            if (this.seekingToLive)
            {
                this.seekingToLive = false;
                
                this.OnLiveSeekCompleted(success);
            }
        }

        /// <summary>
        /// Raises the LiveSeekCompleted event.
        /// </summary>
        /// <param name="success">Indicates if the seek was succesful or not.</param>
        private void OnLiveSeekCompleted(bool success)
        {
            EventHandler<DataEventArgs<bool>> completed = this.LiveSeekCompleted;

            if (completed != null)
            {
                completed(this, new DataEventArgs<bool>(success));
            }
        }

        /// <summary>
        /// Handles the PlaySubClip event. Pause the player when the Player is in SubClipMode. 
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event args instance containing event data.</param>
        private void PlaySubClip_Click(object sender, RoutedEventArgs e)
        {
            if (this.IsPlaySubClipMode)
            {
                this.PauseMedia();
                this.OnPlaySubClipClicked();
            }
        }

        /// <summary>
        /// Handles the Click event of the MarkInButton.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event args instance containing event data.</param>
        private void MarkIn_Click(object sender, RoutedEventArgs e)
        {
            this.OnSetSubMarkClipClicked(ScrubShiftType.In);
        }

        /// <summary>
        /// Handles the Click event of the MarkOutButton.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event args instance containing event data.</param>
        private void MarkOut_Click(object sender, RoutedEventArgs e)
        {
            this.OnSetSubMarkClipClicked(ScrubShiftType.Out);
        }

        /// <summary>
        /// Handles the Click event of the ShowFragmentBoundariesButton.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event args instance containing event data.</param>
        private void ShowFragmentBoundariesButton_Click(object sender, RoutedEventArgs e)
        {
            this.OnShowFragmentBoundariesClicked(this.ShowFragmentBoundariesButton.IsChecked ?? false);
        }

        /// <summary>
        /// Raises the ShowFragmentBoundariesChanged event.
        /// </summary>
        /// <param name="showFragmentBoundaries">The event data.</param>
        private void OnShowFragmentBoundariesClicked(bool showFragmentBoundaries)
        {
            EventHandler<DataEventArgs<bool>> clicked = this.ShowFragmentBoundariesChanged;
            if (clicked != null)
            {
                clicked(this, new DataEventArgs<bool>(showFragmentBoundaries));
            }
        }

        /// <summary>
        /// Raises the SubMarkClicpClicked event.
        /// </summary>
        /// <param name="scrubShiftType">The event data.</param>
        private void OnSetSubMarkClipClicked(ScrubShiftType scrubShiftType)
        {
            EventHandler<DataEventArgs<ScrubShiftType>> clicked = this.SetSubMarkClipClicked;
            if (clicked != null)
            {
                clicked(this, new DataEventArgs<ScrubShiftType>(scrubShiftType));
            }
        }

        /// <summary>
        /// Raises the PlaySubClipClicked event.
        /// </summary>
        private void OnPlaySubClipClicked()
        {
            EventHandler handler = this.PlaySubClipClicked;

            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private void OnGoToTimeCodeClicked()
        {
            EventHandler handler = this.GoToTimeCodeClicked;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private void OnMediaErrorExpandedChanged()
        {
            RoutedEventHandler handler = this.MediaErrorExpandedChanged;
            if (handler != null)
            {
                handler(this, new RoutedEventArgs());
            }
        }

        private void GoToTimecodeButton_Click(object sender, RoutedEventArgs e)
        {
            this.OnGoToTimeCodeClicked();
        }

        private void OnSelectedAudioStreamChanged()
        {
            EventHandler handler = this.SelectedAudioStreamChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}
