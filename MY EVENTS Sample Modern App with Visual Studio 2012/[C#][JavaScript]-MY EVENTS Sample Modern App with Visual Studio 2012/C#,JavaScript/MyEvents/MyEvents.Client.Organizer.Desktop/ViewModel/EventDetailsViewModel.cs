using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyEvents.Api.Client;
using MyEvents.Client.Organizer.Desktop.Services.Navigation;

namespace MyEvents.Client.Organizer.Desktop.ViewModel
{
    /// <summary>
    /// Event details viewmodel.
    /// </summary>
    public class EventDetailsViewModel : ViewModelBase
    {

        IMyEventsClient _eventsClient;
        INavigationService _navService;
        EventDefinition _eventDefinition;
        Lazy<RelayCommand> _showManageEventMap;
        Lazy<RelayCommand> _showEventSchedule;
        Lazy<RelayCommand> _navigateBackCommand;
        bool _isBusy = true;
        

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="navService"></param>
        /// <param name="eventsClient"></param>
        public EventDetailsViewModel(INavigationService navService, IMyEventsClient eventsClient)
        {
            _navService = navService;
            _eventsClient = eventsClient;
            SuscribeCommands();

            _eventsClient.SetAccessToken(Credentials.UserCredentials.Current.MyEventsToken);
        }

        /// <summary>
        /// Actual selected event.
        /// </summary>
        public EventDefinition Event
        {
            get
            {
                return _eventDefinition;
            }
            set
            {
                _eventDefinition = value;
                IsBusy = false;
                RaisePropertyChanged(() => Event);
            }
        }


        /// <summary>
        /// Property used to show a progressbar when the app is busy retrieving data
        /// </summary>
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

       
        /// <summary>
        /// Command to navigate to the manage event map
        /// </summary>
        public ICommand ShowManageEventmap
        {
            get { return _showManageEventMap.Value; }          
        }

        /// <summary>
        /// Command to navigate to manage schedule event
        /// </summary>
        public ICommand ShowManageScheduleEvent
        {
            get { return _showEventSchedule.Value; }  
        }

        /// <summary>
        /// Command to navigate to last page.
        /// </summary>
        public ICommand NavigateBackCommand
        {
            get { return _navigateBackCommand.Value; }
        }

        private void SuscribeCommands()
        {
            _showManageEventMap = new Lazy<RelayCommand>(() => new RelayCommand(ShowManageEventExecute));
            _showEventSchedule = new Lazy<RelayCommand>(() => new RelayCommand(ShowManageScheduleExecute));
            _navigateBackCommand = new Lazy<RelayCommand>(() => new RelayCommand(NavigateBackExecute));
        }

        private void ShowManageScheduleExecute()
        {
            _navService.NavigateToEventSchedule(Event);
        }

        private void ShowManageEventExecute()
        {
            _navService.NavigateToManageEventMap(Event);
        }

        private void NavigateBackExecute()
        {
            _navService.NavigateBack();
        }
    }
}
