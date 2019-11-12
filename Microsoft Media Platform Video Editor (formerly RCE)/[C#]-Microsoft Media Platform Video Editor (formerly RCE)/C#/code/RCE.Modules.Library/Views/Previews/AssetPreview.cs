// <copyright file="AssetPreview.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: AssetPreview.cs                     
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
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using Infrastructure.Models;
   
    /// <summary>
    /// Base class for all the <see cref="ImageAsset"/>, <see cref="AudioPreview"/>,
    ///  <see cref="VideoPreview"/>, <see cref="FolderPreview"/> preview controls.
    /// </summary>
    public class AssetPreview : UserControl
    {
        /// <summary>
        /// Occurs when [adding asset].
        /// </summary>
        public virtual event EventHandler AddingAsset;

        /// <summary>
        /// Gets or sets the asset.
        /// </summary>
        /// <value>The asset.</value>
        public virtual Asset Asset { get; set; }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public virtual void Stop()
        {
        }

        /// <summary>
        /// Scales the current preview control to the specified size.
        /// </summary>
        /// <param name="size">The size to which the preview control is to be scaled.</param>
        public virtual void Scale(Size size)
        {
        }

        public void OnAddingAsset()
        {
            EventHandler handler = this.AddingAsset;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}
