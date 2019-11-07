// <copyright file="OverlayPreview.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: OverlayPreview.xaml.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Overlays.Views.Previews
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;
    using System.Windows.Media;
    using System.Windows.Media.Animation;

    using RCE.Infrastructure.Models;

    public partial class OverlayPreview : UserControl
    { 
        /// <summary>
        /// The <seealso cref="Canvas"/> to store the XAML of the overlay.
        /// </summary>
        private Canvas xamlResource;

        public OverlayPreview()
        {
            this.Loaded += this.OverlayPreview_Loaded;
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the title template.
        /// </summary>
        /// <value>The title template.</value>
        public OverlayAsset Overlay
        {
            get
            {
                return this.DataContext as OverlayAsset;
            }

            set
            {
                this.DataContext = value;
            }
        }

        /// <summary>
        /// Gets the XAML of a title template of the title.
        /// </summary>
        /// <returns>A <seealso cref="Canvas"/> that represents the title template.</returns>
        private Canvas GetPreview()
        {
            if (this.xamlResource == null && this.Overlay != null)
            {
                this.xamlResource = XamlReader.Load(this.Overlay.XamlResource) as Canvas;
            }

            return this.xamlResource;
        }

        private void DisplayOverlayPreview()
        {
            Canvas preview = this.GetPreview();
            if (preview != null)
            {
                if (!this.PreviewCanvas.Children.Contains(preview))
                {
                    preview.RenderTransform = new ScaleTransform
                        {
                            ScaleX = 0.5,
                            ScaleY = 0.5
                        };

                    // clip
                    this.PreviewCanvas.Clip = new RectangleGeometry
                        {
                            Rect = new Rect(0, 0, this.PreviewCanvas.ActualWidth, this.PreviewCanvas.ActualHeight)
                        };
                    this.PreviewCanvas.Children.Add(preview);
                }

                ////this.DescriptionCanvas.Visibility = Visibility.Collapsed;
                this.PreviewCanvas.Visibility = Visibility.Visible;
                Storyboard inStoryboard = (Storyboard)preview.Resources["InTransition"];
                inStoryboard.Begin();
            }
        }

        private void OverlayPreview_Loaded(object sender, RoutedEventArgs e)
        {
            this.DisplayOverlayPreview();
        }
    }
}
