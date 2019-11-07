// <copyright file="PlayByPlayEditBox.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: PlayByPlayEditBox.xaml.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.PlayByPlay.Views.TimelineBar
{
    using System.Windows;
    using System.Windows.Controls;

    public partial class PlayByPlayEditBox : UserControl, IPlayByPlayEditBox
    {
        public PlayByPlayEditBox()
        {
            InitializeComponent();
        }

        public IPlayByPlayBoxesPresentationModel Model
        {
            get { return this.DataContext as IPlayByPlayBoxesPresentationModel; }
            set { this.DataContext = value; }
        }

        public void Close()
        {
            this.Popup.IsOpen = false;
        }

        public void Show()
        {
            this.Popup.IsOpen = true;
        }
    }
}
