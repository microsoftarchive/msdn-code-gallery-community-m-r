// <copyright file="DownloaderManagerEventArgs.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: DownloaderManagerEventArgs.cs                     
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
    using System.IO;

    public class DownloaderManagerEventArgs : EventArgs
    {
        public DownloaderManagerEventArgs(Stream stream, Uri uri)
        {
            this.Stream = stream;
            this.ManifestUri = uri;
        }

        public Stream Stream { get; private set; }

        public Uri ManifestUri { get; private set; }
    }
}
