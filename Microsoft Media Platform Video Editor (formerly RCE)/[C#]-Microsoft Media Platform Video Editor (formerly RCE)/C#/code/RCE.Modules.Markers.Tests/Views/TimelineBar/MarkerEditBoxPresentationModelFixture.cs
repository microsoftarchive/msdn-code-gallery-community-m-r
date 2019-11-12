// <copyright file="MarkerEditBoxPresentationModelFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MarkerEditBoxPresentationModelFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Markers.Tests.Views.TimelineBar
{
    using System;
    using Infrastructure.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;

    [TestClass]
    public class MarkerEditBoxPresentationModelFixture
    {
        private MockMarkerEditBox view;

        private MockMarkerViewPreview preview;

        private MockSequenceRegistry sequenceRegistry;

        private MockSequenceModel sequenceModel;

        private Sequence sequence;

        /// <summary>
        /// Initilize the local data.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.view = new MockMarkerEditBox();
            this.preview = new MockMarkerViewPreview();
            this.sequenceRegistry = new MockSequenceRegistry();
            this.sequenceModel = new MockSequenceModel();
            this.sequence = new Sequence();
            this.sequenceRegistry.CurrentSequence = this.sequence;

            this.sequenceRegistry.CurrentSequenceModel = this.sequenceModel;
        }

        /// <summary>
        /// Tests if the <see cref="MarkerEditBoxPresentationModel"/> initilizes the view and the preview.
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
        /// Tests that a marker is being added to the project.
        /// </summary>
        [TestMethod]
        public void ShouldAddMarkerToCurrentSequence()
        {
            Assert.AreEqual(0, this.sequence.Markers.Count);
            
            var presentationModel = this.CreatePresentationModel();

            Assert.AreEqual(1, this.sequence.Markers.Count);
        }

        /// <summary>
        /// Tests that values are being set on the marker.
        /// </summary>
        [TestMethod]
        public void ShouldSetValuesToMarker()
        {
            Assert.AreEqual(0, this.sequence.Markers.Count);

            var presentationModel = this.CreatePresentationModel();

            Assert.AreEqual(1, this.sequence.Markers.Count);
            Assert.AreEqual(presentationModel.Text, this.sequence.Markers[0].Text);
            Assert.AreEqual(0, this.sequence.Markers[0].Time);
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

            presentationModel.Text = "Test1";

            Assert.IsTrue(propertyChangedRaised);
            Assert.AreEqual("Text", propertyChangedEventArgsArgument);
        }

        /// <summary>
        /// Tests if OnPropertyChanged event is raised when Time property is set.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseOnPropertyChangedEventWhenTimeIsUpdated()
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

            presentationModel.Time = 10;

            Assert.IsTrue(propertyChangedRaised);
            Assert.AreEqual("Time", propertyChangedEventArgsArgument);
        }

        /// <summary>
        /// Tests if the position returns the correct time.
        /// </summary>
        [TestMethod]
        public void ShouldReturnCurrentTimeWhenUsingPositionProperty()
        {
            var presentationModel = this.CreatePresentationModel();

            presentationModel.Time = 10;

            Assert.AreEqual(presentationModel.Time, presentationModel.Position);
        }

        /// <summary>
        /// Tests if values from the marker are being set to the model after calling ShowEditBox
        /// </summary>
        [TestMethod]
        public void ShouldSetValuesOnModelWhenCallToShowEditBox()
        {
            var presentationModel = this.CreatePresentationModel();

            this.sequence.Markers[0].Time = TimeSpan.FromSeconds(10).Ticks;
            this.sequence.Markers[0].Text = "Test1";

            Assert.AreNotEqual(this.sequence.Markers[0].Time, presentationModel.Time);
            Assert.AreNotEqual(this.sequence.Markers[0].Text, presentationModel.Text);

            presentationModel.ShowEditBox();

            Assert.AreEqual(TimeSpan.FromTicks(this.sequence.Markers[0].Time).TotalSeconds, presentationModel.Time);
            Assert.AreEqual(this.sequence.Markers[0].Text, presentationModel.Text);
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
        /// Tests if Time is being set after calling to SetPosition
        /// </summary>
        [TestMethod]
        public void ShouldSetTimeWhenCallingToSetPosition()
        {
            var position = TimeSpan.FromSeconds(155);

            var presentationModel = this.CreatePresentationModel();

            Assert.AreNotEqual(position.TotalSeconds, presentationModel.Time);
            
            presentationModel.SetPosition(position);

            Assert.AreEqual(position.TotalSeconds, presentationModel.Time);
        }

        /// <summary>
        /// Tests if marker  time is being set after calling to SetPosition
        /// </summary>
        [TestMethod]
        public void ShouldSetMarkerTimeWhenCallingToSetPosition()
        {
            var position = TimeSpan.FromSeconds(155);

            var presentationModel = this.CreatePresentationModel();

            Assert.AreNotEqual(this.sequence.Markers[0].Time, position.Ticks);

            presentationModel.SetPosition(position);

            Assert.AreEqual(this.sequence.Markers[0].Time, position.Ticks);
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
        /// Tests if an exception is being thrown if the Time set is not valid
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InputValidationException))]
        public void ShouldThrowExceptionIfTimeIsNotValid()
        {
            var presentationModel = this.CreatePresentationModel();

            presentationModel.Time = -1;
        }

        /// <summary>
        /// Tests if values from the model are being set to the marker after executing SaveCommand.
        /// </summary>
        [TestMethod]
        public void ShouldSetValuesOnMarkerWhenSaveCommandIsBeingExecuted()
        {
            var presentationModel = this.CreatePresentationModel();

            presentationModel.Time = TimeSpan.FromSeconds(10).Ticks;
            presentationModel.Text = "Test1";

            Assert.AreNotEqual(presentationModel.Time, TimeSpan.FromTicks(this.sequence.Markers[0].Time).TotalSeconds);
            Assert.AreNotEqual(presentationModel.Text, this.sequence.Markers[0].Text);

            presentationModel.SaveCommand.Execute(null);

            Assert.AreEqual(presentationModel.Time, TimeSpan.FromTicks(this.sequence.Markers[0].Time).TotalSeconds);
            Assert.AreEqual(presentationModel.Text, this.sequence.Markers[0].Text);
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
        /// Tests if the marker is being removed from project after executing DeleteCommand.
        /// </summary>
        [TestMethod]
        public void ShouldRemoveMarkerFromProjectWhenDeleteCommandIsBeingExecuted()
        {
            var presentationModel = this.CreatePresentationModel();

            Assert.AreEqual(1, this.sequence.Markers.Count);

            presentationModel.DeleteCommand.Execute(null);

            Assert.AreEqual(0, this.sequence.Markers.Count);
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
        /// Creates the <see cref="MarkerEditBoxPresentationModel"/> using mocked dependencies.
        /// </summary>
        /// <returns>An <seealso cref="MarkerEditBoxPresentationModel"/> instance.</returns>
        private IMarkerEditBoxPresentationModel CreatePresentationModel()
        {
            return new MarkerEditBoxPresentationModel(this.view, this.preview, this.sequenceRegistry);
        }
    }
}