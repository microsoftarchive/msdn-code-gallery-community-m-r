// <copyright file="IMediaBinView.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IMediaBinView.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.MediaBin
{
    using System.Collections.Generic;

    /// <summary>
    /// View of the MediaBin module.
    /// </summary>
    public interface IMediaBinView
    {
        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>The model.</value>
        IMediaBinViewPresentationModel Model { get; set; }

        /// <summary>
        /// It addes the metadata fields in the list view of the assets.
        /// </summary>
        /// <param name="metadataFields">The list of metadataFields.</param>
        void AddMetadataFields(IList<string> metadataFields);

        /// <summary>
        /// Shows a progress bar.
        /// </summary>
        void ShowProgressBar();

        /// <summary>
        /// Hides the progress bar.
        /// </summary>
        void HideProgressBar();

        /// <summary>
        /// Shows the messagebox and gets the delete asset confirmation.
        /// </summary>
        void GetDeleteAssetConfirmation();
    }
}