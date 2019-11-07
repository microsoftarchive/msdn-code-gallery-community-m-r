using System;

namespace Elm327.Core.ObdModes
{
    /// <summary>
    /// Contains methods and properties for retrieving generic
    /// OBD mode 01 PIDs.
    /// </summary>
    public class ObdGenericMode01 : AbstractObdMode
    {
        #region Structures/Enumerations

        /// <summary>
        /// Possible fuel types.
        /// </summary>
        public enum VehicleFuelType : byte
        {
            /// <summary>
            /// Unknown fuel type
            /// </summary>
            Unknown = 0x00,

            /// <summary>
            /// Gasoline
            /// </summary>
            Gasoline = 0x01,

            /// <summary>
            /// Methanol
            /// </summary>
            Methanol = 0x02,

            /// <summary>
            /// Ethanol
            /// </summary>
            Ethanol = 0x03,

            /// <summary>
            /// Diesel
            /// </summary>
            Diesel = 0x04,

            /// <summary>
            /// Liquefied Petroleum Gas (Propane)
            /// </summary>
            LPG = 0x05,

            /// <summary>
            /// Compressed Natural Gas
            /// </summary>
            CNG = 0x06,

            /// <summary>
            /// Propane
            /// </summary>
            Propane = 0x07,

            /// <summary>
            /// Electric Powered
            /// </summary>
            Electric = 0x08,

            /// <summary>
            /// Bi-fuel engine currently running on Gasoline.
            /// </summary>
            BifuelRunningGasoline = 0x09,

            /// <summary>
            ///  Bi-fuel engine currently running on Methanol.
            /// </summary>
            BifuelRunningMethanol = 0x0A,

            /// <summary>
            ///  Bi-fuel engine currently running on Ethanol.
            /// </summary>
            BifuelRunningEthanol = 0x0B,

            /// <summary>
            ///  Bi-fuel engine currently running on Liquid Petroleum Gas.
            /// </summary>
            BifuelRunningLPG = 0x0C,

            /// <summary>
            ///  Bi-fuel engine currently running on Compressed Natural Gas.
            /// </summary>
            BifuelRunningCNG = 0x0D,

            /// <summary>
            ///  Bi-fuel engine currently running on Propane.
            /// </summary>
            BifuelRunningProp = 0x0E,

            /// <summary>
            ///  Bi-fuel engine currently running on Electric power.
            /// </summary>
            BifuelRunningElectricity = 0x0F,

            /// <summary>
            ///  Bi-fuel engine currently running on a mixture of Gas and Electric power.
            /// </summary>
            BifuelMixedGasElectric = 0x10,

            /// <summary>
            /// Hybrid Gasoline.
            /// </summary>
            HybridGasoline = 0x11,

            /// <summary>
            /// Hybrid Ethanol
            /// </summary>
            HybridEthanol = 0x12,

            /// <summary>
            /// Hybrid Diesel
            /// </summary>
            HybridDiesel = 0x13,

            /// <summary>
            /// Hybrid Electric
            /// </summary>
            HybridElectric = 0x14,

            /// <summary>
            /// Hybrid Mixed Fuel
            /// </summary>
            HybridMixedFuel = 0x15,

            /// <summary>
            /// HybridRegenerative
            /// </summary>
            HybridRegenerative = 0x16
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates an instance of <see cref="ObdGenericMode01"/>.
        /// </summary>
        /// <param name="elm">A reference to the ELM327 driver.</param>
        internal ObdGenericMode01(ElmDriver elm)
            : base(elm, "01")
        {
        }

        #endregion

        #region Private Instance Properties

        /// <summary>
        /// Gets the current speed of the vehicle in km/h.
        /// </summary>
        private int VehicleSpeedInKilometersPerHour
        {
            get
            {
                string[] reading = this.GetPidResponse("0D");

                return
                    reading.Length > 0 ?
                    Util.ConvertHexToInt(reading[0]) :
                    0;
            }
        }

        #endregion

        #region Public Instance Properties

        /// <summary>
        /// Gets the ambient air temperature (in celsius or farenheit,
        /// depending on the current unit selection).
        /// </summary>
        public double AmbientAirTemperature
        {
            get
            {
                // The formula for this value is (A-40)

                string[] reading = this.GetPidResponse("46");

                if (reading.Length > 0)
                {
                    return
                        this.MeasuringUnitType == ElmDriver.ElmMeasuringUnitType.English ?
                        Util.ConvertCelsiusToFarenheit(Util.ConvertHexToInt(reading[0]) - 40) :
                        Util.ConvertHexToInt(reading[0]) - 40;
                }
                else
                    return 0;
            }
        }

        /// <summary>
        /// Gets the current engine coolant temperature (in celsius or farenheit,
        /// depending on the current unit selection).
        /// </summary>
        public double EngineCoolantTemperature
        {
            get
            {
                // The formula for this value is (A-40)

                string[] reading = this.GetPidResponse("05");

                if (reading.Length > 0)
                {
                    return
                        this.MeasuringUnitType == ElmDriver.ElmMeasuringUnitType.English ?
                        Util.ConvertCelsiusToFarenheit(Util.ConvertHexToInt(reading[0]) - 40) :
                        Util.ConvertHexToInt(reading[0]) - 40;
                }
                else
                    return 0;
            }
        }

        /// <summary>
        /// Gets the current engine RPM.
        /// </summary>
        public double EngineRpm
        {
            get
            {
                // This value is returned in two bytes.  Divide the reading by
                // 4 to get the correct RPM value.

                string[] reading = this.GetPidResponse("0C");

                return
                    reading.Length > 1 ?
                    Util.ConvertHexToInt(reading[0] + reading[1]) / 4 :
                    0;
            }
        }

        /// <summary>
        /// Gets the estimated distance per gallon (either miles per gallon
        /// or kilometers per gallon, depending on the current unit selection).
        /// Note that the vehicle must be equipped with a mass air flow sensor 
        /// in order for this value to be reported accurately. 
        /// </summary>
        public double EstimatedDistancePerGallon
        {
            get
            {
                // Check this link for discussions about this calculation:
                // http://www.mp3car.com/vbulletin/engine-management-obd-ii-engine-diagnostics-etc/75138-calculating-mpg-vss-maf-obd2.html
                // TODO: What I have below could be completely wrong.
                // TODO: This value is instantaneous.  Either we or the caller need 
                // to average the values over a period of time to smooth out the reading.

                // Km/Gallon = (41177.346 * Speed) / (3600 * MAF Rate)

                double mafRate = this.MassAirFlowRate;

                double kmPerGallon =
                    mafRate != 0 ?
                    (41177.346 * this.VehicleSpeedInKilometersPerHour) / (this.MassAirFlowRate * 3600) :
                    0;

                return
                    this.MeasuringUnitType == ElmDriver.ElmMeasuringUnitType.English ?
                    Util.ConvertKilometersToMiles(kmPerGallon) :
                    kmPerGallon;
            }
        }

        /// <summary>
        /// Gets the current fuel level as a percentage value between 0
        /// and 100.
        /// </summary>
        public double FuelLevel
        {
            get
            {
                // The formula for this value is (A*100)/255

                string[] reading = this.GetPidResponse("2F");

                return
                    reading.Length > 0 ?
                    (Util.ConvertHexToInt(reading[0]) * 100) / 255 :
                    0;
            }
        }

        /// <summary>
        /// Gets the fuel type for the vehicle.
        /// </summary>
        public VehicleFuelType FuelType
        {
            get
            {
                string[] reading = this.GetPidResponse("51");

                if (reading.Length < 1)
                    return VehicleFuelType.Unknown;

                try
                {
                    return (VehicleFuelType)Util.ConvertHexToInt(reading[0]);
                }
                catch (Exception ex)
                {
                    Util.Log(ex);
                    return VehicleFuelType.Unknown;
                }
            }
        }

        /// <summary>
        /// Gets the intake air temperature (in celsius or farenheit,
        /// depending on the current unit selection).
        /// </summary>
        public double IntakeAirTemperature
        {
            get
            {
                // The formula for this value is (A-40)

                string[] reading = this.GetPidResponse("0F");

                if (reading.Length > 0)
                {
                    return
                        this.MeasuringUnitType == ElmDriver.ElmMeasuringUnitType.English ?
                        Util.ConvertCelsiusToFarenheit(Util.ConvertHexToInt(reading[0]) - 40) :
                        Util.ConvertHexToInt(reading[0]) - 40;
                }
                else
                    return 0;
            }
        }

        /// <summary>
        /// Gets the current MAF rate in grams/sec.
        /// </summary>
        public double MassAirFlowRate
        {
            get
            {
                // This value is returned in two bytes.  Divide the reading by
                // 100 to get the correct MAF value.

                string[] reading = this.GetPidResponse("10");

                return
                    reading.Length > 1 ?
                    Util.ConvertHexToInt(reading[0] + reading[1]) / 100 :
                    0;
            }
        }

        /// <summary>
        /// Gets amount of time, in seconds, that the engine has been
        /// running since cranked.
        /// </summary>
        public int RunTimeSinceEngineStart
        {
            get
            {
                // This reading is returned in two bytes

                string[] reading = this.GetPidResponse("1F");

                return
                    reading.Length > 1 ?
                    Util.ConvertHexToInt(reading[0] + reading[1]) :
                    0;
            }
        }

        /// <summary>
        /// Gets the throttle position as a percentage value between 0
        /// and 100.
        /// </summary>
        public double ThrottlePosition
        {
            get
            {
                // The formula for this value is (A*100)/255

                string[] reading = this.GetPidResponse("11");

                return
                    reading.Length > 0 ?
                    (Util.ConvertHexToInt(reading[0]) * 100) / 255 :
                    0;
            }
        }

        /// <summary>
        /// Gets the current speed of the vehicle (either in mph or km/h,
        /// depending on the current unit selection).
        /// </summary>
        public double VehicleSpeed
        {
            get
            {
                return
                    this.MeasuringUnitType == ElmDriver.ElmMeasuringUnitType.English ?
                    Util.ConvertKilometersToMiles(this.VehicleSpeedInKilometersPerHour) :
                    this.VehicleSpeedInKilometersPerHour;
            }
        }

        #endregion
    }
}
