// <copyright file="ImageMetadata.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ImageMetadata.cs                     
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
    using System.Drawing.Imaging;
    using System.Linq;
    using System.Text;
    using System.Windows.Media.Imaging;
    using Services.Contracts;
    using SMPTETimecode;

    internal class ImageMetadata : Metadata
    {
        private const int NumberOfBytesIn32Bits = 4;

        private readonly IList<MetadataField> metadataFields;

        public ImageMetadata()
        {
            this.metadataFields = new List<MetadataField>();
            this.MetadataFields = new ReadOnlyCollection<MetadataField>(this.metadataFields);
        }

        public override string Title
        {
            get 
            {
                MetadataField field = this.metadataFields.FirstOrDefault(x => x.Name == ImagePropertyTags.ImageTitle.ToString() || x.Name == BitmapMetadataItem.Title.ToString());

                if (field != null && field.Value != null && !string.IsNullOrEmpty(field.Value.ToString()))
                {
                    return field.Value.ToString().Trim();
                }

                return null;
            }
        }

        public override TimeSpan Duration
        {
            get { throw new NotImplementedException(); }
        }

        public override SmpteFrameRate FrameRate
        {
            get { throw new NotImplementedException(); }
        }

        public override int? Width
        {
            get
            {
                MetadataField field = this.metadataFields.SingleOrDefault(x => x.Name == ImagePropertyTags.ImageWidth.ToString() || x.Name == ImagePropertyTags.ExifPixXDim.ToString());

                int width;

                if (field != null && int.TryParse(field.Value.ToString(), out width))
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
                MetadataField field = this.metadataFields.SingleOrDefault(x => x.Name == ImagePropertyTags.ImageHeight.ToString() || x.Name == ImagePropertyTags.ExifPixYDim.ToString());

                int height;

                if (field != null && int.TryParse(field.Value.ToString(), out height))
                {
                    return height;
                }

                return null;
            }
        }

        public void AddPropertyItem(PropertyItem propertyItem)
        {
            string name = Enum.GetName(typeof(ImagePropertyTags), propertyItem.Id);
            
            object value = ParsePropertyItemValue(propertyItem);

            MetadataField metadataField = new MetadataField(name, value);

            this.metadataFields.Add(metadataField);
        }

        public void AddBitmapMetadata(BitmapMetadata bitmapMetadata)
        {
            if (bitmapMetadata.Title != null && !string.IsNullOrEmpty(bitmapMetadata.Title.Trim()))
            {
                this.metadataFields.Add(new MetadataField(BitmapMetadataItem.Title.ToString(), bitmapMetadata.Title));
            }
        }

        // TODO: Complete parsing
        private static object ParsePropertyItemValue(PropertyItem propertyItem)
        {
            object value = null;

            switch ((ImagePropertyTagType)propertyItem.Type)
            {
                case ImagePropertyTagType.ASCII:

                    if (propertyItem.Value != null)
                    {
                        value = Encoding.ASCII.GetString(propertyItem.Value);
                    }

                    break;

                case ImagePropertyTagType.Long:

                    if (propertyItem.Len >= NumberOfBytesIn32Bits && propertyItem.Len / NumberOfBytesIn32Bits == 1)
                    {
                        value = Convert.ToInt64(BitConverter.ToUInt32(propertyItem.Value, 0));
                    }

                    break;

                default:
                    break;
            }

            return value;
        }
    }
}
