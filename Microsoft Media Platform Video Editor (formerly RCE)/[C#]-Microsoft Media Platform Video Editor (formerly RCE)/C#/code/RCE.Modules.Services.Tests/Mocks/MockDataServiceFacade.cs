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

namespace RCE.Modules.Services.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
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

        public bool SaveProjectAsyncCalled { get; set; }

        public bool LoadProjectAsyncCalled { get; set; }

        public bool ThrowException { get; set; }

        public void LoadMediaBinAssetsAsync(Uri containerUri)
        {
            throw new System.NotImplementedException();
        }

        public void GetProjectsByUserAsync(string userName)
        {
            throw new System.NotImplementedException();
        }

        public void LoadProjectAsync(Uri projectUri)
        {
            this.LoadProjectAsyncCalled = true;
        }

        public void SaveProjectAsync(Project project)
        {
            this.SaveProjectAsyncCalled = true;
        }

        public void LoadTitleTemplatesAsync()
        {
            throw new System.NotImplementedException();
        }

        public void InvokeLoadProjectCompleted(Project project)
        {
            EventHandler<DataEventArgs<Project>> loadProjectCompletedHandler = this.LoadProjectCompleted;
            if (loadProjectCompletedHandler != null)
            {
                if (this.ThrowException)
                {
                    loadProjectCompletedHandler(this, new DataEventArgs<Project>(project, new Exception()));                   
                }
                else
                {
                    loadProjectCompletedHandler(this, new DataEventArgs<Project>(project));
                }
            }
        }

        public void DeleteProject(Uri site)
        {
            throw new System.NotImplementedException();
        }

        public void InvokeSaveProjectCompleted(bool saved)
        {
            EventHandler<DataEventArgs<bool>> completed = this.SaveProjectCompleted;
            if (completed != null)
            {
                completed(this, new DataEventArgs<bool>(saved));
            }
        }
    }
}