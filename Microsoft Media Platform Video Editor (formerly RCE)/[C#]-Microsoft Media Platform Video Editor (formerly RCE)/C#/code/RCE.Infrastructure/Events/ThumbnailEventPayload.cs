// <copyright file="ThumbnailEventPayload.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ThumbnailEventPayload.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Events
{
    using System.Windows.Media.Imaging;
    using Models;

    public class ThumbnailEventPayload
    {
        public ThumbnailEventPayload(WriteableBitmap writeableBitmap, ThumbnailType thumbnailType)
        {
            this.Bitmap = writeableBitmap;

            this.Type = thumbnailType;
        }

        public WriteableBitmap Bitmap { get; set; }

        public ThumbnailType Type { get; set; }
    }
}
