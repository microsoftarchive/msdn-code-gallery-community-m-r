namespace MyCompany.Expenses.Client.WP.ViewModel
{
    using GalaSoft.MvvmLight.Command;
    using Microsoft.Phone.Maps.Services;
    using MyCompany.Expenses.Client.WP.Model;
    using MyCompany.Expenses.Client.WP.Resources;
    using MyCompany.Expenses.Client.WP.Services.Location;
    using MyCompany.Expenses.Client.WP.Services.Navigation;
    using MyCompany.Expenses.Client.WP.ViewModel.Base;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Device.Location;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Threading;

    /// <summary>
    /// Track route viewmodel.
    /// </summary>
    public class VMTrackRoute : VMBase
    {
        private readonly INavigationService navService;
        private readonly ILocationService locationService;

        private RelayCommand startPositionTrackingCommand;
        private RelayCommand stopPositionTrackingCommand;
        private RelayCommand<CancelEventArgs> backKeyCommand;

        private bool isTracking;
        private GeoCoordinate currentPosition = new GeoCoordinate();
        private ObservableCollection<GeoCoordinate> trackPoints;
        private int trackLength;
        private DateTime? trackStart = null;
        private DispatcherTimer timer;
        private bool isInBackground;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="navService">Navigation service.</param>
        /// <param name="locationService">Location service.</param>
        public VMTrackRoute(INavigationService navService, ILocationService locationService)
        {
            this.navService = navService;
            this.locationService = locationService;

            InitializeCommands();

            //Controls when the app is going to background!!
            (App.Current as App).RunningInBackgroundChanged += VMTrackRoutePage_RunningInBackgroundChanged;
        }

        void VMTrackRoutePage_RunningInBackgroundChanged(object sender, EventArgs<bool> e)
        {
            this.isInBackground = e.EventData;
            if (this.isInBackground)
            {
                this.timer.Stop();
            }
            else
            {
                this.timer.Start();
                base.RaisePropertyChanged(() => CurrentPosition);
                base.RaisePropertyChanged(() => TrackPoints);
                base.RaisePropertyChanged(() => TrackDuration);
            }
        }

        /// <summary>
        /// Start tracking the position.
        /// </summary>
        public ICommand StartPositionTrackingCommand
        {
            get { return this.startPositionTrackingCommand; }
        }

        /// <summary>
        /// Stop tracking the position.
        /// </summary>
        public ICommand StopPositionTrackingCommand
        {
            get { return this.stopPositionTrackingCommand; }
        }

        /// <summary>
        /// Back key is pressed and need to warn user.
        /// </summary>
        public ICommand BackKeyCommand
        {
            get { return this.backKeyCommand; }
        }

        /// <summary>
        /// Flag to know if we are tracking or not.
        /// </summary>
        public bool IsTracking
        {
            get { return this.isTracking; }
            set
            {
                this.isTracking = value;
                base.RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Store the device current geo position
        /// </summary>
        public GeoCoordinate CurrentPosition
        {
            get { return this.currentPosition; }
            set { }
        }


        /// <summary>
        /// Return the list of trackPoints.
        /// </summary>
        public ObservableCollection<GeoCoordinate> TrackPoints
        {
            get { return this.trackPoints; }
        }

        /// <summary>
        /// Length in kilometers of the track.
        /// </summary>
        public int TrackLength
        {
            get { return this.trackLength; }
            set
            {
                this.trackLength = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Calculate the duration of the track from the start to current time.
        /// </summary>
        public string TrackDuration
        {
            get
            {
                if (!this.trackStart.HasValue)
                    return "00:00:00";

                var ellapsedTicks = DateTime.Now.Subtract(this.trackStart.Value);
                return string.Format("{0}:{1}:{2}", ellapsedTicks.Hours.ToString("00"), ellapsedTicks.Minutes.ToString("00"), ellapsedTicks.Seconds.ToString("00"));
            }
        }


        /// <summary>
        /// Initialize location to the current user position.
        /// </summary>
        /// <returns></returns>
        public void InitializeData()
        {
            this.trackLength = 0;
            this.trackStart = DateTime.Now;
            this.trackPoints = new ObservableCollection<GeoCoordinate>();
            RaisePropertyChanged(() => TrackPoints);
        }

        /// <summary>
        /// Get the initial position.
        /// </summary>
        /// <returns></returns>
        public async Task GetInitialPosition()
        {
            IsBusy = true;
            this.currentPosition = await this.locationService.GetCurrentPosition();
            RaisePropertyChanged(() => CurrentPosition);
            IsBusy = false;
        }

        private void InitializeCommands()
        {
            this.startPositionTrackingCommand = new RelayCommand(this.StartTrackingExecute, this.CanStartTracking);
            this.stopPositionTrackingCommand = new RelayCommand(this.StopTrackingExecute, this.CanStopTracking);
            this.backKeyCommand = new RelayCommand<CancelEventArgs>(this.BackKeyExecute);
        }

        private void StartTrackingExecute()
        {
            IsBusy = false;
            IsTracking = true;
            this.trackStart = DateTime.Now;

            this.timer = new DispatcherTimer();
            this.timer.Interval = new TimeSpan(0, 0, 1);
            this.timer.Tick += timer_Tick;
            this.timer.Start();

            this.trackPoints = new ObservableCollection<GeoCoordinate>();
            this.locationService.CurrentPositionChanged += locationService_CurrentPositionChanged;
            this.locationService.StartBackgroundPositionTracking();
            this.startPositionTrackingCommand.RaiseCanExecuteChanged();
            this.stopPositionTrackingCommand.RaiseCanExecuteChanged();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            RaisePropertyChanged(() => TrackDuration);
        }

        private void locationService_CurrentPositionChanged(object sender, EventArgs<GeoCoordinate> e)
        {
            App.Dispatcher.BeginInvoke(() =>
            {
                this.currentPosition = e.EventData;
                if (!this.isInBackground)
                    base.RaisePropertyChanged(() => CurrentPosition);
                this.trackPoints.Add(e.EventData);
                if (!this.isInBackground)
                    base.RaisePropertyChanged(() => TrackPoints);
            });
        }

        private bool CanStartTracking()
        {
            return !this.isTracking;
        }

        private async void StopTrackingExecute()
        {
            string from = await this.locationService.GetCityFromLocation(this.trackPoints.First().Latitude, this.trackPoints.First().Longitude);
            string to = await this.locationService.GetCityFromLocation(this.trackPoints.Last().Latitude, this.trackPoints.Last().Longitude);
            StopTracking();
            this.navService.NavigateBackToAddExpense(from, to, this.trackLength);
        }

        private bool CanStopTracking()
        {
            return this.isTracking;
        }

        private void StopTracking()
        {
            IsTracking = false;

            this.timer.Tick -= timer_Tick;
            this.timer.Stop();
            this.trackStart = null;
            this.locationService.StopBackgroundPositionTracking();
            this.startPositionTrackingCommand.RaiseCanExecuteChanged();
            this.stopPositionTrackingCommand.RaiseCanExecuteChanged();
        }

        private void BackKeyExecute(CancelEventArgs args)
        {
            var result = MessageBox.Show(AppResources.TrackRoutePageExitCancelsMessage, AppResources.TrackRoutePageTitle, MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.Cancel)
            {
                args.Cancel = true;
            }
            else
            {
                StopTracking();
            }
        }

        /// <summary>
        /// Dispose events and objects
        /// </summary>
        /// <param name="dispose"></param>
        protected override void Dispose(bool dispose)
        {
            this.locationService.CurrentPositionChanged -= locationService_CurrentPositionChanged;
        }
    }
}
