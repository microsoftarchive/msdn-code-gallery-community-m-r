// <copyright file="XmlAssetsDataProvider.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: XmlAssetsDataProvider.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Data.Xml
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Web;
    using System.Xml.Linq;
    using RCE.Services.Contracts;

    using SmoothStreamingManifestGenerator.Models;

    using SMPTETimecode;

    public class XmlAssetsDataProvider : IAssetsDataProvider
    {
        /// <summary>
        /// The metadata locator used to retrieve assets metadata.
        /// </summary>
        private readonly IMetadataLocator metadataLocator;

        private readonly Container library;

        public XmlAssetsDataProvider(IMetadataLocator metadataLocator)
            : this(HttpContext.Current.Server.MapPath(@"bin\Assets.xml"), metadataLocator)
        {
        }

        protected XmlAssetsDataProvider(string filePath, IMetadataLocator metadataLocator)
        {
            this.metadataLocator = metadataLocator;
            this.library = new Container();
            this.LoadAssets(filePath);
        }

        /// <summary>
        /// Returns back given no. of items from the library.
        /// </summary>
        /// <param name="maxNumberOfItems">Maximum no. of records in the result.</param>
        /// <returns>A <see cref="Container"/> with the items.</returns>
        public Container LoadLibrary(int maxNumberOfItems)
        {
            return this.library;
        }

        /// <summary>
        /// Returns back all of the items that are contained in the library filtering them using the filter provided.
        /// </summary>
        /// <param name="filter">The filter used to get the items.</param>
        /// <param name="maxNumberOfItems">Maximum no. of records in the result.</param>
        /// <returns>A <see cref="Container"/> with the items.</returns>
        public Container LoadLibrary(string filter, int maxNumberOfItems)
        {
            // TODO: Implement the filter operation.
            return this.library;
        }

        /// <summary>
        /// Returns back given no. of items from the library.
        /// </summary>
        /// <param name="libraryId">The <see cref="Uri"/> of the container to load from the library.</param>
        /// <param name="maxNumberOfItems">Maximum no. of records in the result.</param>
        /// <returns>A <see cref="Container"/> with the items.</returns>
        public Container LoadLibraryById(Uri libraryId, int maxNumberOfItems)
        {
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

        private static AudioItem CreateAudioItem(Uri sourceUri, string title, double? duration, string archiveURL, string cmsId, string azureId)
        {
            AudioItem item = new AudioItem
                {
                    Id = CreateUri("Audios"),
                    CMSId = cmsId,
                    AzureId = azureId,
                    Title = title,
                    Duration = duration,
                    Resources =
                        new ResourceCollection
                            {
                                new Resource
                                    { Id = CreateUri("Resources"), Ref = sourceUri.ToString(), ResourceType = "Master", }
                            },
                    IsStereo = true,
                    ArchiveURL = archiveURL
                };
            return item;
        }

        private static OverlayItem CreateOverlayItem(string xamlTemplate, string title, string fields, double width, double height, double x, double y, double? duration, string cmsId, string azureId)
        {
            List<MetadataField> overlayMetadata = GetOverlayMetadata(fields);

            var item = new OverlayItem
                {
                    Id = CreateUri("Overlays"),
                    CMSId = cmsId,
                    AzureId = azureId,
                    Title = title,
                    Metadata = overlayMetadata,
                    XamlTemplate = xamlTemplate,
                    Height = height * 100,
                    Width = width * 100,
                    X = x * 100,
                    Y = y * 100,
                    Duration = duration.HasValue ? duration.Value : 10
                };

            return item;
        }

        private static List<MetadataField> GetOverlayMetadata(string fields)
        {
            return new List<MetadataField>(fields.Split(',').Select(f => new MetadataField(f, string.Empty)));
        }

        private static VideoItem CreateVideoItem(bool isAdaptiveStreaming, IList<string> dataStreams, Uri sourceUri, string title, Uri thumbnailUri, double? duration, SmpteFrameRate smpteFrameRate, double width, double height, IList<AudioStreamInfo> audioStreamsInformation, IList<string> videoStreamInformation, string archiveURL, string cmsId, string vodUrl, string azureId)
        {
            VideoItem item;

            if (isAdaptiveStreaming)
            {
                SmoothStreamingVideoItem tempItem = new SmoothStreamingVideoItem();

                if (dataStreams != null && dataStreams.Count > 0)
                {
                    tempItem.DataStreams = (List<string>)dataStreams;
                }

                if (audioStreamsInformation != null && audioStreamsInformation.Count > 0)
                {
                    tempItem.AudioStreams = (List<AudioStreamInfo>)audioStreamsInformation;
                }
                
                if (videoStreamInformation != null && videoStreamInformation.Count > 0)
                {
                    tempItem.VideoStreams = (List<string>)videoStreamInformation;
                }

                tempItem.VodUrl = vodUrl;
                item = tempItem;
            }
            else
            {
                item = new VideoItem();
            }

            item.Id = CreateUri("Videos");
            item.Resources = new ResourceCollection();
            item.Resources.Add(new Resource
                {
                    Id = CreateUri("Resources"),
                    Ref = sourceUri.ToString(),
                    ResourceType = isAdaptiveStreaming ? (UtilityHelper.IsLiveAdaptiveStreaming(sourceUri) ? "LiveSmoothStream" : "SmoothStream") : "Master",
                });

            item.Title = title;
            item.ThumbnailSource = (thumbnailUri != null) ? thumbnailUri.ToString() : null;
            item.Duration = duration;
            item.FrameRate = smpteFrameRate;
            item.Width = (int)width;
            item.Height = (int)height;

            item.ArchiveURL = archiveURL;
            item.CMSId = cmsId;
            item.AzureId = azureId;

            return item;
        }

        private static ImageItem CreateImageItem(Uri sourceUri, string title, double width, double height, string cmsId, string azureId)
        {
            return new ImageItem
            {
                Id = CreateUri("Images"),
                CMSId = cmsId,
                AzureId = azureId,
                Title = title,
                Width = (int)width,
                Height = (int)height,
                Resources =
                               {
                                   new Resource
                                       {
                                           Id = CreateUri("Resources"),
                                           ResourceType = "Master",
                                           Ref = sourceUri.ToString()
                                       }
                               }
            };
        }

        private static Uri CreateUri(string item)
        {
            string uriString = string.Format(CultureInfo.InvariantCulture, "http://rce.litwareinc.com/samples/2.0/{0}/{1}", item, Guid.NewGuid().ToString("D"));
            return new Uri(uriString);
        }

        private void LoadAssets(string filePath)
        {
            XDocument document = XDocument.Load(filePath);

            XElement assets = document.Element("Assets");

            if (assets != null)
            {
                foreach (XElement asset in assets.Elements())
                {
                    string type = asset.Attribute("Type").GetValue().ToLowerInvariant();
                    string title = asset.Attribute("Title").GetValue();
                    Uri thumbnailUri = asset.Attribute("ThumbnailUri").GetValueAsUri(UriKind.Absolute);
                    Uri sourceUri = asset.Attribute("SourceUri").GetValueAsUri(UriKind.Absolute);
                    double? duration = asset.Attribute("DurationInSeconds").GetValueAsDouble();
                    bool isAdaptiveStreaming = asset.Attribute("IsAdaptiveStreaming").GetValueAsBoolean().GetValueOrDefault();
                    IList<string> dataStreams = asset.Attribute("DataStreams").GetValueAsStringList(",");
                    string frameRate = asset.Attribute("FrameRate").GetValue();
                    double width = asset.Attribute("Width").GetValueAsDouble().GetValueOrDefault(1280);
                    double height = asset.Attribute("Height").GetValueAsDouble().GetValueOrDefault(720);
                    double x = asset.Attribute("X").GetValueAsDouble().GetValueOrDefault(0);
                    double y = asset.Attribute("Y").GetValueAsDouble().GetValueOrDefault(0);
                    string fields = asset.Attribute("Fields").GetValue();
                    string template = asset.Attribute("Template").GetValue();
                    string archiveUrl = asset.Attribute("ArchiveURL").GetValue();
                    string cmsId = asset.Attribute("CMSId").GetValue();
                    string vodUrl = asset.Attribute("VODUrl").GetValue();
                    string azureId = asset.Attribute("AzureId").GetValue();

                    Metadata metadata = this.metadataLocator.GetMetadata(sourceUri);

                    List<AudioStreamInfo> audioStreamsInformation = new List<AudioStreamInfo>();

                    IList<string> videoStreamInformation = new List<string>();

                    if (metadata != null)
                    {
                        IEnumerable<StreamInfo> audioStream = metadata.MetadataFields.SingleOrDefault(mf => mf.Name.Equals("AudioStreams", StringComparison.OrdinalIgnoreCase)).Value as IEnumerable<StreamInfo>;

                        if (audioStream != null)
                        {
                            foreach (StreamInfo streamInfo in audioStream)
                            {
                                string name = null;

                                if (streamInfo.Attributes.ContainsKey("Name"))
                                {
                                    name = streamInfo.Attributes["Name"];
                                }

                                bool isStereo = true;

                                if (streamInfo.QualityLevels.Count > 0 && streamInfo.QualityLevels.FirstOrDefault().Attributes.ContainsKey("Channels"))
                                {
                                    isStereo = streamInfo.QualityLevels.FirstOrDefault().Attributes["Channels"] == "2";
                                }

                                audioStreamsInformation.Add(new AudioStreamInfo { IsStereo = isStereo, Name = name });
                            }
                        }

                        MetadataField videoStreamMetadataField =
                            metadata.MetadataFields.SingleOrDefault(mf => mf.Name.Equals("VideoStreams", StringComparison.OrdinalIgnoreCase));
                        
                        if (videoStreamMetadataField != null)
                        {
                            IList<string> videoStreams = videoStreamMetadataField.Value as IList<string>;

                            if (videoStreams != null)
                            {
                                videoStreamInformation = videoStreams;
                            }
                        }
                    }

                    SmpteFrameRate smpteFrameRate;
                    if (frameRate == null || !Enum.TryParse(frameRate, true, out smpteFrameRate))
                    {
                        smpteFrameRate = SmpteFrameRate.Smpte2997NonDrop;
                    }

                    // TODO: Refactor this)))
                    if (type == "video")
                    {
                        VideoItem item = CreateVideoItem(isAdaptiveStreaming, dataStreams, sourceUri, title, thumbnailUri, duration, smpteFrameRate, width, height, audioStreamsInformation, videoStreamInformation, archiveUrl, cmsId, vodUrl, azureId);

                        this.library.Items.Add(item);
                    }

                    if (type == "audio")
                    {
                        AudioItem item = CreateAudioItem(sourceUri, title, duration, archiveUrl, cmsId, azureId);

                        this.library.Items.Add(item);
                    }

                    if (type == "image")
                    {
                        ImageItem item = CreateImageItem(sourceUri, title, width, height, cmsId, azureId);

                        this.library.Items.Add(item);
                    }

                    if (type == "overlay")
                    {
                        OverlayItem item = CreateOverlayItem(template, title, fields, width, height, x, y, duration, cmsId, azureId);

                        this.library.Items.Add(item);
                    }
                }
            }
        }
    }
}
