// <copyright file="MockDataServiceFacade.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockDataServiceFacade.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Projects.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using Infrastructure;
    using Infrastructure.Models;

    using RCE.Infrastructure.Services;

    public class MockDataServiceFacade : IDataServiceFacade
    {
        public event EventHandler<DataEventArgs<MediaBin>> LoadMediaBinAssetCompleted;

        public event EventHandler<DataEventArgs<Project>> LoadProjectCompleted;

        public event EventHandler<DataEventArgs<bool>> SaveProjectCompleted;

        public event EventHandler<DataEventArgs<List<TitleTemplate>>> LoadTitleTemplatesCompleted;

        public event EventHandler<DataEventArgs<List<Project>>> GetProjectsByUserCompleted;

        /// <summary>
        /// The handler that invokes when DeleteProject method completed.
        /// </summary>
        public event EventHandler<DataEventArgs<bool>> DeleteProjectCompleted;

        public bool GetProjectsByUserAsyncCalled { get; set; }

        public bool DeleteProjectCalled { get; set; }

        public List<Project> Projects { get; set; }

        public bool DeleteProjectResult { get; set; }

        public void GetProjectsByUserAsync(string userName)
        {
            this.GetProjectsByUserAsyncCalled = true;
            this.GetProjectsByUserCompleted(this, new DataEventArgs<List<Project>>(this.Projects));
        }

        public void LoadMediaBinAssetsAsync(Uri containerUri)
        {
            throw new System.NotImplementedException();
        }

        public void LoadProjectAsync(Uri projectUri)
        {
            throw new System.NotImplementedException();
        }

        public void SaveProjectAsync(Project project)
        {
            throw new System.NotImplementedException();
        }

        public void LoadTitleTemplatesAsync()
        {
            throw new System.NotImplementedException();
        }

        public void InvokeGetProjectsByUserCompleted(DataEventArgs<List<Project>> e)
        {
            EventHandler<DataEventArgs<List<Project>>> getProjectsByUserHandler = this.GetProjectsByUserCompleted;
            if (getProjectsByUserHandler != null)
            {
                getProjectsByUserHandler(this, e);
            }
        }

        public void DeleteProject(Uri site)
        {
            this.DeleteProjectCalled = true;
            this.DeleteProjectCompleted(this, new DataEventArgs<bool>(this.DeleteProjectResult));
        }
    }
}