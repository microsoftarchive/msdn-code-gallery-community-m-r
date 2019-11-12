using System;

using GTM = Gadgeteer.Modules;

// OBD Communication includes
using Elm327.Core;
using Elm327.Core.ObdModes;

namespace Gadgeteer.Modules.GHIElectronics
{
    // -- CHANGE FOR MICRO FRAMEWORK 4.2 --
    // If you want to use Serial, SPI, or DaisyLink (which includes GTI.SoftwareI2C), you must do a few more steps
    // since these have been moved to separate assemblies for NETMF 4.2 (to reduce the minimum memory footprint of Gadgeteer)
    // 1) add a reference to the assembly (named Gadgeteer.[interfacename])
    // 2) in GadgeteerHardware.xml, uncomment the lines under <Assemblies> so that end user apps using this module also add a reference.

    /// <summary>
    /// A OBD_II module for Microsoft .NET Gadgeteer.
    /// </summary>
    /// <example>
    /// <para>
    /// This driver currently only supports simple statistics, suce as RPM and vehicle speed.
    /// You are more than welcome to contribute and add more OBD-PIDs, found here: http://en.wikipedia.org/wiki/OBD-II_PIDs
    /// Important PIDs: any other performance statistic (oil temps, torque, horsepower, turbo stats etc) and fuel statistics (fuel pressure, injector timing, fuel flow rate etc)
    /// Also, support for reading/clearing MIL (check engine light) is needed.
    /// 
    /// The following example code demonstrates how to read the vehicle's fuel level and VIN.
    /// First, we connect the OBDII module using the Connect() function. The function takes two arguments for specific connection variables, but they are defaulted to automatically 
    /// connect, making it easier to use. Now, all we have to do is ask the module driver for the vehicle's information.
    /// If statistics other than what are provided are desired, these can be obtained using the elm class.
    /// </para>
    /// <code>
    /// using System;
    /// using System.Collections;
    /// using System.Threading;
    /// using Microsoft.SPOT;
    /// using Microsoft.SPOT.Presentation;
    /// using Microsoft.SPOT.Presentation.Controls;
    /// using Microsoft.SPOT.Presentation.Media;
    /// using Microsoft.SPOT.Touch;
    ///
    /// using Gadgeteer.Networking;
    /// using GT = Gadgeteer;
    /// using GTM = Gadgeteer.Modules;
    /// using Gadgeteer.Modules.GHIElectronics;
    ///
    /// namespace TestApp
    /// {
    ///     public partial class Program
    ///     {
    ///         void ProgramStarted()
    ///         {
    ///             obd_II.Connect();
    ///
    ///             double fuelLevel = obd_II.GetFuelLevel();
    ///
    ///             string VIN = obd_II.GetVIN();
    ///         }
    ///     }
    /// }
    ///</code>
    /// </example>
    public class OBD_II : GTM.Module
    {
        /// <summary>
        /// The underlying class that handles all of the communication with the ELM327 chip.
        /// </summary>
        public ElmDriver elm;

        private bool connected = false;

        /// <summary>
        /// Returns if the module has successfully connected to both the ELM327 and to the vehicle's ECU. 
        /// If a connection is successful, communication with the ECU is possible.
        /// </summary>
        public bool Connected
        {
            get { return connected; }
        }

        /// <summary>Constructor for this module. Initializes the ELM327 Core driver.</summary>
        /// <param name="socketNumber">The socket that this module is plugged in to.</param>
        public OBD_II(int socketNumber)
        {
            // This finds the Socket instance from the user-specified socket number.  
            // This will generate user-friendly error messages if the socket is invalid.
            // If there is more than one socket on this module, then instead of "null" for the last parameter, 
            // put text that identifies the socket to the user (e.g. "S" if there is a socket type S)
            Socket socket = Socket.GetSocket(socketNumber, true, this, null);
            char[] types = new char[] { 'U', 'K' };
            socket.EnsureTypeIsSupported(types, this);

            // Moved to Connect method
            //elm = new ElmDriver(socket.SerialPortName);
            serialPortName = new string(null);
            serialPortName += socket.SerialPortName;

            // Attempts to connect to the ECU. If successful, will retrieve one time information from the ECU.
            //Connect();
        }

        private string serialPortName;

        /// <summary>
        /// Attempts to connect to the ECU. If successful, will retrieve one time information from the ECU.
        /// </summary>
        public void Connect(ElmDriver.ElmObdProtocolType protocolType = ElmDriver.ElmObdProtocolType.Automatic, ElmDriver.ElmMeasuringUnitType measurementUnitType = ElmDriver.ElmMeasuringUnitType.English)
        {
            elm = new ElmDriver(serialPortName, protocolType, measurementUnitType);

            ElmDriver.ElmConnectionResultType connectionResult = elm.Connect();

            if (connectionResult == ElmDriver.ElmConnectionResultType.NoConnectionToElm)
            {
                throw new Exception("Failed to connect to the ELM327");
            }
            else if (connectionResult == ElmDriver.ElmConnectionResultType.NoConnectionToObd)
            {
                throw new Exception("Failed to connect to the vehicle's ECU");
            }
            else
            {
                connected = true;

                // Get the currently used OBD protocol type
                OBDProtocolType = GetFriendlyObdProtocolModeTypeName(elm.ProtocolType);
                VehicleFuelType = GetFriendlyFuelTypeName(elm.ObdMode01.FuelType);
                VIN = elm.ObdMode09.VehicleIdentificationNumber;
            }
        }

        #region Vehicle Statistics
        /// <summary>
        /// Gets the current speed of the vehicle (either in mph or km/h,
        /// depending on the current unit selection).
        /// </summary>
        public double GetVehicleSpeed()
        {
            return elm.ObdMode01.VehicleSpeed;
        }

        /// <summary>
        /// Returns the Revolutions Per Minute of the vehicle's engine.
        /// </summary>
        public double GetRPM()
        {
            return elm.ObdMode01.EngineRpm;
        }

        /// <summary>
        /// Gets the throttle position as a percent (0-100).
        /// </summary>
        public double GetThrottlePosition()
        {
            return elm.ObdMode01.ThrottlePosition;
        }

        /// <summary>
        /// Gets the current engine coolant temperature (in celsius or farenheit,
        /// depending on the current unit selection).
        /// </summary>
        public double GetEngineCoolantTemp()
        {
            return elm.ObdMode01.EngineCoolantTemperature;
        }

        /// <summary>
        /// Gets the intake air temperature (in celsius or farenheit,
        /// depending on the current unit selection).
        /// </summary>
        public double GetIntakeAirTemp()
        {
            return elm.ObdMode01.IntakeAirTemperature;
        }

        /// <summary>
        /// Gets the ambient air temperature (in celsius or farenheit,
        /// depending on the current unit selection).
        /// </summary>
        public double GetAmbientAirTemp()
        {
            return elm.ObdMode01.AmbientAirTemperature;
        }

        /// <summary>
        /// Gets the battery voltage reading.  Note that this value is read
        /// directly off the supply pin from the OBD port.
        /// </summary>
        public double GetBatteryVoltage()
        {
            return elm.BatteryVoltage;
        }

        /// <summary>
        /// Gets the current fuel level as a percentage value between 0
        /// and 100.
        /// </summary>
        public double GetFuelLevel()
        {
            return elm.ObdMode01.FuelLevel;
        }
        #endregion Vehicle Statistics

        #region Constant Stats
        // These stats will not change, therefore, only get them once.
        private static string OBDProtocolType = new string(null);
        private static string VehicleFuelType = new string(null);
        private static string VIN = new string(null);

        /// <summary>
        /// Gets the current OBD protocol.
        /// </summary>
        public string GetOBDProtocolType()
        {
            return OBDProtocolType;
            // set functionality was left out of this driver. Refer to the ELM.Core on how to set this.
        }

        /// <summary>
        /// Gets the fuel type for this vehicle.
        /// </summary>
        public string GetVehicleFuelType()
        {
            return VehicleFuelType;
        }

        /// <summary>
        /// Gets the Vehicle Identification Number.
        /// </summary>
        public string GetVIN()
        {
            return VIN;
        }
        #endregion Constant Stats

        #region Helper Functions
        /// <summary>
        /// Utility method to return a human-readable fuel type name.
        /// </summary>
        /// <param name="fuelType">The enumerated fuel type.</param>
        /// <returns>The friendly English name for the type.</returns>
        private string GetFriendlyFuelTypeName(ObdGenericMode01.VehicleFuelType fuelType)
        {
            switch (fuelType)
            {
                case Elm327.Core.ObdModes.ObdGenericMode01.VehicleFuelType.BifuelMixedGasElectric:
                case Elm327.Core.ObdModes.ObdGenericMode01.VehicleFuelType.BifuelRunningCNG:
                case Elm327.Core.ObdModes.ObdGenericMode01.VehicleFuelType.BifuelRunningElectricity:
                case Elm327.Core.ObdModes.ObdGenericMode01.VehicleFuelType.BifuelRunningEthanol:
                case Elm327.Core.ObdModes.ObdGenericMode01.VehicleFuelType.BifuelRunningGasoline:
                case Elm327.Core.ObdModes.ObdGenericMode01.VehicleFuelType.BifuelRunningLPG:
                case Elm327.Core.ObdModes.ObdGenericMode01.VehicleFuelType.BifuelRunningMethanol:
                case Elm327.Core.ObdModes.ObdGenericMode01.VehicleFuelType.BifuelRunningProp:
                    {
                        return "Bifuel";
                    }

                case Elm327.Core.ObdModes.ObdGenericMode01.VehicleFuelType.CNG:
                    {
                        return "Compressed Natural Gas";
                    }

                case Elm327.Core.ObdModes.ObdGenericMode01.VehicleFuelType.Diesel:
                    {
                        return "Diesel";
                    }

                case Elm327.Core.ObdModes.ObdGenericMode01.VehicleFuelType.Electric:
                    {
                        return "Electric";
                    }

                case Elm327.Core.ObdModes.ObdGenericMode01.VehicleFuelType.Ethanol:
                    {
                        return "Ethanol";
                    }

                case Elm327.Core.ObdModes.ObdGenericMode01.VehicleFuelType.Gasoline:
                    {
                        return "Gasoline";
                    }

                case Elm327.Core.ObdModes.ObdGenericMode01.VehicleFuelType.HybridDiesel:
                case Elm327.Core.ObdModes.ObdGenericMode01.VehicleFuelType.HybridElectric:
                case Elm327.Core.ObdModes.ObdGenericMode01.VehicleFuelType.HybridEthanol:
                case Elm327.Core.ObdModes.ObdGenericMode01.VehicleFuelType.HybridGasoline:
                case Elm327.Core.ObdModes.ObdGenericMode01.VehicleFuelType.HybridMixedFuel:
                case Elm327.Core.ObdModes.ObdGenericMode01.VehicleFuelType.HybridRegenerative:
                    {
                        return "Hybrid";
                    }

                case Elm327.Core.ObdModes.ObdGenericMode01.VehicleFuelType.LPG:
                case Elm327.Core.ObdModes.ObdGenericMode01.VehicleFuelType.Propane:
                    {
                        return "Propane";
                    }

                case Elm327.Core.ObdModes.ObdGenericMode01.VehicleFuelType.Methanol:
                    {
                        return "Methanol";
                    }

                default:
                    {
                        return "Unknown";
                    }
            }
        }

        /// <summary>
        /// Utility method to return a human-readable OBD protocol type name.
        /// </summary>
        /// <param name="protocolType">The enumerated OBD protocol type.</param>
        /// <returns>The friendly English name for the type.</returns>
        private string GetFriendlyObdProtocolModeTypeName(ElmDriver.ElmObdProtocolType protocolType)
        {
            switch (protocolType)
            {
                case Elm327.Core.ElmDriver.ElmObdProtocolType.Iso14230_4_Kwp:
                    {
                        return "ISO 14230-4 KWP";
                    }

                case Elm327.Core.ElmDriver.ElmObdProtocolType.Iso14230_4_KwpFastInit:
                    {
                        return "ISO 14230-4 KWP Fast Init";
                    }

                case Elm327.Core.ElmDriver.ElmObdProtocolType.Iso15765_4_Can11Bit:
                    {
                        return "CAN 11 Bit 250kb";
                    }

                case Elm327.Core.ElmDriver.ElmObdProtocolType.Iso15765_4_Can11BitFast:
                    {
                        return "CAN 11 Bit 500kb";
                    }

                case Elm327.Core.ElmDriver.ElmObdProtocolType.Iso15765_4_Can29Bit:
                    {
                        return "CAN 29 Bit 250kb";
                    }

                case Elm327.Core.ElmDriver.ElmObdProtocolType.Iso15765_4_Can29BitFast:
                    {
                        return "CAN 29 Bit 500kb";
                    }

                case Elm327.Core.ElmDriver.ElmObdProtocolType.Iso9141_2:
                    {
                        return "ISO 9141-2";
                    }

                case Elm327.Core.ElmDriver.ElmObdProtocolType.SaeJ1850Pwm:
                    {
                        return "SAE J1850 PWM";
                    }

                case Elm327.Core.ElmDriver.ElmObdProtocolType.SaeJ1850Vpw:
                    {
                        return "SAE J1850 VPW";
                    }

                case Elm327.Core.ElmDriver.ElmObdProtocolType.SaeJ1939Can:
                    {
                        return "SAE J1939 CAN";
                    }

                default:
                    {
                        return "Automatic";
                    }
            }
        }

        /// <summary>
        /// Utility method to return a human-readable connection type name.
        /// </summary>
        /// <param name="resultType">The enumerated ELM327 connection type.</param>
        /// <returns>The friendly English connection state.</returns>
        private string GetFriendlyElmConnectionResultTypeName(Elm327.Core.ElmDriver.ElmConnectionResultType resultType)
        {
            switch (resultType)
            {
                case Elm327.Core.ElmDriver.ElmConnectionResultType.NoConnectionToElm:
                    {
                        return "No ELM connection";
                    }

                case Elm327.Core.ElmDriver.ElmConnectionResultType.NoConnectionToObd:
                    {
                        return "No OBD connection";
                    }

                default:
                    {
                        return "Connected";
                    }
            }
        }
        #endregion
    }
}