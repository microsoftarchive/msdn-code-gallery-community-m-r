////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace Gadgeteer.SocketInterfaces
{
    using System;
    using Hardware = Microsoft.SPOT.Hardware;

    /// <summary>
    /// Provides the I2C functionality.
    /// </summary>
    public abstract class I2CBus : System.IDisposable
    {
        /// <summary>
        /// Gets or sets the number of milliseconds before a time-out occurs.
        /// </summary>
        public abstract int Timeout { get; set; }
        
        /// <summary>
        /// Gets or sets the address of the <see cref="I2CBus" /> device.
        /// </summary>
        public abstract ushort Address { get; set; }

        /// <summary>
        /// Gets or sets the clock speed of the <see cref="I2CBus" /> device.
        /// </summary>
        public abstract int ClockRateKHz { get; set; }

        /// <summary>
        /// The behavior of handling length errors.
        /// </summary>
        public ErrorBehavior LengthErrorBehavior;

        /// <summary>
        /// Writes an array of bytes and then reads an array of bytes from/to an I2C device.
        /// </summary>
        /// <param name="writeBuffer">The array of data to write to the device.</param>
        /// <param name="writeOffset">The index of the first byte in the "writeBuffer" array to be written.</param>
        /// <param name="writeLength">The number of bytes from the "writeBuffer" array to be written.</param>
        /// <param name="readBuffer">The array that will hold data read from the device.</param>
        /// <param name="readOffset">The index of the first location in the "readBuffer" array to be written to.</param>
        /// <param name="readLength">The number of bytes that will be written to the "readBuffer" array.</param>
        /// <param name="numWritten">The number of bytes actually written to the device.</param>
        /// <param name="numRead">The number of bytes actually read from the device.</param>
        public abstract void WriteRead(byte[] writeBuffer, int writeOffset, int writeLength, byte[] readBuffer, int readOffset, int readLength, out int numWritten, out int numRead);
        
        /// <summary>
        /// Writes an array of bytes and then reads an array of bytes from/to an I2C device.
        /// </summary>
        /// <param name="writeBuffer">The array of data to write to the device.</param>
        /// <param name="readBuffer">The array that will hold data read from the device.</param>
        /// <returns>The total number of bytes transferred in the transaction.</returns>
        public int WriteRead(byte[] writeBuffer, byte[] readBuffer)
        {
            int writeLength = writeBuffer == null ? 0 : writeBuffer.Length;
            int readLength = readBuffer == null ? 0 : readBuffer.Length;

            int written, read;
            WriteRead(writeBuffer, 0, writeLength, readBuffer, 0, readLength, out written, out read);

            if (written < 0) return written;
            if (read < 0) return read;
            
            return written + read;
        }

        /// <summary>
        /// Writes an array of bytes to an I2C device.
        /// </summary>
        /// <param name="writeBuffer">The array of bytes that will be written to the I2C device.</param>
        /// <returns>The number of bytes written to the device.</returns>
        public int Write(params byte[] writeBuffer)
        {
            return WriteRead(writeBuffer, null);
        }

        /// <summary>
        /// Reads an array of bytes from an I2C device.
        /// </summary>
        /// <param name="readBuffer">The array of bytes that will be read from the I2C device.</param>
        /// <returns>The number of bytes read from the device.</returns>
        public int Read(byte[] readBuffer)
        {
            return WriteRead(null, readBuffer);
        }

        /// <summary>
        /// Reads a register from a I2C device using a memory map API.
        /// </summary>
        /// <param name="register">The single byte to write to the device (normally the register address on the device).</param>
        /// <returns>The single byte read from the device.</returns>
        public byte ReadRegister(byte register)
        {
            byte[] buffer = new byte[1] { register };

            if (WriteRead(buffer, buffer) > 0)
                return buffer[0];

            return 0;
        }

        /// <summary>
        /// Performs a series of I2C transactions. 
        /// </summary>
        /// <remarks>
        /// This is a more advanced API for when <see cref="Write"/> and <see cref="Read"/> methods do not suffice. You may wish to use <see cref="Hardware.I2CDevice.CreateWriteTransaction"/> and <see cref="Hardware.I2CDevice.CreateReadTransaction"/> to create the transactions.
        /// </remarks>
        /// <param name="transactions">The list of transactions to perform.</param>
        /// <returns>The number of bytes successfully transacted.</returns>
        public virtual int Execute(params Hardware.I2CDevice.I2CTransaction[] transactions)
        {
            if (transactions == null)
                return 0;

            int totalBytes = 0;

            for (int i = 0; i < transactions.Length; i++)
            {
                int length = transactions[i].Buffer == null ? 0 : transactions[i].Buffer.Length;
                int written, read;

                if (i + 1 < transactions.Length &&
                    transactions[i] is Hardware.I2CDevice.I2CWriteTransaction &&
                    transactions[i + 1] is Hardware.I2CDevice.I2CReadTransaction)
                {
                    WriteRead(transactions[i].Buffer, 0, transactions[i].Buffer.Length, transactions[i + 1].Buffer, 0, transactions[i + 1].Buffer.Length, out written, out read);
                    i++;

                    if (this.LengthErrorBehavior == ErrorBehavior.ThrowException && (written != length || read != transactions[i + 1].Buffer.Length))
                        throw NewLengthErrorException();
                }
                else if (transactions[i] is Hardware.I2CDevice.I2CWriteTransaction)
                {
                    WriteRead(transactions[i].Buffer, 0, length, null, 0, 0, out written, out read);

                    if (this.LengthErrorBehavior == ErrorBehavior.ThrowException && written != length)
                        throw NewLengthErrorException();
                }
                else // if (transactions[i] is Hardware.I2CDevice.I2CReadTransaction)
                {
                    WriteRead(null, 0, 0, transactions[i].Buffer, 0, length, out written, out read);

                    if (this.LengthErrorBehavior == ErrorBehavior.ThrowException && read != length)
                        throw NewLengthErrorException();
                }

                totalBytes += written + read;
            }

            return totalBytes;
        }

        internal Exception NewLengthErrorException()
        {
            return new ApplicationException("SoftwareI2C: Exception writing to device at address " + this.Address + " - perhaps device is not responding or not plugged in.");
        }

        /// <summary>
        /// Releases all resources used by the interface.
        /// </summary>
        public virtual void Dispose() { }
    }
}