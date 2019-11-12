// <copyright file="MockAssetsDataServiceFacade.cs" company="Microsoft Corporation">
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

    public class MockAssetsDataServiceFacade : IAssetsDataServiceFacade
    {
        public MockAssetsDataServiceFacade()
        {
            this.Assets = new ObservableCollection<Asset>();
        }

        public event EventHandler<DataEventArgs<List<Asset>>> LoadAssetsCompleted;

        public event EventHandler<DataEventArgs<List<Asset>>> LoadAssetsByLibraryIdCompleted;

        public ObservableCollection<Asset> Assets { get; set; }

        public bool LoadAssetsByIdAsyncCalled { get; set; }

        public void LoadAssetsAsync(string filter, int maxNumberOfItems)
        {
            throw new System.NotImplementedException();
        }

        public void LoadAssetsAsync(int maxNumberOfItems)
        {
            throw new System.NotImplementedException();
        }

        public void LoadAssetsByLibraryIdAsync(Uri libraryId, int maxNumberOfItems)
        {
            this.LoadAssetsByIdAsyncCalled = true;
        }
    }
}