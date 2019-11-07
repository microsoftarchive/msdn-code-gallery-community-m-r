// <copyright file="AdsListView.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: AdsListView.xaml.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>
namespace RCE.Modules.Ads.Views
{
    using System.Windows.Controls;

    public partial class AdsListView : UserControl, IAdsListView
    {
        public AdsListView()
        {
            InitializeComponent();
        }

        public IAdsListViewPresentationModel Model
        {
            get
            {
                return this.DataContext as IAdsListViewPresentationModel;
            }

            set
            {
                this.DataContext = value;
            }
        }
    }
}
