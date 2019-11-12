using System;
using GalaSoft.MvvmLight;
using MyEvents.Api.Client;
using MyEvents.Client.Organizer.Credentials;
using MyEvents.Client.Organizer.Helper;
using MyEvents.Client.Organizer.Services.Navigation;
using MyEvents.Client.Organizer.Services.UserInterface;
using Windows.Security.Authentication.Web;
using Windows.Storage;
using Windows.UI.Core;

namespace MyEvents.Client.Organizer.ViewModel
{
    public class SplashScreenViewModel : ViewModelBase
    {
        INavigationService _navService;
        IMyEventsClient _myEventsService;
        IUIService _uiService;

        bool showProgress = true;

        /// <summary>
        /// Initializes a new instance of the SpashScreenViewModel 
        /// </summary>
        /// <param name="navSrv">Navigation service</param>
        /// <param name="myEventsrSrv">myEvents service</param>
        /// <param name="uiSrv"></param>       
        public SplashScreenViewModel(INavigationService navSrv, IMyEventsClient myEventsrSrv, IUIService uiSrv)
        {
            _navService = navSrv;
            _myEventsService = myEventsrSrv;
            _uiService = uiSrv;
        }

        public bool ShowProgress
        {
            get
            {
                return showProgress;
            }
            set
            {
                showProgress = value;
                RaisePropertyChanged(() => ShowProgress);
            }
        }


        /// <summary>
        /// Do facebook login.
        /// </summary>
        public async void FacebookLogin()
        {
            string facebook = string.Format("https://www.facebook.com/dialog/oauth?client_id={0}", GlobalConfig.FacebookAppId);
            string callback = "https://www.facebook.com/connect/login_success.html";

            string Url = string.Format("{0}&redirect_uri={1}&display=popup&response_type=token", facebook, Uri.EscapeUriString(callback));

            try
            {
                var result = await WebAuthenticationBroker.AuthenticateAsync(
                                    WebAuthenticationOptions.None,
                                    new Uri(Url), new Uri(callback));

                if (result.ResponseStatus == WebAuthenticationStatus.Success)
                {
                    var response = result.ResponseData;
                    var searchFor = "access_token=";
                    var findEqual = response.IndexOf(searchFor, 0) + searchFor.Length;
                    var findAmper = response.IndexOf("&", findEqual);
                    var length = findAmper - findEqual;
                    var accessToken = response.Substring(findEqual, length);

                    CheckIfValidUser(accessToken);
                }
                else if (result.ResponseStatus == WebAuthenticationStatus.ErrorHttp)
                {
                    //Handle HTTP error. TODO: Show no internet mode??
                    ShowProgress = false;
                    _uiService.ShowMessage("LoginFail");
                }
                else
                {
                    ShowProgress = false;
                    _uiService.ShowMessage("LoginFail");
                }
            }
            catch (Exception)
            {
                // TODO: Show no internet mode??
            }
        }

        /// <summary>
        /// Check if the user has access to our application.
        /// </summary>
        /// <param name="accessToken"></param>
        public void CheckIfValidUser(string accessToken)
        {
            UserCredentials.Current.CurrentUser = new User();
            _myEventsService.AuthenticationService.LogOnAsync(accessToken, (result) =>
            {
                if (result != null)
                {
                    UserCredentials.Current.CurrentUser.UserId = result.RegisteredUserId;
                    UserCredentials.Current.CurrentUser.FacebookToken = accessToken;
                    UserCredentials.Current.CurrentUser.MyEventsToken = result.Token;
                    UserCredentials.Current.CurrentUser.FullName = result.UserName;
                    UserCredentials.Current.CurrentUser.FacebookId = result.FacebookUserId;

                    ApplicationData.Current.LocalSettings.Values["Facebook_AccessToken"] = accessToken;
                    ApplicationData.Current.LocalSettings.Values["Facebook_UserId"] = UserCredentials.Current.CurrentUser.FacebookId;

                    _myEventsService.SetAccessToken(UserCredentials.Current.CurrentUser.MyEventsToken);
                    App.RootFrame.Dispatcher.RunAsync(CoreDispatcherPriority.High, new DispatchedHandler(() =>
                    {
                        ShowProgress = true;
                        NavigateFromAppOrTile();
                    })).AsTask().Wait();
                }
                else
                {
                    App.RootFrame.Dispatcher.RunAsync(CoreDispatcherPriority.High, new DispatchedHandler(() =>
                    {
                        ApplicationData.Current.LocalSettings.Values["Facebook_AccessToken"] = "";
                        FacebookLogin();
                    })).AsTask().Wait();
                }
            });
        }

        public void LoadOfflineUserInformation()
        {
            UserCredentials.Current.CurrentUser = new User();
            _myEventsService.AuthenticationService.GetFakeAuthorizationAsync((result) =>
            {
                UserCredentials.Current.CurrentUser.UserId = result.RegisteredUserId;
                UserCredentials.Current.CurrentUser.FullName = result.UserName;

                UserCredentials.Current.CurrentUser.FacebookToken = "";
                UserCredentials.Current.CurrentUser.MyEventsToken = result.Token;

                _myEventsService.SetAccessToken(UserCredentials.Current.CurrentUser.MyEventsToken);
                App.RootFrame.Dispatcher.RunAsync(CoreDispatcherPriority.High, new DispatchedHandler(() =>
                {
                    ShowProgress = true;
                    NavigateFromAppOrTile();
                })).AsTask().Wait();
            });
        }

        private void NavigateFromAppOrTile()
        {
            string tileId = ApplicationData.Current.LocalSettings.Values["TileId"].ToString();

            if (tileId == "App")
            {
                _navService.NavigateToMainPage();
            }
            else if (TileHelper.GetTileType() == "Event")
            {
                int eventId = TileHelper.GetTileId();
                _myEventsService.EventDefinitionService.GetEventDefinitionByIdAsync(eventId, (resultEvent) =>
                {
                    App.RootFrame.Dispatcher.RunAsync(CoreDispatcherPriority.High, new DispatchedHandler(() =>
                    {
                        _navService.NavigateToEventDetails(resultEvent);

                    })).AsTask().Wait();
                });
            }
            else if (TileHelper.GetTileType() == "Session")
            {
                int sessionId = TileHelper.GetTileId();
                _myEventsService.SessionService.GetSessionAsync(sessionId, (resultSession) =>
                {
                    App.RootFrame.Dispatcher.RunAsync(CoreDispatcherPriority.High, new DispatchedHandler(() =>
                    {
                        _navService.NavigateToSessionDetails(resultSession);

                    })).AsTask().Wait();
                });
            }
        }
    }
}
