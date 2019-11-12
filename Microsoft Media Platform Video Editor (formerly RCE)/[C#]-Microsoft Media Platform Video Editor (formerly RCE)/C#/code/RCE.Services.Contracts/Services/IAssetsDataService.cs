// <copyright file="IAssetsDataService.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IAssetsDataService.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Services.Contracts
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Web;

    /// <summary>
    /// Interface that defines the assets operations that are available to the RCE.
    /// </summary>
    [ServiceContract(Namespace = "http://schemas.microsoft.com/rce/")]
    public interface IAssetsDataService
    {
        /// <summary>
        /// Returns back given no. of items from the library.
        /// </summary>
        /// <param name="maxNumberOfItems">Maximum no. of records in the result.</param>
        /// <returns>A <see cref="Container"/> with the items.</returns>
        [OperationContract(Name = "LoadLibraryWithLimitedItems"),]
        [WebGet(BodyStyle = WebMessageBodyStyle.Wrapped)]
        Container LoadLibrary(int maxNumberOfItems);

        /// <summary>
        /// Returns back all of the items that are contained in the library filtering them using the filter provided.
        /// </summary>
        /// <param name="filter">The filter used to get the items.</param>
        /// <param name="maxNumberOfItems">Maximum no. of records in the result.</param>
        /// <returns>A <see cref="Container"/> with the items.</returns>
        [OperationContract(Name = "LoadLibraryFilter")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Wrapped)]
        Container LoadLibrary(string filter, int maxNumberOfItems);

        /// <summary>
        /// Returns back given no. of items from the library.
        /// </summary>
        /// <param name="libraryId">The <see cref="Uri"/> of the container to load from the library.</param>
        /// <param name="maxNumberOfItems">Maximum no. of records in the result.</param>
        /// <returns>A <see cref="Container"/> with the items.</returns>
        [OperationContract(Name = "LoadLibraryById"),]
        [WebGet(BodyStyle = WebMessageBodyStyle.Wrapped)]
        Container LoadLibraryById(Uri libraryId, int maxNumberOfItems);

        /// <summary>
        /// Returns back given no. of items from the library.
        /// </summary>
        /// <param name="libraryId">The <see cref="Uri"/> of the container to load from the library.</param>
        /// <param name="filter">The filter used to get the items.</param>
        /// <param name="maxNumberOfItems">Maximum no. of records in the result.</param>
        /// <returns>A <see cref="Container"/> with the items.</returns>
        [OperationContract(Name = "LoadLibraryByIdFilter"),]
        [WebGet(BodyStyle = WebMessageBodyStyle.Wrapped)]
        Container LoadLibraryById(Uri libraryId, string filter, int maxNumberOfItems);
    }
}
