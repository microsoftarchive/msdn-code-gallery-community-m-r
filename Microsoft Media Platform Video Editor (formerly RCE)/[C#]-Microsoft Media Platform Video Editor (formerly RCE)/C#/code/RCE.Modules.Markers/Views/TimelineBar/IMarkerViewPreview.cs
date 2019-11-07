// <copyright file="IMarkerViewPreview.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IMarkerViewPreview.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Markers
{
    /// <summary>
    /// Defines the interface for the ad preview.
    /// </summary>
    public interface IMarkerViewPreview
    {
        /// <summary>
        /// Gets or sets the <see cref="IMarkerEditBoxPresentationModel"/> associated with the preview.
        /// </summary>
        /// <value>The <see cref="IMarkerEditBoxPresentationModel"/> associated with the preview.</value>
        IMarkerEditBoxPresentationModel Model { get; set; }
    }
}