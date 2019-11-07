//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace Gadgeteer.Interfaces
{
    using System;
    using System.IO.Ports;
    using System.Threading;
    using Microsoft.SPOT;
    using Gadgeteer.Modules;

    internal class NativeSerial : Socket.SocketInterfaces.Serial
    {
        private SerialPort _port;
        private bool _hardwareFlowControl;

        public NativeSerial(Socket socket, int baudRate, Socket.SocketInterfaces.SerialParity parity, Socket.SocketInterfaces.SerialStopBits stopBits, int dataBits, Socket.SocketInterfaces.HardwareFlowControl hwFlowRequirement, Module module, string portName, bool hwFlowSupported)
        {
            if (portName == null || portName == "")
            {
                // this is a mainboard error that should not happen (we already check for it in SocketInterfaces.RegisterSocket) but just in case...
                throw Socket.InvalidSocketException.FunctionalityException(socket, "Serial");
            }

            _port = new SerialPort(portName, baudRate, (System.IO.Ports.Parity)parity, dataBits, (System.IO.Ports.StopBits)stopBits);

            if ((hwFlowRequirement != Socket.SocketInterfaces.HardwareFlowControl.NotRequired) && hwFlowSupported)
            {
                _port.Handshake = Handshake.RequestToSend;
                _hardwareFlowControl = true;
            }

            try
            {
                this.ReadTimeout = System.Threading.Timeout.Infinite;
                this.WriteTimeout = System.Threading.Timeout.Infinite;
            }
            catch { }
        }


        public override string PortName
        {
            get { return _port.PortName; }
        }

        public override int BaudRate
        {
            get { return _port.BaudRate; }
            set { _port.BaudRate = value; }
        }

        public override Socket.SocketInterfaces.SerialParity Parity
        {
            get { return (Socket.SocketInterfaces.SerialParity)_port.Parity; }
            set { _port.Parity = (Parity)value; }
        }

        public override Socket.SocketInterfaces.SerialStopBits StopBits
        {
            get { return (Socket.SocketInterfaces.SerialStopBits)_port.StopBits; }
            set { _port.StopBits = (System.IO.Ports.StopBits)value; }
        }

        public override int DataBits
        {
            get { return _port.DataBits; }
            set { _port.DataBits = value; }
        }

        public override bool IsUsingHardwareFlowControl
        {
            get { return _hardwareFlowControl; }
        }

        public override void Open()
        {
            if (!_port.IsOpen)
                _port.Open();
        }

        public override bool IsOpen
        {
            get { return _port.IsOpen; }
        }

        public override void Close()
        {
            _port.Close();
        }

        public override void Flush()
        {
            _port.Flush();
        }

        public override int ReadTimeout
        {
            get { return _port.ReadTimeout; }
            set { _port.ReadTimeout = value; }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _port.Read(buffer, offset, count);
        }

        public override void DiscardInBuffer()
        {
            _port.DiscardInBuffer();
        }

        public override int BytesToRead
        {
            get { return _port.BytesToRead; }
        }

        public override int WriteTimeout
        {
            get { return _port.WriteTimeout; }
            set { _port.WriteTimeout = value; }
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _port.Write(buffer, offset, count);
        }

        public override void DiscardOutBuffer()
        {
            _port.DiscardOutBuffer();
        }

        public override int BytesToWrite
        {
            get { return _port.BytesToWrite; }
        }

        protected override void OnDataReceivedFirstSubscribed()
        {
            _port.DataReceived += OnPortDataReceived;
        }

        protected override void OnDataReceivedLastUnsubscribed()
        {
            _port.DataReceived -= OnPortDataReceived;
        }

        private void OnPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (e.EventType == SerialData.Chars)
                RaiseDataReceived();
        }

        public override void Dispose()
        {
            _port.Dispose();
        }
    }


    /// <summary>
    /// Represents a serial communcations interface port.
    /// </summary>
    public class Serial
    {
        internal readonly Socket.SocketInterfaces.Serial Interface;
        private bool _autoReadLineEnabled;

        /// <summary>
        /// Gets or sets a value that determines how the <see cref="DataReceived"/> and <see cref="LineReceived"/> event is raised.
        /// </summary>
        /// <remarks>
        /// An interrupt may occur on a thread other than the application thread. 
        /// When <see cref="SynchronousUnsafeEventInvocation"/> is <b>false</b> (the default),
        /// the <see cref="DataReceived"/> and <see cref="LineReceived"/> events are not raised immediately,
        /// instead it is queued for raising on the application's dispatcher thread. However, 
        /// when <see cref="SynchronousUnsafeEventInvocation"/> is <b>true</b>, the 
        /// events are raised immediately on the same thread that generated the interrupt.  
        /// This results in faster interrupt processing and may be useful to respond to realtime events, but extra care 
        /// must be taken when using this facility to be thread-safe, i.e. to handle issues such as locking, 
        /// atomic reading/writing of streams/files, deadlock avoidance, etc.
        /// </remarks>
        public bool SynchronousUnsafeEventInvocation;

        // Note: A constructor summary is auto-generated by the doc builder.
        /// <summary></summary>
        /// <remarks>This automatically checks that the socket supports Type U, and reserves the pins.
        /// An exception will be thrown if there is a problem with these checks.</remarks>
        /// <param name="baudRate">The baud rate for the serial port.</param>
        /// <param name="parity">A value from the <see cref="SerialParity"/> enumeration that specifies 
        /// the parity for the port.</param>
        /// <param name="stopBits">A value from the <see cref="SerialStopBits"/> enumeration that specifies 
        /// the stop bits for the port.</param>
        /// <param name="dataBits">The number of data bits.</param>
        /// <param name="socket">The socket for this serial interface.</param>
        /// <param name="hardwareFlowControlRequirement">Specifies whether the module must use hardware flow control, will use hardware flow control if available, or does not use hardware flow control.</param>
        /// <param name="module">The module using this interface (which can be null if unspecified).</param>
        public Serial(Socket socket, int baudRate, SerialParity parity, SerialStopBits stopBits, int dataBits, HardwareFlowControl hardwareFlowControlRequirement, Module module)
        {
            bool hwFlowSupported = false;

            if (hardwareFlowControlRequirement == HardwareFlowControl.Required)
                socket.EnsureTypeIsSupported('K', module);
            else
            {
                hwFlowSupported = socket.SupportsType('K');

                if (!hwFlowSupported)
                    socket.EnsureTypeIsSupported('U', module);
            }

            socket.ReservePin(Socket.Pin.Four, module);
            socket.ReservePin(Socket.Pin.Five, module);
            if (hardwareFlowControlRequirement != HardwareFlowControl.NotRequired)
            {
                // must reserve hardware flow control pins even if not using them, since they are electrically connected.
                socket.ReservePin(Socket.Pin.Six, module);
                socket.ReservePin(Socket.Pin.Seven, module);
            }

            string portName = socket.SerialPortName;

            if ((portName == null || portName == "") && socket.SerialIndirector != null)
                Interface = socket.SerialIndirector(socket, baudRate, (Socket.SocketInterfaces.SerialParity)parity, (Socket.SocketInterfaces.SerialStopBits)stopBits, dataBits, (Socket.SocketInterfaces.HardwareFlowControl)hardwareFlowControlRequirement, module);

            else
                Interface = new NativeSerial(socket, baudRate, (Socket.SocketInterfaces.SerialParity)parity, (Socket.SocketInterfaces.SerialStopBits)stopBits, dataBits, (Socket.SocketInterfaces.HardwareFlowControl)hardwareFlowControlRequirement, module, portName, hwFlowSupported);

            Interface.NewLine = "\n";
            Interface.Encoding = System.Text.Encoding.UTF8;
            Interface.ReadTimeout = InfiniteTimeout;
            Interface.WriteTimeout = InfiniteTimeout;
        }

        /// <summary>
        /// A value that represents an infinite timeout.
        /// </summary>
        public const int InfiniteTimeout = System.Threading.Timeout.Infinite;

        /// <summary>
        /// Gets or sets the line-received event delimiter.
        /// </summary>
        /// <remarks>
        /// <para>
        ///  The default value of this property is a new-line character, ASCII 0x0A.
        ///  When you set <see cref="AutoReadLineEnabled"/> to <b>true</b>, the value
        ///  of this property is used to determine when a complete line of data has been received
        ///  and, consequently, when to raise the <see cref="LineReceived"/> event.
        /// </para>
        /// <para>
        ///  The value of this property is also appended to the specifed text when you
        ///  call the <see cref="WriteLine"/> method.
        /// </para> 
        /// </remarks>
        public string LineReceivedEventDelimiter
        {
            get { return Interface.NewLine; }
            set { Interface.NewLine = value; }
        }

        /// <summary>
        /// Gets the port name associated with this serial interface.
        /// </summary>
        public string PortName
        {
            get { return Interface.PortName; }
        }

        /// <summary>
        /// Gets or sets the baud rate of this serial interface.
        /// </summary>
        public int BaudRate
        {
            get { return Interface.BaudRate; }
            set { Interface.BaudRate = value; }
        }

        /// <summary>
        /// Gets or sets the parity of this serial interface.
        /// </summary>
        /// <value>
        /// A value from the <see cref="SerialParity"/> enumeration that specifies the parity of 
        /// this serial interface.
        /// </value>
        public SerialParity Parity
        {
            get { return (SerialParity)Interface.Parity; }
            set { Interface.Parity = (Socket.SocketInterfaces.SerialParity)value; }
        }

        /// <summary>
        /// Gets or sets the stop bits of this serial interface.
        /// </summary>
        /// <value>
        ///  A value from the <see cref="SerialStopBits"/> enumeration that specifies the 
        ///  stop bits of this serial interface.
        /// </value>
        public SerialStopBits StopBits
        {
            get { return (SerialStopBits)Interface.StopBits; }
            set { Interface.StopBits = (Socket.SocketInterfaces.SerialStopBits)value; }
        }

        /// <summary>
        /// Gets or sets the number of data bits of this serial interface. 
        /// </summary>
        public int DataBits
        {
            get { return Interface.DataBits; }
            set { Interface.DataBits = value; }
        }

        /// <summary>
        /// Returns a Boolean value that indicates whether the Serial interface is using hardware flow control.
        /// </summary>
        public bool UsingHardwareFlowControl
        {
            get { return Interface.IsUsingHardwareFlowControl; }
        }

        /// <summary>
        /// Gets or sets the encoding used on this serial port for writing and reading strings.
        /// </summary>
        public System.Text.Encoding Encoding
        {
            get { return Interface.Encoding; }
            set { Interface.Encoding = value; }
        }

        /// <summary>
        /// Gets or sets a value that determines whether automatic line reading is enabled.
        /// </summary>
        /// <remarks>
        /// <para>
        ///  When you set <see cref="AutoReadLineEnabled"/> to <b>true</b>, automatic reading
        ///  of the serial port is enabled. When enabled, <see cref="Serial"/> will continuously 
        ///  monitor the serial port; if the port is open (that is, <see cref="IsOpen"/> is <b>true</b>),
        ///  <see cref="Serial"/> will collect incoming data. Whenever a complete line of data is received 
        ///  as determined by the value of <see cref="LineReceivedEventDelimiter"/>, 
        ///  <see cref="Serial"/> raises the <see cref="LineReceived"/> event.
        /// </para>
        /// </remarks>
        public bool AutoReadLineEnabled
        {
            get { return _autoReadLineEnabled; }
            set
            {
                if (_autoReadLineEnabled == value)
                    return;

                _autoReadLineEnabled = value;

                if (value)
                    Interface.LineReceived += OnInterfaceLineReceived;
                else
                    Interface.LineReceived -= OnInterfaceLineReceived;
            }
        }

        /// <summary>
        /// Specifies the parity bit for a <see cref="Serial"/> object. 
        /// </summary>
        /// <remarks>
        /// <para>
        ///  Use this enumeration when setting the <see cref="SerialParity"/> property for a serial port connection.
        /// </para>
        /// <para>
        ///  Parity is an error-checking procedure in which the number of 1s must always be the same—either even or odd—for each 
        ///  group of bits that is transmitted without error. In modem-to-modem communications, parity is often one of the parameters 
        ///  that must be agreed upon by sending parties and receiving parties before transmission can take place.
        /// </para>
        /// </remarks>
        public enum SerialParity
        {
            /// <summary>
            /// Sets the parity bit so that the count of bits set is an even number.
            /// </summary>
            Even = System.IO.Ports.Parity.Even,
            /// <summary>
            /// Sets the parity bit so that the count of bits set is an odd number.
            /// </summary>
            Odd = System.IO.Ports.Parity.Odd,
            /// <summary>
            /// Leaves the parity bit set to 1.
            /// </summary>
            Mark = System.IO.Ports.Parity.Mark,
            /// <summary>
            /// Leaves the parity bit set to 0.
            /// </summary>
            Space = System.IO.Ports.Parity.Space,
            /// <summary>
            /// No parity check occurs.
            /// </summary>
            None = System.IO.Ports.Parity.None
        }

        /// <summary>
        /// Specifies the number of stop bits used on the <see cref="Serial"/> object. 
        /// </summary>
        /// <remarks>
        /// <para>
        ///  This enumeration specifies the number of stop bits to use. Stop bits separate each unit of 
        ///  data on an asynchronous serial connection. 
        ///  They are also sent continuously when no data is available for transmission.
        /// </para>
        /// <para>
        /// The <b>None</b> option is not supported. Setting the <see cref="StopBits"/> property 
        /// to <b>None</b> raises an ArgumentOutOfRangeException.
        /// </para>
        /// </remarks>
        public enum SerialStopBits
        {
            /// <summary>
            /// No stop bits are used. This value is not supported. Setting the <see cref="StopBits"/> property 
            /// to <b>None</b> raises an ArgumentOutOfRangeException.
            /// </summary>
            None = System.IO.Ports.StopBits.None,
            /// <summary>
            /// One stop bit is used.
            /// </summary>
            One = System.IO.Ports.StopBits.One,
            /// <summary>
            /// 1.5 stop bits are used.
            /// </summary>
            OnePointFive = System.IO.Ports.StopBits.OnePointFive,
            /// <summary>
            /// Two stop bit are used.
            /// </summary>
            Two = System.IO.Ports.StopBits.Two
        }

        /// <summary>
        /// Specifies whether the <see cref="Serial"/> module requires hardware flow control. 
        /// </summary>
        public enum HardwareFlowControl
        {
            /// <summary>
            /// The module does not require hardware flow control.
            /// </summary>
            NotRequired,

            /// <summary>
            /// The module will use hardware flow control if available.
            /// </summary>
            UseIfAvailable,

            /// <summary>
            /// The module must have hardware flow control and will not function without it.
            /// </summary>
            Required
        }

        /// <summary>
        /// Gets or sets the number of milliseconds before a time-out occurs when a read operation does not finish. 
        /// </summary>
        public int ReadTimeout
        {
            get { return Interface.ReadTimeout; }
            set { Interface.ReadTimeout = value; }
        }

        /// <summary>
        /// Gets or sets the number of milliseconds before a time-out occurs when a write operation does not finish. 
        /// </summary>
        public int WriteTimeout
        {
            get { return Interface.WriteTimeout; }
            set { Interface.WriteTimeout = value; }
        }

        /// <summary>
        /// Opens a new serial port connection. 
        /// </summary>
        public void Open()
        {
            Interface.Open();
        }

        /// <summary>
        /// Gets a Boolean value indicating the open or closed status of the <see cref="Serial"/> object. 
        /// </summary>
        public bool IsOpen
        {
            get { return Interface.IsOpen; }
        }

        /// <summary>
        /// Closes the port connection, and sets the <see cref="IsOpen"/> property to <b>false</b>.
        /// </summary>
        public void Close()
        {
            Interface.Close();
        }

        /// <summary>
        /// Writes a variable number of bytes to the serial port using data from a buffer. 
        /// </summary>
        /// <param name="data">The data to write as a byte[] array.</param>
        public void Write(params byte[] data)
        {
            Interface.Write(data);
        }

        /// <summary>
        /// Writes a specified number of bytes to the serial port using data from a buffer. 
        /// </summary>
        /// <param name="buffer">The byte[] array that contains the data to write to the port.</param>
        /// <param name="offset">The zero-based byte offset of the <paramref name="buffer"/> parameter 
        /// at which to begin copying bytes to the port.</param>
        /// <param name="count">The number of bytes to write.</param>
        public void Write(byte[] buffer, int offset, int count)
        {
            Interface.Write(buffer, offset, count);
        }

        /// <summary>
        /// Writes the specified text to the serial port.
        /// </summary>
        /// <param name="text">The text to write.</param>
        public void Write(string text)
        {
            Interface.Write(text);
        }

        /// <summary>
        /// Writes the specified text and the value of <see cref="LineReceivedEventDelimiter"/> to the serial port.
        /// </summary>
        /// <param name="text">The text to write.</param>
        public void WriteLine(string text)
        {
            Interface.WriteLine(text);
        }

        /// <summary>
        /// Reads a number of bytes from the serial port input buffer and writes those bytes 
        /// to a byte array at the specified offset. 
        /// </summary>
        /// <param name="buffer">The byte[] array to write the input to.</param>
        /// <param name="offset">The offset in the <paramref name="buffer"/> array to begin writing.</param>
        /// <param name="count">The number of bytes to read.</param>
        /// <returns>The number of bytes read.</returns>
        public int Read(byte[] buffer, int offset, int count)
        {
            return Interface.Read(buffer, offset, count);
        }

        /// <summary>
        /// Reads a byte from the serial port.
        /// </summary>
        /// <returns>The byte read from the port as an integer value.</returns>
        public int ReadByte()
        {
            return Interface.ReadByte();
        }

        /// <summary>
        /// Sends any data waiting in the 'send' buffer and clears the buffer.
        /// </summary>
        public void Flush()
        {
            Interface.Flush();
        }

        /// <summary>
        /// Discards data from the serial driver's 'send' buffer. 
        /// </summary>
        public void DiscardOutBuffer()
        {
            Interface.DiscardOutBuffer();
        }

        /// <summary>
        /// Discards data from the serial driver's 'receive' buffer. 
        /// </summary>
        public void DiscardInBuffer()
        {
            Interface.DiscardInBuffer();
        }
        /// <summary>
        /// Gets the number of bytes of data in the 'send' buffer. 
        /// </summary>
        public int BytesToWrite
        {
            get { return Interface.BytesToWrite; }
        }

        /// <summary>
        /// Gets the number of bytes of data in the 'receive' buffer. 
        /// </summary>
        public int BytesToRead
        {
            get { return Interface.BytesToRead; }
        }

        /// <summary>
        /// Represents the delegate used for the <see cref="DataReceived"/> event.
        /// </summary>
        /// <param name="sender">The <see cref="Serial"/> object that raised the event.</param>
        /// <param name="data">A <see cref="SerialData"/> object that contains the data received.</param>
        public delegate void DataReceivedEventHandler(Serial sender, SerialData data);
        private DataReceivedEventHandler _dataReceivedHandler;

        /// <summary>
        /// Delegate that handles the event raised when the serial port signals that data has been received.
        /// </summary>
        public event DataReceivedEventHandler DataReceived
        {
            add
            {
                if (value == null)
                    return;

                if (_dataReceivedHandler == null)
                    Interface.DataReceived += OnInterfaceDataReceived;

                _dataReceivedHandler += value;
            }
            remove
            {
                if (value == null)
                    return;

                _dataReceivedHandler -= value;

                if (_dataReceivedHandler == null)
                    this.Interface.DataReceived -= OnInterfaceDataReceived;
            }
        }

        private void OnInterfaceDataReceived(Socket.SocketInterfaces.Serial sender)
        {
            OnDataReceivedEvent(this, SerialData.Chars);
        }

        /// <summary>
        /// Event raised when data is received from the <see cref="Serial"/> object.
        /// </summary>
        /// <param name="sender">The <see cref="Serial"/> object that raised the event</param>
        /// <param name="data">A <see cref="SerialData"/> object that contains the data received.</param>
        protected virtual void OnDataReceivedEvent(Serial sender, SerialData data)
        {
            DataReceivedEventHandler handler = _dataReceivedHandler;

            if (handler == null)
                return;

            if (SynchronousUnsafeEventInvocation)
            {
                try { handler(sender, data); }
                catch { }
            }
            else
            {
                if (Program.CheckAndInvoke(handler, handler, sender, data))
                    handler(sender, data);
            }
        }

        /// <summary>
        /// Represents the delegate used for the <see cref="LineReceived"/> event.
        /// </summary>
        /// <param name="sender">The <see cref="Serial"/> object that raised the event.</param>
        /// <param name="line">The received line of data as string.</param>
        public delegate void LineReceivedEventHandler(Serial sender, string line);
        private LineReceivedEventHandler _lineReceivedHandler;

        /// <summary>
        /// Raised when a complete line of data has been received.
        /// </summary>
        /// <remarks>
        /// <para>
        ///  Handle this event to minimize the overhead required to obtain
        ///  data from the serial port.
        /// </para>
        /// <para>
        ///  When you set <see cref="AutoReadLineEnabled"/> to <b>true</b>, automatic reading
        ///  of the serial port is enabled. When enabled, <see cref="Serial"/> will continuously 
        ///  monitor the serial port; if the port is open (that is, <see cref="IsOpen"/> is <b>true</b>),
        ///  <see cref="Serial"/> will collect incoming data. Whenever a complete line of data is received, 
        ///  as determined by the value of <see cref="LineReceivedEventDelimiter"/>,
        ///  <see cref="Serial"/> raises the <see cref="LineReceived"/> event.
        /// </para>
        /// </remarks>
        public event LineReceivedEventHandler LineReceived
        {
            add
            {
                if (value == null)
                    return;

                if (_lineReceivedHandler == null)
                {
                    AutoReadLineEnabled = true;
                }

                _lineReceivedHandler += value;
            }

            remove
            {
                if (value == null)
                    return;

                _lineReceivedHandler -= value;

                if (_lineReceivedHandler == null)
                {
                    AutoReadLineEnabled = false;
                }
            }
        }

        private void OnInterfaceLineReceived(Socket.SocketInterfaces.Serial sender, string line)
        {
            OnLineReceivedEvent(this, line);
        }

        /// <summary>
        /// Raises the <see cref="LineReceived"/> event.
        /// </summary>
        /// <param name="sender">The <see cref="Serial"/> object that raised the event.</param>
        /// <param name="line">The received line of data.</param>
        protected virtual void OnLineReceivedEvent(Serial sender, string line)
        {
            LineReceivedEventHandler handler = _lineReceivedHandler;

            if (handler == null)
                return;

            if (SynchronousUnsafeEventInvocation)
            {
                try { handler(sender, line); }
                catch { }
            }
            else
            {
                if (Program.CheckAndInvoke(handler, handler, sender, line))
                    handler(sender, line);
            }
        }

        /// <summary>
        /// Represents the exception that is raised when the serial port has not been
        /// opened prior to a read or write operation.
        /// </summary>
        public class PortNotOpenException : ApplicationException
        {
            // Note: A constructor summary is auto-generated by the doc builder.
            /// <summary></summary>
            public PortNotOpenException()
                : base("The port must be opened before use by calling the Open() method.")
            {
            }

            // Note: A constructor summary is auto-generated by the doc builder.
            /// <summary></summary>
            /// <param name="innerException">The inner exception, or <b>null</b> if none.</param>
            public PortNotOpenException(Exception innerException)
                : base("The port must be opened before use by calling the Open() method.", innerException)
            {
            }
        }

        /// <summary>
        /// Returns the <see cref="Socket.SocketInterfaces.Serial" /> for a <see cref="Serial" /> interface.
        /// </summary>
        /// <param name="this">An instance of <see cref="Serial" />.</param>
        /// <returns>The <see cref="Socket.SocketInterfaces.Serial" /> for <paramref name="this"/>.</returns>
        public static explicit operator Socket.SocketInterfaces.Serial(Serial @this)
        {
            return @this.Interface;
        }

        /// <summary>
        /// Releases all resources used by the interface.
        /// </summary>
        public void Dispose()
        {
            Interface.Dispose();
        }
    }
}
