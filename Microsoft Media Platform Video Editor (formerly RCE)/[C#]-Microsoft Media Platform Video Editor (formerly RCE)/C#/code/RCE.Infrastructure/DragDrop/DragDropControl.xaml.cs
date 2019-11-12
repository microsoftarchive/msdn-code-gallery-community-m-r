// <copyright file="DragDropControl.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: DragDropControl.xaml.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Represents the drag and drop visualization.
    /// </summary>
    public partial class DragDropControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DragDropControl"/> class.
        /// </summary>
        public DragDropControl()
        {
            InitializeComponent();
        }

        public object DragContent
        {
            get { return this.DragContentControl.Content; }
            set { this.DragContentControl.Content = value; }
        }

        public DataTemplate DragTemplate
        {
            set
            {
                if (value != null)
                {
                    this.DragContentControl.ContentTemplate = value;
                    value.LoadContent();
                }
            }
        }

        public bool DropAllowed
        {
            set
            {
                if (value)
                {
                    this.CanDropImageContent.Visibility = Visibility.Visible;
                }
                else
                {
                    this.CanDropImageContent.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}