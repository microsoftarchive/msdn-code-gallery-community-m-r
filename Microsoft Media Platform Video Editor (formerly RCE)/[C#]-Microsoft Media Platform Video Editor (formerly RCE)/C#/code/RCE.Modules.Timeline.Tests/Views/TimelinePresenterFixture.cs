// <copyright file="TimelinePresenterFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: TimelinePresenterFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Timeline.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Infrastructure.DragDrop;
    using Microsoft.Practices.ServiceLocation;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RCE.Infrastructure;
    using RCE.Infrastructure.Events;
    using RCE.Infrastructure.Models;
    using RCE.Modules.Timeline.Commands;
    using RCE.Modules.Timeline.Tests.Mocks;
    using SMPTETimecode;
    using Project = RCE.Infrastructure.Models.Project;
    using Sequence = RCE.Infrastructure.Models.Sequence;
    using Track = RCE.Infrastructure.Models.Track;
    using TrackType = RCE.Infrastructure.Models.TrackType;

    /// <summary>
    /// A class for testing the <see cref="TimelinePresenter"/>.
    /// </summary>
    [TestClass]
    public class TimelinePresenterFixture
    {
        /// <summary>
        /// The mocked TimelineView.
        /// </summary>
        private MockTimelineView view;

        /// <summary>
        /// The mocked TimelineModel.
        /// </summary>
        private MockSequenceModel sequenceModel;

        /// <summary>
        /// The mocked EventAggregator.
        /// </summary>
        private MockEventAggregator eventAggregator;

        /// <summary>
        /// The mocked StartTimeCodeChangedEvent event.
        /// </summary>
        private MockStartTimeCodeChangedEvent startTimeCodeChangedEvent;

        /// <summary>
        /// The mocked AddAssetEvent event.
        /// </summary>
        private MockAddAssetEvent addAssetEvent;

        /// <summary>
        /// The mocked DeleteMediaBinAssetEvent event.
        /// </summary>
        private MockDeleteMediaBinAssetEvent deleteMediaBinAssetEvent;

        /// <summary>
        /// The mocked AddAssetToTimelineEvent event.
        /// </summary>
        private MockAddAssetToTimelineEvent addAssetToTimelineEvent;

        /// <summary>
        /// The mocked PauseEvent event.
        /// </summary>
        private MockPauseEvent pauseEvent;

        /// <summary>
        /// The mocked PositionUpdateEvent event.
        /// </summary>
        private MockPositionUpdatedEvent positionUpdatedEvent;

        /// <summary>
        /// The mocked PlayheadMovedEvent event.
        /// </summary>
        private MockPlayheadMovedEvent playheadMovedEvent;

        /// <summary>
        /// The mocked EditModeChangedEvent event.
        /// </summary>
        private MockEditModeChangedEvent editModeChangedEvent;

        /// <summary>
        /// The mocked ElementMovedEvent event.
        /// </summary>
        private MockElementMovedEvent elementMovedEvent;

        /// <summary>
        /// The mocked ProjectService service.
        /// </summary>
        private MockProjectService projectService;

        /// <summary>
        /// The mocked SmpteTimeCodeChangedEvent event.
        /// </summary>
        private MockSmpteTimecodeChangedEvent smpteTimeCodeChangedEvent;

        /// <summary>
        /// The mocked PositionDoubleClickedEvent event.
        /// </summary>
        private MockPositionDoubleClickedEvent positionDoubleClickedEvent;

        /// <summary>
        /// The mocked RefreshElementsEvent event.
        /// </summary>
        private MockRefreshElementsEvent refreshElementsEvent;

        /// <summary>
        /// The mocked ThumbnailEvent event.
        /// </summary>
        private MockPickThumbnailEvent pickThumbnailEvent;

        /// <summary>
        /// The mocked PlayerEvent event.
        /// </summary>
        private MockPlayerEvent playerEvent;

        /// <summary>
        /// The mocked Caretaker.
        /// </summary>
        private MockCaretaker caretaker;

        /// <summary>
        /// The mocked ConfigurationService service.
        /// </summary>
        private MockConfigurationService configurationService;

        private MockLockGroupManager lockGroupManagerService;

        private MockSequenceRegistry sequenceRegistry;

        private MockResetWindowsEvent resetWindowEvent;

        private MockOperationExecutedInTimelineEvent operationExecutedInTimelineEvent;

        private MockHideMetadataEvent hideMetadataEvent;

        private MockShowMetadataEvent showMetadataEvent;

        /// <summary>
        /// Initializes resources need it by the tests.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.view = new MockTimelineView();
            this.sequenceModel = new MockSequenceModel();
            this.eventAggregator = new MockEventAggregator();
            this.projectService = new MockProjectService();
            this.caretaker = new MockCaretaker();
            this.configurationService = new MockConfigurationService();

            this.addAssetEvent = new MockAddAssetEvent();
            this.deleteMediaBinAssetEvent = new MockDeleteMediaBinAssetEvent();
            this.pauseEvent = new MockPauseEvent();
            this.positionUpdatedEvent = new MockPositionUpdatedEvent();
            this.playheadMovedEvent = new MockPlayheadMovedEvent();
            this.editModeChangedEvent = new MockEditModeChangedEvent();
            this.elementMovedEvent = new MockElementMovedEvent();
            this.addAssetToTimelineEvent = new MockAddAssetToTimelineEvent();
            this.smpteTimeCodeChangedEvent = new MockSmpteTimecodeChangedEvent();
            this.startTimeCodeChangedEvent = new MockStartTimeCodeChangedEvent();
            this.positionDoubleClickedEvent = new MockPositionDoubleClickedEvent();
            this.refreshElementsEvent = new MockRefreshElementsEvent();
            this.playerEvent = new MockPlayerEvent();
            this.pickThumbnailEvent = new MockPickThumbnailEvent();
            this.lockGroupManagerService = new MockLockGroupManager();
            this.resetWindowEvent = new MockResetWindowsEvent();
            this.operationExecutedInTimelineEvent = new MockOperationExecutedInTimelineEvent();
            this.showMetadataEvent = new MockShowMetadataEvent();
            this.hideMetadataEvent = new MockHideMetadataEvent();

            this.projectService.GetCurrentProjectReturnValue = new Project();
            this.projectService.GetCurrentProjectReturnValue.AddTimeline(new Sequence());

            this.sequenceRegistry = new MockSequenceRegistry();
            this.sequenceRegistry.CurrentSequenceModel = this.sequenceModel;
            this.sequenceRegistry.CurrentSequence = new Sequence();

            this.eventAggregator.AddMapping<AddAssetEvent>(this.addAssetEvent);
            this.eventAggregator.AddMapping<DeleteMediaBinAssetEvent>(this.deleteMediaBinAssetEvent);
            this.eventAggregator.AddMapping<PauseEvent>(this.pauseEvent);
            this.eventAggregator.AddMapping<PositionUpdatedEvent>(this.positionUpdatedEvent);
            this.eventAggregator.AddMapping<PlayheadMovedEvent>(this.playheadMovedEvent);
            this.eventAggregator.AddMapping<EditModeChangedEvent>(this.editModeChangedEvent);
            this.eventAggregator.AddMapping<ElementMovedEvent>(this.elementMovedEvent);
            this.eventAggregator.AddMapping<SmpteTimeCodeChangedEvent>(this.smpteTimeCodeChangedEvent);
            this.eventAggregator.AddMapping<AddAssetToTimelineEvent>(this.addAssetToTimelineEvent);
            this.eventAggregator.AddMapping<StartTimeCodeChangedEvent>(this.startTimeCodeChangedEvent);
            this.eventAggregator.AddMapping<PositionDoubleClickedEvent>(this.positionDoubleClickedEvent);
            this.eventAggregator.AddMapping<RefreshElementsEvent>(this.refreshElementsEvent);
            this.eventAggregator.AddMapping<PlayerEvent>(this.playerEvent);
            this.eventAggregator.AddMapping<PickThumbnailEvent>(this.pickThumbnailEvent);
            this.eventAggregator.AddMapping<ResetWindowsEvent>(this.resetWindowEvent);
            this.eventAggregator.AddMapping<OperationExecutedInTimelineEvent>(this.operationExecutedInTimelineEvent);
            this.eventAggregator.AddMapping<ShowMetadataEvent>(this.showMetadataEvent);
            this.eventAggregator.AddMapping<HideMetadataEvent>(this.hideMetadataEvent);
        }

        /// <summary>
        /// Tests that the constructor sets the default duration.
        /// </summary>
        [TestMethod]
        public void ConstructorSetsDefaultDuration()
        {
            Assert.IsFalse(this.view.SetDurationCalled);

            var presenter = this.CreatePresenter();
            
            Assert.IsTrue(this.view.SetDurationCalled);
            Assert.AreEqual(TimelinePresenter.DefaultTimelineDuration, this.view.SetDurationArgument.TotalSeconds);
            Assert.AreEqual(TimelinePresenter.DefaultTimelineDuration, this.sequenceModel.Duration.TotalSeconds);
        }

        /// <summary>
        /// Should set the presenter into view.
        /// </summary>
        [TestMethod]
        public void ShouldSetPresenterlIntoView()
        {
            var presenter = this.CreatePresenter();
            Assert.AreSame(presenter, this.view.Model);
        }

        /// <summary>
        /// Tests that the RemoveElementCommand to all the elements of a video asset when the DeleteMediaBinAsset event is being published.
        /// </summary>
        [TestMethod]
        public void ShouldCallToRemoveElementCommandToAllTheElementsOfTheGivenAssetByLookingForSameProviderUriWithDeleteMediaBinAssetEvent()
        {
            var asset = new VideoAsset { ProviderUri = new Uri("http://test") };
            var otherAsset = new VideoAsset { ProviderUri = new Uri("http://other") };

            var timelineElement0 = new TimelineElement { Asset = asset };
            var timelineElement1 = new TimelineElement { Asset = asset };
            var timelineElement2 = new TimelineElement { Asset = otherAsset };

            var track = new Track { TrackType = TrackType.Visual };

            track.Shots.Add(timelineElement0);
            track.Shots.Add(timelineElement1);
            track.Shots.Add(timelineElement2);

            this.sequenceModel.Tracks.Add(track);
            
            var presenter = this.CreatePresenter();

            Assert.IsFalse(this.caretaker.ExecuteCommandCalled);

            this.deleteMediaBinAssetEvent.SubscribeArgumentAction(asset);

            Assert.AreEqual(2, this.caretaker.ExecuteCommandNumberOfCalls);
            Assert.IsTrue(this.caretaker.ExecuteCommandCalled);
            Assert.IsInstanceOfType(this.caretaker.ExecuteCommandArgument, typeof(RemoveElementCommand));
        }

        /// <summary>
        /// Tests that the RemoveElementCommand to all the elements of an audio asset when the DeleteMediaBinAsset event is being published.
        /// </summary>
        [TestMethod]
        public void ShouldCallToRemoveElementCommandToAllTheElementsOfTheGivenAssetByLookingForSameIdWithDeleteMediaBinAssetEvent()
        {
            var asset = new AudioAsset();
            var otherAsset = new AudioAsset();

            var timelineElement0 = new TimelineElement { Asset = asset };
            var timelineElement1 = new TimelineElement { Asset = asset };
            var timelineElement2 = new TimelineElement { Asset = otherAsset };

            var track = new Track { TrackType = TrackType.Audio };

            track.Shots.Add(timelineElement0);
            track.Shots.Add(timelineElement1);
            track.Shots.Add(timelineElement2);

            this.sequenceModel.Tracks.Add(track);

            var presenter = this.CreatePresenter();

            Assert.IsFalse(this.caretaker.ExecuteCommandCalled);

            this.deleteMediaBinAssetEvent.SubscribeArgumentAction(asset);

            Assert.AreEqual(2, this.caretaker.ExecuteCommandNumberOfCalls);
            Assert.IsTrue(this.caretaker.ExecuteCommandCalled);
            Assert.IsInstanceOfType(this.caretaker.ExecuteCommandArgument, typeof(RemoveElementCommand));
        }

        /// <summary>
        /// Tests that the AddElement method should be called when invoking the ElementAdded event.
        /// </summary>
        [TestMethod]
        public void ShouldCallToAddElementOnViewWhenInvokingElementAddedEvent()
        {
            var presenter = this.CreatePresenter();
            this.sequenceRegistry.InvokeCurrentSequenceChanged();

            Assert.IsFalse(this.view.AddElementCalled);

            this.sequenceModel.InvokeElementAdded(new TimelineElement());

            Assert.IsTrue(this.view.AddElementCalled);
        }

        /// <summary>
        /// Tests that the UnselectElement method should be called when invoking the ElementAdded event.
        /// </summary>
        [TestMethod]
        public void ShouldCallToUnselectElementOnViewWhenInvokingElementAddedEvent()
        {
            var selectedTimelineElement = new TimelineElement();
            var newTimelineElement = new TimelineElement { Asset = new VideoAsset() };

            var presenter = this.CreatePresenter();

            this.sequenceRegistry.InvokeCurrentSequenceChanged();

            this.view.InvokeElementSelect(selectedTimelineElement, TimeCode.FromSeconds(0d, SmpteFrameRate.Smpte2997NonDrop));

            Assert.IsFalse(this.view.UnselectElementCalled);

            this.sequenceModel.InvokeElementAdded(newTimelineElement);

            Assert.IsTrue(this.view.UnselectElementCalled);
            Assert.AreEqual(selectedTimelineElement, this.view.UnselectElementArgument);
        }

        /// <summary>
        /// Tests that the UndoLevel should be retrieved from the ConfigurationService.
        /// </summary>
        [TestMethod]
        public void ShouldCallToGetUndoLevelOnConfigurationService()
        {
            bool getUndoLevelCalled = false;
            this.configurationService.GetParameterValueReturnFunction = parameter =>
            {
                if (parameter == "UndoLevel")
                {
                    getUndoLevelCalled = true;
                }

                return string.Empty;
            };

            var presenter = this.CreatePresenter();

            Assert.IsTrue(getUndoLevelCalled);
        }

        /// <summary>
        /// Tests that the UndoLevel should be set on the Caretaker.
        /// </summary>
        [TestMethod]
        public void ShouldCallToSetUndoLevelOnCaretaker()
        {
            Assert.IsFalse(this.caretaker.SetUndoLevelCalled);

            var presenter = this.CreatePresenter();

            Assert.IsTrue(this.caretaker.SetUndoLevelCalled);
        }

        /// <summary>
        /// Tests that the RemoveElement should be called when invoking the ElementRemoved event.
        /// </summary>
        [TestMethod]
        public void ShouldCallToRemoveElementOnViewWhenInvokingElementRemovedEvent()
        {
            var presenter = this.CreatePresenter();

            this.sequenceRegistry.InvokeCurrentSequenceChanged();

            Assert.IsFalse(this.view.RemoveElementCalled);

            this.sequenceModel.InvokeElementRemoved(new TimelineElement());

            Assert.IsTrue(this.view.RemoveElementCalled);
        }

        /// <summary>
        /// Test that the RemoveElement should be called when invoking the ElementRemoved event even if
        /// the removed element is the selected.
        /// </summary>
        [TestMethod]
        public void ShouldCallToRemoveElementOnViewWhenInvokingElementRemovedEventEvenIfTheElementIsTheSelectedOne()
        {
            var timelineElement = new TimelineElement();
            var presenter = this.CreatePresenter();

            this.sequenceRegistry.InvokeCurrentSequenceChanged();

            this.view.InvokeElementSelect(timelineElement, TimeCode.FromSeconds(0d, SmpteFrameRate.Smpte2997NonDrop));

            Assert.IsFalse(this.view.RemoveElementCalled);

            this.sequenceModel.InvokeElementRemoved(timelineElement);

            Assert.IsTrue(this.view.RemoveElementCalled);
        }

        /// <summary>
        /// Tests that the ShowLink method should be called when invoking the ElementLinked event.
        /// </summary>
        [TestMethod]
        public void ShouldCallToShowLinkOnViewWhenInvokingElementLinkedEvent()
        {
            var track = new Track { TrackType = TrackType.Visual };
            this.sequenceModel.Tracks.Add(track);

            var previousElement = new TimelineElement
            {
                Asset = new VideoAsset(),
                Position = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                InPosition = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                OutPosition = TimeCode.FromAbsoluteTime(20, this.sequenceModel.Duration.FrameRate)
            };

            var currentElement = new TimelineElement
            {
                Asset = new VideoAsset(),
                Position = TimeCode.FromAbsoluteTime(20, this.sequenceModel.Duration.FrameRate),
                InPosition = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                OutPosition = TimeCode.FromAbsoluteTime(20, this.sequenceModel.Duration.FrameRate)
            };

            track.Shots.Add(previousElement);
            track.Shots.Add(currentElement);

            this.sequenceModel.GetElementAtPositionReturnFunction = () =>
            {
                if (this.sequenceModel.GetElementAtPositionPositionArgument == currentElement.Position)
                {
                    return previousElement;
                }
                else
                {
                    return null;
                }
            };

            var presenter = this.CreatePresenter();

            this.sequenceRegistry.InvokeCurrentSequenceChanged();

            Assert.IsFalse(this.view.ShowLinkCalled);

            this.sequenceModel.InvokeElementLinked(currentElement);

            Assert.IsTrue(this.view.ShowLinkCalled);
        }

        /// <summary>
        /// Tests that the HideLink method should be called when invoking the ElementUnliked event.
        /// </summary>
        [TestMethod]
        public void ShouldCallToHideLinkOnViewWhenInvokingElementUnlinkedEvent()
        {
            var presenter = this.CreatePresenter();

            this.sequenceRegistry.InvokeCurrentSequenceChanged();

            Assert.IsFalse(this.view.HideLinkCalled);

            this.sequenceModel.InvokeElementUnlinked(new TimelineElement());

            Assert.IsTrue(this.view.HideLinkCalled);
        }

        /// <summary>
        /// Tests that the ShowLink method should not be called when invoking the ElementLinked event
        /// if the element is null.
        /// </summary>
        [TestMethod]
        public void ShouldNotCallToShowLinkOnViewWhenInvokingElementLinkedEventWithNullElement()
        {
            var presenter = this.CreatePresenter();

            this.sequenceRegistry.InvokeCurrentSequenceChanged();

            Assert.IsFalse(this.view.ShowLinkCalled);

            this.sequenceModel.InvokeElementLinked(null);

            Assert.IsFalse(this.view.ShowLinkCalled);
        }

        /// <summary>
        /// Tests that the HideLink method should not be called when invoking the ElementUnlinked event
        /// if the element is null.
        /// </summary>
        [TestMethod]
        public void ShouldNotCallToHideLinkOnViewWhenInvokingElementUnlinkedEventWithNullElement()
        {
            var presenter = this.CreatePresenter();

            this.sequenceRegistry.InvokeCurrentSequenceChanged();

            Assert.IsFalse(this.view.HideLinkCalled);

            this.sequenceModel.InvokeElementUnlinked(null);

            Assert.IsFalse(this.view.HideLinkCalled);
        }

        /// <summary>
        /// Tests that the Drop Command should not be executed if the MouseEventArgs is null.
        /// </summary>
        [TestMethod]
        public void ShouldNotCanExecuteDropCommandIfMouseEventArgsIsNull()
        {
            var presenter = this.CreatePresenter();

            var payload = new DropPayload
            {
                DraggedItem = new VideoAsset(),
                MouseEventArgs = null
            };

            var result = presenter.DropCommand.CanExecute(payload);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Tests that the AddElementCommand should not be executed if the asset dropped has not layer resolved.
        /// </summary>
        [TestMethod]
        public void ShouldNotExecuteAddElementCommandOnCaretakerWhenDropAssetIfNoLayerIsResolved()
        {
            var presenter = this.CreatePresenter();

            this.view.MockedResolvedLayerPosition = null;

            var payload = new DropPayload
            {
                DraggedItem = new VideoAsset
                {
                    Duration = TimeCode.FromAbsoluteTime(30, SmpteFrameRate.Smpte30),
                    FrameRate = SmpteFrameRate.Smpte30,
                    Title = "Test Video #1"
                },
                MouseEventArgs = null
            };

            Assert.IsFalse(this.caretaker.ExecuteCommandCalled);

            presenter.DropCommand.Execute(payload);

            Assert.IsFalse(this.caretaker.ExecuteCommandCalled);
        }

        /// <summary>
        /// Tests that the AddElementCommand should be executed when a video asset is dropped.
        /// </summary>
        [TestMethod]
        public void ShouldExecuteAddElementCommandOnCaretakerWhenDropVideoAsset()
        {
            var presenter = this.CreatePresenter();

            this.view.MockedResolvedLayerPosition = new LayerPosition
            {
                LayerType = TrackType.Visual,
                Position = TimeCode.FromAbsoluteTime(10, SmpteFrameRate.Smpte30)
            };

            var payload = new DropPayload
            {
                DraggedItem = new VideoAsset
                {
                    Duration = TimeCode.FromAbsoluteTime(30, SmpteFrameRate.Smpte30),
                    FrameRate = SmpteFrameRate.Smpte30,
                    Title = "Test Video #1"
                },
                MouseEventArgs = null
            };

            Assert.IsFalse(this.caretaker.ExecuteCommandCalled);

            presenter.DropCommand.Execute(payload);

            Assert.IsTrue(this.caretaker.ExecuteCommandCalled);
            Assert.IsInstanceOfType(this.caretaker.ExecuteCommandArgument, typeof(AddElementCommand));
        }

        /// <summary>
        /// Tests that the AddElementCommand should be executed when an image asset is dropped.
        /// </summary>
        [TestMethod]
        public void ShouldExecuteAddElementCommandOnCaretakerWhenDropImageAsset()
        {
            var presenter = this.CreatePresenter();

            this.view.MockedResolvedLayerPosition = new LayerPosition
            {
                LayerType = TrackType.Visual,
                Position = TimeCode.FromAbsoluteTime(3, SmpteFrameRate.Smpte30)
            };

            var payload = new DropPayload
            {
                DraggedItem = new ImageAsset
                {
                    Title = "Test Image #1"
                },
                MouseEventArgs = null
            };

            Assert.IsFalse(this.caretaker.ExecuteCommandCalled);

            presenter.DropCommand.Execute(payload);

            Assert.IsTrue(this.caretaker.ExecuteCommandCalled);
            Assert.IsInstanceOfType(this.caretaker.ExecuteCommandArgument, typeof(AddElementCommand));
        }

        /// <summary>
        /// Tests that the AddElementCommand should be executed when an audio asset is dropped.
        /// </summary>
        [TestMethod]
        public void ShouldExecuteAddElementCommandOnCaretakerWhenDropAudioAsset()
        {
            var presenter = this.CreatePresenter();

            this.view.MockedResolvedLayerPosition = new LayerPosition
            {
                LayerType = TrackType.Audio,
                Position = TimeCode.FromAbsoluteTime(5, SmpteFrameRate.Smpte30)
            };

            var payload = new DropPayload
            {
                DraggedItem = new AudioAsset
                {
                    DurationInSeconds = 2,
                    Title = "Test Audio #1"
                },
                MouseEventArgs = null
            };

            Assert.IsFalse(this.caretaker.ExecuteCommandCalled);

            presenter.DropCommand.Execute(payload);

            Assert.IsTrue(this.caretaker.ExecuteCommandCalled);
            Assert.IsInstanceOfType(this.caretaker.ExecuteCommandArgument, typeof(AddElementCommand));
        }

        /// <summary>
        /// Tests that the RemoveElementCommand should be executed when executing KeyboardActionCommand
        /// </summary>
        [TestMethod]
        public void ShouldExecuteRemoveElementCommandWhenExecutingKeyboardActionCommand()
        {
            var presenter = this.CreatePresenter();

            var element = new TimelineElement
            {
                Asset = new VideoAsset
                {
                    Duration = TimeCode.FromAbsoluteTime(1000, SmpteFrameRate.Smpte30),
                    FrameRate = SmpteFrameRate.Smpte30,
                    Title = "Test Video #3"
                },
                InPosition = TimeCode.FromAbsoluteTime(250, SmpteFrameRate.Smpte30),
                OutPosition = TimeCode.FromAbsoluteTime(750, SmpteFrameRate.Smpte30),
                Position = TimeCode.FromAbsoluteTime(5000, SmpteFrameRate.Smpte30)
            };

            this.sequenceRegistry.InvokeCurrentSequenceChanged();

            this.view.InvokeElementSelect(element, TimeCode.FromAbsoluteTime(0, SmpteFrameRate.Smpte30));

            Assert.IsFalse(this.caretaker.ExecuteCommandCalled);

            presenter.KeyboardActionCommand.Execute(new Tuple<KeyboardAction, object>(KeyboardAction.Delete, null));

            Assert.IsTrue(this.caretaker.ExecuteCommandCalled);
            Assert.IsInstanceOfType(this.caretaker.ExecuteCommandArgument, typeof(RemoveElementCommand));
        }

        /// <summary>
        /// Tests that the RemoveElementCommand should not be called when Keyboard Action Command is executed
        /// if there is no element selected.
        /// </summary>
        [TestMethod]
        public void ShouldNotExecuteRemoveElementCommandWhenKeyboardActionCommandIsExecutedIfNoElementIsSelected()
        {
            var presenter = this.CreatePresenter();

            Assert.IsFalse(this.caretaker.ExecuteCommandCalled);

            presenter.KeyboardActionCommand.Execute(new Tuple<KeyboardAction, object>(KeyboardAction.Delete, null));

            Assert.IsFalse(this.caretaker.ExecuteCommandCalled);
        }

        /// <summary>
        /// Should call to MoveElement on Model and to RefreshElement on View when invoking element position change.
        /// </summary>
        [TestMethod]
        public void ShouldCallToMoveElementOnModelAndRefreshElementOnViewWhenChangingPosition()
        {
            var presenter = this.CreatePresenter();

            var element = new TimelineElement
            {
                Asset = new VideoAsset
                {
                    Duration = TimeCode.FromAbsoluteTime(1000, SmpteFrameRate.Smpte30),
                    FrameRate = SmpteFrameRate.Smpte30,
                    Title = "Test Video #3"
                },
                InPosition = TimeCode.FromAbsoluteTime(250, SmpteFrameRate.Smpte30),
                OutPosition = TimeCode.FromAbsoluteTime(750, SmpteFrameRate.Smpte30),
                Position = TimeCode.FromAbsoluteTime(5000, SmpteFrameRate.Smpte30)
            };

            var newPosition = TimeCode.FromAbsoluteTime(2000, this.view.SetDurationArgument.FrameRate);

            this.view.InvokeElementSelect(element, TimeCode.FromAbsoluteTime(0, SmpteFrameRate.Smpte30));

            Assert.IsFalse(this.sequenceModel.MoveElementCalled);
            Assert.IsFalse(this.view.RefreshElementCalled);

            this.view.InvokeElementPositionChange(newPosition);

            Assert.IsTrue(this.sequenceModel.MoveElementCalled);
            Assert.IsTrue(this.view.RefreshElementCalled);
        }

        /// <summary>
        /// Tests that the moving an element to end of timeline should fixes position.
        /// </summary>
        [TestMethod]
        public void ShouldMoveElementToEndOfTimelineFixesPosition()
        {
            var presenter = this.CreatePresenterWithDemoData();
            var element = this.sequenceModel.Tracks[0].Shots[2];
            var newPosition = TimeCode.FromAbsoluteTime(10000, this.view.SetDurationArgument.FrameRate);
            var newFixedPosition = TimeCode.FromAbsoluteTime(9700, this.view.SetDurationArgument.FrameRate);

            this.view.InvokeElementSelect(element, TimeCode.FromAbsoluteTime(0, SmpteFrameRate.Smpte30));
            this.view.InvokeElementPositionChange(newPosition);

            Assert.AreEqual(newFixedPosition, this.sequenceModel.MoveElementNewPositionArgument);
        }

        /// <summary>
        /// Tests that the Image MarkOut should be adjusted.
        /// </summary>
        [TestMethod]
        public void AdjustImageMarkOut()
        {
            var presenter = this.CreatePresenterWithDemoData();
            var element = this.sequenceModel.Tracks[0].Shots[2];

            this.view.InvokeElementSelect(element, TimeCode.FromAbsoluteTime(0, SmpteFrameRate.Smpte30));
            this.view.DoMoveElementMarkOut(TimeCode.FromAbsoluteTime(2000, this.view.SetDurationArgument.FrameRate));

            Assert.AreEqual(500, element.Duration.TotalSeconds);
            Assert.IsFalse(this.sequenceModel.MoveElementCalled);
        }

        /// <summary>
        /// Tests that the Image MarkIn should be adjusted.
        /// </summary>
        [TestMethod]
        public void ShouldAdjustImageMarkIn()
        {
            var presenter = this.CreatePresenterWithDemoData();
            var element = this.sequenceModel.Tracks[0].Shots[2];

            this.view.InvokeElementSelect(element, TimeCode.FromAbsoluteTime(0, SmpteFrameRate.Smpte30));
            this.view.DoMoveElementMarkIn(TimeCode.FromAbsoluteTime(1600, this.view.SetDurationArgument.FrameRate));

            Assert.AreEqual(200, element.Duration.TotalSeconds);
            Assert.IsTrue(this.sequenceModel.MoveElementCalled);
            Assert.AreEqual(TimeCode.FromAbsoluteTime(1600, this.view.SetDurationArgument.FrameRate), this.sequenceModel.MoveElementNewPositionArgument);
        }

        /// <summary>
        /// Tests that the elements should be splitted when the KeyboardCommandAction is executed.
        /// </summary>
        [TestMethod]
        public void ShouldSplitElementWhenKeyboardCommandActionIsExecuted()
        {
            var presenter = this.CreatePresenter();

            this.sequenceModel.CurrentPosition = TimeCode.FromAbsoluteTime(5200, SmpteFrameRate.Smpte30);

            var element = new TimelineElement
            {
                Asset = new VideoAsset
                {
                    Duration = TimeCode.FromAbsoluteTime(1000, SmpteFrameRate.Smpte30),
                    FrameRate = SmpteFrameRate.Smpte30,
                    Title = "Test Video #3"
                },
                InPosition = TimeCode.FromAbsoluteTime(100, SmpteFrameRate.Smpte30),
                OutPosition = TimeCode.FromAbsoluteTime(500, SmpteFrameRate.Smpte30),
                Position = TimeCode.FromAbsoluteTime(5000, SmpteFrameRate.Smpte30)
            };

            this.sequenceModel.Tracks.Add(new Track());
            this.sequenceModel.Tracks[0].Shots.Add(element);

            this.sequenceModel.GetElementsAtPositionReturnValue = new List<TimelineElement> { element };

            var expectedOutPosition = this.sequenceModel.CurrentPosition.TotalSeconds - element.Position.TotalSeconds  + element.InPosition.TotalSeconds;

            this.view.InvokeElementSelect(element, TimeCode.FromAbsoluteTime(0, SmpteFrameRate.Smpte30));

            Assert.IsFalse(this.view.RefreshElementCalled);
            Assert.IsFalse(this.caretaker.ExecuteCommandCalled);

            presenter.KeyboardActionCommand.Execute(new Tuple<KeyboardAction, object>(KeyboardAction.Split, null));

            Assert.IsTrue(this.view.RefreshElementCalled);
            Assert.IsTrue(this.caretaker.ExecuteCommandCalled);

            Assert.AreEqual(expectedOutPosition, this.view.RefreshElementArgument[0].OutPosition.TotalSeconds);
            
            // Assert.AreEqual(this.view.RefreshElementArgument[0].OutPosition, this.timelineModel.AddElementArgument.InPosition);
            // Assert.AreEqual(element.Position + this.view.RefreshElementArgument[0].Duration, this.timelineModel.AddElementArgument.Position);
            Assert.IsInstanceOfType(this.caretaker.ExecuteCommandArgument, typeof(AddElementCommand));
        }

        /// <summary>
        /// Tests that no split operation should be performed if there is no elements under the playhead position.
        /// </summary>
        [TestMethod]
        public void ShouldNotSplitIfNoElementIsUnderThePlayheadPosition()
        {
            var presenter = this.CreatePresenter();

            this.sequenceModel.CurrentPosition = TimeCode.FromAbsoluteTime(4200, SmpteFrameRate.Smpte30);

            var element = new TimelineElement
            {
                Asset = new VideoAsset
                {
                    Duration = TimeCode.FromAbsoluteTime(1000, SmpteFrameRate.Smpte30),
                    FrameRate = SmpteFrameRate.Smpte30,
                    Title = "Test Video #3"
                },
                InPosition = TimeCode.FromAbsoluteTime(100, SmpteFrameRate.Smpte30),
                OutPosition = TimeCode.FromAbsoluteTime(500, SmpteFrameRate.Smpte30),
                Position = TimeCode.FromAbsoluteTime(5000, SmpteFrameRate.Smpte30)
            };

            this.sequenceModel.Tracks.Add(new Track());
            this.sequenceModel.Tracks[0].Shots.Add(element);

            this.sequenceModel.GetElementsAtPositionReturnValue = new List<TimelineElement>();

            Assert.IsFalse(this.view.RefreshElementCalled);
            Assert.IsFalse(this.sequenceModel.AddElementCalled);

            presenter.KeyboardActionCommand.Execute(new Tuple<KeyboardAction, object>(KeyboardAction.Split, null));

            Assert.IsFalse(this.view.RefreshElementCalled);
            Assert.IsFalse(this.sequenceModel.AddElementCalled);
        }

        /// <summary>
        /// Tests that the PauseEvent should be published when invokin to MovingPlayhead event.
        /// </summary>
        [TestMethod]
        public void ShouldPublishPauseEventWhenInvokingToMovingPlayheadEvent()
        {
            var presenter = this.CreatePresenter();

            Assert.IsFalse(this.pauseEvent.PublishCalled);

            this.view.InvokeMovingPlayhead();

            Assert.IsTrue(this.pauseEvent.PublishCalled);
        }

        /// <summary>
        /// Tests that the SmpteFrameRate should be updated when the SmpteTimeCodeChangedEvent is being published.
        /// </summary>
        [TestMethod]
        public void ShouldUpdateSmpteFrameRateWithSmpteTimecodeChangedEventSubscription()
        {
            var frameRate = SmpteFrameRate.Smpte25;

            Assert.IsFalse(this.view.SetDurationCalled);

            var presenter = this.CreatePresenter();

            Assert.IsTrue(this.view.SetDurationCalled);

            this.view.SetDurationCalled = false;

            Assert.IsFalse(this.view.SetDurationCalled);

            this.smpteTimeCodeChangedEvent.SubscribeArgumentAction(frameRate);

            Assert.IsTrue(this.view.SetDurationCalled);
            Assert.AreEqual(frameRate, this.view.SetDurationArgument.FrameRate);
        }

        /// <summary>
        /// Tests that the Undo method of the Caretaker is being called when Keyboard Acton Command is executed.
        /// </summary>
        [TestMethod]
        public void ShouldUndoOnCaretakerWhenKeyboardActionCommandIsExecuted()
        {
            var presenter = this.CreatePresenter();

            Assert.IsFalse(this.caretaker.UndoCalled);

            presenter.KeyboardActionCommand.Execute(new Tuple<KeyboardAction, object>(KeyboardAction.Undo, null));

            Assert.IsTrue(this.caretaker.UndoCalled);
        }

        /// <summary>
        /// Tests that the Redo method of the Caretaker is being callen when Keyboard Action Command is executed
        /// </summary>
        [TestMethod]
        public void ShouldRedoOnCaretakerWhenKeyboardActionCommandIsExecuted()
        {
            var presenter = this.CreatePresenter();

            Assert.IsFalse(this.caretaker.RedoCalled);

            presenter.KeyboardActionCommand.Execute(new Tuple<KeyboardAction, object>(KeyboardAction.Redo, null));

            Assert.IsTrue(this.caretaker.RedoCalled);
        }

        /// <summary>
        /// Tests that the PublishedEditModeChangedEvent event should be published when Keyboard Action Command is executed
        /// </summary>
        [TestMethod]
        public void ShouldPublishEditModeChangedEventEventIfKeyboardActionCommandIsExecuted()
        {
            var presenter = this.CreatePresenter();

            this.editModeChangedEvent.PublishCalled = false;

            presenter.KeyboardActionCommand.Execute(new Tuple<KeyboardAction, object>(KeyboardAction.ToggleEdit, null));

            Assert.IsTrue(this.editModeChangedEvent.PublishCalled);
        }

        /// <summary>
        /// Tests that the PublishedEditModeChangedEvent event should be published when the IsInRippleMode property is changed.
        /// </summary>
        [TestMethod]
        public void ShouldPublishEditModeChangedEventEventIfIsInRippleModeIsChanged()
        {
            var presenter = this.CreatePresenter();

            this.editModeChangedEvent.PublishCalled = false;

            presenter.IsInRippleMode = true;

            Assert.IsTrue(this.editModeChangedEvent.PublishCalled);
        }

        /// <summary>
        /// Tests that SetStartTimeCode method should be called when the StartTimeCodeChangedEvent is being published.
        /// </summary>
        [TestMethod]
        public void ShouldCallToSetStartTimeCodeOnViewWithStartTimeCodeChangedEventSubscription()
        {
            var timeCode = TimeCode.FromHours(1, SmpteFrameRate.Smpte2997NonDrop);

            var presenter = this.CreatePresenter();

            this.view.SetStartTimeCodeCalled = false;

            this.startTimeCodeChangedEvent.SubscribeArgumentAction(timeCode);

            Assert.IsTrue(this.view.SetStartTimeCodeCalled);
            Assert.AreEqual(timeCode, this.view.SetStartTimeCodeArgument);
        }

        /// <summary>
        /// Tests that the AddElementCommand should be executed when the AddAssetToTimelineEvent is being published.
        /// </summary>
        [TestMethod]
        public void ShouldExecuteAddElementCommandOnCaretakerWithAddAssetToTimelineEventSubscription()
        {
            var presenter = this.CreatePresenter();

            this.view.MockedResolvedLayerPosition = new LayerPosition
            {
                LayerType = TrackType.Visual,
                Position = TimeCode.FromAbsoluteTime(3, SmpteFrameRate.Smpte30)
            };

            var asset = new ImageAsset { Title = "Test Image #1" };

            Assert.IsFalse(this.caretaker.ExecuteCommandCalled);

            this.addAssetToTimelineEvent.SubscribeArgumentAction(asset);

            Assert.IsTrue(this.caretaker.ExecuteCommandCalled);
            Assert.IsInstanceOfType(this.caretaker.ExecuteCommandArgument, typeof(AddElementCommand));
        }

        /// <summary>
        /// Tests that the REfreshElement method should be callend when invoking the ElementMovedEvent of the TimelineModel.
        /// </summary>
        [TestMethod]
        public void ShouldCallToRefreshElementOnViewWhenInvokingTimelineModelElementMovedEvent()
        {
            var timelineElement = new TimelineElement();

            var presenter = this.CreatePresenter();

            this.sequenceRegistry.InvokeCurrentSequenceChanged();

            Assert.IsFalse(this.view.RefreshElementCalled);

            this.sequenceModel.InvokeElementMoved(timelineElement);

            Assert.IsTrue(this.view.RefreshElementCalled);
            Assert.AreEqual(timelineElement, this.view.RefreshElementArgument[0]);
        }

        /// <summary>
        /// Tests that the SelectElement method should be called when invoking the SelectElement event.
        /// </summary>
        [TestMethod]
        public void ShouldCallToSelectElementOnViewWhenInvokingSelectElementEvent()
        {
            var timelineElement = new TimelineElement();

            var presenter = this.CreatePresenter();

            Assert.IsFalse(this.view.SelectElementCalled);

            this.view.InvokeElementSelect(timelineElement, TimeCode.FromSeconds(0d, SmpteFrameRate.Smpte2997Drop));

            Assert.IsTrue(this.view.SelectElementCalled);
            Assert.AreEqual(timelineElement, this.view.SelectElementArgument);
        }

        /// <summary>
        /// Tests that the UnselectElement method should be called when invoking the SeelectElement event if there is an element selected.
        /// </summary>
        [TestMethod]
        public void ShouldCallToUnselectElementOnViewWhenInvokingSelectElementEventIfThereIsAnElementSelected()
        {
            var selectedTimelineElement = new TimelineElement();
            var newSelectedTimelineElement = new TimelineElement() { Asset = new VideoAsset() };

            var presenter = this.CreatePresenter();

            this.view.InvokeElementSelect(selectedTimelineElement, TimeCode.FromSeconds(0d, SmpteFrameRate.Smpte2997Drop));

            Assert.IsFalse(this.view.UnselectElementCalled);

            this.view.InvokeElementSelect(newSelectedTimelineElement, TimeCode.FromSeconds(0d, SmpteFrameRate.Smpte2997Drop));

            Assert.IsTrue(this.view.UnselectElementCalled);
            Assert.AreEqual(selectedTimelineElement, this.view.UnselectElementArgument);
        }

        /// <summary>
        /// Tests that the ExecuteLayerSnapshotCommand commadn should be executed when the StopMovingEvent is invoked if the StartMovingEvent was previously invoked.
        /// </summary>
        [TestMethod]
        public void ShouldExecuteLayerSnapshotCommandOnCaretakerWhenStopMovingEventIsInvokedIfStartMovingEventWasInvoked()
        {
            var timelineElement = new TimelineElement { Asset = new VideoAsset() };

            var track = new Track { TrackType = TrackType.Visual };

            track.Shots.Add(timelineElement);

            this.sequenceModel.Tracks.Add(track);

            var presenter = this.CreatePresenter();

            this.view.InvokeStartMoving(timelineElement);

            Assert.IsFalse(this.caretaker.ExecuteCommandCalled);

            this.view.InvokeStopMoving(timelineElement);

            Assert.IsTrue(this.caretaker.ExecuteCommandCalled);
        }

        /// <summary>
        /// Tests that the PositionDoubleClickedEvent event should be published when invoking the TopBarDoubleClicked event.
        /// </summary>
        [TestMethod]
        public void ShouldPublishPositionDoubleClickedEventWhenTopBarDoubleClickedEventIsInvoked()
        {
            var payload = new PositionPayloadEventArgs(TimeSpan.FromSeconds(10));

            var presenter = this.CreatePresenter();

            this.positionDoubleClickedEvent.PublishCalled = false;

            this.view.InvokeTopBarDoubleClicked(payload);

            Assert.IsTrue(this.positionDoubleClickedEvent.PublishCalled);
            Assert.AreEqual(payload, this.positionDoubleClickedEvent.PublishArgumentPayload);
        }

        /// <summary>
        /// Tests that the RefreshElementsEvent event should be published when invoking the RefreshingElements event.
        /// </summary>
        [TestMethod]
        public void ShouldPublishRefreshElementsEventEventWhenRefresingElementsEventIsInvoked()
        {
            var payload = new RefreshElementsEventArgs(10, CommentMode.Timeline);

            var presenter = this.CreatePresenter();

            this.refreshElementsEvent.PublishCalled = false;

            this.view.InvokeRefreshingElements(payload);

            Assert.IsTrue(this.refreshElementsEvent.PublishCalled);
            Assert.AreEqual(payload, this.refreshElementsEvent.PublishArgumentPayload);
        }

        /// <summary>
        /// Tests that the PlayerEvent should be published when keyboard action context is executed.
        /// </summary>
        [TestMethod]
        public void ShouldPublishPlayerEventEventWhenKeyboardActionContextIsExecuted()
        {
            var presenter = this.CreatePresenter();

            this.playerEvent.PublishCalled = false;

            presenter.KeyboardActionCommand.Execute(new Tuple<KeyboardAction, object>(KeyboardAction.TogglePlay, null));

            Assert.IsTrue(this.playerEvent.PublishCalled);
            Assert.AreEqual(PlayerMode.Timeline, this.playerEvent.PublishArgumentPayload.PlayerMode);
        }

        /// <summary>
        /// Tests that the HideLinks method should be callend when invoking the HidingLinks event.
        /// </summary>
        [TestMethod]
        public void ShouldCallToHideLinksOnViewWhenHidingLinksEventIsInvoked()
        {
            var timelineElement = new TimelineElement();
            var presenter = this.CreatePresenter();

            this.view.HideLinkCalled = false;

            this.view.InvokeHidingLinks(timelineElement);

            Assert.IsTrue(this.view.HideLinkCalled);
        }

        /// <summary>
        /// Tests that ShowLinks should be called when ShowLinks event is invoked with InPosition as LinkPosition
        /// and if there is an element in next position.
        /// </summary>
        [TestMethod]
        public void ShouldCallToShowLinksOnViewWhenShowLinksEventIsInvokedWithInPositionIfThereIsAElementInPreviuosPosition()
        {
            var timelineElement = new TimelineElement
                                      {
                                          Position = TimeCode.FromSeconds(60d, SmpteFrameRate.Smpte2997Drop),
                                          InPosition = TimeCode.FromSeconds(10d, SmpteFrameRate.Smpte2997Drop),
                                          OutPosition = TimeCode.FromSeconds(40d, SmpteFrameRate.Smpte2997Drop)
                                      };

            var previousElement = new TimelineElement();
            var presenter = this.CreatePresenter();

            this.view.ShowLinkCalled = false;

            this.sequenceModel.GetElementAtPositionReturnFunction = () =>
                                                                        {
                                                                            if (this.sequenceModel.GetElementAtPositionPositionArgument == timelineElement.Position)
                                                                            {
                                                                                return previousElement;
                                                                            }

                                                                            return null;
                                                                        };

            this.view.InvokeShowingLinks(timelineElement);

            Assert.IsTrue(this.view.ShowLinkCalled);
            Assert.AreEqual(LinkPosition.In, this.view.ShowLinkLinkPositionArgument);
        }

        /// <summary>
        /// Tests that the ShowLinks method should be callend when invoking the ShowLinksEvent with OutPosition as LinkPosition
        /// and if there is an element in a next position.
        /// </summary>
        [TestMethod]
        public void ShouldCallToShowLinksOnViewWhenShowLinksEventIsInvokedWithOutPositionIfThereIsAElementInNextPosition()
        {
            var timelineElement = new TimelineElement
            {
                Position = TimeCode.FromSeconds(60d, SmpteFrameRate.Smpte2997Drop),
                InPosition = TimeCode.FromSeconds(10d, SmpteFrameRate.Smpte2997Drop),
                OutPosition = TimeCode.FromSeconds(40d, SmpteFrameRate.Smpte2997Drop)
            };

            var nextElement = new TimelineElement();
            var presenter = this.CreatePresenter();

            this.view.ShowLinkCalled = false;

            this.sequenceModel.GetElementAtPositionReturnFunction = () =>
            {
                if (this.sequenceModel.GetElementAtPositionPositionArgument == timelineElement.Position + timelineElement.Duration)
                {
                    return nextElement;
                }

                return null;
            };

            this.view.InvokeShowingLinks(timelineElement);

            Assert.IsTrue(this.view.ShowLinkCalled);
            Assert.AreEqual(LinkPosition.Out, this.view.ShowLinkLinkPositionArgument);
        }

        /// <summary>
        /// Tests that the ToggleLinkElementCommand should be executed when the LinkingElement event is invoked.
        /// </summary>
        [TestMethod]
        public void ShouldExecuteToggleLinkElementCommandOnCaretakerWhenLinkingElementIsInvoked()
        {
            var timelineElement = new TimelineElement { Asset = new VideoAsset() };

            var presenter = this.CreatePresenter();

            this.caretaker.ExecuteCommandCalled = false;

            this.view.InvokeLinkingElement(new LinkElementEventArgs(timelineElement, LinkPosition.In));

            Assert.IsTrue(this.caretaker.ExecuteCommandCalled);
            Assert.IsInstanceOfType(this.caretaker.ExecuteCommandArgument, typeof(ToggleLinkElementCommand));
        }

        /// <summary>
        /// Tests that the PickThumbnailEvent event should be published when Keyboard Action Command is executed.
        /// </summary>
        [TestMethod]
        public void ShouldPublishPickThumbnailEventEventWhenKeyboardActionCommandIsExecuted()
        {
            var presenter = this.CreatePresenter();

            this.pickThumbnailEvent.PublishCalled = false;

            presenter.KeyboardActionCommand.Execute(new Tuple<KeyboardAction, object>(KeyboardAction.PickThumbnail, null));

            Assert.IsTrue(this.pickThumbnailEvent.PublishCalled);
        }

        [TestMethod]
        public void ShouldNotCanExecuteRemoveAudioTrackCommandIfThereIsOnlyOneAudioTrack()
        {
            this.projectService.GetCurrentProjectReturnValue = new RCE.Infrastructure.Models.Project();
            
            this.projectService.GetCurrentProjectReturnValue.Timelines.Add(new Sequence());

            this.projectService.GetCurrentProjectReturnValue.Timelines[0].Tracks.Add(new Track { TrackType = TrackType.Audio, Number = 1 });
            
            var presenter = this.CreatePresenter();

            this.projectService.InvokeProjectRetrieved();

            bool result = presenter.RemoveAudioTrackCommand.CanExecute(null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ShouldCanExecuteRemoveAudioTrackCommandIfThereAreMoreThanOneAudioTrack()
        {
            this.projectService.GetCurrentProjectReturnValue = new RCE.Infrastructure.Models.Project();

            this.projectService.GetCurrentProjectReturnValue.Timelines.Add(new Sequence());

            this.projectService.GetCurrentProjectReturnValue.Timelines[0].Tracks.Add(new Track { TrackType = TrackType.Audio, Number = 1 });
            this.projectService.GetCurrentProjectReturnValue.Timelines[0].Tracks.Add(new Track { TrackType = TrackType.Audio, Number = 2 });
            
            var presenter = this.CreatePresenter();

            this.projectService.InvokeProjectRetrieved();

            bool result = presenter.RemoveAudioTrackCommand.CanExecute(null);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ShouldNotCanExecuteAddAudioTrackCommandIfMaxAudioTrackLimitWasReached()
        {
            this.projectService.GetCurrentProjectReturnValue = new RCE.Infrastructure.Models.Project();

            this.projectService.GetCurrentProjectReturnValue.Timelines.Add(new Sequence());

            this.projectService.GetCurrentProjectReturnValue.Timelines[0].Tracks.Add(new Track { TrackType = TrackType.Audio, Number = 1 });
            this.projectService.GetCurrentProjectReturnValue.Timelines[0].Tracks.Add(new Track { TrackType = TrackType.Audio, Number = 2 });
            
            this.configurationService.GetParameterValueReturnFunction = parameter => parameter == "MaxNumberOfAudioTracks" ? "2" : null;

            var presenter = this.CreatePresenter();

            this.projectService.InvokeProjectRetrieved();

            bool result = presenter.AddAudioTrackCommand.CanExecute(null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ShouldCanExecuteAddAudioTrackCommandIfMaxAudioTrackLimitWasNotReached()
        {
            this.projectService.GetCurrentProjectReturnValue = new RCE.Infrastructure.Models.Project();
            this.projectService.GetCurrentProjectReturnValue.Timelines.Add(new Sequence());

            this.projectService.GetCurrentProjectReturnValue.Timelines[0].Tracks.Add(new Track { TrackType = TrackType.Audio, Number = 1 });
            this.projectService.GetCurrentProjectReturnValue.Timelines[0].Tracks.Add(new Track { TrackType = TrackType.Audio, Number = 2 });

            this.configurationService.GetParameterValueReturnFunction = parameter => parameter == "MaxNumberOfAudioTracks" ? "5" : null;

            var presenter = this.CreatePresenter();

            this.projectService.InvokeProjectRetrieved();

            bool result = presenter.AddAudioTrackCommand.CanExecute(null);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ShouldAddAudioTrackWhenExecutingAddAudioTrackCommand()
        {
            this.projectService.GetCurrentProjectReturnValue = new RCE.Infrastructure.Models.Project();

            this.projectService.GetCurrentProjectReturnValue.Timelines.Add(new Sequence());

            this.projectService.GetCurrentProjectReturnValue.Timelines[0].Tracks.Add(new Track { TrackType = TrackType.Audio, Number = 1 });

            var presenter = this.CreatePresenter();
            this.sequenceRegistry.InvokeCurrentSequenceChanged();

            this.projectService.InvokeProjectRetrieved();

            presenter.AddAudioTrackCommand.Execute(null);

            Assert.AreEqual(2, presenter.AudioTracks.Count);
            Assert.AreEqual(2, presenter.AudioTracks[1].Number);
        }

        [TestMethod]
        public void ShouldRemoveAudioTrackWhenExecutingAddAudioTrackCommand()
        {
            this.projectService.GetCurrentProjectReturnValue = new RCE.Infrastructure.Models.Project();
            this.projectService.GetCurrentProjectReturnValue.Timelines.Add(new Sequence());

            this.projectService.GetCurrentProjectReturnValue.Timelines[0].Tracks.Add(new Track { TrackType = TrackType.Audio, Number = 1 });
            this.projectService.GetCurrentProjectReturnValue.Timelines[0].Tracks.Add(new Track { TrackType = TrackType.Audio, Number = 2 });

            var presenter = this.CreatePresenter();
            this.sequenceRegistry.InvokeCurrentSequenceChanged();

            this.projectService.InvokeProjectRetrieved();

            presenter.RemoveAudioTrackCommand.Execute(null);

            Assert.AreEqual(1, presenter.AudioTracks.Count);
            Assert.AreEqual(1, presenter.AudioTracks[0].Number);
        }

        [TestMethod]
        public void ShouldIncreaseCurrentPositionByOneFrameWhenExecutingMoveFrameCommand()
        {
            TimeCode timeCode = new TimeCode(10, 10, 20, 5, SmpteFrameRate.Smpte24);
            
            var presenter = this.CreatePresenter();
            
            this.sequenceModel.CurrentPosition = timeCode;

            presenter.MoveFrameCommand.Execute(1);

            timeCode = timeCode.Add(TimeCode.FromFrames(1, SmpteFrameRate.Smpte24));

            Assert.AreEqual(timeCode, this.sequenceModel.CurrentPosition);
        }

        [TestMethod]
        public void ShouldDecreaseCurrentPositionByOneFrameWhenExecutingMoveFrameCommand()
        {
            TimeCode timeCode = new TimeCode(10, 10, 20, 5, SmpteFrameRate.Smpte24);

            var presenter = this.CreatePresenter();

            this.sequenceModel.CurrentPosition = timeCode;

            presenter.MoveFrameCommand.Execute(-1);

            timeCode = timeCode.Subtract(TimeCode.FromFrames(1, SmpteFrameRate.Smpte24));

            Assert.AreEqual(timeCode, this.sequenceModel.CurrentPosition);
        }

        /// <summary>
        /// Tests that the SnapModeEnable value should be retrieved from the ConfigurationService.
        /// </summary>
        [TestMethod]
        public void ShouldGetIfTheSnapModeIsEnableFromTheConfigurationService()
        {
            this.configurationService.GetParameterValueReturnFunction = parameter =>
            {
                if (parameter == "SnapModeEnabled")
                {
                    return "true";
                }

                return string.Empty;
            };

            var presenter = this.CreatePresenter();

            Assert.AreEqual(bool.Parse("true"), presenter.IsInSnapMode);
        }

        [TestMethod]
        public void ShouldMoveTimelinePositionToNextElementPositionWhenExecutingTheMoveNextClipCommand()
        {
            var element = new TimelineElement
            {
                Asset = new VideoAsset(),
                Position = TimeCode.FromAbsoluteTime(20, this.sequenceModel.Duration.FrameRate),
                InPosition = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                OutPosition = TimeCode.FromAbsoluteTime(20, this.sequenceModel.Duration.FrameRate)
            };

            var visualTrack = new Track { TrackType = TrackType.Visual };
            visualTrack.Shots.Add(element);

            this.sequenceModel.Tracks.Add(visualTrack);

            var presenter = this.CreatePresenter();

            this.sequenceModel.CurrentPosition = TimeCode.FromAbsoluteTime(10, this.sequenceModel.Duration.FrameRate);

            this.sequenceModel.GetNextElementReturnFunction = (position, track) =>
                                                                  {
                                                                      if (position == this.sequenceModel.CurrentPosition && track == visualTrack)
                                                                      {
                                                                          return element;
                                                                      }

                                                                      return null;
                                                                  };

            presenter.MoveNextClipCommand.Execute(null);

            Assert.AreEqual((element.Position + TimeCode.FromFrames(1, element.Position.FrameRate)).TotalSeconds, this.sequenceModel.CurrentPosition.TotalSeconds);
        }

        [TestMethod]
        public void ShouldMoveTimelinePositionToPreviousElementPositionWhenExecutingTheMovePreviousClipCommand()
        {
            var element = new TimelineElement
            {
                Asset = new VideoAsset(),
                Position = TimeCode.FromAbsoluteTime(20, this.sequenceModel.Duration.FrameRate),
                InPosition = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                OutPosition = TimeCode.FromAbsoluteTime(20, this.sequenceModel.Duration.FrameRate)
            };

            var visualTrack = new Track { TrackType = TrackType.Visual };
            visualTrack.Shots.Add(element);

            this.sequenceModel.Tracks.Add(visualTrack);

            var presenter = this.CreatePresenter();

            this.sequenceModel.CurrentPosition = TimeCode.FromAbsoluteTime(50, this.sequenceModel.Duration.FrameRate);

            this.sequenceModel.GetPreviousElementReturnFunction = (position, track) =>
            {
                if (position == this.sequenceModel.CurrentPosition && track == visualTrack)
                {
                    return element;
                }

                return null;
            };

            presenter.MovePreviousClipCommand.Execute(null);

            Assert.AreEqual((element.Position + TimeCode.FromFrames(1, element.Position.FrameRate)).TotalSeconds, this.sequenceModel.CurrentPosition.TotalSeconds);
        }

        /// <summary>
        /// Tests if the OnPropertyChanged event is being raised when the IsInRippleMode property change.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseOnPropertyChangedEventWhenIsInRippleModeIsUpdated()
        {
            var propertyChangedRaised = false;
            string propertyChangedEventArgsArgument = null;

            var presenter = this.CreatePresenter();
            presenter.PropertyChanged += (sender, e) =>
            {
                propertyChangedRaised = true;
                propertyChangedEventArgsArgument = e.PropertyName;
            };

            Assert.IsFalse(propertyChangedRaised);
            Assert.IsNull(propertyChangedEventArgsArgument);

            presenter.IsInRippleMode = true;

            Assert.IsTrue(propertyChangedRaised);
            Assert.AreEqual("IsInRippleMode", propertyChangedEventArgsArgument);
        }

        [TestMethod]
        public void ShouldGetCorrectTimelineDurationAfterAddingAnElementToTheTimelineModel()
        {
            var element = new TimelineElement
            {
                Asset = new VideoAsset(),
                Position = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                InPosition = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                OutPosition = TimeCode.FromAbsoluteTime(20, this.sequenceModel.Duration.FrameRate)
            };

            var visualTrack = new Track { TrackType = TrackType.Visual };
            visualTrack.Shots.Add(element);

            var presenter = this.CreatePresenter();

            Assert.AreEqual(0, presenter.TimelineDuration.TotalSeconds);

            this.sequenceModel.Tracks.Add(visualTrack);

            Assert.AreEqual(element.Duration.TotalSeconds, presenter.TimelineDuration.TotalSeconds);
        }

        [TestMethod]
        public void ShouldAlignSingleElementToPlayheadPosition()
        {
            var presenter = this.CreatePresenter();
            var element = new TimelineElement
            {
                Asset = new VideoAsset(),
                Position = TimeCode.FromAbsoluteTime(300, this.sequenceModel.Duration.FrameRate),
            };

            this.view.InvokeElementSelect(element, element.Position);

            TimeCode playHeadPosition = TimeCode.FromAbsoluteTime(10, this.sequenceModel.Duration.FrameRate);
            this.sequenceModel.CurrentPosition = playHeadPosition;

            presenter.AlignSelectedElementsToPlayheadPosition();

            Assert.IsTrue(this.sequenceModel.MoveElementCalled);
            Assert.AreEqual(playHeadPosition, this.sequenceModel.MoveElementNewPositionArgument);
            Assert.AreSame(element, this.sequenceModel.MoveElementElementArgument);
        }

        [TestMethod]
        public void ShouldAlignSelectedElementsToPlayheadPosition()
        {
            this.sequenceModel.Tracks.Add(new Track { TrackType = TrackType.Visual });
            this.sequenceModel.Tracks.Add(new Track { TrackType = TrackType.Audio });
            this.sequenceModel.Tracks.Add(new Track { TrackType = TrackType.Audio });
            var presenter = this.CreatePresenter();
            
            var videoElement = new TimelineElement
            {
                Asset = new VideoAsset(),
                Position = TimeCode.FromAbsoluteTime(50, this.sequenceModel.Duration.FrameRate),
            };

            this.sequenceModel.Tracks[0].Shots.Add(videoElement);

            var audioElement1 = new TimelineElement
            {
                Asset = new AudioAsset(),
                Position = TimeCode.FromAbsoluteTime(4, this.sequenceModel.Duration.FrameRate),
            };

            this.sequenceModel.Tracks[1].Shots.Add(audioElement1);

            var audioElement2 = new TimelineElement
            {
                Asset = new AudioAsset(),
                Position = TimeCode.FromAbsoluteTime(300, this.sequenceModel.Duration.FrameRate),
            };
            
            this.sequenceModel.Tracks[2].Shots.Add(audioElement2);

            this.view.InvokeElementMultiSelect(videoElement, videoElement.Position);
            this.view.InvokeElementMultiSelect(audioElement1, audioElement1.Position);
            this.view.InvokeElementMultiSelect(audioElement2, audioElement2.Position);

            TimeCode playHeadPosition = TimeCode.FromAbsoluteTime(10, this.sequenceModel.Duration.FrameRate);
            this.sequenceModel.CurrentPosition = playHeadPosition;

            presenter.AlignSelectedElementsToPlayheadPosition();

            // 6  takes into account considers movement from memento
            Assert.AreEqual(6, this.sequenceModel.MoveElementTimesCalled);
            Assert.AreEqual(playHeadPosition, this.sequenceModel.MoveElementNewPositionArguments[0]);
            Assert.AreEqual(playHeadPosition, this.sequenceModel.MoveElementNewPositionArguments[2]);
            Assert.AreEqual(playHeadPosition, this.sequenceModel.MoveElementNewPositionArguments[4]);
            CollectionAssert.Contains(this.sequenceModel.MoveElementElementArguments, videoElement);
            CollectionAssert.Contains(this.sequenceModel.MoveElementElementArguments, audioElement1);
            CollectionAssert.Contains(this.sequenceModel.MoveElementElementArguments, audioElement2);
        }

        [TestMethod]
        public void ShouldLockSelectedElementsWhenExecutingLockCommand()
        {
            this.sequenceModel.Tracks.Add(new Track { TrackType = TrackType.Audio });

            var presenter = this.CreatePresenter();

            const double AudioSecondsDuration = 10;

            var audioElement1 = new TimelineElement
            {
                Asset = new AudioAsset(),
                Position = TimeCode.FromAbsoluteTime(300, this.sequenceModel.Duration.FrameRate),
                InPosition = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                OutPosition = TimeCode.FromSeconds(AudioSecondsDuration, this.sequenceModel.Duration.FrameRate)
            };

            var audioElement2 = new TimelineElement
            {
                Asset = new AudioAsset(),
                Position = TimeCode.FromAbsoluteTime(150, this.sequenceModel.Duration.FrameRate),
                InPosition = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                OutPosition = TimeCode.FromSeconds(AudioSecondsDuration, this.sequenceModel.Duration.FrameRate)
            };

            this.sequenceModel.Tracks[0].Shots.Add(audioElement1);
            this.sequenceModel.Tracks[0].Shots.Add(audioElement2);

            this.view.InvokeElementMultiSelect(audioElement1, audioElement1.Position);
            this.view.InvokeElementMultiSelect(audioElement2, audioElement2.Position);

            presenter.LockCommand.Execute(null);

            IEnumerable<TimelineElement> elementsLocked = this.lockGroupManagerService.ElementsLocked;

            Assert.AreEqual(2, elementsLocked.Count());
            Assert.IsTrue(elementsLocked.Contains(audioElement1));
            Assert.IsTrue(elementsLocked.Contains(audioElement2));
        }

        [TestMethod]
        public void ShouldUnlockElementWhenHandlingUnlockEvent()
        {
            var audioElement1 = new TimelineElement
            {
                Asset = new AudioAsset(),
            };

            this.CreatePresenter();

            this.view.InvokeElementUnlock(audioElement1);

            Assert.AreSame(audioElement1, this.lockGroupManagerService.ElementUnlocked);
        }

        [TestMethod]
        public void ShouldRemoveElementsInTracksWhenCurrentSequenceChanges()
        {
            var presenter = this.CreatePresenter();

            var audioElement1 = new TimelineElement();
            var audioElement2 = new TimelineElement();
            var audioElement3 = new TimelineElement();
            var videoElement1 = new TimelineElement();
            var videoElement2 = new TimelineElement();

            var audioTrack1 = new Track();
            var audioTrack2 = new Track();
            var videoTrack = new Track();

            audioTrack1.Shots.Add(audioElement1);
            audioTrack1.Shots.Add(audioElement2);
            audioTrack2.Shots.Add(audioElement3);
            videoTrack.Shots.Add(videoElement1);
            videoTrack.Shots.Add(videoElement2);

            presenter.AudioTracks.Add(audioTrack1);
            presenter.AudioTracks.Add(audioTrack2);
            presenter.VideoTracks.Add(videoTrack);
           
            Assert.AreEqual(0, this.view.RemoveElements.Count);

            this.sequenceRegistry.InvokeCurrentSequenceChanged(this.sequenceModel);

            Assert.AreEqual(5, this.view.RemoveElements.Count);
            Assert.IsTrue(this.view.RemoveElements.Contains(audioElement1));
            Assert.IsTrue(this.view.RemoveElements.Contains(audioElement2));
            Assert.IsTrue(this.view.RemoveElements.Contains(audioElement3));
            Assert.IsTrue(this.view.RemoveElements.Contains(videoElement1));
            Assert.IsTrue(this.view.RemoveElements.Contains(videoElement2));
        }

        [TestMethod]
        public void ShouldClearAudioAndVideoTracksWhenCurrentSequenceChanges()
        {
            var presenter = this.CreatePresenter();

            var audioTrack1 = new Track();
            var audioTrack2 = new Track();
            var videoTrack = new Track();

            presenter.AudioTracks.Add(audioTrack1);
            presenter.AudioTracks.Add(audioTrack2);
            presenter.VideoTracks.Add(videoTrack);

            Assert.AreEqual(2, presenter.AudioTracks.Count);
            Assert.AreEqual(1, presenter.VideoTracks.Count);

            this.sequenceRegistry.InvokeCurrentSequenceChanged(this.sequenceModel);

            Assert.AreEqual(0, presenter.AudioTracks.Count);
            Assert.AreEqual(0, presenter.VideoTracks.Count);
        }

        [TestMethod]
        public void ShouldPopulateAudioAndVideoTracksFromNewSequence()
        {
            this.sequenceModel.Tracks.Add(new Track { TrackType = TrackType.Audio });
            this.sequenceModel.Tracks.Add(new Track { TrackType = TrackType.Audio });
            this.sequenceModel.Tracks.Add(new Track { TrackType = TrackType.Audio });
            this.sequenceModel.Tracks.Add(new Track { TrackType = TrackType.Visual });

            var presenter = this.CreatePresenter();
            this.sequenceRegistry.CurrentSequenceModel = this.sequenceModel;
            
            Assert.AreEqual(0, presenter.AudioTracks.Count);
            Assert.AreEqual(0, presenter.VideoTracks.Count);

            this.sequenceRegistry.InvokeCurrentSequenceChanged(this.sequenceModel);

            Assert.AreEqual(3, presenter.AudioTracks.Count);
            Assert.AreEqual(1, presenter.VideoTracks.Count);
        }

        [TestMethod]
        public void ShouldAddElementsFromTracksIntoView()
        {
            var presenter = this.CreatePresenter();

            var audioElement1 = new TimelineElement();
            var audioElement2 = new TimelineElement();
            var audioElement3 = new TimelineElement();
            var videoElement1 = new TimelineElement();
            var videoElement2 = new TimelineElement();

            var audioTrack1 = new Track();
            var audioTrack2 = new Track();
            var videoTrack = new Track();

            audioTrack1.Shots.Add(audioElement1);
            audioTrack1.Shots.Add(audioElement2);
            audioTrack2.Shots.Add(audioElement3);
            videoTrack.Shots.Add(videoElement1);
            videoTrack.Shots.Add(videoElement2);

            this.sequenceModel.AddTrack(audioTrack1);
            this.sequenceModel.AddTrack(audioTrack2);
            this.sequenceModel.AddTrack(videoTrack);

            Assert.AreEqual(0, this.view.AddElements.Count);

            this.sequenceRegistry.CurrentSequenceModel = this.sequenceModel;
            this.sequenceRegistry.InvokeCurrentSequenceChanged(this.sequenceModel);

            Assert.AreEqual(5, this.view.AddElements.Count);
            Assert.IsTrue(this.view.AddElements.Contains(audioElement1));
            Assert.IsTrue(this.view.AddElements.Contains(audioElement2));
            Assert.IsTrue(this.view.AddElements.Contains(audioElement3));
            Assert.IsTrue(this.view.AddElements.Contains(videoElement1));
            Assert.IsTrue(this.view.AddElements.Contains(videoElement2));
        }

        /// <summary>
        /// Creates the TimelinePresenter for testing.
        /// </summary>
        /// <returns>The TimelinePresenter with all the dependencies mocked.</returns>
        private ITimelinePresenter CreatePresenter()
        {
            ServiceLocator.SetLocatorProvider(() => new MockServiceLocator());

            return new TimelinePresenter(this.view, this.eventAggregator, this.sequenceRegistry, this.projectService, this.caretaker, this.configurationService, this.lockGroupManagerService);
        }

        /// <summary>
        /// Creates the TimelinePresenter for testing.
        /// </summary>
        /// <returns>The TimelinePresenter with all the dependencies mocked and with sample data.</returns>
        private ITimelinePresenter CreatePresenterWithDemoData()
        {
            this.sequenceModel.Tracks.Add(new Track { TrackType = TrackType.Visual });
            var presenter = this.CreatePresenter();
            this.sequenceModel.Duration = TimeCode.FromAbsoluteTime(10000, SmpteFrameRate.Smpte30);
            this.view.SetDuration(this.sequenceModel.Duration);

            // video 1
            // dur: 1000  (0/1000)
            // start: 0
            var element1 = new TimelineElement
            {
                Asset = new VideoAsset
                {
                    Duration = TimeCode.FromAbsoluteTime(1000, SmpteFrameRate.Smpte30),
                    FrameRate = SmpteFrameRate.Smpte24,
                    Title = "Test Video #1"
                },
                InPosition = TimeCode.FromAbsoluteTime(0, SmpteFrameRate.Smpte30),
                OutPosition = TimeCode.FromAbsoluteTime(1000, SmpteFrameRate.Smpte30),
                Position = TimeCode.FromAbsoluteTime(0, SmpteFrameRate.Smpte30)
            };

            this.sequenceModel.Tracks[0].Shots.Add(element1);

            // video 2
            // dur: 500 (500/1000)
            // start: 1000
            var element2 = new TimelineElement
            {
                Asset = new VideoAsset
                {
                    Duration = TimeCode.FromAbsoluteTime(1000, SmpteFrameRate.Smpte30),
                    FrameRate = SmpteFrameRate.Smpte25,
                    Title = "Test Video #2"
                },
                InPosition = TimeCode.FromAbsoluteTime(500, SmpteFrameRate.Smpte30),
                OutPosition = TimeCode.FromAbsoluteTime(1000, SmpteFrameRate.Smpte30),
                Position = TimeCode.FromAbsoluteTime(1000, SmpteFrameRate.Smpte30)
            };

            this.sequenceModel.Tracks[0].Shots.Add(element2);

            // image 3
            // dur: 300 (300)
            // start: 1500
            var element3 = new TimelineElement
            {
                Asset = new ImageAsset
                {
                    Title = "Test Image #1"
                },
                InPosition = TimeCode.FromAbsoluteTime(0, SmpteFrameRate.Smpte30),
                OutPosition = TimeCode.FromAbsoluteTime(300, SmpteFrameRate.Smpte30),
                Position = TimeCode.FromAbsoluteTime(1500, SmpteFrameRate.Smpte30)
            };

            this.sequenceModel.Tracks[0].Shots.Add(element3);

            // video 3
            // dur: 500 (250/750)
            // start: 5000
            var element4 = new TimelineElement
            {
                Asset = new VideoAsset
                {
                    Duration = TimeCode.FromAbsoluteTime(1000, SmpteFrameRate.Smpte30),
                    FrameRate = SmpteFrameRate.Smpte30,
                    Title = "Test Video #3"
                },
                InPosition = TimeCode.FromAbsoluteTime(250, SmpteFrameRate.Smpte30),
                OutPosition = TimeCode.FromAbsoluteTime(750, SmpteFrameRate.Smpte30),
                Position = TimeCode.FromAbsoluteTime(5000, SmpteFrameRate.Smpte30)
            };

            this.sequenceModel.Tracks[0].Shots.Add(element4);

            return presenter;
        }

        internal class MockServiceLocator : ServiceLocatorImplBase
        {
            public MockServiceLocator()
            {
            }

            protected override object DoGetInstance(Type serviceType, string key)
            {
                return null;
            }

            protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
            {
                throw new NotImplementedException();
            }
        }
    }
}