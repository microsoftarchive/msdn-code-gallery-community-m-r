// <copyright file="NotificationViewPresenter.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: NotificationViewPresentationModel.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Services.Views
{
    using System;
    using System.Globalization;
    using System.Windows;
    using Infrastructure;
    using Infrastructure.Events;
    using Microsoft.Practices.Composite.Events;

    using RCE.Infrastructure.Services;

    /// <summary>
    /// Interacts with the <see cref="INotificationView"/> view.
    /// </summary>
    public class NotificationViewPresenter : INotificationViewPresenter
    {
        /// <summary>
        /// The <seealso cref="IProjectService"/> instance used to save the current project.
        /// </summary>
        private readonly IProjectService projectService;

        /// <summary>
        /// The <seealso cref="IEventAggregator"/> instance used to publish and subscribe to events.
        /// </summary>
        private readonly IEventAggregator eventAggregator;

        /// <summary>
        /// The error view resolver.
        /// </summary>
        private readonly Func<IErrorView> errorViewResolver;

        /// <summary>
        /// Indicates if there were errors or not while loading the project.
        /// </summary>
        private bool hasErrors;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationViewPresenter"/> class.
        /// </summary>
        /// <param name="view">The <see cref="INotificationView"/> view instance.</param>
        /// <param name="projectService">The <see cref="IProjectService"/> service instance used to determine when to show the progress bar.</param>
        /// <param name="eventAggregator">The <seealso cref="IEventAggregator"/> service used to publish and subscribe to events.</param>
        /// <param name="errorViewResolver">The error view resolver.</param>
        public NotificationViewPresenter(INotificationView view, IProjectService projectService, IEventAggregator eventAggregator, Func<IErrorView> errorViewResolver)
        {
            this.View = view;
            this.projectService = projectService;
            this.eventAggregator = eventAggregator;
            this.errorViewResolver = errorViewResolver;

            this.eventAggregator.GetEvent<SaveProjectEvent>().Subscribe(this.SaveProject, true);

            this.projectService.ProjectSaving += this.ProjectService_ProjectSaving;
            this.projectService.ProjectSaved += this.ProjectService_ProjectSaved;

            this.ToggleProgress();
        }

        /// <summary>
        /// Gets the <see cref="INotificationView"/> view.
        /// </summary>
        /// <value>A <seealso cref="INotificationView"/> that represents the current view.</value>
        public INotificationView View { get; private set; }

        /// <summary>
        /// Toggles the visibility of the progres bar based on the the <see cref="ProjectState"/> state.
        /// </summary>
        private void ToggleProgress()
        {
            if (this.projectService.State == ProjectState.Retrieving)
            {
                this.projectService.ProjectRetrieved += (sender, e) =>
                {
                    this.View.HideProgressBar();

                    // this.ProjectName = this.projectService.GetCurrentProject().Name;
                };
                this.projectService.ProjectError += (sender, e) =>
                {
                    this.hasErrors = true;
                    this.HandlePendingNotifications();
                };

                this.View.ShowProgressBar();
            }
            else if (this.projectService.State == ProjectState.Retrieved)
            {
                this.View.HideProgressBar();

                // this.ProjectName = this.projectService.GetCurrentProject().Name;
            }
            else
            {
               this.hasErrors = true;
               this.HandlePendingNotifications();
            }
        }

        private void SaveProject(object parameter)
        {
            this.projectService.SaveProject();
        }

       /// <summary>
        /// Shows the pending notifications if any.
        /// </summary>
        private void HandlePendingNotifications()
        {
            if (Deployment.Current.Dispatcher.CheckAccess())
            {
                if (this.hasErrors)
                {
                    this.View.HideProgressBar();
                    IErrorView errorView = this.errorViewResolver();
                    errorView.ErrorMessage = Resources.Resources.ProjectServiceLoadProjectsError;
                    errorView.Show();
                }
            }
            else
            {
                Deployment.Current.Dispatcher.BeginInvoke(() => this.HandlePendingNotifications());
            }
        }

        /// <summary>
        /// Shows the project saving message.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args containing the event data.</param>
        private void ProjectService_ProjectSaving(object sender, EventArgs e)
        {
            this.PublishStatus(Resources.Resources.Saving);
        }

        /// <summary>
        /// Shows the project saved message.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The event args containing the event data.</param>
        private void ProjectService_ProjectSaved(object sender, RCE.Infrastructure.DataEventArgs<bool> args)
        {
            string statusMessage;
            if (args.Data)
            {
                statusMessage = string.Format(CultureInfo.InvariantCulture, Resources.Resources.LastSaved, DateTime.Now.ToString("HH:mm:ss", CultureInfo.InvariantCulture));
            }
            else
            {
                statusMessage = Resources.Resources.FailedSavingProject;
            }

            this.PublishStatus(statusMessage);
        }

        /// <summary>
        /// Publishes the Status event.
        /// </summary>
        /// <param name="status">The message payload.</param>
        private void PublishStatus(string status)
        {
            this.eventAggregator.GetEvent<StatusEvent>().Publish(status);
        }
    }
}
