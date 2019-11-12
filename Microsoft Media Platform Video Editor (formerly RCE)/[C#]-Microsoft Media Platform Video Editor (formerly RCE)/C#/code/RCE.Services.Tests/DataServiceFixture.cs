// <copyright file="DataServiceFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: DataServiceFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Services.Tests
{
    using System;
    using Contracts;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;

    /// <summary>
    /// Test class for <see cref="DataService"/>.
    /// </summary>
    [TestClass]
    public class DataServiceFixture
    {
        /// <summary>
        /// Mock for <see cref="IDataProvider"/>.
        /// </summary>
        private MockDataProvider dataProvider;

        /// <summary>
        /// Mock for ILoggerService.
        /// </summary>
        private MockLoggerService loggerService;

        /// <summary>
        /// Initilize the data.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.dataProvider = new MockDataProvider();
            this.loggerService = new MockLoggerService();
        }

        /// <summary>
        /// Should call to LoadProject.
        /// </summary>
        [TestMethod]
        public void ShouldCallToLoadProject()
        {
            var dataService = this.CreateDataService();

            Assert.IsFalse(this.dataProvider.LoadProjectCalled);

            dataService.LoadProject(new Uri("http://test"));

            Assert.IsTrue(this.dataProvider.LoadProjectCalled);
        }

        /// <summary>
        /// Should call to LoadMediaBin.
        /// </summary>
        [TestMethod]
        public void ShouldCallToLoadMediaBin()
        {
            var dataService = this.CreateDataService();

            Assert.IsFalse(this.dataProvider.LoadMediaBinCalled);

            dataService.LoadMediaBin(new Uri("http://test"));

            Assert.IsTrue(this.dataProvider.LoadMediaBinCalled);
        }

        /// <summary>
        /// Should call to SaveProject.
        /// </summary>
        [TestMethod]
        public void ShouldCallToSaveProject()
        {
            var dataService = this.CreateDataService();

            Assert.IsFalse(this.dataProvider.SaveProjectCalled);

            dataService.SaveProject(new Project());

            Assert.IsTrue(this.dataProvider.SaveProjectCalled);
        }

        /// <summary>
        /// Tests that the DeleteProject method of the DataProvider is being called 
        /// when the DeleteProject of the service is being invoked.
        /// </summary>
        [TestMethod]
        public void ShouldCallToDeleteProject()
        {
            var uri = new Uri("http://test");

            var dataService = this.CreateDataService();

            Assert.IsFalse(this.dataProvider.DeleteProjectCalled);

            dataService.DeleteProject(uri);

            Assert.IsTrue(this.dataProvider.DeleteProjectCalled);
        }

        /// <summary>
        /// Should call to LoadTitleTemplates.
        /// </summary>
        [TestMethod]
        public void ShouldCallToLoadTitleTemplates()
        {
            var dataService = this.CreateDataService();

            Assert.IsFalse(this.dataProvider.LoadTitleTemplatesCalled);

            dataService.LoadTitleTemplates();

            Assert.IsTrue(this.dataProvider.LoadTitleTemplatesCalled);
        }

        /// <summary>
        /// Should call to GetProjectsByUser.
        /// </summary>
        [TestMethod]
        public void ShouldCallToGetProjectsByUser()
        {
            var dataService = this.CreateDataService();

            Assert.IsFalse(this.dataProvider.GetProjectsByUserCalled);

            dataService.GetProjectsByUser(string.Empty);

            Assert.IsTrue(this.dataProvider.GetProjectsByUserCalled);
        }

        /// <summary>
        /// Should call to LoadProject of data provider when calling to GetProject.
        /// </summary>
        [TestMethod]
        public void ShouldCallToLoadProjectInDataProviderWhenCallingToGetProject()
        {
            var dataService = this.CreateDataService();

            Assert.IsFalse(this.dataProvider.LoadProjectCalled);

            dataService.GetProject("http://test");

            Assert.IsTrue(this.dataProvider.LoadProjectCalled);
        }

        /// <summary>
        /// Should return project from data provider result which has 
        /// the same id when loading A project.
        /// </summary>
        [TestMethod]
        public void ShouldReturnDataProviderResultWhenLoadingAProject()
        {
            var dataService = this.CreateDataService();

            this.dataProvider.LoadProjectResult = null;

            var project = dataService.LoadProject(new Uri("http://test"));

            Assert.IsNull(project);

            this.dataProvider.LoadProjectResult = new Project();

            project = dataService.LoadProject(new Uri("http://test"));

            Assert.IsNotNull(project);
            Assert.AreEqual(this.dataProvider.LoadProjectResult, project);
        }

        /// <summary>
        /// Should return project from data provider result which has 
        /// the same id when getting A project.
        /// </summary>
        [TestMethod]
        public void ShouldReturnDataProviderResultWhenGettingAProject()
        {
            var dataService = this.CreateDataService();

            this.dataProvider.LoadProjectResult = null;

            var project = dataService.GetProject("http://test");

            Assert.IsNull(project);

            this.dataProvider.LoadProjectResult = new Project();

            project = dataService.GetProject("http://test");

            Assert.IsNotNull(project);
            Assert.AreEqual(this.dataProvider.LoadProjectResult, project);
        }

        /// <summary>
        /// Should Throw An Exception if an exception happens while loading A project.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ShouldThrowAnExceptionIfAnExceptionHappensWhenLoadingAProject()
        {
            var dataService = this.CreateDataService();

           this.dataProvider.ThrowException = true;

           var project = dataService.LoadProject(new Uri("http://test"));            
        }

        /// <summary>
        /// Should return null if an invalid URI is passed while getting A project.
        /// </summary>
        [TestMethod]
        public void ShouldReturnNullIfAnInvalidUriIsPassedWhenGettingAProject()
        {
            var dataService = this.CreateDataService();

            var project = dataService.GetProject("invalid");

            Assert.IsNull(project);
        }

        /// <summary>
        /// Should log exception if an exception happens when loading A project.
        /// </summary>
        [TestMethod]
        public void ShouldLogExceptionIfAnExceptionHappensWhenLoadingAProject()
        {
            var dataService = this.CreateDataService();

            this.dataProvider.ThrowException = true;

            Assert.IsFalse(this.loggerService.LogEntriesCalled);

            try
            {
                dataService.LoadProject(new Uri("http://test"));
            }
            catch
            {
            }
            
            Assert.IsTrue(this.loggerService.LogEntriesCalled);
        }

        /// <summary>
        /// Should log exception if an exception happens when getting A project.
        /// </summary>
        [TestMethod]
        public void ShouldLogExceptionIfAnExceptionHappensWhenGettingAProject()
        {
            var dataService = this.CreateDataService();

            this.dataProvider.ThrowException = true;

            Assert.IsFalse(this.loggerService.LogEntriesCalled);

            try
            {
                dataService.GetProject("http://test");
            }
            catch
            {
            }

            Assert.IsTrue(this.loggerService.LogEntriesCalled);
        }

        /// <summary>
        /// Should return mediabin from data provider results with the given id
        ///  when loading media bin.
        /// </summary>
        [TestMethod]
        public void ShouldReturnDataProviderResultWhenLoadingAMediaBin()
        {
            var dataService = this.CreateDataService();

            this.dataProvider.LoadMediaBinResult = null;

            var mediaBin = dataService.LoadMediaBin(new Uri("http://test"));

            Assert.IsNull(mediaBin);

            this.dataProvider.LoadMediaBinResult = new MediaBin();

            mediaBin = dataService.LoadMediaBin(new Uri("http://test"));

            Assert.IsNotNull(mediaBin);
            Assert.AreEqual(this.dataProvider.LoadMediaBinResult, mediaBin);
        }

        /// <summary>
        /// Should Throw An Exception if an exception happens when loading media bin.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ShouldThrowAnExceptionIfAnExceptionHappensWhenLoadingMediaBin()
        {
            var dataService = this.CreateDataService();

            this.dataProvider.ThrowException = true;

            var mediaBin = dataService.LoadMediaBin(new Uri("http://test"));
        }

        /// <summary>
        /// Should log exception if an exception happens when loading media bin.
        /// </summary>
        [TestMethod]
        public void ShouldLogExceptionIfAnExceptionHappensWhenLoadingMediaBin()
        {
            var dataService = this.CreateDataService();

            this.dataProvider.ThrowException = true;

            Assert.IsFalse(this.loggerService.LogEntriesCalled);

            try
            {
                dataService.LoadMediaBin(new Uri("http://test"));
            }
            catch
            {
            }

            Assert.IsTrue(this.loggerService.LogEntriesCalled);
        }

        /// <summary>
        /// Should return data provider result when saving A project.
        /// </summary>
        [TestMethod]
        public void ShouldReturnDataProviderResultWhenSavingAProject()
        {
            var dataService = this.CreateDataService();

            this.dataProvider.SaveProjectResult = false;

            var result = dataService.SaveProject(new Project());

            Assert.IsFalse(result);

            this.dataProvider.SaveProjectResult = true;

            result = dataService.SaveProject(new Project());

            Assert.IsTrue(result);
            Assert.AreEqual(this.dataProvider.SaveProjectResult, result);
        }

        /// <summary>
        /// Tests that the DeleteProject method returns the values provided by the dataprovider.
        /// </summary>
        [TestMethod]
        public void ShouldReturnDataProviderResultWhenDeletingAProject()
        {
            var dataService = this.CreateDataService();

            this.dataProvider.DeleteProjectResult = false;

            var result = dataService.DeleteProject(new Uri("http://test"));

            Assert.IsFalse(result);

            this.dataProvider.DeleteProjectResult = true;

            result = dataService.DeleteProject(new Uri("http://test"));

            Assert.IsTrue(result);
            Assert.AreEqual(this.dataProvider.DeleteProjectResult, result);
        }

        /// <summary>
        /// Should Throw An Exception if an exception happens when saving A project.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ShouldThrowAnExceptionIfAnExceptionHappensWhenSavingAProject()
        {
            var dataService = this.CreateDataService();

            this.dataProvider.ThrowException = true;

            var result = dataService.SaveProject(new Project());
        }

        /// <summary>
        /// Should throw an exception if an exception happens when saving A project.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ShouldThrowAnExceptionIfAnExceptionHappensWhenDeletingAProject()
        {
            var dataService = this.CreateDataService();

            this.dataProvider.ThrowException = true;

            var result = dataService.DeleteProject(new Uri("http://test"));
        }

        /// <summary>
        /// Should log exception if an exception happens when saving A project.
        /// </summary>
        [TestMethod]
        public void ShouldLogExceptionIfAnExceptionHappensWhenSavingAProject()
        {
            var dataService = this.CreateDataService();

            this.dataProvider.ThrowException = true;

            Assert.IsFalse(this.loggerService.LogEntriesCalled);

            try
            {
                dataService.SaveProject(new Project());
            }
            catch
            {
            }

            Assert.IsTrue(this.loggerService.LogEntriesCalled);
        }

        /// <summary>
        /// Tests that the exception thrown by the DeleteProject method is being logged.
        /// </summary>
        [TestMethod]
        public void ShouldLogExceptionIfAnExceptionHappensWhenDeletingAProject()
        {
            var dataService = this.CreateDataService();

            this.dataProvider.ThrowException = true;

            Assert.IsFalse(this.loggerService.LogEntriesCalled);

            try
            {
                dataService.DeleteProject(new Uri("http://test"));
            }
            catch
            {
            }

            Assert.IsTrue(this.loggerService.LogEntriesCalled);
        }

        /// <summary>
        /// Should return data provider result when loading title templates.
        /// </summary>
        [TestMethod]
        public void ShouldReturnDataProviderResultWhenLoadingTitleTemplates()
        {
            var dataService = this.CreateDataService();

            this.dataProvider.LoadTitleTemplatesResult = null;

            var templates = dataService.LoadTitleTemplates();

            Assert.IsNull(templates);

            this.dataProvider.LoadTitleTemplatesResult = new TitleTemplateCollection();

            templates = dataService.LoadTitleTemplates();

            Assert.IsNotNull(templates);
            Assert.AreEqual(this.dataProvider.LoadTitleTemplatesResult, templates);
        }

        /// <summary>
        /// Should throw an exception if an exception happens when loading title templates.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ShouldThrowAnExceptionIfAnExceptionHappensWhenLoadingTitleTemplates()
        {
            var dataService = this.CreateDataService();

            this.dataProvider.ThrowException = true;

            var templates = dataService.LoadTitleTemplates();
        }

        /// <summary>
        /// Should log exception if an exception happens when loading title templates.
        /// </summary>
        [TestMethod]
        public void ShouldLogExceptionIfAnExceptionHappensWhenLoadingTitleTemplates()
        {
            var dataService = this.CreateDataService();

            this.dataProvider.ThrowException = true;

            Assert.IsFalse(this.loggerService.LogEntriesCalled);

            try
            {
                dataService.LoadTitleTemplates();
            }
            catch
            {
            }

            Assert.IsTrue(this.loggerService.LogEntriesCalled);
        }

        /// <summary>
        /// Should return data provider result when getting projects by user.
        /// </summary>
        [TestMethod]
        public void ShouldReturnDataProviderResultWhenGettingProjectsByUser()
        {
            var dataService = this.CreateDataService();

            this.dataProvider.GetProjectsByUserResult = null;

            var projects = dataService.GetProjectsByUser(string.Empty);

            Assert.IsNull(projects);

            this.dataProvider.GetProjectsByUserResult = new ProjectCollection();

            projects = dataService.GetProjectsByUser(string.Empty);

            Assert.IsNotNull(projects);
            Assert.AreEqual(this.dataProvider.GetProjectsByUserResult, projects);
        }

        /// <summary>
        /// Should throw an exception if an exception happens when getting projects by user.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ShouldThrowAnExceptionIfAnExceptionHappensWhenGettingProjectsByUser()
        {
            var dataService = this.CreateDataService();

            this.dataProvider.ThrowException = true;

            var projects = dataService.GetProjectsByUser(string.Empty);
        }

        /// <summary>
        /// Should log exception if an exception happens when getting projects by user.
        /// </summary>
        [TestMethod]
        public void ShouldLogExceptionIfAnExceptionHappensWhenGettingProjectsByUser()
        {
            var dataService = this.CreateDataService();

            this.dataProvider.ThrowException = true;

            Assert.IsFalse(this.loggerService.LogEntriesCalled);

            try
            {
                dataService.GetProjectsByUser(string.Empty);
            }
            catch
            {
            }

            Assert.IsTrue(this.loggerService.LogEntriesCalled);
        }

        /// <summary>
        /// Creates the data service.
        /// </summary>
        /// <returns>The <see cref="IDataService"/>.</returns>
        private IDataService CreateDataService()
        {
            return new DataService(this.dataProvider, this.loggerService);
        }
    }
}