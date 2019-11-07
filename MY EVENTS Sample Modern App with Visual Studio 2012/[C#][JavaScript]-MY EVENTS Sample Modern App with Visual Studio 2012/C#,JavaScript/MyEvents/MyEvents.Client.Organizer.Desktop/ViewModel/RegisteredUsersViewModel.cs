using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyEvents.Api.Client;
using MyEvents.Client.Organizer.Desktop.Services.Navigation;

namespace MyEvents.Client.Organizer.Desktop.ViewModel
{
    /// <summary>
    /// Registered users viewmodel.
    /// </summary>
    public class RegisteredUsersViewModel : ViewModelBase
    {

        EventDefinition _currentEvent;

        Lazy<RelayCommand> _navigateBackCommand;
        INavigationService _navService;
        IMyEventsClient _eventsClient;


        /// <summary>
        /// Constructor or the class
        /// </summary>
        /// <param name="navService">INavigationService dependency</param>
        /// <param name="eventsClient">IMyEventsClient dependency</param>
        public RegisteredUsersViewModel(INavigationService navService, IMyEventsClient eventsClient)
        {  
            _navService = navService;
            _eventsClient = eventsClient;

            SuscribeCommands();
        }

        /// <summary>
        /// Current EventDefinition shown in the registered users view
        /// </summary>
        public EventDefinition CurrentEvent
        {
            get { return _currentEvent; }
            set
            {
                _currentEvent = value;
                GetRegisteredUsers();
                RaisePropertyChanged(() => CurrentEvent);
            }
        }

        /// <summary>
        /// List of RegisteredUsers
        /// </summary>
        public IList<RegisteredUser> RegisteredUsers
        {
            set;
            get;
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
            _navigateBackCommand = new Lazy<RelayCommand>(() => new RelayCommand(NavigateBackExecute));
        }

        private void NavigateBackExecute()
        {
            _navService.NavigateBack();
        }

        private void GetRegisteredUsers()
        {
            _eventsClient.RegisteredUserService.GetAllRegisteredUsersByEventIdAsync(CurrentEvent.EventDefinitionId, (registeredUsers) =>
                {
                    RegisteredUsers = registeredUsers;

                    App.Current.Dispatcher.Invoke(() =>
                    {
                        RaisePropertyChanged(() => RegisteredUsers);
                    });

                });
        }
    }
}
