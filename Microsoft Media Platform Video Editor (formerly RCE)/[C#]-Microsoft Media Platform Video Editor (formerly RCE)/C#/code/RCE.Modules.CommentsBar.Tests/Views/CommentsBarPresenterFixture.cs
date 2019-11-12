// <copyright file="CommentsBarPresenterFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: CommentsBarPresenterFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.CommentsBar.Tests.Views
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Practices.Composite.Presentation.Events;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RCE.Infrastructure.Events;
    using RCE.Infrastructure.Models;
    using RCE.Infrastructure.Services;
    using RCE.Modules.CommentsBar.Tests.Mocks;
    using RCE.Services.Contracts;

    using SMPTETimecode;

    /// <summary>
    /// Test class for <see cref="BaseCommentsBarPresenter"/>.
    /// </summary>
    [TestClass]
    public class CommentsBarPresenterFixture
    {
        /// <summary>
        /// Mock for <see cref="CommentsBarView"/>.
        /// </summary>
        private MockCommentsBarView view;

        /// <summary>
        /// Mock for IEventAggregator.
        /// </summary>
        private MockEventAggregator eventAggregator;

        /// <summary>
        /// Mock for <see cref="TimelineModel"/>.
        /// </summary>
        private MockSequenceModel sequenceModel;

        /// <summary>
        /// Mock for <see cref="ITimelineBarRegistry"/>.
        /// </summary>
        private MockTimelineBarRegistry timelineBarRegistry;

        /// <summary>
        /// Mock for <see cref="PositionDoubleClickedEvent"/>.
        /// </summary>
        private MockPositionDoubleClickedEvent positionDoubleClickedEvent;

        /// <summary>
        /// Mock for <see cref="RefreshElementsEvent"/>.
        /// </summary>
        private MockRefreshElementsEvent refreshElementsEvent;

        /// <summary>
        /// Mock for <see cref="AddPreviewEvent"/>.
        /// </summary>
        private MockAddPreviewEvent addPreviewEvent;

        private MockSequenceRegistry sequenceRegistry;

        private MockDeleteAllPreviewsEvent deleteAllPreviewsEvent;

        private MockRemovePreviewEvent removePreviewEvent;

        /// <summary>
        /// Initilize the local data.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.view = new MockCommentsBarView();
            this.eventAggregator = new MockEventAggregator();
            this.sequenceModel = new MockSequenceModel();
            this.timelineBarRegistry = new MockTimelineBarRegistry();

            this.positionDoubleClickedEvent = new MockPositionDoubleClickedEvent();
            this.refreshElementsEvent = new MockRefreshElementsEvent();
            this.addPreviewEvent = new MockAddPreviewEvent();
            this.deleteAllPreviewsEvent = new MockDeleteAllPreviewsEvent();
            this.removePreviewEvent = new MockRemovePreviewEvent();
            
            this.sequenceRegistry = new MockSequenceRegistry();
            this.sequenceRegistry.CurrentSequenceModel = this.sequenceModel;

            this.eventAggregator.AddMapping<PositionDoubleClickedEvent>(this.positionDoubleClickedEvent);
            this.eventAggregator.AddMapping<RefreshElementsEvent>(this.refreshElementsEvent);
            this.eventAggregator.AddMapping<AddPreviewEvent>(this.addPreviewEvent);
            this.eventAggregator.AddMapping<DeleteAllPreviewsEvent>(this.deleteAllPreviewsEvent);
            this.eventAggregator.AddMapping<RemovePreviewEvent>(this.removePreviewEvent);
        }

        /// <summary>
        /// Tests if the <see cref="BaseCommentsBarPresenter"/> initilizes the view.
        /// </summary>
        [TestMethod]
        public void CanInitPresenter()
        {
            var presenter = this.CreatePresenter();

            Assert.AreEqual(this.view, presenter.View);
        }

        /// <summary>
        /// Should set the timeline model duration to <see cref="CommentsBarView"/>.
        /// </summary>
        [TestMethod]
        public void ShouldSetTimelineModelDurationToView()
        {
            this.sequenceModel.Duration = TimeCode.FromSeconds(600d, SmpteFrameRate.Smpte2997Drop);

            Assert.IsFalse(this.view.SetDurationCalled);
            Assert.AreNotEqual(this.sequenceModel.Duration, this.view.SetDurationArgument);
            this.sequenceRegistry.CurrentSequenceModel = this.sequenceModel;

            var presenter = this.CreatePresenter();
            
            Assert.IsTrue(this.view.SetDurationCalled);
            Assert.AreEqual(this.sequenceModel.Duration, this.view.SetDurationArgument);
        }

        /// <summary>
        /// Should call RefreshPreviews of view when <see cref="RefreshElementsEvent"/>
        /// event is published.
        /// </summary>
        [TestMethod]
        public void ShouldCallRefreshElementsWithRefreshElementsEventEventSubscription()
        {
            Assert.IsNull(this.refreshElementsEvent.SubscribeArgumentAction);

            var presenter = this.CreatePresenter();

            Assert.IsFalse(this.view.RefreshPreviewsCalled);
            Assert.AreEqual(0, this.view.RefreshPreviewsArgument);

            Assert.IsNotNull(this.refreshElementsEvent.SubscribeArgumentAction);
            Assert.AreEqual(ThreadOption.PublisherThread, this.refreshElementsEvent.SubscribeArgumentThreadOption);

            this.refreshElementsEvent.SubscribeArgumentAction(new RefreshElementsEventArgs(100, CommentMode.Timeline));

            Assert.IsTrue(this.view.RefreshPreviewsCalled);
            Assert.AreEqual(100, this.view.RefreshPreviewsArgument);
        }

        /// <summary>
        /// Should call RefreshPreviews on added timeline elements bar when <see cref="RefreshElementsEvent"/>
        /// event is published.
        /// </summary>
        [TestMethod]
        public void ShouldCallRefreshPreviewOnAddedTimelineBarElementsWithRefreshElementsEventEventSubscription()
        {
            var timelineBarElement = new MockTimelineBarElement();

            Assert.IsNull(this.refreshElementsEvent.SubscribeArgumentAction);

            this.timelineBarRegistry.GetTimelineBarElementKeysReturnValue = new List<string> { "Test" };
            this.timelineBarRegistry.GetTimelineBarElementFunction = key => key == "Test" ? timelineBarElement : null;

            var presenter = this.CreatePresenter();

            this.positionDoubleClickedEvent.SubscribeArgumentAction(new PositionPayloadEventArgs(TimeSpan.FromSeconds(100)));

            timelineBarElement.RefreshPreviewCalled = false;
            timelineBarElement.RefreshPreviewArgument = null;

            Assert.IsNotNull(this.refreshElementsEvent.SubscribeArgumentAction);
            Assert.AreEqual(ThreadOption.PublisherThread, this.refreshElementsEvent.SubscribeArgumentThreadOption);

            this.refreshElementsEvent.SubscribeArgumentAction(new RefreshElementsEventArgs(100, CommentMode.Timeline));

            Assert.IsTrue(timelineBarElement.RefreshPreviewCalled);
            Assert.AreEqual(100, timelineBarElement.RefreshPreviewArgument);
        }

        /// <summary>
        /// Should call SetPosition and SetRefreshedPreview On TimelineBarElement of timeline model when 
        /// <see cref="PositionDoubleClickedEvent"/> is published.
        /// </summary>
        [TestMethod]
        public void ShouldCallSetPositionAndSetRefreshedPreviewOnTimelineBarElementWithPositionDoubleClickedEventEventSubscription()
        {
            var position = TimeSpan.FromSeconds(60);
            var timelineBarElement = new MockTimelineBarElement();

            this.timelineBarRegistry.GetTimelineBarElementKeysReturnValue = new List<string> { "Test" };
            this.timelineBarRegistry.GetTimelineBarElementFunction = key => key == "Test" ? timelineBarElement : null;

            Assert.IsNull(this.positionDoubleClickedEvent.SubscribeArgumentAction);

            var presenter = this.CreatePresenter();

            Assert.IsFalse(timelineBarElement.SetPositionCalled);
            Assert.AreNotEqual(position, timelineBarElement.SetPositionArgument);
            Assert.IsFalse(timelineBarElement.RefreshPreviewCalled);
            Assert.IsNull(timelineBarElement.RefreshPreviewArgument);

            Assert.IsNotNull(this.positionDoubleClickedEvent.SubscribeArgumentAction);
            Assert.AreEqual(ThreadOption.PublisherThread, this.positionDoubleClickedEvent.SubscribeArgumentThreadOption);

            this.positionDoubleClickedEvent.SubscribeArgumentAction(new PositionPayloadEventArgs(position));

            Assert.IsTrue(timelineBarElement.SetPositionCalled);
            Assert.AreEqual(position, timelineBarElement.SetPositionArgument);
            Assert.IsTrue(timelineBarElement.RefreshPreviewCalled);
        }

        /// <summary>
        /// Should call AddPreview in the view when
        /// <see cref="PositionDoubleClickedEvent"/> is published.
        /// </summary>
        [TestMethod]
        public void ShouldCallAddPreviewInTheViewWithPositionDoubleClickedEventEventSubscription()
        {
            var position = TimeSpan.FromSeconds(60);
            var timelineBarElement = new MockTimelineBarElement
                                         {
                                             Preview = new object(),
                                             EditBox = new object(),
                                             Position = position.TotalSeconds,
                                         };

            this.timelineBarRegistry.GetTimelineBarElementKeysReturnValue = new List<string> { "Test" };
            this.timelineBarRegistry.GetTimelineBarElementFunction = key => key == "Test" ? timelineBarElement : null;

            Assert.IsNull(this.positionDoubleClickedEvent.SubscribeArgumentAction);

            var presenter = this.CreatePresenter();

            Assert.IsFalse(this.view.AddPreviewCalled);
            Assert.IsNull(this.view.AddPreviewPreviewArgument);
            Assert.IsNull(this.view.AddPreviewPositionArgument);
            Assert.IsNull(this.view.AddPreviewEditBoxArgument);

            Assert.IsNotNull(this.positionDoubleClickedEvent.SubscribeArgumentAction);
            Assert.AreEqual(ThreadOption.PublisherThread, this.positionDoubleClickedEvent.SubscribeArgumentThreadOption);

            this.positionDoubleClickedEvent.SubscribeArgumentAction(new PositionPayloadEventArgs(position));

            Assert.IsTrue(this.view.AddPreviewCalled);
            Assert.AreEqual(timelineBarElement.Preview, this.view.AddPreviewPreviewArgument);
            Assert.AreEqual(timelineBarElement.Position, this.view.AddPreviewPositionArgument);
            Assert.AreEqual(timelineBarElement.EditBox, this.view.AddPreviewEditBoxArgument);
        }

        /// <summary>
        /// Tests it the Options are being set when there are more than one element registed on the timeline registry and 
        /// the PositionDoubleClickedEvent is being invoked.
        /// </summary>
        [TestMethod]
        public void ShouldSetOptionsWithPositionDoubleClickedEventEventSubscriptionWhenThereAreMoreThanOneTimelineElementBarRegistered()
        {
            var position = TimeSpan.FromSeconds(60);
            
            this.timelineBarRegistry.GetTimelineBarElementKeysReturnValue = new List<string> { "Test", "Test2" };
            
            Assert.IsNull(this.positionDoubleClickedEvent.SubscribeArgumentAction);

            var presenter = this.CreatePresenter();

            Assert.AreEqual(0, presenter.Options.Count);

            Assert.IsNotNull(this.positionDoubleClickedEvent.SubscribeArgumentAction);
            Assert.AreEqual(ThreadOption.PublisherThread, this.positionDoubleClickedEvent.SubscribeArgumentThreadOption);

            this.positionDoubleClickedEvent.SubscribeArgumentAction(new PositionPayloadEventArgs(position));

            Assert.AreEqual(2, presenter.Options.Count);
        }

        /// <summary>
        /// Tests it the ShowOptions method is being called when there are more than one element registed on the timeline registry and 
        /// the PositionDoubleClickedEvent is being invoked.
        /// </summary>
        [TestMethod]
        public void ShouldCallToShowOptionsOnViewWithPositionDoubleClickedEventEventSubscriptionWhenThereAreMoreThanOneTimelineElementBarRegistered()
        {
            var position = TimeSpan.FromSeconds(60);

            this.timelineBarRegistry.GetTimelineBarElementKeysReturnValue = new List<string> { "Test", "Test2" };

            Assert.IsNull(this.positionDoubleClickedEvent.SubscribeArgumentAction);

            var presenter = this.CreatePresenter();

            Assert.IsFalse(this.view.ShowOptionsCalled);
            Assert.IsNull(this.view.ShowOptionsArgument);

            Assert.IsNotNull(this.positionDoubleClickedEvent.SubscribeArgumentAction);
            Assert.AreEqual(ThreadOption.PublisherThread, this.positionDoubleClickedEvent.SubscribeArgumentThreadOption);

            this.positionDoubleClickedEvent.SubscribeArgumentAction(new PositionPayloadEventArgs(position));

            Assert.IsTrue(this.view.ShowOptionsCalled);
            Assert.IsNotNull(this.view.ShowOptionsArgument);
            Assert.AreEqual(position.TotalSeconds, this.view.ShowOptionsArgument);
        }

        /// <summary>
        /// Tests it the IsOptionsMenuVisible is being set when there are more than one element registed on the timeline registry and 
        /// the PositionDoubleClickedEvent is being invoked.
        /// </summary>
        [TestMethod]
        public void ShouldSetIsOptionsMenuVisibleWithPositionDoubleClickedEventEventSubscriptionWhenThereAreMoreThanOneTimelineElementBarRegistered()
        {
            var position = TimeSpan.FromSeconds(60);

            this.timelineBarRegistry.GetTimelineBarElementKeysReturnValue = new List<string> { "Test", "Test2" };

            Assert.IsNull(this.positionDoubleClickedEvent.SubscribeArgumentAction);

            var presenter = this.CreatePresenter();

            Assert.IsFalse(presenter.IsOptionMenuVisible);

            Assert.IsNotNull(this.positionDoubleClickedEvent.SubscribeArgumentAction);
            Assert.AreEqual(ThreadOption.PublisherThread, this.positionDoubleClickedEvent.SubscribeArgumentThreadOption);

            this.positionDoubleClickedEvent.SubscribeArgumentAction(new PositionPayloadEventArgs(position));

            Assert.IsTrue(presenter.IsOptionMenuVisible);
        }

        /// <summary>
        /// Should call RemovePreview in the view when Deleting event of TimelineBarElement is invoked.
        /// </summary>
        [TestMethod]
        public void ShouldCallRemovePreviewWhenDeletingEventOfTimelineBarElementIsInvoked()
        {
            var position = TimeSpan.FromSeconds(60);
            var timelineBarElement = new MockTimelineBarElement
            {
                Preview = new object(),
                EditBox = new object(),
                Position = position.TotalSeconds,
            };

            this.timelineBarRegistry.GetTimelineBarElementKeysReturnValue = new List<string> { "Test" };
            this.timelineBarRegistry.GetTimelineBarElementFunction = key => key == "Test" ? timelineBarElement : null;

            var presenter = this.CreatePresenter();

            this.positionDoubleClickedEvent.SubscribeArgumentAction(new PositionPayloadEventArgs(position));

            Assert.IsFalse(this.view.RemovePreviewCalled);
            Assert.IsNull(this.view.RemovePreviewPreviewArgument);
            Assert.IsNull(this.view.RemovePreviewEditBoxArgument);

            timelineBarElement.InvokeDeleting();

            Assert.IsTrue(this.view.RemovePreviewCalled);
            Assert.AreEqual(timelineBarElement.Preview, this.view.RemovePreviewPreviewArgument);
            Assert.AreEqual(timelineBarElement.EditBox, this.view.RemovePreviewEditBoxArgument);
        }

        /// <summary>
        /// Should call UpdatePreview in the view when TimelineBarElementUpdated event of TimelineBarElement is invoked.
        /// </summary>
        [TestMethod]
        public void ShouldCallUpdatePreviewWhenTimelineBarElementUpdatedEventOfTimelineBarElementIsInvoked()
        {
            var position = TimeSpan.FromSeconds(60);
            var timelineBarElement = new MockTimelineBarElement
            {
                Preview = new object(),
                EditBox = new object(),
                Position = position.TotalSeconds,
            };

            this.timelineBarRegistry.GetTimelineBarElementKeysReturnValue = new List<string> { "Test" };
            this.timelineBarRegistry.GetTimelineBarElementFunction = key => key == "Test" ? timelineBarElement : null;

            var presenter = this.CreatePresenter();

            this.positionDoubleClickedEvent.SubscribeArgumentAction(new PositionPayloadEventArgs(position));

            Assert.IsFalse(this.view.UpdatePreviewCalled);
            Assert.IsNull(this.view.UpdatePreviewPreviewArgument);
            Assert.IsNull(this.view.UpdatePreviewPositionArgument);
            Assert.IsNull(this.view.UpdatePreviewPreviewArgument);

            timelineBarElement.InvokeTimelineBarElementUpdated();

            Assert.IsTrue(this.view.UpdatePreviewCalled);
            Assert.AreEqual(timelineBarElement.Preview, this.view.UpdatePreviewPreviewArgument);
            Assert.AreEqual(timelineBarElement.Position, this.view.UpdatePreviewPositionArgument);
            Assert.AreEqual(timelineBarElement.EditBox, this.view.UpdatePreviewEditBoxArgument);
        }

        /// <summary>
        /// Should call AddPreview in the view when the OptionSelectedCommand is being executed.
        /// </summary>
        [TestMethod]
        public void ShouldCallAddPreviewInTheViewWhenOptionSelectedCommandIsBeingExecuted()
        {
            var position = TimeSpan.FromSeconds(60);
            var timelineBarElement = new MockTimelineBarElement
            {
                Preview = new object(),
                EditBox = new object(),
                Position = position.TotalSeconds,
            };

            this.timelineBarRegistry.GetTimelineBarElementKeysReturnValue = new List<string> { "Test" };
            this.timelineBarRegistry.GetTimelineBarElementFunction = key => key == "Test" ? timelineBarElement : null;

            var presenter = this.CreatePresenter();

            Assert.IsFalse(this.view.AddPreviewCalled);
            Assert.IsNull(this.view.AddPreviewPreviewArgument);
            Assert.IsNull(this.view.AddPreviewPositionArgument);
            Assert.IsNull(this.view.AddPreviewEditBoxArgument);

            presenter.OptionSelectedCommand.Execute("Test");

            Assert.IsTrue(this.view.AddPreviewCalled);
            Assert.AreEqual(timelineBarElement.Preview, this.view.AddPreviewPreviewArgument);
            Assert.AreEqual(timelineBarElement.Position, this.view.AddPreviewPositionArgument);
            Assert.AreEqual(timelineBarElement.EditBox, this.view.AddPreviewEditBoxArgument);
        }

        /// <summary>
        /// Creates the presenter.
        /// </summary>
        /// <returns>The <see cref="ICommentsBarPresenter"/>.</returns>
        private ICommentsBarPresenter CreatePresenter()
        {
            return new TimelineCommentsBarPresenter(this.view, this.eventAggregator, this.sequenceRegistry, this.timelineBarRegistry);
        }
    }
}
