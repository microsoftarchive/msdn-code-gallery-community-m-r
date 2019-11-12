// <copyright file="MetadataView.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MetadataView.xaml.cs                     
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
    /// Class to load the MetadataView view that implements <see cref="IMetadataView"/>.
    /// </summary>
    public partial class MetadataView : UserControl, IMetadataView
    {
        /// <summary>
        /// Initializes a new instance of the MetadataView class.
        /// </summary>
        public MetadataView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>The model.</value>
        public IMetadataViewPresentationModel Model
        {
            get
            {
                return this.DataContext as IMetadataViewPresentationModel;
            }

            set
            {
                this.DataContext = value;
            }
        }
    }
}
