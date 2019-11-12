//  ------------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation
//  All rights reserved. 
//  
//  Licensed under the Apache License, Version 2.0 (the ""License""); you may not use this 
//  file except in compliance with the License. You may obtain a copy of the License at 
//  http://www.apache.org/licenses/LICENSE-2.0  
//  
//  THIS CODE IS PROVIDED *AS IS* BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, 
//  EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT LIMITATION ANY IMPLIED WARRANTIES OR 
//  CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR PURPOSE, MERCHANTABLITY OR 
//  NON-INFRINGEMENT. 
// 
//  See the Apache Version 2.0 License for specific language governing permissions and 
//  limitations under the License.
//  ------------------------------------------------------------------------------------

#define OBDSIMULATE
using System;
using System.Threading;

using Gadgeteer.Modules.GHIElectronics;

using AmqpSendReceive;

namespace Microsoft.Samples.ObdRecorder
{
    public partial class OBDIIReader
    {

        OBD_II obd_II;
        ILogger Logger = null;

        DateTime _lastObdConnectAttempt = DateTime.MinValue;
        bool _bObdInitializing = false;

        public OBDIIReader(ILogger logger, OBD_II obdModule)
        {
            Logger = logger;
            obd_II = obdModule;
        }

#if OBDSIMULATE
        string simulatedVIN = ("JTDKB20U" + Guid.NewGuid().ToString()).Substring(0,17);
#endif
        public bool InitializeObd()
        {
            bool bResult = false;
            if (obd_II != null)
            {
                if (!obd_II.Connected && !this._bObdInitializing && _lastObdConnectAttempt.AddMilliseconds(60000) < DateTime.UtcNow)
                {
                    _bObdInitializing = true;
                    Thread t = new Thread(InitializeObdInternal);
                    t.Start();
                }
            }
#if !OBDSIMULATE
            bResult = obd_II.Connected;
#else
            bResult = true;
#endif
            return bResult;
        }

        void InitializeObdInternal()
        {
            try
            {
                //obd_II.Connect(Elm327.Core.ElmDriver.ElmObdProtocolType.Automatic, Elm327.Core.ElmDriver.ElmMeasuringUnitType.Metric);
                Logger.TraceLog("Connecting to OBD2");
                obd_II.DebugPrintEnabled = true;

                _lastObdConnectAttempt = DateTime.UtcNow;

                obd_II.Connect(Elm327.Core.ElmDriver.ElmObdProtocolType.Automatic);

                Logger.TraceLog("Connected to OBD2. Protocol Type: " + obd_II.elm.ProtocolType.ToString());
                obd_II.elm.CanBusError += new Elm327.Core.ElmDriver.Elm327EventHandler(elm_CanBusError);
                obd_II.elm.ObdConnectionLost += new Elm327.Core.ElmDriver.Elm327EventHandler(elm_ObdConnectionLost);
            }
            catch (Exception ex3)
            {
                Logger.TraceLog("Error OBD: " + ex3.Message);
            }
            finally
            {
                _bObdInitializing = false;
            }
        }

        public class OBDIIData
        {
            public DateTime Time;
            public double VehicleSpeed;
            public double RPM;
            public double ThrottlePosition;
            public double AmbientAirTemp;
            public double IntakeAirTemp;
            public double FuelLevel;
            public string VehicleFuelType; 
            public string OBDProtocolType;
            public double BatteryVoltage;
#if MyMode01
            public double EngineFuelRate;
#endif
            public double DistancePerGallon;
            public string VIN;
        }

        public OBDIIData ReadObdIIData()
        {
            OBDIIData obdiiData;
#if OBDSIMULATE
            if (this.obd_II !=null && this.obd_II.Connected)
            {
#endif
                obdiiData = new OBDIIData();

                obdiiData.Time = DateTime.UtcNow;

                obdiiData.VehicleSpeed = obd_II.elm.ObdMode01.VehicleSpeed; // 01 0D
                //this.obd_II.GetVehicleSpeed();
                obdiiData.RPM = this.obd_II.GetRPM(); // 01 0C

                obdiiData.ThrottlePosition = this.obd_II.GetThrottlePosition();

                obdiiData.AmbientAirTemp = this.obd_II.GetAmbientAirTemp(); // 01 46
                obdiiData.IntakeAirTemp = this.obd_II.GetIntakeAirTemp();

                obdiiData.FuelLevel = this.obd_II.GetFuelLevel(); // 01 2F

                obdiiData.VehicleFuelType = this.obd_II.GetVehicleFuelType();
                obdiiData.OBDProtocolType = this.obd_II.GetOBDProtocolType();
                obdiiData.BatteryVoltage = this.obd_II.GetBatteryVoltage();

                // Engine fuel rate 01 5E 
#if MyMode01
                if (myMode01 == null)
                {
                    myMode01 = new MyObdMode01(this.obd_II.elm);
                }
                obdiiData.EngineFuelRate = myMode01.EngineFuelRate;
#endif
                obdiiData.DistancePerGallon = this.obd_II.elm.ObdMode01.EstimatedDistancePerGallon;

                obdiiData.VIN = this.obd_II.GetVIN();
#if OBDSIMULATE
            }
            else
            {
                this.InitializeObd();
                Random r = new Random();
                obdiiData = new OBDIIData
                {
                    VIN = simulatedVIN,
                    AmbientAirTemp = r.Next(20) + 10,
                    DistancePerGallon = r.Next(50),
                    FuelLevel = r.Next(100),
                    IntakeAirTemp = r.Next(40) + 20,
                    RPM = r.Next(6000),
                    ThrottlePosition = r.Next(100),
                    VehicleSpeed = r.Next(200),
                    BatteryVoltage = r.Next(2) + 10,
                    OBDProtocolType = "n/a",
                    VehicleFuelType = "n/a",
                    Time = DateTime.UtcNow,
                };
            }
#endif
            return obdiiData;
        }

        void elm_ObdConnectionLost()
        {
            Logger.TraceLog("elm_ObdConnectionLost");
        }

        void elm_CanBusError()
        {
            Logger.TraceLog("CanBusError");
        }


    }

#if myMode01
    class MyObdMode01 : ObdGenericMode01
    {
        public MyObdMode01(Elm327.Core.ElmDriver elm) : base(elm)
        {
        }

        public double EngineFuelRate
        {
            get
            {
                // The formula for this value is (A-40)

                string[] reading = this.GetPidResponse("5E");

                if (reading.Length > 0)
                {
                    return
                        Util.ConvertHexToInt(reading[0]);
                }
                else
                    return 0;
            }
        }

    }
#endif

}
