  // <copyright file="MockProjectService.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockProjectService.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Services.Tests.Mocks
{
    using System;
    using Infrastructure;
    using Infrastructure.Models;

    using RCE.Infrastructure.Services;

    public class MockProjectService : IProjectService
    {
        private readonly Project project = new Project();

        public MockProjectService()
        {
            this.State = ProjectState.Retrieved;
        }

        public event EventHandler ProjectRetrieved;

        public event EventHandler ProjectError;
        
        public event EventHandler ProjectSaving;

        public event EventHandler<DataEventArgs<bool>> ProjectSaved;

        public ProjectState State { get; set; }

        public bool SaveProjectCalled { get; set; }

        public Project GetCurrentProject()
        {
            return this.project;
        }

        public void SaveProject()
        {
            this.SaveProjectCalled = true;
        }

        public Sequence CreateTimeline()
        {
            return null;
        }

        public void InvokeProjectRetrieved()
        {
            EventHandler projectRetrievedHandler = this.ProjectRetrieved;
            if (projectRetrievedHandler != null)
            {
                projectRetrievedHandler(this, EventArgs.Empty);
            }
        }

        public void InvokeProjectError()
        {
            EventHandler error = this.ProjectError;
            if (error != null)
            {
                error(this, EventArgs.Empty);
            }
        }

        public void InvokeProjectSaving()
        {
            EventHandler handler = this.ProjectSaving;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public void InvokeProjectSaved(bool saved)
        {
            EventHandler<DataEventArgs<bool>> handler = this.ProjectSaved;
            if (handler != null)
            {
                handler(this, new DataEventArgs<bool>(saved));
            }
        }
    }
}