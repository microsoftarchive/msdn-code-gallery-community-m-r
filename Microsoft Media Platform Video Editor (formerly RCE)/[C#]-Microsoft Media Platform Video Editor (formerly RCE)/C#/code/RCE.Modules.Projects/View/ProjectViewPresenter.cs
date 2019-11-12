// <copyright file="ProjectViewPresenter.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ProjectViewPresenter.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Projects
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;

    using Microsoft.Practices.Composite.Events;
    using Microsoft.Practices.Composite.Presentation.Commands;
    using Microsoft.Practices.Composite.Regions;
    using RCE.Infrastructure;
    using RCE.Infrastructure.Events;
    using RCE.Infrastructure.Models;
    using RCE.Infrastructure.Services;
    using RCE.Infrastructure.Windows;

    /// <summary>
    /// Presenter class for the Project View control.
    /// </summary>
    public class ProjectViewPresenter : BaseModel, IProjectViewPresenter, IWindowMetadataProvider
    {
        /// <summary>
        /// The <seealso cref="IDataServiceFacade"/> instance used to used to get the list of projects.
        /// </summary>
        private readonly IDataServiceFacade serviceFacade;

        /// <summary>
        /// The <seealso cref="IConfigurationService"/> instance used to get the username.
        /// </summary>
        private readonly IConfigurationService configurationService;

        /// <summary>
        /// The <see cref="IRegionManager"/>.
        /// </summary>
        private readonly IRegionManager regionManager;

        /// <summary>
        /// Command used to delete a Project.
        /// </summary>
        private readonly DelegateCommand<object> deleteCommand;

        /// <summary>
        /// List of projects for the current user.
        /// </summary>
        private ObservableCollection<Project> projects;

        /// <summary>
        /// Uri of the project to be deleted.
        /// </summary>
        private Uri deletedProjectUri;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectViewPresenter"/> class.
        /// </summary>
        /// <param name="view">The <see cref="IProjectView"/> view instance.</param>
        /// <param name="configurationService">The <seealso cref="IConfigurationService"/> service used get the list of the projects.</param>
        /// <param name="serviceFacade">The service facade.</param>
        /// <param name="regionManager">The <see cref="IRegionManager"/>.</param>
        public ProjectViewPresenter(IProjectView view, IConfigurationService configurationService, IDataServiceFacade serviceFacade, IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            this.serviceFacade = serviceFacade;
            this.configurationService = configurationService;
            this.regionManager = regionManager;
            this.deleteCommand = new DelegateCommand<object>(this.Delete);

            this.KeyboardActionCommand = new DelegateCommand<Tuple<KeyboardAction, object>>(this.ExecuteKeyboardAction);

            eventAggregator.GetEvent<ResetWindowsEvent>().Subscribe(this.ResetWindow);

            this.View = view;
            this.View.Model = this;

            this.serviceFacade.GetProjectsByUserCompleted += this.OnGetProjectsCompleted;

            this.serviceFacade.DeleteProjectCompleted += (sender, e) =>
                     {
                         if (e.Data && this.deletedProjectUri != null)
                         {
                             Project project = this.projects.SingleOrDefault(x => x.ProviderUri == this.deletedProjectUri);

                             if (project != null)
                             {
                                 this.Projects.Remove(project);
                             }
                         }

                         this.deletedProjectUri = null;
                     };

            this.serviceFacade.GetProjectsByUserAsync(this.configurationService.GetUserName());
            this.serviceFacade.SaveProjectCompleted += this.LoadProjectList;
        }

        public event EventHandler<Infrastructure.DataEventArgs<object>> TitleUpdated;

        public event EventHandler<Infrastructure.DataEventArgs<object>> ResetPositionRaised;

        /// <summary>
        /// Gets or sets the <see cref="IProjectView"/> of the presenter.
        /// </summary>
        /// <value>A <seealso cref="IProjectView"/> that represents the current view of the presenter.</value>
        public IProjectView View { get; set; }

        /// <summary>
        /// Gets or sets the Projects of the presenter.
        /// </summary>
        /// <value>A List of projects for the given user.</value>
        public ObservableCollection<Project> Projects
        {
            get
            {
                return this.projects;
            }

            set
            {
                this.projects = value;
                this.OnPropertyChanged("Projects");
            }
        }

        /// <summary>
        /// Gets the header icon (off status).
        /// </summary>
        /// <value>An <seealso cref="string" /> that represents the header icon off resource.</value>
        public string HeaderIconOff
        {
            get { return Resources.Resources.HeaderIconOff; }
        }

        public VerticalWindowPosition VerticalPosition
        {
            get
            {
                return VerticalWindowPosition.Center;
            }
        }

        public HorizontalWindowPosition HorizontalPosition
        {
            get
            {
                return HorizontalWindowPosition.Left;
            }
        }

        public object Title
        {
            get
            {
                return "Project List";
            }
        }

        /// <summary>
        /// Gets the header info.
        /// </summary>
        /// <value>The header info.</value>
        public string HeaderInfo
        {
            get { return Resources.Resources.HeaderInfo; }
        }

        /// <summary>
        /// Gets the Header Icon.
        /// </summary>
        /// <value>The header icon name.</value>
        public string HeaderIconOn
        {
            get { return Resources.Resources.HeaderIconOn; }
        }

        public ResizeDirection ResizeDirection
        {
            get
            {
                return ResizeDirection.Vertical;
            }
        }

        public Size Size
        {
            get
            {
                return new Size(709, 300);
            }
        }

        /// <summary>
        /// Gets the command that deletes the selected Project.
        /// </summary>
        /// <value>The <see cref="DelegateCommand{T}"/>.</value>
        public DelegateCommand<object> DeleteCommand
        {
            get
            {
                return this.deleteCommand;
            }
        }

        public DelegateCommand<Tuple<KeyboardAction, object>> KeyboardActionCommand { get; private set; }

        public KeyboardActionContext ActionContext
        {
            get
            {
                return KeyboardActionContext.Projects;
            }
        }

        public void ReloadProjectList(object obj)
        {
            this.serviceFacade.GetProjectsByUserAsync(this.configurationService.GetUserName());
        }

        public void ResetWindow(object obj)
        {
            EventHandler<Infrastructure.DataEventArgs<object>> handler = this.ResetPositionRaised;

            if (handler != null)
            {
                handler.Invoke(this, new Infrastructure.DataEventArgs<object>(this.View));
            }
        }

        /// <summary>
        /// Activates this Project view.
        /// </summary>
        private void Activate()
        {
            this.regionManager.Regions[RegionNames.MainRegion].Activate(this.View);
        }

        /// <summary>
        /// Deletes the Project having the given Uri.
        /// </summary>
        /// <param name="projectUri">The Uri of the project.</param>
        private void Delete(object projectUri)
        {
            Uri uri = projectUri as Uri;

            if (uri != null)
            {
                this.deletedProjectUri = uri;
                this.serviceFacade.DeleteProject(uri);
            }
        }

        private void OnGetProjectsCompleted(object sender, Infrastructure.DataEventArgs<List<Project>> e)
        {
            var retrivedProjects = new ObservableCollection<Project>();

            if (e.Data != null)
            {
                Uri currentProjectId = this.configurationService.GetProjectId();

                e.Data.ForEach(
                    x =>
                    {
                        if (currentProjectId == null || x.ProviderUri != currentProjectId)
                        {
                            retrivedProjects.Add(x);
                        }
                    });
            }

            this.Projects = retrivedProjects;
        }

        private void LoadProjectList(object sender, Infrastructure.DataEventArgs<bool> dataEventArgs)
        {
            this.serviceFacade.GetProjectsByUserAsync(this.configurationService.GetUserName());
        }

        private void ExecuteKeyboardAction(Tuple<KeyboardAction, object> parameter)
        {
            switch (parameter.Item1)
            {
                case KeyboardAction.ActivateModel:
                    this.Activate();
                    break;
            }
        }
    }
}