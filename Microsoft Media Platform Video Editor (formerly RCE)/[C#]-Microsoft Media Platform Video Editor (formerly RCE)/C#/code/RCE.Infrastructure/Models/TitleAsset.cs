// <copyright file="TitleAsset.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: TitleAsset.cs                     
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

    /// <summary>
    /// A class that represents an title asset.
    /// </summary>
    public class TitleAsset : Asset
    {
        /// <summary>
        /// Main text of the title.
        /// </summary>
        private string mainText;

        /// <summary>
        /// Sub text of the title.
        /// </summary>
        private string subText;

        /// <summary>
        /// Initializes a new instance of the <see cref="TitleAsset"/> class.
        /// </summary>
        public TitleAsset()
        {
            this.Id = Guid.NewGuid();
        }

        /// <summary>
        /// Gets or sets the main text.
        /// </summary>
        /// <value>The main text.</value>
        public string MainText
        {
            get
            {
                return this.mainText;
            }

            set
            {
                this.mainText = value;
                this.OnPropertyChanged("MainText");
            }
        }

        /// <summary>
        /// Gets or sets the sub text.
        /// </summary>
        /// <value>The sub text.</value>
        public string SubText
        {
            get
            {
                return this.subText;
            }

            set
            {
                this.subText = value;
                this.OnPropertyChanged("SubText");
            }
        }

        /// <summary>
        /// Gets or sets the title template.
        /// </summary>
        /// <value>The title template.</value>
        public TitleTemplate TitleTemplate { get; set; }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>The Cloned <see cref="TitleAsset"/>.</returns>
        public TitleAsset Clone()
        {
            return new TitleAsset { Id = this.Id, Title = this.Title, MainText = this.MainText, SubText = this.SubText, Source = this.Source };
        }
    }
}
