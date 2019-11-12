// <copyright file="SearchView.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: SearchView.xaml.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Search
{
    using System.Windows.Controls;

    public partial class SearchView : UserControl, ISearchView
    {
        public SearchView()
        {
            InitializeComponent();
        }

        public ISearchViewPresentationModel Model
        {
            get { return this.DataContext as ISearchViewPresentationModel; }
            set { this.DataContext = value; }
        }
    }
}