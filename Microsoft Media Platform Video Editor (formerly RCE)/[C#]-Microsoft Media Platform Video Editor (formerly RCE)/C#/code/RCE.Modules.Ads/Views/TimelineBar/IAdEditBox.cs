// <copyright file="IAdEditBox.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IAdEditBox.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Ads
{
    public interface IAdEditBox
    {
        /// <summary>
        /// Gets or sets the <see cref="IAdEditBoxPresentationModel"/> associated with the ad edit box.
        /// </summary>
        /// <value>The <see cref="IAdEditBoxPresentationModel"/> associated with the ad edit box.</value>
        IAdEditBoxPresentationModel Model { get; set; }

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
