namespace MyShuttle.Vehicle
{
    using Gadgeteer.Modules.GHIElectronics;
    using GHI.Networking;
    using Microsoft.SPOT;
    using MyShuttle.Vehicle.EventHub;
    using MyShuttle.Vehicle.Model;
    using System;
    using System.Collections;
    using System.Threading;
    using GTM = Gadgeteer.Modules;

    public partial class Program
    {
        bool _sendInfo = false;

        void ProgramStarted()
        {
            statusLED.TurnRed();

            StartRfidReader();
            ConnectToNetwork();
            StartAccelerometer();
            StartCompass();

            statusLED.TurnGreen();
            Debug.Print("Program Started");
        }

        private void StartCompass()
        {
            Debug.Print("Starting compass.");

            compass.MeasurementComplete += Compass_MeasurementComplete;
            compass.MeasurementInterval = new TimeSpan(0,0,0,0,1000);
            compass.StartTakingMeasurements();
        }

        void Compass_MeasurementComplete(Compass sender, Compass.MeasurementCompleteEventArgs e)
        {
            Debug.Print("Compass measurement complete: X = " + e.X + " Y = " + e.Y + " Z = " + e.Z);
            double headingDegrees = System.Math.Atan2((double)e.Y, (double)e.X) * (180.0 / System.Math.PI) + 180.0;
            Debug.Print("Heading degrees: " + headingDegrees);

            Thread t = new Thread(() => SendCompassData(headingDegrees));
            t.Start();
        }

        private void StartAccelerometer()
        {
            Debug.Print("Starting accident accelerometer.");

            Accelerometer.Calibrate();
            Accelerometer.MeasurementRange = Accelerometer.Range.TwoG;
            Accelerometer.EnableThresholdDetection(10.0, true, true, true, false, false, true);
            Accelerometer.ThresholdExceeded += AccidentAcelerometer_ThresholdExceeded;

            Accelerometer.MeasurementComplete += Accelerometer_MeasurementComplete;
            Accelerometer.MeasurementInterval = new TimeSpan(0, 0, 0, 0, 2000);
        }

        void Accelerometer_MeasurementComplete(Accelerometer sender, GTM.GHIElectronics.Accelerometer.MeasurementCompleteEventArgs e)
        {
            Debug.Print("Acelerometer measurement complete: X = " + e.X + " Y = " + e.Y + " Z = " + e.Z);

            Thread t = new Thread(() => SendAccelerometerData(e.X, e.Y, e.Z));
            t.Start();
        }


        void AccidentAcelerometer_ThresholdExceeded(Accelerometer sender, EventArgs e)
        {
            Debug.Print("Crash!");
            statusLED.TurnBlue();

            Thread t = new Thread(() => SendAccidentAlarm());
            t.Start();
        }

        void ConnectToNetwork()
        {
            Debug.Print("Connecting network.");

            int connectionAttempt = 0;
            const int MAX_CONNECTION_ATTEMPT = 10;

            wifiRS21.NetworkUp += wifiRS21_NetworkUp;
            wifiRS21.NetworkInterface.Open();


            wifiRS21.UseStaticIP(
               "192.168.1.20",
               "255.255.255.0",
               "192.168.1.1",
               new string[] { 
                   "8.8.8.8", 
                   "8.8.4.4" }
               );

            WiFiRS9110.NetworkParameters networkParameters = new WiFiRS9110.NetworkParameters();
            networkParameters.Ssid = "Plain Concepts";
            networkParameters.Key = "abra.cadabra.77";

            networkParameters.Channel = 11;
            networkParameters.NetworkType = WiFiRS9110.NetworkType.AccessPoint;
            networkParameters.SecurityMode = WiFiRS9110.SecurityMode.Wpa2;

            while (connectionAttempt < MAX_CONNECTION_ATTEMPT && !wifiRS21.IsNetworkConnected)
            {
                try 
                {
                    var networks = wifiRS21.NetworkInterface.Scan(networkParameters.Ssid);
                    wifiRS21.NetworkInterface.Join(networkParameters);
                }
                catch(Exception ex)
                {
                    //Keep trying to connect.
                    Debug.Print(ex.ToString());
                }

                Thread.Sleep(1000);
            }

            Debug.Print("Network is connected: " + wifiRS21.IsNetworkConnected);
        }

        void wifiRS21_NetworkUp(GTM.Module.NetworkModule sender, GTM.Module.NetworkModule.NetworkState state)
        {
            Debug.Print("Network up.");
            statusLED.TurnColor(Gadgeteer.Color.Orange);
        }

        private void StartRfidReader()
        {
            rfidReader.IdReceived += rfidReader_IdReceived;
        }

        void rfidReader_IdReceived(RFIDReader sender, string e)
        {
            Debug.Print("Rfid Event!");
            statusLED.BlinkOnce(Gadgeteer.Color.White);

            Thread t = new Thread(() => SendRfidEvent());
            t.Start();
        }

        void SendCompassData(double headingDegrees)
        {
            if (wifiRS21.IsNetworkConnected)
            {
                if (_sendInfo)
                {
                    statusLED.TurnColor(Gadgeteer.Color.Orange);

                    EventHubClient.SendEvent(new CompassEvent() 
                        { 
                            DeviceId = VehicleConfig.DeviceId,
                            HeadingDegrees = headingDegrees
                        });           
                }

                statusLED.TurnGreen();

            }
            else
                statusLED.TurnRed();
        }

        void SendAccelerometerData(double x, double y, double z)
        {
            if (wifiRS21.IsNetworkConnected)
            {
                if (_sendInfo)
                {
                    EventHubClient.SendEvent(new AccelerometerEvent()
                    {
                        DeviceId = VehicleConfig.DeviceId,
                        X = x, 
                        Y = y, 
                        Z = z
                    });
                }
            }
        }

        void SendRfidEvent()
        {
            if (wifiRS21.IsNetworkConnected)
            {
                statusLED.TurnColor(Gadgeteer.Color.Orange);

                EventHubClient.SendEvent(new RfidEvent() 
                { 
                    DeviceId = VehicleConfig.DeviceId 
                });               
                
                statusLED.TurnGreen();
                _sendInfo = true;
            }
            else
                statusLED.TurnRed();
        }

        void SendAccidentAlarm()
        {
            if (wifiRS21.IsNetworkConnected)
            {
                statusLED.TurnColor(Gadgeteer.Color.Orange);
                Hashtable table = new Hashtable();

                EventHubClient.SendEvent(new OBDEvent()
                {
                    DeviceId = VehicleConfig.DeviceId,
                    Code = VehicleConfig.OBDAccidentCode
                });   

                statusLED.TurnGreen();
                _sendInfo = false;
            }
            else
                statusLED.TurnRed();
        }

    }
}
