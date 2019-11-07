//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Media.Protection.PlayReady;
using PlayReadyUAP.Data;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PlayReadyUAP
{
    /// <summary>
    /// A page that displays a grouped collection of items.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //public string CustomRightsURL { get; set; }

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private ListView groupListView = null;

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        public MainPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;

            if (IsPhone)
            {
                Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            }
        }

        private bool IsPhone
        {
            get
            {
                return Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons");
            }
        }

        /// <summary>
        /// Handles back button press.  If app is at the root page of app, don't go back and the
        /// system will suspend the app.
        /// </summary>
        /// <param name="sender">The source of the BackPressed event.</param>
        /// <param name="e">Details for the BackPressed event.</param>
        private void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;
            if (frame == null)
            {
                return;
            }

            if (frame.CanGoBack)
            {
                frame.GoBack();
                e.Handled = true;
            }
        }


        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            var groups = await SampleDataSource.GetGroupsAsync();
            this.DefaultViewModel["Groups"] = groups;

            if (IsPhone)
            {
                // Hide the Hamburger buttons when it is on a phone.  Use the app bar on the bottom of the screen.
                SplitView splitView = (SplitView)FindName("mainSplitView");
                splitView.CompactPaneLength = 0;

                // Limit the width of the group list so the item list is partially visible for selection
                HubSection hybSection = (HubSection)FindName("groupList_HubSection");
                hybSection.Width = 250;
            }
            else
            {
                // Hide the app bar when it is not on a phone.  Use the Hamburger buttons on the left of the screen.
                CommandBar commandBar = (CommandBar)FindName("commandBar");
                commandBar.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Invoked when an item within a section is clicked.
        /// </summary>
        /// <param name="sender">The GridView or ListView
        /// displaying the item clicked.</param>
        /// <param name="e">Event data that describes the item clicked.</param>
        void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Navigate to the appropriate destination page, configuring the new page
            // by passing required information as a navigation parameter
            var itemId = ((SampleDataItem)e.ClickedItem).UniqueId;
            string[] itemIDs = new string[1];
            itemIDs[0] = itemId;
            this.Frame.Navigate(typeof(ItemPage), itemIDs);
        }

        /// <summary>
        /// Invoked when the selected index within a list is changed.
        /// </summary>
        /// <param name="sender">The ListView displaying the item changed.</param>
        /// <param name="e">Event data that describes the change.</param>
        void ListView_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            ListView listView = sender as ListView;
            if( listView != null && listView.SelectedIndex != -1 )
            {
                var groupSelected = (SampleDataGroup)listView.Items[listView.SelectedIndex];
                itemList_HubSection.Header = groupSelected.Title;
                itemList_HubSection.DataContext = groupSelected;
            }
        }
        /// <summary>
        /// Invoked when an item within a list is loaded.
        /// </summary>
        /// <param name="sender">The ListView displaying the item loaded.</param>
        /// <param name="e">Event data that describes the load event.</param>
        void ListView_Loaded(object sender, RoutedEventArgs e)
        {
            groupListView = sender as ListView;
        }

        /// <summary>
        /// Invoked when an item is added to the container.
        /// </summary>
        /// <param name="sender">The content container that changed.</param>
        /// <param name="e">Event data that describes the change event.</param>
        void ListView_ContainerContentChanging(object sender, ContainerContentChangingEventArgs e)
        {
            // Once the list view is loaded with at least one itme, select the first group as default
            if (groupListView != null && groupListView.Items.Count > 0 && groupListView.SelectedIndex == -1)
            {
                var groupSelected = groupListView.Items[0] as SampleDataGroup;

                if( groupSelected != null )
                {
                    itemList_HubSection.Header = groupSelected.Title;
                    itemList_HubSection.DataContext = groupSelected;
                }

                groupListView.SelectedIndex = 0;
            }
        }

        void Details_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the appropriate destination page, configuring the new page
            // by passing required information as a navigation parameter
            var button = sender as HyperlinkButton;
            if( button != null )
            {
                var parent = button.Parent as FrameworkElement;
                if( parent != null )
                {
                    var uniqueIdBox = parent.FindName("uniqueIdBox") as TextBox;
                    if( uniqueIdBox != null )
                    {
                        var item = SampleDataSource.GetItemAsync(uniqueIdBox.Text);
                        this.Frame.Navigate(typeof(DetailsPage), item.Result);
                    }
                }
            }
        }

        /// <summary>
        /// Invoked when the wrap grid is loaded.
        /// </summary>
        /// <param name="sender">The warp frip that is loaded.</param>
        /// <param name="e">Event data that describes the load event.</param>
        void WrapGrip_Loaded(object sender, RoutedEventArgs e)
        {
            // On desktop set the number of view items per row to 4
            WrapGrid wrapGrid = sender as WrapGrid;
            if (wrapGrid != null)
            {
                if (!Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
                {
                    wrapGrid.MaximumRowsOrColumns = 4;
                }
            }
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        private void CustomLicense_Tapped(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SettingPage));
        }

        private void Playlist_Tapped(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PlayListPage));
        }

        /// <summary>
        /// Perform proactive individualizatrion (indiv) if it hasn't been performed yet.
        /// An app should do indiv only when it is necessary.  To check whether the device indiv'd
        /// or not the app can get the PlayReady security version.  There will be an exception if it 
        /// hasn't indiv'd yet. 
        /// </summary>
        private void Indiv_Tapped(object sender, RoutedEventArgs e)
        {
            // If the individualization hasn't been performed getting the 
            // security version will cause exception.
            try
            {
                uint secVersion = PlayReadyStatics.PlayReadySecurityVersion;
                TextBlock indivResultElement = (TextBlock)FindName( IsPhone ? "abIndivResultOnAppBar" : "abIndivResult");
                if (indivResultElement != null)
                {
                    indivResultElement.Text = "Individualized already";
                }
            }
            catch (Exception)
            {
                // Start indiv
                Indiv indivSR = new IndivAndReportResult(new ReportResultDelegate(IndivCompleted));
                indivSR.IndivProactively();
            }
            finally
            {
                UpdateIndivState();
            };
        }

        private void IndivCompleted(bool bResult, object context)
        {
            TextBlock indivResultElement = (TextBlock)FindName( IsPhone ? "abIndivResultOnAppBar" : "abIndivResult");
            if (indivResultElement != null)
            {
                indivResultElement.Text = bResult ? "Individualization succeeded" : "Individualization failed";
            }

            UpdateIndivState();
        }

        private async void DeleteHDS_Tapped(object sender, RoutedEventArgs e)
        {
            bool succeeded = true;
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            IReadOnlyList<IStorageItem> itemsInFolder = await localFolder.GetItemsAsync();

            try
            {
                // Iterate over the results and print the list of items
                // to the Visual Studio Output window.
                foreach (IStorageItem item in itemsInFolder)
                {
                    if (item.Name.ToLower() == "playready")
                    {
                        await item.DeleteAsync();
                    }
                }
            }
            catch ( Exception )
            {
                succeeded = false;
            }
            finally
            {
                TextBlock deleteHDSElement = (TextBlock)FindName( IsPhone ? "abDeleteHDSResultOnAppBar" : "abDeleteHDSResult");
                if(deleteHDSElement != null)
                {
                    deleteHDSElement.Text = succeeded ? "Delete license store succeeded" : "Delete license store failed." + "\n" + "License store may be in use," + "\n" + "please close app and retry right after app launch.";
                }
            }
        }

        private void hgbMenu_Click(object sender, RoutedEventArgs e)
        {
            UpdateIndivState();
            mainSplitView.IsPaneOpen = !mainSplitView.IsPaneOpen;
        }

        private void UpdateIndivState()
        {
            string currentIndivState;

            try
            {
                // If there no exception that means individualized.
                uint securityVersion = PlayReadyStatics.PlayReadySecurityVersion;
                currentIndivState = "(individualized)";
            }
            catch (Exception)
            {
                currentIndivState = "(not individualized yet)";
            }

            tbIndivState.Text = "Indiv " + currentIndivState;
        }

        #endregion
    }
}

