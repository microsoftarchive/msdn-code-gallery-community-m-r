using System;
using System.IO.Ports;
using System.Text;
using System.Threading;
using Elm327.Core.ObdModes;

namespace Elm327.Core
{
    /// <summary>
    /// A driver for the ELM327 chip (www.elmelectronics.com).  This class provides the
    /// basic communication infrastructure for the ELM.
    /// </summary>
    public class ElmDriver : IDisposable
    {
        #region Fields

        /// <summary>
        /// The baud rate used to communicate with the ELM.  TODO: We need
        /// to enable a high baud rate (500kbps, which is the max supported
        /// by the ELM chip), but right now it is locked to 38400. Pin 6 of 
        /// the ELM is expected to be tied to a high level to enable this baud 
        /// rate.
        /// </summary>
        private const int BAUD_RATE = 38400;

        /// <summary>
        /// The character that will be sent by the ELM when it is ready to
        /// accept commands.
        /// </summary>
        private const char CHIP_READY_PROMPT_CHAR = '>';

        /// <summary>
        /// The maximum amount of time, in seconds, to wait for the ELM to
        /// send us a message response.  A timeout error will occur if the chip
        /// doesn't respond within this amount of time.
        /// </summary>
        private const int MAX_WAIT_RECEIVE_SECONDS = 10;

        /// <summary>
        /// The message reported by the ELM when there's a problem communicating
        /// on the CAN bus.
        /// </summary>
        private const string MESSAGE_CAN_ERROR = "CAN ERROR";

        /// <summary>
        /// The message reported by the ELM when it is unable to connect via
        /// one of the OBD protocols.
        /// </summary>
        private const string MESSAGE_NO_CONNECTION = "UNABLE TO CONNECT";

        /// <summary>
        /// The message reported by the ELM when a bus request times out or
        /// an unsupported PID is requested.
        /// </summary>
        private const string MESSAGE_NO_DATA = "NO DATA";

        /// <summary>
        /// The message reported by the ELM when it is performing a search on
        /// one of the protocols.
        /// </summary>
        private static string MESSAGE_SEARCHING = "SEARCHING..." + ElmDriver.MESSAGE_TERMINATOR_CHAR;

        /// <summary>
        /// The character used to indicate the end of a message sent to or
        /// received from the ELM.
        /// </summary>
        internal const char MESSAGE_TERMINATOR_CHAR = '\r';

        /// <summary>
        /// Stores the currently selected OBD protocol.
        /// </summary>
        private ElmObdProtocolType protocolType;

        /// <summary>
        /// The size of the receive buffer for data received from the ELM.
        /// </summary>
        private const int RECEIVE_BUFFER_SIZE = 1024;

        /// <summary>
        /// The buffer used to hold data received from the ELM.
        /// </summary>
        private byte[] receiveBuffer;

        /// <summary>
        /// The serial port used to communicate with the ELM.  Note that
        /// we typically put a lock() statement around the use of this
        /// object to prevent concurrency issues.
        /// </summary>
        private SerialPort serialPort;

        #endregion

        #region Event Definitions

        /// <summary>
        /// Basic delegate used for ELM events.  This will probably change.
        /// </summary>
        public delegate void Elm327EventHandler();

        /// <summary>
        /// Fired when a CAN bus error has occurred.  This probably means an 
        /// incorrect CAN protocol was chosen or there is a wiring issue with
        /// the ELM327 circuit.
        /// </summary>
        public event Elm327EventHandler CanBusError;

        /// <summary>
        /// Fired when the ELM reports that it is unable to connect to
        /// the vehicle via one of the OBD protocols.  Note that although the event 
        /// is named <see cref="ObdConnectionLost"/>, it could also mean that there 
        /// was never an available connection to start with.  If this event is fired, 
        /// you should probably attempt to close and reopen the connection to the ELM.
        /// </summary>
        public event Elm327EventHandler ObdConnectionLost;

        #endregion

        #region Structures/Enumerations

        /// <summary>
        /// Possible results of calling the <see cref="Elm327.Core.ElmDriver.Connect"/>()
        /// method.
        /// </summary>
        public enum ElmConnectionResultType
        {
            /// <summary>
            /// A connection was successfully made to the ELM as well
            /// as the vehicle's OBD system.
            /// </summary>
            Connected,

            /// <summary>
            /// A connection couldn't be made to the ELM.  Check the wiring
            /// and ensure the COM port is set correctly.
            /// </summary>
            NoConnectionToElm,

            /// <summary>
            /// Communication was successful with the ELM, but the chip
            /// could not establish communication with the vehicle's
            /// OBD system.  This is most likely due to incorrect wiring
            /// or incorrect OBD protocol specification.
            /// </summary>
            NoConnectionToObd
        }

        /// <summary>
        /// Defines the available units of measure for reporting
        /// values.
        /// </summary>
        public enum ElmMeasuringUnitType
        {
            /// <summary>
            /// English units.
            /// </summary>
            English,

            /// <summary>
            /// Metric units.
            /// </summary>
            Metric
        }

        /// <summary>
        /// Represents all OBD protocols supported by the ELM chip.
        /// </summary>
        public enum ElmObdProtocolType
        {
            /// <summary>
            /// The ELM chip will automatically determine the best protocol to use.
            /// </summary>
            Automatic = '0',

            /// <summary>
            /// SAE J1850 PWM (41.6 kbaud).
            /// </summary>
            SaeJ1850Pwm = '1',

            /// <summary>
            /// SAE J1850 VPW (10.4 kbaud).
            /// </summary>
            SaeJ1850Vpw = '2',

            /// <summary>
            /// ISO 9141-2 (5 baud init, 10.4 kbaud).
            /// </summary>
            Iso9141_2 = '3',

            /// <summary>
            /// ISO 14230-4 KWP (5 baud init, 10.4 kbaud).
            /// </summary>
            Iso14230_4_Kwp = '4',

            /// <summary>
            /// ISO 14230-4 KWP (fast init, 10.4 kbaud).
            /// </summary>
            Iso14230_4_KwpFastInit = '5',

            /// <summary>
            /// ISO 15765-4 CAN (11 bit ID, 500 kbaud).
            /// </summary>
            Iso15765_4_Can11BitFast = '6',

            /// <summary>
            /// ISO 15765-4 CAN (29 bit ID, 500 kbaud).
            /// </summary>
            Iso15765_4_Can29BitFast = '7',

            /// <summary>
            /// ISO 15765-4 CAN (11 bit ID, 250 kbaud).
            /// </summary>
            Iso15765_4_Can11Bit = '8',

            /// <summary>
            /// ISO 15765-4 CAN (29 bit ID, 250 kbaud).
            /// </summary>
            Iso15765_4_Can29Bit = '9',

            /// <summary>
            /// SAE J1939 CAN (29 bit ID, 250 kbaud).
            /// </summary>
            SaeJ1939Can = 'A'
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates an instance of the ELM327 driver.
        /// </summary>
        /// <param name="serialPortName">The name of the serial port the ELM is connected to
        /// ("COM1", "COM2", etc.).</param>
        /// <param name="protocolType">The desired OBD protocol to use.  Using 
        /// <see cref="Elm327.Core.ElmDriver.ElmObdProtocolType.Automatic"/> is usually a good idea.</param>
        /// <param name="measuringUnit">The desired unit type for reporting readings.</param>
        public ElmDriver(
            string serialPortName,
            ElmObdProtocolType protocolType,
            ElmMeasuringUnitType measuringUnit)
        {
            this.MeasuringUnitType = measuringUnit;
            this.ElmVersionID = string.Empty;
            this.ObdMode01 = new ObdGenericMode01(this);
            this.ObdMode09 = new ObdGenericMode09(this);
            this.protocolType = protocolType;
            this.receiveBuffer = new byte[ElmDriver.RECEIVE_BUFFER_SIZE];

            this.serialPort = new SerialPort(
                serialPortName,
                ElmDriver.BAUD_RATE,
                Parity.None,
                8,
                StopBits.One);

            this.serialPort.ErrorReceived += new SerialErrorReceivedEventHandler(ElmDriver.uart_ErrorReceived);
        }

        /// <summary>
        /// Creates an instance of the ELM327 driver.
        /// </summary>
        /// <param name="serialPortName">The name of the serial port the ELM is connected to
        /// ("COM1", "COM2", etc.).</param>
        public ElmDriver(string serialPortName)
            : this(serialPortName, ElmObdProtocolType.Automatic, ElmMeasuringUnitType.English)
        {
        }

        #endregion

        #region Public Instance Properties

        /// <summary>
        /// Gets the battery voltage reading.  Note that this value is read
        /// directly off the supply pin from the OBD port.
        /// </summary>
        public double BatteryVoltage
        {
            get
            {
                try
                {
                    // The reading will come back as something like "12.7V", so get
                    // rid of the 'V' and Double.Parse() the rest

                    string reading = this.SendAndReceiveMessage("ATRV");

                    if (reading != null && reading != string.Empty)
                        return Double.Parse(reading.Substring(0, reading.Length - 1));
                    else
                        return 0;
                }
                catch (Exception ex)
                {
                    Util.Log(ex);
                    return 0;
                }
            }
        }

        /// <summary>
        /// Gets the version identifier for the connected ELM chip.
        /// </summary>
        public string ElmVersionID
        {
            get;
            private set;
        }

        /// <summary>
        /// Contains methods and properties for retrieving generic
        /// OBD mode 01 PIDs.
        /// </summary>
        public ObdGenericMode01 ObdMode01
        {
            get;
            private set;
        }

        /// <summary>
        /// Contains methods and properties for retrieving generic
        /// OBD mode 09 PIDs.
        /// </summary>
        public ObdGenericMode09 ObdMode09
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the measuring unit.  Setting this property will
        /// affect the values of OBD readings returned.  The value is set 
        /// to English by default.
        /// </summary>
        public ElmMeasuringUnitType MeasuringUnitType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the current OBD protocol.  Note that if this value was originally
        /// set to <see cref="Elm327.Core.ElmDriver.ElmObdProtocolType.Automatic"/>, the value you
        /// get when requesting this property should be the actual protocol that
        /// is in use.
        /// </summary>
        public ElmObdProtocolType ProtocolType
        {
            get
            {
                return this.protocolType;
            }

            set
            {
                this.SendAndReceiveMessage("ATSP" + (char)value);
                this.protocolType = value;
            }
        }

        #endregion

        #region Private Instance Methods

        /// <summary>
        /// Returns a message received from the ELM.  Note that this call
        /// waits until the ready prompt character is received from the ELM
        /// before returning control.
        /// </summary>
        /// <returns>The received message.  If a null value is returned, it means
        /// either an error occurred during the read attempt or a timeout
        /// occurred while waiting for the ELM to respond.</returns>
        private string ReceiveMessage()
        {
            try
            {
                lock (this.serialPort)
                {
                    // Keep reading from the serial connection until the ELM sends the 
                    // ready prompt character or we time out.  We wait for the ready prompt
                    // because while most responses from the ELM will be only one line,
                    // some will be multiline, so we need to keep reading lines until we
                    // receive the ready prompt.

                    DateTime timeStarted = DateTime.Now;
                    int i = 0;

                    do
                    {
                        if (this.serialPort.BytesToRead > 0 && this.serialPort.Read(this.receiveBuffer, i, 1) > 0)
                        {
                            if (this.receiveBuffer[i] == ElmDriver.CHIP_READY_PROMPT_CHAR)
                            {
                                string message = new string(
                                    Encoding.UTF8.GetChars(this.receiveBuffer),
                                    0,
                                    i);

                                // Trim the message to the point just before the last EOL terminator

                                message = message.Substring(0, message.LastIndexOf(ElmDriver.MESSAGE_TERMINATOR_CHAR) - 1).Trim();
                                Util.Log("RCV <- " + message);

                                if (message.IndexOf(ElmDriver.MESSAGE_NO_DATA) > -1)
                                {
                                    // A request timed out or a PID not supported by an ECU
                                    // was requested

                                    return null;
                                }
                                else if (message.IndexOf(ElmDriver.MESSAGE_NO_CONNECTION) > -1)
                                {
                                    // The ELM is unable to connect to the vehicle's OBD system for
                                    // some reason

                                    if (this.ObdConnectionLost != null)
                                        this.ObdConnectionLost();

                                    return null;
                                }
                                else if (message.IndexOf(ElmDriver.MESSAGE_CAN_ERROR) > -1)
                                {
                                    if (this.CanBusError != null)
                                        this.CanBusError();

                                    return null;
                                }
                                else if (message.IndexOf(ElmDriver.MESSAGE_SEARCHING) > -1)
                                {
                                    // If the ELM returned a "SEARCHING..." message, trim it
                                    // out before returning to the caller

                                    return
                                        message.Length > ElmDriver.MESSAGE_SEARCHING.Length ?
                                        message.Substring(ElmDriver.MESSAGE_SEARCHING.Length) :
                                        string.Empty;
                                }
                                else
                                    return message;
                            }

                            i++;
                        }

                        Thread.Sleep(1);
                    }
                    while (timeStarted.AddSeconds(ElmDriver.MAX_WAIT_RECEIVE_SECONDS) > DateTime.Now);

                    // Reaching this point means we've waited too long for the chip to send us a response

                    return null;
                }
            }
            catch (Exception ex)
            {
                Util.Log(ex);
                return null;
            }
        }

        #endregion

        #region Internal Instance Methods

        /// <summary>
        /// Attempts to send a message to the ELM and then receive a response from it.
        /// Note that this call waits until the ready prompt character is received 
        /// from the ELM before returning control.
        /// </summary>
        /// <param name="message">The message to send to the ELM.  The message terminator
        /// character will be appended to this message automatically.</param>
        /// <returns>The received message.  If a null value is returned, it means
        /// either an error occurred during the send or receive attempt.</returns>
        internal string SendAndReceiveMessage(string message)
        {
            lock (this.serialPort)
            {
                try
                {
                    this.serialPort.Write(
                        Encoding.UTF8.GetBytes(message + ElmDriver.MESSAGE_TERMINATOR_CHAR),
                        0,
                        message.Length + 1);

                    Util.Log("SND -> " + message);
                }
                catch (Exception ex)
                {
                    Util.Log(ex);
                    return null;
                }

                return this.ReceiveMessage();
            }
        }

        #endregion

        #region Public Instance Methods

        /// <summary>
        /// Attempts to open a connection to the ELM chip.
        /// </summary>
        /// <returns>A value indicating the result of the connection attempt.</returns>
        public ElmConnectionResultType Connect()
        {
            // Try to force the connection closed if it is open and the caller wants to
            // reinitialize
                
            lock (this.serialPort)
            {
                if (this.serialPort.IsOpen)
                {
                    // TODO: This is untested

                    try
                    {
                        this.serialPort.Close();
                    }
                    catch (Exception ex)
                    {
                        Util.Log(ex);
                    }
                }
            }

            // Try to open the COM port

            lock (this.serialPort)
            {
                try
                {
                    this.serialPort.Open();
                    this.serialPort.Flush();
                    this.serialPort.DiscardInBuffer();
                    this.serialPort.DiscardOutBuffer();
                }
                catch (Exception ex)
                {
                    Util.Log(ex);
                    return ElmConnectionResultType.NoConnectionToElm;
                }
            }

            // Perform a reset on the ELM.  If we get a null back from this call,
            // it means we're unable to see it (probably a wiring issue).

            if (this.SendAndReceiveMessage("ATZ") == null)
                return ElmConnectionResultType.NoConnectionToElm;

            // Turn line feeds and echo off, turn on space printing (as our code currently
            // expects it) and retrieve the ELM's version information
            // TODO: ELM responses will be faster if we turn off space printing, but leaving
            // as-is for now to enable easy message parsing

            this.SendAndReceiveMessage("ATL0");
            this.SendAndReceiveMessage("ATE0");
            this.SendAndReceiveMessage("ATS1");
            this.ElmVersionID = this.SendAndReceiveMessage("ATI");

            // Set the caller's desired protocol, then make a simple "0100" call to
            // make sure the ECU responds.  If we get anything back other than something
            // that starts with "41 00", it means the ELM can't talk to the OBD
            // system.

            this.ProtocolType = this.protocolType;
                    
            string response = this.SendAndReceiveMessage("0100");

            if (response == null || response.IndexOf("41 00") != 0)
                return ElmConnectionResultType.NoConnectionToObd;

            // Ask the ELM to give us the protocol it's using.  We need to ask for
            // this value in case the user chose the "Automatic" setting for protocol
            // type, so we'll know which protocol was actually selected by the ELM.

            response = this.SendAndReceiveMessage("ATDPN");

            if (response != null && response.Length > 0)
            {
                try
                {
                    // 'A' will be the first character returned if the user chose
                    // automatic search mode

                    if (response[0] == 'A' && response.Length > 1)
                        this.protocolType = (ElmObdProtocolType)response[1];
                    else
                        this.protocolType = (ElmObdProtocolType)response[0];
                }
                catch (Exception ex)
                {
                    Util.Log(ex);
                }
            }

            return ElmConnectionResultType.Connected;
        }

        /// <summary>
        /// Attempts to disconnect from the ELM chip.
        /// </summary>
        public void Disconnect()
        {
            try
            {
                // Simply try to close the serial connection

                if (this.serialPort.IsOpen)
                    this.serialPort.Close();
            }
            catch (Exception ex)
            {
                Util.Log(ex);
            }
        }

        /// <summary>
        /// Cleans up all resources used by the driver.
        /// </summary>
        public void Dispose()
        {
            this.Disconnect();

            try
            {
                this.serialPort = null;
            }
            catch (Exception ex)
            {
                Util.Log(ex);
            }
        }

        #endregion

        #region Private Static Methods

        /// <summary>
        /// Called when a serial communication error occurs.
        /// </summary>
        private static void uart_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            // TODO

            Util.Log(e.ToString());
        }

        #endregion
    }
}
