// <copyright file="IMetadataLocator.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IMetadataLocator.cs                     
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
    /// <summary>
    /// Interface that defines the metadata locator operations.
    /// </summary>
    public interface IMetadataLocator
    {
        /// <summary>
        /// Gets the metadata of the provided target.
        /// </summary>
        /// <param name="target">The object to get the metadata from.</param>
        /// <returns>The metadata of the provided target.</returns>
        Metadata GetMetadata(object target);
    }
}