// <copyright file="PlayByPlayView.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: PlayByPlayView.xaml.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.PlayByPlayMarker
{
    using System;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using RCE.Infrastructure;
    using RCE.Modules.PlayByPlay.Views.TimelineBar;

    public partial class PlayByPlayView : UserControl, IPlayByPlayViewPreview
    {
        private readonly Brush originalBrush;
        
        private readonly Brush hoverBrush;
        
        private long lastClickTicks;

        public PlayByPlayView()
        {
            InitializeComponent();
            this.originalBrush = this.Icon.Fill;
            this.hoverBrush = new SolidColorBrush(Colors.Green);

            this.MouseLeftButtonDown += this.PlayByPlayView_MouseLeftButtonDown;
            this.MouseEnter += this.PlayByPlayView_MouseEnter;
            this.MouseLeave += this.PlayByPlayView_MouseLeave;
        }

        public IPlayByPlayBoxesPresentationModel Model
        {
            get { return this.DataContext as IPlayByPlayBoxesPresentationModel; }
            set { this.DataContext = value; }
        }

        private void PlayByPlayView_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Model.CloseDisplayView();
            this.Icon.Fill = this.originalBrush;
        }

        private void PlayByPlayView_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Model.ShowDisplayBox();
            this.Icon.Fill = this.hoverBrush;
        }

        private void PlayByPlayView_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Model.RaisePreviewClickedEvent();

            if (this.IsDoubleClick())
            {
                this.Model.ShowEditBox();
            }

            this.lastClickTicks = DateTime.Now.Ticks;
        }

        private bool IsDoubleClick()
        {
            return (DateTime.Now.Ticks - this.lastClickTicks) < UtilityHelper.MouseDoubleClickDurationValue;
        }
    }
}
