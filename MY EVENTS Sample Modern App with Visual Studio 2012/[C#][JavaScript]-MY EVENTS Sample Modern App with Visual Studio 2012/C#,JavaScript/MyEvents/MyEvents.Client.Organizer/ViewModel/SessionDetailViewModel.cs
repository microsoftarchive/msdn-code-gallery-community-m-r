using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyEvents.Api.Client;
using MyEvents.Client.Organizer.Credentials;
using MyEvents.Client.Organizer.Helper;
using MyEvents.Client.Organizer.Services.Navigation;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.StartScreen;

namespace MyEvents.Client.Organizer.ViewModel
{
    /// <summary>
    /// Session details view model.
    /// </summary>
    public class SessionDetailViewModel : ViewModelBase
    {
        INavigationService _navService;
        IMyEventsClient _myEventsService;
        
        Lazy<RelayCommand> _navigateBackCommand;
        Lazy<RelayCommand> _deleteCommentCommand;
        Lazy<RelayCommand<Rect>> _pinToStartCommand;
        
        Session _session = new Session();
        ObservableCollection<SessionUserDetailsViewModel> _attendees;
        Comment _selectedComment;
        bool _getCommentCompleted = false;
        bool _getUsersCompleted = false;
        bool _isAppBarOpen;
        bool _isAppBarSticky;
        DataTransferManager _datatransferManager;

        /// <summary>
        /// Initializes a new instance of the SessionDetailViewModel class.
        /// </summary>
        public SessionDetailViewModel(INavigationService navSrv, IMyEventsClient myEventSrv)
        {
            _navService = navSrv;
            _myEventsService = myEventSrv;
            _myEventsService.SetAccessToken(UserCredentials.Current.CurrentUser.MyEventsToken);
            _navigateBackCommand = new Lazy<RelayCommand>(() => new RelayCommand(NavigateBackCommandExecute));
            _deleteCommentCommand = new Lazy<RelayCommand>(() => new RelayCommand(DeleteCommentCommandExecute));
            _pinToStartCommand = new Lazy<RelayCommand<Rect>>(() => new RelayCommand<Rect>(PinToStartCommandExecute));

        }

        /// <summary>
        /// Load Page
        /// </summary>
        public void Load()
        {
            try
            {
                _datatransferManager = DataTransferManager.GetForCurrentView();
                _datatransferManager.DataRequested += datatransferManager_DataRequested;
            }
            catch
            {
                // TODO: CHANGE THIS CATCH BEFORE FIXING THE BUG
            }
        }

        /// <summary>
        /// Unload Page
        /// </summary>
        public void UnLoad()
        {
            _datatransferManager.DataRequested -= datatransferManager_DataRequested;
        }

        void datatransferManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            args.Request.Data.Properties.Title = String.Format(CultureInfo.InvariantCulture, "{0}", Session.Title);
            args.Request.Data.Properties.Description = Session.Description;
            args.Request.Data.SetUri(new Uri(String.Format(CultureInfo.InvariantCulture, "{0}Event/SessionDetail?sessionId={1}", GlobalConfig.WebUrlPrefix, Session.SessionId)));
        } 

        /// <summary>
        /// Selected session
        /// </summary>
        public Session Session
        {
            get
            {
                return _session;
            }
            set
            {
                _session = value;
                RaisePropertyChanged(() => Session);
            }
        }

        /// <summary>
        /// Session registered attendees
        /// </summary>
        public ObservableCollection<SessionUserDetailsViewModel> Attendees
        {
            get
            {
                return _attendees;
            }
            set
            {
                _attendees = value;
                RaisePropertyChanged(() => Attendees);
            }
        }

        /// <summary>
        /// Selected comment
        /// </summary>
        public Comment SelectedComment 
        {
            get
            {
                return _selectedComment;
            }
            set
            {
                _selectedComment = value;
                if (_selectedComment == null)
                {
                    IsAppBarSticky = false;
                    IsAppBarOpen = false;
                }
                else
                {
                    IsAppBarSticky = true;
                    IsAppBarOpen = true;
                }
                
                RaisePropertyChanged(() => SelectedComment);
                RaisePropertyChanged(() => IsCommentSelected);
            }
        }

        /// <summary>
        /// Controls the visibility of the delete comment button
        /// </summary>
        public bool IsCommentSelected 
        {
            get 
            {
                return SelectedComment != null;
            }            
        }


        /// <summary>
        /// Actual comments count
        /// </summary>
        public string CommentCount 
        {
            get
            {
                if (_session.Comments == null)
                    return string.Empty;
                return _session.Comments.Count().ToString();                    
            }
        }

        /// <summary>
        /// Actual attendee count
        /// </summary>
        public string AttendeeCount
        {
            get
            {
                if (_attendees == null)
                    return string.Empty;
                return _attendees.Count().ToString();
            }
        }

        /// <summary>
        /// Submited voles
        /// </summary>
        public string SubmittedVotes
        {
            get 
            {
                if (_attendees == null)
                    return string.Empty;
                return _attendees.Where(a => a.Rated).Count().ToString();
            }            
        }


        /// <summary>
        /// Pending votes
        /// </summary>
        public string PendingVotes
        {
            get
            {
                if (_attendees == null)
                    return string.Empty;
                return _attendees.Where(a => a.Rated == false).Count().ToString();
            }
        }

        /// <summary>
        /// Indicate if we are loading data.
        /// </summary>
        public bool IsLoading 
        {
            get 
            {
                return !(_getCommentCompleted && _getUsersCompleted);
            }             
        }

        /// <summary>
        /// Indicate if we have app bar open
        /// </summary>
        public bool IsAppBarOpen 
        {
            get 
            {
                return _isAppBarOpen;
            }
            set 
            {
                _isAppBarOpen = value;
                RaisePropertyChanged(() => IsAppBarOpen);
            }
        }

        /// <summary>
        /// Indicate if we have app bar sticked.
        /// </summary>
        public bool IsAppBarSticky 
        {
            get
            {
                return _isAppBarSticky;
            }
            set
            {
                _isAppBarSticky = value;
                RaisePropertyChanged(() => IsAppBarSticky);
            }
        }

        /// <summary>
        /// Initialize data.
        /// </summary>
        /// <param name="session"></param>
        public void InitializeData(Session session)
        {
            _myEventsService.CommentService.GetAllCommentsAsync(session.SessionId, (commentsResult) =>
            {
                _getCommentCompleted = false;

                if (commentsResult != null)
                {
                    App.RootFrame.Dispatcher.RunAsync(CoreDispatcherPriority.High, new DispatchedHandler(() =>
                    {
                        _session = session;
                        _session.Comments = new ObservableCollection<Comment>(commentsResult);
                        
                        RaisePropertyChanged(() => Session);
                        RaisePropertyChanged(() => CommentCount);                        
                        _getCommentCompleted = true;
                        RaisePropertyChanged(() => IsLoading);

                    })).AsTask().Wait();
                }
            });

            
            _myEventsService.RegisteredUserService.GetAllRegisteredUsersBySessionIdAsync(session.SessionId, (attendeeResult) => 
            {
                _getUsersCompleted = false;

                if (attendeeResult != null)
                {
                    App.RootFrame.Dispatcher.RunAsync(CoreDispatcherPriority.High, new DispatchedHandler(() =>
                    {
                        var sessionUserDetails = attendeeResult.Select(q =>
                            new SessionUserDetailsViewModel
                            {
                                Name = q.Name,
                                Bio = q.Bio,
                                Photo = string.Format("https://graph.facebook.com/{0}/picture", q.FacebookId),
                                Score = q.SessionRegisteredUsers.First(s => s.SessionId == session.SessionId).Score,
                                Rated = q.SessionRegisteredUsers.First(s=> s.SessionId == session.SessionId).Rated
                            }).ToList();

                        _attendees = new ObservableCollection<SessionUserDetailsViewModel>(sessionUserDetails);


                        RaisePropertyChanged(() => Attendees);
                        RaisePropertyChanged(() => AttendeeCount);
                        RaisePropertyChanged(() => SubmittedVotes);
                        RaisePropertyChanged(() => PendingVotes);
                        

                        _getUsersCompleted = true;
                        RaisePropertyChanged(() => IsLoading);

                    })).AsTask().Wait();                    
                }                
            });
        }

        /// <summary>
        /// Expose command logic to navigate to previous visited page.
        /// </summary>
        public ICommand NavigateBackCommand
        {
            get
            {
                return _navigateBackCommand.Value;
            }
        }

        /// <summary>
        /// Delete command
        /// </summary>
        public ICommand DeleteCommentCommand 
        {
            get 
            {
                return _deleteCommentCommand.Value;
            }
        }

        /// <summary>
        /// Expose commnad logic to pin a session to the start menu
        /// </summary>
        public ICommand PinToStartCommand
        {
            get
            {
                return _pinToStartCommand.Value;
            }
        }

        /// <summary>
        /// Navigate to previous visited page.
        /// </summary>
        public void NavigateBackCommandExecute()
        {
            if (ApplicationData.Current.LocalSettings.Values["TileId"].ToString() == "App")
                _navService.NavigateBack();
            else 
            {
                _myEventsService.EventDefinitionService.GetEventDefinitionByIdAsync(_session.EventDefinitionId, (eventResult) => {

                    App.RootFrame.Dispatcher.RunAsync(CoreDispatcherPriority.High, new DispatchedHandler(() =>
                    {
                        _navService.NavigateToEventDetails(eventResult);
                            
                    })).AsTask().Wait();
                });                           
            }

            Session.Comments.Clear();
        }


        /// <summary>
        /// Delete the selected comment
        /// </summary>
        public void DeleteCommentCommandExecute()
        {

            _myEventsService.CommentService.DeleteCommentAsync(SelectedComment.CommentId, (deleteResult) => 
            {
                App.RootFrame.Dispatcher.RunAsync(CoreDispatcherPriority.High, new DispatchedHandler(() =>
                {
                    _session.Comments.Remove(SelectedComment);
                    SelectedComment = null;

                    RaisePropertyChanged(() => CommentCount);

                })).AsTask().Wait();             
              
            });        
        }


        /// <summary>
        /// Pin a tile for the event in the start menu.
        /// </summary>
        public async void PinToStartCommandExecute(Rect rect)
        {
            Uri logo = new Uri("ms-appx:///Assets/SessionTileSmall.png");
            Uri wideLogo = new Uri("ms-appx:///Assets/SessionTileWide.png");

            SecondaryTile tile = new SecondaryTile(TileHelper.SetSessionTileId(Session.SessionId),
                                                    Session.Title,
                                                    Session.Title,
                                                    TileHelper.SetSessionTileActivationArguments(Session.SessionId),
                                                    TileOptions.ShowNameOnLogo | TileOptions.ShowNameOnWideLogo,
                                                    logo,
                                                    wideLogo);

            bool isPinned = await tile.RequestCreateForSelectionAsync(rect, Placement.Above);
        }
    }
}
