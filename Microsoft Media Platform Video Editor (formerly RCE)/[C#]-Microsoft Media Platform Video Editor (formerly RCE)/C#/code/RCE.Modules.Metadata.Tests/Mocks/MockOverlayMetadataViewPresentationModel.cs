// <copyright file="MockOverlayMetadataViewPresentationModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockOverlayMetadataViewPresentationModel.cs                     
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
    using RCE.Infrastructure.Models;
    using RCE.Modules.Metadata.Views;

    public class MockOverlayMetadataViewPresentationModel : IOverlayMetadataViewPresentationModel
    {
        private IOverlayMetadataView view = new MockOverlayMetadataView();

        public bool ShowMetadataInformation { get; set; }

        public IOverlayMetadataView View
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

        public OverlayAsset Overlay
        {
            get
            {
                return null;
            }
        }
    }
}
