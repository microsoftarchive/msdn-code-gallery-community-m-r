////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace Gadgeteer.SocketInterfaces
{
    using System;
    using System.Collections;
    using Gadgeteer.Modules;
    using Hardware = Microsoft.SPOT.Hardware;

    internal class NativeSpi : Spi
    {
        private class HardwareInstance
        {
            internal SpiSharing Sharing;
            internal int Users;
            internal Hardware.SPI Interface;
            internal Hardware.SPI.SPI_module Module;

            internal NativeSpi LastClient;

            internal HardwareInstance(Hardware.SPI.SPI_module module)
            {
                Sharing = SpiSharing.Shared;
                Users = 0;

                Module = module;
            }
        }

        private HardwareInstance _hardwareInstance;
        private Hardware.SPI.Configuration _configuration;

        private Hardware.Cpu.Pin _chipSelectPin;
        private Hardware.Cpu.Pin _busyPin;

        public NativeSpi(Socket socket, SpiConfiguration configuration, SpiSharing sharing, Hardware.Cpu.Pin chipSelectPin, Hardware.Cpu.Pin busyPin, Module module, Hardware.SPI.SPI_module spiModule)
        {
            _hardwareInstance = GetAndUseInstance(socket, sharing, module, spiModule);
            _chipSelectPin = chipSelectPin;
            _busyPin = busyPin;
            _configuration = ToNativeConfiguration(configuration, chipSelectPin, busyPin, spiModule);

            if (_hardwareInstance.Interface == null && _configuration != null)
                _hardwareInstance.Interface = new Hardware.SPI(_configuration);
        }

        public override SocketInterfaces.SpiConfiguration Configuration
        {
            get { return ToInterfaceConfiguration(_configuration); }
            set { _configuration = ToNativeConfiguration(value, _chipSelectPin, _busyPin, _hardwareInstance.Module); }
        }

        private static Microsoft.SPOT.Hardware.SPI.Configuration ToNativeConfiguration(SpiConfiguration config, Hardware.Cpu.Pin chipSelectPin, Hardware.Cpu.Pin busyPin, Hardware.SPI.SPI_module spiModule)
        {
            if (config == null)
                return new Microsoft.SPOT.Hardware.SPI.Configuration(chipSelectPin, false, 0, 0, false, false, 0, spiModule, busyPin, false);

            return new Microsoft.SPOT.Hardware.SPI.Configuration(
                chipSelectPin,
                config.IsChipSelectActiveHigh,
                config.ChipSelectSetupTime,
                config.ChipSelectHoldTime,
                config.IsClockIdleHigh,
                config.IsClockSamplingEdgeRising,
                config.ClockRateKHz,
                spiModule,
                busyPin,
                config.IsBusyActiveHigh);
        }
        private static SocketInterfaces.SpiConfiguration ToInterfaceConfiguration(Hardware.SPI.Configuration config)
        {
            if (config == null)
                return null;

            return new SocketInterfaces.SpiConfiguration(
                config.ChipSelect_ActiveState,
                config.ChipSelect_SetupTime,
                config.ChipSelect_HoldTime,
                config.Clock_IdleState,
                config.Clock_Edge,
                config.Clock_RateKHz,
                config.BusyPin_ActiveState);
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

        private static HardwareInstance GetAndUseInstance(Socket socket, SpiSharing sharing, Module module, Hardware.SPI.SPI_module spiModule)
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
                if (foundInstance.Sharing != SocketInterfaces.SpiSharing.Shared)
                    throw new Exception("SPI bus " + spiModule + " is already reserved, cannot instantiate it again.");

                if (foundInstance.Sharing == SocketInterfaces.SpiSharing.Exclusive)
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
}