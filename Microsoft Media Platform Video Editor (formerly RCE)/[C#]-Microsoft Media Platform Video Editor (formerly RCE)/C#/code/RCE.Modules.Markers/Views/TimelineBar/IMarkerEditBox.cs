// <copyright file="IMarkerEditBox.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IMarkerEditBox.xaml.cs                     
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
    /// Defines the interface for the Ad Edit Box.
    /// </summary>
    public interface IMarkerEditBox
    {
        /// <summary>
        /// Gets or sets the <see cref="IMarkerEditBoxPresentationModel"/> associated with the ad edit box.
        /// </summary>
        /// <value>The <see cref="IMarkerEditBoxPresentationModel"/> associated with the ad edit box.</value>
        IMarkerEditBoxPresentationModel Model { get; set; }
        
        /// <summary>
        /// Closes the edit box.
        /// </summary>
        void Close();

        /// <summary>
        /// Shows the edit box.
        /// </summary>
        void Show();
    }
}