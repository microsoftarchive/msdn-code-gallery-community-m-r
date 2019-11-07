// <copyright file="WMMetadata.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: WMMetadata.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Metadata.Strategies.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Services.Contracts;
    using SMPTETimecode;

    internal class WMMetadata : Metadata
    {
        private readonly IList<MetadataField> metadataFields;

        public WMMetadata()
        {
            this.metadataFields = new List<MetadataField>();
            this.MetadataFields = new ReadOnlyCollection<MetadataField>(this.metadataFields);
        }

        public override string Title
        {
            get
            {
                object value = this.GetMetadataAttributeValueByName("Title");

                if (value != null && !string.IsNullOrEmpty(value.ToString()))
                {
                    return value.ToString();
                }

                return null;
            }
        }

        public override TimeSpan Duration
        {
            get 
            {
                object value = this.GetMetadataAttributeValueByName("Duration");

                long duration;

                if (value != null && long.TryParse(value.ToString(), out duration))
                {
                    return TimeSpan.FromTicks(duration);
                }

                return TimeSpan.FromSeconds(0);
            }
        }

        public override SmpteFrameRate FrameRate
        {
            get
            {
                object value = this.GetMetadataAttributeValueByName("FrameRate");

                double frameRate;

                if (value == null || !double.TryParse(value.ToString(), out frameRate))
                {
                    value = this.GetMetadataAttributeValueByName("WM/VideoFrameRate");
                }

                if (value != null && double.TryParse(value.ToString(), out frameRate))
                {
                    if (frameRate == 23.98)
                    {
                        return SmpteFrameRate.Smpte2398;
                    }

                    if (frameRate == 24)
                    {
                        return SmpteFrameRate.Smpte24;
                    }

                    if (frameRate == 25)
                    {
                        return SmpteFrameRate.Smpte25;
                    }

                    if (frameRate == 29.97)
                    {
                        return SmpteFrameRate.Smpte2997NonDrop;
                    }

                    if (frameRate == 30)
                    {
                        return SmpteFrameRate.Smpte30;
                    }
                }

                return SmpteFrameRate.Unknown;
            }
        }

        public override int? Width
        {
            get 
            { 
                object value = this.GetMetadataAttributeValueByName("WM/VideoWidth");

                int width;

                if (value != null && int.TryParse(value.ToString(), out width))
                {
                    return width; 
                }

                return null;
            }
        }

        public override int? Height
        {
            get
            {
                object value = this.GetMetadataAttributeValueByName("WM/VideoHeight");

                int height;

                if (value != null && int.TryParse(value.ToString(), out height))
                {
                    return height;
                }

                return null;
            }
        }

        public void AddMetadataField(MetadataField metadataField)
        {
            this.metadataFields.Add(metadataField);
        }

        private object GetMetadataAttributeValueByName(string attributeName)
        {
            MetadataField metadataField = this.MetadataFields.Where(x => x.Name == attributeName).FirstOrDefault();

            return metadataField != null ? metadataField.Value : null;
        }
    }
}
