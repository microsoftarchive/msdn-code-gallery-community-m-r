// <copyright file="OverlayItem.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: OverlayItem.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>
namespace RCE.Services.Contracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// A class that describes an Overlay Item
    /// </summary>
#if !SILVERLIGHT
    [Serializable]
#endif
    [DataContract(Namespace = "http://schemas.microsoft.com/rce/")]
    public class OverlayItem : Item
    {
        private double y;

        private double x;

        private double height;

        private double width;

        [DataMember]
        public double Height
        {
            get
            {
                return this.height;
            }

            set
            {
                this.height = GetValidatedPercentage(value);
            }
        }

        [DataMember]
        public double Width
        {
            get
            {
                return this.width;
            }

            set
            {
                this.width = GetValidatedPercentage(value);
            }
        }

        [DataMember]
        public double X
        {
            get
            {
                return this.x;
            }

            set
            {
                this.x = GetValidatedPercentage(value);
            }
        }

        [DataMember]
        public double Y
        {
            get
            {
                return this.y;
            }

            set
            {
                this.y = GetValidatedPercentage(value);
            }
        }

        [DataMember]
        public string XamlTemplate { get; set; }

        [DataMember]
        public double Duration { get; set; }

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
