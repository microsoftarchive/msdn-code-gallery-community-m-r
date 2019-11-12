// <copyright file="ImageMetadataStrategy.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ImageMetadataStrategy.cs                     
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
    using System.Drawing.Imaging;
    using System.IO;
    using System.Windows.Media.Imaging;
    using Contracts;
    using Services.Contracts;

    public class ImageMetadataStrategy : IMetadataStrategy
    {
        public bool CanRetrieveMetadata(object target)
        {
            if (target == null || !(target is string))
            {
                return false;
            }

            string filename = target.ToString();

            string extension = Path.GetExtension(filename).ToUpperInvariant();

            return extension == ".JPG" || extension == ".PNG" || extension == ".BMP" || extension == ".TIFF";
        }

        public Metadata GetMetadata(object target)
        {
            if (target == null || !(target is string))
            {
                return null;
            }

           string filename = target.ToString();
           ImageMetadata metadata = new ImageMetadata();

           using (Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                PropertyItem[] propertyItems;

                try
                {
                    using (System.Drawing.Image image = System.Drawing.Image.FromStream(stream, true, false))
                    {
                        try
                        {
                            propertyItems = image.PropertyItems;
                        }
                        catch (NotImplementedException ex)
                        {
                            // TODO: Log exception;
                            propertyItems = new PropertyItem[0];
                        }
                    }
                }
                catch (ArgumentException ex)
                {
                    // TODO: Log exception;
                    propertyItems = new PropertyItem[0];
                }

                foreach (PropertyItem propertyItem in propertyItems)
                {
                    System.Diagnostics.Debug.WriteLine(propertyItem.Id);
                    if (Enum.IsDefined(typeof(ImagePropertyTags), propertyItem.Id))
                    {
                        metadata.AddPropertyItem(propertyItem);
                    }
                }
            }

           BitmapFrame frame = BitmapFrame.Create(new Uri(filename), BitmapCreateOptions.IgnoreImageCache, BitmapCacheOption.OnLoad);
           BitmapMetadata bitmapMetadata = frame.Metadata as BitmapMetadata;

           if (bitmapMetadata != null)
           {
               metadata.AddBitmapMetadata(bitmapMetadata);
           }

           return metadata;
        }
    }
}