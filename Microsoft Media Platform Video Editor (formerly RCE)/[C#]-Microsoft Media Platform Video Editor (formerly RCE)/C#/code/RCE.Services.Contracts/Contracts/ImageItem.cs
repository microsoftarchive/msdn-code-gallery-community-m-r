// <copyright file="ImageItem.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ImageItem.cs                     
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
    using System.Runtime.Serialization;

    /// <summary>
    /// A class that represents an Image.
    /// </summary>
#if !SILVERLIGHT
    [Serializable]
#endif
    [DataContract(Namespace = "http://schemas.microsoft.com/rce/")]
    public class ImageItem : MediaItem
    {
        /// <summary>
        /// Gets or sets the height of the Image.
        /// </summary>
        /// <value>The height of the image.</value>
        [DataMember]
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the width of the Image.
        /// </summary>
        /// <value>The width of the image.</value>
        [DataMember]
        public int Width { get; set; }
    }
}
