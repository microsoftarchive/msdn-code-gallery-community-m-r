// <copyright file="OverlayAsset.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: OverlayAsset.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Models
{
    using System;
    using System.Collections.Generic;

    using RCE.Services.Contracts;

    public class OverlayAsset : Asset
    {
        private double positionX;
        private double positionY;
        private double height;
        private double width;

        public OverlayAsset()
        {
            this.Id = Guid.NewGuid();
        }

        public double DurationInSeconds { get; set; }

        public string XamlResource { get; set; }

        public double PositionX
        {
            get
            {
                return this.positionX;
            } 

            set 
            {
                this.positionX = GetValidatedPercentage(value);
                this.OnPropertyChanged("PositionX");
            }
        }

        public double PositionY 
        {
            get
            {
                return this.positionY;
            }

            set
            {
                this.positionY = GetValidatedPercentage(value);
                this.OnPropertyChanged("PositionY");
            }
        }

        public double Width 
        {
            get
            {
                return this.width;
            }

            set
            {
                this.width = GetValidatedPercentage(value);
                this.OnPropertyChanged("Width");
            }
        }

        public double Height
        {
            get
            {
                return this.height;
            }

            set
            {
                this.height = GetValidatedPercentage(value);
                this.OnPropertyChanged("Height");
            }
        }

        public OverlayAsset Clone()
        {
            var clonedAsset = new OverlayAsset
                {
                    Height = this.Height,
                    Width = this.Width,
                    PositionX = this.PositionX,
                    PositionY = this.PositionY,
                    Title = this.Title,
                    ProviderUri = this.ProviderUri,
                    ModifiedBy = this.ModifiedBy,
                    Modified = this.Modified,
                    Created = this.Created,
                    Creator = this.Creator,
                    XamlResource = this.XamlResource,
                    DurationInSeconds = this.DurationInSeconds
                };

            clonedAsset.Metadata = new List<MetadataField>();

            this.Metadata.ForEach(mf => clonedAsset.Metadata.Add(new MetadataField(mf.Name, mf.Value)));

            return clonedAsset;
        }

        private static double GetValidatedPercentage(double value)
        {
            if (value > 100)
            {
                return 100.0;
            }

            if (value < 0.0)
            {
                return 0.0;
            }

            return value;
        }
    }
}