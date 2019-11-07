// <copyright file="PlayerViewPresenter.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: PlayerViewPresenter.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Player
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Browser;
    using System.Windows.Media.Imaging;
    using System.Windows.Threading;
    using Microsoft.Practices.Composite.Events;
    using Microsoft.Practices.Composite.Presentation.Commands;
    using Microsoft.Practices.Composite.Presentation.Events;
    using Microsoft.SilverlightMediaFramework.Plugins;
    using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
    using RCE.Infrastructure;
    using RCE.Infrastructure.Events;
    using RCE.Infrastructure.Models;
    using RCE.Infrastructure.Services;
    using RCE.Infrastructure.Windows;
    using RCE.Modules.Player.Models;
    using RCE.Modules.Player.Services;
    using RCE.Overlays.Infrastructure.Manager;
    using RCE.Overlays.Infrastructure.UI;
    using SMPTETimecode;
    using Comment = RCE.Infrastructure.Models.Comment;
    using Track = RCE.Infrastructure.Models.Track;

    /// <summary>
    /// Presenter for the <see cref="PlayerView"/>.
    /// </summary>
    public class PlayerViewPresenter : BaseModel, IPlayerViewPresenter, IWindowMetadataProvider, IWindowAware
    {
        /// <summary>
        /// The <seealso cref="IEventAggregator"/> instance used to 
        /// publish and subscribe to events.
        /// </summary>
        private readonly IEventAggregator eventAggregator;

        private readonly IPlaybackManifestGenerator playbackManifestGenerator;

        private readonly IManifestMediaModel manifestMediaModel;

        private readonly IOverlaysManager overlaysManager;

        private readonly IOverlaysDisplayController overlaysDisplayController;

        private readonly ISequenceRegistry sequenceRegistry;

        private readonly IConfigurationService configurationService;

        /// <summary>
        /// The <see cref="DispatcherTimer"/> to frame rewind/forward the currently playing model.
        /// </summary>
        private readonly DispatcherTimer frameRewindForwardTimer;

        /// <summary>
        /// Contains the max number of Audio Tracks allowables.
        /// </summary>
        private readonly int maxNumberOfAudioTracks;

        /// <summary>
        /// To have the current player mode.
        /// </summary>
        private PlayerMode playerMode;

        /// <summary>
        /// Value indicating if the forwar/rewind is going on.
        /// </summary>
        private int currentSkipDirection;

        /// <summary>
        /// Value indicating if the loop back is on for the player.
        /// </summary>
        private bool isLoopPlayback;

        /// <summary>
        /// The current playing comment.
        /// </summary>
        private List<Comment> currentPlayingComments;

        /// <summary>
        /// The <seealso cref="Comment"/> instance used to store the comment for which 
        /// the detail is being displayed.
        /// </summary>
        private Comment currentPlayingComment;

        private bool isMuted;

        /// <summary>
        /// Tracks the balance of operations performed on the timeline so we can keep 
        /// track of wheter or not the manifest should be regenerated.
        /// </summary>
        private int operationBalance = 0;

        private bool mediaIsOpening;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerViewPresenter"/> class.
        /// </summary>
        /// <param name="view">The <see cref="IPlayerView"/> instance as view.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="sequenceRegistry"></param>
        /// <param name="playbackManifestGenerator"></param>
        /// <param name="manifestMediaModel"></param>
        /// <param name="overlaysManager"></param>
        /// <param name="overlaysDisplayController"></param>
        /// <param name="configurationService"></param>
        public PlayerViewPresenter(IPlayerView view, IEventAggregator eventAggregator, ISequenceRegistry sequenceRegistry, IPlaybackManifestGenerator playbackManifestGenerator, IManifestMediaModel manifestMediaModel, IOverlaysManager overlaysManager, IOverlaysDisplayController overlaysDisplayController, IConfigurationService configurationService)
        {
            this.PropertyChanged += this.PlayerViewPresenter_PropertyChanged;
            this.PlayerMode = PlayerMode.Timeline;

            this.maxNumberOfAudioTracks = configurationService.GetParameterValueAsInt("MaxNumberOfAudioTracks").GetValueOrDefault(1);

            this.playbackManifestGenerator = playbackManifestGenerator;

            this.manifestMediaModel = manifestMediaModel;
            this.overlaysManager = overlaysManager;
            this.overlaysDisplayController = overlaysDisplayController;
            this.manifestMediaModel.PlayingStateChanged += this.OnPlayingStateChanged;
            this.manifestMediaModel.PositionUpdated += this.OnPositionUpdated;
            this.manifestMediaModel.FinishedPlaying += this.OnManifestMediaModelPlayFinished;
            this.manifestMediaModel.MediaElementFailed += this.OnManifestMediaModelMediaElementFailed;

            this.eventAggregator = eventAggregator;
            this.configurationService = configurationService;
            this.sequenceRegistry = sequenceRegistry;

            this.eventAggregator.GetEvent<KeyMappingEvent>().Subscribe(this.OnKeyAction, ThreadOption.PublisherThread, true, Filter);
            this.eventAggregator.GetEvent<SmpteTimeCodeChangedEvent>().Subscribe(this.UpdateSmpteFrameRate, true);
            this.eventAggregator.GetEvent<AspectRatioChangedEvent>().Subscribe(this.UpdatePlayerAspectRatio, true);
            this.eventAggregator.GetEvent<PauseEvent>().Subscribe(this.OnPauseEventPublished, true);
            this.eventAggregator.GetEvent<PlayEvent>().Subscribe(this.OnPlayEventPublished, true);
            this.eventAggregator.GetEvent<PlayerEvent>().Subscribe(this.OnPlayerEventPublished, true);
            this.eventAggregator.GetEvent<PlayheadMovedEvent>().Subscribe(this.UpdatePosition, true);
            this.eventAggregator.GetEvent<PlayCommentEvent>().Subscribe(this.PlayComment, ThreadOption.PublisherThread, true, CanPlayComment);
            this.eventAggregator.GetEvent<PickThumbnailEvent>().Subscribe(this.PickProjectThumbnail, true);
            this.eventAggregator.GetEvent<OperationUndoneInTimelineEvent>().Subscribe(this.OperationUndone, true);
            this.eventAggregator.GetEvent<OperationExecutedInTimelineEvent>().Subscribe(this.OperationExecuted, true);
            this.eventAggregator.GetEvent<ShowPreviewOverlayEvent>().Subscribe(this.ShowOverlayPreview, true);
            this.eventAggregator.GetEvent<HidePreviewOverlayEvent>().Subscribe(this.HideOverlayPreview, true);
            this.eventAggregator.GetEvent<ResetWindowsEvent>().Subscribe(this.ResetWindow);
            this.eventAggregator.GetEvent<RubberBandingStateChangedEvent>().Subscribe(this.ToggleRubberBanding, true);
            this.eventAggregator.GetEvent<CheckedTreatGapAsErrorEvent>().Subscribe(this.UpdateTreatGapAsErrorValue, true);
            this.eventAggregator.GetEvent<TrackMuteStateChangedEvent>().Subscribe(this.ToggleTrackMute, true);

            this.frameRewindForwardTimer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 1) };
            this.frameRewindForwardTimer.Tick += this.FrameRewindForwardTimerTick;

            this.FastRewindCommand = new DelegateCommand<object>(this.FastRewind, this.CanFastRewindForward);
            this.FastForwardCommand = new DelegateCommand<object>(this.FastForward, this.CanFastRewindForward);
            this.MoveToStartCommand = new DelegateCommand<object>(this.MoveToStart);
            this.MoveToEndCommand = new DelegateCommand<object>(this.MoveToEnd);
            this.MediaRepeatCommand = new DelegateCommand<object>(this.ToggleLoopPlayback);
            this.MuteCommand = new DelegateCommand<object>(this.MutePlayer);
            this.AddTimelineElementCommand = new DelegateCommand<object>(this.AddTimelineElementAtCurrentPosition);
            this.KeyboardActionCommand = new DelegateCommand<Tuple<KeyboardAction, object>>(this.ExecuteKeyboardAction);

            this.View = view;
            this.View.Model = this;

            var visualMediaPlugin = this.manifestMediaModel.GetVisualMediaData().MediaPlugin;

            this.overlaysManager.SetAdaptivePlugin(visualMediaPlugin as IAdaptiveMediaPlugin);
            this.overlaysDisplayController.OverlaysContainer = this.View.OverlaysContainer;
            this.overlaysManager.OverlayBeginReached += this.overlaysDisplayController.ShowOverlay;
            this.overlaysManager.OverlayEndReached += this.overlaysDisplayController.HideOverlay;
            this.overlaysManager.OverlayBeginSeeked += this.overlaysDisplayController.ShowStaticOverlay;

            visualMediaPlugin.SeekCompleted += this.overlaysDisplayController.PluginSeekCompleted;
            visualMediaPlugin.CurrentStateChanged += this.overlaysDisplayController.OnPlayerStateChangedHandler;

            this.manifestMediaModel.InvokeMethodForAllMediaData(md => this.View.AddElement(md, 512, 288));

            this.View.AddOverlaysSupport();
            this.UpdateSmpteFrameRate(SmpteFrameRate.Smpte2997NonDrop);

            this.View.FullScreenChanged += this.View_FullScreenChanged;
            this.View.PlayClicked += (sender, e) => this.TogglePlay();
            this.View.PauseClicked += (sender, e) => this.TogglePlay();
            this.View.FrameRewindStarted += (sender, e) => this.StartFrameRewindForward(-1);
            this.View.FrameRewindEnded += (sender, e) => this.EndFrameForwardRewind();
            this.View.FrameForwardStarted += (sender, e) => this.StartFrameRewindForward(1);
            this.View.FrameForwardEnded += (sender, e) => this.EndFrameForwardRewind();
            this.View.PickThumbnailClicked += (sender, e) => this.PickSequenceThumbnail(null);
            this.View.SlowMotionClicked += (sender, e) => this.ToggleSlowMotion();

            this.TreatGapAsErrors = this.configurationService.GetTreatGapAsError();

            HtmlPage.RegisterScriptableObject("Player", this);
        }

        public event EventHandler<Infrastructure.DataEventArgs<object>> TitleUpdated;

        public event EventHandler<Infrastructure.DataEventArgs<object>> ResetPositionRaised;

        public PlayerStatusType PlayerStatus
        {
            get
            {
                if (this.ShouldManifestBeGenerated())
                {
                    return PlayerStatusType.NotReady;
                }

                if (this.mediaIsOpening)
                {
                    return PlayerStatusType.Loading;
                }

                return PlayerStatusType.Ready;
            }
        }

        /// <summary>
        /// Gets or sets the instance of <see cref="IPlayerView"/> as the view.
        /// </summary>
        /// <value>The <see cref="IPlayerView"/> instance as view.</value>
        public IPlayerView View { get; set; }

        /// <summary>
        /// Gets or sets the player mode.
        /// </summary>
        /// <value>The player mode.</value>
        public PlayerMode PlayerMode
        {
            get
            {
                return this.playerMode;
            }

            set
            {
                bool changed = value != this.PlayerMode;
                this.playerMode = value;

                if (changed)
                {
                    this.OnPropertyChanged("PlayerMode");
                }
            }
        }

        public VerticalWindowPosition VerticalPosition
        {
            get
            {
                return VerticalWindowPosition.Top;
            }
        }

        public HorizontalWindowPosition HorizontalPosition
        {
            get
            {
                return HorizontalWindowPosition.Right;
            }
        }

        public object Title
        {
            get
            {
                return "Sequence Preview";
            }
        }

        public ResizeDirection ResizeDirection
        {
            get
            {
                return ResizeDirection.None;
            }
        }

        public Size Size
        {
            get
            {
                return new Size(532, 375);
            }
        }

        /// <summary>
        /// Gets the command executed on fast rewind.
        /// </summary>
        /// <value>The delegate command used to start/stop fast rewind.</value>
        public DelegateCommand<object> FastRewindCommand { get; private set; }

        /// <summary>
        /// Gets the command executed on fast forward.
        /// </summary>
        /// <value>The delegate command used to start/stop fast forward.</value>
        public DelegateCommand<object> FastForwardCommand { get; private set; }

        public DelegateCommand<object> MoveToStartCommand { get; private set; }

        public DelegateCommand<object> MoveToEndCommand { get; private set; }

        public DelegateCommand<object> MediaRepeatCommand { get; private set; }

        public DelegateCommand<object> AddTimelineElementCommand { get; private set; }

        public DelegateCommand<object> MuteCommand { get; private set; }

        public DelegateCommand<Tuple<KeyboardAction, object>> KeyboardActionCommand { get; private set; }

        public KeyboardActionContext ActionContext
        {
            get
            {
                return KeyboardActionContext.Player;
            }
        }

        public bool IsMuted
        {
            get
            {
                return this.isMuted;
            }

            private set
            {
                this.isMuted = value;
                this.OnPropertyChanged("IsMuted");
            }
        }

        public bool IsDisplayed { private get; set; }

        public bool IsInLoop
        {
            get
            {
                return this.isLoopPlayback;
            }

            private set
            {
                this.isLoopPlayback = value;
                this.OnPropertyChanged("IsInLoop");
            }
        }

        /// <summary>
        /// Gets a value indicating whether the timeline is playing.
        /// </summary>
        /// <value>
        /// <c>True</c> if timeline is playing; otherwise, <c>false</c>.
        /// </value>
        private bool IsPlayModelPlaying
        {
            get
            {
                return this.manifestMediaModel.IsPlaying;
            }
        }

        /// <summary>
        /// Sets a value indicating whether the manifestMediaModel is muted.
        /// </summary>
        private bool MuteModel
        {
            set
            {
                this.manifestMediaModel.Mute = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether the Visual/Audio is visible.
        /// </summary>
        private bool IsVisibleModel
        {
            set
            {
                if (this.currentPlayingComments != null && !value)
                {
                    this.View.HideComments();
                }
            }
        }

        /// <summary>
        /// Sets a value indicating wherer Treat Gap As Errors or not
        /// </summary>
        private bool TreatGapAsErrors { get; set; }

        /// <summary>
        /// Toggles the play timeline.
        /// </summary>
        [ScriptableMember]
        public void TogglePlayTimeline()
        {
            this.PlayerMode = PlayerMode.Timeline;
            this.TogglePlayModel();
        }

        /// <summary>
        /// Stops the timeline.
        /// </summary>
        [ScriptableMember]
        public void StopTimeline()
        {
            this.PlayerMode = PlayerMode.Timeline;
            this.PauseModel();
            TimeSpan position = TimeSpan.FromSeconds(0);
            this.SetCurrentPosition(position);
            this.OnPositionUpdated(this, new PositionPayloadEventArgs(position));
        }

        public void ResetWindow(object obj)
        {
            EventHandler<Infrastructure.DataEventArgs<object>> handler = this.ResetPositionRaised;

            if (handler != null)
            {
                handler.Invoke(this, new Infrastructure.DataEventArgs<object>(this.View));
            }
        }

        /// <summary>
        /// Publishes the ThumbnailEvent.
        /// </summary>
        /// <param name="bitmap">The bitmap being published.</param>
        /// <param name="thumbnailType">The type of the Thumbnail</param>
        public void SetThumbnail(WriteableBitmap bitmap, ThumbnailType thumbnailType)
        {
            var payload = new ThumbnailEventPayload(bitmap, thumbnailType);
            this.eventAggregator.GetEvent<ThumbnailEvent>().Publish(payload);
        }

        /// <summary>
        /// Filter that indicates whether a comment can be played or not.
        /// </summary>
        /// <param name="comment">Instance of the comment.</param>
        /// <returns>True if the comment can be played, otherwise [False].</returns>
        private static bool CanPlayComment(Comment comment)
        {
            return comment != null && comment.MarkIn != null && comment.MarkOut != null
                && comment.MarkOut >= comment.MarkIn;
        }

        /// <summary>
        /// Filter for KeyMappingEvent event.
        /// </summary>
        /// <param name="keyMappingAction">Returns true if KeyMappingAction is 
        /// PlayTimeLine, PauseTimeLine or Toggle.<see cref="RCE.Infrastructure.Models.KeyMappingAction"/>.</param>
        /// <returns>True if KeyMappingAction is PlayTimeline, PauseTimeline, Toggle.</returns>
        private static bool Filter(KeyMappingAction keyMappingAction)
        {
            switch (keyMappingAction)
            {
                case KeyMappingAction.PlayTimeline:
                case KeyMappingAction.PausePlayer:
                case KeyMappingAction.Toggle:
                    return true;
                default:
                    return false;
            }
        }

        private bool CanFastRewindForward(object payload)
        {
            if (this.PlayerMode == PlayerMode.Timeline)
            {
                return this.manifestMediaModel.IsPlaying;
            }

            return false;
        }

        private void FastForward(object payload)
        {
            if (this.manifestMediaModel.IsPlaying)
            {
                this.manifestMediaModel.FastForward();
            }
        }

        private void FastRewind(object payload)
        {
            if (this.manifestMediaModel.IsPlaying)
            {
                this.manifestMediaModel.FastRewind();
            }
        }

        /// <summary>
        /// Take the action corresponding to the given key action.
        /// </summary>
        /// <param name="keyAction">Key Action Value.<seealso cref="RCE.Infrastructure.Models.KeyMappingAction"/>.</param>
        private void OnKeyAction(KeyMappingAction keyAction)
        {
            if (keyAction == KeyMappingAction.Toggle)
            {
                if (this.PlayerMode == PlayerMode.Timeline)
                {
                    this.TogglePlayModel();
                }
                else if (this.PlayerMode == PlayerMode.MediaBin || this.PlayerMode == PlayerMode.MediaLibrary)
                {
                    this.View.TogglePlay();
                }
            }
            else if (keyAction == KeyMappingAction.PlayTimeline && !this.IsPlayModelPlaying)
            {
                this.PlayerMode = PlayerMode.Timeline;
                this.PlayModel();
            }
            else if (keyAction == KeyMappingAction.PausePlayer)
            {
                if (this.PlayerMode == PlayerMode.Timeline && this.IsPlayModelPlaying)
                {
                    this.PauseModel();
                }
                else if (this.PlayerMode == PlayerMode.MediaLibrary || this.PlayerMode == PlayerMode.MediaBin)
                {
                    this.View.PausePlayer();
                }
            }
        }

        private void PickProjectThumbnail(object payload)
        {
            this.PickThumbnail(payload, ThumbnailType.ProjectThumbnail);
        }

        private void PickSequenceThumbnail(object payload)
        {
            this.PickThumbnail(payload, ThumbnailType.SequenceThumbnail);
        }

        /// <summary>
        /// Picks a thumbnail from the view.
        /// </summary>
        /// <param name="payload">The payload.</param>
        private void PickThumbnail(object payload, ThumbnailType thumbnailType)
        {
            MediaData mediaData = this.manifestMediaModel.GetVisualMediaData();

            if (this.IsPlayModelPlaying)
            {
                this.PauseModel();
            }

            this.View.PickThumbnail(mediaData, thumbnailType);
        }

        /// <summary>
        /// Updates the smpte frame rate.
        /// </summary>
        /// <param name="frameRate">The frame rate.</param>
        private void UpdateSmpteFrameRate(SmpteFrameRate frameRate)
        {
            this.View.SetCurrentSmpteFrameRate(frameRate);
        }

        /// <summary>
        /// Called when [player event published].
        /// </summary>
        /// <param name="payload">The payload.</param>
        private void OnPlayerEventPublished(PlayerEventPayload payload)
        {
            this.PlayerMode = payload.PlayerMode;

            if (payload.PlayerMode == PlayerMode.Timeline)
            {
                this.TogglePlayModel();
            }
            else
            {
                if (!(payload.Asset is FolderAsset))
                {
                    this.View.HidePreviewImage();
                    this.View.SetSource(payload.Asset);
                }
            }
        }

        /// <summary>
        /// Called when [pause event published].
        /// </summary>
        /// <param name="payload">The payload.</param>
        private void OnPauseEventPublished(object payload)
        {
            this.PauseModel();
        }

        /// <summary>
        /// Called when [play event published].
        /// </summary>
        /// <param name="payload">The payload.</param>
        private void OnPlayEventPublished(object payload)
        {
            this.PlayModel();
        }

        /// <summary>
        /// Handles the playhead position update event.
        /// </summary>
        /// <param name="payload">Holds the new position of the playhead.</param>
        private void UpdatePosition(PositionPayloadEventArgs payload)
        {
            this.PlayerMode = PlayerMode.Timeline;

            this.SetCurrentPosition(payload.Position);
            this.View.SetCurrentTime(payload.Position);
            this.HandleCommentsAtCurrentPosition(payload.Position.TotalSeconds);
            this.View.RemoveOverlayPreviews();
        }

        /// <summary>
        /// Updates the aspect ratio for the asset for the player control.
        /// </summary>
        /// <param name="selectedAspectRatio">New aspect ratio.</param>
        private void UpdatePlayerAspectRatio(AspectRatio selectedAspectRatio)
        {
            this.View.SetAspectRatio(selectedAspectRatio);
        }

        /// <summary>
        /// Handles the PlayCommentEvent event. <seealso cref="RCE.Infrastructure.Events.PlayCommentEvent"/>.
        /// </summary>
        /// <param name="comment">Comment to be played.</param>
        private void PlayComment(Comment comment)
        {
            if (comment != null)
            {
                this.PlayerMode = PlayerMode.Comment;
                TimeSpan commentPosition = TimeSpan.FromSeconds(comment.MarkIn.GetValueOrDefault());

                // To pause the aggregate model so that the OnFrameRendered event could not be triggered.
                this.PauseModel();
                this.SetCurrentPosition(commentPosition);
                this.View.SetCurrentTime(commentPosition);
                this.eventAggregator.GetEvent<PositionUpdatedEvent>().Publish(new PositionPayloadEventArgs(commentPosition));
                this.currentPlayingComment = comment;
                this.PlayModel();
            }
        }

        /// <summary>
        /// Sets the position of all the <see cref="IAggregateMediaModel"/>(visual/audio/title).
        /// </summary>
        /// <param name="position">The position.</param>
        private void SetCurrentPosition(TimeSpan position)
        {
            this.manifestMediaModel.Position = position;

            if (position.TotalSeconds > 0)
            {
                if (!this.ShouldManifestBeGenerated() && this.manifestMediaModel.Position != position && !this.manifestMediaModel.IsStopped)
                {
                    this.View.EnablePlayButton(false);
                }
            }
        }

        /// <summary>
        /// Toggles between play/pause.
        /// </summary>
        private void TogglePlay()
        {
            if (this.PlayerMode == PlayerMode.Timeline || this.PlayerMode == PlayerMode.Comment)
            {
                this.TogglePlayModel();
            }
            else
            {
                this.View.TogglePlay();
            }

            this.FastRewindCommand.RaiseCanExecuteChanged();
            this.FastForwardCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Toggles the SlowMotion of the current Media Data.
        /// </summary>
        private void ToggleSlowMotion()
        {
            if (this.PlayerMode == PlayerMode.Timeline)
            {
                this.manifestMediaModel.PlaySlowMotion();

                var rcePlugin = this.manifestMediaModel.GetVisualMediaData().MediaPlugin as IRCESmoothStreamingMediaPlugin;
                if (rcePlugin != null)
                {
                    var isSlowMotion = rcePlugin.PlaySpeedManager.IsSlowMotion;
                    this.View.ToggleSlowMotion(isSlowMotion);
                }
            }
            else
            {
                this.View.ToggleSlowMotion(false);
            }
        }

        /// <summary>
        /// Moves to start of the current playing media in the player.
        /// </summary>
        private void MoveToStart(object o)
        {
            if (this.PlayerMode == PlayerMode.Timeline)
            {
                this.MoveToStartModel();
            }
            else if (this.playerMode == PlayerMode.Comment)
            {
                this.MoveToStartOfComment();
            }
            else
            {
                this.View.MoveToStart();
            }
        }

        /// <summary>
        /// Moves to start of current playing comment.
        /// </summary>
        private void MoveToStartOfComment()
        {
            if (this.currentPlayingComment != null)
            {
                TimeSpan position = TimeSpan.FromMilliseconds(this.currentPlayingComment.MarkIn.GetValueOrDefault() * 1000);
                this.SetCurrentPosition(position);
                this.OnPositionUpdated(this, new PositionPayloadEventArgs(position));
            }
        }

        /// <summary>
        /// Moves to end of current playing media.
        /// </summary>
        private void MoveToEnd(object o)
        {
            if (this.PlayerMode == PlayerMode.Timeline)
            {
                this.MoveToEndModel();
            }
            else if (this.playerMode == PlayerMode.Comment)
            {
                this.MoveToEndOfComment();
            }
            else
            {
                this.View.MoveToEnd();
            }
        }

        private void UpdateTreatGapAsErrorValue(bool payload)
        {
            this.TreatGapAsErrors = payload;
        }

        private void ToggleRubberBanding(RubberBandingStateChangedPayload payload)
        {
            var enabled = payload.IsEnabled;
            var volume = payload.TrackVolume;

            this.manifestMediaModel.ChangeVolumeSettingsRubberBanding(enabled, volume);
        }

        private void ToggleTrackMute(TrackMuteStateChangedPayload payload)
        {
            this.manifestMediaModel.ChangeTrackMute(payload.TrackNumber - 1, payload.IsMuted);
        }

        /// <summary>
        /// Starts the rewind forward.
        /// </summary>
        /// <param name="skipDirection">The skip direction.</param>
        private void StartFrameRewindForward(int skipDirection)
        {
            if (this.PlayerMode == PlayerMode.Timeline || this.playerMode == PlayerMode.Comment)
            {
                this.StartFrameForwardRewindModel(skipDirection);
            }
            else
            {
                this.View.StartFrameRewindForward(skipDirection);
            }
        }

        /// <summary>
        /// Ends the forward rewind.
        /// </summary>
        private void EndFrameForwardRewind()
        {
            if (this.PlayerMode == PlayerMode.Timeline || this.playerMode == PlayerMode.Comment)
            {
                this.StopFrameForwardRewindModel();
            }
            else
            {
                this.View.EndFrameRewindForward();
            }
        }

        /// <summary>
        /// Toggles the loop playback.
        /// </summary>
        private void ToggleLoopPlayback(object o)
        {
            this.ToggleLoopPlaybackModel();
            this.View.ToggleLoopPlayback();
        }

        /// <summary>
        /// Toggles the play model between play/pause.
        /// </summary>
        private void TogglePlayModel()
        {
            if (this.IsPlayModelPlaying)
            {
                this.PauseModel();
            }
            else
            {
                this.View.RemoveOverlayPreviews();
                this.PlayModel();
            }
        }

        /// <summary>
        /// Toggles the loop playback.
        /// </summary>
        private void ToggleLoopPlaybackModel()
        {
            this.IsInLoop = !this.IsInLoop;
        }

        /// <summary>
        /// Plays all the <see cref="IAggregateMediaModel"/>(visual/audio/title).
        /// </summary>
        private void PlayModel()
        {
            if (this.TreatGapAsErrors && this.SequenceHasGap())
            {
                this.View.ShowSequenceHasGapErrorMessage(true);
                return;
            }
            
            this.View.EnablePlayButton(false);
            this.mediaIsOpening = true;
            this.View.ShowErrorMessage(false);
            this.View.ShowSequenceHasGapErrorMessage(false);

            if (this.ShouldManifestBeGenerated())
            {
                this.overlaysManager.Reset();
                this.manifestMediaModel.ResetRubberBandingManagers();
                this.manifestMediaModel.ResetTransitionsManagers();
                
                for (int i = 0; i <= this.maxNumberOfAudioTracks; i++)
                {
                    // all tracks have no stream to play
                    this.manifestMediaModel.InvokeMethodForAllMediaData(md => md.Hide());
                    this.manifestMediaModel.SetStreamSource(i, null);
                    this.manifestMediaModel.Stop();
                }

                this.ResetOperationCount();
                this.playbackManifestGenerator.BeginManifestGeneration(this.OnPlaybackManifestGenerated);

                var mediaData = this.manifestMediaModel.GetVisualMediaData();
                mediaData.MediaPlugin.CurrentStateChanged += ModelVisualMediaPlugin_CurrentStateChanged;
            }
            else
            {
                this.View.EnablePlayButton(true);
                this.manifestMediaModel.Play();
            }

            this.MuteModel = this.View.IsMuted;
            this.View.TogglePlayVisibility(this.IsPlayModelPlaying);
            this.eventAggregator.GetEvent<PlayClickedEvent>().Publish(null);
        }

        private void ModelVisualMediaPlugin_CurrentStateChanged(IMediaPlugin mediaPlugin, MediaPluginState state)
        {
            var playableMediaData = this.manifestMediaModel.GetVisualMediaData() as ManifestPlayableMediaData;

            if (playableMediaData != null)
            {
                if (!playableMediaData.LastSeekPosition.HasValue || playableMediaData.LastSeekPosition.Value == playableMediaData.Position)
                {
                    this.View.EnablePlayButton(true);
                }
            }
        }

        private bool SequenceHasGap()
        {
            return this.sequenceRegistry.CurrentSequenceModel.SequenceHasGap();
        }

        private void OnPlaybackManifestGenerated(IDictionary<Track, string> manifestByTrack)
        {
            foreach (var manifestTrackPair in manifestByTrack)
            {
                int trackId = manifestTrackPair.Key.Number;
                Stream manifestStream = new MemoryStream();
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(manifestTrackPair.Value);
                manifestStream.Write(bytes, 0, bytes.Length);
                manifestStream.Position = 0;

                this.manifestMediaModel.SetStreamSource(trackId - 1, manifestStream);
                this.manifestMediaModel.ChangeVolumeSettingsRubberBanding(true, 1);
            }

            this.manifestMediaModel.Position = this.GetTimeSpanFromSeconds(this.GetPlayHeadPosition());
            this.manifestMediaModel.Play();
        }

        private TimeSpan GetTimeSpanFromSeconds(double positionInSeconds)
        {
            return TimeSpan.FromSeconds(positionInSeconds);
        }

        private double GetPlayHeadPosition()
        {
            return this.sequenceRegistry.CurrentSequenceModel.CurrentPosition.TotalSeconds;
        }

        /// <summary>
        /// Pauses all the <see cref="IAggregateMediaModel"/>(visual/audio/title).
        /// </summary>
        private void PauseModel()
        {
            this.manifestMediaModel.Pause();

            this.View.EnablePlayButton(true);
            this.View.TogglePlayVisibility(this.IsPlayModelPlaying);
        }

        /// <summary>
        /// Moves to start of the <see cref="IAggregateMediaModel"/>.
        /// </summary>
        private void MoveToStartModel()
        {
            var position = TimeSpan.FromSeconds(0);
            this.SetCurrentPosition(position);
            this.OnPositionUpdated(this, new PositionPayloadEventArgs(position));
        }

        /// <summary>
        /// Handles the FullScreenChanged event of the View control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RCE.Infrastructure.Models.FullScreenModeEventArgs"/> instance containing the event data.</param>
        private void View_FullScreenChanged(object sender, FullScreenModeEventArgs e)
        {
            this.eventAggregator.GetEvent<FullScreenEvent>().Publish(e.Mode);
        }

        /// <summary>
        /// Moves to end of the timeline element.
        /// </summary>
        private void MoveToEndModel()
        {
            var position = this.manifestMediaModel.Duration.TotalSeconds > 0.5 ? this.manifestMediaModel.Duration.Subtract(new TimeSpan(0, 0, 0, 0, 500)) : this.manifestMediaModel.Duration;
            this.SetCurrentPosition(position);
            this.OnPositionUpdated(this, new PositionPayloadEventArgs(position));
        }

        /// <summary>
        /// Sets the position to the end of the comment.
        /// </summary>
        private void MoveToEndOfComment()
        {
            if (this.currentPlayingComment != null)
            {
                this.PauseModel();
                TimeSpan position = TimeSpan.FromSeconds((double)this.currentPlayingComment.MarkOut);
                this.PauseModel();
                this.SetCurrentPosition(position);
                this.OnPositionUpdated(this, new PositionPayloadEventArgs(position));
            }
        }

        /// <summary>
        /// Starts the forward/rewind model.
        /// </summary>
        /// <param name="skipDirection">The skip direction.</param>
        private void StartFrameForwardRewindModel(int skipDirection)
        {
            this.PauseModel();
            this.currentSkipDirection = skipDirection;
            this.frameRewindForwardTimer.Start();
        }

        /// <summary>
        /// Stops the forward/rewind model.
        /// </summary>
        private void StopFrameForwardRewindModel()
        {
            this.currentSkipDirection = 0;
            this.frameRewindForwardTimer.Stop();
        }

        /// <summary>
        /// Handles the Rewind/Forward of the <see cref="IAggregateMediaModel"/>.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void FrameRewindForwardTimerTick(object sender, EventArgs e)
        {
            if (this.currentSkipDirection == 0)
            {
                return;
            }

            bool add = this.currentSkipDirection > 0;
            long newSkipDirection = 1;

            TimeCode currentTimeCode = TimeCode.FromTimeSpan(this.manifestMediaModel.Position, this.sequenceRegistry.CurrentSequenceModel.Duration.FrameRate);
            long currentTotalFrames = currentTimeCode.TotalFrames;

            TimeCode frameTimeCode = TimeCode.FromFrames(newSkipDirection, this.sequenceRegistry.CurrentSequenceModel.Duration.FrameRate);

            currentTimeCode = add ? currentTimeCode.Add(frameTimeCode) : currentTimeCode.Subtract(frameTimeCode);
            newSkipDirection++;

            while (currentTimeCode.TotalFrames == currentTotalFrames)
            {
                frameTimeCode = TimeCode.FromFrames(newSkipDirection, this.sequenceRegistry.CurrentSequenceModel.Duration.FrameRate);
                currentTimeCode = add ? currentTimeCode.Add(frameTimeCode) : currentTimeCode.Subtract(frameTimeCode);
                newSkipDirection++;
            }

            TimeSpan position = TimeSpan.FromSeconds(Math.Max(0, currentTimeCode.TotalSeconds));

            // Check if the playing mode is comment. If yes then don't allow to go forwar/backwar
            // beyond the comment Markin and MarkOut position.
            if (this.PlayerMode == PlayerMode.Comment && this.currentPlayingComment != null)
            {
                if (position.TotalSeconds < this.currentPlayingComment.MarkIn)
                {
                    position = TimeSpan.FromMilliseconds(this.currentPlayingComment.MarkIn.GetValueOrDefault() * 1000);
                }
                else if (position.TotalSeconds > this.currentPlayingComment.MarkOut)
                {
                    position = TimeSpan.FromMilliseconds(this.currentPlayingComment.MarkOut.GetValueOrDefault() * 1000);
                }
            }

            this.manifestMediaModel.Position = position;
            this.OnPositionUpdated(this, new PositionPayloadEventArgs(position));
        }

        /// <summary>
        /// Called when position of the <see cref="IAggregateMediaModel"/> is updated.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RCE.Infrastructure.Events.PositionPayloadEventArgs"/> instance containing the event data.</param>
        private void OnPositionUpdated(object sender, PositionPayloadEventArgs e)
        {
            this.View.SetCurrentTime(e.Position);
            this.eventAggregator.GetEvent<PositionUpdatedEvent>().Publish(e);
            this.HandleCommentsAtCurrentPosition(e.Position.TotalSeconds);
            this.StopPlayingComment(e.Position.TotalSeconds);
        }

        /// <summary>
        /// Handles the comment at current position.
        /// It shows if there is any comment at the current position and hides if the current 
        /// comment is not null and the current position is not in between the In and Out position 
        /// of the current comment.
        /// </summary>
        /// <param name="timePosition">The time position.</param>
        private void HandleCommentsAtCurrentPosition(double timePosition)
        {
            List<Comment> comments = this.sequenceRegistry.CurrentSequence.CommentElements.Where(
                        x => x.MarkIn <= timePosition && x.MarkOut >= timePosition).ToList();

            if (comments.Count > 0 && this.currentPlayingComments != comments)
            {
                this.View.ShowComments(comments);
                this.currentPlayingComments = comments;
            }
            else
            {
                this.HideComments();
            }
        }

        /// <summary>
        /// Stops playing comment if the position reaches to the out position of the comment.
        /// </summary>
        /// <param name="position">The current position.</param>
        private void StopPlayingComment(double position)
        {
            if (this.playerMode == PlayerMode.Comment && this.currentPlayingComment != null
                && this.IsPlayModelPlaying && position > this.currentPlayingComment.MarkOut)
            {
                this.PauseModel();
            }
        }

        /// <summary>
        /// Hides the comment.
        /// </summary>
        private void HideComments()
        {
            this.View.HideComments();
            this.currentPlayingComments = null;
        }

        private void OnManifestMediaModelPlayFinished(object sender, EventArgs e)
        {
            if (this.IsInLoop)
            {
                this.SetCurrentPosition(TimeSpan.FromSeconds(0));
                this.PlayModel();
            }

            this.OnPositionUpdated(this, new PositionPayloadEventArgs(this.manifestMediaModel.Duration));
        }

        private void OnManifestMediaModelMediaElementFailed(IMediaPlugin plugin)
        {
            if (plugin != null)
            {
                for (int i = 0; i <= this.maxNumberOfAudioTracks; i++)
                {
                    // all tracks have no strem to play
                    this.manifestMediaModel.InvokeMethodForAllMediaData(md => md.Hide());
                    this.manifestMediaModel.SetStreamSource(i, null);
                    this.manifestMediaModel.Stop();
                }

                this.operationBalance++;
                this.OnPropertyChanged("PlayerStatus");

                this.View.EnablePlayButton(true);
                this.View.ShowErrorMessage(true);
            }
        }

        /// <summary>`
        /// Handles the PropertyChanged event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">PropertyChangedEventArgs arguments.</param>
        private void PlayerViewPresenter_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "PlayerMode")
            {
                if (this.PlayerMode == PlayerMode.Timeline || this.PlayerMode == PlayerMode.Comment)
                {
                    if (this.View != null)
                    {
                        this.View.Stop();
                        this.View.HidePreviewImage();
                        this.IsVisibleModel = true;
                    }
                }
                else
                {
                    this.PauseModel();
                    this.IsVisibleModel = false;
                }
            }
        }

        /// <summary>
        /// Adds the comment at the current position if the playermode is Timeline.
        /// </summary>
        /// <param name="sender">The sender.</param>
        private void AddTimelineElementAtCurrentPosition(object sender)
        {
            if (this.playerMode == PlayerMode.Timeline)
            {
                this.eventAggregator.GetEvent<PositionDoubleClickedEvent>().Publish(new PositionPayloadEventArgs(TimeSpan.FromSeconds(this.sequenceRegistry.CurrentSequenceModel.CurrentPosition.TotalSeconds), CommentMode.Timeline));
            }
        }

        /// <summary>
        /// Handles the MuteClicked event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        private void MutePlayer(object sender)
        {
            if (this.playerMode == PlayerMode.Timeline)
            {
                this.IsMuted = !this.View.IsMuted;
                this.MuteModel = !this.View.IsMuted;
            }

            this.View.IsMuted = !this.View.IsMuted;
        }

        private void ExecuteKeyboardAction(Tuple<KeyboardAction, object> parameter)
        {
            switch (parameter.Item1)
            {
                case KeyboardAction.MoveToStart:
                    this.MoveToStart(null);
                    break;

                case KeyboardAction.MoveToEnd:
                    this.MoveToEnd(null);
                    break;

                case KeyboardAction.LoopPlayback:
                    this.ToggleLoopPlayback(null);
                    break;

                case KeyboardAction.AddTimelineElement:
                    this.AddTimelineElementAtCurrentPosition(null);
                    break;

                case KeyboardAction.Mute:
                    this.MutePlayer(null);
                    break;

                case KeyboardAction.Rewind:
                case KeyboardAction.Forward:
                case KeyboardAction.FullScreen:
                    this.View.HandleKeyboardAction(parameter.Item1);
                    break;
            }
        }

        private void OnPlayingStateChanged(object sender, EventArgs e)
        {
            this.View.TogglePlayVisibility(this.IsPlayModelPlaying);

            this.FastRewindCommand.RaiseCanExecuteChanged();
            this.FastForwardCommand.RaiseCanExecuteChanged();

            if (this.mediaIsOpening)
            {
                this.mediaIsOpening = false;
                this.OnPropertyChanged("PlayerStatus");
            }

            if (this.IsPlayModelPlaying)
            {
                this.View.EnablePlayButton(true);
            }
        }

        private void OperationUndone(object parameter)
        {
            if (this.IsPlayModelPlaying)
            {
                this.PauseModel();
            }

            this.operationBalance--;
            this.OnPropertyChanged("PlayerStatus");
        }

        private void OperationExecuted(object parameter)
        {
            if (this.IsPlayModelPlaying)
            {
                this.PauseModel();
            }

            this.operationBalance++;
            this.OnPropertyChanged("PlayerStatus");
        }

        private void ResetOperationCount()
        {
            this.operationBalance = 0;
            this.OnPropertyChanged("PlayerStatus");
        }

        private bool ShouldManifestBeGenerated()
        {
            return this.operationBalance != 0;
        }

        private void ShowOverlayPreview(PreviewOverlayPayload payload)
        {
            OverlayAsset overlay = payload.OverlayAsset;

            if (overlay != null)
            {
                this.View.AddXamlElement(overlay.XamlResource, this.GetOverlayMetadata(overlay), overlay.PositionX, overlay.PositionY, overlay.Height, overlay.Width);
            }

            this.View.RemovePlaybackOverlays();
        }

        private void HideOverlayPreview(object payload)
        {
            this.View.RemoveOverlayPreviews();
        }

        private IDictionary<string, string> GetOverlayMetadata(OverlayAsset overlay)
        {
            var metadataList = overlay.Metadata;

            return metadataList.ToDictionary(metadata => metadata.Name, metadata => metadata.Value as string);
        }
    }
}
