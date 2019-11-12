// <copyright file="ClipMetadataView.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ClipMetadataView.xaml.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Metadata
{
    using System.Windows.Controls;

    /// <summary>
    /// Class to load the ClipMetadataView view that implements <see cref="IClipMetadataView"/>.
    /// </summary>
    public partial class ClipMetadataView : UserControl, IClipMetadataView
    {
        /// <summary>
        /// Initializes a new instance of the ClipMetadataView class.
        /// </summary>
        public ClipMetadataView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>The model.</value>
        public IClipMetadataViewPresentationModel Model
        {
            get
            {
                return this.DataContext as IClipMetadataViewPresentationModel;
            }

            set
            {
                this.DataContext = value;
            }
        }
    }
}
