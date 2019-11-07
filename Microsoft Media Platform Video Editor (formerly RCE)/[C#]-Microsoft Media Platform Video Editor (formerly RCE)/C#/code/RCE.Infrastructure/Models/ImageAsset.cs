// <copyright file="ImageAsset.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ImageAsset.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Models
{
    using System;

    /// <summary>
    /// A class that represents an image asset.
    /// </summary>
    public class ImageAsset : Asset
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageAsset"/> class.
        /// </summary>
        public ImageAsset()
        {
            this.Id = Guid.NewGuid();
        }

        /// <summary>
        /// Gets or sets the height of the Image.
        /// </summary>
        /// <value>The height of the image.</value>
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the width of the Image.
        /// </summary>
        /// <value>The width of the image.</value>
        public int Width { get; set; }

        /// <summary>
        /// Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
        /// </summary>
        /// <returns> A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</returns>
        public override string ToString()
        {
            return "Image";
        }
    }
}
