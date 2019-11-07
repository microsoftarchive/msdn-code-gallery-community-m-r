// <copyright file="PreviewTimeline.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: PreviewTimeline.xaml.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.SubClip.Previews
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media.Imaging;
    using System.Windows.Threading;
    using Microsoft.Practices.Composite.Events;
    using Microsoft.Practices.Composite.Presentation.Events;
    using Microsoft.Practices.Composite.Presentation.Regions;
    using Microsoft.Practices.Composite.Regions;
    using Microsoft.Practices.ServiceLocation;
    using Microsoft.SilverlightMediaFramework.Plugins;
    using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
    using RCE.Infrastructure;
    using RCE.Infrastructure.Events;
    using RCE.Infrastructure.Models;
    using RCE.Infrastructure.Services;
    using RCE.Modules.SubClip.Models;
    using SMPTETimecode;

    /// <summary>
    /// Control to display a video file.
    /// </summary>
    public partial class PreviewTimeline : AssetPreview
    {
        /// <summary>
        /// The Seperator used between the position and duration of the video.
        /// </summary>
        private const string DurationSeparator = " | ";

        /// <summary>
        /// Total margin value in x coordinate.
        /// </summary>
        private const double MarginX = 0;

        /// <summary>
        /// Total margin value in y coordinate.
        /// </summary>
        private const double MarginY = 0;

        /// <summary>
        /// The <see cref="IThumbnailService"/> service used to get the asset thumbnail frame.
        /// </summary>
        private readonly IThumbnailService thumbnailService;

        /// <summary>
        /// DependencyProperty for Asset.
        /// </summary>
        private static readonly DependencyProperty AssetProperty =
                DependencyProperty.RegisterAttached("Asset", typeof(Asset), typeof(PreviewTimeline), new PropertyMetadata(AssetChanged));

        /// <summary>
        /// DependencyProperty for Asset.
        /// </summary>
        private static readonly DependencyProperty InStreamDataProperty =
                DependencyProperty.RegisterAttached("InStreamData", typeof(ILogEntryCollection), typeof(PreviewTimeline), null);

        /// <summary>
        /// DependencyProperty for Asset.
        /// </summary>
        private static readonly DependencyProperty VideoInOutProperty =
                DependencyProperty.RegisterAttached("VideoInOut", typeof(VideoAssetInOut), typeof(PreviewTimeline), null);

        /// <summary>
        /// DependencyProperty for PlayheadPosition.
        /// </summary>
        private static readonly DependencyProperty PlayheadPositionProperty =
                DependencyProperty.RegisterAttached("PlayheadPosition", typeof(TimeCode), typeof(PreviewTimeline), null);

        /// <summary>
        /// The configuration service.
        /// </summary>
        private readonly IConfigurationService configurationService;

        private readonly ICacheManager cacheManager;

        private readonly IEventAggregator eventAggregator;

        private const double ZoomValue = 2;

        /// <summary>
        /// Flag to maintain if the storyboard have been loaded.
        /// </summary>
        private bool interfaceLoaded;

        /// <summary>
        /// True if the <see cref="VideoPreview"/> is loaded.
        /// </summary>
        private bool isLoaded;

        /// <summary>
        /// Indicates if the media element is buffering or not.
        /// </summary>
        private bool isBuffering;

        /// <summary>
        /// Current scale size(Current Preview size).
        /// </summary>
        private Size scaleSize;

        /// <summary>
        /// If the asset is in fullscreen or not.
        /// </summary>
        private bool isFullScreen;

        private IRegionManager regionManager;

        private AudioAssetInOut audioInOut;

        private SubscriptionToken videoPreviewTimelineEventToken;

        private KeyboardAction? action;

        private DispatcherTimer frameRewindForwardTimer;

        private ISequenceRegistry sequenceRegistry;

        private TimeSpan? lastSeekPosition;

        private object lastSeekPositionLock = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="PreviewTimeline"/> class.
        /// </summary>
        public PreviewTimeline()
        {
            InitializeComponent();
            if (!System.ComponentModel.DesignerProperties.IsInDesignTool)
            {
                Application.Current.RootVisual.MouseWheel += this.RootVisual_MouseWheel;
                this.Timeline.MovingPlayHead += this.Timeline_MovingPlayHead;
                this.Timeline.PositionChange += this.Timeline_PositionChange;
                this.Timeline.Resized += this.Timeline_Resized;
                this.Player.StartMediaPlay += this.Player_StartPlay;
                this.Player.ExpandToFullScreen += this.Player_ExpandToFullScreen;
                this.Player.MediaPositionChanged += this.Player_MediaPositionChanged;
                this.Player.LiveSeekCompleted += this.Player_LiveSeekCompleted;
                this.Player.SetSubMarkClipClicked += this.Player_SetSubMarkClipClicked;
                this.Player.PlaySubClipClicked += this.Player_PlaySubClipClicked;
                this.Player.FrameRateParsed += this.Player_FrameRateParsed;
                this.Player.MediaErrorExpandedChanged += this.Player_MediaErrorExpandedChanged;
                this.Player.GoToTimeCodeClicked += this.PlayerGoToTimeCodeClicked;
                this.Player.SelectedAudioStreamChanged += (s, e) => this.UpdateVideoInOutProperties();
                this.Player.ShowFragmentBoundariesChanged += this.Player_ShowFragmentBoundariesChanged;
                this.Player.HasSubClipControls = true;
                this.Player.HasGoToButton = true;

                this.configurationService =
                    ServiceLocator.Current.GetInstance(typeof(IConfigurationService)) as IConfigurationService;
                this.thumbnailService =
                    ServiceLocator.Current.GetInstance(typeof(IThumbnailService)) as IThumbnailService;
                this.cacheManager = ServiceLocator.Current.GetInstance(typeof(ICacheManager)) as ICacheManager;

                this.sequenceRegistry = ServiceLocator.Current.GetInstance(typeof(ISequenceRegistry)) as ISequenceRegistry;

                this.frameRewindForwardTimer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 1) };
                this.frameRewindForwardTimer.Tick += this.FrameRewindForwardTimerTick;

                this.eventAggregator = (IEventAggregator)ServiceLocator.Current.GetInstance(typeof(IEventAggregator));

                this.videoPreviewTimelineEventToken =
                    this.eventAggregator.GetEvent<VideoPreviewTimelineEvent>().Subscribe(
                        this.HandleVideoPreviewTimelineEvent,
                        ThreadOption.UIThread,
                        false);

                this.eventAggregator.GetEvent<MetadataEventSelected>().Subscribe(this.OnMetadataEventSelected, ThreadOption.PublisherThread, true, this.FilterMetadaEventSelected);
            }
        }

        ~PreviewTimeline()
        {
            if (!System.ComponentModel.DesignerProperties.IsInDesignTool)
            {
                this.eventAggregator.GetEvent<VideoPreviewTimelineEvent>().Unsubscribe(this.videoPreviewTimelineEventToken);
            }
        }

        /// <summary>
        /// Occurs when user clicks on FullScreen button of the <see cref="Player"/>.
        /// </summary>
        public event EventHandler TogglingFullScreen;

        /// <summary>
        /// Occurs when user clicks on Stop button of the <see cref="Player"/>.
        /// </summary>
        public event EventHandler Stopping;

        /// <summary>
        /// Occurs when user clicks on Play button of the <see cref="Player"/>.
        /// </summary>
        public event EventHandler Playing;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is full screen.
        /// </summary>
        /// <value>
        /// A <seealso cref="bool"/> value.<c>true</c> if this instance is full screen; otherwise, <c>false</c>.
        /// </value>
        public bool IsFullScreen
        {
            get
            {
                return this.isFullScreen;
            }

            set
            {
                this.isFullScreen = value;
            }
        }

        /// <summary>
        /// Gets the Mark In position of the asset.
        /// </summary>
        /// <value>The Mark In position.</value>
        public TimeCode InPosition
        {
            get
            {
                return this.Timeline.InPosition;
            }
        }

        /// <summary>
        /// Gets the Mark Out position of the asset.
        /// </summary>
        /// <value>The oMark Out position.</value>
        public TimeCode OutPosition
        {
            get
            {
                return this.Timeline.OutPosition;
            }
        }

        /// <summary>
        /// Gets or sets the asset.
        /// </summary>
        /// <value>The asset.</value>
        public override Asset Asset
        {
            get { return GetAsset(this); }
            set { SetAsset(this, value); }
        }

        public ILogEntryCollection InStreamData
        {
            get
            {
                return GetInStreamData(this);
            }

            set
            {
                SetInStreamData(this, value);
            }
        }

        public TimeCode PlayheadPosition
        {
            get
            {
                return GetPlayheadPosition(this);
            }

            set
            {
                SetPlayheadPosition(this, value);
            }
        }

        public ISubClipAsset SubClipAsset
        {
            get
            {
                if (this.Asset is VideoAsset)
                {
                    return this.VideoInOut;
                }

                return this.AudioInOut;
            }
        }

        /// <summary>
        /// Gets the VideoAssetInOut associdated with the current asset.
        /// </summary>
        /// <value>The current video asset sub clip.</value>
        public VideoAssetInOut VideoInOut
        {
            get
            {
                VideoAssetInOut videoInOut = GetVideoInOut(this);

                if (videoInOut == null)
                {
                    VideoAssetInOut originalVideoInOut = this.Asset as VideoAssetInOut;
                    if (originalVideoInOut == null)
                    {
                        videoInOut = new VideoAssetInOut(this.Asset as VideoAsset);
                    }
                    else
                    {
                        videoInOut = originalVideoInOut;
                    }

                    videoInOut.PropertyChanged += this.VideoInOut_PropertyChanged;
                    SetVideoInOut(this, videoInOut);
                }

                return videoInOut;
            }

            set
            {
                SetVideoInOut(this, value);
            }
        }

        public AudioAssetInOut AudioInOut
        {
            get
            {
                if (this.audioInOut == null)
                {
                    AudioAssetInOut originalVideoInOut = this.Asset as AudioAssetInOut;
                    if (originalVideoInOut == null)
                    {
                        this.audioInOut = new AudioAssetInOut(this.Asset as AudioAsset);
                    }
                    else
                    {
                        this.audioInOut = originalVideoInOut;
                    }
                }

                return this.audioInOut;
            }

            set
            {
                this.audioInOut = value;
            }
        }

        public static ILogEntryCollection GetInStreamData(DependencyObject obj)
        {
            return obj.GetValue(InStreamDataProperty) as ILogEntryCollection;
        }

        public static void SetInStreamData(DependencyObject obj, ILogEntryCollection value)
        {
            obj.SetValue(InStreamDataProperty, value);
        }

        public static TimeCode GetPlayheadPosition(DependencyObject obj)
        {
            var videoPreviewTimeline = obj as PreviewTimeline;
            TimeCode tc = (TimeCode)obj.GetValue(PlayheadPositionProperty);
            if (videoPreviewTimeline != null)
            {
                return TimeCode.FromSeconds(tc.TotalSeconds, videoPreviewTimeline.GetCurrentFrameRate());
            }

            return tc;
        }

        public static void SetPlayheadPosition(DependencyObject obj, TimeCode value)
        {
            obj.SetValue(PlayheadPositionProperty, value);
        }

        public static VideoAssetInOut GetVideoInOut(DependencyObject obj)
        {
            return obj.GetValue(VideoInOutProperty) as VideoAssetInOut;
        }

        public static void SetVideoInOut(DependencyObject obj, VideoAssetInOut value)
        {
            obj.SetValue(VideoInOutProperty, value);
        }

        /// <summary>
        /// Gets the asset.
        /// </summary>
        /// <param name="obj">The DependencyObject.</param>
        /// <returns>Returns the value of the Asset property.</returns>
        public static Asset GetAsset(DependencyObject obj)
        {
            return obj.GetValue(AssetProperty) as Asset;
        }

        /// <summary>
        /// Sets the asset.
        /// </summary>
        /// <param name="obj">The DependencyObject.</param>
        /// <param name="value">The <see cref="Asset"/>.</param>
        public static void SetAsset(DependencyObject obj, Asset value)
        {
            obj.SetValue(AssetProperty, value);
        }

        /// <summary>
        /// Stop the currently playing media element.
        /// </summary>
        public override void Stop()
        {
            if (this.Player.MediaPlugin != null)
            {
                this.AssetContainer.Children.Remove(this.Player.MediaPlugin.VisualElement);
                this.Player.MediaPlugin.MediaOpened -= this.MediaElement_MediaOpened;
                this.Player.MediaPlugin.CurrentStateChanged -= this.MediaElement_CurrentStateChanged;
                this.Player.MediaPlugin.SeekCompleted -= this.MediaElement_SeekCompleted;
                this.Player.MediaPlugin.DownloadProgressChanged -= this.MediaPlugin_DownloadProgressChanged;

                if (this.cacheManager != null)
                {
                    this.cacheManager.CacheUpdated -= this.OnCacheUpdated;
                    this.cacheManager.CacheRebuilt -= this.OnCacheRebuilt;
                }

                this.Player.StopMedia();
                this.Timeline.MediaPlugin = null;
                this.FramePreviewImage.Visibility = Visibility.Visible;
                this.ClearPreviewState();
                this.Timeline.Dispose();
                this.UpdateMediaError(false);
                this.OnStopping();
            }
        }

        public void HandleVideoPreviewTimelineEvent(KeyboardAction action)
        {
            switch (action)
            {
                case KeyboardAction.TogglePlay:
                    this.TogglePlay();
                    break;
                case KeyboardAction.PlayTimeline:
                    this.PlayPlayer();
                    break;
                case KeyboardAction.PausePlayer:
                    this.PausePlayer();
                    break;
                case KeyboardAction.SetMarkIn:
                    this.SetMarkIn(this.Timeline.CurrentPosition);
                    break;
                case KeyboardAction.SetMarkOut:
                    this.SetMarkOut(this.Timeline.CurrentPosition);
                    break;
                case KeyboardAction.RemoveMarkIn:
                    this.RemoveMarkIn();
                    break;
                case KeyboardAction.RemoveMarkOut:
                    this.RemoveMarkOut();
                    break;
                case KeyboardAction.RemoveMarkInAndOut:
                    this.RemoveMarkIn();
                    this.RemoveMarkOut();
                    break;
                case KeyboardAction.GoToIn:
                    this.Timeline.GoToPosition(TimeCode.FromAbsoluteTime(this.Timeline.InPosition.TotalSeconds, this.GetCurrentFrameRate()) - this.Timeline.StartOffset);
                    break;
                case KeyboardAction.GoToOut:
                    this.Timeline.GoToPosition(TimeCode.FromAbsoluteTime(this.Timeline.OutPosition.TotalSeconds, this.GetCurrentFrameRate()) - this.Timeline.StartOffset);
                    break;
                case KeyboardAction.NextFrame:
                    this.MoveTimelineOnFrames(1);
                    break;
                case KeyboardAction.PreviousFrame:
                    this.MoveTimelineOnFrames(-1);
                    break;
                case KeyboardAction.Mute:
                    this.Player.MediaPlugin.IsMuted = !this.Player.MediaPlugin.IsMuted;
                    break;
                case KeyboardAction.MoveToStart:
                    this.Timeline.GoToPosition(TimeCode.FromAbsoluteTime(0, this.GetCurrentFrameRate()));
                    break;
                case KeyboardAction.MoveToEnd:
                    this.Timeline.GoToPosition(this.Timeline.Duration);
                    break;
                case KeyboardAction.Rewind:
                    if (this.action == KeyboardAction.Rewind)
                    {
                        this.frameRewindForwardTimer.Stop();
                        this.action = null;
                    }
                    else
                    {
                        this.frameRewindForwardTimer.Start();
                        this.action = KeyboardAction.Rewind;
                    }

                    break;
                case KeyboardAction.Forward:
                    if (this.action == KeyboardAction.Forward)
                    {
                        this.frameRewindForwardTimer.Stop();
                        this.action = null;
                    }
                    else
                    {
                        this.frameRewindForwardTimer.Start();
                        this.action = KeyboardAction.Forward;
                    }

                    break;
                case KeyboardAction.ZoomIn:
                    this.Timeline.ZoomTimeline(ZoomValue);
                    break;
                case KeyboardAction.ZoomOut:
                    this.Timeline.ZoomTimeline(-ZoomValue);
                    break;
            }
        }

        public void TogglePlay()
        {
            if (!this.Player.IsPlaying)
            {
                this.PlayPlayer();
            }
            else
            {
                this.PausePlayer();
            }
        }

        public void PlayPlayer()
        {
            if (!this.Player.IsPlaying)
            {
                this.Player_StartPlay(this, null);
                this.Player.PlayMedia();
            }
        }

        public void PausePlayer()
        {
            this.Player.PauseMedia();
        }

        /// <summary>
        /// Scales the current <see cref="VideoPreview"/> to the specified size.
        /// </summary>
        /// <param name="size">The size to scale preview.</param>
        public override void Scale(Size size)
        {
            this.scaleSize = size;

            if (size.Width > MarginX && size.Width > MarginY)
            {
                VideoGrid.Width = size.Width;
                VideoGrid.Height = size.Height;

                Size previewSize = this.GetSizeMaintainingAspectRatio(size.Width, size.Height);
                FramePreviewImage.Width = previewSize.Width;
                FramePreviewImage.Height = previewSize.Height;

                if (this.Player.MediaPlugin != null && this.Player.MediaPlugin.VisualElement != null)
                {
                    this.Player.MediaPlugin.VisualElement.Width = previewSize.Width;
                    this.Player.MediaPlugin.VisualElement.Height = previewSize.Height;
                }
            }
        }

        /// <summary>
        /// Updates the smpte frame rate.
        /// </summary>
        /// <param name="frameRate">The frame rate.</param>
        public void UpdateSmpteFrameRate(SmpteFrameRate frameRate)
        {
        }

        /// <summary>
        /// Assets the changed.
        /// </summary>
        /// <param name="d">The <see cref="DependencyObject"/>.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void AssetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PreviewTimeline preview = d as PreviewTimeline;

            if (preview != null && preview.thumbnailService != null)
            {
                preview.VideoInOut = null;
                preview.AudioInOut = null;
                preview.Timeline.RemoveMarkIn();
                preview.Timeline.RemoveMarkOut();

                Uri frameUri = new Uri(preview.thumbnailService.GetThumbnailSource(preview.Asset), UriKind.RelativeOrAbsolute);
                preview.FramePreviewImage.Source = new BitmapImage(frameUri);

                UpdateCurrentAsset(preview);
                preview.Timeline.MediaPlugin = preview.Player.MediaPlugin;

                if (Infrastructure.DragDrop.DragDropManager.GetIsDragSource(d) && e.OldValue != e.NewValue)
                {
                    if (preview.Asset is VideoAsset)
                    {
                        Infrastructure.DragDrop.DragDropManager.SetDragData(d, preview.VideoInOut);
                        Infrastructure.DragDrop.DragDropManager.SetCustomData(d, preview.Player.MediaPlugin);
                    }
                    else
                    {
                        Infrastructure.DragDrop.DragDropManager.SetDragData(d, preview.AudioInOut);
                    }
                }
            }
        }

        private static void UpdateCurrentAsset(PreviewTimeline preview)
        {
            if (preview.Player.MediaPlugin != null)
            {
                preview.Player.PauseMedia();
                preview.Player.MediaPlugin.Stop();
                preview.AssetContainer.Children.Remove(preview.Player.MediaPlugin.VisualElement);
                preview.AssetPreview.UpdateLayout();
            }

            preview.OnPlaying();

            preview.Player.MediaPlugin = new RCESmoothStreamingMediaPlugin { AutoPlay = false };

            if (preview.cacheManager != null)
            {
                preview.cacheManager.CacheUpdated += preview.OnCacheUpdated;
                preview.cacheManager.CacheRebuilt += preview.OnCacheRebuilt;
            }

            preview.Player.MediaPlugin.VisualElement.Width = preview.FramePreviewImage.Width * preview.FramePreviewImageRenderTransform.ScaleX;
            preview.Player.MediaPlugin.VisualElement.Height = preview.FramePreviewImage.Height * preview.FramePreviewImageRenderTransform.ScaleY;

            preview.Player.MediaPlugin.MediaOpened += preview.MediaElement_MediaOpened;
            preview.Player.MediaPlugin.CurrentStateChanged += preview.MediaElement_CurrentStateChanged;
            preview.Player.MediaPlugin.SeekCompleted += preview.MediaElement_SeekCompleted;
            preview.Player.MediaPlugin.DownloadProgressChanged += preview.MediaPlugin_DownloadProgressChanged;

            preview.Player.SetSource(preview.Asset);
            preview.Timeline.Asset = preview.Asset;

            preview.FramePreviewImage.Visibility = Visibility.Collapsed;
            preview.AssetContainer.Children.Add(preview.Player.MediaPlugin.VisualElement);
            preview.Player.SetSmpteFrameRate(preview.GetCurrentFrameRate());

            preview.InitialLoad();

            UpdateVideoInOut(preview);
            preview.UpdateVideoInOutProperties();
        }

        private static void UpdateVideoInOut(PreviewTimeline preview)
        {
            double outPosition = preview.SubClipAsset.OutPosition;

            if (preview.SubClipAsset.InPosition > 0)
            {
                preview.SetMarkIn(TimeCode.FromSeconds(preview.SubClipAsset.InPosition, preview.GetCurrentFrameRate()));
            }

            if (preview.SubClipAsset.OutPosition > 0)
            {
                preview.SetMarkOut(TimeCode.FromSeconds(outPosition, preview.GetCurrentFrameRate()));
            }
        }

        private void InitialLoad()
        {
            this.Timeline.MediaPlugin = this.Player.MediaPlugin;

            Size aspectSize = this.GetSizeMaintainingAspectRatio(this.VideoGrid.ActualWidth, this.VideoGrid.ActualHeight);
            this.FramePreviewImage.Width = aspectSize.Width;
            this.FramePreviewImage.Height = aspectSize.Height;

            this.FramePreviewImage.Visibility = Visibility.Visible;

            this.Timeline.SetDuration(this.GetCurrentDuration());

            if (this.scaleSize.Width != 0 && this.scaleSize.Height != 0)
            {
                this.Scale(this.scaleSize);
            }

            this.Player.IsLive = this.Asset.ResourceType == ResourceType.LiveSmoothStream;

            this.isLoaded = true;
        }

        /// <summary>
        /// Handles the ExpandToFullScreen event of the Player control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Player_ExpandToFullScreen(object sender, EventArgs e)
        {
            this.IsFullScreen = !this.IsFullScreen;

            this.OnTogglingFullScreen();
        }

        /// <summary>
        /// Called when [mouse wheel] event occures.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MouseWheelEventArgs"/> instance containing the event data.</param>
        private void RootVisual_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            this.Timeline.ZoomTimeline(e.Delta);
            e.Handled = true;
        }

        /// <summary>
        /// Creates a WriteableBitmap of the current media element.
        /// </summary>
        /// <returns>The WriteableBitmap created.</returns>
        private WriteableBitmap CreateWriteableBitmap()
        {
            WriteableBitmap writeableBitmap = new WriteableBitmap((int)this.Player.MediaPlugin.VisualElement.ActualWidth, (int)this.Player.MediaPlugin.VisualElement.ActualHeight);
            writeableBitmap.Render(this.Player.MediaPlugin.VisualElement, null);
            writeableBitmap.Invalidate();

            return writeableBitmap;
        }

        /// <summary>
        /// Updates the In/Out position.
        /// </summary>
        private void UpdateVideoInOutProperties()
        {
            this.SubClipAsset.InPosition = (this.InPosition.TotalSeconds >= this.Timeline.StartOffset.TotalSeconds) ? this.InPosition.TotalSeconds - this.Timeline.StartOffset.TotalSeconds : this.InPosition.TotalSeconds;
            this.SubClipAsset.OutPosition = (this.OutPosition.TotalSeconds >= this.Timeline.StartOffset.TotalSeconds) ? this.OutPosition.TotalSeconds - this.Timeline.StartOffset.TotalSeconds : this.OutPosition.TotalSeconds;
            this.SubClipDuration.Text = TimeCode.FromSeconds(this.SubClipAsset.SubClipDuration, this.GetCurrentFrameRate()).ToString(this.GetCurrentFrameRate());
        }

        /// <summary>
        /// Sets the mark in value based on the given position.
        /// </summary>
        /// <param name="currentPosition">The current position of the mark in.</param>
        private void SetMarkIn(TimeCode currentPosition)
        {
            if (this.Player.MediaPlugin != null)
            {
                currentPosition = this.Timeline.SetMarkIn(currentPosition);
                this.UpdateVideoInOutProperties();

                if (!this.Player.IsPlaying)
                {
                    this.Player.Position = TimeSpan.FromSeconds(currentPosition.TotalSeconds);

                    var playHeadPosition = this.Timeline.GetPositionWithoutStartOffset(currentPosition);
                    this.Timeline.GoToPosition(playHeadPosition);
                }
            }
        }

        /// <summary>
        /// Sets the mark out value based on the given position.
        /// </summary>
        /// <param name="currentPosition">The current position of the mark out.</param>
        private void SetMarkOut(TimeCode currentPosition)
        {
            if (this.Player.MediaPlugin != null)
            {
                currentPosition = this.Timeline.SetMarkOut(currentPosition);
                this.UpdateVideoInOutProperties();

                if (!this.Player.IsPlaying)
                {
                    this.Player.Position = TimeSpan.FromSeconds(currentPosition.TotalSeconds);

                    var playHeadPosition = this.Timeline.GetPositionWithoutStartOffset(currentPosition);
                    this.Timeline.GoToPosition(playHeadPosition);
                }
            }
        }

        /// <summary>
        /// Called when [toggling full screen].
        /// </summary>
        private void OnTogglingFullScreen()
        {
            EventHandler togglingFullScreenHandler = this.TogglingFullScreen;
            if (togglingFullScreenHandler != null)
            {
                togglingFullScreenHandler(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Returns the best fit size for the asset for the given size.
        /// </summary>
        /// <param name="width">Max possible width.</param>
        /// <param name="height">Max possible height.</param>
        /// <returns>Returns the best fit size for the asset.</returns>
        private Size GetSizeMaintainingAspectRatio(double width, double height)
        {
            width -= MarginX;

            height -= MarginY;

            if (this.IsFullScreen)
            {
                height -= this.Timeline.ActualHeight;
            }

            VideoAsset videoAsset = this.Asset as VideoAsset;

            if (videoAsset != null)
            {
                double aspectRatioWidth = Convert.ToDouble(videoAsset.Width.GetValueOrDefault());
                double aspectRatioHeight = Convert.ToDouble(videoAsset.Height.GetValueOrDefault());

                if (aspectRatioWidth == 0 || aspectRatioHeight == 0)
                {
                    return new Size(width, height);
                }

                if (width >= height * aspectRatioWidth / aspectRatioHeight)
                {
                    return new Size(height * aspectRatioWidth / aspectRatioHeight, height);
                }
                else
                {
                    return new Size(width, width * aspectRatioHeight / aspectRatioWidth);
                }
            }

            return new Size(width, height);
        }

        private void ClearPreviewState()
        {
            // this.CurrentPositionLabel.Text = String.Empty;
            this.RemoveMarkIn();
            this.RemoveMarkOut();
            this.Timeline.SetAvailableTime(TimeCode.FromAbsoluteTime(0, this.GetCurrentFrameRate()));
            this.EndBuffer();
        }

        /// <summary>
        /// Handles the StartPlay event of the Player control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Player_StartPlay(object sender, EventArgs e)
        {
            this.Timeline.SubClipPlayheadContentControl.Focus();
            this.Timeline.Asset = this.Asset;

            if (this.Player.MediaPlugin != null)
            {
                if (this.Player.MediaPlugin.CurrentState == MediaPluginState.Closed || this.Player.MediaPlugin.CurrentState == MediaPluginState.Playing)
                {
                    this.UpdateMediaError(false);
                    this.Player.RetrySource(this.Asset);
                }

                // this.Player.Position = TimeSpan.FromSeconds(this.Timeline.CurrentPosition.TotalSeconds);
            }
        }

        private void Player_ShowFragmentBoundariesChanged(object sender, RCE.Infrastructure.DataEventArgs<bool> e)
        {
            this.Timeline.ShowFragmentBoundaries = e.Data;
        }

        private void MediaPlugin_DownloadProgressChanged(IMediaPlugin arg1, double progress, double offset)
        {
            IAdaptiveAsset adaptiveAsset = this.Asset as IAdaptiveAsset;

            if (adaptiveAsset != null)
            {
                IDictionary<double, double> dict = this.cacheManager.RetrieveAssetCache(adaptiveAsset);

                foreach (KeyValuePair<double, double> pair in dict)
                {
                    this.Timeline.UpdateDownloadProgress(pair.Key, pair.Value);
                }
            }
            else
            {
                this.Timeline.UpdateDownloadProgress(progress, offset);
            }
        }

        private void OnCacheUpdated(object sender, Infrastructure.DataEventArgs<Tuple<double, double, Asset>> e)
        {
            if (Deployment.Current.Dispatcher.CheckAccess())
            {
                this.UpdateCacheProgress(e.Data);
            }
            else
            {
                Deployment.Current.Dispatcher.BeginInvoke(() => this.UpdateCacheProgress(e.Data));
            }
        }

        private void UpdateCacheProgress(Tuple<double, double, Asset> tuple)
        {
            if (tuple.Item3.Id == this.Asset.Id)
            {
                this.Timeline.UpdateDownloadProgress(tuple.Item1, tuple.Item2);
            }
        }

        private void OnCacheRebuilt(object sender, Infrastructure.DataEventArgs<Tuple<IDictionary<double, double>, Asset>> e)
        {
            if (Deployment.Current.Dispatcher.CheckAccess())
            {
                this.RebuildCacheProgress(e.Data);
            }
            else
            {
                Deployment.Current.Dispatcher.BeginInvoke(() => this.RebuildCacheProgress(e.Data));
            }
        }

        private void RebuildCacheProgress(Tuple<IDictionary<double, double>, Asset> e)
        {
            if (e.Item2.Id == this.Asset.Id)
            {
                this.Timeline.UpdateDownloadProgress(e.Item1);
            }
        }

        /// <summary>
        /// Handles the CurrentStateChanged event of the media element.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args instance containing event data.</param>
        private void MediaElement_CurrentStateChanged(IMediaPlugin mediaPlugin, MediaPluginState mediaPluginState)
        {
            if (this.Player != null && this.Player.MediaPlugin != null)
            {
                lock (this.lastSeekPositionLock)
                {
                    if (mediaPluginState != MediaPluginState.Buffering && this.lastSeekPosition.HasValue)
                    {
                        mediaPlugin.Position = this.lastSeekPosition.Value;
                        this.lastSeekPosition = null;
                    }
                }

                if (this.Player.MediaPlugin.CurrentState == MediaPluginState.Buffering)
                {
                    this.StartBuffer();
                }
                else if (this.isBuffering)
                {
                    this.EndBuffer();
                }
            }
        }

        /// <summary>
        /// Starts the animation of buffering in the player.
        /// </summary>
        private void StartBuffer()
        {
            this.isBuffering = true;
            this.BufferBar.Visibility = Visibility.Visible;
            this.Spinner.BeginAnimation();
        }

        /// <summary>
        /// End the animation of buffering in the player.
        /// </summary>
        private void EndBuffer()
        {
            this.isBuffering = false;
            this.BufferBar.Visibility = Visibility.Collapsed;
            this.Spinner.StopAnimation();
        }

        /// <summary>
        /// Handles the MediaPositionChanged event of the Player control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Player_MediaPositionChanged(object sender, EventArgs e)
        {
            if (this.Player.MediaPlugin != null)
            {
                if (this.Asset.IsAdaptiveAsset)
                {
                    double startOffset = this.Player.MediaPlugin.GetVideoStreamStartOffset().TotalSeconds;
                    double mediaElementDuration;

                    try
                    {
                        if (this.Player.MediaPlugin.LivePosition.TotalSeconds == 0)
                        {
                            mediaElementDuration = this.Player.MediaPlugin.EndPosition.TotalSeconds - this.Player.MediaPlugin.StartPosition.TotalSeconds;
                        }
                        else
                        {
                            mediaElementDuration = this.Player.MediaPlugin.LivePosition.TotalSeconds - startOffset;
                        }
                    }
                    catch (NullReferenceException)
                    {
                        mediaElementDuration = this.Player.MediaPlugin.EndPosition.TotalSeconds;
                    }

                    // if reached end position of timeline and there is more live stream
                    if (Math.Round(this.Timeline.Duration.TotalSeconds, 7) < Math.Round(mediaElementDuration, 7))
                    {
                        //increase timeline duration each 5 min (300 secs)
                        TimeCode duration;

                        if ((this.Timeline.Duration.TotalSeconds == 0 && mediaElementDuration != 0) ||
                            (mediaElementDuration - this.Timeline.Duration.TotalSeconds > 300) ||
                            this.Player.MediaPlugin.LivePosition.TotalSeconds == 0)
                        {
                            duration = TimeCode.FromSeconds(mediaElementDuration, this.GetCurrentFrameRate());
                        }
                        else
                        {
                            // expand timeline duration by 15% of the current live position
                            double range = this.Player.MediaPlugin.LivePosition.TotalSeconds - startOffset;
                            double seconds = (this.Player.MediaPlugin.LivePosition.TotalSeconds + (range * 0.15)) - startOffset;
                            duration = TimeCode.FromSeconds(seconds, this.GetCurrentFrameRate());
                        }

                        this.SetDuration(duration);
                    }

                    this.UpdateAvailableTime(false);
                }

                if (!this.Player.MediaPlugin.IsSeeking && (this.Player.MediaPlugin.CurrentState == MediaPluginState.Playing || this.Player.MediaPlugin.CurrentState == MediaPluginState.Paused))
                {
                    TimeCode currentPosition = TimeCode.FromSeconds(this.Player.Position.TotalSeconds, this.GetCurrentFrameRate());

                    if (this.Player.IsPlaySubClipMode && currentPosition >= this.OutPosition + this.Timeline.StartOffset)
                    {
                        currentPosition = this.OutPosition + this.Timeline.StartOffset;
                        this.Player.IsPlaySubClipMode = false;
                        this.Player.PauseMedia();
                    }

                    this.PlayheadPosition = currentPosition;
                    this.CurrentPlayheadPosition.Text = currentPosition.ToString();

                    currentPosition = this.Timeline.GetPositionWithoutStartOffset(currentPosition);

                    this.Timeline.SetPlayHeadPosition(currentPosition);
                }

                lock (this.lastSeekPositionLock)
                {
                    if (!this.lastSeekPosition.HasValue || this.lastSeekPosition == this.Player.MediaPlugin.Position)
                    {
                        this.Player.EnablePlayButton(true);
                    }
                }
            }
        }

        /// <summary>
        /// Handles the MediaOpened event of the CurrentMediaElement control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void MediaElement_MediaOpened(IMediaPlugin mediaPlugin)
        {
            if (this.Asset is VideoAsset && !string.IsNullOrEmpty(this.VideoInOut.PreviewVideoCamera))
            {
                this.Player.SetVideoCamera(this.VideoInOut.PreviewVideoCamera);
            }

            if (this.Asset is VideoAsset && this.VideoInOut.PreviewAudioStream != null && !string.IsNullOrEmpty(this.VideoInOut.PreviewAudioStream.Name))
            {
                this.Player.SetAudioStream(this.VideoInOut.PreviewAudioStream.Name);
            }

            this.SetDuration(this.Player.MediaPlugin.Duration);

            if (this.Asset.IsAdaptiveAsset)
            {
                this.Timeline.SetStartOffsetTimeCode(TimeCode.FromSeconds(this.Player.MediaPlugin.GetVideoStreamStartOffset().TotalSeconds, this.GetCurrentFrameRate()));
            }
            else
            {
                this.Timeline.SetStartOffsetTimeCode(TimeCode.FromSeconds(0.0, this.GetCurrentFrameRate()));
            }

            this.eventAggregator.GetEvent<SubClipMediaElementOpenedEvent>().Publish(this.Player.MediaPlugin.GetVideoStreamStartOffset());

            this.UpdateAvailableTime(true);

            SmoothStreamingVideoAsset smoothStreamingVideoAsset = this.Asset as SmoothStreamingVideoAsset;

            if (smoothStreamingVideoAsset != null && smoothStreamingVideoAsset.StartPosition != this.Player.MediaPlugin.StartPosition.TotalSeconds)
            {
                smoothStreamingVideoAsset.StartPosition = this.Player.MediaPlugin.StartPosition.TotalSeconds;

                smoothStreamingVideoAsset.IsStereo = this.Player.MediaPlugin.IsStereo;
            }

            this.InStreamData = this.Player.InStreamData;

            this.UpdateVideoInOutProperties();
        }

        /// <summary>
        /// Handles the LiveSeekCompleted event of the Player.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event args instance containing the event data.</param>
        private void Player_LiveSeekCompleted(object sender, Infrastructure.DataEventArgs<bool> e)
        {
            if (e.Data)
            {
                if (this.Timeline.Duration.TotalSeconds < this.Player.MediaPlugin.Duration.TotalSeconds)
                {
                    this.SetDuration(this.Player.MediaPlugin.Duration);
                }

                this.UpdateAvailableTime(false);
                TimeCode timeCode = TimeCode.FromSeconds(this.Player.Position.TotalSeconds, this.GetCurrentFrameRate());

                this.PlayheadPosition = timeCode;

                this.CurrentPlayheadPosition.Text = timeCode.ToString();

                if (timeCode > this.Timeline.StartOffset)
                {
                    timeCode = timeCode - this.Timeline.StartOffset;
                }

                this.Timeline.SetPlayHeadPosition(timeCode);
            }
        }

        private void UpdateAvailableTime(bool processVod)
        {
            if (this.Player.MediaPlugin.CurrentState == MediaPluginState.Playing || this.Player.MediaPlugin.CurrentState == MediaPluginState.Paused || this.Player.MediaPlugin.CurrentState == MediaPluginState.Opening)
            {
                if (this.Asset.ResourceType == ResourceType.LiveSmoothStream && this.Player.IsLive)
                {
                    double liveBuffer = (double)(this.Player.MediaPlugin.PositionLiveBuffer.TotalSeconds / 1000);

                    TimeSpan position = TimeSpan.FromSeconds(this.Player.MediaPlugin.LivePosition.TotalSeconds - liveBuffer);

                    TimeCode timeCode = TimeCode.FromSeconds(position.TotalSeconds, this.GetCurrentFrameRate());

                    timeCode = this.Timeline.GetPositionWithoutStartOffset(timeCode);

                    this.Timeline.SetAvailableTime(timeCode);
                }
                else if (processVod)
                {
                    this.Timeline.SetAvailableTime(TimeCode.FromSeconds(this.Player.MediaPlugin.Duration.TotalSeconds, this.GetCurrentFrameRate()));
                }
            }
        }

        /// <summary>
        /// Sets the duration to the timeline and the video asset.
        /// </summary>
        /// <param name="duration">The duration being set.</param>
        private void SetDuration(TimeCode duration)
        {
            this.Timeline.SetDuration(duration);
            var videoAsset = this.Asset as VideoAsset;
            if (videoAsset != null)
            {
                videoAsset.Duration = duration;
            }

            var audioAsset = this.Asset as AudioAsset;
            if (audioAsset != null)
            {
                audioAsset.DurationInSeconds = duration.TotalSeconds;
            }

            this.UpdateVideoInOutProperties();
        }

        /// <summary>
        /// Sets the duration to the timeline and the video asset.
        /// </summary>
        /// <param name="duration">The duration being set.</param>
        private void SetDuration(TimeSpan duration)
        {
            TimeCode currentDuration = TimeCode.FromSeconds(duration.TotalSeconds, this.GetCurrentFrameRate());
            this.SetDuration(currentDuration);
        }

        /// <summary>
        /// Handles the FrameRateParsed event of the Player control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event args instance containing the event data.</param>
        private void Player_FrameRateParsed(object sender, Infrastructure.DataEventArgs<SmpteFrameRate> e)
        {
            var videoAsset = this.Asset as VideoAsset;
            if (e.Data != SmpteFrameRate.Unknown && videoAsset != null)
            {
                this.Player.SetSmpteFrameRate(e.Data);
                videoAsset.Duration = TimeCode.FromSeconds(videoAsset.Duration.TotalSeconds, e.Data);
                videoAsset.FrameRate = e.Data;
                this.Timeline.SetDuration(TimeCode.FromSeconds(this.Timeline.Duration.TotalSeconds, e.Data));
                this.UpdateVideoInOutProperties();
            }
        }

        /// <summary>
        /// Handles the PositionChange event of the Timeline control.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args instance containing the event data.</param>
        private void Timeline_PositionChange(object sender, PositionChangeEventArgs e)
        {
            if (this.Player.MediaPlugin != null)
            {
                // this.Player.MediaElement.Scrubbing = true;
                if (!this.Player.MediaPlugin.IsSeeking)
                {
                    this.StartBuffer();
                }

                TimeSpan position = TimeSpan.FromSeconds(e.NewPosition.TotalSeconds);

                if ((this.Asset.ResourceType == ResourceType.LiveSmoothStream && this.Player.IsLive) || this.Player.MediaPlugin.StartPosition.TotalSeconds > 0)
                {
                    // Prevent Seeking
                    if (position.TotalSeconds < this.Player.MediaPlugin.StartPosition.TotalSeconds)
                    {
                        position = this.Player.MediaPlugin.StartPosition;
                        this.Timeline.SetPlayHeadPosition(this.Timeline.GetPositionWithoutStartOffset(TimeCode.FromSeconds(position.TotalSeconds, this.GetCurrentFrameRate())));
                    }
                }

                if (this.Asset.ResourceType == ResourceType.LiveSmoothStream && this.Player.IsLive && this.Player.MediaPlugin.LivePosition.TotalSeconds < position.TotalSeconds)
                {
                    position = TimeSpan.FromSeconds(this.Player.MediaPlugin.LivePosition.TotalSeconds);

                    TimeCode timeCode = TimeCode.FromSeconds(position.TotalSeconds, this.GetCurrentFrameRate());

                    timeCode = this.Timeline.GetPositionWithoutStartOffset(timeCode);

                    this.Timeline.SetPlayHeadPosition(timeCode);
                }

                if (this.Player.IsPlaySubClipMode && (this.InPosition.TotalSeconds > position.TotalSeconds || this.OutPosition.TotalSeconds < position.TotalSeconds))
                {
                    this.Player.IsPlaySubClipMode = false;
                }

                if (this.Player.MediaPlugin.CurrentState == MediaPluginState.Paused)
                {
                    this.Player.Position = position;
                }
                else
                {
                    lock (this.lastSeekPositionLock)
                    {
                        this.lastSeekPosition = position;
                    }
                }

                this.PlayheadPosition = TimeCode.FromSeconds(position.TotalSeconds, this.GetCurrentFrameRate());

                this.CurrentPlayheadPosition.Text = this.PlayheadPosition.ToString();


                if (position.TotalSeconds > 0 && this.Player.Position != position)
                {
                    this.Player.EnablePlayButton(false);
                }
            }
        }

        /// <summary>
        /// Handles the MovingPlayhead event of the Timeline control.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args instance containing the event data.</param>
        private void Timeline_MovingPlayHead(object sender, EventArgs e)
        {
            if (this.Player.MediaPlugin != null)
            {
                this.Player.PauseMedia();
            }
        }

        /// <summary>
        /// Handles the Resized event of the Timeline control.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args instance containing the event data.</param>
        private void Timeline_Resized(object sender, EventArgs e)
        {
            if (this.IsFullScreen)
            {
                this.Resize();
            }
        }

        /// <summary>
        /// Called when stop button of <see cref="Player"/> control is clicked.
        /// </summary>
        private void OnStopping()
        {
            EventHandler stoppingHandler = this.Stopping;
            if (stoppingHandler != null)
            {
                stoppingHandler(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Called when play button of <see cref="Player"/> control is clicked.
        /// </summary>
        private void OnPlaying()
        {
            EventHandler playingHandler = this.Playing;
            if (playingHandler != null)
            {
                playingHandler(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Resizes and adjusts the previes.
        /// </summary>
        private void Resize()
        {
            this.Scale(new Size(this.VideoGrid.Width, this.VideoGrid.Height));
        }

        /// <summary>
        /// Removes the current mark out.
        /// </summary>
        private void RemoveMarkIn()
        {
            this.Timeline.RemoveMarkIn();
            this.Resize();
            this.UpdateVideoInOutProperties();
        }

        /// <summary>
        /// Removes the current mark out.
        /// </summary>
        private void RemoveMarkOut()
        {
            this.Timeline.RemoveMarkOut();
            this.Resize();
            this.UpdateVideoInOutProperties();
        }

        /// <summary>
        /// Handles the Click event of the MarkInDeleteButton. Removes the Mark in.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args istance containing the event data.</param>
        private void MarkInDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            this.RemoveMarkIn();
        }

        /// <summary>
        /// Handles the Click event of the MarkOutDeleteButton. Removes the Mark out.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args istance containing the event data.</param>
        private void MarkOutDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            this.RemoveMarkOut();
        }

        /// <summary>
        /// Handles the SeekCompleted event of the MediaElement. Stops the buffering animation.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event args instance containing event data.</param>
        private void MediaElement_SeekCompleted(IMediaPlugin mediaPlugin, bool success)
        {
            if (this.Player.MediaPlugin != null && !this.Player.MediaPlugin.IsSeeking)
            {
                // this.Player.MediaElement.Scrubbing = false;
                this.EndBuffer();
            }

            lock (this.lastSeekPositionLock)
            {
                if (!this.lastSeekPosition.HasValue || this.lastSeekPosition == this.Player.MediaPlugin.Position)
                {
                    this.Player.EnablePlayButton(true);
                }
            }
        }

        /// <summary>
        /// Handles the SetSubMarkClipClicked event of the Player. Sets the Mark In / Out.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event args instance containing event data.</param>
        private void Player_SetSubMarkClipClicked(object sender, Infrastructure.DataEventArgs<ScrubShiftType> e)
        {
            switch (e.Data)
            {
                case ScrubShiftType.In:
                    this.SetMarkIn(this.Timeline.CurrentPosition);
                    break;

                case ScrubShiftType.Out:
                    this.SetMarkOut(this.Timeline.CurrentPosition);
                    break;
            }
        }

        /// <summary>
        /// Handles the PlaySubClipClicked event of the Player. Enables the play subclip feature.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event args instance containing event data.</param>
        private void Player_PlaySubClipClicked(object sender, EventArgs e)
        {
            if (this.Player.MediaPlugin != null && this.Timeline.HasMarkIn)
            {
                this.GoToSubClipPosition(this.InPosition);
            }
            else
            {
                this.Player.IsPlaySubClipMode = false;
            }
        }

        /// <summary>
        /// Handles the MediaErrorExpandedChanged event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event args instance containing event data.</param>
        private void Player_MediaErrorExpandedChanged(object sender, RoutedEventArgs e)
        {
            this.ClearPreviewState();
            this.UpdateMediaError(true);
        }

        /// <summary>
        /// Update the media error state.
        /// </summary>
        /// <param name="expanded">If the media error should be expanded or not.</param>
        private void UpdateMediaError(bool expanded)
        {
            if (expanded)
            {
                VisualStateManager.GoToState(this, "MediaErrorExpanded", true);
            }
            else
            {
                VisualStateManager.GoToState(this, "MediaErrorCollapsed", true);
            }
        }

        /// <summary>
        /// Goes to the given subclip position.
        /// </summary>
        /// <param name="position">The Mark In or Mark Out position.</param>
        private void GoToSubClipPosition(TimeCode position)
        {
            var timeCode = this.Timeline.GetPositionWithoutStartOffset(position);
            var timeCodeWithOffset = timeCode + this.Timeline.StartOffset;
            this.Timeline.SetPlayHeadPosition(timeCode);
            this.Player.Position = TimeSpan.FromSeconds(timeCodeWithOffset.TotalSeconds);
            this.CurrentPlayheadPosition.Text = timeCodeWithOffset.ToString();
        }

        /// <summary>
        /// Handles the Clicked event on the Mark In. Goes to the mark in position.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event args containing event data.</param>
        private void MarkIn_Clicked(object sender, MouseButtonEventArgs e)
        {
            if (this.Player.MediaPlugin != null && this.Timeline.HasMarkIn)
            {
                this.GoToSubClipPosition(this.InPosition);
            }
        }

        /// <summary>
        /// Handles the Clicked event on the Mark Out. Goes to the mark out position.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event args containing event data.</param>
        private void MarkOut_Clicked(object sender, MouseButtonEventArgs e)
        {
            if (this.Player.MediaPlugin != null && this.Timeline.HasMarkOut)
            {
                this.GoToSubClipPosition(this.OutPosition);
            }
        }

        private void PlayerGoToTimeCodeClicked(object sender, EventArgs e)
        {
            if (this.GoToPanel.Visibility == Visibility.Collapsed)
            {
                this.GoToPanel.Visibility = Visibility.Visible;
                this.GoToTextBox.Focus();
            }
            else
            {
                this.CloseGoToPanel();
            }
        }

        private void CloseGoToPanel()
        {
            this.GoToPanel.Visibility = Visibility.Collapsed;
            this.GoToTextBox.Text = string.Empty;
            VisualStateManager.GoToState(this.GoToTextBox, "Valid", false);
            ToolTipService.SetToolTip(this.GoToTextBox, null);
            this.Timeline.SubClipPlayheadContentControl.Focus();
        }

        private void GoToTimeCodeClose_Click(object sender, RoutedEventArgs e)
        {
            this.CloseGoToPanel();
        }

        private void FrameRewindForwardTimerTick(object sender, EventArgs e)
        {
            if (this.action == KeyboardAction.Forward && this.Timeline.CurrentPosition - this.Timeline.StartOffset < this.Timeline.Duration)
            {
                this.MoveTimelineOnFrames(1);
            }
            else if (this.action == KeyboardAction.Rewind && this.Timeline.CurrentPosition - this.Timeline.StartOffset > TimeCode.FromAbsoluteTime(0, this.GetCurrentFrameRate()))
            {
                this.MoveTimelineOnFrames(-1);
            }
        }

        private void GoToTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.Player.MediaPlugin != null)
            {
                if (e.Key == Key.Enter)
                {
                    if (!TimeCode.ValidateSmpte12MTimecode(this.GoToTextBox.Text))
                    {
                        VisualStateManager.GoToState(this.GoToTextBox, "InvalidUnfocused", false);
                        ToolTipService.SetToolTip(this.GoToTextBox, "Invalid timecode");
                        return;
                    }

                    TimeCode position = new TimeCode(this.GoToTextBox.Text, this.GetCurrentFrameRate());

                    if (position < this.Timeline.StartOffset || position > (this.Timeline.StartOffset + this.Timeline.Duration))
                    {
                        VisualStateManager.GoToState(this.GoToTextBox, "InvalidUnfocused", false);
                        ToolTipService.SetToolTip(this.GoToTextBox, "Invalid timecode");
                        return;
                    }

                    this.CloseGoToPanel();

                    if (!this.Player.MediaPlugin.IsSeeking)
                    {
                        this.StartBuffer();
                    }

                    TimeCode positionWithoutOffset = this.Timeline.GetPositionWithoutStartOffset(position);

                    this.Player.Position = TimeSpan.FromSeconds(position.TotalSeconds);
                    this.CurrentPlayheadPosition.Text = position.ToString();
                    this.Timeline.SetPlayHeadPosition(positionWithoutOffset);
                }
            }
        }

        private void VideoInOut_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (string.Equals("PreviewVideoCamera", e.PropertyName))
            {
                this.Player.SetVideoCamera(this.VideoInOut.PreviewVideoCamera);
            }

            if (string.Equals(e.PropertyName, "PreviewAudioStream"))
            {
                this.Player.SetAudioStream(this.VideoInOut.PreviewAudioStream.Name);
            }
        }

        private void MoveTimelineOnFrames(long frames)
        {
            this.Timeline.GoToPosition(this.Timeline.CurrentPosition - this.Timeline.StartOffset + TimeCode.FromFrames(frames, this.GetCurrentFrameRate()));
        }

        private void OnMetadataEventSelected(MetadaSelectedPayload payload)
        {
            TimeCode position = this.GetPosition(payload.EventData);
            this.Timeline.SetPlayHeadPosition(position); // position does not have start offset
            this.Player.Position = this.GetTime(payload.EventData);
            this.CurrentPlayheadPosition.Text = (position + this.Timeline.StartOffset).ToString();
        }

        private TimeSpan GetTime(EventData eventData)
        {
            if (eventData.IsRelativeTime)
            {
                return eventData.Time;
            }

            return eventData.Time + TimeSpan.FromSeconds(this.Timeline.StartOffset.TotalSeconds);
        }

        private TimeCode GetPosition(EventData eventData)
        {
            TimeCode position = TimeCode.FromSeconds(eventData.Time.TotalSeconds, this.GetCurrentFrameRate());

            if (eventData.IsRelativeTime)
            {
                position -= this.Timeline.StartOffset;
            }

            return position;
        }

        private SmpteFrameRate GetCurrentFrameRate()
        {
            var videoAsset = this.Asset as VideoAsset;

            if (videoAsset != null)
            {
                return videoAsset.FrameRate;
            }

            return this.sequenceRegistry.CurrentSequenceModel.Duration.FrameRate;
        }

        private TimeCode GetCurrentDuration()
        {
            var videoAsset = this.Asset as VideoAsset;

            if (videoAsset != null)
            {
                return videoAsset.Duration;
            }

            var audioAsset = this.Asset as AudioAsset;

            if (audioAsset != null)
            {
                return TimeCode.FromSeconds(audioAsset.DurationInSeconds, this.sequenceRegistry.CurrentSequenceModel.Duration.FrameRate);
            }

            return TimeCode.FromSeconds(0.0, this.sequenceRegistry.CurrentSequenceModel.Duration.FrameRate);
        }

        private void RegisterRegionManager()
        {
            this.regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            RegionManager.SetRegionManager(this, this.regionManager);
        }

        private bool FilterMetadaEventSelected(MetadaSelectedPayload payload)
        {
            return payload.CommentMode == CommentMode.SubClip;
        }
    }
}
