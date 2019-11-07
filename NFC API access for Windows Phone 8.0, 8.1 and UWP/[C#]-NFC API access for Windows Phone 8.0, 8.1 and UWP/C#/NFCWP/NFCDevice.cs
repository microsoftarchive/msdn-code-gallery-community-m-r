using NdefLibrary.Ndef;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
//add the reference to to windows proximity hw...
using Windows.Networking.Proximity;

namespace NFCWP
{
    public class NFCDevice
    {
        private ProximityDevice _nfcDevice;
        public string Stats { get; set; }
        private long _messageSubscribeId;
       


        public NFCDevice()
        {
            // init the proximity sensor...
            _nfcDevice = ProximityDevice.GetDefault();
           

            if(_nfcDevice !=null)
            {
                _nfcDevice.DeviceArrived += NFCDevideArrived;
                _nfcDevice.DeviceDeparted += NFCDeviceDeparted;

                //subscribe....
                _messageSubscribeId = _nfcDevice.SubscribeForMessage("NDEF",ReadCard);
            }
            else
            {
                Stats = "no NFC module found...";
            }
        }

        // events to listen to proximity changes...
        private void NFCDevideArrived(ProximityDevice device)
        {
            Stats = "houray!!!!!";
        }
        private void NFCDeviceDeparted(ProximityDevice device)
        {
            Stats = "Nooooooooo";
        }

        private void ReadCard(ProximityDevice sender, ProximityMessage message)
        {
            var rawMsg = message.Data.ToArray();
            var ndefMessage = NdefMessage.FromByteArray(rawMsg);

            // Loop over all records contained in the NDEF message
            foreach (NdefRecord record in ndefMessage)
            {
               // here we loop through the record....
            }

        }


    }
}
