using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Windows.Networking.Proximity;
using Windows.Storage.Streams;

namespace NfcEditor.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _Url;
        private bool _NfcDetected;
        private ProximityDevice _proximityDevice;
        private long _MessageType;

        public MainWindowViewModel()
        {
            _proximityDevice = ProximityDevice.GetDefault();
            if (_proximityDevice != null)
            {
                _proximityDevice.DeviceArrived += _proximityDevice_DeviceArrived;
                _proximityDevice.DeviceDeparted += _proximityDevice_DeviceDeparted;
                _MessageType = _proximityDevice.SubscribeForMessage("WindowsUri", MessageReceivedHandler);
            }
        }


        void _proximityDevice_DeviceDeparted(ProximityDevice sender)
        {
            NfcDetected = false;
            Url = "http://";
        }

        void _proximityDevice_DeviceArrived(ProximityDevice sender)
        {
            NfcDetected = true;
        }


        private void MessageReceivedHandler(ProximityDevice sender, ProximityMessage message)
        {
            try
            {
                using (var reader = DataReader.FromBuffer(message.Data))
                {
                    reader.UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding.Utf16LE;
                    string receivedString = reader.ReadString(reader.UnconsumedBufferLength / 2 - 1);
                    Debug.WriteLine("Received message from NFC: " + receivedString);
                    Url = receivedString;
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
            }

        }
        

        private void DoWriteTag()
        {
            try
            {
                using (var writer = new DataWriter{ UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding.Utf16LE } )
                {
                    Debug.WriteLine("Writing message to NFC: " + Url);
                    writer.WriteString(Url);
                    long id = _proximityDevice.PublishBinaryMessage("WindowsUri:WriteTag", writer.DetachBuffer());
                    _proximityDevice.StopPublishingMessage(id);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
            }

        }

        RelayCommand _writeCommand;
        public ICommand WriteCommand
        {
            get
            {
                if (_writeCommand == null)
                {
                    _writeCommand = new RelayCommand(p => this.DoWriteTag(), p => this.NfcDetected);
                }
                return _writeCommand;
            }
        }

        public string Url
        {
            get { return _Url; }
            set
            {
                if (value.Equals(_Url)) return;
                OnPropertyChanged();
                _Url = value;
            }
        }

        public bool NfcDetected
        {
            get { return _NfcDetected; }
            set
            {
                _NfcDetected = value;
                OnPropertyChanged("NfcDetected");
                OnPropertyChanged("NfcSearching");
            }
        }

        public bool NfcSearching
        {
            get { return !_NfcDetected; }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
