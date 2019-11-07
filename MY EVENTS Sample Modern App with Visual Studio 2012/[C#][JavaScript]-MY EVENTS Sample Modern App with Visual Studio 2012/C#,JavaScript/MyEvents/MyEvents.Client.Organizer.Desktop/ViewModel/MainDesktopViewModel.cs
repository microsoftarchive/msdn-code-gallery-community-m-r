using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight;
using MyEvents.Api.Client;
using MyEvents.Client.Organizer.Desktop.Credentials;
using MyEvents.Client.Organizer.Desktop.Model;
using MyEvents.Client.Organizer.Desktop.Services.Navigation;
using System.Configuration;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Threading;
using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace MyEvents.Client.Organizer.Desktop.ViewModel
{
    /// <summary>
    /// Main desktop viewmodel.
    /// </summary>
    public class MainDesktopViewModel : ViewModelBase
    {
        INavigationService _navService;
        IMyEventsClient _eventsClient;

        ObservableCollection<EventDefinition> _events = null;
        ObservableCollection<Tag> _topTags;
        EventDefinition _selectedEvent = null;

        Lazy<RelayCommand> _reloadTopTagsCommand;

        int _pendingTasks;

        /// <summary>
        /// Initialices a new instance o Main desktop viewmodel with navigation service param
        /// </summary>
        /// <param name="navigationService"></param>
        /// <param name="eventsClient"></param>
        public MainDesktopViewModel(INavigationService navigationService, IMyEventsClient eventsClient)
        {
            _navService = navigationService;
            TopTags = new ObservableCollection<Tag>();
            _eventsClient = eventsClient;
            TopSpeakers = new ObservableCollection<OrdererSpeaker>();
            SuscribeCommands();
            LoadData();
        }

        /// <summary>
        /// Property used to show a progressbar when the app is busy retrieving data
        /// </summary>
        public bool IsBusy
        {
            get { return _pendingTasks > 0; }
            set { }
        }

        /// <summary>
        /// Property that holds the top speakers
        /// </summary>
        public ObservableCollection<OrdererSpeaker> TopSpeakers { get; set; }

        /// <summary>
        /// List of events.
        /// </summary>
        public ObservableCollection<EventDefinition> Events
        {
            get { return _events; }
            set
            {
                _events = value;
                RaisePropertyChanged(() => Events);
            }
        }

        /// <summary>
        /// SelectedEvent, if not null, navigate to details.
        /// </summary>
        public EventDefinition SelectedEvent
        {
            get { return _selectedEvent; }
            set
            {
                _selectedEvent = value;
                if (value != null)
                    _navService.NavigateToEventDetails(_selectedEvent);
                RaisePropertyChanged(() => SelectedEvent);
            }
        }

        /// <summary>
        /// Stores the top tags of the event
        /// </summary>
        public ObservableCollection<Tag> TopTags
        {
            get { return _topTags; }
            set
            {
                _topTags = value;
                RaisePropertyChanged(() => TopTags);
            }
        }

        /// <summary>
        /// Command that navigates back from the edit session view
        /// </summary>
        public ICommand ReloadTopTagsCommand
        {
            get
            {
                return _reloadTopTagsCommand.Value;
            }
        }

        private void SuscribeCommands()
        {
            _reloadTopTagsCommand = new Lazy<RelayCommand>(() => new RelayCommand(ReloadTopTags));
        }

        private void LoadData()
        {
            ShowLoading();
            ReloadTopTags();
            _eventsClient.SetAccessToken(Credentials.UserCredentials.Current.MyEventsToken);
            GetEventDefinitions();
            GetTopSpeakers();
        }

        private void GetEventDefinitions()
        {
            _eventsClient.EventDefinitionService.GetEventDefinitionByOrganizerIdAsync(UserCredentials.Current.UserId, string.Empty, 50, 0, (result) =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    Events = new ObservableCollection<EventDefinition>(result);
                    HideLoading();
                });
            });
        }

        private void GetTopSpeakers()
        {
            _eventsClient.ReportService.GetTopSpeakersAsync(UserCredentials.Current.UserId, (result) =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    int position = 0;
                    var orderedSpeakers = result.OrderByDescending(x => x.Score).ToList();
                    foreach (var speaker in orderedSpeakers)
                    {
                        var orderedSpeaker = new OrdererSpeaker()
                        {
                            Name = speaker.Name,
                            Position = position,
                            Rating = speaker.Score
                        };

                        TopSpeakers.Add(orderedSpeaker);
                        position++;
                    }
                });
            });
        }

        private void ReloadTopTags()
        {
            GetTopTagsAsyncAwait();
        }

        private Task<List<Tag>> GetTopTagsAsync()
        {
            ShowLoading();
            return Task<List<Tag>>.Factory.StartNew((t) =>
            {
                string serviceAddress = ConfigurationManager.AppSettings.Get("ServicesAddress");
                string url = string.Format(CultureInfo.InvariantCulture, "{0}api/tags/{1}", serviceAddress, UserCredentials.Current.UserId);
                Thread.Sleep(3000);
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Headers["Authorization"] = Credentials.UserCredentials.Current.MyEventsToken;
                request.Method = "GET";
                var response = request.GetResponse();
                List<Tag> tags = SerializeResponse<List<Tag>>(response);
                return tags;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void GetTopTagsSync()
        {
            ShowLoading();
            string serviceAddress = ConfigurationManager.AppSettings.Get("ServicesAddress");
            string url = string.Format(CultureInfo.InvariantCulture, "{0}api/tags/{1}", serviceAddress, UserCredentials.Current.UserId);
            Thread.Sleep(3000);
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Headers["Authorization"] = Credentials.UserCredentials.Current.MyEventsToken;
            request.Method = "GET";
            var response = request.GetResponse();
            List<Tag> tags = SerializeResponse<List<Tag>>(response);
            this.TopTags = new ObservableCollection<Tag>(tags);
            HideLoading();
        }

        private void GetTopTagsTpl()
        {
            GetTopTagsAsync().ContinueWith(
                t =>
                {
                    this.TopTags = new ObservableCollection<Tag>(t.Result);
                    HideLoading();
                }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private async void GetTopTagsAsyncAwait()
        {
            List<Tag> tags = await GetTopTagsAsync();
            this.TopTags = new ObservableCollection<Tag>(tags);
            HideLoading();
        }

        private static T SerializeResponse<T>(WebResponse response)
        {
            Stream streamResponse = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(streamResponse);
            string responseString = streamReader.ReadToEnd();

            T deserializedObject = JsonConvert.DeserializeObject<T>(responseString);
            return deserializedObject;
        }

        private void ShowLoading()
        {
            _pendingTasks++;
            this.RaisePropertyChanged(() => this.IsBusy);
        }

        private void HideLoading()
        {
            _pendingTasks--;
            this.RaisePropertyChanged(() => this.IsBusy);
        }

    }
}
