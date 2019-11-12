// <copyright file="SequencesViewModelFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: SequencesViewModelFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Timeline.Tests.Views
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RCE.Infrastructure.Models;
    using RCE.Modules.Timeline.Tests.Mocks;
    using RCE.Modules.Timeline.Views;

    [TestClass]
    public class SequencesViewModelFixture
    {
        private MockSequencesView view;

        private MockProjectService projectService;

        private Project project;

        private MockSequenceRegistry sequenceRegistry;

        [TestInitialize]
        public void TestInitialize()
        {
            this.view = new MockSequencesView();
            this.projectService = new MockProjectService();
            this.sequenceRegistry = new MockSequenceRegistry();
            this.project = new Project();
            this.projectService.GetCurrentProjectReturnValue = this.project;
        }

        [TestMethod]
        public void ShouldSetViewParameterAsView()
        {
            var viewModel = this.CreateViewModel();

            Assert.AreSame(this.view, viewModel.View);
        }

        [TestMethod]
        public void ShouldPassViewModelAsViewModelParameter()
        {
            var viewModel = this.CreateViewModel();

            Assert.AreSame(viewModel, this.view.SetViewModelParameter);
        }
        
        [TestMethod]
        public void ShouldSetSequencesToProjectTimelineCollection()
        {
            var viewModel = this.CreateViewModel();

            Assert.AreSame(this.project.Timelines, viewModel.Sequences);
        }

        [TestMethod]
        public void ShouldChangeCurrentSequenceCommandToSelectedSequenceWhenChangeCurrentSequenceCommandIsExecuted()
        {
            var viewModel = this.CreateViewModel();

            Sequence sequence = new Sequence();

            viewModel.SelectedSequence = sequence;
            SequenceModel sequenceModel = new SequenceModel(new MockEventAggregator());

            this.sequenceRegistry.SequenceForSequenceModel = sequenceModel;

            Assert.IsNull(this.sequenceRegistry.CurrentSequenceModel);

            viewModel.ChangeCurrentSequenceCommand.Execute(null);

            Assert.AreSame(sequenceModel, this.sequenceRegistry.CurrentSequenceModel);
        }

        private SequencesViewModel CreateViewModel()
        {
            return new SequencesViewModel(this.view, this.projectService, this.sequenceRegistry);
        }
    }
}