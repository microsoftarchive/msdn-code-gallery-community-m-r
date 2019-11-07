// <copyright file="MediaBinViewPresentationModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MediaBinViewPresentationModel.cs                     
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
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Resources;
    using System.Windows;

    using Infrastructure;
    using Infrastructure.DragDrop;
    using Infrastructure.Events;
    using Infrastructure.Models;
    using Library.Resources;
    using Microsoft.Practices.Composite.Events;
    using Microsoft.Practices.Composite.Presentation.Commands;
    using Microsoft.Practices.Composite.Presentation.Events;
    using Microsoft.Practices.Composite.Regions;

    using RCE.Infrastructure.Services;
    using RCE.Infrastructure.Windows;

    using SMPTETimecode;
    using Project = RCE.Infrastructure.Models.Project;

    /// <summary>
    /// Presentation model of the MediaBin view.
    /// </summary>
    public class MediaBinViewPresentationModel : BaseModel, IMediaBinViewPresentationModel, IWindowMetadataProvider
    {
        // Todo: Get this value from the config file.

        /// <summary>
        /// Value indicating the slider increment/decrement.
        /// </summary>
        private const double ShiftScaleMargin = 0.05;

        /// <summary>
        /// Assets Data Service facade object.
        /// </summary>
        private readonly IAssetsDataServiceFacade assetsDataServiceFacade;

        /// <summary>
        /// Event aggregator object.
        /// </summary>
        private readonly IEventAggregator eventAggregator;

        /// <summary>
        /// Region manger object.
        /// </summary>
        private readonly IRegionManager regionManager;

        /// <summary>
        /// Configuration manager object.
        /// </summary>
        private readonly IConfigurationService configurationService;

        /// <summary>
        /// Command object which executes when the user click on search button.
        /// </summary>
        private readonly DelegateCommand<string> searchCommand;

        /// <summary>
        /// Command object which executes when the user click on help button.
        /// </summary>
        private readonly DelegateCommand<string> helpViewCommand;

        /// <summary>
        /// Logger object.
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// Project service object to load the mediabin assets.
        /// </summary>
        private readonly IProjectService projectService;

        /// <summary>
        /// Command that executes when the user increment/decrement the slider value by +/- button.
        /// </summary>
        private readonly DelegateCommand<string> shiftSliderScaleCommand;

        /// <summary>
        /// Command that executes when the user add a folder.
        /// </summary>
        private readonly DelegateCommand<string> addFolderCommand;

        /// <summary>
        /// Command that executes when the user clicks the PLAY icon in the detail view.
        /// </summary>
        private readonly DelegateCommand<object> playSelectedAssetCommand;

        /// <summary>
        /// Command that executes when the user click on uparrow button to go to the parent folder.
        /// </summary>
        private readonly DelegateCommand<string> parentFolderCommand;

        /// <summary>
        /// Command that executes when the user click on the delete asset button.
        /// </summary>
        private readonly DelegateCommand<string> deleteAssetCommand;

        /// <summary>
        /// Resource manger object.
        /// </summary>
        private readonly ResourceManager resourceManager;

        /// <summary>
        /// List of filtered assets.
        /// </summary>
        private ObservableCollection<Asset> assets;

        /// <summary>
        /// List of all the assets in the mediabin.
        /// </summary>
        private ObservableCollection<Asset> currentAssets;

        /// <summary>
        /// Flag to show the video assets in the list.
        /// </summary>
        private bool showVideos;

        /// <summary>
        /// Flag to show the audio assets in the list.
        /// </summary>
        private bool showAudio;

        /// <summary>
        /// Flag to show the image assets in the list.
        /// </summary>
        private bool showImages;

        /// <summary>
        /// Flag to indicate if the thumbnail/list view is visible.
        /// true - thumbnail view is visible and list view is invisible.
        /// false - list view is visible and thumbnail view is inivisible.
        /// </summary>
        private bool isThumbChecked;

        /// <summary>
        /// Holds the title of the folder entered by the user in the UI.
        /// </summary>
        private string folderTitle;

        /// <summary>
        /// The value of the slider.
        /// </summary>
        private double scale;

        /// <summary>
        /// Current selected folder asset whose assets are visible in the list.
        /// </summary>
        private FolderAsset currentFolderAsset;

        /// <summary>
        /// Help popup window open property.
        /// </summary>
        private bool isHelpWindowOpen;

        /// <summary>
        /// To have the current selected asset in the thumbnail/list view.
        /// </summary>
        private Asset selectedAsset;

        private Asset clipBoardAsset;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaBinViewPresentationModel"/> class.
        /// </summary>
        /// <param name="view">The view of Media Bin Module.</param>
        /// <param name="assetsDataServiceFacade">The service facade.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="logger">The logger class object.</param>
        /// <param name="projectService">The project service.</param>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="configurationService">Tje Configuration service.</param>
        public MediaBinViewPresentationModel(IMediaBinView view, IAssetsDataServiceFacade assetsDataServiceFacade, IEventAggregator eventAggregator, ILogger logger, IProjectService projectService, IRegionManager regionManager, IConfigurationService configurationService)
        {
            this.logger = logger;
            this.assetsDataServiceFacade = assetsDataServiceFacade;
            this.assetsDataServiceFacade.LoadAssetsByLibraryIdCompleted += this.OnLoadAssetsByLibraryIdCompleted;
            this.eventAggregator = eventAggregator;
            this.projectService = projectService;
            this.projectService.ProjectRetrieved += this.RefreshMediaBin;
            this.regionManager = regionManager;
            this.configurationService = configurationService;
            this.currentAssets = new ObservableCollection<Asset>();
            this.View = view;

            this.resourceManager = new ResourceManager(typeof(Resources));

            this.searchCommand = new DelegateCommand<string>(this.Search);
            this.shiftSliderScaleCommand = new DelegateCommand<string>(this.ShiftScale, this.CanShiftScale);
            this.addFolderCommand = new DelegateCommand<string>(this.AddFolder, this.CanAddFolder);
            this.parentFolderCommand = new DelegateCommand<string>(this.ShowParentFolders, this.CanShowParentFolder);
            this.deleteAssetCommand = new DelegateCommand<string>(this.DeleteAsset, this.CanDeleteAsset);
            this.helpViewCommand = new DelegateCommand<string>(this.ShowHelpView);
            this.playSelectedAssetCommand = new DelegateCommand<object>(this.PlaySelectedAsset);
            this.DropCommand = new DelegateCommand<DropPayload>(this.DropAssetOnFolder);
            this.KeyboardActionCommand = new DelegateCommand<Tuple<KeyboardAction, object>>(this.ExecuteKeyboardAction);
            this.ShowImages = true;
            this.ShowVideos = true;
            this.ShowAudio = true;
            this.LoadMediaBin();
            this.PropertyChanged += this.MediaBinPresentationModel_PropertyChanged;

            this.eventAggregator.GetEvent<ResetWindowsEvent>().Subscribe(this.ResetWindow);
            this.eventAggregator.GetEvent<AddAssetEvent>()
                .Subscribe(this.AddAsset, ThreadOption.PublisherThread, true, this.AddAssetFilter);

            // Set the default value of the text box.
            this.FolderTitle = "Root";

            // Todo: Get this value from the config file.
            this.Scale = 0.2;
            
            // Add metadata fields.
            this.View.AddMetadataFields(this.configurationService.GetMetadataFields());
            this.IsThumbChecked = true;
            this.View.Model = this;
        }

        public event EventHandler<Infrastructure.DataEventArgs<object>> TitleUpdated;

        public event EventHandler<Infrastructure.DataEventArgs<object>> ResetPositionRaised;

        /// <summary>
        /// Gets the shiftSliderScaleCommand command.
        /// </summary>
        /// <value>The slider command.</value>
        public DelegateCommand<string> ShiftSliderScaleCommand
        {
            get
            {
                return this.shiftSliderScaleCommand;
            }
        }

        /// <summary>
        /// Gets the addFolderCommand command.
        /// </summary>
        /// <value>Add folder command.</value>
        public DelegateCommand<string> AddFolderCommand
        {
            get
            {
                return this.addFolderCommand;
            }
        }

        /// <summary>
        /// Gets the UpArrowCommand command.
        /// </summary>
        /// <value>Up arrow command.</value>
        public DelegateCommand<string> UpArrowCommand
        {
            get
            {
                return this.parentFolderCommand;
            }
        }

        /// <summary>
        /// Gets the command executed on drop elements.
        /// </summary>
        /// <value>The delegate command used to drop the elements.</value>
        public DelegateCommand<DropPayload> DropCommand { get; private set; }

        /// <summary>
        /// Gets the command executed on keyboard actions ocurrences.
        /// </summary>
        /// <value>The delegate command used to execute keyboard actons.</value>
        public DelegateCommand<Tuple<KeyboardAction, object>> KeyboardActionCommand { get; private set; }

        public KeyboardActionContext ActionContext
        {
            get
            {                
                return KeyboardActionContext.MediaBin;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance of help window is open.
        /// </summary>
        /// <value>
        /// A <seealso cref="bool"/> value.<c>true</c> if this instance is help window open; otherwise, <c>false</c>.
        /// </value>
        public bool IsHelpWindowOpen
        {
            get
            {
                return this.isHelpWindowOpen;
            }

            set
            {
                this.isHelpWindowOpen = value;
                this.OnPropertyChanged("IsHelpWindowOpen");
            }
        }

        /// <summary>
        /// Gets or sets the value of the folder title from the UI .
        /// </summary>
        /// <value>Folder text from the mediabin.</value>
        public string FolderTitle
        {
            get
            {
                return this.folderTitle;
            }

            set
            {
                this.folderTitle = value;
                this.OnPropertyChanged("FolderTitle");
                this.AddFolderCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Gets a value indicating whether the Mediabin is the active view.
        /// </summary>
        /// <value>True if MediaBin is active else false.</value>
        public bool IsActive
        {
            get
            {
                return this.regionManager.Regions[RegionNames.MainRegion].ActiveViews.Where(x => x == this.View).SingleOrDefault() != null;
            }
        }

        /// <summary>
        /// Gets or sets the selected asset in thumbnail view or list view.
        /// </summary>
        /// <value>The selected asset.</value>
        public Asset SelectedAsset
        {
            get
            {
                return this.selectedAsset;
            }

            set
            {
                this.selectedAsset = value;
                this.OnPropertyChanged("SelectedAsset");
                this.DeleteAssetCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether thumbnail view is visible or
        /// list view is visible.
        /// </summary>
        /// <value>The value would be <c>true</c> if thumbnail view is visible; otherwise, <c>false</c>.</value>
        public bool IsThumbChecked
        {
            get
            {
                return this.isThumbChecked;
            }

            set
            {
                this.isThumbChecked = value;
                this.OnPropertyChanged("IsThumbChecked");
            }
        }

        /// <summary>
        /// Gets or sets the scale value of the slider.
        /// </summary>
        /// <value>Value of the slider.</value>
        public double Scale
        {
            get
            {
                return this.scale;
            }

            set
            {
                // Raise the canexecute command on boundary conditions.
                if ((value == 0 || value == 1) || (this.scale == 0 && value > 0) || (this.scale == 1 && value < 1))
                {
                    // Set the scale value so that CanExecute method of ShiftSliderScaleCommand command can refer this value.
                    this.scale = value;
                    this.ShiftSliderScaleCommand.RaiseCanExecuteChanged();
                }

                this.scale = value;
                this.OnPropertyChanged("Scale");
            }
        }

        /// <summary>
        /// Gets or sets the view.
        /// </summary>
        /// <value>Object of the mediabin view.</value>
        public IMediaBinView View { get; set; }

        /// <summary>
        /// Gets or sets the assets.
        /// </summary>
        /// <value>The assets.</value>
        public ObservableCollection<Asset> Assets
        {
            get
            {
                return this.assets;
            }

            set
            {
                this.assets = value;
                this.OnPropertyChanged("Assets");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show videos].
        /// </summary>
        /// <value><c>True</c> If [show videos]; otherwise, <c>false</c>.</value>
        public bool ShowVideos
        {
            get
            {
                return this.showVideos;
            }

            set
            {
                this.showVideos = value;
                this.OnPropertyChanged("ShowVideos");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show audio].
        /// </summary>
        /// <value><c>True</c> If [show audio]; otherwise, <c>false</c>.</value>
        public bool ShowAudio
        {
            get
            {
                return this.showAudio;
            }

            set
            {
                this.showAudio = value;
                this.OnPropertyChanged("ShowAudio");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show images].
        /// </summary>
        /// <value><c>True</c> If [show images]; otherwise, <c>false</c>.</value>
        public bool ShowImages
        {
            get
            {
                return this.showImages;
            }

            set
            {
                this.showImages = value;
                this.OnPropertyChanged("ShowImages");
            }
        }

        /// <summary>
        /// Gets the search command.
        /// </summary>
        /// <value>The search command.</value>
        public DelegateCommand<string> SearchCommand
        {
            get
            {
                return this.searchCommand;
            }
        }

        /// <summary>
        /// Gets the header info.
        /// </summary>
        /// <value>The header info.</value>
        public string HeaderInfo
        {
            get { return this.resourceManager.GetString("MediaBinHeaderInfo"); }
        }

        /// <summary>
        /// Gets the header icon (on status).
        /// </summary>
        /// <value>The header icon.</value>
        public string HeaderIconOn
        {
            get { return this.resourceManager.GetString("MediaBinHeaderIconOn"); }
        }

        /// <summary>
        /// Gets the header icon (off status).
        /// </summary>
        /// <value>The header icon off status.</value>
        public string HeaderIconOff
        {
            get { return this.resourceManager.GetString("MediaBinHeaderIconOff"); }
        }

        /// <summary>
        /// Gets the add to media bin command.
        /// </summary>
        /// <value>The add to media bin command.</value>
        public DelegateCommand<string> HelpButtonCommand
        {
            get
            {
                return this.helpViewCommand;
            }
        }

        /// <summary>
        /// Gets the play selected asset.
        /// </summary>
        /// <value>The play selected asset.</value>
        public DelegateCommand<object> PlaySelectedAssetCommand
        {
            get
            {
                return this.playSelectedAssetCommand;
            }
        }

        /// <summary>
        /// Gets the delete asset button command.
        /// </summary>
        /// <value>The delete asset command.</value>
        public DelegateCommand<string> DeleteAssetCommand
        {
            get
            {
                return this.deleteAssetCommand;
            }
        }

        public VerticalWindowPosition VerticalPosition
        {
            get
            {
                return VerticalWindowPosition.Center;
            }
        }

        public HorizontalWindowPosition HorizontalPosition
        {
            get
            {
                return HorizontalWindowPosition.Left;
            }
        }

        public object Title
        {
            get
            {
                return "MediaBin";
            }
        }

        public ResizeDirection ResizeDirection
        {
            get
            {
                return Infrastructure.Windows.ResizeDirection.Both;
            }
        }

        public Size Size
        {
            get
            {
                return System.Windows.Size.Empty;
            }
        }

        /// <summary>
        /// Gets the Id of the current folder whose assets are visible in the media bin.
        /// </summary>
        /// <value>Id of the folder asset.</value>
        private Guid GetCurrentFolderAssetId
        {
            // Return current folder's parent folder id.
            // Return null if the current folder is null.
            get { return (this.currentFolderAsset == null) ? Guid.Empty : this.currentFolderAsset.Id; }
        }

        /// <summary>
        /// Gets the parent folder asset of the current folder.
        /// </summary>
        /// <value>Folder asset (Parent of the current folder).</value>
        private FolderAsset GetParentFolderAsset
        {
            get
            {
                if (this.currentFolderAsset == null || this.currentFolderAsset.ParentFolder == null)
                {
                    return null;
                }

                return this.currentFolderAsset.ParentFolder;
            }
        }

        /// <summary>
        /// Show the helpview popup.
        /// </summary>
        /// <param name="key">Key Value(It not used so can be null).</param>
        public void ShowHelpView(string key)
        {
            this.IsHelpWindowOpen = !this.IsHelpWindowOpen;
        }

        /// <summary>
        /// Deletes the current selected asset if there is no asset inside it(in case of folder asset).
        /// </summary>
        /// <param name="value">This value is not used.</param>
        public void DeleteAsset(string value)
        {
            if (this.CanDeleteAsset(null))
            {
                // It shows a confirmation messagebox to the user and after confirmation
                // it call back the DeleteCurrentAsset method to delete the asset.
                this.View.GetDeleteAssetConfirmation();
            }
        }

        public void ResetWindow(object obj)
        {
            EventHandler<Infrastructure.DataEventArgs<object>> handler = this.ResetPositionRaised;

            if (handler != null)
            {
                handler.Invoke(this, new Infrastructure.DataEventArgs<object>(this.View));
            }
        }

        /// <summary>
        /// Deletes the current selected asset.
        /// </summary>
        public void DeleteCurrentAsset()
        {
            if (this.SelectedAsset != null)
            {
                if (!(this.SelectedAsset is FolderAsset))
                {
                    this.eventAggregator.GetEvent<DeleteMediaBinAssetEvent>().Publish(this.SelectedAsset);
                }

                this.RemoveAssetFromOriginalSource(this.SelectedAsset);

                // Remove the asset from the current view otherwiset the current view wouldn't be changed.
                // We can call the FilterAsset also to refresh the view.
                this.Assets.Remove(this.SelectedAsset);

                this.AddFolderCommand.RaiseCanExecuteChanged(); 
            }
        }

        /// <summary>
        /// It shows the child assets of the folder if the asset is folder asset
        /// else publishes the player event to play the currently selected asset.
        /// </summary>
        /// <param name="asset">Current selected asset.</param>
        public void OnAssetSelected(Asset asset)
        {
            this.ShowChildFolders(asset);
        }

        /// <summary>
        /// Shows the metadata of the current selected timeline element.
        /// </summary>
        /// <param name="asset">The current timeline element.</param>
        public void ShowMetadata(TimelineElement timelineElement)
        {
            if (timelineElement != null && !(timelineElement.Asset is FolderAsset || timelineElement.Asset is TitleAsset))
            {
                this.eventAggregator.GetEvent<ShowMetadataEvent>().Publish(timelineElement);
            }
        }

        /// <summary>
        /// Returns the list of provider Uris of all the assets in the media bin.
        /// </summary>
        /// <returns>The list of provider Uris.</returns>
        public string[] GetMediaBin()
        {
            IList<string> currentMediaBin = new List<string>();

            if (this.currentAssets != null)
            {
                foreach (Asset asset in this.currentAssets)
                {
                    if (asset.ProviderUri != null)
                    {
                        currentMediaBin.Add(asset.ProviderUri.ToString());
                    }
                }
            }

            return currentMediaBin.ToArray();
        }

        /// <summary>
        /// Publish the <see cref="AddAssetToTimelineEvent"/> to add the asset in the timeline at the current playhead position.
        /// </summary>
        /// <param name="asset">Asset to be added to timeline.</param>
        public void AddAssetToTimeline(Asset asset)
        {
            if (asset != null && !(asset is FolderAsset))
            {
                this.eventAggregator.GetEvent<AddAssetToTimelineEvent>().Publish(asset);
            }
        }

        /// <summary>
        /// Finds the folder by folderid.
        /// </summary>
        /// <param name="actualAssets">The list of all the assets.</param>
        /// <param name="folderId">The folder id.</param>
        /// <returns>The <see cref="FolderAsset"/>.</returns>
        private static FolderAsset FindFolderById(IEnumerable<Asset> actualAssets, Guid folderId)
        {
            foreach (Asset currentAsset in actualAssets)
            {
                FolderAsset folder = currentAsset as FolderAsset;
                if (folder != null)
                {
                    if (folder.Id == folderId)
                    {
                        return folder;
                    }
                    else
                    {
                        FolderAsset subFolder = FindFolderById(folder.Assets, folderId);

                        if (subFolder != null)
                        {
                            return subFolder;
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Finds the parent folder of the given asset.
        /// </summary>
        /// <param name="actualAssets">The IEnumerable list of assets.</param>
        /// <param name="asset">The <see cref="Asset"/>.</param>
        /// <returns>The <see cref="FolderAsset"/>.</returns>
        private static FolderAsset FindFolderByAsset(IEnumerable<Asset> actualAssets, Asset asset)
        {
            foreach (Asset currentAsset in actualAssets)
            {
                FolderAsset folder = currentAsset as FolderAsset;
                if (folder != null)
                {
                    bool found = folder.Assets.Any(x => x.Id == asset.Id);

                    if (found)
                    {
                        return folder;
                    }
                    else
                    {
                        FolderAsset subFolder = FindFolderByAsset(folder.Assets, asset);

                        if (subFolder != null)
                        {
                            return subFolder;
                        }                        
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Determines whether given asset exist in the given list of assets.
        /// </summary>
        /// <param name="actualAssets">The list of assets.</param>
        /// <param name="asset">The <see cref="Asset"/>.</param>
        /// <returns>
        /// <c>true</c> if asset exists in the  list; otherwise, <c>false</c>.
        /// </returns>
        private static bool AssetExists(IEnumerable<Asset> actualAssets, Asset asset)
        {
            foreach (Asset currentAsset in actualAssets)
            {
                if (currentAsset.Id == asset.Id)
                {
                   return true;    
                }

                FolderAsset folder = currentAsset as FolderAsset;
                
                if (folder != null)
                {
                    if (AssetExists(folder.Assets, asset))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Add the given assets in the ObservableCollection object.
        /// </summary>
        /// <param name="collection">ObservableCollection object.</param>
        /// <param name="assets">Array of assets to be added in the collection.</param>
        private static void AddAssetRange(ObservableCollection<Asset> collection, IEnumerable<Asset> assets)
        {
            foreach (var obj in assets)
            {
                collection.Add(obj);
            }
        }

        /// <summary>
        /// Removes the asset from original source.
        /// </summary>
        /// <param name="asset">The asset.</param>
        private void RemoveAssetFromOriginalSource(Asset asset)
        {
            Asset currentAsset = this.currentAssets.SingleOrDefault(x => x.Id == asset.Id);

            if (currentAsset != null)
            {
                this.currentAssets.Remove(currentAsset);
            }
            else
            {
                FolderAsset folderAsset = FindFolderByAsset(this.currentAssets, asset);

                if (folderAsset != null)
                {
                    Asset assetToRemove = folderAsset.Assets.Single(x => x.Id == asset.Id);
                    folderAsset.Assets.Remove(assetToRemove);
                }
            }
        }

        /// <summary>
        /// Add the given asset in the local collection object.
        /// </summary>
        /// <param name="asset">Asset to be added.</param>
        private void AddAsset(Asset asset)
        {
            // Add the asset to the current asset as it is the original copy.
            this.currentAssets.Add(asset);

            // Check if in the current view ShowImage/ShowAudio/ShowVideo is selected or not.
            // Check if the current folder is the top most folder(GetCurrentFolderAssetId == null).
            // We are doing it so that we don't have to filter the assets again.
            if (asset is ImageAsset && this.ShowImages && this.GetCurrentFolderAssetId == Guid.Empty)
            {
                this.Assets.Add(asset);
            }
            else if (asset is AudioAsset && this.ShowAudio && this.GetCurrentFolderAssetId == Guid.Empty)
            {
                this.Assets.Add(asset);
            }
            else if (asset is VideoAsset && this.ShowVideos && this.GetCurrentFolderAssetId == Guid.Empty)
            {
                this.Assets.Add(asset);
            }
        }

        /// <summary>
        /// Check if the given asset can be deleted.
        /// </summary>
        /// <param name="value">This value is not used.</param>
        /// <returns>true if the asset can be deleted else false.</returns>
        private bool CanDeleteAsset(string value)
        {
            if (this.SelectedAsset == null)
            {
                return false;
            }

            // If the asset to be deleted is folder asset then it should be empty.
            FolderAsset folderAsset = this.SelectedAsset as FolderAsset;

            if (folderAsset != null && folderAsset.Assets.Count() > 0)
            {
                return false;
            }

            if (!this.currentAssets.Contains(this.SelectedAsset) && FindFolderByAsset(this.currentAssets, this.SelectedAsset) == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Loads the Mediabin of the given project.
        /// </summary>
        private void LoadMediaBin()
        {
            this.View.ShowProgressBar();

            if (this.projectService.State != ProjectState.Retrieved)
            {
                this.projectService.ProjectRetrieved += (sender, e) =>
                    this.LoadMediaBin(this.projectService.GetCurrentProject());
            }
            else
            {
                this.LoadMediaBin(this.projectService.GetCurrentProject());
            }
        }

        /// <summary>
        /// Load all the assets from the Mediabin of the project.
        /// </summary>
        /// <param name="project">The current project.</param>
        private void LoadMediaBin(Project project)
        {
            if (project != null)
            {
                if (project.MediaBin.ProviderUri != null)
                {
                    if (this.currentFolderAsset != null)
                    {
                        this.currentFolderAsset.AddAssets(project.MediaBin.Assets);
                    }
                    else
                    {
                        this.currentAssets = project.MediaBin.Assets;
                    }

                    this.FilterAssets();
                    this.UpArrowCommand.RaiseCanExecuteChanged();
                    this.AddFolderCommand.RaiseCanExecuteChanged();
                }
                else
                {
                    this.projectService.GetCurrentProject().MediaBin.AddAssets(new ObservableCollection<Asset>());
                    this.currentAssets = this.projectService.GetCurrentProject().MediaBin.Assets;
                    this.FilterAssets();
                }

                this.View.HideProgressBar();
            }
        }

        /// <summary>
        /// Searches the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        private void Search(string filter)
        {
            IEnumerable<Asset> currentFolderAssets = (this.currentFolderAsset != null) ? this.currentFolderAsset.Assets.ToList() : this.currentAssets.ToList();

            var filteredAssets = new ObservableCollection<Asset>();

            if (!string.IsNullOrEmpty(filter))
            {
                currentFolderAssets = currentFolderAssets.Where(x => x.Title.ToLowerInvariant().Contains(filter.ToLowerInvariant()));
            }

            this.FilterAssetsByType(filteredAssets, currentFolderAssets);

            this.Assets = filteredAssets;
        }

        private void RefreshMediaBin(object sender, EventArgs e)
        {
            this.ClearCurrentAssets();
            this.LoadMediaBin();
        }

        private void ClearCurrentAssets()
        {
            this.currentAssets.Clear();
        }

        /// <summary>
        /// Plays the selected asset.
        /// </summary>
        /// <param name="assetId">The asset id.</param>
        private void PlaySelectedAsset(object assetId)
        {
            Guid id = Guid.Empty;

            if (assetId is Guid)
            {
                id = (Guid)assetId;

                Asset asset = this.Assets.FirstOrDefault(x => x.Id.Equals(id));

                if (asset != null && !(asset is FolderAsset))
                {
                    var payload = new PlayerEventPayload { Asset = asset, PlayerMode = PlayerMode.MediaLibrary };
                    this.eventAggregator.GetEvent<PlayerEvent>().Publish(payload);
                }
            }
        }

        /// <summary>
        /// Handles the PropertyChanged event of the LibraryViewPresentationModel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void MediaBinPresentationModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("ShowVideos") || e.PropertyName.Equals("ShowAudio") || e.PropertyName.Equals("ShowImages"))
            {
                this.FilterAssets();

                this.logger.Log(e.PropertyName, TraceEventType.Warning);
            }
        }

        /// <summary>
        /// Filters the assets.
        /// </summary>
        private void FilterAssets()
        {
            ObservableCollection<Asset> filteredAssets = new ObservableCollection<Asset>();

            // Get all the assets in the current selected folder.
            IEnumerable<Asset> currentFolderAssets = (this.currentFolderAsset != null) ? this.currentFolderAsset.Assets.ToList() : this.currentAssets.ToList();

            this.FilterAssetsByType(filteredAssets, currentFolderAssets);

            this.Assets = filteredAssets;
        }

        /// <summary>
        /// Filter the assets by type.
        /// </summary>
        /// <param name="filteredAssets">The collection where the assets that satisfy the filter condition are being stored.</param>
        /// <param name="currentFolderAssets">The colletion where the filter is going to be applied.</param>
        private void FilterAssetsByType(ObservableCollection<Asset> filteredAssets, IEnumerable<Asset> currentFolderAssets)
        {
            if (this.ShowAudio)
            {
                AddAssetRange(filteredAssets, currentFolderAssets.Where(x => x is AudioAsset));
            }

            if (this.ShowVideos)
            {
                AddAssetRange(filteredAssets, currentFolderAssets.Where(x => x is VideoAsset));
            }

            if (this.ShowImages)
            {
                AddAssetRange(filteredAssets, currentFolderAssets.Where(x => x is ImageAsset));
            }

            // Add all the the folder assets which exist in the current selected folder.
            AddAssetRange(filteredAssets, currentFolderAssets.Where(x => x is FolderAsset));
        }

        /// <summary>
        /// Shifts the slider value by a fixed margin(Defined by ShiftScaleMargin variable).
        /// </summary>
        /// <param name="shiftType">It shold be + if the scale value is to be incremented else -.</param>
        private void ShiftScale(string shiftType)
        {
            switch (shiftType)
            {
                case "+":
                    if (this.Scale != 1)
                    {
                        this.Scale = (this.Scale + ShiftScaleMargin > 1) ? 1 : this.Scale + ShiftScaleMargin;
                    }

                    break;
                case "-":
                    if (this.Scale != 0)
                    {
                        this.Scale = (this.Scale - ShiftScaleMargin < 0) ? 0 : this.Scale - ShiftScaleMargin;
                    }

                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Check if the slider value can be increment/decrement.
        /// </summary>
        /// <param name="shiftType">It should be + if you want to test if the scale value can 
        /// increse and vice versa.</param>
        /// <returns>Returns if slider is not at the max or min value.</returns>
        private bool CanShiftScale(string shiftType)
        {
            switch (shiftType)
            {
                case "+":
                    if (this.scale < 1)
                    {
                        return true;
                    }

                    break;
                case "-":
                    if (this.scale > 0)
                    {
                        return true;
                    }

                    break;
                default:
                    break;
            }

            return false;
        }

        /// <summary>
        /// Adds a new folder in the current folder with the given name.
        /// </summary>
        /// <param name="folderName">Name of the new folder.</param>
        private void AddFolder(string folderName)
        {
            // When this method is called by the command then this value would be null as we are 
            // not passing any parameter from the xaml file.
            // We are checking null value so that we can reuse the same function with given folderName.
            folderName = string.IsNullOrEmpty(folderName) ? this.FolderTitle.Trim() : folderName.Trim();
            if (this.CanAddFolder(folderName))
            {
                FolderAsset folderAsset = new FolderAsset
                                              {
                                                  // Unique value for all the folders.
                                                  Title = folderName,
                                                  ParentFolder = this.currentFolderAsset
                                              };
                if (this.currentFolderAsset != null)
                {
                    this.currentFolderAsset.Assets.Add(folderAsset);
                }
                else
                {
                    this.currentAssets.Add(folderAsset);
                }
            
                this.FilterAssets();
                this.AddFolderCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Check if the folder with the given name can be added in the current folder.
        /// </summary>
        /// <param name="folderName">The name of the folder.</param>
        /// <returns>Returns true if the folder with the given name can be added else false.</returns>
        private bool CanAddFolder(string folderName)
        {
            // When this method is called by the command then this value would be null as we are 
            // not passing any parameter from the xaml file.
            // We are checking null value so that we can reuse the same function with given folderName.
            folderName = string.IsNullOrEmpty(folderName) ? this.FolderTitle.Trim() : folderName.Trim();

            if (string.IsNullOrEmpty(folderName))
            {
                return false;
            }

            // Check if the folder with the given name and with the same parentfolderid exists.                
            if (this.currentFolderAsset != null)
            {
                return this.currentFolderAsset.Assets.Count(x => x.Title == folderName && x is FolderAsset) == 0;
            }
            else
            {
                return this.currentAssets.Count(x => x.Title == folderName && x is FolderAsset) == 0;
            }
        }

        /// <summary>
        /// Shows the assets of the parent folders.
        /// </summary>
        /// <param name="folderName">Name of the folder.</param>
        private void ShowParentFolders(string folderName)
        {
            if (this.currentFolderAsset != null)
            {
                this.currentFolderAsset = this.GetParentFolderAsset;
                this.UpArrowCommand.RaiseCanExecuteChanged();
                this.FilterAssets();
            }
        }

        /// <summary>
        /// Determines whether parent folder exists for the current <see cref="FolderAsset"/>.
        /// </summary>
        /// <param name="folderName">Name of the folder.</param>
        /// <returns>
        /// <c>true</c> if this instance [can show parent folder] ; otherwise, <c>false</c>.
        /// </returns>
        private bool CanShowParentFolder(string folderName)
        {
            return this.currentFolderAsset != null;
        }

        /// <summary>
        /// Shows the assets of the given folder asset.
        /// </summary>
        /// <param name="asset">The folder asset.</param>
        private void ShowChildFolders(Asset asset)
        {
            FolderAsset folderAsset = asset as FolderAsset;
            if (folderAsset != null)
            {
                // Set the current folder.
                this.currentFolderAsset = folderAsset;
                
                this.FilterAssets();

                // TODO: Add a loaded property to the folder to know if is loaded.
                if (this.currentFolderAsset.ProviderUri != null && this.currentFolderAsset.Assets.Count == 0)
                {
                    this.View.ShowProgressBar();
                    this.assetsDataServiceFacade.LoadAssetsByLibraryIdAsync(folderAsset.ProviderUri, this.configurationService.GetMaxNumberOfItems());
                }
                else
                {
                    this.UpArrowCommand.RaiseCanExecuteChanged();
                    this.AddFolderCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private void OnLoadAssetsByLibraryIdCompleted(object sender, RCE.Infrastructure.DataEventArgs<List<Asset>> e)
        {
            if (e.Error == null && this.currentFolderAsset != null && this.currentFolderAsset.Assets.Count == 0 &&
                e.Data != null)
            {
                this.currentFolderAsset.AddAssets(e.Data);
                this.FilterAssets();
            }
        }

        /// <summary>
        /// Drops an asset to a folder.
        /// </summary>
        /// <param name="dropPayload">The drop payload that contains the drop information.</param>
        private void DropAssetOnFolder(DropPayload dropPayload)
        {
            FolderAsset folderAsset = dropPayload.DropData as FolderAsset;
            Asset asset = dropPayload.DraggedItem as Asset;

            if (folderAsset != null)
            {
                this.AddAssetToFolder(asset, folderAsset.Id);
                this.RefreshCurrentAssets();
            }
            else if (this.currentFolderAsset != null)
            {
                this.AddAssetToFolder(asset, this.currentFolderAsset.Id);
                this.RefreshCurrentAssets();
            }
            else
            {
                if (asset != null && !this.Assets.Contains(asset))
                {
                    this.AddAsset(asset);
                }
            }
        }

        /// <summary>
        /// Activates this MediaBin view.
        /// </summary>
        private void Activate()
        {
            this.regionManager.Regions[RegionNames.MainRegion].Activate(this.View);
        }

        /// <summary>
        /// Executes the keyboard action.
        /// </summary>
        /// <param name="tuple">The tuple .</param>
        private void ExecuteKeyboardAction(Tuple<KeyboardAction, object> tuple)
        {
            switch (tuple.Item1)
            {
                case KeyboardAction.ActivateModel:
                    this.Activate();
                    break;

                case KeyboardAction.DeleteAsset:
                    this.DeleteAsset(null);
                    break;

                case KeyboardAction.CutAsset:
                    if (this.SelectedAsset != null && !(this.SelectedAsset is FolderAsset))
                    {
                        this.clipBoardAsset = this.SelectedAsset;
                    }

                    break;
                case KeyboardAction.PasteAsset:
                    this.PasteAsset();
                    break;

                case KeyboardAction.Search:
                    if (tuple.Item2 != null)
                    {
                        this.Search(tuple.Item2.ToString());
                    }

                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Adds the asset to folder.
        /// </summary>
        /// <param name="asset">The asset.</param>
        /// <param name="folderId">The folder id.</param>
        private void AddAssetToFolder(Asset asset, Guid folderId)
        {
            FolderAsset folderAsset = FindFolderById(this.currentAssets, folderId);

            if (folderAsset != null)
            {
                this.RemoveAssetFromOriginalSource(asset);
                folderAsset.Assets.Add(asset);
                this.FilterAssets();
            }
        }

        private bool AddAssetFilter(Asset asset)
        {
            return !AssetExists(this.currentAssets, asset);
        }

        /// <summary>
        /// It copies the given asset to the current folder.
        /// </summary>
        /// <param name="asset">Asset to be copied to the current selected folder.</param>
        private void CopyAssetFromClipboardToCurrentFolder(Asset asset)
        {
            if (this.currentFolderAsset != null)
            {
                if (!this.currentFolderAsset.Assets.Contains(asset))
                {
                    this.RemoveAssetFromOriginalSource(asset);
                    this.currentFolderAsset.Assets.Add(asset);
                }
            }
            else
            {
                this.RemoveAssetFromOriginalSource(asset);
                this.currentAssets.Add(asset);
            }

            this.FilterAssets();
        }

        /// <summary>
        /// Refresh the assets of the current folder.
        /// </summary>
        private void RefreshCurrentAssets()
        {
            this.FilterAssets();
        }

        private void PasteAsset()
        {
            if (this.clipBoardAsset != null)
            {
                if (this.SelectedAsset != null && this.SelectedAsset is FolderAsset)
                {
                    this.AddAssetToFolder(this.clipBoardAsset, this.SelectedAsset.Id);
                    this.RefreshCurrentAssets();
                }
                else
                {
                    this.CopyAssetFromClipboardToCurrentFolder(this.clipBoardAsset);
                }

                this.clipBoardAsset = null;
            }
        }
    }
}