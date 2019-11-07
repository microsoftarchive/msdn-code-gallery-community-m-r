// <copyright file="LibraryView.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: LibraryView.xaml.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Library
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using Infrastructure;
    using Infrastructure.Models;
    using RCE.Infrastructure.Controls;

    /// <summary>
    /// View for the Asset library.
    /// </summary>
    public partial class LibraryView : UserControl, ILibraryView
    {
        // Todo: Get this value from the config file.

        /// <summary>
        /// Value by which the slider should move.
        /// </summary>
        private const double ShiftScaleMargin = 0.05;

        /// <summary>
        /// Margin in x coordinate for full screen mode.
        /// </summary>
        private const double ListRightOffset = 35;

        /// <summary>
        /// Margin in y coordinate for full screen mode.
        /// </summary>
        private const double ListBottomOffset = 20;

        /// <summary>
        /// Minimum possible asset height of the asset in the list.
        /// </summary>
        private readonly double assetMinHeight;

        /// <summary>
        /// Minimum possible asset width of the asset in the list.
        /// </summary>
        private readonly double assetMinWidth;

        /// <summary>
        /// AspectRatio of the items in the asset listbox.
        /// </summary>
        private readonly double previewAspectRatio;

        /// <summary>
        /// Size of the preview controls.
        /// </summary>
        private readonly Size previewSize = new Size(90, 90);

        /// <summary>
        /// Current selected asset.
        /// </summary>
        private AssetPreview currentAsset;

        /// <summary>
        /// Ticks for the last click of the mouse.
        /// </summary>
        private long lastClickTicks;

        /// <summary>
        /// Size of the listbox items(Varies on slider movement).
        /// </summary>
        private Size currentAssetSize;

        /// <summary>
        /// True is if the control is in list box size change event.
        /// </summary>
        private bool lstSizeChanging;

        /// <summary>
        /// True if the control has been loaded.
        /// </summary>
        private bool isLoaded;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryView"/> class.
        /// </summary>
        public LibraryView()
        {
            InitializeComponent();

            // Initilaize values related to the scaling assets.
            this.assetMinHeight = this.previewSize.Height;
            this.assetMinWidth = this.previewSize.Width;
            this.previewAspectRatio = this.previewSize.Width / this.previewSize.Height;
            this.currentAssetSize = new Size(this.previewSize.Width, this.previewSize.Height);
        }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>The model.</value>
        public ILibraryViewPresentationModel Model
        {
            get
            {
                return this.DataContext as ILibraryViewPresentationModel;
            }

            set
            {
                this.DataContext = value;
            }
        }

        /// <summary>
        /// Gets the maximum possible height of the preview.
        /// </summary>
        /// <value>The max height of the preview.</value>
        private double MaxPreviewHeight
        {
            get
            {
                return this.AssetsList.ActualHeight - ListBottomOffset > this.assetMinHeight ? this.AssetsList.ActualHeight - ListBottomOffset : this.assetMinHeight;
            }
        }

        /// <summary>
        /// Gets the maximum possible width of the preview.
        /// </summary>
        /// <value>The max width of the preview.</value>
        private double MaxPreviewWidth
        {
            get
            {
                return this.AssetsList.ActualWidth - ListRightOffset > this.assetMinWidth ? this.AssetsList.ActualWidth - ListRightOffset : this.assetMinWidth;
            }
        }

       /// <summary>
        /// It addes the metadata fields in the list view of the assets.
        /// </summary>
        /// <param name="metadataFields">Name of the metadata fields.</param>
        public void AddMetadataFields(IList<string> metadataFields)
        {
            foreach (string field in metadataFields)
            {
                if (UtilityHelper.IsMetadataFieldExist(field))
                {
                    DataGridTextColumn column = new DataGridTextColumn { Header = field };

                    Binding binding;
                    if (field == "Duration")
                    {
                        binding = new Binding(field)
                                      {
                                          Converter = new Infrastructure.Converters.DurationConverter()
                                      };
                    }
                    else
                    {
                        binding = new Binding(field.Replace(" ", string.Empty));
                    }

                    column.Binding = binding;
                    DetailsDataGrid.Columns.Add(column);
                }
            }
        }

        /// <summary>
        /// Shows a progress bar.
        /// </summary>
        public void ShowProgressBar()
        {
            this.ProgressBar.Visibility = Visibility.Visible;
            this.Spinner.BeginAnimation();
        }

        /// <summary>
        /// Hides the progress bar.
        /// </summary>
        public void HideProgressBar()
        {
            this.ProgressBar.Visibility = Visibility.Collapsed;
            this.Spinner.StopAnimation();
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the <see cref="LibraryView"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((DateTime.Now.Ticks - this.lastClickTicks) < UtilityHelper.MouseDoubleClickDurationValue)
            {
                Asset asset = this.AssetsList.SelectedItem as Asset;

                if (asset != null)
                {
                    this.Model.OnAssetSelected(asset);    
                }
                
                this.lastClickTicks = 0;
            }

            this.lastClickTicks = DateTime.Now.Ticks;
        }

        /// <summary>
        /// Handles the Checked event of the ThumbButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ThumbButton_Checked(object sender, RoutedEventArgs e)
        {
            if (this.AssetsList != null)
            {
                this.AssetsList.Visibility = Visibility.Visible;
            }

            if (this.DetailsDataGrid != null)
            {
                this.DetailsDataGrid.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Handles the Checked event of the ListButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ListButton_Checked(object sender, RoutedEventArgs e)
        {
            if (this.AssetsList != null)
            {
                this.AssetsList.Visibility = Visibility.Collapsed;
            }

            if (this.DetailsDataGrid != null)
            {
                this.DetailsDataGrid.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Handles the Loaded event of the UserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!this.isLoaded)
            {
                Binding addItemCommand = new Binding("AddItemCommand") { Source = this.DataContext };
                ((BindingHelper)this.Resources["AddItemCommand"]).SetBinding(BindingHelper.ValueProperty, addItemCommand);

                Binding playSelectedAssetCommand = new Binding("PlaySelectedAssetCommand") { Source = this.DataContext };
                ((BindingHelper)this.Resources["PlaySelectedAssetCommand"]).SetBinding(BindingHelper.ValueProperty, playSelectedAssetCommand);

                this.isLoaded = true;
            }
        }

        /// <summary>
        /// Handles the Add event of the Asset control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Asset_Add(object sender, EventArgs e)
        {
            AssetPreview preview = sender as AssetPreview;

            if (preview != null)
            {
                this.Model.OnAddAsset(preview.Asset);
            }
        }
    }
}
