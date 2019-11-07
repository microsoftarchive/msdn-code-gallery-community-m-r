// <copyright file="PlayerViewPresenterFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: PlayerViewPresenterFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Player.Tests.Views
{
    using System;
    using System.Windows.Media.Imaging;
    using Infrastructure.DragDrop;
    using Infrastructure.Events;
    using Infrastructure.Models;
    using Microsoft.Practices.Composite.Presentation.Events;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;

    using RCE.Modules.Player.Models;

    using SMPTETimecode;
    using Comment = RCE.Infrastructure.Models.Comment;
    using Track = RCE.Infrastructure.Models.Track;
    using TrackType = RCE.Infrastructure.Models.TrackType;

    /// <summary>
    /// Test class for <see cref="PlayerViewPresenter"/>.
    /// </summary>
    [TestClass]
    public class PlayerViewPresenterFixture
    {
        /// <summary>
        /// Mock for <see cref="PlayerView"/>.
        /// </summary>
        private MockPlayerView view;

        /// <summary>
        /// Mock for <see cref="Microsoft.Practices.Composite.Events.IEventAggregator"/>.
        /// </summary>
        private MockEventAggregator eventAggregator;

        /// <summary>
        /// Mock for <see cref="SequenceModel"/>.
        /// </summary>
        private MockSequenceModel sequenceModel;

        /// <summary>
        /// Mock for <see cref="KeyMappingEvent"/>.
        /// </summary>
        private MockKeyMappingEvent keyMappingEvent;

        /// <summary>
        /// Mock for <see cref="FullScreenEvent"/>.
        /// </summary>
        private MockFullScreenEvent fullScreenEvent;

        /// <summary>
        /// Mock for <see cref="PlayheadMovedEvent"/>.
        /// </summary>
        private MockPlayheadMovedEvent playheadMovedEvent;

        /// <summary>
        /// Mock for <see cref="PositionDoubleClickedEvent"/>.
        /// </summary>
        private MockPositionDoubleClickedEvent addCommentEvent;

        /// <summary>
        /// Mock for <see cref="SmpteTimeCodeChangedEvent"/>.
        /// </summary>
        private MockSmpteTimecodeChangedEvent smpteTimeCodeChangedEvent;

        /// <summary>
        /// Mock for <see cref="AspectRatioChangedEvent"/>.
        /// </summary>
        private MockAspectRatioChangedEvent aspectRatioChangedEvent;

        /// <summary>
        /// Mock for <see cref="PositionUpdatedEvent"/>.
        /// </summary>
        private MockPositionUpdatedEvent positionUpdatedEvent;

        /// <summary>
        /// Mock for <see cref="PauseEvent"/>.
        /// </summary>
        private MockPauseEvent pauseEvent;

        /// <summary>
        /// Mock for <see cref="PlayerEvent"/>.
        /// </summary>
        private MockPlayerEvent playerEvent;

        /// <summary>
        /// Mock for <see cref="PlayCommentEvent"/>.
        /// </summary>
        private MockPlayCommentEvent playCommentEvent;

        /// <summary>
        /// Mock for <see cref="HideMetadataEvent"/>.
        /// </summary>
        private MockHideMetadataEvent hideMetadataEvent;

        /// <summary>
        /// Mock for <see cref="ShowMetadataEvent"/>.
        /// </summary>
        private MockShowMetadataEvent showMetadataEvent;

        /// <summary>
        /// Mock for <see cref="PickThumbnailEvent"/>
        /// </summary>
        private MockPickThumbnailEvent pickThumbnailEvent;
        
        /// <summary>
        /// Mock for <see cref="PlayClickedEvent"/>
        /// </summary>
        private MockPlayClickedEvent playClickedEvent;

        /// <summary>
        /// Mock for <see cref="thumbnailEvent"/>.
        /// </summary>
        private MockThumbnailEvent thumbnailEvent;

        private MockCacheManager cacheManager;

        private MockPlayEvent playEvent;

        private MockSequenceRegistry sequenceRegistry;

        private MockPlaybackManifestGenerator playbackManifestGenerator;

        private MockOverlaysManager overlaysManager;

        private MockRubberBandingManager rubberBandingManager;

        private MockOverlaysDisplayController overlaysDisplayController;

        private MockManifestMediaModel manifestMediaModel;

        private MockShowPreviewOverlayEvent showPreviewOverlayEvent;

        private MockHidePreviewOverlayEvent hidePreviewOverlayEvent;

        private MockResetWindowsEvent resetWindowsEvent;

        private MockConfigurationService configurationSettings;

        private MockRubberBandingStateChangedEvent rubberBandingStateChangedEvent;

        private MockOperationExecutedInTimelineEvent operationExecutedInTimelineEvent;

        private MockOperationUndoneInTimelineEvent operationUndoneInTimelineEvent;

        private CheckedTreatGapAsErrorEvent checkedTreatGapAsErrorEvent;

        private TrackMuteStateChangedEvent trackMuteStateChangedEvent;

        /// <summary>
        /// Initializes the test class.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.overlaysManager = new MockOverlaysManager();
            this.overlaysDisplayController = new MockOverlaysDisplayController();
            this.rubberBandingManager = new MockRubberBandingManager();
            this.manifestMediaModel = new MockManifestMediaModel();

            this.view = new MockPlayerView();
            this.eventAggregator = new MockEventAggregator();
            this.sequenceModel = new MockSequenceModel();
            this.playbackManifestGenerator = new MockPlaybackManifestGenerator();
            this.keyMappingEvent = new MockKeyMappingEvent();
            this.fullScreenEvent = new MockFullScreenEvent();
            this.playheadMovedEvent = new MockPlayheadMovedEvent();
            this.pauseEvent = new MockPauseEvent();
            this.addCommentEvent = new MockPositionDoubleClickedEvent();
            this.positionUpdatedEvent = new MockPositionUpdatedEvent();
            this.playerEvent = new MockPlayerEvent();
            this.playCommentEvent = new MockPlayCommentEvent();
            this.hideMetadataEvent = new MockHideMetadataEvent();
            this.showMetadataEvent = new MockShowMetadataEvent();
            this.showPreviewOverlayEvent = new MockShowPreviewOverlayEvent();
            this.hidePreviewOverlayEvent = new MockHidePreviewOverlayEvent();
            this.resetWindowsEvent = new MockResetWindowsEvent();
            this.rubberBandingStateChangedEvent = new MockRubberBandingStateChangedEvent();
            this.operationExecutedInTimelineEvent = new MockOperationExecutedInTimelineEvent();
            this.operationUndoneInTimelineEvent = new MockOperationUndoneInTimelineEvent();
            this.playEvent = new MockPlayEvent();
            this.playClickedEvent = new MockPlayClickedEvent();
            this.checkedTreatGapAsErrorEvent = new MockCheckedTreatGapAsErrorEvent();
            this.trackMuteStateChangedEvent = new MockTrackMuteStateChangedEvent();

            this.aspectRatioChangedEvent = new MockAspectRatioChangedEvent();
            this.smpteTimeCodeChangedEvent = new MockSmpteTimecodeChangedEvent();
            this.pickThumbnailEvent = new MockPickThumbnailEvent();
            this.thumbnailEvent = new MockThumbnailEvent();
            this.cacheManager = new MockCacheManager();
            this.configurationSettings = new MockConfigurationService();

            this.sequenceRegistry = new MockSequenceRegistry();
            this.sequenceRegistry.CurrentSequenceModel = this.sequenceModel;

            this.eventAggregator.AddMapping<PlayClickedEvent>(this.playClickedEvent);
            this.eventAggregator.AddMapping<KeyMappingEvent>(this.keyMappingEvent);
            this.eventAggregator.AddMapping<FullScreenEvent>(this.fullScreenEvent);
            this.eventAggregator.AddMapping<SmpteTimeCodeChangedEvent>(this.smpteTimeCodeChangedEvent);
            this.eventAggregator.AddMapping<PlayheadMovedEvent>(this.playheadMovedEvent);
            this.eventAggregator.AddMapping<AspectRatioChangedEvent>(this.aspectRatioChangedEvent);
            this.eventAggregator.AddMapping<PauseEvent>(this.pauseEvent);
            this.eventAggregator.AddMapping<PositionUpdatedEvent>(this.positionUpdatedEvent);
            this.eventAggregator.AddMapping<PlayerEvent>(this.playerEvent);
            this.eventAggregator.AddMapping<PlayCommentEvent>(this.playCommentEvent);
            this.eventAggregator.AddMapping<PositionDoubleClickedEvent>(this.addCommentEvent);
            this.eventAggregator.AddMapping<HideMetadataEvent>(this.hideMetadataEvent);
            this.eventAggregator.AddMapping<ShowMetadataEvent>(this.showMetadataEvent);
            this.eventAggregator.AddMapping<PickThumbnailEvent>(this.pickThumbnailEvent);
            this.eventAggregator.AddMapping<ThumbnailEvent>(this.thumbnailEvent);
            this.eventAggregator.AddMapping<PlayEvent>(this.playEvent);
            this.eventAggregator.AddMapping<ShowPreviewOverlayEvent>(this.showPreviewOverlayEvent);
            this.eventAggregator.AddMapping<HidePreviewOverlayEvent>(this.hidePreviewOverlayEvent);
            this.eventAggregator.AddMapping<RubberBandingStateChangedEvent>(this.rubberBandingStateChangedEvent);
            this.eventAggregator.AddMapping<ResetWindowsEvent>(this.resetWindowsEvent);
            this.eventAggregator.AddMapping<OperationExecutedInTimelineEvent>(this.operationExecutedInTimelineEvent);
            this.eventAggregator.AddMapping<OperationUndoneInTimelineEvent>(this.operationUndoneInTimelineEvent);
            this.eventAggregator.AddMapping<CheckedTreatGapAsErrorEvent>(this.checkedTreatGapAsErrorEvent);
            this.eventAggregator.AddMapping<TrackMuteStateChangedEvent>(this.trackMuteStateChangedEvent);
        }

        /// <summary>
        /// Determines whether this instance inits the view.
        /// </summary>
        [TestMethod]
        public void CanInitPresenter()
        {
            var presenter = this.CreatePresenter();

            Assert.AreEqual(this.view, presenter.View);
        }

        /// <summary>
        /// Tests that <see cref="PlayerViewPresenter"/> calls TogglePlay when <see cref="KeyMappingEvent"/>
        /// is published and <see cref="PlayerMode"/> is MediaBin.
        /// </summary>
        [TestMethod]
        public void ShouldCallTogglePlayWhenKeyMappingEventIsTriggerdAndPlayerModeIsMediaBin()
        {
            Assert.IsNull(this.keyMappingEvent.SubscribeArgumentAction);
            Assert.IsNull(this.keyMappingEvent.SubscribeArgumentFilter);

            var presenter = this.CreatePresenter();
            presenter.PlayerMode = PlayerMode.MediaBin;
            Assert.IsFalse(this.view.TogglePlayCalled);

            Assert.IsNotNull(this.keyMappingEvent.SubscribeArgumentAction);
            Assert.IsNotNull(this.keyMappingEvent.SubscribeArgumentFilter);
            Assert.AreEqual(ThreadOption.PublisherThread, this.keyMappingEvent.SubscribeArgumentThreadOption);

            this.keyMappingEvent.SubscribeArgumentAction(KeyMappingAction.Toggle);

            Assert.IsTrue(this.view.TogglePlayCalled);
        }

        /// <summary>
        /// Tests that <see cref="PlayerViewPresenter"/> calls TogglePlay when <see cref="KeyMappingEvent"/>
        /// is published and <see cref="PlayerMode"/> is Library.
        /// </summary>
        [TestMethod]
        public void ShouldCallTogglePlayWhenKeyMappingEventIsTriggerdAndPlayerModeIsMediaLibrary()
        {
            Assert.IsNull(this.keyMappingEvent.SubscribeArgumentAction);
            Assert.IsNull(this.keyMappingEvent.SubscribeArgumentFilter);

            var presenter = this.CreatePresenter();
            presenter.PlayerMode = PlayerMode.MediaLibrary;
            Assert.IsFalse(this.view.TogglePlayCalled);

            Assert.IsNotNull(this.keyMappingEvent.SubscribeArgumentAction);
            Assert.IsNotNull(this.keyMappingEvent.SubscribeArgumentFilter);
            Assert.AreEqual(ThreadOption.PublisherThread, this.keyMappingEvent.SubscribeArgumentThreadOption);

            this.keyMappingEvent.SubscribeArgumentAction(KeyMappingAction.Toggle);

            Assert.IsTrue(this.view.TogglePlayCalled);
        }

        /// <summary>
        /// Tests that <see cref="PlayerViewPresenter"/> doesn't call TogglePlay when <see cref="KeyMappingEvent"/>
        /// is published and <see cref="PlayerMode"/> is Timeline.
        /// </summary>
        [TestMethod]
        public void ShouldNotCallTogglePlayWhenKeyMappingEventIsTriggerdAndPlayerModeIsTimeline()
        {
            Assert.IsNull(this.keyMappingEvent.SubscribeArgumentAction);
            Assert.IsNull(this.keyMappingEvent.SubscribeArgumentFilter);

            var presenter = this.CreatePresenter();
            presenter.PlayerMode = PlayerMode.Timeline;
            Assert.IsFalse(this.view.TogglePlayCalled);

            Assert.IsNotNull(this.keyMappingEvent.SubscribeArgumentAction);
            Assert.IsNotNull(this.keyMappingEvent.SubscribeArgumentFilter);
            Assert.AreEqual(ThreadOption.PublisherThread, this.keyMappingEvent.SubscribeArgumentThreadOption);

            this.keyMappingEvent.SubscribeArgumentAction(KeyMappingAction.Toggle);

            Assert.IsFalse(this.view.TogglePlayCalled);
        }

        /// <summary>
        /// Tests that <see cref="PlayerViewPresenter"/> calls TogglePlay method when
        /// <see cref="KeyMappingEvent"/> is published.
        /// </summary>
        [TestMethod]
        public void ShouldCallPlayWithKeyMappingEventSubscription()
        {
            Assert.IsNull(this.keyMappingEvent.SubscribeArgumentAction);
            Assert.IsNull(this.keyMappingEvent.SubscribeArgumentFilter);

            var presenter = this.CreatePresenter();

            Assert.IsFalse(this.view.TogglePlayVisibilityCalled);

            Assert.IsNotNull(this.keyMappingEvent.SubscribeArgumentAction);
            Assert.IsNotNull(this.keyMappingEvent.SubscribeArgumentFilter);
            Assert.AreEqual(ThreadOption.PublisherThread, this.keyMappingEvent.SubscribeArgumentThreadOption);

            this.keyMappingEvent.SubscribeArgumentAction(KeyMappingAction.Toggle);

            Assert.IsTrue(this.view.TogglePlayVisibilityCalled);
        }

        /// <summary>
        /// Tests that <see cref="PlayerViewPresenter"/> set the playermode to timeline when 
        /// <see cref="KeyMappingEvent"/> is published.
        /// </summary>
        [TestMethod]
        public void ShouldSetPlayerModeToTimeLineIfKeyMappingActionIsPlayTimelineWithKeyMappingEventSubscription()
        {
            Assert.IsNull(this.keyMappingEvent.SubscribeArgumentAction);
            Assert.IsNull(this.keyMappingEvent.SubscribeArgumentFilter);

            var presenter = this.CreatePresenter();

            Assert.IsNotNull(this.keyMappingEvent.SubscribeArgumentAction);
            Assert.IsNotNull(this.keyMappingEvent.SubscribeArgumentFilter);
            Assert.AreEqual(ThreadOption.PublisherThread, this.keyMappingEvent.SubscribeArgumentThreadOption);

            presenter.PlayerMode = PlayerMode.Comment;

            this.keyMappingEvent.SubscribeArgumentAction(KeyMappingAction.PlayTimeline);

            Assert.AreEqual(PlayerMode.Timeline, presenter.PlayerMode);
        }

        /// <summary>
        /// Tests that <see cref="PlayerViewPresenter"/> publishes <see cref="FullScreenEvent"/>
        /// when FullScreenModeEvent is triggered.
        /// </summary>
        [TestMethod]
        public void ShouldPublishEventWhenInvokingFullScreenChangedEvent()
        {
            var presenter = this.CreatePresenter();

            Assert.IsFalse(this.fullScreenEvent.PublishCalled);

            this.view.InvokeFullScreenChanged(FullScreenMode.Player);

            Assert.IsTrue(this.fullScreenEvent.PublishCalled);
        }

        /// <summary>
        /// Tests that <see cref="PlayerViewPresenter"/> subscribes to the <see cref="PlayCommentEvent"/>.
        /// </summary>
        [TestMethod]
        public void ShouldSubscribeToPlayCommentEvent()
        {
            this.playCommentEvent.SubscribeArgumentAction = null;
            this.playCommentEvent.SubscribeArgumentFilter = null;
            
            var preseter = this.CreatePresenter();

            Assert.IsNotNull(this.playCommentEvent.SubscribeArgumentAction);
            Assert.IsNotNull(this.playCommentEvent.SubscribeArgumentFilter);
        }

        /// <summary>
        /// Should update smpte frame rate when <see cref="SmpteTimeCodeChangedEvent"/> is published.
        /// </summary>
        [TestMethod]
        public void ShouldUpdateSmpteFrameRateWithSmpteTimecodeChangedEventSubscription()
        {
            var frameRate = SmpteFrameRate.Smpte25;

            var presenter = this.CreatePresenter();

            this.view.SetCurrentSmpteFrameRateCalled = false;

            Assert.IsFalse(this.view.SetCurrentSmpteFrameRateCalled);

            this.smpteTimeCodeChangedEvent.SubscribeArgumentAction(frameRate);

            Assert.IsTrue(this.view.SetCurrentSmpteFrameRateCalled);
            Assert.AreEqual(frameRate, this.view.SetCurrentSmpteFrameRateArgument);
        }

        /// <summary>
        /// Should update AspectRatio rate when <see cref="AspectRatioChangedEvent"/> is published.
        /// </summary>
        [TestMethod]
        public void ShouldUpdateAspectRatioWithAspectRatioChangedEventSubscription()
        {
            var aspectRatio = AspectRatio.Wide;

            var presenter = this.CreatePresenter();

            Assert.IsFalse(this.view.SetAspectRatioCalled);

            this.aspectRatioChangedEvent.SubscribeArgumentAction(aspectRatio);

            Assert.IsTrue(this.view.SetAspectRatioCalled);
            Assert.AreEqual(aspectRatio, this.view.SetCurrentAspectRatio);
        }

        /// <summary>
        /// Tests that <see cref="PlayerViewPresenter"/> calls TogglePlay of <see cref="PlayerView"/>
        /// when PlayClicked event is triggered from the <see cref="PlayerView"/>.
        /// </summary>
        [TestMethod]
        public void ShouldCallToTogglePlayWhenPlayClickedEventIsInvokedAndPlayerModeIsNotTimeline()
        {
            var presenter = this.CreatePresenter();
            presenter.PlayerMode = PlayerMode.MediaLibrary;

            Assert.IsFalse(this.view.TogglePlayCalled);

            this.view.InvokePlayClicked();

            Assert.IsTrue(this.view.TogglePlayCalled);
        }

        /// <summary>
        /// Tests that <see cref="PlayerViewPresenter"/> calls TogglePlay of <see cref="PlayerView"/>
        /// when PauseClicked event is triggered from the <see cref="PlayerView"/>.
        /// </summary>
        [TestMethod]
        public void ShouldCallToTogglePlayWhenPauseClickedEventIsInvokedAndPlayerModeIsNotTimeline()
        {
            var presenter = this.CreatePresenter();
            presenter.PlayerMode = PlayerMode.MediaLibrary;

            Assert.IsFalse(this.view.TogglePlayCalled);

            this.view.InvokePauseClicked();

            Assert.IsTrue(this.view.TogglePlayCalled);
        }
        
        /// <summary>
        /// Call MovetoStart when Move to start command is executed and player mode is not timeline.
        /// </summary>
        [TestMethod]
        public void ShouldCallToMoveToStartWhenMoveToStartCommandIsExecutedAndPlayerModeIsNotTimeline()
        {
            var presenter = this.CreatePresenter();

            presenter.PlayerMode = PlayerMode.MediaBin;

            Assert.IsFalse(this.view.MoveToStartCalled);

            presenter.MoveToStartCommand.Execute(null);

            Assert.IsTrue(this.view.MoveToStartCalled);
        }

        /// <summary>
        /// Call MovetoEnd when Move to end command is executed and player mode is not timeline.
        /// </summary>
        [TestMethod]
        public void ShouldCallToMoveToEndWhenMoveToEndCommandIsExecutedAndPlayerModeIsNotTimeline()
        {
            var presenter = this.CreatePresenter();

            presenter.PlayerMode = PlayerMode.MediaBin;

            Assert.IsFalse(this.view.MoveToEndCalled);

            presenter.MoveToEndCommand.Execute(null);

            Assert.IsTrue(this.view.MoveToEndCalled);
        }

        /// <summary>
        /// Call to StartFrameRewindForward when FrameRewindStarted event is invoked and player mode is not timeline.
        /// </summary>
        [TestMethod]
        public void ShouldCallToStartRewindForwardWhenRewindStartedEventIsInvokedAndPlayerModeIsNotTimeline()
        {
            var presenter = this.CreatePresenter();

            presenter.PlayerMode = PlayerMode.MediaBin;

            Assert.IsFalse(this.view.StartRewindForwardCalled);

            this.view.InvokeRewindStarted();

            Assert.IsTrue(this.view.StartRewindForwardCalled);
        }

        /// <summary>
        /// Call to StartFrameRewindForward when FrameForwardStarted event is invoked and player mode is not timeline.
        /// </summary>
        [TestMethod]
        public void ShouldCallToStartRewindForwardWhenForwardStartedEventIsInvokedAndPlayerModeIsNotTimeline()
        {
            var presenter = this.CreatePresenter();

            presenter.PlayerMode = PlayerMode.MediaBin;

            Assert.IsFalse(this.view.StartRewindForwardCalled);

            this.view.InvokeForwardStarted();

            Assert.IsTrue(this.view.StartRewindForwardCalled);
        }

        /// <summary>
        /// Call to EndFrameRewindForward when FrameRewindEnded event is invoked and player mode is not timeline.
        /// </summary>
        [TestMethod]
        public void ShouldCallToEndRewindForwardWhenRewindEndedEventIsInvokedAndPlayerModeIsNotTimeline()
        {
            var presenter = this.CreatePresenter();

            presenter.PlayerMode = PlayerMode.MediaBin;

            Assert.IsFalse(this.view.EndRewindForwardCalled);

            this.view.InvokeRewindEnded();

            Assert.IsTrue(this.view.EndRewindForwardCalled);
        }

        /// <summary>
        /// Call to end EndFrameRewindForward when FrameForwardEnded event is invoked and player mode is not timeline.
        /// </summary>
        [TestMethod]
        public void ShouldCallToEndRewindForwardWhenForwardEndedEventIsInvokedAndPlayerModeIsNotTimeline()
        {
            var presenter = this.CreatePresenter();

            presenter.PlayerMode = PlayerMode.MediaBin;

            Assert.IsFalse(this.view.EndRewindForwardCalled);

            this.view.InvokeForwardEnded();

            Assert.IsTrue(this.view.EndRewindForwardCalled);
        }

        /// <summary>
        /// Should call to SetCurrentTime when Move to start command is executed and player mode is comment.
        /// </summary>
        [TestMethod]
        public void ShouldCallToSetCurrentTimeWhenMoveToStartCommandIsExecutedAndPlayerModeIsComment()
        {
            var presenter = this.CreatePresenter();
            Comment comment = new Comment(Guid.NewGuid())
            {
                CommentType = CommentType.Timeline,
                MarkIn = 100,
                MarkOut = 200,
            };

            // To set the currentPlayingComment variable.
            this.playCommentEvent.Publish(comment);
            presenter.PlayerMode = PlayerMode.Comment;
            this.view.SetCurrentTimeCalled = false;
            
            presenter.MoveToStartCommand.Execute(null);

            Assert.IsTrue(this.view.SetCurrentTimeCalled);
            Assert.IsTrue(this.view.SetCurrentTimeArgument.TotalSeconds == comment.MarkIn);
        }

        /// <summary>
        /// Should call to SetCurrentTime when Move to end command is executed and player mode is comment.
        /// </summary>
        [TestMethod]
        public void ShouldCallToSetCurrentTimeWhenMoveToEndCommandIsExecutedAndPlayerModeIsComment()
        {
            var presenter = this.CreatePresenter();
            Comment comment = new Comment(Guid.NewGuid())
            {
                CommentType = CommentType.Timeline,
                MarkIn = 100,
                MarkOut = 200,
            };

            // To set the currentPlayingComment variable.
            this.playCommentEvent.Publish(comment);
            presenter.PlayerMode = PlayerMode.Comment;
            this.view.SetCurrentTimeCalled = false;

            presenter.MoveToEndCommand.Execute(null);

            Assert.IsTrue(this.view.SetCurrentTimeCalled);
            Assert.IsTrue(this.view.SetCurrentTimeArgument.TotalSeconds == comment.MarkOut);
        }

        /// <summary>
        /// Should publish Add Comment event when add timeline element command is executed.
        /// </summary>
        [TestMethod]
        public void ShouldPublishPositionDoubleClickedEventWhenAddTimelineElementCommandIsExecutedFromView()
        {
            var presenter = this.CreatePresenter();
            presenter.PlayerMode = PlayerMode.Timeline;
            this.addCommentEvent.PositionPayloadEventArgs = null;
            this.addCommentEvent.PublishCalled = false;

            presenter.AddTimelineElementCommand.Execute(null);

            Assert.IsTrue(this.addCommentEvent.PublishCalled);
        }

        /// <summary>
        /// Should publish Add Comment event when add timeline element command is executed.
        /// </summary>
        [TestMethod]
        public void ShouldNotPublishPositionDoubleClickedEventWhenAddTimelineElementIsExecutedFromViewAndPlayerModeIsMediaBin()
        {
            var presenter = this.CreatePresenter();
            presenter.PlayerMode = PlayerMode.MediaBin;
            this.addCommentEvent.PositionPayloadEventArgs = null;
            this.addCommentEvent.PublishCalled = false;

            presenter.AddTimelineElementCommand.Execute(null);

            Assert.IsFalse(this.addCommentEvent.PublishCalled);
            Assert.AreEqual(null, this.addCommentEvent.PositionPayloadEventArgs);
        }

        /// <summary>
        /// Should publish Add Comment event when add timeline element command is executed and player mode is media library.
        /// </summary>
        [TestMethod]
        public void ShouldNotPublishPositionDoubleClickedEventWhenAddTimelineElementCommandIsExecutedAndPlayerModeIsMediaLibrary()
        {
            var presenter = this.CreatePresenter();
            presenter.PlayerMode = PlayerMode.MediaLibrary;
            this.addCommentEvent.PositionPayloadEventArgs = null;
            this.addCommentEvent.PublishCalled = false;

            presenter.AddTimelineElementCommand.Execute(null);

            Assert.IsFalse(this.addCommentEvent.PublishCalled);
            Assert.AreEqual(null, this.addCommentEvent.PositionPayloadEventArgs);
        }

        /// <summary>
        /// Should publish Add Comment event when add timeline element comamnd is executed and player mode is comment.
        /// </summary>
        [TestMethod]
        public void ShouldNotPublishPositionDoubleClickedEventWhenAddTimelineElementCommandIsExecutedAndPlayerModeIsComment()
        {
            var presenter = this.CreatePresenter();
            presenter.PlayerMode = PlayerMode.Comment;
            this.addCommentEvent.PositionPayloadEventArgs = null;
            this.addCommentEvent.PublishCalled = false;

            presenter.AddTimelineElementCommand.Execute(null);

            Assert.IsFalse(this.addCommentEvent.PublishCalled);
            Assert.AreEqual(null, this.addCommentEvent.PositionPayloadEventArgs);
        }

        /// <summary>
        /// Should set IsMuted to true if it is false when MuteCommand is executed.
        /// </summary>
        [TestMethod]
        public void ShouldSetIsMutedToTrueIfItIsFalseWhenMuteCommandIsExecuted()
        {
            this.view.IsMuted = false;
            var presenter = this.CreatePresenter();

            presenter.MuteCommand.Execute(null);

            Assert.IsTrue(this.view.IsMuted);
        }

        /// <summary>
        /// Shoulds set IsMuted to false if it is true when MuteCommand is executed.
        /// </summary>
        [TestMethod]
        public void ShouldSetIsMutedToFalseIfItIsTrueWhenMuteCommandIsExecuted()
        {
            this.view.IsMuted = true;
            var presenter = this.CreatePresenter();

            presenter.MuteCommand.Execute(null);

            Assert.IsFalse(this.view.IsMuted);
        }

        /// <summary>
        /// Should HidePreviewImage method of view if player event is triggered form timeline model.
        /// </summary>
        [TestMethod]
        public void ShouldCallHidePreviewImageMethodOfViewIfPlayerEventIsTriggeredFormTimelineModel()
        {
            var asset = new VideoAsset { Source = new Uri("http://test") };

            var presenter = this.CreatePresenter();
            this.view.HidePreviewImageCalled = false;

            Assert.IsFalse(this.view.SetSourceCalled);
            Assert.IsNull(this.view.SetSourceArgument);

            this.playerEvent.SubscribeArgumentAction(new PlayerEventPayload { Asset = asset, PlayerMode = PlayerMode.MediaBin });

            Assert.IsTrue(this.view.SetSourceCalled);
            Assert.AreEqual(asset.Source, this.view.SetSourceArgument);
            Assert.IsTrue(this.view.HidePreviewImageCalled);
        }

        /// <summary>
        /// Should HidePreviewImage method of view if Player mode is set to Timeline.
        /// </summary>
        [TestMethod]
        public void ShouldCallHidePreviewImageMethodOfViewWhenPlayerModeIsSetToTimeline()
        {
            var presenter = this.CreatePresenter();
            
            // PlayerMode must change its value to raise the HidePreview call
            // Since its initialize with PlayerMode.Timeline we need to change it to Comment
            presenter.PlayerMode = PlayerMode.Comment;
            this.view.HidePreviewImageCalled = false;

            presenter.PlayerMode = PlayerMode.Timeline;
            Assert.IsTrue(this.view.HidePreviewImageCalled);
        }

        /// <summary>
        /// Should HidePreviewImage method of view when player mode is set to comment.
        /// </summary>
        [TestMethod]
        public void ShouldCallHidePreviewImageMethodOfViewWhenPlayerModeIsSetToComment()
        {
            var presenter = this.CreatePresenter();
            presenter.PlayerMode = PlayerMode.Comment;
            Assert.IsTrue(this.view.HidePreviewImageCalled);
        }

        /// <summary>
        /// Tests if <see cref="keyMappingEvent"/> is published, playermode is MediaBin
        ///  then it should not call pause of <see cref="PlayerView"/>.
        /// </summary>
        [TestMethod]
        public void ShouldCallPlayerViewIfPlayerModeIsMediaBinAndkeyMappingEventIsPublished()
        {
            var presenter = this.CreatePresenter();
            presenter.PlayerMode = PlayerMode.MediaBin;
            this.view.PausePlayerCalled = false;

            this.keyMappingEvent.SubscribeArgumentAction(KeyMappingAction.PausePlayer);

            Assert.IsTrue(this.view.PausePlayerCalled);
        }

        /// <summary>
        /// Tests if <see cref="keyMappingEvent"/> is published, playermode is MediaLibrary
        ///  then it should not call pause of <see cref="PlayerView"/>.
        /// </summary>
        [TestMethod]
        public void ShouldCallPlayerViewIfPlayerModeIsMediaLibraryAndkeyMappingEventIsPublished()
        {
            var presenter = this.CreatePresenter();
            presenter.PlayerMode = PlayerMode.MediaLibrary;
            this.view.PausePlayerCalled = false;

            this.keyMappingEvent.SubscribeArgumentAction(KeyMappingAction.PausePlayer);

            Assert.IsTrue(this.view.PausePlayerCalled);
        }

        /// <summary>
        /// Tests if the KeyMappingEvent Filter is being passed with KeyMappingAction PausePlayer.
        /// </summary>
        [TestMethod]
        public void ShouldPassKeyMappingEventFilterWhenKeyMappingActionIsPausePlayer()
        {
            var presenter = this.CreatePresenter();

            var result = this.keyMappingEvent.SubscribeArgumentFilter(KeyMappingAction.PausePlayer);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests if the KeyMappingEvent Filter is being passed with KeyMappingAction PlayTimeline.
        /// </summary>
        [TestMethod]
        public void ShouldPassKeyMappingEventFilterWhenKeyMappingActionIsPlayTimeline()
        {
            var presenter = this.CreatePresenter();

            var result = this.keyMappingEvent.SubscribeArgumentFilter(KeyMappingAction.PlayTimeline);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests if the KeyMappingEvent Filter is being passed with KeyMappingAction Toggle.
        /// </summary>
        [TestMethod]
        public void ShouldPassKeyMappingEventFilterWhenKeyMappingActionIsToggle()
        {
            var presenter = this.CreatePresenter();
            
            var result = this.keyMappingEvent.SubscribeArgumentFilter(KeyMappingAction.Toggle);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests if the scriptable member TooglePlayTimeline changes the player mode to timeline.
        /// </summary>
        [TestMethod]
        public void ShouldChangePlayerModeToTimelineWhenScriptableMemberTooglePlayTimelineIsExecuted()
        {
            var presenter = new PlayerViewPresenter(this.view, this.eventAggregator, this.sequenceRegistry, this.playbackManifestGenerator, this.manifestMediaModel, this.overlaysManager, this.overlaysDisplayController, this.configurationSettings);
            
            presenter.PlayerMode = PlayerMode.MediaBin;

            presenter.TogglePlayTimeline();

            Assert.AreEqual(PlayerMode.Timeline, presenter.PlayerMode);
        }

        /// <summary>
        /// Tests if the scriptable member StopTimeline changes the player mode to timeline.
        /// </summary>
        [TestMethod]
        public void ShouldChangePlayerModeToTimelineWhenScriptableMemberStopTimelineIsExecuted()
        {
            var presenter = new PlayerViewPresenter(this.view, this.eventAggregator, this.sequenceRegistry, this.playbackManifestGenerator, this.manifestMediaModel, this.overlaysManager, this.overlaysDisplayController, this.configurationSettings);

            presenter.PlayerMode = PlayerMode.MediaBin;

            presenter.StopTimeline();

            Assert.AreEqual(PlayerMode.Timeline, presenter.PlayerMode);
        }

        /// <summary>
        /// Tests that PickThumbnail is being called when PickThumbnailEvent event subscription action is being invoked.
        /// </summary>
        [TestMethod]
        public void ShouldCallToPickThumbnailOnViewWithPickThumbnailEventIsSubscription()
        {
            var presenter = this.CreatePresenter();

            Assert.IsFalse(this.view.PickThumbnailCalled);

            this.pickThumbnailEvent.SubscribeArgumentAction(null);

            Assert.IsTrue(this.view.PickThumbnailCalled);
        }

        /// <summary>
        /// Tests that the ThumbnailEvent event is being published when the SetThumbnail method is called.
        /// </summary>
        [TestMethod]
        public void ShouldPublishThumbnailEventWhenCallingToSetThumbnail()
        {
            var presenter = this.CreatePresenter();

            Assert.IsFalse(this.thumbnailEvent.PublishCalled);

            var bitmap = new WriteableBitmap(10, 15);

            presenter.SetThumbnail(bitmap, ThumbnailType.SequenceThumbnail);

            Assert.IsTrue(this.thumbnailEvent.PublishCalled);
        }

        /// <summary>
        /// Tests that PickThumbnail is being called when PickThumbnailClicked event is invoked.
        /// </summary>
        [TestMethod]
        public void ShouldCallToPickThumbnailOnViewWhenPickThumbnailClickedEventIsInvoked()
        {
            var presenter = this.CreatePresenter();

            Assert.IsFalse(this.view.PickThumbnailCalled);

            this.view.InvokePickThumbnailClicked();

            Assert.IsTrue(this.view.PickThumbnailCalled);
        }

        /// <summary>
        /// Tests that PickThumbnail is being called when PickThumbnailClicked event is invoked.
        /// </summary>
        [TestMethod]
        public void ShouldCallToToggleSlowMotionOnViewWhenSlowMotionClickedEventIsInvoked()
        {
            var presenter = this.CreatePresenter();

            Assert.IsFalse(this.view.ToggleSlowMotionCalled);

            this.view.InvokeSlowMotionClicked();

            Assert.IsTrue(this.view.ToggleSlowMotionCalled);
        }

        /// <summary>
        /// Creates the presenter.
        /// </summary>
        /// <returns>The <see cref="PlayerViewPresenter"/>.</returns>
        private IPlayerViewPresenter CreatePresenter()
        {
            return new PlayerViewPresenter(
                this.view,
                this.eventAggregator,
                this.sequenceRegistry,
                this.playbackManifestGenerator,
                this.manifestMediaModel,
                this.overlaysManager,
                this.overlaysDisplayController,
                this.configurationSettings);
        }
    }
}