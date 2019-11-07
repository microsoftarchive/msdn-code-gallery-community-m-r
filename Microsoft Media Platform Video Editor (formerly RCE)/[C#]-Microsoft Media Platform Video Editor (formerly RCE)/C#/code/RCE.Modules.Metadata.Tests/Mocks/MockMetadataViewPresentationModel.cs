// <copyright file="MockMetadataViewPresentationModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockMetadataViewPresentationModel.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Metadata.Tests.Mocks
{
    using System.Collections.Generic;
    using RCE.Infrastructure.Models;

    public class MockMetadataViewPresentationModel : IMetadataViewPresentationModel
    {
        private IMetadataView view = new MockMetadataView();

        public bool ShowMetadataInformation { get; set; }

        /// <summary>
        /// Gets or sets the value of the [View] as IMetadataView
        /// </summary>
        public IMetadataView View
        {
            get
            {
                return this.view;
            }

            set
            {
                this.view = value;
            }
        }
        
        /// <summary>
        /// Gets or sets a <see cref="AssetMetadata"/> list that has metadata detials for the asset.
        /// </summary>
        public IList<AssetMetadata> AssetMetadataDetails { get; set; }

        /// <summary>
        /// Responsible for showing the metadata details region when the user click the [Show Metadat] icon.
        /// </summary>
        /// <param name="asset">The asset for which the metadata needs to be displayed.</param>
        public void ShowMetadata(Asset asset)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Responsible for hiding the metadata details region.
        /// </summary>
        /// <param name="asset">The asset for which the metadata needs to be displayed.</param>
        public void HideMetadata(object asset)
        {
            throw new System.NotImplementedException();
        }
    }
}
