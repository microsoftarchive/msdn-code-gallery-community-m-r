// <copyright file="IMetadataStrategy.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IMetadataStrategy.cs                     
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

    /// <summary>
    /// Interface that defines the operations for the metadata strategies.
    /// </summary>
    public interface IMetadataStrategy
    {
        event EventHandler<MetadataEventArgs> GetManifestCompleted;

        /// <summary>
        /// Determines wnether the strategy can retrieve the metadata of the provided target.
        /// </summary>
        /// <param name="target">The object to determine if the strategy can be used.</param>
        /// <returns>A true if the strategy can retrieve the metadate of the provided target;otherwise false.</returns>
        bool CanRetrieveMetadata(object target);

        /// <summary>
        /// Gets the metadata of the provided target.
        /// </summary>
        /// <param name="target">The object to get the metadata from.</param>
        void GetMetadata(object target);
    }
}