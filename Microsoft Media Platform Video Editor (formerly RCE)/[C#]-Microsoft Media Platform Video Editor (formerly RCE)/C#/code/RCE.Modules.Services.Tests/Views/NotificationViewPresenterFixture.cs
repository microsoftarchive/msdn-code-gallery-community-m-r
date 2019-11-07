// <copyright file="NotificationViewPresenterFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: NotificationViewPresenterFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Services.Tests.Views
{
    using System.Globalization;
    using Infrastructure;
    using Infrastructure.Events;
    using Microsoft.Practices.Composite.Events;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;
    using Modules.Services.Views;

    using RCE.Infrastructure.Services;

    [TestClass]
    public class NotificationViewPresenterFixture
    {
        /// <summary>
        /// Mock for <see cref="IEventAggregator"/>.
        /// </summary>
        private MockEventAggregator eventAggregator;

        /// <summary>
        /// Mock for <see cref="IProjectService"/>.
        /// </summary>
        private MockProjectService projectService;

        /// <summary>
        /// Mock for <seealso cref="IErrorView"/>
        /// </summary>
        private MockErrorView errorView;

        /// <summary>
        /// Mock for <see cref="NotificationView"/>.
        /// </summary>
        private MockNotificationView view;

        /// <summary>
        /// Mock for <see cref="SaveProjectEvent"/>
        /// </summary>
        private MockSaveProjectEvent saveProjectEvent;

        /// <summary>
        /// Mock for <see cref="statusEvent"/>
        /// </summary>
        private MockStatusEvent statusEvent;

        /// <summary>
        /// Initilize the test class.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.view = new MockNotificationView();
            this.projectService = new MockProjectService();
            this.eventAggregator = new MockEventAggregator();
            this.errorView = new MockErrorView();
            
            this.saveProjectEvent = new MockSaveProjectEvent();
            this.statusEvent = new MockStatusEvent();

            this.eventAggregator.AddMapping<SaveProjectEvent>(this.saveProjectEvent);
            this.eventAggregator.AddMapping<StatusEvent>(this.statusEvent);
        }

        /// <summary>
        /// Tests if <see cref="NotificationViewPresenter"/> shows the progress bar if the 
        /// project is in Retrieve state.
        /// </summary>
        [TestMethod]
        public void ShouldCallToShowProgressBarIfProjectStateIsRetrieving()
        {
            this.projectService.State = ProjectState.Retrieving;

            Assert.IsFalse(this.view.ShowProgressBarCalled);

            this.CreatePresenter();

            Assert.IsTrue(this.view.ShowProgressBarCalled);
        }

        /// <summary>
        /// Tests if <see cref="NotificationViewPresenter"/> calls HideProgressBar when 
        /// project state changes to Retrieved.
        /// </summary>
        [TestMethod]
        public void ShouldCallToHideProgressBarIfProjectStateIsRetrieved()
        {
            this.projectService.State = ProjectState.Retrieved;

            Assert.IsFalse(this.view.HideProgressBarCalled);

            this.CreatePresenter();

            Assert.IsTrue(this.view.HideProgressBarCalled);
        }

        /// <summary>
        /// Tests if <see cref="NotificationViewPresenter"/> calls HideProgressBar when 
        /// project state changes to Retrieved.
        /// </summary>
        [TestMethod]
        public void ShouldCallToHideProgressBarAfterInvokingProjectRetrievedEvent()
        {
            this.projectService.State = ProjectState.Retrieving;

            Assert.IsFalse(this.view.HideProgressBarCalled);

            this.CreatePresenter();

            Assert.IsFalse(this.view.HideProgressBarCalled);

            this.projectService.InvokeProjectRetrieved();

            Assert.IsTrue(this.view.HideProgressBarCalled);
        }

        /// <summary>
        /// Tests if the Show method of the IErrorView is not being called if the retrieval of the project does not fail.
        /// </summary>
        [TestMethod]
        public void ShouldNotShowErrorViewIfNoErrorOccurredWhenRetrievingTheProject()
        {
            this.projectService.State = ProjectState.Retrieving;

            var presenter = this.CreatePresenter();

            this.projectService.InvokeProjectRetrieved();

            Assert.IsFalse(this.errorView.ShowCalled);
        }

        /// <summary>
        /// Tests if the Show method of the IErrorView is being called if the retrieval of the project does not fail.
        /// </summary>
        [TestMethod]
        public void ShouldShowErrorViewIfAnErrorOccurredWhenRetrievingTheProject()
        {
            this.projectService.State = ProjectState.Retrieving;

            var presenter = this.CreatePresenter();

            this.projectService.InvokeProjectError();

            Assert.IsTrue(this.errorView.ShowCalled);
        }

        /// <summary>
        /// Tests if <see cref="NotificationViewPresenter"/> publishes the StatusEvent
        /// when ProjectSaved event is triggered and project was not saved sucessfully.
        /// </summary>
        [TestMethod]
        public void ShouldPublishStatusEventWhenProjectSavedEventIsTriggeredAndProjectWasNotSavedSuccesfully()
        {
            var presenter = this.CreatePresenter();

            Assert.IsFalse(this.statusEvent.PublishCalled);

            this.projectService.InvokeProjectSaved(false);

            Assert.IsTrue(this.statusEvent.PublishCalled);

            Assert.AreEqual(Resources.Resources.FailedSavingProject, this.statusEvent.PublishArgumentPayload);
        }

        /// <summary>
        /// Tests if <see cref="NotificationViewPresenter"/> publishes the StatusEvent
        /// when ProjectSaved event is triggered and project was saved sucessfully.
        /// </summary>
        [TestMethod]
        public void ShouldPublishSetStatusEventWhenProjectSavedEventIsTriggeredAndProjectWasSavedSuccesfully()
        {
            var presenter = this.CreatePresenter();

            Assert.IsFalse(this.statusEvent.PublishCalled);

            this.projectService.InvokeProjectSaved(true);

            Assert.IsTrue(this.statusEvent.PublishCalled);

            StringAssert.Contains(this.statusEvent.PublishArgumentPayload, string.Format(CultureInfo.InvariantCulture, Resources.Resources.LastSaved, string.Empty));
        }

        /// <summary>
        /// Tests if <see cref="NotificationViewPresenter"/> publishes the StatusEvent
        /// when ProjectSaving event is triggered.
        /// </summary>
        [TestMethod]
        public void ShouldPublishStatusEventWhenProjectSavingEventIsTriggered()
        {
            var presenter = this.CreatePresenter();

            Assert.IsFalse(this.statusEvent.PublishCalled);

            this.projectService.InvokeProjectSaving();

            Assert.IsTrue(this.statusEvent.PublishCalled);

            Assert.AreEqual(Resources.Resources.Saving, this.statusEvent.PublishArgumentPayload);
        }

        /// <summary>
        /// Creates the presenter.
        /// </summary>
        /// <returns>The <see cref="NotificationViewPresenter"/>.</returns>
        private NotificationViewPresenter CreatePresenter()
        {
            return new NotificationViewPresenter(this.view, this.projectService, this.eventAggregator, () => this.errorView);
        }
    }
}
