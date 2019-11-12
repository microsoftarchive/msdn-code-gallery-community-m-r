// <copyright file="IThumbnailService.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IThumbnailService.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Services
{
    using RCE.Infrastructure.Models;

    using SMPTETimecode;

    /// <summary>
    /// Service used to retrieve thumbnails from assets.
    /// </summary>
    public interface IThumbnailService
    {
        /// <summary>
        /// Gets the thumbnail of an asset.
        /// </summary>
        /// <param name="asset">The asset to extract the thumbnail from.</param>
        /// <returns>The thumbnail uri string.</returns>
        string GetThumbnailSource(Asset asset);

        /// <summary>
        /// Gets the thumbnail frame of the asset at the specific position.
        /// </summary>
        /// <param name="asset">The asset to extract the thumbnail from.</param>
        /// <returns>The thumbnail uri string.</returns>
        /// <param name="timeCode">The timecode to get the frame.</param>
        string GetThumbnailSource(Asset asset, TimeCode timeCode);

        /// <summary>
        /// Gets the thumbnail of an asset.
        /// </summary>
        /// <returns>The thumbnail uri string.</returns>
        /// <param name="asset">The asset to extract the thumbnail from.</param>
        /// <param name="seconds">The seconds.</param>
        /// <param name="width">The width of the image.</param>
        /// <param name="height">The height of the image.</param>
        string GetThumbnailSource(Asset asset, int seconds, double width, double height);
    }
}
