// <copyright file="FileSystemAssetsDataProvider.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: FileSystemAssetsDataProvider.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Data.FileSystem
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Xml;
    using System.Xml.Linq;
    using Services.Contracts;
    using SMPTETimecode;

    /// <summary>
    /// Data Provider that retrieves the assets from the file system.
    /// </summary>
    public class FileSystemAssetsDataProvider : IAssetsDataProvider
    {
        /// <summary>
        /// The metadata locator used to retrieve assets metadata.
        /// </summary>
        private readonly IMetadataLocator metadataLocator;

        /// <summary>
        /// The path where the assets are located.
        /// </summary>
        private readonly string assetsPath;

        /// <summary>
        /// The uri template used to build the assets URI.
        /// </summary>
        private readonly string assetsUriTemplate;

        /// <summary>
        /// The uri template used to build the assets thumbnail URI.
        /// </summary>
        private readonly string assetsThumbnailUriTemplate;

        // The suffix used to match thumbnails on the folder.
        private readonly string assetsThumbnailSuffix;

        /// <summary>
        /// The list of allowed video extensions.
        /// </summary>
        private readonly string[] allowedVideoAssets;

        /// <summary>
        /// The list of allowed audio extensions.
        /// </summary>
        private readonly string[] allowedAudioAssets;

        /// <summary>
        /// The list of allowed image extensions.
        /// </summary>
        private readonly string[] allowedImageAssets;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemAssetsDataProvider"/> class.
        /// </summary>
        /// <param name="metadataLocator">The metadata locator used to retrieve assets metadata.</param>
        public FileSystemAssetsDataProvider(IMetadataLocator metadataLocator)
            : this(metadataLocator, HttpContext.Current.Server.MapPath(@"bin\FileSystemAssetsDataProviderConfiguration.xml"))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemAssetsDataProvider"/> class.
        /// </summary>
        /// <param name="metadataLocator">The metadata locator used to retrieve assets metadata.</param>
        /// <param name="configurationPath">The path where the configuration file is located.</param>
        protected FileSystemAssetsDataProvider(IMetadataLocator metadataLocator, string configurationPath)
        {
            this.metadataLocator = metadataLocator;

            using (XmlReader reader = XmlReader.Create(configurationPath))
            {
                XDocument document = XDocument.Load(reader);
            
                this.assetsPath = document.Root.Element("AssetsPath").Value;

                if (!Path.IsPathRooted(this.assetsPath))
                {
                    this.assetsPath = HttpContext.Current.Server.MapPath(this.assetsPath);
                }

                this.assetsUriTemplate = document.Root.Element("AssetsUriTemplate").Value;
                this.assetsThumbnailUriTemplate = document.Root.Element("AssetsThumbnailUriTemplate").Value;
                this.assetsThumbnailSuffix = document.Root.Element("AssetsThumbnailSuffix").Value;
                this.allowedVideoAssets = document.Root.Element("AllowedVideoAssets").Value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                this.allowedAudioAssets = document.Root.Element("AllowedAudioAssets").Value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                this.allowedImageAssets = document.Root.Element("AllowedImageAssets").Value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        /// <summary>
        /// Returns back given no. of items from the library.
        /// </summary>
        /// <param name="maxNumberOfItems">Maximum no. of records in the result.</param>
        /// <returns>A <see cref="Container"/> with the items.</returns>
        public Container LoadLibrary(int maxNumberOfItems)
        {
            Container libraryContainer = new Container();
            if (Directory.Exists(this.assetsPath))
            {
                string[] files = Directory.GetFiles(this.assetsPath, "*.*", SearchOption.AllDirectories);

                foreach (string file in files)
                {
                    Metadata metadata = this.metadataLocator.GetMetadata(file);

                    int index = file.IndexOf(this.assetsPath, StringComparison.OrdinalIgnoreCase) + this.assetsPath.Length;
                    string relativePath = file.Substring(index).Replace(@"\", "/");

                    if (this.IsVideoItem(file))
                    {
                        double duration;
                        VideoItem item;
                        ResourceCollection resources = new ResourceCollection();
                        Resource resource;

                        if (UtilityHelper.IsProgressiveDownloadFile(file))
                        {
                            duration = 0;
                            item = new VideoItem();
                            resource = new Resource
                                                {
                                                    Ref = string.Format(CultureInfo.InvariantCulture, this.assetsUriTemplate, relativePath),
                                                    ResourceType = "Master"
                                                };
                        }
                        else
                        {
                            duration = 60;
                            item = new SmoothStreamingVideoItem();
                            resource = new Resource
                            {
                                Ref = string.Format(CultureInfo.InvariantCulture, this.assetsUriTemplate, relativePath) + (UtilityHelper.IsCompositeAdaptiveStreaming(file) ? string.Empty : "/manifest"),
                                ResourceType = UtilityHelper.IsLiveAdaptiveStreaming(file) ? "LiveSmoothStream" : "SmoothStream"
                            };
                        }

                        item.Id = new Uri("http://" + Guid.NewGuid());
                        
                        resources.Add(resource);
                        item.Resources = resources;

                        if (metadata != null)
                        {
                            item.Duration = metadata.Duration.TotalSeconds;
                            item.Title = string.IsNullOrEmpty(metadata.Title) ? Path.GetFileName(file) : metadata.Title;
                            item.FrameRate = metadata.FrameRate == SmpteFrameRate.Unknown
                                                 ? SmpteFrameRate.Smpte2997NonDrop
                                                 : metadata.FrameRate;
                            item.Width = metadata.Width ?? 1280;
                            item.Height = metadata.Height ?? 720;
                            item.Metadata = new List<MetadataField>(metadata.MetadataFields);
                        }
                        else
                        {
                            item.Duration = duration;
                            item.Title = Path.GetFileName(file);
                            item.FrameRate = SmpteFrameRate.Smpte2997NonDrop;
                            item.Width = 1280;
                            item.Height = 720;
                        }

                        item.ThumbnailSource = string.Format(CultureInfo.InvariantCulture, this.assetsThumbnailUriTemplate, Path.GetFileNameWithoutExtension(file));

                        libraryContainer.Items.Add(item);
                    }
                    else if (this.IsAudioItem(file))
                    {
                        AudioItem item = new AudioItem
                                             {
                                                 Id = new Uri("http://" + Guid.NewGuid()),
                                                 Resources = new ResourceCollection()
                                             };
                        Resource resource = new Resource
                        {
                            Ref = string.Format(CultureInfo.InvariantCulture, this.assetsUriTemplate, relativePath),
                            ResourceType = "Master"
                        };

                        item.Resources.Add(resource);

                        if (metadata != null)
                        {
                            item.Duration = metadata.Duration.TotalSeconds;
                            item.Title = string.IsNullOrEmpty(metadata.Title) ? Path.GetFileName(file) : metadata.Title;
                            item.Metadata = new List<MetadataField>(metadata.MetadataFields);
                        }
                        else
                        {
                            item.Duration = 51;
                            item.Title = Path.GetFileName(file);
                        }

                        libraryContainer.Items.Add(item);
                    }
                    else if (this.IsImageItem(file) && !file.EndsWith(this.assetsThumbnailSuffix, StringComparison.Ordinal))
                    {
                        ImageItem item = new ImageItem
                                             {
                                                 Id = new Uri("http://" + Guid.NewGuid()),
                                                 Resources = new ResourceCollection()
                                             };

                        Resource resource = new Resource
                        {
                            Ref = string.Format(CultureInfo.InvariantCulture, this.assetsUriTemplate, relativePath),
                            ResourceType = "Master"
                        };

                        item.Resources.Add(resource);

                        if (metadata != null)
                        {
                            item.Title = string.IsNullOrEmpty(metadata.Title) ? Path.GetFileName(file) : metadata.Title;
                            item.Width = metadata.Width ?? 1280;
                            item.Height = metadata.Height ?? 720;
                            item.Metadata = new List<MetadataField>(metadata.MetadataFields);
                        }
                        else
                        {
                            item.Title = Path.GetFileName(file);
                            item.Width = 1280;
                            item.Height = 720;
                        }

                        libraryContainer.Items.Add(item);
                    }
                }
            }

            return libraryContainer;
        }

        /// <summary>
        /// Returns back all of the items that are contained in the library filtering them using the filter provided.
        /// </summary>
        /// <param name="filter">The filter used to get the items.</param>
        /// <param name="maxNumberOfItems">Maximum no. of records in the result.</param>
        /// <returns>A <see cref="Container"/> with the items.</returns>
        public Container LoadLibrary(string filter, int maxNumberOfItems)
        {
            return this.LoadLibrary(maxNumberOfItems);
        }

        /// <summary>
        /// Returns back given no. of items from the library.
        /// </summary>
        /// <param name="libraryId">The <see cref="Uri"/> of the container to load from the library.</param>
        /// <param name="maxNumberOfItems">Maximum no. of records in the result.</param>
        /// <returns>A <see cref="Container"/> with the items.</returns>
        public Container LoadLibraryById(Uri libraryId, int maxNumberOfItems)
        {
            // Add here the logic to retrieve an specific logic
            return new Container();
        }

        /// <summary>
        /// Returns back given no. of items from the library.
        /// </summary>
        /// <param name="libraryId">The <see cref="Uri"/> of the container to load from the library.</param>
        /// <param name="filter">The filter used to get the items.</param>
        /// <param name="maxNumberOfItems">Maximum no. of records in the result.</param>
        /// <returns>A <see cref="Container"/> with the items.</returns>
        public Container LoadLibraryById(Uri libraryId, string filter, int maxNumberOfItems)
        {
            // Add here the logic to retrieve an specific logic
            return new Container();
        }

        /// <summary>
        /// Determines whether the file is a video item or not.
        /// </summary>
        /// <param name="file">The file being verified.</param>
        /// <returns>A true if the file is a video item;otherwise false.</returns>
        private bool IsVideoItem(string file)
        {
            string extension = Path.GetExtension(file).ToUpperInvariant();

            return this.allowedVideoAssets.Contains(extension);
        }

        /// <summary>
        /// Determines whether the file is a audio item or not.
        /// </summary>
        /// <param name="file">The file being verified.</param>
        /// <returns>A true if the file is a audio item;otherwise false.</returns>
        private bool IsAudioItem(string file)
        {
            string extension = Path.GetExtension(file).ToUpperInvariant();

            return this.allowedAudioAssets.Contains(extension);
        }

        /// <summary>
        /// Determines whether the file is a image item or not.
        /// </summary>
        /// <param name="file">The file being verified.</param>
        /// <returns>A true if the file is a image item;otherwise false.</returns>
        private bool IsImageItem(string file)
        {
            string extension = Path.GetExtension(file).ToUpperInvariant();

            return this.allowedImageAssets.Contains(extension);
        }
    }
}
