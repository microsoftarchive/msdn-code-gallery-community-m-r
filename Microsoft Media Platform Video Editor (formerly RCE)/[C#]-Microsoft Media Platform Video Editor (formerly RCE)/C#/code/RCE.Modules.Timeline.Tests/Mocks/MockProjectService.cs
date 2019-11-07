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

namespace RCE.Modules.Timeline.Tests.Mocks
{
    using System;
    using Infrastructure;
    using Infrastructure.Models;

    using RCE.Infrastructure.Services;

    public class MockProjectService : IProjectService
    {
        public MockProjectService()
        {
            this.State = ProjectState.Retrieved;
            this.GetCurrentProjectReturnValue = new Project();
        }

        public event EventHandler ProjectRetrieved;

        public event EventHandler ProjectError;
        
        public event EventHandler ProjectSaving;
        
        public event EventHandler<DataEventArgs<bool>> ProjectSaved;

        public Sequence CreatedSequence { get; set; }

        public bool TimelineCreated { get; set; }

        public Project GetCurrentProjectReturnValue { get; set; }

        public ProjectState State { get; set; }

        public Project GetCurrentProject()
        {
            return this.GetCurrentProjectReturnValue;
        }

        public void SaveProject()
        {
            throw new System.NotImplementedException();
        }

        public Sequence CreateTimeline()
        {
            this.TimelineCreated = true;
            this.CreatedSequence = new Sequence();
            return this.CreatedSequence;
        }

        public void InvokeProjectRetrieved()
        {
            EventHandler projectRetrievedHandler = this.ProjectRetrieved;
            if (projectRetrievedHandler != null)
            {
                projectRetrievedHandler(this, EventArgs.Empty);
            }
        }
    }
}