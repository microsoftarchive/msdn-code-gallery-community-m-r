////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace Gadgeteer.SocketInterfaces
{
    using System;
    using System.Collections;
    using Gadgeteer.Modules;
    using Hardware = Microsoft.SPOT.Hardware;

    internal class NativeI2CBus : I2CBus
    {
        private static Hardware.I2CDevice _device;

        private Hardware.I2CDevice.Configuration _configuration;
        private int timeout = 1000;

        public NativeI2CBus(Socket socket, ushort address, int clockRateKhz, Module module)
        {
            if (_device == null)
                _device = new Hardware.I2CDevice(new Hardware.I2CDevice.Configuration(0, 50));

            _configuration = new Hardware.I2CDevice.Configuration(address, clockRateKhz);
        }

        public override ushort Address
        {
            get { return _configuration.Address; }
            set { _configuration = new Hardware.I2CDevice.Configuration(value, this._configuration.ClockRateKhz); }
        }

        public override int Timeout
        {
            get { return timeout; }
            set { timeout = value; }
        }

        public override int ClockRateKHz
        {
            get { return _configuration.ClockRateKhz; }
            set { _configuration = new Hardware.I2CDevice.Configuration(this._configuration.Address, value); }
        }

        public override int Execute(params Hardware.I2CDevice.I2CTransaction[] transactions)
        {
            int transacted;

            lock (_device)
            {
                _device.Config = _configuration;
                transacted = _device.Execute(transactions, this.timeout);
            }

            if (this.LengthErrorBehavior == ErrorBehavior.ThrowException)
            {
                int count = 0;
                for (int i = 0; i < transactions.Length; i++)
                    count += transactions[i].Buffer.Length;

                if (count != transacted)
                    throw NewLengthErrorException();
            }

            return transacted;
        }

        public override void WriteRead(byte[] writeBuffer, int writeOffset, int writeLength, byte[] readBuffer, int readOffset, int readLength, out int numWritten, out int numRead)
        {
            numWritten = 0;
            numRead = 0;

            if (readLength == 0 && writeLength == 0)
            {
                return;
            }
            else if (readLength == 0 && writeOffset == 0)
            {
                numWritten = Execute(Hardware.I2CDevice.CreateWriteTransaction(writeBuffer));
            }
            else if (writeLength == 0 && readOffset == 0)
            {
                numRead = Execute(Hardware.I2CDevice.CreateReadTransaction(readBuffer));
            }
            else if (readOffset == 0 && writeOffset == 0)
            {
                int bytes = Execute(Hardware.I2CDevice.CreateWriteTransaction(writeBuffer), Hardware.I2CDevice.CreateReadTransaction(readBuffer));
                numWritten = Math.Min(bytes, writeLength);
                numRead = bytes - numWritten;
            }
            else
            {
                Hardware.I2CDevice.I2CTransaction writeTransaction = null;
                Hardware.I2CDevice.I2CTransaction readTransaction = null;
                
                Hardware.I2CDevice.I2CTransaction[] transactions = new Hardware.I2CDevice.I2CTransaction[1];

                int count = 0;
                if (writeLength > 0)
                {
                    transactions[0] = writeTransaction = Hardware.I2CDevice.CreateWriteTransaction(writeBuffer);
                    count++;
                }
                if (readLength > 0)
                {
                    if (count > 0)
                    {
                        transactions = new Hardware.I2CDevice.I2CTransaction[2];
                        transactions[0] = writeTransaction;
                    }

                    transactions[count] = readTransaction = Hardware.I2CDevice.CreateReadTransaction(readBuffer);
                }

                int bytes = Execute(transactions);
                numWritten = Math.Min(bytes, writeLength);
                numRead = bytes - numWritten;
            }
        }
    }
}