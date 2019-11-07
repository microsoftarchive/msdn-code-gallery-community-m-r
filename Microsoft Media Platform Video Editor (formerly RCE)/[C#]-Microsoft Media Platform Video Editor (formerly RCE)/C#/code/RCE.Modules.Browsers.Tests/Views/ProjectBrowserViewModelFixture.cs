// <copyright file="ProjectBrowserViewModelFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ProjectBrowserViewModelFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Browsers.Tests.Views
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RCE.Infrastructure.Events;
    using RCE.Infrastructure.Models;
    using RCE.Modules.Browsers.Tests.Mocks;
    using RCE.Modules.Browsers.Views;

    [TestClass]
    public class ProjectBrowserViewModelFixture
    {
        private MockProjectBrowserView view;

        private MockProjectService projectService;

        private MockSequenceRegistry sequenceRegistry;

        private MockEventAggregator eventAggregator;

        private Project project;

        private MockStatusEvent statusEvent;

        private MockSaveProjectEvent saveProjectEvent;

        private MockResetWindowsEvent resetWindowsEvent;

        private MockConfigurationService configurationService;

        [TestInitialize]
        public void TestInitialize()
        {
            this.view = new MockProjectBrowserView();
            this.projectService = new MockProjectService();
            this.sequenceRegistry = new MockSequenceRegistry();
            this.eventAggregator = new MockEventAggregator();
            this.statusEvent = new MockStatusEvent();
            this.resetWindowsEvent = new MockResetWindowsEvent();
            this.saveProjectEvent = new MockSaveProjectEvent();
            this.configurationService = new MockConfigurationService();

            this.eventAggregator.AddMapping<SaveProjectEvent>(this.saveProjectEvent);
            this.eventAggregator.AddMapping<StatusEvent>(this.statusEvent);
            this.eventAggregator.AddMapping<ResetWindowsEvent>(this.resetWindowsEvent);

            this.saveProjectEvent.WasPublished = false;

            this.project = new Project();
            this.projectService.GetCurrentProjectReturnValue = this.project;
        }

        [TestMethod]
        public void ShouldUseViewPassedThroughConstructor()
        {
            var viewModel = this.CreateViewModel();

            Assert.AreEqual(this.view, viewModel.View);
        }

        [TestMethod]
        public void ShouldCallSetDataContextInView()
        {
            var viewModel = this.CreateViewModel();

            Assert.AreSame(this.view.SetDataContextParameter, viewModel);
        }

        [TestMethod]
        public void ShoulUpdateTitleWithProjectName()
        {
            Project project = new Project { Name = "Test Project" };

            this.projectService.GetCurrentProjectReturnValue = project;

            var viewModel = this.CreateViewModel();

            Assert.AreEqual("Project: Test Project", viewModel.Title);
        }

        [TestMethod]
        public void ShouldRaiseTitleUpdateWhenProjectIsRetrieved()
        {
            var viewModel = this.CreateViewModel();

            bool raised = false;

            viewModel.TitleUpdated += (s, a) => { raised = true; };

            Assert.IsFalse(raised);
            
            this.projectService.InvokeProjectRetrieved();

            Assert.IsTrue(raised);
        }

        [TestMethod]
        public void ShouldRaiseTitleUpdateWhenProjectIsSaved()
        {
            var viewModel = this.CreateViewModel();

            bool raised = false;

            viewModel.TitleUpdated += (s, a) => { raised = true; };

            Assert.IsFalse(raised);

            this.projectService.InvokeProjectSaved();

            Assert.IsTrue(raised);
        }

        [TestMethod]
        public void ShouldCallCreateTimelineWhenNewSequenceCommandIsExecuted()
        {
            var viewModel = this.CreateViewModel();

            Assert.IsFalse(this.projectService.TimelineCreated);

            viewModel.NewSequenceCommand.Execute(null);

            Assert.IsTrue(this.projectService.TimelineCreated);
        }

        [TestMethod]
        public void ShouldCallCreateSequencePassingCreatedTimelineWhenNewSequenceCommandIsExecuted()
        {
            var viewModel = this.CreateViewModel();

            Assert.IsNull(this.sequenceRegistry.CreateSequenceParameter);

            viewModel.NewSequenceCommand.Execute(null);

            Assert.AreSame(this.projectService.CreatedSequence, this.sequenceRegistry.CreateSequenceParameter);
        }

        [TestMethod]
        public void ShouldAddCreatedTimelineToTheProject()
        {
            var viewModel = this.CreateViewModel();

            Assert.AreEqual(0, this.project.Timelines.Count);

            viewModel.NewSequenceCommand.Execute(null);

            Assert.AreEqual(1, this.project.Timelines.Count);
            Assert.AreSame(this.projectService.CreatedSequence, this.project.Timelines[0]);
        }

        [TestMethod]
        public void ShouldCallSaveProjectWhenSaveProjectCommandIsExecuted()
        {
            var viewModel = this.CreateViewModel();

            Assert.IsFalse(this.projectService.SaveCalled);

            viewModel.SaveProjectCommand.Execute(null);

            Assert.IsTrue(this.saveProjectEvent.WasPublished);
        }

        [TestMethod]
        public void ShouldSubscribeToStatusEventInConstructor()
        {
            Assert.IsNull(this.statusEvent.SubscribeArgumentAction);

            var viewModel = this.CreateViewModel();

            Assert.IsNotNull(this.statusEvent.SubscribeArgumentAction);
        }

        [TestMethod]
        public void ShouldChangedStatusPropertyWhenUpdatingStatus()
        {
            var viewModel = this.CreateViewModel();

            Assert.IsTrue(string.IsNullOrEmpty(viewModel.Status));

            viewModel.UpdateStatus("New status");

            Assert.AreEqual("New status", viewModel.Status);
        }

        [TestMethod]
        public void ShouldNotifyAboutChangedPropertyWhenStatusChanges()
        {
            var viewModel = this.CreateViewModel();

            bool wasCalled = false;

            viewModel.PropertyChanged += 
                (s, a) => 
                {
                    if (a.PropertyName == "Status")
                    {
                        wasCalled = true;
                    }
                };

            Assert.IsFalse(wasCalled);

            viewModel.UpdateStatus("New status");

            Assert.IsTrue(wasCalled);
        }

        private ProjectBrowserViewModel CreateViewModel()
        {
            return new ProjectBrowserViewModel(this.view, this.projectService, this.sequenceRegistry, this.eventAggregator, () => new MockErrorView(), this.configurationService);
        }
    }
}