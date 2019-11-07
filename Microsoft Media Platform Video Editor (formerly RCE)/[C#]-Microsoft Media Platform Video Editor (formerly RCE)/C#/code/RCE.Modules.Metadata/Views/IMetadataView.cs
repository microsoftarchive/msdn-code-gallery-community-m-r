// <copyright file="IMetadataView.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IMetadataView.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Metadata
{
    /// <summary>
    /// Interface for the view.
    /// </summary>
    public interface IMetadataView
    {
        /// <summary>
        /// Gets or sets the value for the <see cref="IMetadataViewPresentationModel"/>.
        /// </summary>
        /// <value>The model.</value>
        IMetadataViewPresentationModel Model { get; set; }
    }
}