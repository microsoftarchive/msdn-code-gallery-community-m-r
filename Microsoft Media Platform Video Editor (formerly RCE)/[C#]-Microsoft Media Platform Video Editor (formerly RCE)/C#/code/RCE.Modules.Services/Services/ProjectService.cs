// <copyright file="ProjectService.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ProjectService.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Services
{
    using System;
    using System.Windows;
    using System.Windows.Browser;
    using RCE.Infrastructure;
    using RCE.Infrastructure.Services;

    using SMPTETimecode;
    using Project = RCE.Infrastructure.Models.Project;
    using Sequence = RCE.Infrastructure.Models.Sequence;
    using Track = RCE.Infrastructure.Models.Track;
    using TrackType = RCE.Infrastructure.Models.TrackType;

    /// <summary>
    /// It interacts with the DataService for all the project related calls
    /// and maintains the current project in which the user is working.
    /// </summary>
    public class ProjectService : IProjectService
    {
        /// <summary>
        /// The <see cref="ILogger"/> to log the exceptions.
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// The <see cref="IDataServiceFacade"/> for server calls.
        /// </summary>
        private readonly IDataServiceFacade dataService;

        /// <summary>
        /// The <see cref="IConfigurationService"/> for configuration settings.
        /// </summary>
        private readonly IConfigurationService configurationService;

        /// <summary>
        /// The resolver of the IErrorView.
        /// </summary>
        private readonly Func<IErrorView> errorViewResolver;

        /// <summary>
        /// The current project.
        /// </summary>
        private Project currentProject;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectService"/> class.
        /// </summary>
        /// <param name="dataService">
        /// The data service.
        /// </param>
        /// <param name="configurationService">
        /// The configuration service.
        /// </param>
        /// <param name="errorViewResolver">
        /// The error View Resolver.
        /// </param>
        /// <param name="logger">The <see cref="ILogger"/> used to log application events.</param>
        public ProjectService(IDataServiceFacade dataService, IConfigurationService configurationService, Func<IErrorView> errorViewResolver, ILogger logger)
        {
            this.dataService = dataService;
            this.configurationService = configurationService;
            this.logger = logger;
            this.errorViewResolver = errorViewResolver;
            this.dataService.LoadProjectCompleted += this.DataService_LoadProjectCompleted;
            this.dataService.SaveProjectCompleted += this.DataService_SaveProjectCompleted;

            this.RetrieveProject(this.configurationService.GetProjectId());
        }

        /// <summary>
        /// Occurs when [project retrieved].
        /// </summary>
        public event EventHandler ProjectRetrieved;

        /// <summary>
        /// Occurs when the project retrieving fails.
        /// </summary>
        public event EventHandler ProjectError;

        /// <summary>
        /// Occures when the saving of the project ended.
        /// </summary>
        public event EventHandler<DataEventArgs<bool>> ProjectSaved;

        /// <summary>
        /// Occurs when the save of the project started.
        /// </summary>
        public event EventHandler ProjectSaving;

        /// <summary>
        /// Gets the state of the project.
        /// </summary>
        /// <value>The <see cref="ProjectState"/>.</value>
        public ProjectState State { get; private set; }

        /// <summary>
        /// Gets the current project.
        /// </summary>
        /// <returns>The <see cref="Project"/>.</returns>
        public Project GetCurrentProject()
        {
            return this.currentProject; 
        }

        /// <summary>
        /// Saves the project.
        /// </summary>
        public void SaveProject()
        {
            if (this.State == ProjectState.Retrieved)
            {
                this.OnProjectSaving();

                // Set the duration of the project.
                this.currentProject.SetProjectDuration();
                this.dataService.SaveProjectAsync(this.currentProject);
            }
        }

        public Sequence CreateTimeline()
        {
            var timeline = new Sequence();
            timeline.Tracks.Add(new Track { Number = 1, TrackType = TrackType.Visual, Volume = 1 });

            int? maxNumberOfAudioTracks =
                this.configurationService.GetParameterValueAsInt("MaxNumberOfAudioTracks").GetValueOrDefault(1);

            for (int i = 1; i <= maxNumberOfAudioTracks.Value; i++)
            {
                var track = new Track
                {
                    Number = i + 1,
                    TrackType = TrackType.Audio,
                    IsMuted = true,
                    Volume = 1
                };

                timeline.Tracks.Add(track);
            }

            timeline.Tracks.Add(new Track { TrackType = TrackType.Overlay });

            return timeline;
        }

        /// <summary>
        /// Shows the error view.
        /// </summary>
        private void ShowError()
        {
            IErrorView errorView = this.errorViewResolver();
            errorView.ErrorMessage = Resources.Resources.ProjectServiceSaveProjectError;
            errorView.Show();
        }

        /// <summary>
        /// Handles the SaveProjectCompleted event of the <see cref="IDataServiceFacade"/>.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <seealso cref="DataEventArgs{T}"/> instance containing the event data.</param>
        private void DataService_SaveProjectCompleted(object sender, DataEventArgs<bool> e)
        {
            this.OnProjectSaved(e.Error == null);

            if (e.Error != null)
            {
                this.logger.Log(this.GetType().Name, e.Error);

                if (Deployment.Current != null && Deployment.Current.Dispatcher != null)
                {
                    if (Deployment.Current.Dispatcher.CheckAccess())
                    {
                        this.ShowError();
                    }
                    else
                    {
                        Deployment.Current.Dispatcher.BeginInvoke(this.ShowError);
                    }
                }
            }
        }

        /// <summary>
        /// Retrieves the project.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        private void RetrieveProject(Uri projectId)
        {
           this.State = ProjectState.Retrieving;
           this.dataService.LoadProjectAsync(projectId);
        }

        /// <summary>
        /// Handles the LoadProjectcompleted event of the <see cref="IDataServiceFacade"/>.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <seealso cref="DataEventArgs{T}"/> instance containing the event data.</param>
        private void DataService_LoadProjectCompleted(object sender, DataEventArgs<Project> e)
        {
            if (e.Error != null)
            {
                this.State = ProjectState.Error;

                this.logger.Log(this.GetType().Name, e.Error);
                this.OnProjectError();
            }
            else
            {
                this.LoadProjectCompleted(e.Data);
                this.State = ProjectState.Retrieved;

                if (Deployment.Current != null && Deployment.Current.Dispatcher != null)
                {
                    if (Deployment.Current.Dispatcher.CheckAccess())
                    {
                        this.OnProjectRetrieved();
                    }
                    else
                    {
                        Deployment.Current.Dispatcher.BeginInvoke(this.OnProjectRetrieved);
                    }
                }
            }
        }

        /// <summary>
        /// Callback handler for the LoadProject method.
        /// </summary>
        /// <param name="project">The project.</param>
        private void LoadProjectCompleted(Project project)
        {
            if (project == null)
            {
                string projectId = null;
                if (!HtmlPage.Document.QueryString.TryGetValue("projectId", out projectId))
                {
                    projectId = Guid.NewGuid().ToString();
                }

                project = new Project
                              {
                                  Name = projectId,
                                  Creator = this.configurationService.GetUserName(),
                                  Created = DateTime.Now,
                                  SmpteFrameRate = SmpteFrameRate.Smpte2997NonDrop,
                                  AutoSaveInterval = 180,
                                  RippleMode = false
                              };

                Sequence sequence = this.CreateTimeline();
                project.AddTimeline(sequence);
                project.StartTimeCode = TimeCode.FromAbsoluteTime(0, project.SmpteFrameRate);
            }

            this.currentProject = project;
        }

        /// <summary>
        /// Raises the <see cref="ProjectRetrieved"/> event.
        /// </summary>
        private void OnProjectRetrieved()
        {
            EventHandler projectRetrievedHandler = this.ProjectRetrieved;
            if (projectRetrievedHandler != null)
            {
                projectRetrievedHandler(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Raises the <see cref="ProjectError"/> event.
        /// </summary>
        private void OnProjectError()
        {
            EventHandler error = this.ProjectError;
            if (error != null)
            {
                error(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Raises the <see cref="ProjectSaved"/> event.
        /// </summary>
        /// <param name="saved">A true if the project was saved succesfully;otherwise false.</param>
        private void OnProjectSaved(bool saved)
        {
            EventHandler<DataEventArgs<bool>> handler = this.ProjectSaved;
            if (handler != null)
            {
                handler(this, new DataEventArgs<bool>(saved));
            }
        }
        
        /// <summary>
        /// Rauses the <see cref="ProjectSaving"/> event.
        /// </summary>
        private void OnProjectSaving()
        {
            EventHandler handler = this.ProjectSaving;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}
