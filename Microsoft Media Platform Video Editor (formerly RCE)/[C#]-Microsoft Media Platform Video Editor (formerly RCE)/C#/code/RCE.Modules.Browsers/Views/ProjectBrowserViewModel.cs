// <copyright file="ProjectBrowserViewModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ProjectBrowserViewModel.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Browsers.Views
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    using Microsoft.Practices.Composite.Events;
    using Microsoft.Practices.Composite.Presentation.Commands;

    using RCE.Infrastructure;
    using RCE.Infrastructure.Events;
    using RCE.Infrastructure.Models;
    using RCE.Infrastructure.Services;
    using RCE.Infrastructure.Windows;

    public class ProjectBrowserViewModel : BaseModel, IProjectBrowserViewModel, IWindowMetadataProvider, IKeyboardAware
    {
        private readonly IProjectService projectService;

        private readonly ISequenceRegistry sequenceRegistry;

        private readonly IEventAggregator eventAggregator;

        private readonly DelegateCommand<Tuple<KeyboardAction, object>> handleKeyboardActionCommand;
        
        private string status;

        private Func<IErrorView> errorViewResolver;

        private bool treatGapAsErrors;

        public ProjectBrowserViewModel(IProjectBrowserView projectBrowserView, IProjectService projectService, ISequenceRegistry sequenceRegistry, IEventAggregator eventAggregator, Func<IErrorView> errorViewResolver, IConfigurationService configurationService)
        {
            this.NewSequenceCommand = new DelegateCommand<object>(this.CreateNewSequence);
            this.SaveProjectCommand = new DelegateCommand<object>(this.SaveProject);
            this.handleKeyboardActionCommand = new DelegateCommand<Tuple<KeyboardAction, object>>(this.HandleKeyboardAction);
            this.projectService = projectService;
            this.sequenceRegistry = sequenceRegistry;
            this.errorViewResolver = errorViewResolver;
            eventAggregator.GetEvent<StatusEvent>().Subscribe(this.UpdateStatus);
            eventAggregator.GetEvent<ResetWindowsEvent>().Subscribe(this.ResetWindow);
            this.eventAggregator = eventAggregator;
            this.projectService.ProjectRetrieved += this.HandleProjectChange;
            this.projectService.ProjectSaved += this.HandleProjectChange;
            projectBrowserView.SetViewModel(this);

            this.treatGapAsErrors = configurationService.GetTreatGapAsError();

            this.View = projectBrowserView;
        }

        public event EventHandler<Infrastructure.DataEventArgs<object>> TitleUpdated;

        public event EventHandler<Infrastructure.DataEventArgs<object>> ResetPositionRaised;

        public object View
        {
            get; private set;
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
                if (this.projectService.State != ProjectState.Retrieved || this.projectService.GetCurrentProject() == null)
                {
                    return string.Empty;
                }

                return string.Format("Project: {0}", this.projectService.GetCurrentProject().Name);
            }
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
                return new Size(450, 300);
            }
        }

        public ICommand NewSequenceCommand { get; set; }

        public ICommand SaveProjectCommand { get; set; }

        public string Status
        {
            get
            {
                return this.status;
            }

            private set
            {
                this.status = value;
                this.OnPropertyChanged("Status");
            }
        }

        public DelegateCommand<Tuple<KeyboardAction, object>> KeyboardActionCommand
        {
            get
            {
                return this.handleKeyboardActionCommand;
            }
        }

        public KeyboardActionContext ActionContext
        {
            get
            {
                var viewModel = this.SelectedView.DataContext as IKeyboardAware;
                if (viewModel != null)
                {
                    return viewModel.ActionContext;
                }

                return KeyboardActionContext.AssetBrowser;
            }
        }

        public FrameworkElement SelectedView { get; set; }

        public void ResetWindow(object obj)
        {
            EventHandler<Infrastructure.DataEventArgs<object>> handler = this.ResetPositionRaised;

            if (handler != null)
            {
                handler.Invoke(this, new Infrastructure.DataEventArgs<object>(this.View));
            }
        }

        public void UpdateStatus(string s)
        {
            this.Status = s;
        }

        private void HandleKeyboardAction(Tuple<KeyboardAction, object> obj)
        {
        }

        private void HandleProjectChange(object sender, EventArgs e)
        {
            this.RaiseTitleChanged();
        }

        private void RaiseTitleChanged()
        {
            EventHandler<Infrastructure.DataEventArgs<object>> handler = this.TitleUpdated;
            if (handler != null)
            {
                handler(this, new Infrastructure.DataEventArgs<object>(this.View));
            }
        }

        private void CreateNewSequence(object obj)
        {
            Sequence sequence = this.projectService.CreateTimeline();
            this.projectService.GetCurrentProject().AddTimeline(sequence);
            this.sequenceRegistry.CreateSequence(sequence);
        }

        private void SaveProject(object obj)
        {
            if (this.treatGapAsErrors && this.SequenceHasGap())
            {
                this.ShowError();
                return;
            }

            this.eventAggregator.GetEvent<SaveProjectEvent>().Publish(null);
        }

        private void ShowError()
        {
            IErrorView errorView = this.errorViewResolver();
            errorView.ErrorMessage = "The project you are trying to save contains gaps and that is not allowed. Please remove the gaps and try again.";
            errorView.Show();
        }

        private bool SequenceHasGap()
        {
            return this.sequenceRegistry.CurrentSequenceModel.SequenceHasGap();
        }
    }
}
