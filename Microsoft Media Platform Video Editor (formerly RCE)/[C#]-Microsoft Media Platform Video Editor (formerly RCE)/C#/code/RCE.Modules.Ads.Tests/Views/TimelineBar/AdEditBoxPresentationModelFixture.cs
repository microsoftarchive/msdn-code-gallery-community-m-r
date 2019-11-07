// <copyright file="AdEditBoxPresentationModelFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: AdEditBoxPresentationModelFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Ads.Tests.Views.TimelineBar
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RCE.Infrastructure.Models;
    using RCE.Modules.Ads.Tests.Mocks;

    [TestClass]
    public class AdEditBoxPresentationModelFixture
    {
        private MockAdEditBox view;

        private MockAdViewPreview preview;

        private MockConfigurationService configurationService;
        
        private MockSequenceRegistry sequenceRegistry;

        private MockSequenceModel sequenceModel;

        private Sequence sequence;

        /// <summary>
        /// Initilize the local data.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.view = new MockAdEditBox();
            this.preview = new MockAdViewPreview();
            this.configurationService = new MockConfigurationService();
            this.sequenceRegistry = new MockSequenceRegistry();
            this.sequenceModel = new MockSequenceModel();
            this.sequence = new Sequence();
            this.sequenceRegistry.CurrentSequence = this.sequence;

            this.sequenceRegistry.CurrentSequenceModel = this.sequenceModel;
        }

        /// <summary>
        /// Tests if the <see cref="AdEditBoxPresentationModel"/> initilizes the view and the preview.
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
        /// Tests that an ad opportunity is being added to the sequence.
        /// </summary>
        [TestMethod]
        public void ShouldAddAdOpportunityToCurrentSequence()
        {
            Assert.AreEqual(0, this.sequenceRegistry.CurrentSequence.AdOpportunities.Count);
            
            var presentationModel = this.CreatePresentationModel();

            Assert.AreEqual(1, this.sequenceRegistry.CurrentSequence.AdOpportunities.Count);
        }

        /// <summary>
        /// Tests that values are being set on the ad opportunity.
        /// </summary>
        [TestMethod]
        public void ShouldSetValuesToAdOpportunity()
        {
            var templateTypes = "Test1;Test2";
            this.configurationService.GetParameterValueReturnFunction = parameter => parameter == "TemplateTypes" ? templateTypes : null;

            Assert.AreEqual(0, this.sequenceRegistry.CurrentSequence.AdOpportunities.Count);

            var presentationModel = this.CreatePresentationModel();

            Assert.AreEqual(1, this.sequenceRegistry.CurrentSequence.AdOpportunities.Count);
            Assert.AreEqual(presentationModel.SelectedTemplateType, this.sequenceRegistry.CurrentSequence.AdOpportunities[0].TemplateType);
            Assert.AreEqual(0, this.sequenceRegistry.CurrentSequence.AdOpportunities[0].Time);
        }

        /// <summary>
        /// Tests if the template types are being set.
        /// </summary>
        [TestMethod]
        public void ShouldSetTemplateTypes()
        {
            var templateTypes = "Test1;Test2";
            this.configurationService.GetParameterValueReturnFunction = parameter => parameter == "TemplateTypes" ? templateTypes : null;
            
            var presentationModel = this.CreatePresentationModel();

            Assert.AreEqual(2, presentationModel.TemplateTypes.Count);
            Assert.AreEqual("Test1", presentationModel.TemplateTypes[0]);
            Assert.AreEqual("Test2", presentationModel.TemplateTypes[1]);
        }

        /// <summary>
        /// Tests if the selecte template type is being set.
        /// </summary>
        [TestMethod]
        public void ShouldSetSelectedTemplateType()
        {
            var templateTypes = "Test1;Test2";
            this.configurationService.GetParameterValueReturnFunction = parameter => parameter == "TemplateTypes" ? templateTypes : null;

            var presentationModel = this.CreatePresentationModel();

            Assert.AreEqual(presentationModel.TemplateTypes[0], presentationModel.SelectedTemplateType);
        }

        /// <summary>
        /// Tests if OnPropertyChanged event is raised when SelectedTemplateType property is set.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseOnPropertyChangedEventWhenSelectedTemplateTypeIsUpdated()
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

            presentationModel.SelectedTemplateType = "Test1";

            Assert.IsTrue(propertyChangedRaised);
            Assert.AreEqual("SelectedTemplateType", propertyChangedEventArgsArgument);
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
        /// Tests if values from the ad opportunity are being set to the model after calling ShowEditBox
        /// </summary>
        [TestMethod]
        public void ShouldSetValuesOnModelWhenCallToShowEditBox()
        {
            var presentationModel = this.CreatePresentationModel();

            this.sequenceRegistry.CurrentSequence.AdOpportunities[0].Time = TimeSpan.FromSeconds(10).Ticks;
            this.sequenceRegistry.CurrentSequence.AdOpportunities[0].TemplateType = "Test1";

            Assert.AreNotEqual(this.sequenceRegistry.CurrentSequence.AdOpportunities[0].Time, presentationModel.Time);
            Assert.AreNotEqual(this.sequenceRegistry.CurrentSequence.AdOpportunities[0].TemplateType, presentationModel.SelectedTemplateType);

            presentationModel.ShowEditBox();

            Assert.AreEqual(TimeSpan.FromTicks(this.sequenceRegistry.CurrentSequence.AdOpportunities[0].Time).TotalSeconds, presentationModel.Time);
            Assert.AreEqual(this.sequenceRegistry.CurrentSequence.AdOpportunities[0].TemplateType, presentationModel.SelectedTemplateType);
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
        /// Tests if Ad Opportunity  time is being set after calling to SetPosition
        /// </summary>
        [TestMethod]
        public void ShouldSetAdOpportunityTimeWhenCallingToSetPosition()
        {
            var position = TimeSpan.FromSeconds(155);

            var presentationModel = this.CreatePresentationModel();

            Assert.AreNotEqual(this.sequenceRegistry.CurrentSequence.AdOpportunities[0].Time, position.Ticks);

            presentationModel.SetPosition(position);

            Assert.AreEqual(this.sequenceRegistry.CurrentSequence.AdOpportunities[0].Time, position.Ticks);
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
        /// Tests if values from the model are being set to the ad opportunity after executing SaveCommand.
        /// </summary>
        [TestMethod]
        public void ShouldSetValuesOnAdOpportunityWhenSaveCommandIsBeingExecuted()
        {
            var presentationModel = this.CreatePresentationModel();

            presentationModel.Time = TimeSpan.FromSeconds(10).Ticks;
            presentationModel.SelectedTemplateType = "Test1";

            Assert.AreNotEqual(presentationModel.Time, TimeSpan.FromTicks(this.sequenceRegistry.CurrentSequence.AdOpportunities[0].Time).TotalSeconds);
            Assert.AreNotEqual(presentationModel.SelectedTemplateType, this.sequenceRegistry.CurrentSequence.AdOpportunities[0].TemplateType);

            presentationModel.SaveCommand.Execute(null);

            Assert.AreEqual(presentationModel.Time, TimeSpan.FromTicks(this.sequenceRegistry.CurrentSequence.AdOpportunities[0].Time).TotalSeconds);
            Assert.AreEqual(presentationModel.SelectedTemplateType, this.sequenceRegistry.CurrentSequence.AdOpportunities[0].TemplateType);
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
        /// Tests if the ad opportunity is being removed from project after executing DeleteCommand.
        /// </summary>
        [TestMethod]
        public void ShouldRemoveAdOportunityFromSequencetWhenDeleteCommandIsBeingExecuted()
        {
            var presentationModel = this.CreatePresentationModel();

            Assert.AreEqual(1, this.sequenceRegistry.CurrentSequence.AdOpportunities.Count);

            presentationModel.DeleteCommand.Execute(null);

            Assert.AreEqual(0, this.sequenceRegistry.CurrentSequence.AdOpportunities.Count);
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
        /// Creates the <see cref="AdEditBoxPresentationModel"/> using mocked dependencies.
        /// </summary>
        /// <returns>An <seealso cref="AdEditBoxPresentationModel"/> instance.</returns>
        private IAdEditBoxPresentationModel CreatePresentationModel()
        {
            return new AdEditBoxPresentationModel(this.view, this.preview, this.configurationService, this.sequenceRegistry);
        }
    }
}
