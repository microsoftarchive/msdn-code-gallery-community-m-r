// <copyright file="AdView.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: AdView.xaml.cs                     
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
    using System;
    using System.Windows.Controls;
    using Infrastructure;

    public partial class AdView : UserControl, IAdViewPreview
    {
        /// <summary>
        /// Last mouse click time to identify the double click event.
        /// </summary>
        private long lastClickTicks;

        public AdView()
        {
            InitializeComponent();
        }

        public IAdEditBoxPresentationModel Model
        {
            get { return this.DataContext as IAdEditBoxPresentationModel; }
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
