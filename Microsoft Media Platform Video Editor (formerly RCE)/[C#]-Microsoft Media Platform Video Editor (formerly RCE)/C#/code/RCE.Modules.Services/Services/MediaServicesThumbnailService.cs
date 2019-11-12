// <copyright file="MediaServicesThumbnailService.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MediaServicesThumbnailService.cs                     
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
    using System;
    using System.Globalization;
    using Infrastructure;
    using Infrastructure.Models;

    using RCE.Infrastructure.Services;

    using SMPTETimecode;

    /// <summary>
    /// Service that get the asset thumbnail image using the IMM Media services.
    /// </summary>
    public class MediaServicesThumbnailService : IThumbnailService
    {
        /// <summary>
        /// The frame template.
        /// </summary>
        private const string GetFrameTemplate = "GetFrame?id={0}&usecache=true";

        /// <summary>
        /// Pattern used for retrieving a frame.
        /// </summary>
        private const string GetFrameTimeCodeTemplate = "GetFrame?id={0}&timecode={1}&usecache=true&outputFormat=PNG";

        /// <summary>
        /// Url for getting the frame of the asset.
        /// </summary>
        private const string GetFrameSecondsTemplate = "GetFrame?id={0}&seconds={1}&width={2}&height={3}&usecache=true&template=filmstrip&outputFormat=PNG";

        /// <summary>
        /// The media services url.
        /// </summary>
        private readonly Uri mediaServicesUri;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaServicesThumbnailService"/> class.
        /// </summary>
        /// ><param name="configurationService">The configuration service used to retrieve the media services url.</param>
        public MediaServicesThumbnailService(IConfigurationService configurationService)
        {
            this.mediaServicesUri = GetMediaServicesUri(configurationService);
        }

        /// <summary>
        /// Gets the thumbnail of an asset.
        /// </summary>
        /// <param name="asset">The asset to extract the thumbnail from.</param>
        /// <returns>The thumbnail uri string.</returns>
        public string GetThumbnailSource(Asset asset)
        {
            string getFrame = string.Format(CultureInfo.InvariantCulture, GetFrameTemplate, asset.ProviderUri);
            
            return string.Concat(this.mediaServicesUri.ToString(), getFrame);
        }

        /// <summary>
        /// Gets the thumbnail frame of the asset at the specific position.
        /// </summary>
        /// <param name="asset">The asset to extract the thumbnail from.</param>
        /// <returns>The thumbnail uri string.</returns>
        /// <param name="timeCode">The timecode to get the frame.</param>
        public string GetThumbnailSource(Asset asset, TimeCode timeCode)
        {
            string getFrame = string.Format(CultureInfo.InvariantCulture, GetFrameTimeCodeTemplate, asset.ProviderUri, timeCode);

            return string.Concat(this.mediaServicesUri.ToString(), getFrame);
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
            string getFrame = string.Format(CultureInfo.InvariantCulture, GetFrameSecondsTemplate, asset.ProviderUri, seconds, width, height);
            
            return string.Concat(this.mediaServicesUri.ToString(), getFrame);
        }

        /// <summary>
        /// Retrieves the media services uri to be used to get frames.
        /// </summary>
        /// <param name="configurationService">The configuration Service.</param>
        /// <returns>The media services uri.</returns>
        private static Uri GetMediaServicesUri(IConfigurationService configurationService)
        {
            return configurationService.GetParameterValueAsUri("MediaServicesUrl", UriKind.Absolute);
        }
    }
}
