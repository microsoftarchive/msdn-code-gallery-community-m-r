////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace Gadgeteer.Interfaces
{
    using System;
    using Microsoft.SPOT;
    using Microsoft.SPOT.Hardware;
    using System.Collections;
    using Gadgeteer.Modules;

    internal class NativeSpi : Socket.SocketInterfaces.Spi
    {
        private class HardwareInstance
        {
            internal Socket.SocketInterfaces.SpiSharing Sharing;
            internal int Users;
            internal Microsoft.SPOT.Hardware.SPI Interface;
            internal Microsoft.SPOT.Hardware.SPI.SPI_module Module;

            internal NativeSpi LastClient;

            internal HardwareInstance(Microsoft.SPOT.Hardware.SPI.SPI_module module)
            {
                Sharing = Socket.SocketInterfaces.SpiSharing.Shared;
                Users = 0;

                Module = module;
            }
        }

        private HardwareInstance _hardwareInstance;
        private Microsoft.SPOT.Hardware.SPI.Configuration _configuration;

        private Cpu.Pin _chipSelectPin;

        public NativeSpi(Socket socket, Socket.SocketInterfaces.SpiConfiguration configuration, Socket.SocketInterfaces.SpiSharing sharing, Cpu.Pin chipSelectPin, Module module, Microsoft.SPOT.Hardware.SPI.SPI_module spiModule)
        {
            _hardwareInstance = GetAndUseInstance(socket, sharing, module, spiModule);
            _chipSelectPin = chipSelectPin;
            _configuration = ToNativeConfiguration(configuration, chipSelectPin, spiModule);

            if (_hardwareInstance.Interface == null && _configuration != null)
                _hardwareInstance.Interface = new Microsoft.SPOT.Hardware.SPI(_configuration);
        }

        public override Socket.SocketInterfaces.SpiConfiguration Configuration
        {
            get { return ToInterfaceConfiguration(_configuration); }
            set { _configuration = ToNativeConfiguration(value, _chipSelectPin, _hardwareInstance.Module); }
        }

        private static Microsoft.SPOT.Hardware.SPI.Configuration ToNativeConfiguration(Socket.SocketInterfaces.SpiConfiguration config, Cpu.Pin chipSelectPin, Microsoft.SPOT.Hardware.SPI.SPI_module spiModule)
        {
            if (config == null)
                return new Microsoft.SPOT.Hardware.SPI.Configuration(chipSelectPin, false, 0, 0, false, false, 0, spiModule);

            return new Microsoft.SPOT.Hardware.SPI.Configuration(
                chipSelectPin,
                config.IsChipSelectActiveHigh,
                config.ChipSelectSetupTime,
                config.ChipSelectHoldTime,
                config.IsClockIdleHigh,
                config.IsClockSamplingEdgeRising,
                config.ClockRateKHz,
                spiModule);
        }
        private static Socket.SocketInterfaces.SpiConfiguration ToInterfaceConfiguration(Microsoft.SPOT.Hardware.SPI.Configuration config)
        {
            if (config == null)
                return null;

            return new Socket.SocketInterfaces.SpiConfiguration(
                config.ChipSelect_ActiveState,
                config.ChipSelect_SetupTime,
                config.ChipSelect_HoldTime,
                config.Clock_IdleState,
                config.Clock_Edge,
                config.Clock_RateKHz,
                false);
        }

        public override void WriteRead(byte[] writeBuffer, int writeOffset, int writeCount, byte[] readBuffer, int readOffset, int readCount, int startReadOffset)
        {
            lock (_hardwareInstance)
            {
                if (_hardwareInstance.LastClient != this)
                {
                    if (_configuration == null)
                        throw new InvalidOperationException("Cannot directly communicate on SPI bus since no configuration was provided.");

                    _hardwareInstance.Interface.Config = _configuration;
                    _hardwareInstance.LastClient = this;
                }

                _hardwareInstance.Interface.WriteRead(writeBuffer, writeOffset, writeCount, readBuffer, readOffset, readCount, startReadOffset);
            }
        }

        public override void WriteRead(ushort[] writeBuffer, int writeOffset, int writeCount, ushort[] readBuffer, int readOffset, int readCount, int startReadOffset)
        {
            lock (_hardwareInstance)
            {
                if (_hardwareInstance.LastClient != this)
                {
                    if (_configuration == null)
                        throw new InvalidOperationException("Cannot directly communicate on SPI bus since no configuration was provided.");

                    _hardwareInstance.Interface.Config = _configuration;
                    _hardwareInstance.LastClient = this;
                }

                _hardwareInstance.Interface.WriteRead(writeBuffer, writeOffset, writeCount, readBuffer, readOffset, readCount, startReadOffset);
            }
        }

        private static ArrayList Instances = new ArrayList();

        private static HardwareInstance GetAndUseInstance(Socket socket, Socket.SocketInterfaces.SpiSharing sharing, Module module, Microsoft.SPOT.Hardware.SPI.SPI_module spiModule)
        {
            if (spiModule == Socket.SocketInterfaces.SPIMissing)
            {
                // this is a mainboard error that should not happen (we already check for it in SocketInterfaces.RegisterSocket) but just in case...
                throw Socket.InvalidSocketException.FunctionalityException(socket, "SPI");
            }

            HardwareInstance foundInstance = null;

            for (int i = 0; i < Instances.Count; i++)
            {
                HardwareInstance instance = (HardwareInstance)Instances[i];

                if (instance.Module == spiModule)
                    foundInstance = instance;
            }

            if (foundInstance == null)
                Instances.Add(foundInstance = new HardwareInstance(spiModule));

            if (foundInstance.Users == 0)
            {
                socket.ReservePin(Socket.Pin.Seven, module);
                socket.ReservePin(Socket.Pin.Eight, module);
                socket.ReservePin(Socket.Pin.Nine, module);
            }
            else
            {
                if (foundInstance.Sharing != Socket.SocketInterfaces.SpiSharing.Shared)
                    throw new Exception("SPI bus " + spiModule + " is already reserved, cannot instantiate it again.");

                if (foundInstance.Sharing == Socket.SocketInterfaces.SpiSharing.Exclusive)
                    throw new Exception("SPI bus " + spiModule + " is already shared among " + foundInstance.Users + " users, cannot instantiate it for exclusive use.");
            }

            lock (foundInstance)
            {
                foundInstance.Users++;
                foundInstance.Sharing = sharing;
            }

            return foundInstance;
        }
    }

    /// <summary>
    ///  Represents a Microwire/Serial Peripheral Interface (SPI) interface to communicate with a Microwire/SPI compatible device. 
    /// </summary>
    /// <remarks>
    /// <para>
    ///  The Microwire/SPI interface is a synchronous serial communications protocol in which multiple devices 
    ///  can be connected with one another by means of a single three-wire system. This three-wire system includes 
    ///  the serial data in the input signal, the serial data out signal, and the serial clock. You must use an additional GPIO 
    ///  pin as a chip select for each device that will communicate on the Microwire/SPI interface.
    /// </para>
    /// <para>
    ///  There are both 8-bit and 16-bit modes of operation using the overloaded read and write methods that have 
    ///  byte (8-bit) arguments or unsigned short (16-bit) arguments. Note that you can configure both 8-bit 
    ///  and 16-bit devices and have them share the SPI interface.
    /// </para>
    /// </remarks>
    public class SPI
    {
        internal readonly Socket.SocketInterfaces.Spi Interface;
        private Microsoft.SPOT.Hardware.SPI.SPI_module _spiModule;

        /// <summary>
        /// Gets the underlying serial peripheral interface (SPI) module associated with this interface.
        /// </summary>
        public Microsoft.SPOT.Hardware.SPI.SPI_module SPIModule
        {
            get { return _spiModule; }
        }

        /// <summary>
        /// </summary>
        /// <remarks>This automatically checks that the socket supports Type S, and reserves the pins if necessary.
        /// An exception will be thrown if there is a problem with these checks.</remarks>
        /// <param name="socket">The <see cref="Socket"/> for the <see cref="SPI"/> interface.</param>
        /// <param name="spiConfiguration">The <see cref="Configuration"/> object for the <see cref="SPI"/> interface.</param>
        /// <param name="sharingMode">The <see cref="Sharing"/> of the <see cref="SPI"/> interface.</param>
        /// <param name="module">The <see cref="Module"/> that is connected to the <see cref="SPI"/> interface.</param>
        public SPI(Socket socket, Configuration spiConfiguration, Sharing sharingMode, Module module)
            : this(socket, spiConfiguration, sharingMode, null, default(Socket.Pin), module)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="socket">The <see cref="Socket"/> for the <see cref="SPI"/> interface.</param>
        /// <param name="spiConfiguration">The <see cref="Configuration"/> object for the <see cref="SPI"/> interface.</param>
        /// <param name="sharingMode">The <see cref="Sharing"/> mode of the <see cref="SPI"/> interface.</param>
        /// <param name="chipSelectSocket">The chip select <see cref="Socket"/>of the <see cref="SPI"/> interface.</param>
        /// <param name="chipSelectPin">The <see cref="Socket"/></param>
        /// <param name="module">The <see cref="Module"/> that is connected to the <see cref="SPI"/> interface.</param>
        public SPI(Socket socket, Configuration spiConfiguration, Sharing sharingMode, Socket chipSelectSocket, Socket.Pin chipSelectPin, Module module)
        {
            socket.EnsureTypeIsSupported('S', module);

            Cpu.Pin reservedSelectPin = Socket.UnspecifiedPin;

            if (chipSelectSocket != null)
                reservedSelectPin = chipSelectSocket.ReservePin(chipSelectPin, module);

            if (socket.SPIModule != Socket.SocketInterfaces.SPIMissing)
                    Interface = new NativeSpi(socket, spiConfiguration == null ? null : new Socket.SocketInterfaces.SpiConfiguration(
                        spiConfiguration.ChipSelectActiveState,
                        spiConfiguration.ChipSelectSetupTime,
                        spiConfiguration.ChipSelectHoldTime,
                        spiConfiguration.ClockIdleState,
                        spiConfiguration.ClockEdge,
                        spiConfiguration.ClockRateKHz,
                        false), (Socket.SocketInterfaces.SpiSharing)sharingMode, reservedSelectPin, module, socket.SPIModule);

            else
                Interface = socket.SpiIndirector(socket, spiConfiguration == null ? null : new Socket.SocketInterfaces.SpiConfiguration(
                    spiConfiguration.ChipSelectActiveState,
                    spiConfiguration.ChipSelectSetupTime,
                    spiConfiguration.ChipSelectHoldTime,
                    spiConfiguration.ClockIdleState,
                    spiConfiguration.ClockEdge,
                    spiConfiguration.ClockRateKHz,
                    false), (Socket.SocketInterfaces.SpiSharing)sharingMode, chipSelectSocket, chipSelectPin, null, default(Socket.Pin), module);

            _spiModule = socket.SPIModule;
        }

        /// <summary>
        /// Changes the configuration of this <see cref="SPI"/> interface.
        /// </summary>
        /// <param name="conf">The configuration to change to.</param>
        public void ChangeSpiConfig(Configuration conf)
        {
            Interface.Configuration = new Socket.SocketInterfaces.SpiConfiguration(
                        conf.ChipSelectActiveState,
                        conf.ChipSelectSetupTime,
                        conf.ChipSelectHoldTime,
                        conf.ClockIdleState,
                        conf.ClockEdge,
                        conf.ClockRateKHz,
                        false);
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
            Interface.WriteRead(writeBuffer, 0, writeBuffer == null ? 0 : writeBuffer.Length, readBuffer, 0, readBuffer == null ? 0 : readBuffer.Length, 0);            
        }

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
            Interface.WriteRead(writeBuffer, 0, writeBuffer == null ? 0 : writeBuffer.Length, readBuffer, 0, readBuffer == null ? 0 : readBuffer.Length, 0);            
        }
        /// <summary>
        /// Writes an array of unsigned values to the SPI interface, and reads an array of unsigned values from the interface.
        /// </summary>
        /// <param name="writeBuffer">The buffer that will write to the interface.</param>
        /// <param name="readBuffer">The buffer that will store the data that is read from the interface.</param>
        public void WriteRead(ushort[] writeBuffer, ushort[] readBuffer)
        {
            Interface.WriteRead(writeBuffer, readBuffer, 0);
        }

        /// <summary>
        ///  Writes an array of bytes to the interface, and reads an array of bytes from the interface.
        /// </summary>
        /// <param name="writeBuffer">The buffer that will write to the interface.</param>
        /// <param name="readBuffer">The buffer that will store the data that is read from the interface.</param>
        public void WriteRead(byte[] writeBuffer, byte[] readBuffer)
        {
            Interface.WriteRead(writeBuffer, readBuffer, 0);
        }

        /// <summary>
        /// Writes an array of bytes to the SPI interface.  This is a synchronous call; it will not return until the bytes are written out.
        /// </summary>
        /// <param name="writeBuffer">The buffer that will write to the interface.</param>
        public void Write(byte[] writeBuffer)
        {
            Interface.WriteRead(writeBuffer, null, 0);
        }

        /// <summary>
        /// Writes an array of bytes to the SPI interface. This is a synchronous call; it will not return until the bytes are written out.
        /// </summary>
        /// <param name="writeBuffer">The buffer that will write to the interface.</param>
        public void WriteParams(params byte[] writeBuffer)
        {
            Interface.WriteRead(writeBuffer, null, 0);
        }

        /// <summary>
        ///  Writes an array of unsigned values to the SPI interface. This is a synchronous call; it will not return until the bytes are written out.
        /// </summary>
        /// <param name="writeBuffer">The buffer that will write to the interface.</param>
        public void Write(ushort[] writeBuffer)
        {
            Interface.WriteRead(writeBuffer, null, 0);
        }

        /// <summary>
        /// Represents the configuration for an <see cref="SPI"/> interface.
        /// </summary>
        public class Configuration
        {
            /// <summary>
            /// Gets the chip select active state.
            /// </summary>
            /// <value>
            ///  The active state for the chip-select port. If <b>true</b>, the chip-select port will be 
            ///  set to high when accessing the chip; 
            ///  if <b>false</b>, the chip-select port will be set to low when accessing the chip.
            /// </value>
            public readonly bool ChipSelectActiveState;

            /// <summary>
            /// Gets the chip-select setup time.
            /// </summary>
            /// <value>
            ///  The amount of time (in milliseconds) that will elapse between the time at which the device is selected and the time at which 
            ///  the clock and the clock data transmission will start.
            /// </value>
            public readonly uint ChipSelectSetupTime;

            /// <summary>
            /// Gets the chip-select hold time.
            /// </summary>
            /// <value>
            ///  The amount of time (in milliseconds) that the chip-select port must remain in the active state before the device is unselected, 
            ///  or the amount of time (in milliseconds) that the chip-select will remain in the active state after the data read/write transaction 
            ///  has been completed.
            /// </value>
            public readonly uint ChipSelectHoldTime;

            /// <summary>
            /// Gets the clock idle state.
            /// </summary>
            /// <value>
            ///  The idle state of the clock. If <b>true</b>, the SPI clock signal will be set to high while the device is idle; 
            ///  if <b>false</b>, the serial peripheral interface (SPI) clock signal will be set to low while the device is idle. 
            ///  The idle state occurs whenever the chip is not selected.
            /// </value>
            public readonly bool ClockIdleState;

            /// <summary>
            /// Gets the sampling clock edge.
            /// </summary>
            /// <value>
            ///  The sampling clock edge. If <b>true</b>, data is sampled on the serial peripheral interface (SPI) clock rising edge; 
            ///  if <b>false</b>, the data is sampled on the SPI clock falling edge.
            /// </value>
            public readonly bool ClockEdge;

            /// <summary>
            /// Gets the clock rate, in KHz.
            /// </summary>
            public readonly uint ClockRateKHz;

            // Note: A constructor summary is auto-generated by the doc builder.
            /// <summary></summary>
            /// <param name="chipSelectActiveState">
            ///  The active state for the chip-select port. If <b>true</b>, the chip-select port will be set to high when 
            ///  accessing the chip; if <b>false</b>, the chip select port will be set to low when accessing the chip.
            /// </param>
            /// <param name="chipSelectSetupTime">
            ///  The amount of time (in milliseconds) that will elapse between the time at which the device is selected and the time at which 
            ///  the clock and the clock data transmission will start.
            /// </param>
            /// <param name="chipSelectHoldTime">
            ///  The amount of time (in milliseconds) that the chip select port must remain in the active state before the device is unselected, 
            ///  or the amount of time (in milliseconds) that the chip select will remain in the active state after the data read/write transaction 
            ///  has been completed.
            /// </param>
            /// <param name="clockIdleState">
            ///  The idle state of the clock. If <b>true</b>, the serial peripheral interface (SPI) clock signal will be set to high while the device is idle; 
            ///  if <b>false</b>, the SPI clock signal will be set to low while the device is idle. The idle state occurs whenever 
            ///  the chip is not selected.
            /// </param>
            /// <param name="clockEdge">
            ///  The sampling clock edge. If <b>true</b>, data is sampled on the SPI clock rising edge; 
            ///  if <b>false</b>, the data is sampled on the SPI clock falling edge.
            /// </param>
            /// <param name="clockRateKHz">The SPI clock rate in kHz.</param>
            public Configuration(bool chipSelectActiveState, uint chipSelectSetupTime, uint chipSelectHoldTime, bool clockIdleState, bool clockEdge, uint clockRateKHz)
            {
                this.ChipSelectActiveState = chipSelectActiveState;
                this.ChipSelectSetupTime = chipSelectSetupTime;
                this.ChipSelectHoldTime = chipSelectHoldTime;
                this.ClockIdleState = clockIdleState;
                this.ClockEdge = clockEdge;
                this.ClockRateKHz = clockRateKHz;
            }
        }

        /// <summary>
        /// Provides values to specify the sharing mode for an <see cref="SPI"/> instance.
        /// </summary>
        public enum Sharing
        {
            /// <summary>
            /// Exclusive, no sharing allowed.
            /// </summary>
            Exclusive,
            /// <summary>
            /// Sharing is allowed.
            /// </summary>
            Shared,
            /// <summary>
            /// No more interfaces may share.
            /// </summary>
            NoMoreAllowed
        }

        /// <summary>
        /// Returns the <see cref="Socket.SocketInterfaces.Spi" /> for an <see cref="SPI" /> interface.
        /// </summary>
        /// <param name="this">An instance of <see cref="SPI" />.</param>
        /// <returns>The <see cref="Socket.SocketInterfaces.Spi" /> for <paramref name="this"/>.</returns>
        public static explicit operator Socket.SocketInterfaces.Spi(SPI @this)
        {
            return @this.Interface;
        }
    }
}
