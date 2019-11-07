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

namespace RCE.Modules.MediaBin.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Infrastructure;
    using Infrastructure.Models;

    using RCE.Infrastructure.Services;

    public class MockDataServiceFacade : IDataServiceFacade
    {
        public MockDataServiceFacade()
        {
            this.Assets = new ObservableCollection<Asset>();
        }

        public event EventHandler<DataEventArgs<MediaBin>> LoadMediaBinAssetCompleted;

        public event EventHandler<DataEventArgs<Project>> LoadProjectCompleted;

        public event EventHandler<DataEventArgs<bool>> SaveProjectCompleted;

        public event EventHandler<DataEventArgs<List<TitleTemplate>>> LoadTitleTemplatesCompleted;

        public event EventHandler<DataEventArgs<List<Project>>> GetProjectsByUserCompleted;

        /// <summary>
        /// The handler that invokes when DeleteProject method completed.
        /// </summary>
        public event EventHandler<DataEventArgs<bool>> DeleteProjectCompleted;

        public ObservableCollection<Asset> Assets { get; set; }

        public bool LoadMediaBinAssetAyncCalled { get; set; }

        public bool LoadAssetsAsyncCalled { get; set; }

        public void GetProjectsByUserAsync(string userName)
        {
            throw new System.NotImplementedException();
        }

        public void LoadMediaBinAssetsAsync(Uri containerUri)
        {
            this.LoadMediaBinAssetAyncCalled = true;
            MediaBin mediaBin = new MediaBin();
            mediaBin.AddAssets(this.Assets);
            this.LoadMediaBinAssetCompleted(this, new DataEventArgs<MediaBin>(mediaBin));
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

        public void DeleteProject(Uri site)
        {
            throw new System.NotImplementedException();
        }
    }
}