// <copyright file="ILibraryView.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ILibraryView.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Library
{
    using System.Collections.Generic;

    /// <summary>
    /// Inteface for library view.
    /// </summary>
    public interface ILibraryView
    {
        /// <summary>
        /// Gets or sets the model of the view.
        /// </summary>
        /// <value>The <see cref="ILibraryViewPresentationModel"/>.</value>
        ILibraryViewPresentationModel Model { get; set; }

        /// <summary>
        /// It addes the metadata fields in the list view of the assets.
        /// </summary>
        /// <param name="metadataFields">The list of metadata fields.</param>
        void AddMetadataFields(IList<string> metadataFields);

        /// <summary>
        /// Shows a progress bar.
        /// </summary>
        void ShowProgressBar();

        /// <summary>
        /// Hides the progress bar.
        /// </summary>
        void HideProgressBar();
    }
}