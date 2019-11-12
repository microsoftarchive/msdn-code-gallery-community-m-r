namespace MyCompany.Expenses.Client.WP.Services.Notification
{
    using Coding4Fun.Toolkit.Controls;
    using Microsoft.Phone.Notification;
    using Microsoft.Phone.Shell;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// <see cref="MyCompany.Expenses.Client.WP.Services.Notification.INotificationService"/>
    /// </summary>
    public class NotificationService : INotificationService, IDisposable
    {
        private const string channelName = "WindowsPhoneToastChannel";
        private Uri channelUri;
        private Uri toastNavigationUri;
        private Client.IMyCompanyClient myCompanyClient;

        private HttpNotificationChannel notificationChannel;

        /// <summary>
        /// Creates a new instance of <see cref="NotificationService"/>
        /// </summary>
        /// <param name="myCompanyClient"></param>
        public NotificationService(Client.IMyCompanyClient myCompanyClient)
        {
            this.myCompanyClient = myCompanyClient;
        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Client.WP.Services.Notification.INotificationService"/>
        /// </summary>
        public void Subscribe()
        {
            // Try to find the push channel.
            notificationChannel = HttpNotificationChannel.Find(channelName);

            // If the channel was not found, then create a new connection to the push service.
            if (notificationChannel == null)
            {
                notificationChannel = new HttpNotificationChannel(channelName);
            }
            else
            {
                // Display the URI for testing purposes. Normally, the URI would be passed back to your web service at this point.
                ChannelUriUpdated();
            }

            // The channel was already open, so just register for all the events.
            notificationChannel.ChannelUriUpdated += new EventHandler<NotificationChannelUriEventArgs>(NotificationChannel_ChannelUriUpdated);
            notificationChannel.ErrorOccurred += new EventHandler<NotificationChannelErrorEventArgs>(NotificationChannel_ErrorOccurred);

            // Register for this notification only if you need to receive the notifications while your application is running.
            notificationChannel.ShellToastNotificationReceived += new EventHandler<NotificationEventArgs>(NotificationChannel_ShellToastNotificationReceived);

            if (notificationChannel.ConnectionStatus != ChannelConnectionStatus.Connected || notificationChannel.ChannelUri == null)
                notificationChannel.Open();

            if (!notificationChannel.IsShellToastBound)
                notificationChannel.BindToShellToast();
        }

        /// <summary>
        /// <see cref="MyCompany.Expenses.Client.WP.Services.Notification.INotificationService"/>
        /// </summary>
        public void Unsubscribe()
        {
            //TODO: Send to server
            channelUri = null;
        }

        /// <summary>
        /// Dispose managed resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose the class' resources
        /// </summary>
        /// <param name="disposing">Indicates it's called by the Dispose method or by the finalizer</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (notificationChannel != null)
                {
                    notificationChannel.Dispose();
                    notificationChannel = null;
                }
            }
        }

        private void NotificationChannel_ChannelUriUpdated(object sender, NotificationChannelUriEventArgs e)
        {
            ChannelUriUpdated();
        }

        private void ChannelUriUpdated()
        {
            channelUri = notificationChannel.ChannelUri;

            if (channelUri == null)
                return;

            myCompanyClient.NotificationsService.Add(new ClientNotificationChannel()
            {
                ChannelUri = channelUri.AbsoluteUri,
                NotificationType = NotificationType.WindowsPhoneNotification
            });
        }

        private void NotificationChannel_ErrorOccurred(object sender, NotificationChannelErrorEventArgs e)
        {
            // TODO: Do something
        }

        private void NotificationChannel_ShellToastNotificationReceived(object sender, NotificationEventArgs e)
        {
            var text1 = e.Collection["wp:Text1"];
            var text2 = e.Collection["wp:Text2"];
            var param = e.Collection["wp:Param"];

            toastNavigationUri = new Uri(param, UriKind.RelativeOrAbsolute);

            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                ToastPrompt toast = new ToastPrompt()
                {
                    Title = text2,
                    Message = text1,
                    MillisecondsUntilHidden = 5000,
                    ImageSource = new BitmapImage(new Uri("/Assets/ApplicationIcon.png", UriKind.RelativeOrAbsolute)) { DecodePixelHeight = 25, DecodePixelWidth = 25 }
                };

                toast.Tap += toast_Tap;
                toast.Show();
            });
        }

        void toast_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            App.RootFrame.Navigate(toastNavigationUri);
        }
    }
}
