// <copyright file="MarkersListView.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MarkersListView.xaml.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Markers.Views
{
    using System;
    using System.Windows.Controls;

    public partial class MarkersListView : UserControl, IMarkersListView
    {
        public MarkersListView()
        {
            InitializeComponent();
        }

        public void SetViewModel(object viewModel)
        {
            this.DataContext = viewModel;
        }
    }
}
