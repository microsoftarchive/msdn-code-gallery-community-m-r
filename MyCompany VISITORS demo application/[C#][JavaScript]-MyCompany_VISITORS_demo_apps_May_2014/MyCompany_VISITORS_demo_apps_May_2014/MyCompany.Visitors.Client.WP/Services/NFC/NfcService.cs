namespace MyCompany.Visitors.Client.WP.Services.NFC
{
    using MyCompany.Visitors.Client.WP.Model;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Xml.Serialization;
    using Windows.Networking.Proximity;
    using Windows.Storage.Streams;
    using NdefLibrary.Ndef;
    using System.Runtime.InteropServices.WindowsRuntime;
    using Newtonsoft.Json;

    /// <summary>
    /// NfcService implementation
    /// </summary>
    public class NfcService : INfcService, IDisposable
    {
        private Visitor pInformation;
        DataWriter writer;
        /// <summary>
        /// Start peer discovering
        /// </summary>
        public void StartPeerFinder(Visitor pInfo)
        {
            this.pInformation = pInfo;
            PeerFinder.AlternateIdentities.Add("Windows", "241465c5-9581-4a6c-aff1-7cd42ca293cd_833ns6bn3bbt0!App");
            PeerFinder.TriggeredConnectionStateChanged += PeerFinder_TriggeredConnectionStateChanged;
            PeerFinder.ConnectionRequested += (d, e) => { };
            PeerFinder.AllowInfrastructure = true;
            PeerFinder.AllowBluetooth = true;
            PeerFinder.Start();
        }

        /// <summary>
        /// Send personal information to windows 8 application.
        /// </summary>
        /// <param name="pInfo"></param>
        /// <returns></returns>
        public async void SendInfo(Visitor pInfo)
        {
            this.pInformation = pInfo;
            var content = JsonConvert.SerializeObject(pInformation);
            if (writer != null)
            {
                writer.WriteInt32(content.Length);
                writer.WriteString(content);
                await writer.StoreAsync();
            }
            PeerFinder.Stop();
            PeerFinder.Start();
        }

        void PeerFinder_TriggeredConnectionStateChanged(object sender, TriggeredConnectionStateChangedEventArgs args)
        {
            switch (args.State)
            {
                case TriggeredConnectState.Connecting:
                    break;
                case TriggeredConnectState.Completed:
                    writer = new DataWriter(args.Socket.OutputStream);
                    SendInfo(pInformation);
                    break;
                case TriggeredConnectState.Canceled:
                    PeerFinder.Stop();
                    PeerFinder.Start();
                    break;
                case TriggeredConnectState.Failed:
                    PeerFinder.Stop();
                    PeerFinder.Start();
                    break;
            }
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose overridable
        /// </summary>
        /// <param name="dispose"></param>
        protected virtual void Dispose(bool dispose)
        {
            if (dispose)
            {
                this.writer.Dispose();
            }
        }
    }
}
