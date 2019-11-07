// <copyright file="CommentEditBoxPresentationModelFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: CommentEditBoxPresentationModelFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Comment.Tests.Views.TimelineBar
{
    using System;
    using Events;
    using Infrastructure.Events;
    using Infrastructure.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;

    using RCE.Infrastructure.Services;

    using SMPTETimecode;
    using Comment = RCE.Infrastructure.Models.Comment;
    using Sequence = RCE.Infrastructure.Models.Sequence;

    [TestClass]
    public class CommentEditBoxPresentationModelFixture
    {
        /// <summary>
        /// Mock for <see cref="CommentEditBox"/>
        /// </summary>
        private MockCommentEditBox view;

        /// <summary>
        /// Mock for <see cref="CommentViewPreview"/>
        /// </summary>
        private MockCommentViewPreview preview;

        /// <summary>
        /// Mock for IEventAggregator.
        /// </summary>
        private MockEventAggregator eventAggregator;

        /// <summary>
        /// Mock for <see cref="SequenceModel"/>.
        /// </summary>
        private MockSequenceModel sequenceModel;

        /// <summary>
        /// Mock for <see cref="PlayCommentEvent"/>.
        /// </summary>
        private MockPlayCommentEvent playCommentEvent;

        /// <summary>
        /// Mock for <see cref="CommentUpdatedEvent"/>.
        /// </summary>
        private MockCommentUpdatedEvent commentUpdatedEvent;

        /// <summary>
        /// Mock for <see cref="IConfigurationService"/>
        /// </summary>
        private MockConfigurationService configurationService;

        private MockSequenceRegistry sequenceRegistry;

        private Sequence sequence;

        /// <summary>
        /// Initilize the local data.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.view = new MockCommentEditBox();
            this.preview = new MockCommentViewPreview();
            this.eventAggregator = new MockEventAggregator();
            this.configurationService = new MockConfigurationService();
            this.sequenceModel = new MockSequenceModel();
            this.playCommentEvent = new MockPlayCommentEvent();
            this.commentUpdatedEvent = new MockCommentUpdatedEvent();
            this.sequenceRegistry = new MockSequenceRegistry();
            this.sequence = new Sequence();
            this.sequenceRegistry.CurrentSequence = this.sequence;

            this.sequenceRegistry.CurrentSequenceModel = this.sequenceModel;

            this.eventAggregator.AddMapping<PlayCommentEvent>(this.playCommentEvent);
            this.eventAggregator.AddMapping<CommentUpdatedEvent>(this.commentUpdatedEvent);
        }

        /// <summary>
        /// Tests if the <see cref="CommentEditBoxPresentationModel"/> initilizes the view and the preview.
        /// </summary>
        [TestMethod]
        public void CanInitPresentationModel()
        {
            var presentationModel = this.CreatePresentationModel();

            Assert.AreEqual(this.view, presentationModel.View);
            Assert.AreEqual(this.view, presentationModel.EditBox);
            Assert.AreEqual(this.preview, presentationModel.Preview);
        }

        /// <summary>
        /// Should set the timeline model duration to <see cref="CommentViewPreview"/>.
        /// </summary>
        [TestMethod]
        public void ShouldSetTimelineModelDurationToView()
        {
            this.sequenceModel.Duration = TimeCode.FromSeconds(600d, SmpteFrameRate.Smpte2997Drop);

            Assert.IsFalse(this.preview.SetTimelineDurationCalled);
            Assert.AreNotEqual(this.sequenceModel.Duration, this.preview.SetTimelineDurationArgument);

            var presentationModel = this.CreatePresentationModel();

            Assert.IsTrue(this.preview.SetTimelineDurationCalled);
            Assert.AreEqual(this.sequenceModel.Duration, this.preview.SetTimelineDurationArgument);
        }

        /// <summary>
        /// Tests that a comment is being added to the timeline model.
        /// </summary>
        [TestMethod]
        public void ShouldAddCommentToTimelineModel()
        {
            Assert.AreEqual(0, this.sequenceRegistry.CurrentSequence.CommentElements.Count);

            var presentationModel = this.CreatePresentationModel();

            Assert.AreEqual(1, this.sequenceRegistry.CurrentSequence.CommentElements.Count);
        }

        /// <summary>
        /// Tests that default values are being set to the added comment..
        /// </summary>
        [TestMethod]
        public void ShouldSetDefaultValuestToAddedComment()
        {
            this.configurationService.GetParameterValueReturnFunction = parameter => parameter == "UserName" ? "test" : null;
            Assert.AreEqual(0, this.sequenceRegistry.CurrentSequence.CommentElements.Count);

            var presentationModel = this.CreatePresentationModel();

            Assert.AreEqual(CommentType.Timeline, this.sequenceRegistry.CurrentSequence.CommentElements[0].CommentType);
            Assert.AreEqual(0, this.sequenceRegistry.CurrentSequence.CommentElements[0].MarkIn);
            Assert.AreNotEqual(0, this.sequenceRegistry.CurrentSequence.CommentElements[0].MarkOut);
            Assert.IsTrue(this.sequenceRegistry.CurrentSequence.CommentElements[0].MarkOut > this.sequenceRegistry.CurrentSequence.CommentElements[0].MarkIn);
            Assert.AreEqual(string.Empty, this.sequenceRegistry.CurrentSequence.CommentElements[0].Text);
            Assert.AreEqual("test", this.sequenceRegistry.CurrentSequence.CommentElements[0].Creator);
        }

        /// <summary>
        /// Tests if OnPropertyChanged event is raised when MarkIn property is set.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseOnPropertyChangedEventWhenMarkInIsUpdated()
        {
            var propertyChangedRaised = false;
            string propertyChangedEventArgsArgument = null;

            var presentationModel = this.CreatePresentationModel();
            presentationModel.PropertyChanged += (sender, e) =>
            {
                propertyChangedRaised = true;
                propertyChangedEventArgsArgument = e.PropertyName;
            };

            Assert.IsFalse(propertyChangedRaised);
            Assert.IsNull(propertyChangedEventArgsArgument);

            presentationModel.MarkOut = 11;
            presentationModel.MarkIn = 10;

            Assert.IsTrue(propertyChangedRaised);
            Assert.AreEqual("MarkIn", propertyChangedEventArgsArgument);
        }

        /// <summary>
        /// Tests if OnPropertyChanged event is raised when MarkOut property is set.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseOnPropertyChangedEventWhenMarkOutIsUpdated()
        {
            var propertyChangedRaised = false;
            string propertyChangedEventArgsArgument = null;

            var presentationModel = this.CreatePresentationModel();
            presentationModel.PropertyChanged += (sender, e) =>
            {
                propertyChangedRaised = true;
                propertyChangedEventArgsArgument = e.PropertyName;
            };

            Assert.IsFalse(propertyChangedRaised);
            Assert.IsNull(propertyChangedEventArgsArgument);

            presentationModel.MarkOut = 11;

            Assert.IsTrue(propertyChangedRaised);
            Assert.AreEqual("MarkOut", propertyChangedEventArgsArgument);
        }

        /// <summary>
        /// Tests if OnPropertyChanged event is raised when Text property is set.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseOnPropertyChangedEventWhenTextIsUpdated()
        {
            var propertyChangedRaised = false;
            string propertyChangedEventArgsArgument = null;

            var presentationModel = this.CreatePresentationModel();
            presentationModel.PropertyChanged += (sender, e) =>
            {
                propertyChangedRaised = true;
                propertyChangedEventArgsArgument = e.PropertyName;
            };

            Assert.IsFalse(propertyChangedRaised);
            Assert.IsNull(propertyChangedEventArgsArgument);

            presentationModel.Text = "Test";

            Assert.IsTrue(propertyChangedRaised);
            Assert.AreEqual("Text", propertyChangedEventArgsArgument);
        }

        /// <summary>
        /// Tests if the position returns the correct time.
        /// </summary>
        [TestMethod]
        public void ShouldReturnMarkInWhenUsingPositionProperty()
        {
            var presentationModel = this.CreatePresentationModel();

            presentationModel.MarkOut = 11;
            presentationModel.MarkIn = 10;

            Assert.AreEqual(presentationModel.MarkIn, presentationModel.Position);
        }

        /// <summary>
        /// Tests if values from the comment are being set to the model after calling ShowEditBox
        /// </summary>
        [TestMethod]
        public void ShouldSetValuesOnModelWhenCallToShowEditBox()
        {
            var presentationModel = this.CreatePresentationModel();

            this.sequenceRegistry.CurrentSequence.CommentElements[0].MarkIn = 10;
            this.sequenceRegistry.CurrentSequence.CommentElements[0].MarkOut = 11;
            this.sequenceRegistry.CurrentSequence.CommentElements[0].Text = "Test1";

            Assert.AreNotEqual(this.sequenceRegistry.CurrentSequence.CommentElements[0].MarkIn, presentationModel.MarkIn);
            Assert.AreNotEqual(this.sequenceRegistry.CurrentSequence.CommentElements[0].MarkOut, presentationModel.MarkOut);
            Assert.AreNotEqual(this.sequenceRegistry.CurrentSequence.CommentElements[0].Text, presentationModel.Text);

            presentationModel.ShowEditBox();

            Assert.AreEqual(this.sequenceRegistry.CurrentSequence.CommentElements[0].MarkIn, presentationModel.MarkIn);
            Assert.AreEqual(this.sequenceRegistry.CurrentSequence.CommentElements[0].MarkOut, presentationModel.MarkOut);
            Assert.AreEqual(this.sequenceRegistry.CurrentSequence.CommentElements[0].Text, presentationModel.Text);
        }

        /// <summary>
        /// Tests if show is being called on view after calling ShowEditBox
        /// </summary>
        [TestMethod]
        public void ShouldCallShowOnViewModelWhenCallToShowEditBox()
        {
            var presentationModel = this.CreatePresentationModel();

            Assert.IsFalse(this.view.ShowCalled);

            presentationModel.ShowEditBox();

            Assert.IsTrue(this.view.ShowCalled);
        }

        /// <summary>
        /// Tests if TimelineBarElementUpdate event is being raised after calling to RefreshPreview
        /// </summary>
        [TestMethod]
        public void ShouldRaiseTimelineBarElementUpdatedWhenCallingToRefreshPreview()
        {
            var timelineBarElementUpdatedEventRaised = false;

            var presentationModel = this.CreatePresentationModel();
            presentationModel.TimelineBarElementUpdated += (sender, e) => timelineBarElementUpdatedEventRaised = true;

            presentationModel.RefreshPreview(0);

            Assert.IsTrue(timelineBarElementUpdatedEventRaised);
        }

        /// <summary>
        /// Tests if RefreshPreview method is being called after calling to RefreshPreview
        /// </summary>
        [TestMethod]
        public void ShouldCallToRefreshPreviewOnPreviewWhenCallingToRefreshPreview()
        {
            var presentationModel = this.CreatePresentationModel();

            Assert.IsFalse(this.preview.RefreshPreviewCalled);

            presentationModel.RefreshPreview(10);

            Assert.IsTrue(this.preview.RefreshPreviewCalled);
            Assert.AreEqual(10, this.preview.RefreshPreviewArgument);
        }

        /// <summary>
        /// Tests if MarkIn and Mark Out are being set after calling to SetPosition
        /// </summary>
        [TestMethod]
        public void ShouldSetMarkInAndMarkOutWhenCallingToSetPosition()
        {
            var position = TimeSpan.FromSeconds(150);

            var presentationModel = this.CreatePresentationModel();

            this.sequenceRegistry.CurrentSequence.CommentElements[0].MarkIn = 10;
            this.sequenceRegistry.CurrentSequence.CommentElements[0].MarkOut = 20;

            var duration = this.sequenceRegistry.CurrentSequence.CommentElements[0].MarkOut - this.sequenceRegistry.CurrentSequence.CommentElements[0].MarkIn;

            Assert.AreNotEqual(position.TotalSeconds, presentationModel.MarkIn);
            Assert.AreNotEqual(position.TotalSeconds + duration, presentationModel.MarkOut);

            presentationModel.SetPosition(position);

            Assert.AreEqual(position.TotalSeconds, presentationModel.MarkIn);
            Assert.AreEqual(position.TotalSeconds + duration, presentationModel.MarkOut);
        }

        /// <summary>
        /// Tests if Comment MarkIn and Comment Mark Out are being set after calling to SetPosition
        /// </summary>
        [TestMethod]
        public void ShouldCommentSetMarkInAndMarkOutWhenCallingToSetPosition()
        {
            var position = TimeSpan.FromSeconds(150);

            var presentationModel = this.CreatePresentationModel();

            var duration = this.sequenceRegistry.CurrentSequence.CommentElements[0].MarkOut - this.sequenceRegistry.CurrentSequence.CommentElements[0].MarkIn;

            Assert.AreNotEqual(position.TotalSeconds, this.sequenceRegistry.CurrentSequence.CommentElements[0].MarkIn);
            Assert.AreNotEqual(position.TotalSeconds + duration, this.sequenceRegistry.CurrentSequence.CommentElements[0].MarkOut);

            presentationModel.SetPosition(position);

            Assert.AreEqual(position.TotalSeconds, this.sequenceRegistry.CurrentSequence.CommentElements[0].MarkIn);
            Assert.AreEqual(position.TotalSeconds + duration, this.sequenceRegistry.CurrentSequence.CommentElements[0].MarkOut); 
        }

        /// <summary>
        /// Tests if UpdateCommentDuration method of Preview is being called after calling to SetPosition
        /// </summary>
        [TestMethod]
        public void ShouldCallUpdateCommentDurationOnPreviewWhenCallingToSetPosition()
        {
            var position = TimeSpan.FromSeconds(150);

            var presentationModel = this.CreatePresentationModel();

            Assert.IsFalse(this.preview.UpdateCommentDurationCalled);

            presentationModel.SetPosition(position);

            var duration = this.sequenceRegistry.CurrentSequence.CommentElements[0].MarkOut - this.sequenceRegistry.CurrentSequence.CommentElements[0].MarkIn;

            Assert.IsTrue(this.preview.UpdateCommentDurationCalled);
            Assert.AreEqual(duration, this.preview.UpdateCommentDurationArgument.TotalSeconds);
        }

        /// <summary>
        /// Tests if TimelineBarElementUpdate event is being raised after calling to SetPosition
        /// </summary>
        [TestMethod]
        public void ShouldRaiseTimelineBarElementUpdatedWhenCallingToSetPosition()
        {
            var timelineBarElementUpdatedEventRaised = false;

            var presentationModel = this.CreatePresentationModel();
            presentationModel.TimelineBarElementUpdated += (sender, e) => timelineBarElementUpdatedEventRaised = true;

            presentationModel.SetPosition(TimeSpan.Zero);

            Assert.IsTrue(timelineBarElementUpdatedEventRaised);
        }

        /// <summary>
        /// Tests if an exception is being thrown if the MarkIn set is not valid
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InputValidationException))]
        public void ShouldThrowExceptionIfMarkInIsNotValid()
        {
            var presentationModel = this.CreatePresentationModel();

            presentationModel.MarkIn = -1;
        }

        /// <summary>
        /// Tests if an exception is being thrown if the MarkOut set is not valid
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InputValidationException))]
        public void ShouldThrowExceptionIfMarkOutIsNotValid()
        {
            var presentationModel = this.CreatePresentationModel();

            presentationModel.MarkOut = -1;
        }

        /// <summary>
        /// Tests if an exception is being thrown if the MarkIn set out of the valid range.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InputValidationException))]
        public void ShouldThrowExceptionIfMarkInIsNotInValidRange()
        {
            var presentationModel = this.CreatePresentationModel();

            presentationModel.MarkOut = 1;
            presentationModel.MarkIn = 2;
        }

        /// <summary>
        /// Tests if an exception is being thrown if the MarkOut set is out of the valid range
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InputValidationException))]
        public void ShouldThrowExceptionIfMarkOutIsNotInValidRange()
        {
            var presentationModel = this.CreatePresentationModel();

            presentationModel.MarkOut = 5;
            
            presentationModel.MarkIn = 2;

            presentationModel.MarkOut = 1;
        }

        /// <summary>
        /// Tests if values from the model are being set to the comment after executing SaveCommand.
        /// </summary>
        [TestMethod]
        public void ShouldSetValuesOnAdOpportunityWhenSaveCommandIsBeingExecuted()
        {
            var presentationModel = this.CreatePresentationModel();

            presentationModel.MarkOut = 12;
            presentationModel.MarkIn = 10;
            presentationModel.Text = "Test1";

            Assert.AreNotEqual(presentationModel.MarkOut, this.sequenceRegistry.CurrentSequence.CommentElements[0].MarkOut);
            Assert.AreNotEqual(presentationModel.MarkIn, this.sequenceRegistry.CurrentSequence.CommentElements[0].MarkIn);
            Assert.AreNotEqual(presentationModel.Text, this.sequenceRegistry.CurrentSequence.CommentElements[0].Text);

            presentationModel.SaveCommand.Execute("Test1");

            Assert.AreEqual(presentationModel.MarkOut, this.sequenceRegistry.CurrentSequence.CommentElements[0].MarkOut);
            Assert.AreEqual(presentationModel.MarkIn, this.sequenceRegistry.CurrentSequence.CommentElements[0].MarkIn);
            Assert.AreEqual(presentationModel.Text, this.sequenceRegistry.CurrentSequence.CommentElements[0].Text);
        }

        /// <summary>
        /// Tests if TimelineBarElement event is being raised after executing SaveCommand.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseTimelineBarElementEventWhenSaveCommandIsBeingExecuted()
        {
            var timelineBarElementUpdatedEventRaised = false;

            var presentationModel = this.CreatePresentationModel();
            presentationModel.TimelineBarElementUpdated += (sender, e) => timelineBarElementUpdatedEventRaised = true;

            presentationModel.SaveCommand.Execute(null);

            Assert.IsTrue(timelineBarElementUpdatedEventRaised);
        }

        /// <summary>
        /// Tests if Close method on View is being called after executing SaveCommand.
        /// </summary>
        [TestMethod]
        public void ShouldCallCloseOnViewWhenSaveCommandIsBeingExecuted()
        {
            var presentationModel = this.CreatePresentationModel();

            Assert.IsFalse(this.view.CloseCalled);

            presentationModel.SaveCommand.Execute(null);

            Assert.IsTrue(this.view.CloseCalled);
        }

        /// <summary>
        /// Tests if UpdateCommentDuration method on Preview is being called after executing SaveCommand.
        /// </summary>
        [TestMethod]
        public void ShouldCallUpdateCommentDurationOnPreviewWhenSaveCommandIsBeingExecuted()
        {
            var presentationModel = this.CreatePresentationModel();

            Assert.IsFalse(this.preview.UpdateCommentDurationCalled);

            presentationModel.SaveCommand.Execute(null);

            Assert.IsTrue(this.preview.UpdateCommentDurationCalled);
        }

        /// <summary>
        /// Tests if Deleting event is being raised after executing DeleteCommand.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseDeletingEventWhenDeleteCommandIsBeingExecuted()
        {
            var deletingEventRaised = false;

            var presentationModel = this.CreatePresentationModel();
            presentationModel.Deleting += (sender, e) => deletingEventRaised = true;

            presentationModel.DeleteCommand.Execute(null);

            Assert.IsTrue(deletingEventRaised);
        }

        /// <summary>
        /// Tests if Close method on View is being called after executing DeleteCommand.
        /// </summary>
        [TestMethod]
        public void ShouldCallCloseOnViewWhenDeleteCommandIsBeingExecuted()
        {
            var presentationModel = this.CreatePresentationModel();

            Assert.IsFalse(this.view.CloseCalled);

            presentationModel.DeleteCommand.Execute(null);

            Assert.IsTrue(this.view.CloseCalled);
        }

        /// <summary>
        /// Tests if the comment is being removed from timeline model after executing DeleteCommand.
        /// </summary>
        [TestMethod]
        public void ShouldRemoveAdOportunityFromProjectWhenDeleteCommandIsBeingExecuted()
        {
            var presentationModel = this.CreatePresentationModel();

            Assert.AreEqual(1, this.sequenceRegistry.CurrentSequence.CommentElements.Count);

            presentationModel.DeleteCommand.Execute(null);

            Assert.AreEqual(0, this.sequenceRegistry.CurrentSequence.CommentElements.Count);
        }

        /// <summary>
        /// Tests if Close method on View is being called after executing CloseCommand.
        /// </summary>
        [TestMethod]
        public void ShouldCallCloseOnViewWhenCloseCommandIsBeingExecuted()
        {
            var presentationModel = this.CreatePresentationModel();

            Assert.IsFalse(this.view.CloseCalled);

            presentationModel.CloseCommand.Execute(null);

            Assert.IsTrue(this.view.CloseCalled);
        }

        /// <summary>
        /// Should publish PlayComment event when PlayComment comamnd is executed
        /// </summary>
        [TestMethod]
        public void ShouldPublishPlayCommentEventWhenPlayCommentCommandIsExecuted()
        {
            var presentationModel = this.CreatePresentationModel();

            this.playCommentEvent.PublishCalled = false;
            this.playCommentEvent.PublishArgumentPayload = null;

            presentationModel.PlayCommand.Execute(null);

            Assert.IsTrue(this.playCommentEvent.PublishCalled);

            Assert.AreEqual(this.sequenceRegistry.CurrentSequence.CommentElements[0], this.playCommentEvent.PublishArgumentPayload);
        }

        /// <summary>
        /// Tests if Close method on View is being called after executing PlayComment command.
        /// </summary>
        [TestMethod]
        public void ShouldCallCloseOnViewWhenPlayCommentCommandIsBeingExecuted()
        {
            var presentationModel = this.CreatePresentationModel();

            Assert.IsFalse(this.view.CloseCalled);

            presentationModel.PlayCommand.Execute(null);

            Assert.IsTrue(this.view.CloseCalled);
        }

        /// <summary>
        /// Tests if the comment is being replaced when calling to set object.
        /// </summary>
        [TestMethod]
        public void ShouldReplaceTheCurrentCommentWhenCallingToSetObject()
        {
            var newComment = new Comment
                                  {
                                      MarkIn = 200,
                                      MarkOut = 250,
                                      Text = "Text"
                                  };

            var presentationModel = this.CreatePresentationModel();

            var oldComment = this.sequenceRegistry.CurrentSequence.CommentElements[0];

            this.sequenceRegistry.CurrentSequence.AddComment(newComment);

            presentationModel.SetElement(newComment, CommentMode.Timeline);

            Assert.AreEqual(1, this.sequenceRegistry.CurrentSequence.CommentElements.Count);
            Assert.AreNotEqual(oldComment, this.sequenceRegistry.CurrentSequence.CommentElements[0]);
            Assert.AreEqual(newComment, this.sequenceRegistry.CurrentSequence.CommentElements[0]);
        }

        /// <summary>
        /// Tests if the comment is being replaced when calling to set object.
        /// </summary>
        [TestMethod]
        public void ShouldReplaceTheCurrentValuesWhenCallingToSetObject()
        {
            var newComment = new Comment
            {
                MarkIn = 200,
                MarkOut = 250,
                Text = "Text"
            };

            var presentationModel = this.CreatePresentationModel();

            var oldComment = this.sequenceRegistry.CurrentSequence.CommentElements[0];

            this.sequenceRegistry.CurrentSequence.AddComment(newComment);

            presentationModel.SetElement(newComment, CommentMode.Timeline);

            Assert.AreEqual(newComment.MarkIn, presentationModel.MarkIn);
            Assert.AreEqual(newComment.MarkOut, presentationModel.MarkOut);
            Assert.AreEqual(newComment.Text, presentationModel.Text);
        }

        /// <summary>
        /// Tests if TimelineBarElement event is being raised after calling to SetElement.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseTimelineBarElementEventWhenSetObjectIsCalled()
        {
            var newComment = new Comment
            {
                MarkIn = 200,
                MarkOut = 250,
                Text = "Text"
            };

            var timelineBarElementUpdatedEventRaised = false;

            var presentationModel = this.CreatePresentationModel();
            presentationModel.TimelineBarElementUpdated += (sender, e) => timelineBarElementUpdatedEventRaised = true;

            presentationModel.SetElement(newComment, CommentMode.Timeline);

            Assert.IsTrue(timelineBarElementUpdatedEventRaised);
        }

        /// <summary>
        /// Tests if Close method on View is being called after calling to SetElement.
        /// </summary>
        [TestMethod]
        public void ShouldCallCloseOnViewWhenSetObjectIsCalled()
        {
            var newComment = new Comment
            {
                MarkIn = 200,
                MarkOut = 250,
                Text = "Text"
            };

            var presentationModel = this.CreatePresentationModel();

            Assert.IsFalse(this.view.CloseCalled);

            presentationModel.SetElement(newComment, CommentMode.Timeline);

            Assert.IsTrue(this.view.CloseCalled);
        }

        /// <summary>
        /// Creates the <see cref="CommentEditBoxPresentationModel"/> using mocked dependencies.
        /// </summary>
        /// <returns>An <seealso cref="CommentEditBoxPresentationModel"/> instance.</returns>
        private ICommentEditBoxPresentationModel CreatePresentationModel()
        {
            return new CommentEditBoxPresentationModel(this.view, this.preview, this.eventAggregator, this.sequenceRegistry, this.configurationService);
        }
    }
}
