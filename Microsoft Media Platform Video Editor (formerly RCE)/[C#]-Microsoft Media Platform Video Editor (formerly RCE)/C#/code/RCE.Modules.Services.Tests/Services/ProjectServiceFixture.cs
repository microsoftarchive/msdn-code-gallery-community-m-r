// <copyright file="ProjectServiceFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ProjectServiceFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Services.Tests.Services
{
    using System;
    using System.Linq;
    using Infrastructure;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;

    using RCE.Infrastructure.Services;

    using SMPTETimecode;
    using Project = RCE.Infrastructure.Models.Project;

    /// <summary>
    /// Test class for <see cref="ProjectService"/>.
    /// </summary>
    [TestClass]
    public class ProjectServiceFixture
    {
        /// <summary>
        /// Mock for <see cref="dataServiceFacade"/>.
        /// </summary>
        private MockDataServiceFacade dataServiceFacade;

        /// <summary>
        /// Mock for <see cref="IConfigurationService"/>.
        /// </summary>
        private MockConfigurationService configurationService;

        /// <summary>
        /// Mock for <see cref="IErrorView"/>
        /// </summary>
        private MockErrorView errorView;

        /// <summary>
        /// Mock for <see cref="ILogger"/>
        /// </summary>
        private MockLoggerFacade logger;

        /// <summary>
        /// Sets up test start configurations.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.dataServiceFacade = new MockDataServiceFacade();
            this.configurationService = new MockConfigurationService();
            this.errorView = new MockErrorView();
            this.logger = new MockLoggerFacade();
        }

        /// <summary>
        /// Tests if <see cref="IConfigurationService"/> calls GetProjectId while initilization.
        /// </summary>
        [TestMethod]
        public void ShouldCallToGetProjectIdOnConfigurationService()
        {
            bool getProjectId = false;
            this.configurationService.GetParameterValueReturnFunction = parameter =>
            {
                if (parameter == "ProjectId")
                {
                    getProjectId = true;
                }

                return string.Empty;
            };

            var projectService = this.CreateProjectService();

            Assert.IsTrue(getProjectId);
        }

        /// <summary>
        /// Tests that a new project is returned if the project id is null.
        /// </summary>
        [TestMethod]
        public void ShouldGetANewProjectIfGetProjectIdIsNull()
        {
            this.configurationService.GetParameterValueReturnFunction = parameter =>
            {
                if (parameter == "ProjectId")
                {
                    return null;
                }

                if (parameter == "UserName")
                {
                    return "test";
                }

                return string.Empty;
            };

            var projectService = this.CreateProjectService();
            this.dataServiceFacade.InvokeLoadProjectCompleted(null);

            Assert.IsNotNull(projectService.GetCurrentProject());
            Assert.IsNotNull(projectService.GetCurrentProject().Name);
            Assert.AreEqual(this.configurationService.GetUserName(), projectService.GetCurrentProject().Creator);
            Assert.AreNotEqual(DateTime.MinValue, projectService.GetCurrentProject().Created);
            Assert.AreEqual(SmpteFrameRate.Smpte2997NonDrop, projectService.GetCurrentProject().SmpteFrameRate);
            Assert.AreEqual(TimeCode.FromAbsoluteTime(0, SmpteFrameRate.Smpte2997NonDrop), projectService.GetCurrentProject().StartTimeCode);
            Assert.AreEqual(false, projectService.GetCurrentProject().RippleMode);
            Assert.AreEqual(180, projectService.GetCurrentProject().AutoSaveInterval);
            Assert.AreEqual(3, projectService.GetCurrentProject().Timelines[0].Tracks.Count);
        }

        /// <summary>
        /// Tests that a new project is returned if the project id is null.
        /// </summary>
        [TestMethod]
        public void ShouldGetAddMaxNumberOfAudioTracks()
        {
            this.configurationService.GetParameterValueReturnFunction = parameter =>
            {
                if (parameter == "ProjectId")
                {
                    return null;
                }

                if (parameter == "UserName")
                {
                    return "test";
                }

                if (parameter == "MaxNumberOfAudioTracks")
                {
                    return "5";
                }

                return string.Empty;
            };

            var projectService = this.CreateProjectService();
            this.dataServiceFacade.InvokeLoadProjectCompleted(null);

            Assert.IsNotNull(projectService.GetCurrentProject());
            Assert.IsNotNull(projectService.GetCurrentProject().Name);
            Assert.AreEqual(this.configurationService.GetUserName(), projectService.GetCurrentProject().Creator);
            Assert.AreNotEqual(DateTime.MinValue, projectService.GetCurrentProject().Created);
            Assert.AreEqual(SmpteFrameRate.Smpte2997NonDrop, projectService.GetCurrentProject().SmpteFrameRate);
            Assert.AreEqual(TimeCode.FromAbsoluteTime(0, SmpteFrameRate.Smpte2997NonDrop), projectService.GetCurrentProject().StartTimeCode);
            Assert.AreEqual(false, projectService.GetCurrentProject().RippleMode);
            Assert.AreEqual(180, projectService.GetCurrentProject().AutoSaveInterval);
            Assert.AreEqual(7, projectService.GetCurrentProject().Timelines[0].Tracks.Count);
            Assert.AreEqual(5, projectService.GetCurrentProject().Timelines[0].Tracks.Where(x => x.TrackType == RCE.Infrastructure.Models.TrackType.Audio).Count());
        }

        /// <summary>
        /// Tests that the project is saved while getting the state of the project.
        /// </summary>
        [TestMethod]
        public void ShouldCallToSaveProjectAsyncIfProjectStateIsRetrieved()
        {
            var projectService = this.CreateProjectService();

            this.dataServiceFacade.InvokeLoadProjectCompleted(null);

            Assert.AreEqual(ProjectState.Retrieved, projectService.State);

            Assert.IsFalse(this.dataServiceFacade.SaveProjectAsyncCalled);

            projectService.SaveProject();

            Assert.IsTrue(this.dataServiceFacade.SaveProjectAsyncCalled);
        }

        /// <summary>
        /// Tests that the project service starts loading the project if the project id is null.
        /// </summary>
        [TestMethod]
        public void ShouldCallToLoadProjectAsyncIfGetProjectIdIsNull()
        {
            this.configurationService.GetParameterValueReturnFunction = parameter => null;

            Assert.IsFalse(this.dataServiceFacade.LoadProjectAsyncCalled);

            var projectService = this.CreateProjectService();

            Assert.IsTrue(this.dataServiceFacade.LoadProjectAsyncCalled);
        }

        /// <summary>
        /// Tests that the project service starts loading the project if the project id is 
        /// not null.
        /// </summary>
        [TestMethod]
        public void ShouldCallToLoadProjectAsyncIfGetProjectIdIsNotNull()
        {
            this.configurationService.GetParameterValueReturnFunction = parameter => parameter == "ProjectId" ? "http://test/" : null;

            Assert.IsFalse(this.dataServiceFacade.LoadProjectAsyncCalled);

            var projectService = this.CreateProjectService();

            Assert.IsTrue(this.dataServiceFacade.LoadProjectAsyncCalled);
        }

        /// <summary>
        /// Tests if the current project is set when the project has been loaded.
        /// </summary>
        [TestMethod]
        public void ShouldSetCurrentProjectWhenInvokingLoadProjectCompleted()
        {
            var project = new Project();

            this.configurationService.GetParameterValueReturnFunction = parameter => parameter == "ProjectId" ? "http://test/" : null;

            var projectService = this.CreateProjectService();

            Assert.AreNotEqual(project, projectService.GetCurrentProject());

            this.dataServiceFacade.InvokeLoadProjectCompleted(project);

            Assert.AreEqual(project, projectService.GetCurrentProject());
        }

        /// <summary>
        /// Tests that the ProjectSaving event is raised when saving a project.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseProjectSavingEventWhenSavingAProject()
        {
            var saving = false;

            var projectService = this.CreateProjectService();

            this.dataServiceFacade.InvokeLoadProjectCompleted(null);
            
            projectService.ProjectSaving += (sender, e) => saving = true;
            
            projectService.SaveProject();

            Assert.IsTrue(saving);
        }

        /// <summary>
        /// Tests that the ProjectSaved event is raised when a project was saved.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseProjectSavedEventWhenAProjectWasSaved()
        {
            var saved = false;

            var projectService = this.CreateProjectService();

            this.dataServiceFacade.InvokeLoadProjectCompleted(null);

            projectService.ProjectSaved += (sender, e) => saved = true;

            projectService.SaveProject();

            this.dataServiceFacade.InvokeSaveProjectCompleted(true);

            Assert.IsTrue(saved);
        }

        /// <summary>
        /// Creates the project service.
        /// </summary>
        /// <returns>The <see cref="ProjectService"/>.</returns>
        private IProjectService CreateProjectService()
        {
            return new ProjectService(this.dataServiceFacade, this.configurationService, () => this.errorView, this.logger);
        }
    }
}