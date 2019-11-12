// <copyright file="AdEditBox.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: AdEditBox.xaml.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Ads
{
    using System.Windows;
    using System.Windows.Controls;

    public partial class AdEditBox : UserControl, IAdEditBox
    {
        public AdEditBox()
        {
            InitializeComponent();
        }

        public IAdEditBoxPresentationModel Model
        {
            get { return this.DataContext as IAdEditBoxPresentationModel; }
            set { this.DataContext = value; }
        }

        public void Close()
        {
            this.EditBoxPopup.IsOpen = false;
        }

        public void Show()
        {
            this.EditBoxPopup.IsOpen = true;
        }
    }
}
