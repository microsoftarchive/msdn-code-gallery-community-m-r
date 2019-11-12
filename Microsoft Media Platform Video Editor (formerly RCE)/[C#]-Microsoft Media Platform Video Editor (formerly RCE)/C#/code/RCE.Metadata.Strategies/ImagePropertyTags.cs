// <copyright file="ImagePropertyTags.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ImagePropertyTags.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Metadata.Strategies
{
    /// <summary>
    /// Partially defines the Microsoft Windows GDI+ Image property tags defined at <see cref="http://msdn.microsoft.com/en-us/library/ms534413(VS.85).aspx"/>
    /// </summary>
    public enum ImagePropertyTags : int
    {
        /// <summary>
        /// No available tag.
        /// </summary>
        None = 0,

        /// <summary>
        /// Number of pixels per row.
        /// </summary>
        ImageWidth = 256,

        /// <summary>
        /// Number of pixel rows.
        /// </summary>
        ImageHeight = 257,

        /// <summary>
        /// Null-terminated character string that specifies the title of the image.
        /// </summary>
        ImageTitle = 800,

        /// <summary>
        /// Information specific to compressed data. When a compressed file is recorded, the valid width of the meaningful image must be recorded in this tag, 
        /// whether or not there is padding data or a restart marker. This tag should not exist in an uncompressed file.
        /// </summary>
        ExifPixXDim = 40962,

        /// <summary>
        /// Information specific to compressed data. When a compressed file is recorded, the valid height of the meaningful image must be recorded in this tag 
        /// whether or not there is padding data or a restart marker. This tag should not exist in an uncompressed file. 
        /// Because data padding is unnecessary in the vertical direction, the number of lines recorded in this valid image height tag will be the same as that recorded in the SOF.
        /// </summary>
        ExifPixYDim = 40963,
    }
}
