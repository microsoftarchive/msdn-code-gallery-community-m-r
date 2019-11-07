// <copyright file="MenuButtonView.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MenuButtonView.xaml.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Menu
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;

    public partial class MenuButtonView : UserControl, IMenuButtonView
    {
        public MenuButtonView()
        {
            InitializeComponent();
        }

        public void SetViewModel(object viewModel)
        {
            this.DataContext = viewModel;
        }
    }
}
