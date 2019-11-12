// <copyright file="MediaBinView.xaml.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MediaBinView.xaml.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.MediaBin
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Windows;
    using System.Windows.Browser;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;

    using Library;
    using RCE.Infrastructure;
    using RCE.Infrastructure.Models;
    using RCE.Modules.Library.Views.MediaBin;

    /// <summary>
    /// The view for the MediaBin module.
    /// </summary>
    public partial class MediaBinView : UserControl, IMediaBinView
    {
        // Todo: Get this value from the config file.
        
        /// <summary>
        /// Minimum possible asset height of the asset in the list.
        /// </summary>
        private readonly double assetMinHeight;

        /// <summary>
        /// Minimum possible asset width of the asset in the list.
        /// </summary>
        private readonly double assetMinWidth;

        /// <summary>
        /// Size of the preview controls.
        /// </summary>
        private readonly Size previewSize = new Size(90, 90);

        /// <summary>
        /// Ticks for the last click of the mouse.
        /// </summary>
        private long lastClickTicks;

        /// <summary>
        /// Size of the listbox items(Varies on slider movement).
        /// </summary>
        private Size currentAssetSize;

        /// <summary>
        /// True if the control has been loaded.
        /// </summary>
        private bool isLoaded;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaBinView"/> class.
        /// </summary>
        public MediaBinView()
        {
            InitializeComponent();

            HtmlPage.RegisterScriptableObject("MediaBin", this);

            // Key Commands
            // Register to MouseWheel event.
            if (Application.Current.RootVisual != null)
            {
                Application.Current.RootVisual.KeyUp += this.RootVisual_KeyUp;
            }

            // Initilaize values related to the scaling assets.
            this.currentAssetSize = this.previewSize;
            this.assetMinHeight = this.previewSize.Height;
            this.assetMinWidth = this.previewSize.Width;
        }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>The model.</value>
        public IMediaBinViewPresentationModel Model
        {
            get
            {
                return this.DataContext as IMediaBinViewPresentationModel;
            }

            set
            {
                this.DataContext = value;
            }
        }

        /// <summary>
        /// It addes the metadataFields fields in the list view of the assets.
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
        /// Gets the media bin.
        /// </summary>
        /// <returns>The array of provider uri of the all the assets.</returns>
        [ScriptableMember]
        public string[] GetMediaBin()
        {
            return this.Model.GetMediaBin();
        }

        /// <summary>
        /// Shows the messagebox and gets the delete asset confirmation.
        /// </summary>
        public void GetDeleteAssetConfirmation()
        {
            HtmlPage.Window.Dispatcher.BeginInvoke(() => this.DisplayAssetDeleteConfirmation());
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the UserControl control as well as the double click.
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
        /// Handles the KeyDown event of the RootVisual control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void RootVisual_KeyUp(object sender, KeyEventArgs e)
        {
            ContentControl contentControl = e.OriginalSource as ContentControl;

            bool handleKey = e.OriginalSource is ListBoxItem ||
                             (contentControl != null &&
                              contentControl.Name.ToUpper(CultureInfo.CurrentCulture).StartsWith("SUBCLIPPLAYHEAD"));

            if (this.Model.IsActive)
            {
                switch (e.Key)
                {
                    case Key.A:
                        if (handleKey)
                        {
                            this.AddAssetToTimeline();
                        }

                        break;
                }
            }
        }

        /// <summary>
        /// Adds the asset to timeline.
        /// </summary>
        private void AddAssetToTimeline()
        {
            Asset selectedAsset = this.AssetsList.SelectedItem as Asset;
            if (selectedAsset != null && !(selectedAsset is FolderAsset))
            {
                if (selectedAsset is VideoAsset)
                {
                    // VideoPreview preview = this.AssetsList.GetChildControls<VideoPreview>().Where(x => x.Asset == selectedAsset).Single();
                    VideoPreview preview = this.AssetsList.GetChildControls<VideoPreview>().Where(x => x.Asset == selectedAsset).Single();
                    VideoAsset videoAsset = selectedAsset as VideoAsset;
                    VideoAssetInOut videoAssetInOut = new VideoAssetInOut(videoAsset);
                    this.Model.AddAssetToTimeline(videoAssetInOut);
                }
                else
                {
                    this.Model.AddAssetToTimeline(selectedAsset);
                }
            }
        }

        /// <summary>
        /// Displays the asset delete confirmation messagebox.
        /// </summary>
        private void DisplayAssetDeleteConfirmation()
        {
            //// This is required else the IE browser crashes. This is the bug in Silverlight v2.0.
            //// Refer http://silverlight.net/forums/p/61357/152971.aspx for details
            
            MessageBoxResult result = MessageBox.Show(
                                                        "This will delete the current asset from the project and the instances on the timelines. This action cannot be undone. Are you sure you want to proceed?",
                                                        "Delete Media Asset",
                                                        MessageBoxButton.OKCancel);
            if (MessageBoxResult.OK == result)
            {
                this.Model.DeleteCurrentAsset();
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
                Binding playSelectedAssetCommand = new Binding("PlaySelectedAssetCommand") { Source = this.DataContext };
                ((BindingHelper)this.Resources["PlaySelectedAssetCommand"]).SetBinding(BindingHelper.ValueProperty, playSelectedAssetCommand);

                this.isLoaded = true;
            }

            Binding dropCommand = new Binding("DropCommand") { Source = this.DataContext };
            ((BindingHelper)this.Resources["DropItemCommand"]).SetBinding(BindingHelper.ValueProperty, dropCommand);
        }

        /// <summary>
        /// Handles the TextChanged event of the FolderName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.TextChangedEventArgs"/> instance containing the event data.</param>
        private void FolderName_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Model.FolderTitle = this.FolderName.Text;
            this.Model.AddFolderCommand.RaiseCanExecuteChanged();
        }
    }
}
