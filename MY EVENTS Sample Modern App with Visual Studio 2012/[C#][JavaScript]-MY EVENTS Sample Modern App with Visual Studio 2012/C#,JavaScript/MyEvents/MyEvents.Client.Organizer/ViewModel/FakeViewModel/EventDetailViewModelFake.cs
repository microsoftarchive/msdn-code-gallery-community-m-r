using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyEvents.Api.Client;
using MyEvents.Client.Organizer.Credentials;
using MyEvents.Client.Organizer.Helper;
using MyEvents.Client.Organizer.Model;
using MyEvents.Client.Organizer.Services.Navigation;
using MyEvents.Client.Organizer.Services.Twitter;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.StartScreen;
using Windows.UI.Xaml;
using MyEvents.Client.Organizer.ViewModel.FakeViewModel;
using System.Collections.Generic;

namespace MyEvents.Client.Organizer.ViewModel
{
    /// <summary>
    /// Event details view model.
    /// </summary>
    public class EventDetailViewModelFake
    {        
        /// <summary>
        /// EventDefinition property.
        /// </summary>
        public EventDefinition Event 
        { 
            get
            {
                return FakeDataHelper.GetFakeEventWithSessions();
            }
        }

        /// <summary>
        /// Collection of hours for the duration of the event.
        /// </summary>
        public ObservableCollection<string> EventHours
        {
            get
            {
                return new ObservableCollection<string>(FakeDataHelper.GetEventHours());
            }
        }

        /// <summary>
        /// Collection of rooms for the event.
        /// </summary>
        public ObservableCollection<string> EventRooms
        {
            get
            {
                return new ObservableCollection<string>(new List<string>() { "room 1" });
            }
        }

        /// <summary>
        /// Session currently selected in the view.
        /// </summary>
        public Session SelectedSession
        {
            get 
            {
                return null;
            }            
        }

        /// <summary>
        /// Manages the visibility of the AppBar.
        /// </summary>
        public bool IsAppBarOpen 
        { 
            get
            {
                return false;
            }
        }


        public bool IsAppBarSticky
        {
            get
            {
                return false;
            }
        }


        /// <summary>
        /// Text shwon in twitter button.
        /// </summary>
        public string TwitterBtnContent 
        {
            get 
            {
                return "twitter login";
            }
        }
        /// <summary>
        /// Tweet message.
        /// </summary>
        public string TweetMessage
        {
            get
            {
                return "@speaker";
            }
        }

        /// <summary>
        /// Twitter timeline.
        /// </summary>
        public ObservableCollection<TwitterItem> Timeline
        {
            get
            {
                return new ObservableCollection<TwitterItem>(FakeDataHelper.GetTwitterItems());
            }
        }

        /// <summary>
        /// Shows or hides the progress bar for twitter.
        /// </summary>
        public bool ShowTwitterProgress
        {
            get
            {
                return false;
            }
        }       
    }
}
