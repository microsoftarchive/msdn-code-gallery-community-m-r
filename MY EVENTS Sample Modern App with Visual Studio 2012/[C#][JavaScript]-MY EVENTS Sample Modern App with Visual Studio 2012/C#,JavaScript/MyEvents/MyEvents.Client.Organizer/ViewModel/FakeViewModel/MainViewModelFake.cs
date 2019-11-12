using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyEvents.Api.Client;
using MyEvents.Client.Organizer.Credentials;
using MyEvents.Client.Organizer.Model;
using MyEvents.Client.Organizer.Services.Navigation;
using Windows.ApplicationModel.Resources;
using Windows.UI.Core;
using Windows.Storage;
using System.Collections.Generic;
using Windows.UI.Xaml.Media.Imaging;
using System.IO;
using Windows.Storage.Streams;
using MyEvents.Client.Organizer.ViewModel.FakeViewModel;

namespace MyEvents.Client.Organizer.ViewModel
{
    /// <summary>
    /// Main page view model.
    /// </summary>
    public class MainViewModelFake
    {
        /// <summary>
        /// Controls the visibility of the progressbar.
        /// </summary>
        public bool ShowProgress
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Controls the visibility of the error message
        /// </summary>
        public bool NoEventsAvailable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Currently selected event
        /// </summary>
        public EventDefinition SelectedEvent
        {
            get
            {
                return FakeDataHelper.GetFakeEventWithSessions();
            }
        }

        /// <summary>
        /// Grouped event list by date.
        /// </summary>
        public ObservableCollection<EventGroup> Events
        {
            get
            {
                return new ObservableCollection<EventGroup>(
                    new List<EventGroup>() { 
                        new EventGroup()
                        {
                            GroupIndex = 1,
                            GroupTitle = "coming soon",
                            Items =  new ObservableCollection<EventDefinition>(FakeDataHelper.GetFakeEvents(4, 1))
                        },
                         new EventGroup()
                        {
                            GroupIndex = 2,
                            GroupTitle = "all events",
                            Items =  new ObservableCollection<EventDefinition>(FakeDataHelper.GetFakeEvents(6, 2))
                        }
                    });
            }
        }

        /// <summary>
        /// Plain, ungrouped list of events.
        /// </summary>
        public ObservableCollection<EventDefinition> UngroupedEvents
        {
            get
            {
                return new ObservableCollection<EventDefinition>(FakeDataHelper.GetFakeEvents(6, 2));
            }
        }     

    }
}