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
        /// <summary>
        /// </summary>
        /// <remarks>This automatically checks that the socket supports Type S, and reserves the pins if necessary.
        /// An exception will be thrown if there is a problem with these checks.</remarks>
        /// <param name="socket">The <see cref="Socket"/> for the <see cref="SPI"/> interface.</param>
        /// <param name="spiConfiguration">The <see cref="Configuration"/> object for the <see cref="SPI"/> interface.</param>
        /// <param name="sharingMode">The <see cref="Sharing"/> of the <see cref="SPI"/> interface.</param>
        /// <param name="module">The <see cref="Module"/> that is connected to the <see cref="SPI"/> interface.</param>
        public SPI(Socket socket, Configuration spiConfiguration, Sharing sharingMode, Module module)
            : this(socket, spiConfiguration, sharingMode, Socket.UnspecifiedPin, module)
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
            : this(socket, spiConfiguration, sharingMode, chipSelectSocket.ReservePin(chipSelectPin, module), module)
        {
        }

        private SPI(Socket socket, Configuration conf, Sharing sharingMode, Cpu.Pin chipSelectPin, Module module)
        {
            SPIInstance si = SPIInstance.GetInstance(socket, module);

            if (si.Users == 0)
            {
                socket.ReservePin(Socket.Pin.Seven, module);
                socket.ReservePin(Socket.Pin.Eight, module);
                socket.ReservePin(Socket.Pin.Nine, module);
            }
            else
            {
                if (si.SharingMode != Sharing.Shared)
                {
                    throw new Exception("SPI bus " + si + " is already reserved, cannot instantiate it again");
                }

                if (sharingMode == Sharing.Exclusive)
                {
                    throw new Exception("SPI bus " + si + " is already shared among " + si.Users + " users, cannot instantiate it for exclusive use");
                }
            }

            this.spiInstance = si;

            lock (this.spiInstance)
            {
                si.Users++;
                si.SharingMode = sharingMode;

                if (conf != null)
                {
                    this.spiConfig = new Microsoft.SPOT.Hardware.SPI.Configuration(
                        (Cpu.Pin)chipSelectPin,
                         conf.ChipSelectActiveState,
                         conf.ChipSelectSetupTime,
                         conf.ChipSelectHoldTime,
                         conf.ClockIdleState,
                         conf.ClockEdge,
                         conf.ClockRateKHz,
                         si.SPIModule);
                    if (!si.IsInitialised)
                    {
                        si.SpotSPI = new Microsoft.SPOT.Hardware.SPI(this.spiConfig);
                        si.IsInitialised = true;
                    }

                    this.spotSPI = si.SpotSPI;
                }
            }
        }

        private SPIInstance spiInstance;
        private Microsoft.SPOT.Hardware.SPI spotSPI;
        private Microsoft.SPOT.Hardware.SPI.Configuration spiConfig;

        /// <summary>
        /// Gets the underlying serial peripheral interface (SPI) module associated with this interface.
        /// </summary>
        public Microsoft.SPOT.Hardware.SPI.SPI_module SPIModule
        {
            get
            {
                return this.spiInstance.SPIModule;
            }
        }


        private void CheckSPIConfig()
        {
            if (this.spiConfig == null)
            {
                throw new Exception("Cannot directly communicate on SPI bus since no configuration was provided at init time");
            }

            if (this.spiInstance.LastSPI != this)
            {
                this.spotSPI.Config = this.spiConfig;
                this.spiInstance.LastSPI = this;
            }
        }

        /// <summary>
        /// Changes the configuration of this <see cref="SPI"/> interface.
        /// </summary>
        /// <param name="conf">The configuration to change to.</param>
        public void ChangeSpiConfig(Configuration conf)
        {
            lock (this.spiInstance)
            {
                this.spiConfig = new Microsoft.SPOT.Hardware.SPI.Configuration(
                    (Cpu.Pin)this.spiConfig.ChipSelect_Port,
                    conf.ChipSelectActiveState,
                    conf.ChipSelectSetupTime,
                    conf.ChipSelectHoldTime,
                    conf.ClockIdleState,
                    conf.ClockEdge,
                    conf.ClockRateKHz,
                    this.SPIModule);
                this.spiInstance.LastSPI = null;
            }
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
            lock (this.spiInstance)
            {
                this.CheckSPIConfig();
                this.spotSPI.WriteRead(writeBuffer, readBuffer, readOffset);
            }
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
            lock (this.spiInstance)
            {
                this.CheckSPIConfig();
                this.spotSPI.WriteRead(writeBuffer, readBuffer, readOffset);
            }
        }

        /// <summary>
        /// Writes an array of unsigned values to the SPI interface, and reads an array of unsigned values from the interface.
        /// </summary>
        /// <param name="writeBuffer">The buffer that will write to the interface.</param>
        /// <param name="readBuffer">The buffer that will store the data that is read from the interface.</param>
        public void WriteRead(ushort[] writeBuffer, ushort[] readBuffer)
        {
            this.WriteRead(writeBuffer, readBuffer, 0);
        }

        /// <summary>
        ///  Writes an array of bytes to the interface, and reads an array of bytes from the interface.
        /// </summary>
        /// <param name="writeBuffer">The buffer that will write to the interface.</param>
        /// <param name="readBuffer">The buffer that will store the data that is read from the interface.</param>
        public void WriteRead(byte[] writeBuffer, byte[] readBuffer)
        {
            this.WriteRead(writeBuffer, readBuffer, 0);
        }

        /// <summary>
        /// Writes an array of bytes to the SPI interface.  This is a synchronous call; it will not return until the bytes are written out.
        /// </summary>
        /// <param name="writeBuffer">The buffer that will write to the interface.</param>
        public void Write(byte[] writeBuffer)
        {
            lock (this.spiInstance)
            {
                this.CheckSPIConfig();
                this.spotSPI.Write(writeBuffer);
            }
        }

        /// <summary>
        /// Writes an array of bytes to the SPI interface. This is a synchronous call; it will not return until the bytes are written out.
        /// </summary>
        /// <param name="writeBuffer">The buffer that will write to the interface.</param>
        public void WriteParams(params byte[] writeBuffer)
        {
            this.Write(writeBuffer);
        }

        /// <summary>
        ///  Writes an array of unsigned values to the SPI interface. This is a synchronous call; it will not return until the bytes are written out.
        /// </summary>
        /// <param name="writeBuffer">The buffer that will write to the interface.</param>
        public void Write(ushort[] writeBuffer)
        {
            lock (this.spiInstance)
            {
                this.CheckSPIConfig();
                this.spotSPI.Write(writeBuffer);
            }
        }

        private class SPIInstance
        {
            private static ArrayList spiInstances = new ArrayList();

            public static SPIInstance GetInstance(Socket socket, Module module)
            {
                var spimodule = socket.SPIModule;
                if (spimodule == Socket.SPIMissing)
                {
                    // this is a mainboard error that should not happen (we already check for it in SocketInterfaces.RegisterSocket) but just in case...
                    throw new Socket.InvalidSocketException("Socket " + socket + " has an error with its SPI functionality. Please try a different socket.");
                }

                foreach (SPIInstance spiInstance in spiInstances)
                {
                    if (spiInstance.SPIModule == spimodule)
                    {
                        return spiInstance;
                    }
                }

                socket.EnsureTypeIsSupported('S', module);

                SPIInstance newSpiInstance = new SPIInstance(socket, spimodule);
                spiInstances.Add(newSpiInstance);
                return newSpiInstance;
            }

            public bool IsInitialised
            {
                get;
                set;
            }

            public Microsoft.SPOT.Hardware.SPI.SPI_module SPIModule
            {
                get;
                set;
            }

            public int Users
            {
                get;
                set;
            }

            public Sharing SharingMode
            {
                get;
                set;
            }

            public Microsoft.SPOT.Hardware.SPI SpotSPI
            {
                get;
                set;
            }

            public SPI LastSPI
            {
                get;
                set;
            }

            public int SocketNumber { get { return socket.SocketNumber; } }
            private Socket socket;

            private SPIInstance(Socket socket, Microsoft.SPOT.Hardware.SPI.SPI_module spimodule)
            {
                this.SPIModule = spimodule;
                this.SharingMode = Sharing.Shared;
                this.Users = 0;
                this.socket = socket;
            }

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
            public bool ChipSelectActiveState { get; private set; }

            /// <summary>
            /// Gets the chip-select setup time.
            /// </summary>
            /// <value>
            ///  The amount of time (in milliseconds) that will elapse between the time at which the device is selected and the time at which 
            ///  the clock and the clock data transmission will start.
            /// </value>
            public uint ChipSelectSetupTime { get; private set; }

            /// <summary>
            /// Gets the chip-select hold time.
            /// </summary>
            /// <value>
            ///  The amount of time (in milliseconds) that the chip-select port must remain in the active state before the device is unselected, 
            ///  or the amount of time (in milliseconds) that the chip-select will remain in the active state after the data read/write transaction 
            ///  has been completed.
            /// </value>
            public uint ChipSelectHoldTime { get; private set; }

            /// <summary>
            /// Gets the clock idle state.
            /// </summary>
            /// <value>
            ///  The idle state of the clock. If <b>true</b>, the SPI clock signal will be set to high while the device is idle; 
            ///  if <b>false</b>, the serial peripheral interface (SPI) clock signal will be set to low while the device is idle. 
            ///  The idle state occurs whenever the chip is not selected.
            /// </value>
            public bool ClockIdleState { get; private set; }

            /// <summary>
            /// Gets the sampling clock edge.
            /// </summary>
            /// <value>
            ///  The sampling clock edge. If <b>true</b>, data is sampled on the serial peripheral interface (SPI) clock rising edge; 
            ///  if <b>false</b>, the data is sampled on the SPI clock falling edge.
            /// </value>
            public bool ClockEdge { get; private set; }

            /// <summary>
            /// Gets the clock rate, in KHz.
            /// </summary>
            public uint ClockRateKHz { get; private set; }

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
    }

}
