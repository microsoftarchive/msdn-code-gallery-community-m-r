namespace MyCompany.Expenses.Client.WP.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Navigation;
    using Microsoft.Phone.Controls;
    using Microsoft.Phone.Shell;
    using MyCompany.Expenses.Client.WP.ViewModel;
    using System.Collections.ObjectModel;
    using Microsoft.Phone.Maps.Toolkit;
    using Microsoft.Phone.Maps.Services;
    using Microsoft.Phone.Maps.Controls;
    using System.Threading.Tasks;
    using MyCompany.Expenses.Client.WP.Resources;

    /// <summary>
    /// TrackRoute page
    /// </summary>
    public partial class TrackRoute : PhoneApplicationPage
    {
        VMTrackRoute vm;
        /// <summary>
        /// Default constructor
        /// </summary>
        public TrackRoute()
        {
            InitializeComponent();

            map.Loaded += map_Loaded;
        }

        private async void map_Loaded(object sender, RoutedEventArgs e)
        {
            await this.vm.GetInitialPosition();
        }

        /// <summary>
        /// Code executed when navigating to a new page.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            vm = (VMTrackRoute)this.DataContext;

            if (e.NavigationMode == NavigationMode.New)
            {
                vm.InitializeData();
            }

            vm.PropertyChanged += vm_PropertyChanged;

            //This code is aimed to fix a problem when recovering from background.
            //The activated event is launched before navigation, so when this page subscribe to PropertyChanged again, the vm has notified once TrackPoints changes
            //So we need to launch the method our self to render correctly the routes.
            if (vm.TrackPoints != null)
                vm_PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("TrackPoints"));
        }

        /// <summary>
        /// Code execute when navigating from the page
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            vm.PropertyChanged -= vm_PropertyChanged;
        }

        private void vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "TrackPoints" && vm.TrackPoints.Count > 1)
            {
                RouteQuery query = new RouteQuery()
                {
                    Waypoints = vm.TrackPoints,
                    TravelMode = TravelMode.Driving,
                };

                query.QueryCompleted += query_QueryCompleted;
                query.QueryAsync();
            }
        }

        private void query_QueryCompleted(object sender, QueryCompletedEventArgs<Route> e)
        {
            (sender as RouteQuery).QueryCompleted -= query_QueryCompleted;
            map.SetView(e.Result.BoundingBox);
            map.AddRoute(new MapRoute(e.Result));
            vm.TrackLength = (int)(e.Result.LengthInMeters / 1609);
        }
    }
}