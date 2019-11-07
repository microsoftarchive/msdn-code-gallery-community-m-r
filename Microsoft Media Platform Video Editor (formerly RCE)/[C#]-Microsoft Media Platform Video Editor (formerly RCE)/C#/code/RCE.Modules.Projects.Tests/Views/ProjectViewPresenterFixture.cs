// <copyright file="ProjectViewPresenterFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ProjectViewPresenterFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Projects.Tests.Views
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;
    using RCE.Infrastructure;
    using RCE.Infrastructure.Events;
    using RCE.Infrastructure.Models;
    using RCE.Modules.CommentsBar.Tests.Mocks;
    using RCE.Modules.EncoderOutput.Tests.Mocks;

    /// <summary>
    /// A class for testing the <see cref="ProjectViewPresenter"/>.
    /// </summary>
    [TestClass]
    public class ProjectViewPresenterFixture
    {
        /// <summary>
        /// The mocked ProjectView.
        /// </summary>
        private MockProjectView view;

        /// <summary>
        /// The mocked ConfigurationService.
        /// </summary>
        private MockConfigurationService configurationService;

        /// <summary>
        /// The mocked DataServiceFacade.
        /// </summary>
        private MockDataServiceFacade dataServiceFacade;

        /// <summary>
        /// The mocked RegionManager.
        /// </summary>
        private MockRegionManager regionManager;

        private MockEventAggregator eventAggregator; 

        /// <summary>
        /// The mocked region.
        /// </summary>
        private MockRegion mainRegion;

        private MockResetWindowsEvent resetWindowsEvent;

        /// <summary>
        /// Initializes the data for the tests.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.view = new MockProjectView();
            this.regionManager = new MockRegionManager();
            this.configurationService = new MockConfigurationService();
            this.dataServiceFacade = new MockDataServiceFacade();
            this.mainRegion = new MockRegion();
            this.eventAggregator = new MockEventAggregator();
            this.resetWindowsEvent = new MockResetWindowsEvent();
            this.eventAggregator.AddMapping<ResetWindowsEvent>(this.resetWindowsEvent);

            this.mainRegion.Name = RegionNames.MainRegion;
            this.regionManager.Regions.Add(this.mainRegion);
        }

        /// <summary>
        /// Tests if the <see cref="ProjectViewPresenter"/> can be initialized.
        /// </summary>
        [TestMethod]
        public void CanInitPresentationModel()
        {
            var presenter = this.CreateProjectViewPresenter();

            Assert.AreEqual(this.view, presenter.View);
        }

        /// <summary>
        /// Tests if the <see cref="ProjectViewPresenter"/> is being set on the view. 
        /// </summary>
        [TestMethod]
        public void ShouldSetPresenterIntoView()
        {
            var presenter = this.CreateProjectViewPresenter();

            Assert.AreEqual(presenter, this.view.Model);
        }

        /// <summary>
        /// Tests if the GetProjectsByUser method is being called.
        /// </summary>
        [TestMethod]
        public void ShouldCallGetProjectsByUserAsyncOfDataServiceFacade()
        {
            this.dataServiceFacade.GetProjectsByUserAsyncCalled = false;

            var presenter = this.CreateProjectViewPresenter();

            Assert.IsTrue(this.dataServiceFacade.GetProjectsByUserAsyncCalled);
        }

        /// <summary>
        /// Tests if the data service facade is being used to load the projects.
        /// </summary>
        [TestMethod]
        public void ShouldLoadProjectsFromDataServiceFacade()
        {
            this.dataServiceFacade.GetProjectsByUserAsyncCalled = false;
            var presenter = this.CreateProjectViewPresenter();

            List<Project> projects = new List<Project>() { new Project(), new Project() };

            this.dataServiceFacade.InvokeGetProjectsByUserCompleted(new RCE.Infrastructure.DataEventArgs<List<Project>>(projects));

            Assert.IsTrue(this.dataServiceFacade.GetProjectsByUserAsyncCalled);
            Assert.AreEqual(presenter.Projects.Count, projects.Count);
            Assert.AreEqual(presenter.Projects[0], projects[0]);
        }

        /// <summary>
        /// Tests if the OnPropertyChanged event is being raised when the Projects property change.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseOnPropertyChangedEventWhenProjectsIsUpdated()
        {
            var propertyChangedRaised = false;
            string propertyChangedEventArgsArgument = null;
            ObservableCollection<Project> projects = new ObservableCollection<Project>() { new Project(), new Project() };

            var presenter = this.CreateProjectViewPresenter();
            presenter.PropertyChanged += (sender, e) =>
            {
                propertyChangedRaised = true;
                propertyChangedEventArgsArgument = e.PropertyName;
            };

            Assert.IsFalse(propertyChangedRaised);
            Assert.IsNull(propertyChangedEventArgsArgument);

            presenter.Projects = projects;

            Assert.IsTrue(propertyChangedRaised);
            Assert.AreEqual("Projects", propertyChangedEventArgsArgument);
        }

        /// <summary>
        /// Tests if the HeaderInfo property returns the expected value.
        /// </summary>
        [TestMethod]
        public void ShouldReturnHeaderInfoResource()
        {
            var presenter = this.CreateProjectViewPresenter();

            var result = presenter.HeaderInfo;

            Assert.AreEqual("Projects", result);
        }

        /// <summary>
        /// Tests if the HeaderIconOff property returns the expected value.
        /// </summary>
        [TestMethod]
        public void ShouldReturnHeaderIconOffResource()
        {
            var presenter = this.CreateProjectViewPresenter();

            var result = presenter.HeaderIconOff;

            Assert.AreEqual("/RCE.Modules.Projects;component/images/icon_off.png", result);
        }

        /// <summary>
        /// Tests if the HeaderIconOn property returns the expected value.
        /// </summary>
        [TestMethod]
        public void ShouldReturnHeaderIconOnResource()
        {
            var presenter = this.CreateProjectViewPresenter();

            var result = presenter.HeaderIconOn;

            Assert.AreEqual("/RCE.Modules.Projects;component/images/icon_on.png", result);
        }

        /// <summary>
        /// Tests if the Activate of Region is called when KeyboardActionCommand is called.
        /// </summary>
        [TestMethod]
        public void ShouldActivateTheViewWhenKeyboardActionCommandIsExecuted()
        {
            this.mainRegion.SelectedItem = null;
            var presenter = this.CreateProjectViewPresenter();

            presenter.KeyboardActionCommand.Execute(new Tuple<KeyboardAction, object>(KeyboardAction.ActivateModel, null));

            Assert.AreSame(this.view, this.mainRegion.SelectedItem);
        }

        /// <summary>
        /// Tests if the current project is in the list.
        /// </summary>
        [TestMethod]
        public void ShouldNotShowTheCurrentProjectInTheList()
        {
            var currentProjectUri = new Uri("http://currentproject");

            this.configurationService.GetParameterValueReturnFunction = parameter => parameter == "ProjectId" ? currentProjectUri.ToString() : null;

            this.dataServiceFacade.Projects = new List<Project>()
                                                  {
                                                      new Project() { ProviderUri = currentProjectUri },
                                                      new Project() { ProviderUri = new Uri("http://currentproject1") },
                                                      new Project() { ProviderUri = new Uri("http://currentproject2") },
                                                      new Project() { ProviderUri = new Uri("http://currentproject3") }
                                                  };

            var presenter = this.CreateProjectViewPresenter();

            Assert.IsTrue(presenter.Projects.Count == 3);
        }

        /// <summary>
        /// Tests if the delete project is called when the DeleteCommand is executed and
        /// the project Uri is not null.
        /// </summary>
        [TestMethod]
        public void ShouldCallDeleteProjectWhenDeleteCommandIsExecutedAndUriIsNotNull()
        {
            var presenter = this.CreateProjectViewPresenter();
            this.dataServiceFacade.DeleteProjectCalled = false;

            presenter.DeleteCommand.Execute(new Uri("http://NewProject"));

            Assert.IsTrue(this.dataServiceFacade.DeleteProjectCalled);
        }

        /// <summary>
        /// Tests if the delete project is not called when the DeleteCommand is executed and
        /// the project Uri null.
        /// </summary>
        [TestMethod]
        public void ShouldNotCallDeleteProjectWhenDeleteCommandIsExecutedAndUriIsNull()
        {
            var presenter = this.CreateProjectViewPresenter();
            
            this.dataServiceFacade.DeleteProjectCalled = false;

            presenter.DeleteCommand.Execute(null);

            Assert.IsFalse(this.dataServiceFacade.DeleteProjectCalled);
        }

        /// <summary>
        /// Tests if the project is removed from the list if the DeleteProject returns true.
        /// </summary>
        [TestMethod]
        public void ShouldRemoveProjectWhenDeleteCommandIsExecutedAndDelteProjectReturnTrue()
        {
            Project project = new Project() { ProviderUri = new Uri("http://currentproject4") };
            this.dataServiceFacade.DeleteProjectResult = true;
            this.dataServiceFacade.Projects = new List<Project>()
                                                  {
                                                      project,
                                                      new Project() { ProviderUri = new Uri("http://currentproject1") },
                                                      new Project() { ProviderUri = new Uri("http://currentproject2") },
                                                      new Project() { ProviderUri = new Uri("http://currentproject3") }
                                                  };

            var presenter = this.CreateProjectViewPresenter();
            
            presenter.DeleteCommand.Execute(project.ProviderUri);
            
            Assert.AreEqual(3, presenter.Projects.Count);
        }

        /// <summary>
        /// Tests if the project is removed from the list if the DeleteProject returns true.
        /// </summary>
        [TestMethod]
        public void ShouldNotRemoveProjectWhenDeleteCommandIsExecutedAndDelteProjectReturnFalse()
        {
            Project project = new Project() { ProviderUri = new Uri("http://currentproject4") };
            this.dataServiceFacade.DeleteProjectResult = false;
            
            this.dataServiceFacade.Projects = new List<Project>()
                                                  {
                                                      project,
                                                      new Project() { ProviderUri = new Uri("http://currentproject1") },
                                                      new Project() { ProviderUri = new Uri("http://currentproject2") },
                                                      new Project() { ProviderUri = new Uri("http://currentproject3") }
                                                  };

            var presenter = this.CreateProjectViewPresenter();

            presenter.DeleteCommand.Execute(project.ProviderUri);
            
            Assert.AreEqual(4, presenter.Projects.Count);
        }

        /// <summary>
        /// Creates a <see cref="ProjectViewPresenter"/> using mocks objects.
        /// </summary>
        /// <returns>A new instance of a <see cref="ProjectViewPresenter"/>.</returns>
        private ProjectViewPresenter CreateProjectViewPresenter()
        {
            return new ProjectViewPresenter(this.view, this.configurationService, this.dataServiceFacade, this.regionManager, this.eventAggregator);
        }
    }
}
