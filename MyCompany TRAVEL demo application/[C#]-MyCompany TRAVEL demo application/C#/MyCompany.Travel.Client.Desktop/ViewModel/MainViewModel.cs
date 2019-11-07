namespace MyCompany.Travel.Client.Desktop.ViewModel
{
    using GalaSoft.MvvmLight.Messaging;
    using Microsoft.AspNet.SignalR.Client;
    using Microsoft.AspNet.SignalR.Client.Hubs;
    using MyCompany.Travel.Client.Desktop.Helpers;
    using MyCompany.Travel.Client.Desktop.Model;
    using MyCompany.Travel.Client.Desktop.Resources.Strings;
    using MyCompany.Travel.Client.Desktop.Services.Navigation;
    using MyCompany.Travel.Client.Desktop.Services.SampleData;
    using MyCompany.Travel.Client.Desktop.Services.Security;
    using MyCompany.Travel.Client.Desktop.User;
    using MyCompany.Travel.Client.Desktop.ViewModel.Base;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using Windows.Data.Xml.Dom;
    using Windows.UI.Notifications;

    /// <summary>
    /// MainWindow ViewModel
    /// </summary>
    public class MainViewModel : VMBase
    {
        private const string APPIMAGE_PATH = "Resources/Images/icon.png";
        private readonly INavigationService navService;
        private readonly ISampleDataService sampleDataService;
        private readonly IMyCompanyClient myCompanyClient;
        private bool isVisibleDialog;
        private CustomDialogMessage dialogMessage;
        private TravelRequest travelRequestNotified;
        private ToastNotification toast;
        private HubConnection hubConnection;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(
            INavigationService navService,
            ISampleDataService sampleDataService,
            IMyCompanyClient myCompanyClient)
        {
            this.navService = navService;
            this.sampleDataService = sampleDataService;
            this.myCompanyClient = myCompanyClient;
            Messenger.Default.Register<LoadingMessage>(this, HandleLoadingMessage);
            Messenger.Default.Register<CustomDialogMessage>(this, HandleDialogMessage);
        }

        /// <summary>
        /// Initialize data
        /// </summary>
        /// <returns></returns>
        public async Task InitializeData()
        {
            Messenger.Default.Send(new LoadingMessage(true));

            try
            {
                Employee user = await myCompanyClient.EmployeeService.GetLoggedEmployeeInfo(PictureType.Small);
                if (user == null)
                {
                    ShowErrorMessage(StringResources.UserNotFound);
                }
                else if (user.IsRRHH)
                {
                    User.UserInfo.Current = user;
                    InitializeNotifications();

                    RaisePropertyChanged(() => Username);
                    RaisePropertyChanged(() => UserPhoto);
                    this.navService.NavigateToTravelRequestList();
                }
                else
                {
                    ShowErrorMessage(StringResources.NoPermissionsMessage);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                ShowErrorMessage(StringResources.NotInitializedError);
            }
        }

        private static void ShowErrorMessage(string textMessage)
        {
            CustomDialogMessage message = new CustomDialogMessage(async () => { await SecurityService.Logout(); }, textMessage, Visibility.Collapsed);
            Messenger.Default.Send<CustomDialogMessage>(message);
            Messenger.Default.Send(new LoadingMessage(false));
        }

        /// <summary>
        /// Exposes the name of the current user.
        /// </summary>
        public string Username
        {
            get
            {
                return UserInfo.Current.FirstName + " " + UserInfo.Current.LastName;
            }
        }

        /// <summary>
        /// Exposes the user photo in the system.
        /// </summary>
        public byte[] UserPhoto
        {
            get
            {
                if (UserInfo.Current.EmployeePictures == null)
                    return null;

                return UserInfo.Current.EmployeePictures.Select(ep => ep.Content).FirstOrDefault();
            }
        }

        /// <summary>
        /// Is visible dialog control.
        /// </summary>
        public bool IsVisibleDialog
        {
            get { return this.isVisibleDialog; }
            set
            {
                this.isVisibleDialog = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// CustomDialogMessage instance used as datacontext for the confirmation dialog.
        /// </summary>
        public CustomDialogMessage DialogMessage
        {
            get { return this.dialogMessage; }
            set
            {
                this.dialogMessage = value;
                RaisePropertyChanged();
            }
        }

        private void InitializeNotifications()
        {
            this.hubConnection = new HubConnection(SecurityService.OriginalUrl, new Dictionary<string, string>
                                   {
                                       { "isNoAuth", SecurityService.IsTestMode().ToString() }
                                   });
            this.hubConnection.Error += ErrorHandlerHub;

            if (!SecurityService.IsTestMode())
                this.hubConnection.Headers.Add("Authorization", SecurityService.AccessToken);

            IHubProxy hubProxy = this.hubConnection.CreateHubProxy("TravelsNotificationHub");
            hubProxy.On<TravelRequest>("notifyApproved", TravelRequestApproved);
            hubConnection.Start().ContinueWith(
                task =>
                {
                    if (task.IsCompleted)
                    {
                    }
                });
        }
        private void TravelRequestApproved(TravelRequest travelRequest)
        {
            if (travelRequest != null)
            {
                this.travelRequestNotified = travelRequest;

                // Create the sortcut
                ShortcutHelper.TryCreateShortcut("MyCompany.Travel");

                // Create the toast and attach event listeners
                XmlDocument toastXml = CreateToast();
                var node = toastXml.FirstChild.FirstChild.FirstChild;
                ((XmlElement)node).SetAttribute("addImageQuery", "true");
                this.toast = new ToastNotification(toastXml);
                this.toast.Activated += ToastActivated;
                ToastNotificationManager.CreateToastNotifier("MyCompany.Travel").Show(toast);
            }
        }
        private void ToastActivated(ToastNotification sender, object e)
        {
            App.Current.Dispatcher.Invoke((Action)(() =>
            {
                this.navService.NavigateToTravelRequest(this.travelRequestNotified);

                App.Current.MainWindow.Activate();
                App.Current.MainWindow.Topmost = true;
                App.Current.MainWindow.Topmost = false;
                App.Current.MainWindow.Focus();

                App.Current.MainWindow.WindowState = WindowState.Normal;
            }));
        }
        private XmlDocument CreateToast()
        {
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText01);

            var text = toastXml.GetElementsByTagName("text")[0];
            text.AppendChild(toastXml.CreateTextNode(StringResources.TravelApproved));

            string imagePath = Path.GetFullPath(APPIMAGE_PATH);
            var image = (XmlElement)toastXml.GetElementsByTagName("image")[0];
            image.Attributes.GetNamedItem("src").NodeValue = imagePath;

            return toastXml;
        }
        private void HandleLoadingMessage(LoadingMessage msg)
        {
            IsBusy = msg.IsLoading;
        }
        private void HandleDialogMessage(CustomDialogMessage msg)
        {
            IsVisibleDialog = true;
            msg.CancelAction = () =>
            {
                IsVisibleDialog = false;
            };

            DialogMessage = msg;
        }
        private void ErrorHandlerHub(Exception ex)
        {
            Console.WriteLine("SignalR error: {0}", ex.Message);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="dispose"></param>
        protected override void Dispose(bool dispose)
        {
            base.Dispose(dispose);
            Messenger.Default.Unregister<LoadingMessage>(this, HandleLoadingMessage);
            Messenger.Default.Unregister<CustomDialogMessage>(this, HandleDialogMessage);
            this.hubConnection.Error -= ErrorHandlerHub;
            if (this.toast != null)
            {
                this.toast.Activated -= ToastActivated;
            }
        }
    }
}