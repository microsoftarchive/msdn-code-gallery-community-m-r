// <copyright file="MarkerView.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MarkerView.xaml.cs                     
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
    using System;
    using System.Windows.Controls;
    using Infrastructure;

    public partial class MarkerView : UserControl, IMarkerViewPreview
    {
        /// <summary>
        /// Last mouse click time to identify the double click event.
        /// </summary>
        private long lastClickTicks;

        public MarkerView()
        {
            InitializeComponent();
        }

        public IMarkerEditBoxPresentationModel Model
        {
            get { return this.DataContext as IMarkerEditBoxPresentationModel; }
            set { this.DataContext = value; }
        }

        private void UserControl_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if ((DateTime.Now.Ticks - this.lastClickTicks) < UtilityHelper.MouseDoubleClickDurationValue)
            {
                this.Model.ShowEditBox();
            }

            this.lastClickTicks = DateTime.Now.Ticks;
        }
    }
}
