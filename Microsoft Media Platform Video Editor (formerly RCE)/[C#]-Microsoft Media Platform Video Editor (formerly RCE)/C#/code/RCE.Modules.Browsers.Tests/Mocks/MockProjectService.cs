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

namespace RCE.Modules.Browsers.Tests.Mocks
{
    using System;

    using RCE.Infrastructure;
    using RCE.Infrastructure.Models;
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

        public Project GetCurrentProjectReturnValue { get; set; }

        public ProjectState State { get; set; }

        public bool TimelineCreated { get; set; }

        public Sequence CreatedSequence { get; set; }

        public bool SaveCalled { get; set; }

        public Project GetCurrentProject()
        {
            return this.GetCurrentProjectReturnValue;
        }

        public void SaveProject()
        {
            this.SaveCalled = true;
        }

        public Sequence CreateTimeline()
        {
            this.CreatedSequence = new Sequence();
            this.TimelineCreated = true;
            return this.CreatedSequence;
        }

        public void InvokeProjectRetrieved()
        {
            this.ProjectRetrieved(this, null);
        }

        public void InvokeProjectSaved()
        {
            this.ProjectSaved(this, null);
        }
    }
}