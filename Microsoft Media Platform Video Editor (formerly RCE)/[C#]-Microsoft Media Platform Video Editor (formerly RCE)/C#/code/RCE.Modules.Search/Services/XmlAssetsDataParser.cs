// <copyright file="XmlAssetsDataParser.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: XmlAssetsDataParser.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Search.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Xml.Linq;
    using RCE.Infrastructure;
    using RCE.Infrastructure.Models;
    using RCE.Infrastructure.Services;
    using RCE.Modules.Search.Security;
    using RCE.Modules.Search.Services.Models;
    using RCE.Services.Contracts;
    using SmoothStreamingManifestGenerator.Models;
    using SMPTETimecode;

    public class XmlAssetsDataParser : IXmlAssetsDataParser
    {
        private const string CdnSharedSecretSettingName = "CdnSharedSecret";

        private const string DefaultCdnContentDurationSettingName = "DefaultCdnContentDuration";

        /// <summary>
        /// Identifies the default content network prefix to be used to build the assets uri.
        /// </summary>
        private static string contentNetworkPrefix = "http://rcecdn/";

        private readonly Func<IMetadataStrategy> metadataStrategyFactory;

        private readonly object lockObject = new object();

        private readonly string cdnSharedSecret;

        private readonly ICdnTokenGenerator cdnTokenGenerator;

        private readonly int defaultCdnContentDuration;

        private readonly SmpteFrameRate defaultFrameRate;

        private List<Asset> assets;

        private int remainingAssets;

        public XmlAssetsDataParser(Func<IMetadataStrategy> metadataStrategyFactory, IConfigurationService configurationService, ICdnTokenGenerator cdnTokenGenerator)
        {
            this.metadataStrategyFactory = metadataStrategyFactory;
            this.cdnTokenGenerator = cdnTokenGenerator;

            this.cdnSharedSecret = configurationService.GetParameterValue(CdnSharedSecretSettingName);
            this.defaultCdnContentDuration = configurationService.GetParameterValueAsInt(DefaultCdnContentDurationSettingName) ?? 60 * 60 * 24 * 365;
            this.defaultFrameRate = configurationService.GetDefaultFrameRate();
        }

        public event EventHandler<DataEventArgs<List<Asset>>> ResultsAvailable;

        /// <summary>
        /// Gets or sets the content network prefix.
        /// </summary>
        /// <value>A <seealso cref="string"/> that represents the current content network prefix.</value>
        public static string ContentNetworkPrefix
        {
            get
            {
                return contentNetworkPrefix;
            }

            set
            {
                contentNetworkPrefix = value;
            }
        }

        public int RemainingAssets
        {
            get
            {
                lock (this.lockObject)
                {
                    return this.remainingAssets;
                }
            }

            set
            {
                lock (this.lockObject)
                {
                    this.remainingAssets = value;
                }
            }
        }

        public void ParseAssets(string assetsXml, long cdnTime)
        {
            XDocument document = XDocument.Parse(assetsXml);

            this.Load(document.Element("root"), cdnTime);
        }

        private static AudioAsset CreateAudioAsset(
            string azureId,
            Uri sourceUri,
            string title,
            double? duration,
            string archiveUrl,
            string cmsId)
        {
            AudioAsset audioAsset = new AudioAsset
                {
                    ProviderUri = CreateUri("Audios"),
                    Title = title,
                    Created = DateTime.Now,
                    DurationInSeconds = duration.HasValue ? duration.Value : 0,
                    IsStereo = true,
                    ArchiveURL = archiveUrl,
                    CMSId = cmsId,
                    AzureId = azureId,
                    Source = sourceUri
                };

            return audioAsset;
        }

        private static OverlayAsset CreateOverlayAsset(
            string azureId,
            string xamlTemplate,
            string title,
            string fields,
            double width,
            double height,
            double x,
            double y,
            double? duration,
            string cmsId)
        {
            List<MetadataField> overlayMetadata = GetOverlayMetadata(fields);

            OverlayAsset overlayAsset = new OverlayAsset
                {
                    ProviderUri = CreateUri("Overlays"),
                    Title = title,
                    Created = DateTime.Now,
                    Metadata = overlayMetadata,
                    XamlResource = xamlTemplate,
                    Height = height * 100,
                    Width = width * 100,
                    PositionX = x * 100,
                    PositionY = y * 100,
                    DurationInSeconds = duration.HasValue ? duration.Value : 10,
                    CMSId = cmsId,
                    AzureId = azureId
                };

            return overlayAsset;
        }

        private static VideoAsset CreateVideoAsset(
            string azureId,
            bool isAdaptiveStreaming,
            IList<string> dataStreams,
            Uri sourceUri,
            string title,
            Uri thumbnailUri,
            double? duration,
            SmpteFrameRate smpteFrameRate,
            double width,
            double height,
            IList<AudioStreamInfo> audioStreamsInformation,
            IList<string> videoStreamInformation,
            string archiveUrl,
            string cmsId,
            string vodUrl)
        {
            VideoAsset videoAsset;

            if (isAdaptiveStreaming)
            {
                SmoothStreamingVideoAsset tempAsset = new SmoothStreamingVideoAsset();

                if (dataStreams != null && dataStreams.Count > 0)
                {
                    tempAsset.DataStreams = (List<string>)dataStreams;
                }

                if (audioStreamsInformation != null && audioStreamsInformation.Count > 0)
                {
                    audioStreamsInformation.ForEach(info => tempAsset.AudioStreams.Add(new AudioStream(info.Name, info.IsStereo)));
                }

                if (videoStreamInformation != null && videoStreamInformation.Count > 0)
                {
                    tempAsset.VideoStreams = (List<string>)videoStreamInformation;
                }

                tempAsset.VodUri = string.IsNullOrEmpty(vodUrl) ? null : new Uri(vodUrl);

                videoAsset = tempAsset;
            }
            else
            {
                videoAsset = new VideoAsset();
            }

            videoAsset.ProviderUri = CreateUri("Videos");
            videoAsset.Created = DateTime.Now;
            videoAsset.Title = title;
            videoAsset.ThumbnailSource = (thumbnailUri != null) ? thumbnailUri.ToString() : null;
            videoAsset.Duration = TimeCode.FromSeconds(duration.HasValue ? duration.Value : 0, smpteFrameRate);
            videoAsset.FrameRate = smpteFrameRate;
            videoAsset.Width = (int)width;
            videoAsset.Height = (int)height;
            videoAsset.ArchiveURL = archiveUrl;
            videoAsset.CMSId = cmsId;
            videoAsset.AzureId = azureId;
            videoAsset.Source = sourceUri;
            videoAsset.ResourceType = ResourceType.SmoothStream;

            return videoAsset;
        }

        private static ImageAsset CreateImageAsset(
            string azureId,
            Uri sourceUri,
            string title,
            double width,
            double height,
            string cmsId)
        {
            ImageAsset imageAsset = new ImageAsset
            {
                ProviderUri = CreateUri("Images"),
                Title = title,
                Width = (int)width,
                Height = (int)height,
                Created = DateTime.Now,
                CMSId = cmsId,
                AzureId = azureId,
                Source = sourceUri
            };

            return imageAsset;
        }

        private static List<MetadataField> GetOverlayMetadata(string fields)
        {
            return new List<MetadataField>(fields.Split(',').Select(f => new MetadataField(f, string.Empty)));
        }

        private static Uri CreateUri(string item)
        {
            string uriString = string.Format(CultureInfo.InvariantCulture, "http://rce.litwareinc.com/samples/2.0/{0}/{1}", item, Guid.NewGuid().ToString("D"));
            return new Uri(uriString);
        }

        private Asset CreateAsset(
            string azureId,
            string type,
            string title,
            Uri thumbnailUri,
            Uri sourceUri,
            double? duration,
            bool isAdaptiveStreaming,
            IList<string> dataStreams,
            string frameRate,
            double width,
            double height,
            double x,
            double y,
            string fields,
            string template,
            string archiveUrl,
            string cmsId,
            string vodUrl,
            Metadata metadata)
        {
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

                MetadataField videoStreamMetadataField = metadata.MetadataFields.SingleOrDefault(mf => mf.Name.Equals("VideoStreams", StringComparison.OrdinalIgnoreCase));

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
                smpteFrameRate = this.defaultFrameRate;
            }

            // TODO: Refactor this.
            if (type.Equals("video", StringComparison.OrdinalIgnoreCase))
            {
                return CreateVideoAsset(azureId, isAdaptiveStreaming, dataStreams, sourceUri, title, thumbnailUri, duration, smpteFrameRate, width, height, audioStreamsInformation, videoStreamInformation, archiveUrl, cmsId, vodUrl);
            }

            if (type.Equals("audio", StringComparison.OrdinalIgnoreCase))
            {
                return CreateAudioAsset(azureId, sourceUri, title, duration, archiveUrl, cmsId);
            }

            if (type.Equals("image", StringComparison.OrdinalIgnoreCase))
            {
                return CreateImageAsset(azureId, sourceUri, title, width, height, cmsId);
            }

            if (type.Equals("overlay", StringComparison.OrdinalIgnoreCase))
            {
                return CreateOverlayAsset(azureId, template, title, fields, width, height, x, y, duration, cmsId);
            }

            return null;
        }

        private Uri GetCdnUri(Uri sourceUri, long cdnTime)
        {
            if (string.IsNullOrWhiteSpace(this.cdnSharedSecret))
            {
                return sourceUri;
            }

            var token = this.cdnTokenGenerator.Generate(
                sourceUri.LocalPath,
                this.defaultCdnContentDuration,
                this.cdnSharedSecret,
                null,
                cdnTime,
                "__auth__");

            var uriBuilder = new UriBuilder(sourceUri);

            string separator = "&";
            if (uriBuilder.Query.IndexOf("?", StringComparison.OrdinalIgnoreCase) < 0)
            {
                separator = string.Empty;
            }

            uriBuilder.Query = string.Concat(uriBuilder.Query.TrimStart('?'), separator, token.AssetUrlQueryString);

            return uriBuilder.Uri;
        }

        private void Load(XElement rootElement, long cdnTime)
        {
            this.assets = new List<Asset>();

            if (rootElement != null)
            {
                IEnumerable<XElement> retrievedAssets = rootElement.Elements("asset");
                this.RemainingAssets = retrievedAssets.Count();

                if (this.RemainingAssets == 0)
                {
                    this.InvokeResultsAvailable();
                    return;
                }

                foreach (XElement assetElement in retrievedAssets)
                {
                    AssetBridge asset = AssetBridge.Parse(assetElement);

                    IMetadataStrategy metadataStrategy = this.metadataStrategyFactory();

                    asset.Source = this.GetCdnUri(asset.Source, cdnTime);

                    if (metadataStrategy.CanRetrieveMetadata(asset.Source))
                    {
                        metadataStrategy.GetManifestCompleted +=
                            (s, e) =>
                            {
                                Metadata metadata = e.Metadata;
                                this.AddNewAsset(asset.AzureId, asset.Type, asset.Title, asset.ThumbnailSource, asset.Source, asset.Duration, asset.IsAdaptiveStreaming, asset.DataStreams, asset.FrameRate, asset.Width, asset.Height, asset.X, asset.Y, asset.Fields, asset.Template, asset.ArchiveUrl, asset.CMSId, asset.VodUrl, metadata);
                            };

                        metadataStrategy.GetMetadata(asset.Source);
                    }
                    else
                    {
                        this.AddNewAsset(asset.AzureId, asset.Type, asset.Title, asset.ThumbnailSource, asset.Source, asset.Duration, asset.IsAdaptiveStreaming, asset.DataStreams, asset.FrameRate, asset.Width, asset.Height, asset.X, asset.Y, asset.Fields, asset.Template, asset.ArchiveUrl, asset.CMSId, asset.VodUrl, null);
                    }
                }
            }
            else
            {
                this.InvokeResultsAvailable();
            }
        }

        private void AddNewAsset(string azureId, string type, string title, Uri thumbnailUri, Uri sourceUri, double? duration, bool isAdaptiveStreaming, IList<string> dataStreams, string frameRate, double width, double height, double x, double y, string fields, string template, string archiveUrl, string cmsId, string vodUrl, Metadata metadata)
        {
            Asset asset = this.CreateAsset(azureId, type, title, thumbnailUri, sourceUri, duration, isAdaptiveStreaming, dataStreams, frameRate, width, height, x, y, fields, template, archiveUrl, cmsId, vodUrl, metadata);

            if (asset != null)
            {
                this.assets.Add(asset);
            }

            if (--this.RemainingAssets == 0)
            {
                this.InvokeResultsAvailable();
            }
        }

        private void InvokeResultsAvailable()
        {
            EventHandler<DataEventArgs<List<Asset>>> handler = this.ResultsAvailable;
            if (handler != null)
            {
                handler(this, new DataEventArgs<List<Asset>>(this.assets));
            }
        }
    }
}
