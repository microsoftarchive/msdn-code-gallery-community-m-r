using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MultiWindowingUWPAppLikeMicrosoftEdge
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
     sealed partial class MainPage : Page, IPage
    {       
        public static int counter = 0;
        private ListViewItem currentItem;

        public int ViewId
        {
            get;
            set;
        }
        CoreDispatcher IPage.Dispatcher
        {
            get { return this.Dispatcher; }
        }

        public MainPage()
        {
            this.InitializeComponent();
            this.MainGrid.AllowDrop = true;
            this.MainGrid.Drop += MainGrid_Drop;
            this.Loaded += MainPage_Loaded;
            this.MainGrid.DragEnter += MainGrid_DragEnter;
            Window.Current.SizeChanged += Current_SizeChanged;
        }

        private async void CreateNewView(object model)
        {
            CoreApplicationView newView = CoreApplication.CreateNewView();
            int newViewId = 0;
            await newView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                var frame = new Frame();
                frame.Navigate(typeof(MainPage), model);
                Window.Current.Content = frame;
                Window.Current.Activate();
                newViewId = ApplicationView.GetForCurrentView().Id;
            });
            bool viewShown = await ApplicationViewSwitcher.TryShowAsStandaloneAsync(newViewId);

        }

        private void CreateNewTab_Click(object sender, RoutedEventArgs e)
        {
            counter++;
            ListViewItem tab = new ListViewItem();
            DataModel model = new DataModel();
            model.Name = $"NewTab {counter}";
            tab.DataContext = model;
            tab.Content = model.ToString();
            TabsListView.Items.Add(tab);
        }

        public void RemoveItem()
        {
            if (this.currentItem != null)
            {
                this.TabsListView.Items.Remove(currentItem);
                this.currentItem = null;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ((App)Application.Current).Views.Add(this);
            if (!CoreApplication.GetCurrentView().IsMain)
            {
                this.TabsListView.Items.Clear();
                ListViewItem tab = new ListViewItem();
                tab.DataContext = e.Parameter;
                tab.Content = tab.DataContext.ToString();
                currentItem = tab;
                this.TabsListView.Items.Add(tab);
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            ((App)Application.Current).Views.Remove(this);
        }

        #region CustopTitle

        private double defoultControlsWidth = 0.0;
        private CoreApplicationViewTitleBar mainCoreTitleBar;

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.mainCoreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            this.mainCoreTitleBar.ExtendViewIntoTitleBar = true;
            this.mainCoreTitleBar.LayoutMetricsChanged += TitleBar_LayoutMetricsChanged;
            this.ViewId = ApplicationView.GetForCurrentView().Id;
        }

        private void TitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
        {
            if (FlowDirection == FlowDirection.LeftToRight)
            {
                CustomTitleBar.Margin = new Thickness() { Left = mainCoreTitleBar.SystemOverlayLeftInset, Right = mainCoreTitleBar.SystemOverlayRightInset };
            }
            else
            {
                CustomTitleBar.Margin = new Thickness() { Left = mainCoreTitleBar.SystemOverlayRightInset, Right = mainCoreTitleBar.SystemOverlayLeftInset };
            }
            defoultControlsWidth = sender.SystemOverlayRightInset;
            GrapPanel.Height = sender.Height;
            CustomTitleBar.Height = sender.Height;
            TabsListView.MaxWidth = Window.Current.Bounds.Width - (defoultControlsWidth + CreateNewTab.ActualWidth);
            Window.Current.SetTitleBar(GrapPanel);
        }

        private void Current_SizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            double maxWidth = e.Size.Width - (defoultControlsWidth + CreateNewTab.ActualWidth);
            TabsListView.MaxWidth = maxWidth;
        }


        #endregion

        #region TitleListDrag&drop

        private async void TabsListView_DragItemsStarting(object sender, DragItemsStartingEventArgs e)
        {
            if (TabsListView.Items.Count == 1 && !CoreApplication.GetCurrentView().IsMain)
            {
                await ApplicationViewSwitcher.SwitchAsync(((App)Application.Current).Views[0].ViewId, this.ViewId, ApplicationViewSwitchingOptions.ConsolidateViews);
            }
            DataModel model;
            foreach (var item in e.Items)
            {
                if (!"Default Tab".Equals(item))
                {
                    currentItem = TabsListView.Items.Single((i) =>
                    {
                        var t = i as ListViewItem;
                        return t.Content == item;
                    }) as ListViewItem;

                    model = currentItem.DataContext as DataModel;

                    e.Data.SetData("Name", currentItem.Content.ToString());
                    e.Data.SetData("GUID", model.Id.ToString());
                    ((App)Application.Current).CurrentViewID = this.ViewId;
                }
                else
                {
                    e.Cancel = true;
                    return;
                }

            }
            ;
            e.Data.RequestedOperation = DataPackageOperation.Move;

        }

        private void TabsListView_DragEnter(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Move;
            e.DragUIOverride.IsCaptionVisible = false;
            e.DragUIOverride.IsGlyphVisible = false;
        }

        private async void TabsListView_Drop(object sender, DragEventArgs e)
        {
            var Deferral = e.GetDeferral();

            ListViewItem item = new ListViewItem();

            DataModel model = new DataModel();
            model.Name = (string)await e.DataView.GetDataAsync("Name");
            model.Id = new Guid((string)await e.DataView.GetDataAsync("GUID")); 

            item.DataContext = model;
            item.Content = model.Name;
            TabsListView.Items.Add(item);

            IPage p = ((App)Application.Current).Views.Single((x) =>
            {
                return x.ViewId== ((App)Application.Current).CurrentViewID;
            });

            await p.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => p.RemoveItem());

            Deferral.Complete();
        }


        #endregion

        #region MainGridDrag&drop
        
        private void MainGrid_DragEnter(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Move;
            e.DragUIOverride.IsCaptionVisible = false;
            e.DragUIOverride.IsGlyphVisible = false;
        }

        private async void MainGrid_Drop(object sender, DragEventArgs e)
        {
            if (e.AcceptedOperation == DataPackageOperation.Move)
            {
                var Deferral = e.GetDeferral();
                CoreWindow window = CoreWindow.GetForCurrentThread();
                DataModel model = new DataModel();
                model.Name = (string)await e.DataView.GetDataAsync("Name");
                model.Id = new Guid((string)await e.DataView.GetDataAsync("GUID"));

                await window.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,()=> CreateNewView(model));

                IPage p = ((App)Application.Current).Views.Single((x) =>
                {
                    return x.ViewId == ((App)Application.Current).CurrentViewID;
                });

                await p.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => p.RemoveItem());

                Deferral.Complete();
            }
        }

        #endregion


    }
}
