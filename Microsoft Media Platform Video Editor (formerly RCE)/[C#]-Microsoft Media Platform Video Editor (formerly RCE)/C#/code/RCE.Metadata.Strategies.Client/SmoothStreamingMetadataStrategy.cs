// <copyright file="SmoothStreamingMetadataStrategy.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: SmoothStreamingMetadataStrategy.cs                     
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
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using RCE.Infrastructure.Services;
    using RCE.Metadata.Strategies.Contracts;
    using RCE.Services.Contracts;

    using SmoothStreamingManifestGenerator;
    using SmoothStreamingManifestGenerator.Models;

    public class SmoothStreamingMetadataStrategy : IMetadataStrategy
    {
        public event EventHandler<MetadataEventArgs> GetManifestCompleted;

        public bool CanRetrieveMetadata(object target)
        {
            Uri smoothStreamingManifestUri = target as Uri;

            if (smoothStreamingManifestUri == null)
            {
                return false;
            }

            string manifestUrl = smoothStreamingManifestUri.AbsolutePath.ToUpperInvariant();

            return manifestUrl.EndsWith(".ISML/MANIFEST") || manifestUrl.EndsWith(".ISM/MANIFEST");
        }

        public void GetMetadata(object target)
        {
            Uri smoothStreamingManifestUri = target as Uri;

            if (smoothStreamingManifestUri == null)
            {
                this.OnGetManifestCompleted(null);
            }
            else
            {
                var manager = new RCE.Infrastructure.Services.DownloaderManager();

                manager.DownloadCompleted +=
                    (s, e) =>
                    {
                        Stream manifestStream = e.Stream;
                        SmoothStreamingMetadata metadata = new SmoothStreamingMetadata();

                        if (manifestStream != null)
                        {
                            SmoothStreamingManifestParser parser = new SmoothStreamingManifestParser(manifestStream);

                            IEnumerable<StreamInfo> audioStreams = parser.ManifestInfo.Streams.Where(x => x.StreamType.Equals("audio", StringComparison.OrdinalIgnoreCase));

                            StreamInfo videoStreamInfo = parser.ManifestInfo.Streams.SingleOrDefault(x => x.StreamType.Equals("video", StringComparison.OrdinalIgnoreCase));

                            if (videoStreamInfo != null)
                            {
                                QualityLevel firstQualityLevel = videoStreamInfo.QualityLevels.FirstOrDefault();

                                string bitrate;
                                string cameraAngle;

                                if (firstQualityLevel != null && firstQualityLevel.Attributes.TryGetValue("Bitrate", out bitrate) && firstQualityLevel.CustomAttributes.TryGetValue("cameraAngle", out cameraAngle))
                                {
                                    List<string> cameraAngles = new List<string>();

                                    foreach (QualityLevel qualityLevel in videoStreamInfo.QualityLevels)
                                    {
                                        if (qualityLevel.Attributes["Bitrate"] == bitrate && qualityLevel.CustomAttributes.TryGetValue("cameraAngle", out cameraAngle))
                                        {
                                            cameraAngles.Add(cameraAngle);
                                        }
                                    }

                                    // order by string comparison
                                    cameraAngles.Sort();

                                    if (cameraAngles.Count() > 0)
                                    {
                                        metadata.AddMetadataField(new MetadataField("VideoStreams", cameraAngles));
                                    }
                                }
                            }

                            metadata.AddMetadataField(new MetadataField("Duration", parser.ManifestInfo.ManifestDuration));

                            metadata.AddMetadataField(new MetadataField("AudioStreams", audioStreams));
                        }

                        this.OnGetManifestCompleted(metadata);
                    };

                manager.DownloadManifestAsync(smoothStreamingManifestUri, false, null);
            }
        }

        private void OnGetManifestCompleted(Metadata metadata)
        {
            EventHandler<MetadataEventArgs> handler = this.GetManifestCompleted;
            if (handler != null)
            {
                handler(this, new MetadataEventArgs(metadata));
            }
        }
    }
}
