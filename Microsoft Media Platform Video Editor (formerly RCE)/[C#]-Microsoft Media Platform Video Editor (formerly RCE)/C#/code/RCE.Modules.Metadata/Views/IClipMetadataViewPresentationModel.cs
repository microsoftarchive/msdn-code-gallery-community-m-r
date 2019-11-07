// <copyright file="IClipMetadataViewPresentationModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IClipMetadataViewPresentationModel.cs                     
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
    using System.Collections.Generic;
    using RCE.Infrastructure.Models;

    /// <summary>
    /// Interface for the Metadata information Model.
    /// </summary>
    public interface IClipMetadataViewPresentationModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether the metadata information region displaying 
        /// for the asset metadata will be visible or not.
        /// </summary>
        /// <value>A <c>true</c> if the metadata region is visible;otherwise false.</value>
        bool ShowMetadataInformation { get; set; }

        /// <summary>
        /// Gets or sets the value of the [View] as IClipMetadataView.
        /// </summary>
        /// <value>The metadata view.</value>
        IClipMetadataView View { get; set; }

        /// <summary>
        /// Gets the value of the list of <see cref="AssetMetadata"/> for the given asset.
        /// </summary>
        /// <value>The metadata details for an asset.</value>
        IList<AssetMetadata> AssetMetadataDetails { get; }
    }
}
