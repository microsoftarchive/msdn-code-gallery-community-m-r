////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using Gadgeteer.Interfaces;
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
            /// Creates a new <see cref="Socket"/> object specifying the socket number.
            /// </summary>
            /// <remarks>
            /// This should be used by the mainboard's constructor to create socket instances,
            /// which should then configure the socket properties as appropriate, and then call <see cref="RegisterSocket"/>
            /// NB the socket name is fixed to be the same as the socket number.
            /// </remarks>
            /// <param name="socketNumber">The mainboard socket number</param>
            public static Socket CreateNumberedSocket(int socketNumber)
            {
                return new Socket(socketNumber, socketNumber.ToString());
            }

            /// <summary>
            /// Creates a new <see cref="Socket"/> object specifying the socket name.
            /// </summary>
            /// <remarks>
            /// This should be used by module constructors to create socket instances if they provide sockets to other modules.  
            /// The module constructor should then configure the socket properties as appropriate, and then call <see cref="RegisterSocket"/>
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
                foreach (int pin in pins)
                {
                    if (socket.CpuPins[pin] == Socket.UnspecifiedPin) SocketRegistrationError(socket, "Cpu pin " + pin + " must be specified for socket of type " + type);
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
                                if (socket.AnalogOutput == null) SocketRegistrationError(socket, "Socket of type O must support analog output functionality");
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


            #region Interfaces

            /// <summary>
            /// Provides the analog input functionality.
            /// </summary>
            public abstract class AnalogInput : IDisposable
            {
                /// <summary>
                /// Gets or sets the active state of the analog input.
                /// </summary>
                public abstract bool IsActive { get; set; }
                /// <summary>
                /// Reads the current analog input value as a voltage between 0 and 3.3V.
                /// </summary>
                /// <returns>The current analog value in volts.</returns>
                public abstract double ReadVoltage();
                /// <summary>
                /// Reads the current analog input value as a proportion from 0.0 to 1.0 of the maximum value (3.3V).
                /// </summary>
                /// <returns>The analog input value from 0.0-1.0</returns>
                public virtual double ReadProportion()
                {
                    return ReadVoltage() / 3.3;
                }

                /// <summary>
                /// Releases all resources used by the interface.
                /// </summary>
                public virtual void Dispose() { }
            }

            /// <summary>
            /// Provides the digital input functionality.
            /// </summary>
            public abstract class DigitalInput : IDisposable
            {
                /// <summary>
                /// Reads a Boolean value at the interface port input. 
                /// </summary>
                /// <returns>A Boolean value that represents the current value of the port, either 0 or 1.</returns>
                public abstract bool Read();

                /// <summary>
                /// Releases all resources used by the interface.
                /// </summary>
                public virtual void Dispose() { }
            }

            /// <summary>
            /// Provides the digital input and output functionality.
            /// </summary>
            public abstract class DigitalIO : IDisposable
            {
                /// <summary>
                /// Gets or sets the mode for the digital input/output interface.
                /// </summary>
                /// <value>
                /// <list type="bullet">
                /// <item><see cref="IOMode.Input" /> if the interface is currently set for input operations.</item>
                /// <item><see cref="IOMode.Output" /> if the interface is currently set for ouput operations.</item>
                /// </list>
                /// </value>
                public abstract IOMode Mode { get; set; }
                /// <summary>
                /// Sets the interface to <see cref="IOMode.Output" /> and writes the specified value.
                /// </summary>
                /// <param name="state"></param>
                public abstract void Write(bool state);
                /// <summary>
                /// Sets the interface to <see cref="IOMode.Input" /> and reads a value.
                /// </summary>
                /// <returns>A Boolean value read from the interface.</returns>
                public abstract bool Read();

                /// <summary>
                /// Releases all resources used by the interface.
                /// </summary>
                public virtual void Dispose() { }
            }

            /// <summary>
            /// Provides the digital output functionality.
            /// </summary>
            public abstract class DigitalOutput : IDisposable
            {
                /// <summary>
                /// Writes a value to the interface port output. 
                /// </summary>
                /// <param name="state">The value to be written to the port output.</param>
                public abstract void Write(bool state);
                /// <summary>
                /// Reads a Boolean value at the interface port input.
                /// </summary>
                /// <returns>The current value of the port (either 0 or 1).</returns>
                public abstract bool Read();

                /// <summary>
                /// Releases all resources used by the interface.
                /// </summary>
                public virtual void Dispose() { }
            }

            /// <summary>
            /// Provides the I2C functionality.
            /// </summary>
            public abstract class I2CBus : IDisposable
            {
                /// <summary>
                /// Gets or sets the address of the <see cref="I2CBus" /> device.
                /// </summary>
                public abstract ushort Address { get; set; }
                /// <summary>
                /// Performs a series of I2C transactions.
                /// </summary>
                /// <param name="transactions">The list of transactions to perform.</param>
                /// <param name="millisecondsTimeout">The amount of time, in milliseconds, that the system will wait before resuming execution of the transaction.</param>
                /// <returns>The number of bytes of data transferred in the transaction.</returns>
                public abstract int Execute(I2CDevice.I2CTransaction[] transactions, int millisecondsTimeout);

                /// <summary>
                /// Writes an array of bytes to the I2C device.
                /// </summary>
                /// <param name="writeBuffer">The array of bytes that will be sent to the I2C device.</param>
                /// <param name="millisecondsTimeout">The amount of time, in milliseconds, that the system will wait before resuming execution of the transaction.</param>
                /// <returns>The number of bytes of data transferred in the transaction.</returns>
                public int Write(byte[] writeBuffer, int millisecondsTimeout)
                {
                    if (writeBuffer == null)
                        return 0;

                    var transactions = new I2CDevice.I2CTransaction[] { I2CDevice.CreateWriteTransaction(writeBuffer) };

                    return Execute(transactions, millisecondsTimeout);
                }

                /// <summary>
                /// Reads an array of bytes from the device into a specified read buffer.
                /// </summary>
                /// <param name="readBuffer">The array of bytes that will contain the data read from the I2C device.</param>
                /// <param name="millisecondsTimeout">The amount of time, in milliseconds, that the system will wait before resuming execution of the transaction.</param>
                /// <returns>The number of bytes of data transferred in the transaction.</returns>
                public int Read(byte[] readBuffer, int millisecondsTimeout)
                {
                    if (readBuffer == null)
                        return 0;

                    var transactions = new I2CDevice.I2CTransaction[] { I2CDevice.CreateReadTransaction(readBuffer) };

                    return Execute(transactions, millisecondsTimeout);
                }

                /// <summary>
                ///  Writes an array of bytes to the I2C device, and reads an array of bytes from the device into a specified read buffer.
                /// </summary>
                /// <param name="writeBuffer">The array of bytes that will be sent to the I2C device.</param>
                /// <param name="readBuffer">The array of bytes that will contain the data read from the I2C device.</param>
                /// <param name="millisecondsTimeout">The amount of time, in milliseconds, that the system will wait before resuming execution of the transaction.</param>
                /// <returns>The number of bytes of data transferred in the transaction.</returns>
                public int WriteRead(byte[] writeBuffer, byte[] readBuffer, int millisecondsTimeout)
                {
                    if (writeBuffer == null)
                        return Read(readBuffer, millisecondsTimeout);

                    if (readBuffer == null)
                        return Write(writeBuffer, millisecondsTimeout);

                    var transactions = new I2CDevice.I2CTransaction[] { I2CDevice.CreateWriteTransaction(writeBuffer), I2CDevice.CreateReadTransaction(readBuffer) };

                    return Execute(transactions, millisecondsTimeout);
                }

                /// <summary>
                /// Releases all resources used by the interface.
                /// </summary>
                public virtual void Dispose() { }
            }

            /// <summary>
            /// Provides the interrupt input functionality.
            /// </summary>
            public abstract class InterruptInput : IDisposable
            {
                /// <summary>
                /// Reads a Boolean value at the InterruptInput interface port input.
                /// </summary>
                /// <returns>A Boolean value that indicates the current value of the port as either 0 or 1).</returns>
                public abstract bool Read();

                /// <summary>
                /// Called when the first handler is subscribed to the <see cref="Interrupt" /> event.
                /// </summary>
                protected abstract void OnInterruptFirstSubscribed();
                /// <summary>
                /// Called when the last handler is unsubsrcibed from the <see cref="Interrupt" /> event.
                /// </summary>
                protected abstract void OnInterruptLastUnsubscribed();
                /// <summary>
                /// Raises the <see cref="Interrupt" /> event.
                /// </summary>
                /// <param name="value"><b>true</b> if the the value received from the interrupt is greater than zero; otherwise, <b>false</b>.</param>
                protected void RaiseInterrupt(bool value)
                {
                    if (this.interrupt != null)
                    {
                        this.interrupt(this, value);
                    }
                }

                private InterruptEventHandler interrupt;
                /// <summary>
                /// Raised when the <see cref="InterruptInput" /> interface detects an interrupt.
                /// </summary>
                public event InterruptEventHandler Interrupt
                {
                    add
                    {
                        if (this.interrupt == null)
                        {
                            OnInterruptFirstSubscribed();
                        }

                        this.interrupt += value;
                    }
                    remove
                    {
                        this.interrupt -= value;

                        if (this.interrupt == null)
                        {
                            this.OnInterruptLastUnsubscribed();
                        }
                    }
                }

                /// <summary>
                /// Releases all resources used by the interface.
                /// </summary>
                public virtual void Dispose() { }
            }

            /// <summary>
            /// Provides the PWM output functionality.
            /// </summary>
            public abstract class PwmOutput : IDisposable
            {
                /// <summary>
                /// Gets or sets a Boolean value that indicates whether the PWM interface is active, <b>true</b> if active otherwise <b>false</b>.
                /// </summary>
                public abstract bool IsActive { get; set; }
                /// <summary>
                /// Sets the frequency and duty cycle of the <see cref="PwmOutput" /> interface and starts the PWM signal.
                /// </summary>
                /// <param name="frequency">Required frequency in Hertz.</param>
                /// <param name="dutyCycle">Duty cycle from 0-1.</param>
                public abstract void Set(double frequency, double dutyCycle);
                /// <summary>
                /// Sets the period and high time of the <see cref="PWMOutput" /> interface and starts the PWM signal.
                /// </summary>
                /// <param name="period">Period of the signal in units specified <paramref name="factor" />.</param>
                /// <param name="highTime">High time of the signal in units specified by <paramref name="factor" />.</param>
                /// <param name="factor">The units of <paramref name="period" /> and <paramref name="highTime" />.</param>
                public abstract void Set(uint period, uint highTime, PwmScaleFactor factor);

                /// <summary>
                /// Releases all resources used by the interface.
                /// </summary>
                public virtual void Dispose() { }
            }

            /// <summary>
            /// Provides the SPI functionality.
            /// </summary>
            public abstract class Spi : IDisposable
            {
                /// <summary>
                /// Gets or sets the configuration of this <see cref="Spi" /> interface.
                /// </summary>
                public abstract SpiConfiguration Configuration { get; set; }
                /// <summary>
                /// Writes an array of bytes to the interface, and reads an array of bytes from the interface into a specified location in the read buffer.
                /// </summary>
                /// <param name="writeBuffer">The buffer whose contents will be written to the interface.</param>
                /// <param name="writeOffset">The offset in <paramref name="writeBuffer" /> to start write data from.</param>
                /// <param name="writeLength">The number of elements in <paramref name="writeBuffer" /> to write.</param>
                /// <param name="readBuffer">The buffer that will store the data that is read from the interface.</param>
                /// <param name="readOffset">The offset in <paramref name="readBuffer" /> to start read data from.</param>
                /// <param name="readLength">The number of elements in <paramref name="readBuffer" /> to fill.</param>
                /// <param name="startReadOffset">The offset in time, measured in transacted elements from <paramref name="writeBuffer" />, when to start reading back data into <paramref name="readBuffer" />.</param>
                public abstract void WriteRead(byte[] writeBuffer, int writeOffset, int writeLength, byte[] readBuffer, int readOffset, int readLength, int startReadOffset);
                /// <summary>
                /// Writes an array of unsigned values to the interface, and reads an array of unsigned values from the interface into a specified location in the read buffer.
                /// </summary>
                /// <param name="writeBuffer">The buffer whose contents will be written to the interface.</param>
                /// <param name="writeOffset">The offset in <paramref name="writeBuffer" /> to start write data from.</param>
                /// <param name="writeLength">The number of elements in <paramref name="writeBuffer" /> to write.</param>
                /// <param name="readBuffer">The buffer that will store the data that is read from the interface.</param>
                /// <param name="readOffset">The offset in <paramref name="readBuffer" /> to start read data from.</param>
                /// <param name="readLength">The number of elements in <paramref name="readBuffer" /> to fill.</param>
                /// <param name="startReadOffset">The offset in time, measured in transacted elements from <paramref name="writeBuffer" />, when to start reading back data into <paramref name="readBuffer" />.</param>
                public abstract void WriteRead(ushort[] writeBuffer, int writeOffset, int writeLength, ushort[] readBuffer, int readOffset, int readLength, int startReadOffset);


                /// <summary>
                /// Writes an array of bytes to the SPI interface, and reads an array of bytes from the interface into a 
                /// specified location in the read buffer.
                /// </summary>
                /// <param name="writeBuffer">The buffer that will write to the interface.</param>
                /// <param name="readBuffer">The buffer that will store the data that is read from the interface.</param>
                /// <param name="readOffset">
                ///  The offset in time, measured in transacted elements from <paramref name="writeBuffer"/>, 
                ///  to start reading data into.<paramref name="readBuffer"/>.
                /// </param>
                public void WriteRead(byte[] writeBuffer, byte[] readBuffer, int readOffset)
                {
                    WriteRead(writeBuffer, 0, writeBuffer == null ? 0 : writeBuffer.Length, readBuffer, 0, readBuffer == null ? 0 : readBuffer.Length, readOffset);
                }

                /// <summary>
                /// Writes an array of bytes to the SPI interface, and reads an array of 
                /// bytes from the interface into a specified location in the read buffer. 
                /// </summary>
                /// <param name="writeBuffer">The buffer that will write to the interface.</param>
                /// <param name="readBuffer">The buffer that will store the data that is read from the interface.</param>
                /// <param name="readOffset">
                ///  The offset in time, measured in transacted elements from <paramref name="writeBuffer"/>, 
                ///  to start reading data into <paramref name="readBuffer"/>.
                /// </param>
                public void WriteRead(ushort[] writeBuffer, ushort[] readBuffer, int readOffset)
                {
                    WriteRead(writeBuffer, 0, writeBuffer == null ? 0 : writeBuffer.Length, readBuffer, 0, readBuffer == null ? 0 : readBuffer.Length, readOffset);
                }

                /// <summary>
                ///  Writes an array of bytes to the interface, and reads an array of bytes from the interface.
                /// </summary>
                /// <param name="writeBuffer">The buffer that will write to the interface.</param>
                /// <param name="readBuffer">The buffer that will store the data that is read from the interface.</param>
                public void WriteRead(byte[] writeBuffer, byte[] readBuffer)
                {
                    WriteRead(writeBuffer, 0, writeBuffer == null ? 0 : writeBuffer.Length, readBuffer, 0, readBuffer == null ? 0 : readBuffer.Length, 0);
                }

                /// <summary>
                /// Writes an array of unsigned values to the SPI interface, and reads an array of unsigned values from the interface.
                /// </summary>
                /// <param name="writeBuffer">The buffer that will write to the interface.</param>
                /// <param name="readBuffer">The buffer that will store the data that is read from the interface.</param>
                public void WriteRead(ushort[] writeBuffer, ushort[] readBuffer)
                {
                    WriteRead(writeBuffer, 0, writeBuffer == null ? 0 : writeBuffer.Length, readBuffer, 0, readBuffer == null ? 0 : readBuffer.Length, 0);
                }

                /// <summary>
                /// Writes an array of bytes to the SPI interface.  This is a synchronous call; it will not return until the bytes are written out.
                /// </summary>
                /// <param name="writeBuffer">The buffer that will write to the interface.</param>
                public void Write(params byte[] writeBuffer)
                {
                    WriteRead(writeBuffer, 0, writeBuffer == null ? 0 : writeBuffer.Length, null, 0, 0, 0);
                }

                /// <summary>
                /// Writes an array of unsigned values to the SPI interface. This is a synchronous call; it will not return until the bytes are written out.
                /// </summary>
                /// <param name="writeBuffer">The buffer that will write to the interface.</param>
                public void Write(params ushort[] writeBuffer)
                {
                    WriteRead(writeBuffer, 0, writeBuffer == null ? 0 : writeBuffer.Length, null, 0, 0, 0);
                }

                /// <summary>
                /// Releases all resources used by the interface.
                /// </summary>
                public virtual void Dispose() { }
            }

            #endregion

            #region Delegates

            /// <summary>
            /// Represents the method that provides indirected <see cref="AnalogInput" /> interface for a socket.
            /// </summary>
            /// <param name="socket">The socket.</param>
            /// <param name="pin">The analog input pin to use.</param>
            /// <param name="module">The module using the socket, which can be null if unspecified.</param>
            /// <returns>An <see cref="AnalogInput" /> interface.</returns>
            public delegate AnalogInput AnalogInputIndirector(Socket socket, Socket.Pin pin, Module module);
            /// <summary>
            /// Represents the method that provides indirected <see cref="AnalogOutput" /> interface for a socket.
            /// </summary>
            /// <param name="socket">The analog output capable socket.</param>
            /// <param name="pin">The pin to assign to the analog output.</param>
            /// <param name="module">The module using this analog output interface, which can be null if unspecified.</param>
            /// <returns>An <see cref="AnalogOutput" /> interface.</returns>
            public delegate AnalogOutput AnalogOutputIndirector(Socket socket, Socket.Pin pin, Module module);
            /// <summary>
            /// Represents the method that provides indirected <see cref="DigitalInput" /> interface for a socket.
            /// </summary>
            /// <param name="socket">The socket for the digital input interface.</param>
            /// <param name="pin">The pin used by the digital input interface.</param>
            /// <param name="glitchFilterMode">
            ///  A value from the <see cref="GlitchFilterMode" /> enumeration that specifies 
            ///  whether to enable the glitch filter on this digital input interface.
            /// </param>
            /// <param name="resistorMode">
            ///  A value from the <see cref="ResistorMode" /> enumeration that establishes a default state for the digital input interface. N.B. .NET Gadgeteer mainboards are only required to support ResistorMode.PullUp on interruptable GPIOs and are never required to support ResistorMode.PullDown; consider putting the resistor on the module itself.
            /// </param>
            /// <param name="module">The module using this interface, which can be null if unspecified.</param>
            /// <returns>An <see cref="DigitalInput" /> interface.</returns>
            public delegate DigitalInput DigitalInputIndirector(Socket socket, Socket.Pin pin, GlitchFilterMode glitchFilterMode, ResistorMode resistorMode, Module module);
            /// <summary>
            /// Represents the method that provides indirected <see cref="DigitalIO" /> interface for a socket.
            /// </summary>
            /// <param name="socket">The socket for the digital input/output interface.</param>
            /// <param name="pin">The pin used by the digital input/output interface.</param>
            /// <param name="initialState">
            ///  The initial state to set on the digital input/output interface port.  
            ///  This value becomes effective as soon as the port is enabled as an output port.
            /// </param>
            /// <param name="glitchFilterMode">
            ///  A value from the <see cref="GlitchFilterMode" /> enumeration that specifies 
            ///  whether to enable the glitch filter on this digital input/output interface.
            /// </param>
            /// <param name="resistorMode">
            ///  A value from the <see cref="ResistorMode" /> enumeration that establishes a default state for the digital input/output interface. N.B. .NET Gadgeteer mainboards are only required to support ResistorMode.PullUp on interruptable GPIOs and are never required to support ResistorMode.PullDown; consider putting the resistor on the module itself.
            /// </param>
            /// <param name="module">The module using this interface, which can be null if unspecified.</param>
            /// <returns>An <see cref="DigitalIO" /> interface.</returns>
            public delegate DigitalIO DigitalIOIndirector(Socket socket, Socket.Pin pin, bool initialState, GlitchFilterMode glitchFilterMode, ResistorMode resistorMode, Module module);
            /// <summary>
            /// Represents the method that provides indirected <see cref="DigitalOutput" /> interface for a socket.
            /// </summary>
            /// <param name="socket">The socket for the digital output interface.</param>
            /// <param name="pin">The pin used by the digital output interface.</param>
            /// <param name="initialState">The initial state to place on the digital output interface port.</param>
            /// <param name="module">The module using this interface (which can be null if unspecified).</param>
            /// <returns>An <see cref="DigitalOutput" /> interface.</returns>
            public delegate DigitalOutput DigitalOutputIndirector(Socket socket, Socket.Pin pin, bool initialState, Module module);
            /// <summary>
            /// Represents the method that provides indirected <see cref="I2CBus" /> interface for a socket.
            /// </summary>
            /// <param name="address">The address for the I2C device.</param>
            /// <param name="clockRateKhz">The clock rate, in kHz, used when communicating with the I2C device.</param>
            /// <param name="socket">The socket for this I2C device interface.</param>
            /// <param name="module">The module using this I2C interface, which can be null if unspecified.</param>
            /// <returns>An <see cref="I2CBus" /> interface.</returns>
            public delegate I2CBus I2CBusIndirector(Socket socket, ushort address, int clockRateKhz, Module module);
            /// <summary>
            /// Represents the method that provides indirected <see cref="InterruptInput" /> interface for a socket.
            /// </summary>
            /// <param name="socket">The socket for the interrupt input interface.</param>
            /// <param name="pin">The pin used by the interrupt input interface.</param>
            /// <param name="glitchFilterMode">
            ///  A value from the <see cref="GlitchFilterMode" /> enumeration that specifies 
            ///  whether to enable the glitch filter on this interrupt input interface.
            /// </param>
            /// <param name="resistorMode">
            ///  A value from the <see cref="ResistorMode" /> enumeration that establishes a default state for the interrupt input interface. N.B. .NET Gadgeteer mainboards are only required to support ResistorMode.PullUp on interruptable GPIOs and are never required to support ResistorMode.PullDown; consider putting the resistor on the module itself.
            /// </param>
            /// <param name="interruptMode">
            ///  A value from the <see cref="InterruptMode" /> enumeration that establishes the requisite conditions 
            ///  for the interface port to generate an interrupt.
            /// </param>
            /// <param name="module">The module using this interrupt input interface, which can be null if unspecified.</param>
            /// <returns>An <see cref="InterruptInput" /> interface.</returns>
            public delegate InterruptInput InterruptInputIndirector(Socket socket, Socket.Pin pin, GlitchFilterMode glitchFilterMode, ResistorMode resistorMode, InterruptMode interruptMode, Module module);
            /// <summary>
            /// Represents the method that provides indirected <see cref="PwmOutput" /> interface for a socket.
            /// </summary>
            /// <param name="socket">The socket that supports pulse width modulation (PWM) output.</param>
            /// <param name="pin">The pin on the socket that supports PWM.</param>
            /// <param name="invert">Whether to invert the output voltage.</param>
            /// <param name="module">The module using this PWM output interface, which can be null if unspecified.</param>
            /// <returns>An <see cref="PwmOutput" /> interface.</returns>
            public delegate PwmOutput PwmOutputIndirector(Socket socket, Socket.Pin pin, bool invert, Module module);
            /// <summary>
            /// Represents the method that provides indirected <see cref="Serial" /> interface for a socket.
            /// </summary>
            /// <param name="baudRate">The baud rate for the serial port.</param>
            /// <param name="parity">A value from the <see cref="SerialParity" /> enumeration that specifies 
            /// the parity for the port.</param>
            /// <param name="stopBits">A value from the <see cref="SerialStopBits" /> enumeration that specifies 
            /// the stop bits for the port.</param>
            /// <param name="dataBits">The number of data bits.</param>
            /// <param name="socket">The socket for this serial interface.</param>
            /// <param name="hardwareFlowControlRequirement">Specifies whether the module must use hardware flow control, will use hardware flow control if available, or does not use hardware flow control.</param>
            /// <param name="module">The module using this interface (which can be null if unspecified).</param>
            /// <returns>An <see cref="Serial" /> interface.</returns>
            public delegate Serial SerialIndirector(Socket socket, int baudRate, SerialParity parity, SerialStopBits stopBits, int dataBits, HardwareFlowControl hardwareFlowControlRequirement, Module module);
            /// <summary>
            /// Represents the method that provides indirected <see cref="Spi" /> interface for a socket.
            /// </summary>
            /// <param name="socket">The socket for this SPI interface.</param>
            /// <param name="spiConfiguration">The SPI configuration.</param>
            /// <param name="sharingMode">The sharing mode.</param>
            /// <param name="chipSelectSocket">The chip select socket to use.</param>
            /// <param name="chipSelectSocketPin">The chip select pin to use on the chip select socket.</param>
            /// <param name="busySocket">The busy socket to use.</param>
            /// <param name="busySocketPin">The busy pin to use on the busy socket.</param>
            /// <param name="module">The module using this interface (which can be null if unspecified).</param>
            /// <returns>An <see cref="Spi" /> interface.</returns>
            public delegate Spi SpiIndirector(Socket socket, SpiConfiguration spiConfiguration, SpiSharing sharingMode, Socket chipSelectSocket, Socket.Pin chipSelectSocketPin, Socket busySocket, Socket.Pin busySocketPin, Module module);

            #endregion

            /// <summary>
            /// Set the scale and offset used by <see cref="Microsoft.SPOT.Hardware.AnalogInput.Read"/> to transform the raw value into a voltage between 0 and 3.3V, and the precision in bits.
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
            /// A value indicating that a socket has no associated SPI bus
            /// </summary>
            public static readonly SPI.SPI_module SPIMissing = (SPI.SPI_module)(-1);

            /// <summary>
            /// Delegate for mainboards to provide custom native/hardware I2C support. 
            /// </summary>
            /// <param name="socket">The socket.</param>
            /// <param name="sda">The socket pin for the SDA signal.</param>
            /// <param name="scl">The socket pin for the SCL signal.</param>
            /// <param name="address">The address to which to send the result.</param>
            /// <param name="write">The data buffer to write.</param>
            /// <param name="writeOffset">The offset in the buffer where writing begins (this can be null if no data is to be written).</param>
            /// <param name="writeLen">The number of bytes of data to write.</param>
            /// <param name="read">The data buffer that data is put into after a read operation (this can be null if no data is to be read).</param>
            /// <param name="readOffset">The offset to start writing data after a read operation.</param>
            /// <param name="readLen">The amount of data to read.</param>
            /// <param name="numWritten">The number of bytes actually written and acknowledged.</param>
            /// <param name="numRead">The number of bytes actually read.</param>
            /// <returns>Returns true if the whole transaction succeeds, otherwise false.</returns>
            public delegate bool NativeI2CWriteReadDelegate(Socket socket, Socket.Pin sda, Socket.Pin scl, byte address, byte[] write, int writeOffset, int writeLen, byte[] read, int readOffset, int readLen, out int numWritten, out int numRead);
        }
    }
}
