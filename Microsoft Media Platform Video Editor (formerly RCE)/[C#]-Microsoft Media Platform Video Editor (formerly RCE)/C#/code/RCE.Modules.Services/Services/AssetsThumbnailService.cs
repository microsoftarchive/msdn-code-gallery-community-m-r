// <copyright file="AssetsThumbnailService.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: AssetsThumbnailService.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Services
{
    using RCE.Infrastructure;
    using RCE.Infrastructure.Models;
    using RCE.Infrastructure.Services;

    using SMPTETimecode;

    /// <summary>
    /// Service used for the Olympics to retrieve the assets thumbnails.
    /// </summary>
    public class AssetsThumbnailService : IThumbnailService
    {
        /// <summary>
        /// Contains the default image being used in case that the asset image does not exist.
        /// </summary>
        private readonly string defaultImage;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssetsThumbnailService"/> class.
        /// </summary>
        /// <param name="configurationService">The configuration service being used to retrieve the default image.</param>
        public AssetsThumbnailService(IConfigurationService configurationService)
        {
            this.defaultImage = configurationService.GetParameterValue("DefaultThumbnailImageUri");
        }

        /// <summary>
        /// Gets the thumbnail of an asset.
        /// </summary>
        /// <param name="asset">The asset to extract the thumbnail from.</param>
        /// <returns>The thumbnail uri string.</returns>
        public string GetThumbnailSource(Asset asset)
        {
            VideoAsset videoAsset = asset as VideoAsset;

            return videoAsset != null && !string.IsNullOrEmpty(videoAsset.ThumbnailSource) ? videoAsset.ThumbnailSource : this.defaultImage;
        }

        /// <summary>
        /// Gets the thumbnail frame of the asset at the specific position.
        /// </summary>
        /// <param name="asset">The asset to extract the thumbnail from.</param>
        /// <returns>The thumbnail uri string.</returns>
        /// <param name="timeCode">The timecode to get the frame.</param>
        public string GetThumbnailSource(Asset asset, TimeCode timeCode)
        {
            return this.GetThumbnailSource(asset);
        }

        /// <summary>
        /// Gets the thumbnail of an asset.
        /// </summary>
        /// <returns>The thumbnail uri string.</returns>
        /// <param name="asset">The asset to extract the thumbnail from.</param>
        /// <param name="seconds">The seconds.</param>
        /// <param name="width">The width of the image.</param>
        /// <param name="height">The height of the image.</param>
        public string GetThumbnailSource(Asset asset, int seconds, double width, double height)
        {
            return this.GetThumbnailSource(asset);
        }
    }
}
