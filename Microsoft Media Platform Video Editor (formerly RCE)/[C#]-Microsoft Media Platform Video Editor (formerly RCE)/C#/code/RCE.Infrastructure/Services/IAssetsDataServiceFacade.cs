// <copyright file="IAssetsDataServiceFacade.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IAssetsDataServiceFacade.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Services
{
    using System;
    using System.Collections.Generic;

    using RCE.Infrastructure.Models;

    /// <summary>
    /// Assets Data service interface which interacts with the server to get the data.
    /// </summary>
    public interface IAssetsDataServiceFacade
    {
        /// <summary>
        /// Occurs when [load assets completed].
        /// </summary>
        event EventHandler<DataEventArgs<List<Asset>>> LoadAssetsCompleted;

        /// <summary>
        /// Occurs when [load assets by id completed].
        /// </summary>
        event EventHandler<DataEventArgs<List<Asset>>> LoadAssetsByLibraryIdCompleted;

        /// <summary>
        /// Loads the assets asynchronously.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="maxNumberOfItems">The max number of items.</param>
        void LoadAssetsAsync(string filter, int maxNumberOfItems);

        /// <summary>
        /// Loads the assets asynchronously.
        /// </summary>
        /// <param name="maxNumberOfItems">The max number of items in the result.</param>
        void LoadAssetsAsync(int maxNumberOfItems);

        /// <summary>
        /// Loads the assets asynchronously.
        /// </summary>
        /// <param name="libraryId">The id of the library to load.</param>
        /// <param name="maxNumberOfItems">The max number of items in the result.</param>
        void LoadAssetsByLibraryIdAsync(Uri libraryId, int maxNumberOfItems);
    }
}