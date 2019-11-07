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

namespace RCE.Modules.Markers.Tests.Mocks
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
            throw new NotImplementedException();
        }
    }
}