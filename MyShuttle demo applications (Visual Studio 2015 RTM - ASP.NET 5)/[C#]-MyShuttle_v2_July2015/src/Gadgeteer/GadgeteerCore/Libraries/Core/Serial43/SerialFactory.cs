//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace Gadgeteer.SocketInterfaces
{
    using Gadgeteer.Modules;
    using Microsoft.SPOT.Hardware;

    /// <summary>
    /// Provides access to the <see cref="Serial" /> interfaces on sockets.
    /// </summary>
    public static class SerialFactory
    {
        /// <summary>
        /// Creates an instance of <see cref="Serial" /> for the given socket.
        /// </summary>
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
        /// <returns>An instance of <see cref="Serial" /> for the given socket.</returns>
        public static Serial Create(Socket socket, int baudRate, SerialParity parity, SerialStopBits stopBits, int dataBits, HardwareFlowControl hardwareFlowControlRequirement, Module module)
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

            Serial instance;

            if ((portName == null || portName == "") && socket.SerialIndirector != null)
                instance = socket.SerialIndirector(socket, baudRate, (SerialParity)parity, (SerialStopBits)stopBits, dataBits, (HardwareFlowControl)hardwareFlowControlRequirement, module);

            else
                instance = new NativeSerial(socket, baudRate, (SerialParity)parity, (SerialStopBits)stopBits, dataBits, (HardwareFlowControl)hardwareFlowControlRequirement, module, portName, hwFlowSupported);

            instance.NewLine = "\n";
            instance.ReadTimeout = System.Threading.Timeout.Infinite;
            instance.WriteTimeout = System.Threading.Timeout.Infinite;            
            return instance;
        }
    }
}