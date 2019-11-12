// <copyright file="FolderAsset.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: FolderAsset.cs                     
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
    using System.Collections.ObjectModel;

    /// <summary>
    /// Specifies the folder asset properties.
    /// </summary>
    public class FolderAsset : Asset
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FolderAsset"/> class.
        /// </summary>
        public FolderAsset()
        {
            this.Id = Guid.NewGuid();
            this.Assets = new ObservableCollection<Asset>();
        }

        /// <summary>
        /// Gets or sets the parent folder of the given folder.
        /// </summary>
        /// <value>The parent folder.</value>
        public FolderAsset ParentFolder { get; set; }

        /// <summary>
        /// Gets the assets in the folder.
        /// </summary>
        /// <value>The assets.</value>
        public ObservableCollection<Asset> Assets { get; private set; }

        /// <summary>
        /// Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
        /// </summary>
        /// <returns> A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</returns>
        public override string ToString()
        {
            return "Folder";
        }

        /// <summary>
        /// Adds the assets.
        /// </summary>
        /// <param name="assets">The assets.</param>
        public void AddAssets(IList<Asset> assets)
        {
            foreach (Asset asset in assets)
            {
                this.Assets.Add(asset);   
            }
        }
    }
}
