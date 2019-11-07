////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using Gadgeteer.Modules;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using System;

namespace Gadgeteer
{
    partial class Socket
    {
        /// <summary>
        /// This static class contains interfaces used by mainboards to provide functionalities on sockets to Gadgeteer.  
        /// End users do not need to use this class directly and should normally use GTM.Modules to access functionality.
        /// Module developers do not need to use this class directly and should normally use GT.Socket and GT.Interfaces to access the required functionality.
        /// </summary>
        public static partial class SocketInterfaces
        {
            /// <summary>
            /// Creates a new <see cref="Socket" /> object specifying the socket number.
            /// </summary>
            /// <remarks>
            /// This should be used by the mainboard's constructor to create socket instances,
            /// which should then configure the socket properties as appropriate, and then call <see cref="RegisterSocket" />
            /// NB the socket name is fixed to be the same as the socket number.
            /// </remarks>
            /// <param name="socketNumber">The mainboard socket number</param>
            public static Socket CreateNumberedSocket(int socketNumber)
            {
                return new Socket(socketNumber, socketNumber.ToString());
            }

            /// <summary>
            /// Creates a new <see cref="Socket" /> object specifying the socket name.
            /// </summary>
            /// <remarks>
            /// This should be used by module constructors to create socket instances if they provide sockets to other modules.  
            /// The module constructor should then configure the socket properties as appropriate, and then call <see cref="RegisterSocket" />
            /// A socketNumber is auto-assigned.
            /// </remarks>
            /// <param name="name">The socket's name</param>
            public static Socket CreateUnnumberedSocket(String name)
            {
                int socketNumber;
                lock (Socket._sockets)
                {
                    while (Socket.GetSocket(autoSocketNumber, false, null, null) != null) autoSocketNumber--;
                    socketNumber = autoSocketNumber;
                    autoSocketNumber--;
                }
                return new Socket(socketNumber, name);
            }
            private static int autoSocketNumber = -10;

            private static bool DoRegistrationChecks = true;

            private static void SocketRegistrationError(Socket socket, string message)
            {
                Debug.Print("Warning: socket " + socket + " is not compliant with Gadgeteer : " + message);
            }

            private static void TestPinsPresent(Socket socket, int[] pins, char type)
            {
                for (int i = 0; i < pins.Length; i++)
                {
                    if (socket.CpuPins[pins[i]] == Socket.UnspecifiedPin) SocketRegistrationError(socket, "Cpu pin " + pins[i] + " must be specified for socket of type " + type);
                }
            }

            /// <summary>
            /// Registers a socket.  Should be used by mainboards and socket-providing modules during initialization.
            /// </summary>
            /// <param name="socket">The socket to register</param>
            public static void RegisterSocket(Socket socket)
            {
                if (DoRegistrationChecks)
                {
                    if (socket.CpuPins == null || socket.CpuPins.Length != 11) SocketRegistrationError(socket, "CpuPins array must be of length 11");
                    if (socket.SupportedTypes == null || socket.SupportedTypes.Length == 0) SocketRegistrationError(socket, "SupportedTypes list is null/empty");
                    foreach (char type in socket.SupportedTypes)
                    {
                        switch (type)
                        {
                            case 'A':
                                TestPinsPresent(socket, new int[] { 3, 4, 5, 6 }, type);
                                if (socket.AnalogInputIndirector == null)
                                {
                                    if (socket.AnalogInput3 == Cpu.AnalogChannel.ANALOG_NONE || socket.AnalogInput4 == Cpu.AnalogChannel.ANALOG_NONE || socket.AnalogInput5 == Cpu.AnalogChannel.ANALOG_NONE) SocketRegistrationError(socket, "Socket of type A must support analog input functionality on pins 3, 4 and 5");
                                    if (socket.AnalogInputScale == double.MinValue || socket.AnalogInputOffset == double.MinValue || socket.AnalogInputPrecisionInBits == int.MinValue) SocketRegistrationError(socket, "Socket of type A must provide analog input scale/offset through calling SocketInterfaces.SetAnalogInputFactors");
                                }
                                break;
                            case 'C':
                                TestPinsPresent(socket, new int[] { 3, 4, 5, 6 }, type);
                                break;
                            case 'D':
                                TestPinsPresent(socket, new int[] { 3, 6, 7 }, type);
                                break;
                            case 'E':
                                TestPinsPresent(socket, new int[] { 6, 7, 8, 9 }, type);
                                break;
                            case 'F':
                                TestPinsPresent(socket, new int[] { 3, 4, 5, 6, 7, 8, 9 }, type);
                                break;
                            case 'H':
                                TestPinsPresent(socket, new int[] { 3 }, type);
                                break;
                            case 'I':
                                TestPinsPresent(socket, new int[] { 3, 6, 8, 9 }, type);
                                break;
                            case 'K':
                                TestPinsPresent(socket, new int[] { 3, 4, 5, 6, 7 }, type);
                                if (socket.SerialIndirector == null)
                                {
                                    if (socket.SerialPortName == null) SocketRegistrationError(socket, "Socket of type K must specify serial port name");
                                }
                                break;
                            case 'O':
                                TestPinsPresent(socket, new int[] { 3, 4, 5 }, type);
                                if (socket.AnalogOutputIndirector == null)
                                {
                                    if (socket.AnalogOutput5 == Cpu.AnalogOutputChannel.ANALOG_OUTPUT_NONE) SocketRegistrationError(socket, "Socket of type O must support analog output functionality");
                                    if (socket.AnalogOutputScale == double.MinValue || socket.AnalogOutputOffset == double.MinValue || socket.AnalogOutputPrecisionInBits == int.MinValue) SocketRegistrationError(socket, "Socket of type O must provide analog output scale/offset through calling SocketInterfaces.SetAnalogOutputFactors");
                                }
                                break;
                            case 'P':
                                TestPinsPresent(socket, new int[] { 3, 6, 7, 8, 9 }, type);
                                if (socket.PwmOutputIndirector == null)
                                {
                                    if (socket.PWM7 == Cpu.PWMChannel.PWM_NONE || socket.PWM8 == Cpu.PWMChannel.PWM_NONE || socket.PWM9 == Cpu.PWMChannel.PWM_NONE) SocketRegistrationError(socket, "Socket of type P must support PWM functionality");
                                }
                                break;
                            case 'S':
                                TestPinsPresent(socket, new int[] { 3, 4, 5, 6, 7, 8, 9 }, type);
                                if (socket.SpiIndirector == null)
                                {
                                    if (socket.SPIModule == Socket.SocketInterfaces.SPIMissing) SocketRegistrationError(socket, "Socket of type S must specify SPI module number");
                                }
                                break;
                            case 'T':
                                TestPinsPresent(socket, new int[] { 4, 5, 6, 7 }, type);
                                break;
                            case 'U':
                                TestPinsPresent(socket, new int[] { 3, 4, 5, 6 }, type);
                                if (socket.SerialIndirector == null)
                                {
                                    if (socket.SerialPortName == null) SocketRegistrationError(socket, "Socket of type U must specify serial port name");
                                }
                                break;
                            case 'R':
                                TestPinsPresent(socket, new int[] { 3, 4, 5, 6, 7, 8, 9 }, type);
                                break;
                            case 'G':
                                TestPinsPresent(socket, new int[] { 3, 4, 5, 6, 7, 8, 9 }, type);
                                break;
                            case 'B':
                                TestPinsPresent(socket, new int[] { 3, 4, 5, 6, 7, 8, 9 }, type);
                                break;
                            case 'X':
                                TestPinsPresent(socket, new int[] { 3, 4, 5 }, type);
                                break;
                            case 'Y':
                                TestPinsPresent(socket, new int[] { 3, 4, 5, 6, 7, 8, 9 }, type);
                                break;
                            case 'Z':
                                // manufacturer specific socket - no tests
                                break;
                            case '*':
                                // * is a special case  - daisylink modules don't actually declare their new socket in code, instead reusing the mainboard socket number 
                                // so we don't need the below, but it doesnt hurt to leave it in
                                TestPinsPresent(socket, new int[] { 3, 4, 5 }, type);
                                break;
                            default:
                                SocketRegistrationError(socket, "Socket type '" + type + "' is not supported by Gadgeteer");
                                break;
                        }
                    }
                }

                lock (Socket._sockets)
                {
                    if (Socket.GetSocket(socket.SocketNumber, false, null, null) != null) throw new Socket.InvalidSocketException("Cannot register socket - socket number " + socket.SocketNumber + " already used");
                    Socket._sockets.Add(socket);
                    socket._registered = true;
                }
            }

            /// <summary>
            /// Set the scale and offset used by <see cref="Microsoft.SPOT.Hardware.AnalogInput.Read" /> to transform the raw value into a voltage between 0 and 3.3V, and the precision in bits.
            /// </summary>
            /// <param name="scale">The multiplicative scale factor.</param>
            /// <param name="offset">The additive offset.</param>
            /// <param name="precisionInBits">The number of bits precision by this analog input socket.</param>
            /// <param name="socket">The socket being configured.</param>
            public static void SetAnalogInputFactors(Socket socket, double scale, double offset, int precisionInBits)
            {
                socket.AnalogInputScale = scale;
                socket.AnalogInputOffset = offset;
                socket.AnalogInputPrecisionInBits = precisionInBits;
            }

            /// <summary>
            /// Set the scale and offset used by <see cref="Microsoft.SPOT.Hardware.AnalogInput.Read" /> to transform the raw value into a voltage between 0 and 3.3V, and the precision in bits.
            /// </summary>
            /// <param name="scale">The multiplicative scale factor.</param>
            /// <param name="offset">The additive offset.</param>
            /// <param name="precisionInBits">The number of bits precision by this analog input socket.</param>
            /// <param name="socket">The socket being configured.</param>
            public static void SetAnalogOutputFactors(Socket socket, double scale, double offset, int precisionInBits)
            {
                socket.AnalogOutputScale = scale;
                socket.AnalogOutputOffset = offset;
                socket.AnalogOutputPrecisionInBits = precisionInBits;
            }

            /// <summary>
            /// A value indicating that a socket has no associated SPI bus
            /// </summary>
            public static readonly SPI.SPI_module SPIMissing = (SPI.SPI_module)(-1);
        }
    }
}