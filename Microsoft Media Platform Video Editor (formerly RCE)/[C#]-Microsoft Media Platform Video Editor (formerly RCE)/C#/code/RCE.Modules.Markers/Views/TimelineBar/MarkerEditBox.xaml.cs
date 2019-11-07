// <copyright file="MarkerEditBox.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MarkerEditBox.xaml.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Markers
{
    using System.Windows;
    using System.Windows.Controls;

    public partial class MarkerEditBox : UserControl, IMarkerEditBox
    {
        public MarkerEditBox()
        {
            InitializeComponent();
        }

        public IMarkerEditBoxPresentationModel Model
        {
            get { return this.DataContext as IMarkerEditBoxPresentationModel; }
            set { this.DataContext = value; }
        }

        public void Close()
        {
            this.EditBoxPopup.IsOpen = false;
        }

        public void Show()
        {
            this.EditBoxPopup.IsOpen = true;
            this.MarkerTextBox.Focus();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.MarkerTextBox.Focus();
        }
    }
}
