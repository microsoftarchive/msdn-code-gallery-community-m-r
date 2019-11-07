// <copyright file="OverlayMetadataView.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: OverlayMetadataView.xaml.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Metadata.Views
{
    using System.Windows.Controls;

    public partial class OverlayMetadataView : UserControl, IOverlayMetadataView
    {
        public OverlayMetadataView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>The model.</value>
        public IOverlayMetadataViewPresentationModel Model
        {
            get
            {
                return this.DataContext as IOverlayMetadataViewPresentationModel;
            }

            set
            {
                this.DataContext = value;
            }
        }
    }
}
