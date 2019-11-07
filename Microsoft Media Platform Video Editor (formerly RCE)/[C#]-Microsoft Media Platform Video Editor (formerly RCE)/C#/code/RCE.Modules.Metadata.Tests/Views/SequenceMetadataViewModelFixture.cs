// <copyright file="SequenceMetadataViewModelFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: SequenceMetadataViewModelFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Metadata.Tests.Views
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RCE.Infrastructure.Events;
    using RCE.Modules.Metadata.Tests.Mocks;
    using RCE.Modules.Metadata.Views;

    using SMPTETimecode;

    [TestClass]
    public class SequenceMetadataViewModelFixture
    {
        private MockSequenceMetadataView sequenceMetadataView;

        private MockSequenceRegistry sequenceRegistry;

        private MockEventAggregator eventAggregator;

        private MockDisplayMarkerBrowserWindowEvent displayMarkerBrowserWindowEvent;

        [TestInitialize]
        public void SetUp()
        {
            this.sequenceMetadataView = new MockSequenceMetadataView();
            this.sequenceRegistry = new MockSequenceRegistry();
            this.eventAggregator = new MockEventAggregator();
            this.displayMarkerBrowserWindowEvent = new MockDisplayMarkerBrowserWindowEvent();
            this.eventAggregator.AddMapping<DisplayMarkerBrowserWindowEvent>(this.displayMarkerBrowserWindowEvent);
        }

        [TestMethod]
        public void ShouldSetView()
        {
            var viewModel = this.CreateViewModel();
            Assert.AreSame(this.sequenceMetadataView, viewModel.View);
        }

        [TestMethod]
        public void ShouldSetViewModelForView()
        {
            var viewModel = this.CreateViewModel();
            Assert.AreSame(viewModel, this.sequenceMetadataView.SetViewModelParameter);
        }

        [TestMethod]
        public void ShouldSetDurationToSequenceModelDuration()
        {
            this.sequenceRegistry.CurrentSequenceModel = new MockSequenceModel();
            this.sequenceRegistry.CurrentSequenceModel.Duration = TimeCode.FromSeconds(10.0, SmpteFrameRate.Smpte2997NonDrop);
            var viewModel = this.CreateViewModel();
            Assert.AreEqual(this.sequenceRegistry.CurrentSequenceModel.Duration, viewModel.Duration);
        }

        [TestMethod]
        public void ShouldRaisePropertyChangeForDurationWhenCurrentSequenceModelDurationPropertyChanges()
        {
            var sequenceModel = new MockSequenceModel();
            this.sequenceRegistry.CurrentSequenceModel = sequenceModel;

            var viewModel = this.CreateViewModel();
            bool wasRaised = false;
            viewModel.PropertyChanged += (s, a) =>
                {
                    if (a.PropertyName == "Duration")
                    {
                        wasRaised = true;
                    }
                };

            Assert.IsFalse(wasRaised);

            sequenceModel.InvokePropertyChanged("NotRaise");
            
            Assert.IsFalse(wasRaised);
            
            sequenceModel.InvokePropertyChanged("Duration");

            Assert.IsTrue(wasRaised);
        }

        [TestMethod]
        public void ShouldPublishDisplayCommentsViewEventWhenExecutingDisplayCommentsCommand()
        {
            var viewModel = this.CreateViewModel();
            Assert.IsFalse(this.displayMarkerBrowserWindowEvent.PublishCalled);
            viewModel.DisplayCommentsCommand.Execute(null);
            Assert.IsTrue(this.displayMarkerBrowserWindowEvent.PublishCalled);
        }

        public SequenceMetadataViewModel CreateViewModel()
        {
            return new SequenceMetadataViewModel(this.sequenceMetadataView, this.sequenceRegistry, this.eventAggregator, new MockRegionManager());
        }
    }
}