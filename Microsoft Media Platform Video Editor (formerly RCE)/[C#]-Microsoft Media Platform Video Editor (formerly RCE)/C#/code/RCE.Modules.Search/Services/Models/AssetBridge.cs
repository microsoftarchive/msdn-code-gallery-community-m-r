// <copyright file="AssetBridge.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: AssetBridge.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Search.Services.Models
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Linq;

    public class AssetBridge
    {
        public AssetBridge()
        {
            this.DataStreams = new List<string>();
        }

        public Guid AssetId { get; set; }

        public string AzureId { get; set; }

        public string CMSId { get; set; }

        public string Type { get; set; }

        public string Title { get; set; }

        public string FrameRate { get; set; }

        public Uri Source { get; set; }

        public Uri ThumbnailSource { get; set; }

        public double? Duration { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public string Fields { get; set; }

        public string Template { get; set; }

        public bool IsAdaptiveStreaming { get; set; }

        public IList<string> DataStreams { get; private set; }

        public string ArchiveUrl { get; private set; }
        
        public string VodUrl { get; private set; }
        
        public static AssetBridge Parse(XElement assetElement)
        {
            string assetId = assetElement.Element("AssetID").GetValue();
            string type = assetElement.Element("KindName").GetValue();
            string title = assetElement.Element("Title").GetValue();
            string frameRate = assetElement.Element("Framerate").GetValue();
            Uri thumbnailUri = assetElement.Element("wThumbnailPath").GetValueAsUri(UriKind.Absolute);
            Uri source = assetElement.Element("Source").GetValueAsUri(UriKind.Absolute);
            double? duration = assetElement.Element("Duration").GetValueAsDouble();
            double width = assetElement.Element("Width").GetValueAsDouble().GetValueOrDefault(1280);
            double height = assetElement.Element("Height").GetValueAsDouble().GetValueOrDefault(720);
            double x = assetElement.Element("X").GetValueAsDouble().GetValueOrDefault(0);
            double y = assetElement.Element("Y").GetValueAsDouble().GetValueOrDefault(0);
            string fields = assetElement.Element("Fields").GetValue();
            string template = assetElement.Element("Template").GetValue();
            string archiveUrl = assetElement.Element("ArchiveURL").GetValue();
            string cmsId = assetElement.Element("AssetID").GetValue();
            string vodUrl = assetElement.Element("VODUrl").GetValue();
            string azureId = assetElement.Element("AzureId").GetValue();

            return new AssetBridge
            {
                AssetId = string.IsNullOrWhiteSpace(assetId) ? Guid.NewGuid() : new Guid(assetId),
                AzureId = azureId,
                Type = string.IsNullOrWhiteSpace(type) ? "video" : type,
                Title = title,
                FrameRate = frameRate,
                Source = source,
                ThumbnailSource = thumbnailUri,
                Duration = duration,
                Height = height,
                Width = width,
                X = x,
                Y = y,
                Fields = fields,
                Template = template,
                IsAdaptiveStreaming = true,
                ArchiveUrl = archiveUrl,
                CMSId = cmsId,
                VodUrl = vodUrl
            };
        }
    }
}
