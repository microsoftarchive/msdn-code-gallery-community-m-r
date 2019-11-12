namespace MyCompany.Visitors.Client.UniversalApp.Services.NFC
{
    using MyCompany.Visitors.Client.UniversalApp.Model;
    using Newtonsoft.Json;
    using System;
    using System.Threading.Tasks;
    using Windows.Data.Xml.Dom;
    using Windows.Networking.Proximity;
    using Windows.Networking.Sockets;
    using Windows.Storage.Streams;
    using Windows.UI.Notifications;

    /// <summary>
    /// NFC Contract implementation
    /// </summary>
    public class NfcService : INfcService
    {
        private const string WINDOWS8_APP_IDENTITY = "{b48d1c2e-8a39-4008-aeaf-120aeadc4817}";
        /// <summary>
        /// Prepare the app to receive NFC y Bluetooth data.
        /// </summary>
        public void WaitForDataAsync()
        {
            PeerFinder.AlternateIdentities["WindowsPhone"] = WINDOWS8_APP_IDENTITY;
            PeerFinder.TriggeredConnectionStateChanged += PeerFinder_TriggeredConnectionStateChanged;
            PeerFinder.ConnectionRequested += (d, e) => { };
            PeerFinder.AllowInfrastructure = true;
            PeerFinder.AllowBluetooth = true;
            PeerFinder.Start();

            string famName = Windows.ApplicationModel.Package.Current.Id.FamilyName;
        }

        private void PeerFinder_TriggeredConnectionStateChanged(object sender, TriggeredConnectionStateChangedEventArgs args)
        {
            switch (args.State)
            {
                case TriggeredConnectState.Completed:
                    ReceiveData(args.Socket).Wait();
                    break;
                case TriggeredConnectState.Canceled:
                    PeerFinder.Stop();
                    PeerFinder.Start();
                    break;
                case TriggeredConnectState.Failed:
                    PeerFinder.Stop();
                    PeerFinder.Start();
                    break;
                default:
                    break;
            }
        }

        private async Task ReceiveData(StreamSocket socket)
        {
            ToastNotification notif = new ToastNotification(ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02));
            XmlNodeList toastText = notif.Content.GetElementsByTagName("text");
            toastText[0].InnerText = "Device finded, receiving data.";
            ToastNotificationManager.CreateToastNotifier().Show(notif);

            DataReader reader = new DataReader(socket.InputStream);
            uint bytesRead = await reader.LoadAsync(sizeof(uint));
            if (bytesRead > 0)
            {
                uint strLength = (uint)reader.ReadInt32();
                bytesRead = await reader.LoadAsync(strLength);
                if (bytesRead > 0)
                {
                    string receivedData = reader.ReadString(strLength);
                    Visitor receivedVisitor = JsonConvert.DeserializeObject<Visitor>(receivedData);

                    if (receivedVisitor != null)
                    {
                        //Launch event to start logic.                    
                        await App.RootFrame.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                        {
                            var tmp = VisitorReceived;
                            if (tmp != null)
                                tmp(this, new VisitorEventArgs(receivedVisitor));
                        });
                    }
                }
            }
            PeerFinder.Stop();
            PeerFinder.Start();
        }

        /// <summary>
        /// When a new visitor is received by nfc this event is raised.
        /// </summary>
        public event EventHandler<VisitorEventArgs> VisitorReceived;
    }
}
