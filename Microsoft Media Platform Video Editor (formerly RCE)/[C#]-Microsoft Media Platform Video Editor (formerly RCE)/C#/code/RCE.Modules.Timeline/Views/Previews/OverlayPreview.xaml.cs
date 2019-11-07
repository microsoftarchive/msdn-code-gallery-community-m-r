// <copyright file="OverlayPreview.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: OverlayPreview.xaml.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Timeline
{
    using System;
    using System.Windows;
    using Infrastructure;
    using RCE.Infrastructure.Models;

    public partial class OverlayPreview : IPreview
    {
        public OverlayPreview(OverlayAsset asset)
        {
            InitializeComponent();

            if (asset == null)
            {
                throw new ArgumentNullException("asset");
            }

            this.DataContext = asset;
        }

        public event EventHandler<DataEventArgs<bool>> ItemLocked;

        public void SetSelected(bool selected)
        {
            this.SelectionBox.Visibility = selected ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
